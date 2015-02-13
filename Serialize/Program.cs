using System;
using System.Collections.Generic;
using System.Linq;
namespace Xmlserialize
{
    [System.Serializable]
    public class Person
    {
        public string FirstName{get;set;}
        public string LastName { get; set; }
        public int Age{get;set;}
    }

    public class Order
    {
        [System.Xml.Serialization.XmlAttribute]
        public int Id { get; set; }

        [System.Xml.Serialization.XmlIgnore]
        public bool IsDirty { get; set; }

        [System.Xml.Serialization.XmlArray("Lines")]
        [System.Xml.Serialization.XmlArrayItem("OrderLine")]
        public List<OrderLine> OrderLines { get; set; }
    }

    public class VIPOrder : Order
    {
        public string Desc { get; set; }
    }

    public class OrderLine
    {
        [System.Xml.Serialization.XmlAttribute]
        public int Id { get; set; }

        [System.Xml.Serialization.XmlAttribute]
        public int Amount { get; set; }

        [System.Xml.Serialization.XmlElement("OrderedProdcut")]
        public Product Product { get; set; }
    }
    
    public class Product
    {
        [System.Xml.Serialization.XmlAttribute]
        public int Id { get; set; }

        public decimal Price { get; set; }
        public string Desc { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
          ///the overall process of serialization
          ///Object -> Bytes -> File||Memory||Database

            string value = doSerialize();
            doDeSerialize(value);

            /*
             * https://msdn.microsoft.com/en-IN/library/system.runtime.serialization.ondeserializedattribute.aspx
             * https://msdn.microsoft.com/en-IN/library/system.nonserializedattribute.aspx
             * https://msdn.microsoft.com/en-IN/library/5x6cd29c.aspx
             */

            Console.ReadKey();
        }

        public static Order createOrder()
        {
           Product p1 = new Product { Id = 1, Desc = "p2", Price = 9 }; 
           Product p2 = new Product { Id = 2, Desc = "p3", Price = 6 };

           Order order = new VIPOrder()
           {
               Id = 4,
               Desc = "Order for John Doe. Use the nice giftwrap",
               OrderLines = new List<OrderLine>()
               {
                  new OrderLine { Id = 5, Amount = 1, Product = p1}, 
                  new OrderLine { Id = 6 ,Amount = 10, Product = p2}, 
               }
           };

           return order;
        }
        /// <summary>
        ///To serialize an object, you need the OBJECT to be serialized, 
        ///a STREAM to contain the serialized object, 
        ///and a FORMATTER. System.Runtime.Serialization contains the classes necessary for serializing and deserializing objects
        /// </summary>
        static string doSerialize()
        {
            string objectAsString = string.Empty;
            try
            {
                ///Object
                var order = createOrder();

                System.Xml.Serialization.XmlSerializer xmlSerializer = new System.Xml.Serialization.XmlSerializer(typeof(Order),
                                                                            new Type[]{ typeof(VIPOrder) });

                ///Object -> Memory
                using (var stringWritter = new System.IO.StringWriter())
                {
                    xmlSerializer.Serialize(stringWritter, order);
                    objectAsString = stringWritter.ToString();
                }
                Console.Write(objectAsString);


                ///Object -> File
                var parentpath = System.IO.Directory.GetParent(System.IO.Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).FullName).FullName;
                using (var streamWritter = new System.IO.StreamWriter(parentpath + "\\App_Data\\Doc.xml"))
                {
                    xmlSerializer.Serialize(streamWritter, order);
                }

            }
            catch (System.Runtime.Serialization.SerializationException sEx) { Console.WriteLine(sEx); }
            catch (System.Exception Ex) { Console.WriteLine(Ex); }

            return objectAsString;
        }

        static void doDeSerialize(string objectAsString)
        {
            ///deserialize
            ///Memory -> Object
            
                System.Xml.Serialization.XmlSerializer xmlSerializer = new System.Xml.Serialization.XmlSerializer(typeof(Person));
                using (var stringReader = new System.IO.StringReader(objectAsString))
                {
                    Person dPerson = (Person)xmlSerializer.Deserialize(stringReader);
                    //Console.WriteLine("\nName: {0}\nAge: {1}\nGame:{2}\nNo.Of Matches{3}\nAvg:{4}",dPerson.FirstName+" "+dPerson.LastName,dPerson.Age,dPerson.ODI.Type,dPerson.ODI.NumberOfMatches,dPerson.ODI.Avg);
                }
        }
    }
}
