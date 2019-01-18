using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Xml.Serialization;
using SampleApp.Sources.generated.v3;

namespace SampleApp.Sources.democlient.rest
{
    public static class WebResponseExtensions
    {
        private static readonly Serializer JsonSerializer = new Serializer();

        public static string GetBody(this HttpWebResponse webResponse)
        {
            var responseStream = webResponse?.GetResponseStream();
            if (responseStream == null)
                return null;

            // The GZipStream is required because we always add an Accept-Encoding=gzip header in RestRequest
            using (var gzipStream = new GZipStream(responseStream, CompressionMode.Decompress))
            {
                using (var reader = new StreamReader(gzipStream))
                {
                    return reader.ReadToEnd();
                }
            }
        }

        public static CollectionPage<T> GetResponsePage<T>(this HttpWebResponse webResponse)
        {
            var payload = webResponse.GetBody();

            return JsonSerializer.Deserialize<CollectionPage<T>>(payload);
        }

        public static List<T> GetResponseAsList<T>(this HttpWebResponse webResponse)
        {
            var payload = webResponse.GetBody();
            var collectionPage = JsonSerializer.Deserialize<CollectionPage<T>>(payload);

            return collectionPage.values;
        }

        public static T GetResponseAsObject<T>(this HttpWebResponse webResponse)
        {
            var payload = webResponse.GetBody();

            return JsonSerializer.Deserialize<T>(payload);
        }

        public static T GetXMLResponseAsObject<T>(this HttpWebResponse webResponse, System.Type type)
        {
            var payload = webResponse.GetBody();

            XmlSerializer serializer = new XmlSerializer(type);
            using (StringReader reader = new StringReader(payload))
            {
                return (T)serializer.Deserialize(reader);
            }
        }
    }
}