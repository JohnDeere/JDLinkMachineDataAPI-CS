using System;
using System.Collections.Generic;
using System.Net;
using DevDefined.OAuth.Consumer;
using DevDefined.OAuth.Framework;
using DevDefined.OAuth.Storage.Basic;

namespace SampleApp.Sources.democlient.rest
{
    public class OAuthSignedRestClient
    {
        private readonly ApiCredentials _credentials;

        public OAuthSignedRestClient(ApiCredentials credentials)
        {
            _credentials = credentials;
        }

        public HttpWebResponse Execute(RestRequest request)
        {
            var signedRequest = CreateSignedOauthRequest(request);
            var httpWebRequest = signedRequest.ToWebRequest();

            //Below is important as you dont want the client to redirct and get a 401 response
            httpWebRequest.AllowAutoRedirect = false;

            try
            {
                var httpWebResponse = httpWebRequest.GetResponse();
                return (HttpWebResponse)httpWebResponse;
            }
            catch (WebException ex)
            {
                return ex.Response as HttpWebResponse;
            }

        }

        private IConsumerRequest CreateSignedOauthRequest(RestRequest request)
        {
            var oAuthContext = MapRequestToOauthContext(request);
            var oAuthConsumerContext = new OAuthConsumerContext()
            {
                ConsumerKey = _credentials.ClientKey,
                ConsumerSecret = _credentials.ClientSecret,
                SignatureMethod = SignatureMethod.HmacSha1,
                UseHeaderForOAuthParameters = true
            };
            var accessToken = new AccessToken
            {
                ConsumerKey = _credentials.ClientKey,
                Token = _credentials.TokenKey,
                TokenSecret = _credentials.TokenSecret
            };

            var consumerRequest = new ConsumerRequest(oAuthContext, oAuthConsumerContext, accessToken)
                .WithAcceptHeader(request.Accept)
                .WithRawContentType(request.ContentType);
            if (!request.IncludeHateoasLinks)
                consumerRequest = consumerRequest.WithQueryParameters(new Dictionary<string, string> { { "ShowLinks", "none" } });

            return consumerRequest.SignWithToken(accessToken);
        }

        private static OAuthContext MapRequestToOauthContext(RestRequest request)
        {
            var oAuthContext = new OAuthContext
            {
                UseAuthorizationHeader = true,
                RawUri = new Uri(request.Url),
                RequestMethod = request.Method.Method,
                Headers = request.Headers.ToNameValueCollection()
            };
            if (request.BinaryPayload != null)
            {
                oAuthContext.RawContent = request.BinaryPayload;
            }

            return oAuthContext;
        }
    }
}