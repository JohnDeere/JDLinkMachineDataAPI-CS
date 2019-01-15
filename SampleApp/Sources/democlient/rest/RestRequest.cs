using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace SampleApp.Sources.democlient.rest
{
    public class RestRequest
    {
        private string _url;

        public RestRequest()
        {
            Headers = StandardV3Headers();
            QueryParameters = new Dictionary<string, string>();
        }
         
        public string Url
        {
            get => MaximumPaginationSize.HasValue ? $"{_url};count={MaximumPaginationSize.Value}" : _url;
            set => _url = value;
        }

        public HttpMethod Method { get; set; }
        public byte[] BinaryPayload { get; set; }
        public string Payload
        {
            set => BinaryPayload = Encoding.UTF8.GetBytes(value);
        }
        public Dictionary<string, string> Headers { get; }

        public bool ShouldUseDetag
        {
            set
            {
                if (value && !Headers.ContainsKey("x-deere-signature"))
                    Headers.Add("x-deere-signature", null);
                if (!value && Headers.ContainsKey("x-deere-signature"))
                    Headers.Remove("x-deere-signature");
            }
        }

        public string Detag
        {
            set => Headers["x-deere-signature"] = value;
        }

        // Accept is handled separately from the other headers because this will eventually become a System.Net.HttpWebRequest.
        // System.Net.HttpWebRequest requires that certain restricted headers be set by individual properties.
        // https://docs.microsoft.com/en-us/dotnet/api/system.net.webheadercollection.isrestricted
        // Default value: Deere's API V3 

        public string Accept { get; set; } = "application/vnd.deere.axiom.v3+json";


        // Content-Type is handled separately from the other headers because this will eventually become a System.Net.HttpWebRequest.
        // System.Net.HttpWebRequest requires that certain restricted headers be set by individual properties.
        // https://docs.microsoft.com/en-us/dotnet/api/system.net.webheadercollection.isrestricted
        // Default value: Deere's API V3
        public string ContentType { get; set; } = "application/vnd.deere.axiom.v3+json";

        // If this is false, we'll add a query parameter ShowLinks=None
        // While the links are great for discoverability, they can substantially increase the payload size and execution time of our requests.
        // See https://developer.deere.com/#!documentation&doc=myjohndeere%2Flinks.htm
        public bool IncludeHateoasLinks { get; set; } = true;

        // Any time we call an API that returns a collection, the result will be paginated. This sets the desired page size.
        // The default value (if this is never specified) is 10. The current maximum value is 100. Setting it to 100 generally improves performance.
        public int? MaximumPaginationSize { get; set; }

        public Dictionary<string, string> QueryParameters { get; set; }

        private static Dictionary<string, string> StandardV3Headers()
        {
            return new Dictionary<string, string>
            {
                // Some of our responses are quite large. You'll have a much more responsive application if you enable gzip.
                {"Accept-Encoding", "gzip"}
            };
        }
    }
}