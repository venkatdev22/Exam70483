using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Events
{
    class Program
    {
        static void Main(string[] args)
        {
     
        }

        public abstract class Ball
        {
        }
        public class Sports<T> where T : Ball
        {
            public T Ball { get; set; }
        }
        public class Cricket : Ball{}
        public class Basball : Ball{}

        void Method()
        {
            var firstMatch = new Sports<Cricket>() {};
            var secondMatch = new Sports<Basball>() { };
        }
       
    }
}
