using System;
using System.Xml;
using System.Xml.XPath;

namespace UseXPathNavigator
 {
  class BooksXmlNavigator
  {
   [STAThread]
   static void Main(string[] args)
   {
    try
     {
       string XmlFileName = @"C:\Users\User\Documents\Visual Studio 2012\Projects\Exam70483\ConsumeXML2\books.xml";
       // create an XPathDocument object
       XPathDocument xmlPathDoc = new  XPathDocument(XmlFileName);

       // create a navigator for the xpath doc
       XPathNavigator xNav = xmlPathDoc.CreateNavigator();

        //navigate and query the document
        NavigateBooksXml(xNav);
        FindAllTitles(xNav);
        FindBooksByCategory(xNav, "IT");
		Console.Read();
        Console.ReadKey();
     }
     catch (XmlException e) 
     {
      Console.WriteLine("Exception: " + e.ToString());
     }
   }

   /*
   NavigateBooksXml method
   Accpets: XPathNavigator object
   Returns: Nothing
   Purpose: This method will iterate through the xml document provided in the 
   XPathNavigator object and display it to the console    
   */
 public static void NavigateBooksXml(XPathNavigator xPathNav)
  {
   // move to the root and the first element - <books>
   xPathNav.MoveToRoot();
   xPathNav.MoveToFirstChild();

   // move to first <book> element
   xPathNav.MoveToFirstChild();
   Console.WriteLine("Printing contents of books.xml:");
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
	  //move back to the parent
      xPathNav.MoveToParent();
    }
   } while (xPathNav.MoveToNext());
}




   /*
   FindAllTitles method
   Accpets: XPathNavigator object
   Returns: Nothing
   Purpose: This method will iterate through the xml document provided in the 
   XPathNavigator object and display all of the title elements    
   */
 public static void FindAllTitles(XPathNavigator p_xPathNav)
  {
   //run the XPath query
   XPathNodeIterator xPathIt = p_xPathNav.Select("//book/title");
   //use the XPathNodeIterator to display the results
   if (xPathIt.Count > 0) 
   {
    Console.WriteLine("");
    Console.WriteLine("The catalog contains the following titles:");
    while (xPathIt.MoveNext())
    {
     Console.WriteLine(xPathIt.Current.Value);
    }
   }
   else 
   {
    Console.WriteLine("No titles found in catalog.");
   }
 }

 
    /*
   FindBooksByCategory method
   Accpets: XPathNavigator p_xPathNav
            string p_Category
   Returns: Nothing
   Purpose: This method will iterate through the xml document provided in the 
   XPathNavigator object and search book elements for the category attribute passed into the method
   */
 public static void FindBooksByCategory(XPathNavigator p_xPathNav,string p_Category)
  {
   string query = "//book[@category=\'" + p_Category + "\']";

   XPathNodeIterator xPathIt = p_xPathNav.Select(query);
   //use the XPathNodeIterator to display the results
   if (xPathIt.Count > 0)
   {
    Console.WriteLine("");
    Console.WriteLine("The following books are in the {0} category:", p_Category);
    while (xPathIt.MoveNext())
    {
     Console.WriteLine(xPathIt.Current.Value);
    }
   }
    else
   {
    Console.WriteLine("No books found in the {0} category", p_Category);
   }
}

}
}