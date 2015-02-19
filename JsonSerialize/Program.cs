using System;
using System.Collections.Generic;
using System.Linq;
namespace JsonSerialize
{
    [System.Runtime.Serialization.DataContract]
    class Person
    {
        [System.Runtime.Serialization.DataMember]
        public string Name { get; set; }
        [System.Runtime.Serialization.DataMember]
        public int Id { get; set; }
    }
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                doSerialize();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            Console.ReadKey();
        }


        static  void doSerialize()
        {
            try
            {
                List<Person> persons = getObjects();
                Person person = getObject();
                var dCJsonSerialize = new System.Runtime.Serialization.Json.DataContractJsonSerializer(typeof(List<Person>));
                //var dCJsonSerialize2 = new System.Runtime.Serialization.Json.DataContractJsonSerializer(typeof(Person));
                System.IO.MemoryStream stream;
                string path = @"C:\Users\User\Documents\Visual Studio 2012\Projects\Exam70483\JsonSerialize\App_Code\Json.bin";
                using (stream = new System.IO.MemoryStream())
                {
                    dCJsonSerialize.WriteObject(stream, persons);
                    stream.Position = 0;
                    System.IO.StreamReader sReader = new System.IO.StreamReader(stream);
                    Console.WriteLine(sReader.ReadToEnd());
                    stream.Position = 0;
                    var dperson = (List<Person>)dCJsonSerialize.ReadObject(stream);

                    Console.WriteLine("First item Name:{0}\nId:{1}", dperson.FirstOrDefault().Name,dperson.FirstOrDefault().Id);

                    System.IO.FileStream fstream = new System.IO.FileStream(path,System.IO.FileMode.OpenOrCreate);
                    byte[] bytes = new byte[stream.Length];
                    stream.Read(bytes, 0, (int)stream.Length);
                    stream.Write(bytes, 0, bytes.Length);
                    fstream.Close();
                }

            }
            catch (Exception ex) { Console.WriteLine(ex);}
        }

        static Person getObject()
        {
            return new Person {Id=1,Name="Takeda"};
        }
        static List<Person> getObjects()
        {
            return new List<Person> {getObject(),getObject(),getObject()};
        }

    }
}
