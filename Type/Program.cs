using System;
using System.Reflection;

namespace TypeClass
{
    class Program
    {
        static void Main(string[] args)
        {
           
            Type type1 = typeof(string[]);
            Type type2 = "string".GetType();
            Type type3 = typeof(Type);
            Type type4 = typeof(long);
            Type type5 = typeof(Program);
            Test(type1);
            Test(type2);
            Test(type3);
            Test(type5);
            Console.ReadKey();
        }
        static void Test(Type type)
        {
            // Print some properties of the Type formal parameter.
            Console.WriteLine("IsArray: {0}", type.IsArray);
            Console.WriteLine("Name: {0}", type.Name);
            Console.WriteLine("IsSealed: {0}", type.IsSealed);
            Console.WriteLine("BaseType.Name: {0}", type.BaseType.Name);
            Console.WriteLine();
            
        }
    }
}
