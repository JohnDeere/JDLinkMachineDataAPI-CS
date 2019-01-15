using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading;
using SampleApp.Sources.democlient.rest;
using System.Linq;

namespace SampleApp.Sources.democlient
{
    public class ApiClient
    {
        private readonly OAuthSignedRestClient _oAuthSignedRestClient;
        private readonly Serializer _serializer;

        public ApiClient(ApiCredentials credentials)
        {
            _oAuthSignedRestClient = new OAuthSignedRestClient(credentials);
            _serializer = new Serializer();
        }

        // Unless you are using Deere ETags, every API that returns a collection will be wrapped in a paginated response.
        // A CollectionPage includes a list of objects and links to the previous and next pages of data.
        public IEnumerable<T> GetAllUsingPagination<T>(string endpointUrl, Dictionary<string, string> queryParams = null)
        {
            var restRequest = new RestRequest
            {
                Url = endpointUrl,
                Method = HttpMethod.Get
            };
            if (queryParams != null)
                restRequest.QueryParameters = queryParams;
            do
            {
                var response = ExecuteWithRetry(restRequest);
                var resultPage = response.GetResponsePage<T>();

                // If the page is null, then we got an empty response. 
                // Maybe your user can't access any organizations, or maybe you requested data and there wasn't any.
                if (resultPage?.values == null)
                    yield break;
                foreach (var returnedElement in resultPage.values)
                {
                    yield return returnedElement;
                }
                restRequest.Url = resultPage.links.SingleOrDefault(link => link.rel == LinkRel.nextPage.ToString())?.uri;
            } while (restRequest.Url != null);
        }

        public Tuple<string, List<T>> GetListUsingDetag<T>(string endpointUrl, string detagValue, Dictionary<string, string> queryParams = null)
        {
            var restRequest = new RestRequest
            {
                Url = endpointUrl,
                Method = HttpMethod.Get,
                Detag = detagValue
            };
            if (queryParams != null)
                restRequest.QueryParameters = queryParams;
            var response = ExecuteWithRetry(restRequest);
            detagValue = response.Headers.Get("x-deere-signature");

            if (response.StatusCode == HttpStatusCode.NotModified)
                return Tuple.Create(detagValue, new List<T>());
            return Tuple.Create(detagValue, response.GetResponseAsList<T>());
        }

        public string PostNewObject<T>(string endpointUrl, T @object)
        {
            var restRequest = new RestRequest
            {
                Url = endpointUrl,
                Method = HttpMethod.Post,
                Payload = _serializer.Serialize(@object)
            };
            var response = ExecuteWithRetry(restRequest);
            if (response.StatusCode == HttpStatusCode.BadRequest)
                throw new WebException(response.GetBody());

            return response.Headers.Get("Location");
        }

        public void PutBinaryPayload(string endpointUrl, byte[] payload)
        {
            var restRequest = new RestRequest
            {
                Url = endpointUrl,
                Method = HttpMethod.Put,
                Accept = "application/vnd.deere.axiom.v3+json",
                ContentType = "application/octet-stream",
                BinaryPayload = payload
            };
            var response = ExecuteWithRetry(restRequest);
            if (response.StatusCode == HttpStatusCode.BadRequest)
                throw new WebException(response.GetBody());
        }

        private HttpWebResponse ExecuteWithRetry(RestRequest request)
        {
            var maxRetries = 4;
            var numberOfAttempts = 0;
            while (numberOfAttempts < maxRetries)
            {
                var response = _oAuthSignedRestClient.Execute(request);
                // See https://developer.deere.com/#!documentation&doc=myjohndeere%2F429.htm
                // Note System.Net.HttpStatusCode does not include 429, but C# enum handling allows non-specified values
                if (response.StatusCode == (HttpStatusCode)429)
                {
                    var secondsToWait = Int32.Parse(response.Headers.Get("Retry-After"));
                    Thread.Sleep(1000 * secondsToWait);
                }
                else if (response.StatusCode == HttpStatusCode.ServiceUnavailable || response.StatusCode == HttpStatusCode.InternalServerError)
                {
                    // Sorry. Something probably went wrong on our end. 
                    // Exponential backoff raises the odds that waiting a few milliseconds results in a successful retry
                    Thread.Sleep((int)Math.Pow(10, numberOfAttempts + 1));
                }
                else
                {
                    return response;
                }
                numberOfAttempts++;
            }
            return null;
        }
    }
}
