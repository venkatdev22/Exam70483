using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SerializeService
{
    [System.Runtime.Serialization.DataContract]
    public class PersonData
    {
        [System.Runtime.Serialization.DataMember]
        public int Id { get; set; }

        [System.Runtime.Serialization.OnSerializing]
        public  void onSerialize(System.Runtime.Serialization.StreamingContext sContext)
        {
            Id = 101;//WORKS 
        }

        [System.Runtime.Serialization.OnSerialized]
        public  void onSerialized(System.Runtime.Serialization.StreamingContext sContext)
        {
            Id = 100; //Won't work
        }

        [System.Runtime.Serialization.OnDeserializing]
        public void onDeserialize(System.Runtime.Serialization.StreamingContext sContext)
        {
            Id = 102; //Won't work
        }
    }
}