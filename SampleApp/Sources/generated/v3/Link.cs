using System;
using System.Runtime.Serialization;

namespace SampleApp.Sources.generated.v3
{
    [DataContract]
    public class Link
    {
        [DataMember]
        public string rel;
        [DataMember]
        public string uri;
        [DataMember]
        public string followable;
    }
}
