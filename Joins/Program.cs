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

    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                buildData();



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
            List<Category> categories = new List<Category>()
            { 
            new Category(){Name="Beverages", ID=001},
            new Category(){ Name="Condiments", ID=002},
            new Category(){ Name="Vegetables", ID=003},
            new Category() {  Name="Grains", ID=004},
            new Category() {  Name="Fruit", ID=005}            
            };

            // Specify the second data source.
            List<Product> products = new List<Product>()
            {
            new Product{Name="Cola",  CategoryID=001},
            new Product{Name="Tea",  CategoryID=001},
            new Product{Name="Mustard", CategoryID=002},
            new Product{Name="Pickles", CategoryID=002},
            new Product{Name="Carrots", CategoryID=003},
            new Product{Name="Bok Choy", CategoryID=003},
            new Product{Name="Peaches", CategoryID=005},
            new Product{Name="Melons", CategoryID=005},
            };
        }
    }
}
