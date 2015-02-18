using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
namespace DataContractSerialize.WebAsserts
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service.svc or Service.svc.cs at the Solution Explorer and start debugging.
    public class Service : IService
    {
        public void DoWork()
        {
            var p = new PersonData() { };
            using (var stream = new System.IO.FileStream("data.xml", System.IO.FileMode.Create))
            {
                DataContractSerializer ser = new DataContractSerializer(typeof(PersonData));
                ser.WriteObject(stream, p);
            }

            using (var stream = new System.IO.FileStream("data.xml", System.IO.FileMode.Open))
            {
                DataContractSerializer ser = new DataContractSerializer(typeof(PersonData));
                PersonData result = (PersonData)ser.ReadObject(stream);
            }
        }
    }
}
