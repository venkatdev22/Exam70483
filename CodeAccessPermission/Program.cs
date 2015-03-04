using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeAccessPermissionCAS
{
    class Program
    {
        static void Main(string[] args)
        {

            ImperativeCAS();

            Console.ReadKey();
        }

        /// <summary>
        /// LISTING 3-25  Declarative CAS
        /// </summary>
        [System.Security.Permissions.FileIOPermission(System.Security.Permissions.SecurityAction.Demand, AllLocalFiles = System.Security.Permissions.FileIOPermissionAccess.Read)]
        public void DeclarativeCAS()
        {
            //Method Body
        }

        /// <summary>
        /// LISTING 3-26  Imperative CAS
        /// </summary>
        public static void ImperativeCAS()
        {
            //System.Security.Permissions.FileIOPermission fileIoPermission = new System.Security.Permissions.FileIOPermission(System.Security.Permissions.PermissionState.None);
            string path=@"C:\Users\User\Documents\Visual Studio 2012\Projects\Exam70483\CodeAccessPermission\App_Code\TextFile.txt";
            System.Security.Permissions.FileIOPermission fileIoPermission = new System.Security.Permissions.FileIOPermission(System.Security.Permissions.FileIOPermissionAccess.Read,path);
            fileIoPermission.AllLocalFiles = System.Security.Permissions.FileIOPermissionAccess.Read;
            try
            {
                fileIoPermission.Demand();

                UnicodeEncoding byteConvertor=new UnicodeEncoding();
                //System.IO.StreamReader streamReader = new System.IO.StreamReader(path);
                var fStream= System.IO.File.Open(path, System.IO.FileMode.Open);
                byte[] textByte=byteConvertor.GetBytes("Sample Text");
                fStream.Write(textByte, 0, textByte.Length);
            }
            catch (System.Security.SecurityException sEx) { Console.WriteLine(sEx); }
        }
    }
}
