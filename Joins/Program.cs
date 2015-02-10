using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Joins
{

    /// <summary>
    /// Classes for Join
    /// </summary>
    class Product
    {
        public string Name { get; set; }
        public int CategoryID { get; set; }
    }
    class Category
    {
        public string Name { get; set; }
        public int ID { get; set; }
    }

    class Person
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    class Pet
    {
        public string Name { get; set; }
        public Person Owner { get; set; }
    }
    class Program
    {
        static List<Category> categories;
        static List<Product> products;

        static void Main(string[] args)
        {
            try
            {
                buildData();
                innerJoin();
                groupJoin();
                groupInnerJoin();
                groupJoin3();
                LeftOuterJoin();
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        /// <summary>
        /// Build necessary data for join operations
        /// </summary>
        static void buildData()
        {
            // Specify the first data source.
            categories = new List<Category>()
            { 
            new Category(){Name="Beverages", ID=001},
            new Category(){ Name="Condiments", ID=002},
            new Category(){ Name="Vegetables", ID=003},
            new Category() {  Name="Grains", ID=004},
            new Category() {  Name="Fruit", ID=005}            
            };

            // Specify the second data source.
            products = new List<Product>()
            {
            new Product{Name="Cola",  CategoryID=001},
            new Product{Name="Tea",  CategoryID=001},
            new Product{Name="Mustard", CategoryID=002},
            new Product{Name="Pickles", CategoryID=002},
            new Product{Name="Carrots", CategoryID=003},
            new Product{Name="Bok Choy", CategoryID=003},
            new Product{Name="Peaches", CategoryID=005},
            new Product{Name="Melons"},
            };
        }
        /// <summary>
        /// The INNER JOIN selects all rows from both tables as long as there is a match between the columns in both tables.
        /// </summary>
        static void innerJoin()
        {
            Console.WriteLine("***Inner Join***\n");
            var innerJoinResults = from category in categories
                                   join prod in products on category.ID equals prod.CategoryID
                                   select new { Category = category.ID, Product = prod.Name };
            innerJoinResults.ToList().ForEach(x => Console.WriteLine("{0,-10}{1}\n", x.Product, x.Category));
        }
        /// <summary>
        /// The group join is useful for producing hierarchical data structures.It pairs each element from 
        /// the first collection with a set of correlated elements from the second collection.
        /// </summary>
        static void groupJoin()
        {
            Console.WriteLine("***Group Join***\n");
            /// This is a demonstration query to show the output of a "raw" group join. A more typical group join 
            /// is shown in the GroupInnerJoin method. 
            var groupJoinResult = from category in categories
                                  join prod in products on category.ID equals prod.CategoryID into prodGroup
                                  select prodGroup;

            foreach (var proGrouping in groupJoinResult)
            {
                Console.WriteLine("Group:");
                proGrouping.ToList().ForEach(x => Console.WriteLine("   {0,-10}{1}",x.Name, x.CategoryID));
            }
        }
        /// <summary>
        /// 
        /// </summary>
        static void groupInnerJoin()
        {
            Console.WriteLine("***Group Inner Join***\n");
            var groupIJoinResult = from category in categories
                                   orderby category.ID
                                   join prod in products on category.ID equals prod.CategoryID into prodGroup
                                   select new
                                   {
                                       Category = category.Name,
                                       Product = from prod2 in prodGroup
                                                 orderby prod2.CategoryID
                                                 select prod2
                                   };


            foreach (var proGrouping in groupIJoinResult)
            {
                Console.WriteLine("Group:{0}", proGrouping.Category);
                proGrouping.Product.ToList().ForEach(x => Console.WriteLine("   {0,-10}{1}", x.Name, x.CategoryID));
            }

        }
        /// <summary>
        /// 
        /// </summary>
        static void groupJoin3()
        {
            Console.WriteLine("***Group Join3***\n");
            var groupjoin3Result = from category in categories
                                   join prod in products on category.ID equals prod.CategoryID into prodGroup
                                   from prod2 in prodGroup
                                   orderby prod2.CategoryID
                                   select new {Category=prod2.CategoryID,ProductName=prod2.Name };

            foreach (var item in groupjoin3Result)
            {
                Console.WriteLine("{0,-10}{1}", item.ProductName, item.Category);
            }
        }
        /// <summary>
        /// In a left outer join, all the elements in the left source sequence are returned, even if no matching elements are in the right sequence.
        /// To perform a left outer join in LINQ, use the DefaultIfEmpty method in combination with a group join to specify a default right-side element to produce 
        /// if a left-side element has no matches.
        /// </summary>
        static void LeftOuterJoin()
        {
            Console.WriteLine("***Left Outer Join***\n");

            var leftOJointResult = from category in categories
                                   join prod in products on category.ID equals (int?)prod.CategoryID into prodGroup
                                   select prodGroup.DefaultIfEmpty(new Product { Name = "Nill", CategoryID = category.ID });

            foreach (var prodGrouping in leftOJointResult)
            {
                //Console.WriteLine("Group:", prodGrouping.Count());
                foreach (var item in prodGrouping)
                {
                    Console.WriteLine("  {0,-10}{1}", item.Name, item.CategoryID);
                }
            }

            var leftOJointResult2 = from category in categories
                                   join prod in products on category.ID equals prod.CategoryID into prodGroup
                                   from prodItems in prodGroup.DefaultIfEmpty()
                                   select new { item =(prodItems == null ? "Nill" : prodItems.Name), itemId = category.ID };
                                 //select new { Name = item == null ? "Nothing!" : item.Name, CategoryID = category.ID };

            Console.WriteLine("Left Outer Join2");
            foreach (var item in leftOJointResult2)
            {
                Console.WriteLine("  {0,-10}{1}", item.item, item.itemId);
            }



            Person magnus = new Person { FirstName = "Magnus", LastName = "Hedlund" };
            Person terry = new Person { FirstName = "Terry", LastName = "Adams" };
            Person charlotte = new Person { FirstName = "Charlotte", LastName = "Weiss" };
            Person arlene = new Person { FirstName = "Arlene", LastName = "Huff" };

            Pet barley = new Pet { Name = "Barley", Owner = terry };
            Pet boots = new Pet { Name = "Boots", Owner = terry };
            Pet whiskers = new Pet { Name = "Whiskers", Owner = charlotte };
            Pet bluemoon = new Pet { Name = "Blue Moon", Owner = terry };
            Pet daisy = new Pet { Name = "Daisy", Owner = magnus };

            // Create two lists.
            List<Person> people = new List<Person> { magnus, terry, charlotte, arlene };
            List<Pet> pets = new List<Pet> { barley, boots, whiskers, bluemoon, daisy };

            var query = from person in people
                        join peronPet in pets on person equals (peronPet==null?null:peronPet.Owner) into pt
                        from ptnull in pt.DefaultIfEmpty()
                        select new 
                        {
                             PersonName=(ptnull==null?string.Empty:ptnull.Name)
                            ,person.FirstName
                        };
            foreach (var squery in query)
            {
                Console.WriteLine("{0,-10}{1}",squery.FirstName,squery.PersonName);
            }

        }
    }
}
