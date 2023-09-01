using System;
using System.Net;
using System.IO;
using System.Text;
using System.Management;

namespace IotDBHWInfo
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args[0] == "-cores") Console.WriteLine(GetNumberOfCores().ToString());
            if (args[0] == "-memory") Console.WriteLine(GetPhysicalRAMInMegabytes().ToString());			
        }
		
		static long GetPhysicalRAMInMegabytes()
		{
			try
			{
				using (ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT TotalPhysicalMemory FROM Win32_ComputerSystem"))
				{
					ManagementObjectCollection collection = searcher.Get();
					foreach (ManagementObject obj in collection)
					{
						ulong totalPhysicalMemoryBytes = Convert.ToUInt64(obj["TotalPhysicalMemory"]);
						long totalPhysicalMemoryMegabytes = (long)(totalPhysicalMemoryBytes / (1024 * 1024)); // Convert bytes to megabytes
						return totalPhysicalMemoryMegabytes;
					}
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
				return -1;
			}

			return -1; // Return -1 if query fails
		}
		
		static int GetNumberOfCores()
		{
			try
			{
				using (ManagementObjectSearcher searcher = new ManagementObjectSearcher("Select NumberOfCores from Win32_Processor"))
				{
					ManagementObjectCollection collection = searcher.Get();
					foreach (ManagementObject obj in collection)
					{
						return Convert.ToInt32(obj["NumberOfCores"]);
					}
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
				return 1;
			}

			return Environment.ProcessorCount; // Fallback to the number of logical processors if query fails
		}

        
    }
}


