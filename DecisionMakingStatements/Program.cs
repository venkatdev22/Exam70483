using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DecisionMakingStatements
{
    class Program
    {
        static void Main(string[] args)
        {
            Program obj = new Program();
            obj.managingProgramFlow();
            Console.ReadKey();
        }

        public void managingProgramFlow()
        {
            #region The null-coalescing operator
            //int
            int? n = null;
            int y = n ?? -1;

            Console.WriteLine("Int Type");
            Console.WriteLine("Value of n: {0}", n);
            Console.WriteLine("Value of y: {0}", n ?? -1);

            int? a = 1;
            int? b = 3;
            int c = a ??
                  b ?? 10;
            Console.WriteLine("value of a:{0}& b:{1}", a, b);
            Console.WriteLine("value of c:{0}", c);

            //string
            string s = null;
            string copyofs = s ?? "Hi";
            Console.WriteLine("\nString Type");
            Console.WriteLine("value of copyofs: {0}", copyofs);
            #endregion

            #region Switch statement
            Console.WriteLine("\nSwtich: Char type");
            char letter = 'i';
            switch (letter)
            {
                case 'a':
                case 'e':
                case 'i':
                case 'o':
                case 'u':
                    {
                        Console.WriteLine("'{0}' is a vowels", letter);
                        break;
                    }
                case 'y':
                    {
                        Console.WriteLine("'{0}' is sometimes a vowels", letter);
                        break;
                    }
                default: Console.WriteLine("'{0}' is a Consonant", letter);
                    break;
            }
            #endregion

            #region For loop
            Console.WriteLine("\nfor loop");
            int[] values = { 1, 2, 3, 4, 5, 6 };
            for (int x = 0, yx = values.Length - 1;
                ((x < values.Length) && (yx >= 0));
                x++, yx--)
            {
                Console.Write(values[x]);
                Console.Write(values[yx]);
            }

            Console.WriteLine("\nfor loop with break");
            int[] xvalues = { 1, 2, 3, 4, 5, 6 };
            for (int index = 0; index < xvalues.Length; index++)
            {
                if (xvalues[index] == 4) break; //continue

                Console.Write(xvalues[index]);
            }

            #endregion

            #region Foreach
            var Audi = new List<Car>()
            {
               new Car(){Break="Rear",Window="Rear"},
               new Car(){Break="front ",Window="front "}
            };

            var replaceItem=new Car(){Break="",Window=""};
            foreach (var carDetail in Audi)
            {
                //carDetail = replaceItem;   //Error
                replaceItem = carDetail;
            }
            
            //Enumerator
            var enumeratedAudi = Audi.GetEnumerator();
                       enumeratedAudi.MoveNext();
                           Console.WriteLine(enumeratedAudi.ToString());

                           List<Car>.Enumerator e = Audi.GetEnumerator();
                           e.MoveNext();
                           Console.WriteLine(e.ToString());
            #endregion

            #region Jump(Goto)
                           int jump = 0;
                           goto customLabel;
                           jump++;

                       customLabel:
                       Console.WriteLine("value of jump is:{0}",jump);
                       

            #endregion

            #region While
                       var Hundai = new List<Car>() 
                       {
                              new Car(){Break="Rear",Window="Rear"},
                              new Car(){Break="front ",Window="front "}
                       };
            
            int Iwhile=0;
            while (Iwhile < Hundai.Count())
            {
                 
                Iwhile++;
            }


            #endregion
        }    
     }

    public class Car
    {
        public string Window { get; set; }
        public string Break { get; set; }
    }
        
}