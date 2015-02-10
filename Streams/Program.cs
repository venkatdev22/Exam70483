using System;
using System.IO;
using System.Linq;

namespace Streams
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                string folder = @"C:\Users\User\Documents\Visual Studio 2012\Projects\Exam70483\Streams\FileDirectory";
                string fileName = "testfile1.txt";
                string filePath = Path.Combine(folder,fileName);

                writeFileByFileStream(filePath);
                readFile(filePath);
                compressFile(folder);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            Console.ReadKey();
        }

        public static void writeFileByFileStream(string filePath)
        {
            using (FileStream fileStream = File.Create(filePath))
            {
                string myValue = "MyValue";
                byte[] data = System.Text.Encoding.UTF8.GetBytes(myValue);
                fileStream.Write(data, 0, data.Length);
            }


            using (StreamWriter streamWriter = File.CreateText(filePath))
            {
                string myValue = "MyValue";
                streamWriter.Write(myValue);
            }

        }

        public static void readFile(string filePath)
        {
            using (StreamReader streamReader = File.OpenText(filePath))
            {
                Console.WriteLine("StreamReader:\n{0}\n",streamReader.ReadToEnd());
            }

            using (FileStream fileStream = File.OpenRead(filePath))
            {
                byte[] data = new byte[fileStream.Length];
                for (int i = 0; i < fileStream.Length; i++)
                {
                    data[i] = Convert.ToByte(fileStream.ReadByte());
                    //data[i] = (byte)fileStream.ReadByte();
                }
                Console.WriteLine("FileStream:\n{0}\n",System.Text.Encoding.UTF8.GetString(data));
            }
        }

        public static void compressFile(string folder)
        {
            string uncompressedFilePath = Path.Combine(folder, "uncompressed.txt");
            string compressedFilePath = Path.Combine(folder, "compressed.zip");
            byte[] dataToCompress = Enumerable.Repeat((byte)'a', 1024 * 1024).ToArray();///1024/1024=1KB
            using (FileStream uncompressedFileStream = File.Create(uncompressedFilePath))
            {
                uncompressedFileStream.Write(dataToCompress, 0, dataToCompress.Length);
            }
            using (FileStream compressedFileStream = File.Create(compressedFilePath))
            {
                using (var compressionStream = new System.IO.Compression.GZipStream(compressedFileStream, System.IO.Compression.CompressionMode.Compress))
                {
                    compressionStream.Write(dataToCompress, 0, dataToCompress.Length);
                }
            }
        }

        public static void fileBuffering(string filePath)
        {
            using (FileStream fileStream = File.Open(filePath,FileMode.OpenOrCreate,FileAccess.ReadWrite))
            {
                using (BufferedStream bufferedStream = new BufferedStream(fileStream))
                {
                    using (StreamWriter streamWriter = new StreamWriter(bufferedStream))
                    {
                        streamWriter.WriteLine("A line of text.");
                    }
                }
            }
        }

    }
}
