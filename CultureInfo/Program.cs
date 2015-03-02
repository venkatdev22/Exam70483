using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CultureInfoExample
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var english = new System.Globalization.CultureInfo("En");
                var dutch = new System.Globalization.CultureInfo("Nl");
                string value = "€19,95";
                decimal d = decimal.Parse(value, System.Globalization.NumberStyles.Currency, dutch);
                Console.WriteLine(d);
                Console.WriteLine(d.ToString(english));
            }
            catch (System.FormatException fEx)
            {
                Console.WriteLine(fEx);
            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex); 
            }
            Console.ReadKey();
        }
    }
}
