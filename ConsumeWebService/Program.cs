using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsumeWebService.Exam70843Service;
namespace ConsumeWebService
{
    class Program
    {
        static void Main(string[] args)
        {
            TestService();
            Console.ReadKey();
        }

        public static void TestService()
        {
            ExamServiceClient client = new ExamServiceClient();
            Console.WriteLine(client.DoWork("Mitchel","Marsh"));
        }

    }
}
