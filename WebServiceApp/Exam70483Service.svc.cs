using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WebServiceApp
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Exam70483Service" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Exam70483Service.svc or Exam70483Service.svc.cs at the Solution Explorer and start debugging.
    [System.ServiceModel.ServiceContract]
    public class Exam70483Service// : IExam70483Service
    {
        [System.ServiceModel.OperationContract]
        public string DoWork(string left, string right)
        {
            return left + right;
        }
    }
}
