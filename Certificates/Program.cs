using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Certificates
{
    class Program
    {
        static void Main(string[] args)
        {
            SignAndVerify();

            Console.ReadKey();
        }

        /// <summary>
        ///   LISTING 3-24  Signing and verifying data with a certificate
        /// </summary>
        public static void SignAndVerify()
        {
            string TextToSign = "Test paragraph";
            byte[] Signature = Sign(TextToSign, "cn=WouterDeKort");
            Console.WriteLine(Verify(TextToSign, Signature));

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <param name="certSubject"></param>
        /// <returns>rgbSignature</returns>
        static byte[] Sign(string text, string certSubject)
        {
            System.Security.Cryptography.X509Certificates.X509Certificate2 cert = GetCertificate();
            var csp = (System.Security.Cryptography.RSACryptoServiceProvider)cert.PrivateKey;
            byte[] hash = HashData(text);
            return csp.SignHash(hash, System.Security.Cryptography.CryptoConfig.MapNameToOID("SHA1"));
        }

        private static System.Security.Cryptography.X509Certificates.X509Certificate2 GetCertificate()
        {
            var my = new System.Security.Cryptography.X509Certificates.X509Store("testCertStore",
                System.Security.Cryptography.X509Certificates.StoreLocation.CurrentUser);

            my.Open(System.Security.Cryptography.X509Certificates.OpenFlags.ReadOnly);

            var certificate = my.Certificates[0];
            return certificate;
        }

        private static byte[] HashData(string text)
        {
            System.Security.Cryptography.HashAlgorithm hashAlgorithm = new System.Security.Cryptography.SHA1Managed();
            UnicodeEncoding encoding = new UnicodeEncoding();
            byte[] data = encoding.GetBytes(text);
            byte[] hash = hashAlgorithm.ComputeHash(data);
            return hash;
        }

        static bool Verify(string text, byte[] signature)
        {
            System.Security.Cryptography.X509Certificates.X509Certificate2 cert = GetCertificate();
            var csp = (System.Security.Cryptography.RSACryptoServiceProvider)cert.PublicKey.Key;
            byte[] hash = HashData(text);
            return csp.VerifyHash(hash,
                System.Security.Cryptography.CryptoConfig.MapNameToOID("SHA1"),
                signature);
        }

    }
}
