using System;

class Program
{
    static void Main()
    {
        // Comment out the first 1-2 method invocations.
        try
        {
            A();
          //  B();
            C(null);
        }
       catch (Exception ex)
        {
         Console.WriteLine(ex);
        }

       Console.ReadKey();
    }

    static void A()
    {
        // Rethrow syntax.
       try
        {
            int value = 1 / int.Parse("0");
        }
       catch(Exception ex)
        {
            Console.WriteLine(ex);
       }
    }

    static void B()
    {
        // Filtering exception types.
        try
        {
            int value = 1 / int.Parse("0");
        }
        catch (DivideByZeroException ex)
        {
            throw ex;
        }
    }

    static void C(string value)
    {
        // Generate new exception.
        if (value == null)
        {
            throw new ArgumentNullException("\n ********value****** \n");
        }
    }
}