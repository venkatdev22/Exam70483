using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WcfWebService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "ExamService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select ExamService.svc or ExamService.svc.cs at the Solution Explorer and start debugging.
    public class ExamService : IExamService
    {
        public string DoWork(string x, string y)
        {
            return x + y;
        }
    }
}
