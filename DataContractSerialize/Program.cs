using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataContractSerialize.SerializeWCFService;
namespace DataContractSerialize
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                DataContractSerialize.SerializeWCFService.Service sObj = new Service();
                var personObject = sObj.DoWork();
                Console.WriteLine(personObject.Id);

                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
