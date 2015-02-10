using System;
using System.Net;
using System.IO;

namespace Network
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var webRequest = WebRequest.Create("http://www.microsoft.com");

                WebResponse webResponse = webRequest.GetResponse();
                StreamReader streamReader = new StreamReader(webResponse.GetResponseStream());
                string folder = @"C:\Users\User\Documents\Visual Studio 2012\Projects\Exam70483\Streams\FileDirectory";
                string fileName = "microsoft.com.html";

                using (StreamWriter streamWriter = File.CreateText(Path.Combine(folder, fileName)))
                {
                    string webText = streamReader.ReadToEnd();
                    streamWriter.Write(webText); ///will work
                    //streamWriter.Write(streamReader.ReadToEnd()); ///Won't work
                }
                webResponse.Close();
            }

            catch (FileNotFoundException ex)
            {
                Console.WriteLine(ex);
            }
            catch (UnauthorizedAccessException ex)
            {
                Console.WriteLine(ex);
            }
            Console.ReadKey();
        }
    }
}
