using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YieldExample
{
    class Program
    {
        static void Main(string[] args)
        {

            int[] Items = { 1, 2, 3, 4, 5 };
            
            List<int> BiggerItems= ReserveMethod(Items.ToList());

            foreach (var item in BiggerItems)
            {
                Console.WriteLine(item);
            }
            iteratorMethod(Items.ToList());
            Console.ReadKey();
            
        }

        static List<int> ReserveMethod(List<int> Items)
        {
            List<int> BiggerItems = new List<int>();
            foreach (var item in Items)
            {
                if (item > 3)
                {
                    BiggerItems.Add(item);
                }
            }
            return BiggerItems;
        }

        static IEnumerable<int> iteratorMethod(List<int> Items)
        {
            foreach (var x in Items)
            {
                if (x > 3) yield return x;
            }
        }
    }
}
