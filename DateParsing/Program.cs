using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DateParsing
{
    class Program
    {
        static void Main(string[] args)
        {
            /* https://msdn.microsoft.com/en-us/library/System.DateTime.Parse(v=vs.110).aspx */
            #region DateTime.Parse(String s )
            // Use standard en-US date and time value
            DateTime dateValue;
            string dateString = "2/16/2008 12:15:12 PM";
            try
            {
                dateValue = DateTime.Parse(dateString);
                Console.WriteLine("'{0}' converted to {1}.", dateString, dateValue);
            }
            catch (FormatException)
            {
                Console.WriteLine("Unable to convert '{0}'.", dateString);
            }
            #endregion


            #region DateTime.Parse(String s , IFormatProvider provider )
            #endregion

            #region DateTime.Parse(String s , IFormatProvider provider , DateTypeStyles styles )
            #endregion
            Console.ReadKey();
        }

    }
}
