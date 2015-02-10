using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsumeXML
{
    class Program
    {
        static void Main(string[] args)
        {
            //string xml = "<?xml version=\"1.0\" encoding=\"utf-8\" ?><people><person firstname=\"john\" lastname=\"doe\"><contactdetails><emailaddress>john@unknown.com</emailaddress></contactdetails> </person><person firstname=\"jane\" lastname=\"doe\"><contactdetails><emailaddress>jane@unknown.com</emailaddress><phonenumber>001122334455</phonenumber></contactdetails></person></people>";
            string xml = @"<?xml version=""1.0"" encoding=""utf-8"" ?><people><person firstname=""john"" lastname=""doe""><contactdetails><emailaddress>john@unknown.com</emailaddress></contactdetails></person><person firstname=""jane"" lastname=""doe""><contactdetails><phonenumber>001122334455</phonenumber><emailaddress>jane@unknown.com</emailaddress></contactdetails></person></people>";


            //SaveXmlAsDocument(xml);

            //ParsingXMLwithXmlReader(xml);
            //CreatingXMLwithXmlWriter();

            XpathNavigatorSample(xml);

            Console.ReadKey();
        
        }

        /// <summary>
        /// LISTING 4-43  Parsing an XML file with an XmlReader
        /// </summary>
        static void ParsingXMLwithXmlReader(string xml)
        {
            string sampleXml = "<?xml version=\"1.0\" encoding=\"utf-8\" standalone=\"no\"?><root><EmptyElement /><NonEmptyElement Name=\"NonEmptyElement\">Test Value<SubElement Name=\"SubElement\" /></NonEmptyElement></root>";
            try
            {
                using (var stringReader = new System.IO.StringReader(sampleXml))
            {
                using (var xmlReader =System.Xml.XmlReader.Create(stringReader,new System.Xml.XmlReaderSettings(){IgnoreWhitespace=true}))
                {
                    xmlReader.MoveToContent();
                    xmlReader.ReadStartElement("root");
                    xmlReader.ReadStartElement("EmptyElement");
                    xmlReader.ReadStartElement("NonEmptyElement");
                    //Console.WriteLine(xmlReader.GetAttribute("Name"));
                   // xmlReader.ReadStartElement("SubElement");
                    
                   // xmlReader.ReadEndElement();

                    System.Xml.XmlDocument xmlDocument = new System.Xml.XmlDocument();
                    xmlDocument.LoadXml(xml);
                    

                    //System.Xml.XmlNode xmlNode = xmlDocument.ReadNode(xmlReader);
                    System.Xml.XmlNodeReader xmlNodeReader = new System.Xml.XmlNodeReader(xmlDocument);
                    xmlNodeReader.Read();
                    if (xmlNodeReader.HasAttributes)
                    {
                        Console.WriteLine(xmlNodeReader.MoveToAttribute("firstname"));
                    }
                    //xmlReader.ReadStartElement("people");
                    //xmlReader.ReadStartElement("person");
                    //string firstName = xmlReader.GetAttribute("firstName");
                    //string lastName = xmlReader.GetAttribute("lastName");
                    //Console.WriteLine("Person: {0} {1}", firstName, lastName); 

                    
                 
                    
                    
                    //Console.WriteLine("contactdetails");
                    //xmlReader.ReadStartElement("contactdetails"); 
                    //string emailAddress = xmlReader.ReadString(); 
                    //Console.WriteLine("Email address: {0}", emailAddress);

                    
                    //xmlReader.ReadStartElement("contactdetails");
                    //xmlReader.ReadStartElement("phonenumber");
                    //emailAddress = xmlReader.ReadString();
                    //Console.WriteLine("Email address: {0}", emailAddress);
                }
            }
            }
            catch (System.IO.IOException Ioe)
            {
                Console.WriteLine("IOException:{0}\n", Ioe);
            }
            catch (System.Xml.XmlException Xmle)
            {
                Console.WriteLine("XmlException:{0}\n", Xmle);
            }
        }

        static void SaveXmlAsDocument(string xml)
        {
            try
            {
                System.Xml.XmlDocument xmlDocument = new System.Xml.XmlDocument();
                xmlDocument.LoadXml(xml);
                xmlDocument.Save(@"C:\Users\User\Documents\Visual Studio 2012\Projects\Exam70483\ConsumeXML\App_Data\LISTING443.xml");
            }
            catch (System.Xml.XmlException Xmle)
            {
                Console.WriteLine("XmlException:{0}\n", Xmle);
            }
        }

        /// <summary>
        /// Creating an XML file with XmlWriter
        /// </summary>
        static void CreatingXMLwithXmlWriter()
        {
            var stream = new System.IO.StringWriter();

            using (var writer = System.Xml.XmlWriter.Create(stream, new System.Xml.XmlWriterSettings() { Indent = true }))
            {
                writer.WriteStartDocument();
                writer.WriteStartElement("People");
                writer.WriteStartElement("Person");
                writer.WriteAttributeString("firstName", "John");
                writer.WriteAttributeString("lastName", "Doe");
                writer.WriteStartElement("ContactDetails");
                writer.WriteElementString("EmailAddress", "john@unknown.com");
                writer.WriteEndElement();
                writer.WriteEndElement();
                writer.Flush();
            }
            Console.WriteLine(stream.ToString());
        }

        static void XpathNavigatorSample(string xml)
        {
            System.IO.MemoryStream stream = new System.IO.MemoryStream();
            System.IO.StreamWriter streamWriter = new System.IO.StreamWriter(stream);
            streamWriter.Write(xml);
            streamWriter.Flush();
            //stream.Position = 0;
            
           
            System.Xml.XmlDocument xmlDocument = new System.Xml.XmlDocument();
         
            xmlDocument.LoadXml(xml);
            System.Xml.XPath.XPathDocument xPathDocument = new System.Xml.XPath.XPathDocument(@"C:\Users\User\Documents\Visual Studio 2012\Projects\Exam70483\ConsumeXML\App_Data\LISTING443.xml");
            System.Xml.XPath.XPathNavigator xPathNavigator = xPathDocument.CreateNavigator();
            NavigateXml(xPathNavigator);
            
        }

        /*
   NavigateBooksXml method
   Accpets: XPathNavigator p_xPathNav
   Returns: Nothing
   Purpose: This method will iterate through the xml document provided in the 
   XPathNavigator object and display it to the console    
*/

        public static void NavigateXml(System.Xml.XPath.XPathNavigator xPathNav)
        {
            System.Xml.XPath.XPathNodeIterator xPathNodeIterator=  xPathNav.Select("//person/contactdetails");
            if (xPathNodeIterator.Count > 0)
            {
                Console.WriteLine("");
                Console.WriteLine("The catalog contains the following titles:");

                //begin to loop through the titles and begin to display them
                while (xPathNodeIterator.MoveNext())
                {
                    Console.WriteLine(xPathNodeIterator.Current.Value);
                }
            }
            else
            {
                Console.WriteLine("No titles found in catalog.");
            }

            
            // move to the root and the first element - <books>
            xPathNav.MoveToRoot();
            xPathNav.MoveToFirstChild();

            // move to first <book> element
            xPathNav.MoveToFirstChild();
            Console.WriteLine("Printing contents of books.xml:");

            //begin looping through the nodes
            do
            {
                // list attribute;
                if (xPathNav.MoveToFirstAttribute())
                {
                    Console.WriteLine(xPathNav.Name + "=" + xPathNav.Value);
                    // go back from the attributes to the parent element
                    xPathNav.MoveToParent();
                }

                //display the child nodes
                if (xPathNav.MoveToFirstChild())
                {
                    Console.WriteLine(xPathNav.Name + "=" + xPathNav.Value);
                    while (xPathNav.MoveToNext())
                    {
                        Console.WriteLine(xPathNav.Name + "=" + xPathNav.Value);
                    }
                    xPathNav.MoveToParent();
                }
            } while (xPathNav.MoveToNext());
        }
    }
}
