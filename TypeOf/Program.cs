using System;

using System.IO;


namespace OperatorType
{
    class Program
    {
        static void Main(string[] args)
        {

            var _type = typeof(char);
            Console.WriteLine(_type); // Value type pointer
            Console.WriteLine(typeof(int)); // Value type
            Console.WriteLine(typeof(byte)); // Value type
            Console.WriteLine(typeof(Stream)); // Class type
            Console.WriteLine(typeof(TextWriter)); // Class type
            Console.WriteLine(typeof(Array)); // Class type
            Console.WriteLine(typeof(int[])); // Array reference type

            Console.ReadKey();
        }
    }
}
