using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SymmetricEncryption
{
    class Program
    {
        static void Main(string[] args)
        {
            string original = "My secret data!";

            using (System.Security.Cryptography.SymmetricAlgorithm symmetricAlgorithm =
                new System.Security.Cryptography.AesManaged())
            {
                byte[] encrypted = Encrypt(symmetricAlgorithm, original);
                string roundtrip = Decrypt(symmetricAlgorithm, encrypted);

                // Displays: My secret data!  
                Console.WriteLine("Original:   {0}", original);
                Console.WriteLine("Round Trip: {0}", roundtrip);
            }
            Console.ReadKey();
        }


        static byte[] Encrypt(System.Security.Cryptography.SymmetricAlgorithm aesAlg, string plainText)
        {
            System.IO.MemoryStream msEncrypt=new System.IO.MemoryStream();
            try
            {
                System.Security.Cryptography.ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                    using (var csEncrypt =
                        new System.Security.Cryptography.CryptoStream(msEncrypt, encryptor, System.Security.Cryptography.CryptoStreamMode.Write))
                    {
                        using (var swEncrypt = new System.IO.StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(plainText);
                        }  
                    }
            }
            catch (Exception Ex) { Console.WriteLine(Ex); }

            return msEncrypt.ToArray();
        }

        static string Decrypt(System.Security.Cryptography.SymmetricAlgorithm aesAlg, byte[] cipherText)
        {
            string value = string.Empty;
            try
            {
                System.Security.Cryptography.ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                using (var msDecrypt = new System.IO.MemoryStream(cipherText))
                {
                    using (var csDecrypt =
                        new System.Security.Cryptography.CryptoStream(msDecrypt, decryptor, System.Security.Cryptography.CryptoStreamMode.Read))
                    {
                        var srDecrypt = new System.IO.StreamReader(csDecrypt);
                        value= srDecrypt.ReadToEnd();
                    }
                }
            }
            catch (Exception Ex) { Console.WriteLine(Ex); }

            return value;
        }
    }
}
