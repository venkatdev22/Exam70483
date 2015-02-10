using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interface
{
    class Program
    {
        static void Main(string[] args)
        {
            var items = new List<ISports>() {new Cricket(),new Football()};
            foreach (var item in items)
            {
                Console.WriteLine("Name:{0}", item.Name);
                item.ActionMethod();
            }
            Console.ReadKey();

        }
    }

    interface ISports
    {
        String Name { get; }
        void ActionMethod();
    }

    public abstract class Ball : ISports         
    {
        private readonly string _country;
   
        public string Name { get; set; }
        protected Ball(string Country, string _name)
        {
            _country = Country;
            Name = _name;
        }

        
        public void ActionMethod()
        {
            Console.WriteLine("Country:{0}", _country);
        }
    }

    public class Cricket : Ball              //Cricket class object will hold the properties of Ball class. 
    {
        public Cricket() : base("India", "RahulDravid") { }
    }

    public class Football : Ball
    {
        public Football() : base("Britain", "Rooney") { }
    }
}
