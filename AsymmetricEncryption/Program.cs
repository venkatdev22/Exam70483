using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsymmetricEncryption
{
    class Program
    {
        static void Main(string[] args)
        {
            //doRSAencryt();
            doRSAencryptWithKeyContainer();
            Console.ReadKey();
        }

        static void doRSAencryt()
        {
            try
            {
                System.Security.Cryptography.RSACryptoServiceProvider rsa = new System.Security.Cryptography.RSACryptoServiceProvider();
                string publicKey = rsa.ToXmlString(false);
                string privateKey = rsa.ToXmlString(true);
                Console.WriteLine("*********\nPublic Key\n*********");
                Console.WriteLine(publicKey);
                Console.WriteLine("*********\nPrivate Key\n*********");
                Console.WriteLine(privateKey);

                UnicodeEncoding byteConverter = new UnicodeEncoding();
                byte[] dataToEncrypt = byteConverter.GetBytes("Rise, Awake, Do not stop till the goal is reached");

                byte[] encryptedData;
                using (var RSA = new System.Security.Cryptography.RSACryptoServiceProvider())
                {
                    RSA.FromXmlString(publicKey);
                    encryptedData = RSA.Encrypt(dataToEncrypt, false);
                }
                string encryptedMsg = byteConverter.GetString(encryptedData);
                Console.WriteLine("\nEncrypted Msg:{0}", encryptedMsg);


                byte[] decryptedData;
                using (var RSA = new System.Security.Cryptography.RSACryptoServiceProvider())
                {
                    RSA.FromXmlString(privateKey);
                    decryptedData = RSA.Decrypt(encryptedData, false);
                }
                string decryptedMsg = byteConverter.GetString(decryptedData);
                Console.WriteLine("\nDecrypted Msg:{0}", decryptedMsg);

                WriteAsXMl(publicKey,"PublicKey.xml");
                WriteAsXMl(privateKey, "PrivateKey.xml");
            }
            catch (System.Security.Cryptography.CryptographicException cEx) { Console.WriteLine(cEx); }
        }

        static void doRSAencryptWithKeyContainer()
        {
            try
            {
                string msg = "Rise, Awake, Do not stop till the goal is reached";
                UnicodeEncoding byteConvertor = new UnicodeEncoding();
                byte[] byteMsg = byteConvertor.GetBytes(msg);

                var csp = new System.Security.Cryptography.CspParameters();
                csp.KeyContainerName = "SecretContainer";
                byte[] encryptedMsg;
                using (var RSAwithKC = new System.Security.Cryptography.RSACryptoServiceProvider(csp))
                {
                    encryptedMsg = RSAwithKC.Encrypt(byteMsg, false);
                }
                Console.WriteLine("Encrypted Msg:{0}", byteConvertor.GetString(encryptedMsg));

                byte[] deCryptedMsg;
                using (var RSAwithKC = new System.Security.Cryptography.RSACryptoServiceProvider(csp))
                {
                    deCryptedMsg = RSAwithKC.Decrypt(encryptedMsg, false);
                }
                Console.WriteLine("decrypted Msg:{0}", byteConvertor.GetString(deCryptedMsg));

            }
            catch (System.Security.Cryptography.CryptographicException cEx) { Console.WriteLine(cEx); }

        }

        static void WriteAsXMl(string xmlString,string Filename)
        {
            string path=@"C:\Users\User\Documents\Visual Studio 2012\Projects\Exam70483\AsymmetricEncryption\App_Data";
            System.Xml.XmlDocument xmlDoc = new System.Xml.XmlDocument();
            xmlDoc.LoadXml(xmlString);
            xmlDoc.Save(System.IO.Path.Combine(path,Filename));
        }
    }
}
