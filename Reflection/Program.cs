using System;
using System.Reflection;

namespace Reflection
{
    class Program
    {
        static void Main(string[] args)
        {
            object sports = Activator.CreateInstance(typeof(Sports));
            PropertyInfo[] properites = typeof(Sports).GetProperties();
            PropertyInfo SportsnameProperty1 = properites[0];
        }
    }

    class Sports
    {
        Sports() { }
        Sports(String Name) { }
    }
}
