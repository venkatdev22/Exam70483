using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnderstandingDelegates
{
    class Program
    {
        static void Main(string[] args)
        {
            Program programObject = new Program();
            //programObject.useDelegate();
            programObject.UseMultiMethod();
            Console.ReadKey();
        }

        
        public delegate void MultiMethod();

        public void methodA()
        {
            Console.WriteLine("MethodA");
        }
        public void methodB()
        {
            Console.WriteLine("MethodB");
        }

        public void UseMultiMethod()
        {
            MultiMethod obj = methodA;
       //     obj = methodB; //Replace methodA with methodB
            obj += methodB;  //Add methodB along with methodA. 
            obj();
        }

        public delegate string MainMethod(string x, string y);
        public string methodA(string x,string y)
        {
            return x;
        }
        public string methodB(string x,string y)
        {
            return y;
        }

        public void useDelegate()
        {
            MainMethod obj = methodA;
            Console.WriteLine(obj("Hi", "Its created"));
            obj = methodB;
            Console.WriteLine(obj("Hi", "Its Created"));
            obj += methodB;
            Console.WriteLine(obj("Incremental", "Value"));

        }
    }
}
