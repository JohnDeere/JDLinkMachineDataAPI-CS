using Newtonsoft.Json;

namespace SampleApp.Sources.democlient.rest
{
    public class Serializer
    {
        public string Serialize<T>(T @object)
        {
            return JsonConvert.SerializeObject(@object);
        }

        public T Deserialize<T>(string @object)
        {
            return JsonConvert.DeserializeObject<T>(@object);
        }
    }
}