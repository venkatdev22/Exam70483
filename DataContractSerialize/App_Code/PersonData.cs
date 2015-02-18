using System;
using System.Linq;
using System.Web;

namespace PersonModel
{
    [System.Runtime.Serialization.DataContract] //DataContract attr for WCF service. not serializeable attr.
    public class PersonData
    {
        [System.Runtime.Serialization.DataMember] //DataMember attr required to serialize. Each individual member need to have this attr to expose for serialize
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsDirty { get; set; }
    }
}