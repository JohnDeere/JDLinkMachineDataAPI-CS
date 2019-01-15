using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace SampleApp.Sources.generated.v3
{
    [DataContract]
    public class CollectionPage<T>
    {
        [DataMember]
        public int total;
        [DataMember]
        public List<Link> links;
        [DataMember]
        public List<T> values;
    }
}
