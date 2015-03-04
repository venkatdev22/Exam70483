using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecureStringApp
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                using (System.Security.SecureString secureString = new System.Security.SecureString())
                {
                    Console.WriteLine("\nPlease enter password:");
                    while (true)
                    {
                        ConsoleKeyInfo cki = Console.ReadKey(true);
                        if (cki.Key != ConsoleKey.Backspace)
                        {
                            if (cki.Key == ConsoleKey.Enter)
                            {
                                break;
                            };
                            secureString.AppendChar(cki.KeyChar);
                            Console.Write("*");
                        }
                    }
                    secureString.MakeReadOnly();
                    ConvertToUnsecureString(secureString);
                }
            }
            catch (System.Security.SecurityException sEx) { Console.WriteLine(sEx); }
            catch (Exception Ex) { Console.WriteLine(Ex); }
            Console.ReadKey();
        }

        public static void ConvertToUnsecureString(System.Security.SecureString secureString)
        {
            IntPtr unmanagedString = IntPtr.Zero;
            try
            {
                unmanagedString = System.Runtime.InteropServices.Marshal.SecureStringToGlobalAllocUnicode(secureString);
                /*//Decrypt method
                   SecureStringToBSTR
                   SecureStringToCoTaskMemAnsi
                   SecureStringToCoTaskMemUnicode
                   SecureStringToGlobalAllocAnsi
                   SecureStringToGlobalAllocUnicode
                */
                Console.WriteLine("\n{0}", System.Runtime.InteropServices.Marshal.PtrToStringUni(unmanagedString));
            }

            catch (Exception Ex) { Console.WriteLine(Ex); }
            ///The finally statement makes sure that the string is removed from memory even if there is an exception thrown in the code.
            finally
            {
                System.Runtime.InteropServices.Marshal.ZeroFreeGlobalAllocUnicode(unmanagedString);
                /*
                //Clear memory method
                ZeroFreeBSTR
                ZeroFreeCoTaskMemAnsi
                ZeroFreeCoTaskMemUnicode
                ZeroFreeGlobalAllocAnsi
                ZeroFreeGlobalAllocUnicode  
                */
            }

        }

    }
}
