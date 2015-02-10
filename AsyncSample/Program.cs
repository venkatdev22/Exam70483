using System;
using System.IO;
using System.Threading.Tasks;


namespace AsyncSample
{
    class AsyncProgram
    {
        static void Main(string[] args)
        {
            try
            {
            var asyncProgram = new AsyncProgram();
            var testValue=asyncProgram.CreateAndWriteAsyncToFile();
            //if (testValue.IsCompleted)
            {
                using(StreamReader streamReader=File.OpenText(@"C:\Users\User\Documents\Visual Studio 2012\Projects\Exam70483\AsyncSample\Data\test.txt"))
                {
                  Console.WriteLine(streamReader.ReadToEnd());
                }
            }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            Console.ReadKey();
        }

        public async Task CreateAndWriteAsyncToFile()
        {
            using (FileStream stream = new FileStream(@"C:\Users\User\Documents\Visual Studio 2012\Projects\Exam70483\AsyncSample\Data\test.txt", FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None, 4096, true))
            {
                byte[] data = new byte[8];
                new Random().NextBytes(data);
                await stream.WriteAsync(data, 0, data.Length);
            }



            var uniencoding = new System.Text.UnicodeEncoding();
            string filename = @"C:\Users\User\Documents\Visual Studio 2012\Projects\Exam70483\AsyncSample\Data\userinputlog.txt";

            byte[] byteString = new byte[8];
            new Random().NextBytes(byteString);
            string result = uniencoding.GetString(byteString);
            Console.WriteLine(result);
            using (FileStream SourceStream = File.Open(filename, FileMode.OpenOrCreate))
            {
                SourceStream.Seek(0, SeekOrigin.End);
                await SourceStream.WriteAsync(byteString, 0, result.Length);
            }
        }
    }
}
