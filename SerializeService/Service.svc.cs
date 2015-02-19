using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace SerializeService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service.svc or Service.svc.cs at the Solution Explorer and start debugging.
    public class Service : IService
    {
        public PersonData DoWork()
        {
            var returnData = new PersonData { Id = 91 };

            //By default  WCF will do DataContractSerializer. So onserilze method will get call'd.
           /* var dataContractSerializer = new DataContractSerializer(typeof(PersonData));
            using (var stream = new System.IO.MemoryStream())
            {
                dataContractSerializer.WriteObject(stream, new PersonData { Id = 91 });
                stream.Seek(0, System.IO.SeekOrigin.Begin);
                var retrunData = (PersonData)dataContractSerializer.ReadObject(stream);

                return returnData;
            }*/

            return returnData;
        }
    }
}
