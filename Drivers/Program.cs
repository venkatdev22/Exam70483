using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drives
{
    class Program
    {
        static void Main(string[] args)
        {
            var driverInfo = System.IO.DriveInfo.GetDrives();
            foreach (var dInfo in driverInfo)
            {
                Console.WriteLine("\t*****Drive {0} *****", dInfo.Name);
                Console.WriteLine("File Type: {0}", dInfo.DriveType);
                if (dInfo.IsReady == true)
                {
                 Console.WriteLine("Volume label: {0}", dInfo.VolumeLabel); 
        Console.WriteLine("File system: {0}", dInfo.DriveFormat);
        Console.WriteLine("Available space to current user:{0} bytes",dInfo.AvailableFreeSpace);
      //Console.WriteLine("Available space to current user:{0} GB", ((dInfo.AvailableFreeSpace/1024)/1024)/1024); 
        Console.WriteLine("Total available space:{0} bytes",dInfo.TotalFreeSpace);
        Console.WriteLine("Total size of driver:{0} bytes", dInfo.TotalSize);
                }
                Console.WriteLine("\n");
            }
            Console.ReadKey();
        }
    }
}
