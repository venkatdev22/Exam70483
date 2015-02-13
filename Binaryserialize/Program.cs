using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Binaryserialize
{

    [Serializable]
    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [NonSerialized]
        private bool isDirty = false;

        [System.Runtime.Serialization.OnSerializing()]
        internal void onSerialization(System.Runtime.Serialization.StreamingContext sContext)
        { Console.WriteLine("\n**OnSerializing**\n"); }

        [System.Runtime.Serialization.OnSerialized()]
        internal void onSerialized(System.Runtime.Serialization.StreamingContext sContext)
        { Console.WriteLine("\n**OnSerialized**\n"); }

        [System.Runtime.Serialization.OnDeserializing()]
        internal void onDeSerialzation(System.Runtime.Serialization.StreamingContext sContext)
        { Console.WriteLine("\n**OnDeserializing**\n"); }

        [System.Runtime.Serialization.OnDeserialized()]
        internal void onDeSerialze(System.Runtime.Serialization.StreamingContext sContext)
        { Console.WriteLine("\n**OnSerialized**\n"); }
    }


    class Program
    {
        static void Main(string[] args)
        {
            doSerialize();

            Console.ReadKey();
        }
        static void doSerialize()
        {
            try
            {
                Person person = getPerson();

                //System.Runtime.Serialization.IFormatter binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                //or
                var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();

                ///Object -> Memory
                using (var meomryStream = new System.IO.MemoryStream())
                {
                        binaryFormatter.Serialize(meomryStream, person);
                        
                        ///Memory -> Object
                        ///Point stream at begin.
                        meomryStream.Seek(0, System.IO.SeekOrigin.Begin);
                        Person dePerson = (Person)binaryFormatter.Deserialize(meomryStream);
                        meomryStream.Close();

                        Console.WriteLine("Name:{0}\nId:{1}",dePerson.Name,dePerson.Id);    
                }
                ///Object -> File
                //using (var stream = new System.IO.FileStream("Person.bin", System.IO.FileMode.OpenOrCreate))
                using (var stream = System.IO.File.Open("Person.bin", System.IO.FileMode.OpenOrCreate)) //File will be created in bin folder
                {
                    binaryFormatter.Serialize(stream, person);
                    stream.Close(); //Close the file, to avoid exception
                   
                    if (System.IO.File.Exists(System.IO.Path.Combine(System.IO.Directory.GetCurrentDirectory(),"Person.bin")))
                    {
                        Console.WriteLine("Person.bin file created");
                        
                        var fileStream = System.IO.File.Open("Person.bin", System.IO.FileMode.Open);
                        fileStream.Seek(0, System.IO.SeekOrigin.Begin);

                        Person fileObjPerson = (Person)binaryFormatter.Deserialize(fileStream);
                        fileStream.Close();

                        Console.WriteLine("Name:{0}\nId:{1}", fileObjPerson.Name, fileObjPerson.Id);
                    }

                }

            }
            catch (System.IO.IOException Ioe) { Console.WriteLine(Ioe); }
            catch (System.ObjectDisposedException Ode) { Console.WriteLine(Ode); }
            catch (System.Exception Ex) { Console.WriteLine(Ex); }
        }

        static Person getPerson()
        {
            return new Person {Id=1,Name="John Doe" };
        }
    }
}
