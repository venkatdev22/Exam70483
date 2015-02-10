using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Extention;
namespace Features
{
    public class Person
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    public class Product
    {
        public string Description { get; set; }
        public decimal Price { get; set; }
    }
    public class OrderLine
    {
        public int Amount { get; set; }
        public Product Product { get; set; }
    }
    public class Order
    {
        public List<OrderLine> OrderLines { get; set; }
    }


    class Program
    {
        static void Main(string[] args)
        {
            ///Language features that make LINQ possible

            #region Implicitly Typed Variables
            ///Explicit Typing
            int i = 42;
            System.IO.Stream m = new System.IO.MemoryStream();
            //This line gives a compile error
            //string s = i + m; 

            Dictionary<string, IEnumerable<Tuple<Type, int>>> data = new Dictionary<string, IEnumerable<Tuple<Type, int>>>();

            ///Implicitly typed variables
            var implicitData = new Dictionary<string, IEnumerable<Tuple<Type, int>>>();
            #endregion

            #region Object Initialization
            ///Object initialization syntax
            ///LISTING 4-49  Using an object initializer
            Person p = new Person
            {
                FirstName = "John",
                LastName = "Doe"
            };

            ///LISTING 4-50  Using a collection initializer
            var persons = new List<Person>
            {
                new Person{FirstName="Steve",LastName="Harmison"}
               ,new Person{FirstName="Graham",LastName="Onions"}
            };
            #endregion

            #region Lambda Expressions
            ///LISTING 4-51  Using an anonymous method 
            Func<int, int> myDelegete1 = delegate(int x) { return x * 2; };
            Console.WriteLine(myDelegete1(4));

            ///Using an anonymous method WITHOUT return
            Action<int, int> myAction1 = delegate(int x, int y) { Console.WriteLine(x * y); };
            myAction1(10, 10);

            Action<int, int> myActionMethod2 = WriteToConsole;
            myActionMethod2(2, 3);
            ///Lambda Expressions=> “becomes” or “for which.” 
            Func<int, int> myDelegete2 = j => j * j;  //Func<inT, Tout>
            Console.WriteLine(myDelegete2(4));

            Action<int, int> myActionMethod1 = (x, y) => Console.WriteLine(x * y);
            myActionMethod1(2, 2);

            Action<int, int> myActionLinq = (x, y) => WriteToConsole(2, 3);
            myActionLinq(2, 2);


            #region Sources
            /*
                 https://msdn.microsoft.com/en-us/library/bb534647(v=vs.110).aspx with return
                 https://msdn.microsoft.com/en-us/library/bb549311(v=vs.110).aspx without return
                */
            #endregion
            #endregion

            #region Extension Methods
            //Extention namespace imported;
            string value = "5";
            myDelegete2(value.ToInt());

            string values = "12345678";

            //IEnumerable<string> valueList = values.ToList().Select(x => x.ToString());
            //IEnumerable<int> intList = valueList.ToIntList();

            List<string> valueList = values.ToList().Select(x => x.ToString()).ToList();
            List<int> intList = valueList.ToIntList();
            foreach (var a in intList)
            {
                Console.WriteLine(a * a);
            }

            #endregion

            #region Anonymous Types
            ///LISTING 4-53  Creating an anonymous type
            var person = new
            {
                FirstName = "John",
                LastName = "Doe"
            };

            var anonymousResult = from players in persons select new { players.FirstName, players.LastName };
            foreach (var xres in anonymousResult)
            {
                Console.WriteLine(xres.FirstName + " " + xres.LastName);
            }
            Console.WriteLine(person.GetType().Name); // Displays “<>f__AnonymousType0`2”


            #endregion

            usingLINQ();

            StandardQueryOperators();
            Console.ReadKey();
        }

        static void WriteToConsole(int x, int y)
        {
            Console.WriteLine(x * y);
        }



        static void usingLINQ()
        {
            int[] data = { 1, 2, 5, 8, 11 };

            //Query syntax
            var result1 = from d in data
                          where d % 2 == 0
                          select d;

            result1.ToList().ForEach(i => Console.WriteLine(i));

            //Method syntax
            var result2 = data.Where(x => x % 2 != 0);
            Console.WriteLine(string.Join(",", result2));

            int[] data2 = { 2, 4, 6 };

            var resultMutiple = from d in data
                                from d2 in data2
                                select d * d2;
            Console.WriteLine(string.Join(",", resultMutiple));  // each item in first source multiplied with all the item of second source

            var orders = new List<Order>
            { 
                 new Order
                 {
                     OrderLines=new List<OrderLine>
                 {
                     new OrderLine
                     {
                         Amount=10
                         ,Product=new Product
                         {
                             Description="Audi"
                             ,Price=10.1m
                         }
                     }
                     ,new OrderLine
                     {
                          Amount=20
                          ,Product=new Product
                          {
                              Description="TATA"
                              ,Price=23.1m
                          }
                     }
                 }
                 }
                ,new Order
                {                    
                     OrderLines=new List<OrderLine>
                 {
                     new OrderLine
                     {
                         Amount=10,Product=new Product
                         {
                             Description="Hyundai",Price=9.1m
                         }
                     }
                 }
                }
                ,new Order
                {                     
                     OrderLines=new List<OrderLine>
                 {
                     new OrderLine
                     {
                         Amount=10
                         ,Product=new Product
                         {
                             Description="Suzuki",Price=11.1m
                         }
                     }
                    ,new OrderLine
                     {
                          Amount=20
                          ,Product=new Product
                          {
                              Description="TATA"
                              ,Price=20.1m
                          }
                     }
                 }
                }
            };


            var averageNumberOfOrderLines = orders.Average(o => o.OrderLines.Count);

            Console.WriteLine("\n***Group By***\n");

            var groupByNone = from orderData in orders
                              from oderLineData in orderData.OrderLines
                              //group oderLineData by oderLineData.Product.Description into p
                              select oderLineData;

            Console.WriteLine("\n***Group By None***\n");
            foreach (var g in groupByNone)
            {
                Console.WriteLine("{0}-{1}:", g, g.Product.Description);
            }

            var groupByPro = from orderData in orders
                             from oderLineData in orderData.OrderLines
                             group oderLineData by oderLineData.Product into p
                             select p;

            Console.WriteLine("\n***Group By Pro***\n");
            foreach (var g in groupByPro)
            {
                Console.WriteLine(g.Key);
                foreach (var gin in g)
                {
                    Console.WriteLine("{0}-{1}:", gin, gin.Product.Description);
                }
            }

            var groupByProDesc = from orderData in orders
                                 from oderLineData in orderData.OrderLines
                                 group oderLineData by oderLineData.Product.Description into p
                                 select p;

            Console.WriteLine("\n***Group By Pro Desc***\n");
            foreach (var g in groupByProDesc)
            {
                Console.WriteLine(g.Key);
                foreach (var gin in g)
                {
                    Console.WriteLine("{0}-{1}:", gin, gin.Product.Description);
                }
            }


            ///LISTING 4-60  Using group by and projection
            var projectedResult = from orderData in orders
                                  from orderLineData in orderData.OrderLines
                                  group orderLineData by orderLineData.Product into groupedResult
                                  select
                                  new
                                  {
                                      ProductKey = groupedResult
                                  ,
                                      productPrice = groupedResult.Sum(x => x.Product.Price)
                                  };

            // projectedResult.All(x => x.productPrice > 0);

            projectedResult.ToList().ForEach(x => Console.WriteLine("{0}:{1}", x.ProductKey, x.productPrice));

        }


        /// <summary>
        /// The standard query operators are: All, Any, Average, Cast, Count, Distinct, GroupBy, Join
        /// ,Max, Min, OrderBy, OrderByDescending, Select, SelectMany, Skip
        /// ,SkipWhile, Sum, Take, Take-While, ThenBy, ThenByDescending, and Where.
        /// </summary>
        static void StandardQueryOperators()
        {
            List<Package> packages = new List<Package>
            { 
              new Package { Company = "A", Weight = 25.2 },
              new Package { Company = "B", Weight = 18.7 },
              new Package { Company = "Wingtip Toys", Weight = 6.0 },
              new Package { Company = "Adventure Works", Weight = 33.8 }
            };

            #region Sum
            double totalWeight = packages.Sum(pkg => pkg.Weight);
            Console.WriteLine("***Sum***");
            Console.WriteLine("The total weight of the packages is: {0}", totalWeight);
            #endregion

            #region Join
            ///LISTING 4-61  Using join
            ///join <newsource> in <existingsrouce> on <property> equals <newsource>
            string[] popularProductNames = { "A", "B" };
            var joinResult = from packs in packages
                             join joinPack in popularProductNames on packs.Company equals joinPack
                             select packs;
            Console.WriteLine("***Join***");
            foreach (var jr in joinResult)
            {
                Console.WriteLine("{0}:{1}", jr.Company, jr.Weight);
            }

            /*
             https://msdn.microsoft.com/en-us/library/bb311040.aspx
             http://www.dotnetperls.com/join
             */

            #endregion

        }
    }
    /// <summary>
    /// Class for StandardQueryOperators
    /// </summary>
    class Package
    {
        public string Company { get; set; }
        public double Weight { get; set; }
    }
}


///Extension Class
namespace Extention
{
    public static class GeneralExtention
    {
        public static int ToInt(this string value)
        {
            int result = 0;
            if (int.TryParse(value, out result))
            {
                return result;
            }
            return result;
        }

        public static List<int> ToIntList(this List<string> values)
        {
            if (values.Count() > 0)
            {
                return values.Select(x => x.ToInt()).ToList();
            }
            return new List<int>();
        }

        public static IEnumerable<int> ToIntList(this IEnumerable<string> values)
        {
            if (values.Count() > 0)
            {
                return values.Select(x => x.ToInt());
            }
            return new List<int>();
        }


    }
}