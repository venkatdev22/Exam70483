using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Binaryserialize
{

    [System.Serializable]
    public class Person
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public Match ODI { get; set; }
        public Match TEST { get; set; }
    }

    public class Match
    {          
        public int NumberOfMatches { get; set; }
        public string Type { get; set; }
        public float Avg { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
        }
    }
}
