using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YieldExample2
{
    class Program
    {

        static List<int> MyList = new List<int>();

        static void FillValues()
        {
            MyList.Add(1);
            MyList.Add(2);
            MyList.Add(3);
            MyList.Add(4);
            MyList.Add(5);
        }
        static void Main(string[] args)
        {
            FillValues(); // Fills the list with 5 values
            foreach (int i in MyList) // Browses through the list
            {
                Console.WriteLine(i);
            }
     

          //  FilterWithoutYield();

         var restuld=   FilterWithYield();
            Console.ReadLine();
             
        }

        static IEnumerable<int> FilterWithoutYield()
        {
            List<int> temp = new List<int>();
            foreach (int i in MyList)
            {
                if (i > 3)
                {
                    temp.Add(i);
                }
            }
            return temp;
        }

        static IEnumerable<int> FilterWithYield()
        {
            foreach (int i in MyList)
            {
                if (i > 3) yield return i;
            }
        }
    }
}
