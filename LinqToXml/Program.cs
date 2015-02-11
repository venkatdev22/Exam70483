using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
namespace LinqToXml
{
    class Program
    {
        public static string xml = string.Empty;
        static void Main(string[] args)
        {
            xml = @"<?xml version=""1.0"" encoding=""utf-8"" ?> <people> <person firstname=""john"" lastname=""doe""> <contactdetails> <emailaddress>john@unknown.com</emailaddress> </contactdetails> </person> <person firstname=""jane"" lastname=""doe""> <contactdetails> <emailaddress>jane@unknown.com</emailaddress> <phonenumber>001122334455</phonenumber> </contactdetails> </person> </people>";

            queryXml();
            updateXml();
            Console.ReadKey();
        }


        static void queryXml()
        {
            try
            {
                ///LISTING 4-65  Querying some XML by using LINQ to XML
                XDocument doc = XDocument.Parse(xml);
                var names = from xmlDoc in doc.Descendants("person")
                            select xmlDoc.Attribute("firstname").Value + " " + xmlDoc.Attribute("lastname").Value;
                Console.WriteLine("Querying some XML by using LINQ to XML");
                foreach (var name in names)
                {
                    Console.WriteLine(name);
                }

                var namesOrderby = from xmlDoc in doc.Descendants("person")
                                   where xmlDoc.Descendants("phonenumber").Any()
                                   //let namesQuery = xmlDoc.Attribute("firstname").Value + " " + xmlDoc.Attribute("lastname").Value
                                   let namesQuery = (string)xmlDoc.Attribute("firstname") + " " + (string)xmlDoc.Attribute("lastname")
                                   orderby namesQuery ascending
                                   select namesQuery;
                Console.WriteLine("\nUsing Where and OrderBy in a LINQ to XML query:\n");
                foreach (var name in namesOrderby)
                {
                    Console.WriteLine(name);
                }
            }
            catch (System.Xml.XmlException xmlEx) { Console.WriteLine(xmlEx); }
            catch (Exception Ex) { Console.WriteLine(Ex); }
        }

        static void updateXml()
        {
            try
            {
                ///LISTING 4-68  Updating XML in a procedural way
                XElement doc = XElement.Parse(xml);
                foreach (var xdoc in doc.Descendants("person"))
                {
                    string name = (string)xdoc.Attribute("firstname") + " " + xdoc.Attribute("lastname").Value;
                    xdoc.Add(new XAttribute("IsMale", name.Contains("John")));
                    XElement contactDetailsNode = xdoc.Element("contactdetails");
                    if (!contactDetailsNode.Descendants("phonenumber").Any())
                    {
                        contactDetailsNode.Add(new XElement("phonenumber", "001122334455"));
                    }
                }
                Console.WriteLine("Updating XML in a procedural way:\n");
                Console.WriteLine(doc.ToString());

                ///LISTING 4-69  Transforming XML with functional creation
                XElement tdoc = XElement.Parse(xml);
                XElement newTree = new XElement("people",
                    new XElement("person", from p in tdoc.Descendants("person")
                                           let name = p.Attribute("firstname") + " " + p.Attribute("lastname")
                                           let contactDetails = p.Element("contactdetails")
                                           select new XElement("person",
                                                     p.Attributes(),
                                                     new XAttribute("IsMale", name.Contains("John")),
                                                     new XElement("contactdetails",
                                                         contactDetails.Element("emailaddress"),
                                                         contactDetails.Element("phonenumber") ?? new XElement("phonenumber", "112233") ))));
                Console.WriteLine("\nTransforming XML with functional creation:\n");
                Console.WriteLine(newTree.ToString());

            }
            catch (System.Xml.XmlException xmlEx) { Console.WriteLine(xmlEx); }
            catch (Exception Ex) { Console.WriteLine(Ex); }

        }
    }
}
