using System;
using System.IO;
using System.Security.AccessControl;
namespace DirectoryIO
{
    class Program
    {
        static void Main(string[] args)
        {
            string folderPath = @"C:\Users\User\Documents\Visual Studio 2012\Projects\Exam70483\Directory";
            string folderName = "TestDirectory";
            if (Directory.Exists(folderPath))
            {
                DirectoryInfo directoryInfo = new DirectoryInfo(Path.Combine(folderPath, folderName));
                directoryInfo.Create();
                folderPath = Path.Combine(folderPath, folderName);
                directoryInfo = new DirectoryInfo(folderPath);
                DirectorySecurity directorySecurity = directoryInfo.GetAccessControl();
                var fileSystemAccessRule = new FileSystemAccessRule(@"AMX205\User", FileSystemRights.Read, AccessControlType.Allow);//string identity cab be @"DomainName\AccountName"
                directorySecurity.AddAccessRule(fileSystemAccessRule);
                directoryInfo.SetAccessControl(directorySecurity);

                #region FileSystemAccessRule Constructors
                ////new SecurityIdentifier(WellKnownSidType.WorldSid, null) This can be used rather than "Everyone".
                //var filesystemAccessRule2 = new FileSystemAccessRule(new SecurityIdentifier(WellKnownSidType.WorldSid, null), FileSystemRights.ReadAndExecute, AccessControlType.Allow);
                //var filesystemAccessRule3 = new FileSystemAccessRule(new SecurityIdentifier(System.Security.Principal.WellKnownSidType.WorldSid, null), FileSystemRights.ReadAndExecute, InheritanceFlags.None, PropagationFlags.None, AccessControlType.Allow);
                //var filesystemAccessRule4 = new FileSystemAccessRule("everyone", FileSystemRights.ReadAndExecute, InheritanceFlags.None, PropagationFlags.None, AccessControlType.Allow);
                #endregion

                string fileName = "test.txt";
                string filePath = Path.Combine(folderPath, fileName);
                if (!File.Exists(filePath))
                {
                    File.Create(filePath);
                    
                }

                ///Equivalent of File.Exists(filePath)
                FileInfo fileInfo = new FileInfo(filePath);
                if (fileInfo.Exists)
                {
                    RemoveFileSecurity(filePath, @"AMX205\User", FileSystemRights.ReadAndExecute, AccessControlType.Allow);
                    AddFileSecurity(filePath, @"AMX205\User", FileSystemRights.FullControl, AccessControlType.Allow);
                }

                ///SearchPattern for subfolder level 5
                ///Sample wildcard querry "*Test*" , "?estDirectory"
                ListDirectories(directoryInfo.Parent, "TestDirector?", 5, 0);  
            }
            Console.ReadKey();
        }

        // Adds an ACL entry on the specified file for the specified account. 
        public static void AddFileSecurity(string fileName, string account, FileSystemRights rights, AccessControlType controlType)
        {
            // Get a FileSecurity object that represents the 
            // current security settings.
            FileSecurity fSecurity = File.GetAccessControl(fileName);

            // Add the FileSystemAccessRule to the security settings.
            fSecurity.AddAccessRule(new FileSystemAccessRule(account, rights, controlType));

            // Set the new access settings.
            File.SetAccessControl(fileName, fSecurity);

        }

        // Removes an ACL entry on the specified file for the specified account. 
        public static void RemoveFileSecurity(string fileName, string account, FileSystemRights rights, AccessControlType controlType)
        {
            // Get a FileSecurity object that represents the 
            // current security settings.
            FileSecurity fSecurity = File.GetAccessControl(fileName);

            // Remove the FileSystemAccessRule from the security settings.
            fSecurity.RemoveAccessRule(new FileSystemAccessRule(account, rights, controlType));

            // Set the new access settings.
            File.SetAccessControl(fileName, fSecurity);
        }

        /// <summary>
        /// search for directory
        /// </summary>
        /// <param name="directoryInfo"></param>
        /// Root directory to start search
        /// <param name="searchPattern"></param>
        /// search pattern containing whildcard querry
        /// <param name="maxLevel"></param>
        /// sub folder level to end search
        /// <param name="currentLevel">
        /// while looping, points current folder level.
        /// </param>
        private static void ListDirectories(DirectoryInfo directoryInfo, string searchPattern, int maxLevel, int currentLevel)
        {
            if (currentLevel >= maxLevel)
            {
                return;
            }

            string indent = new string('-', currentLevel);

            try
            {
                DirectoryInfo[] subDirectories = directoryInfo.GetDirectories(searchPattern);
                foreach (DirectoryInfo subDirectory in subDirectories)
                {
                    Console.WriteLine(indent + subDirectory.Name);
                    ListDirectories(subDirectory, searchPattern, maxLevel, currentLevel + 1);
                }
            }
            catch (UnauthorizedAccessException)
            {
                // You don’t have access to this folder.  
                Console.WriteLine(indent + "Can’t access: " + directoryInfo.Name);
                return;
            }
            catch (DirectoryNotFoundException)
            {
                // The folder is removed while iterating 
                Console.WriteLine(indent + "Can't find: " + directoryInfo.Name);
                return;
            }
        }
    }
}
