using System;
using System.IO;
using Numa;
using static Numa.Logger;
using System.Threading;
using static Numa.OSInfo;
using static Numa.Clas;

namespace Numa
{
	public class Numa
	{
        
		public static string NumaRootDirectory = GetNumaRootDirectory();
		private static bool isRootDirectoryHealthy() => (Directory.Exists (NumaRootDirectory) || Directory.Exists(NumaRootDirectory + "ConnectionLogs/") || Directory.Exists(NumaRootDirectory + "NumaLogs/"));
        
        private static string GetNumaRootDirectory() { return OS_LINUX ? $"/home/{Environment.UserName}/.numa/" : "$/User/{Environment.UserName}/.numa/"; }

		public static void Main (string[] args)
		{
            LoadParams(args);
            
            if (!DO_EXECUTE) return;
            
            int nic = (int)Setters["nic"];
            
			Log ("Starting Numa...");
            
			Log ("Checking environment...");
			if (!isRootDirectoryHealthy ()) {
				Warning ("Nuna environment is either not healthy or is being booted for the first time...");
				Log ("Repairing environment");
				RepairEnvironment ();
				Log ("Successfully repaired environment...");
			}

			Log ("Pushing logs to live mode");

			Push ();
			logMode = LogMode.Write;

            Log("Initializing Network Interfaces...");


            Log($"Using interface: " + nic);
			Network.InitializeNetworkInterface ();

            bool running = true;
            while (running)
            {
                NetworkInterfaceStatus set = Network.fetchNetworkStatus(nic);
                Console.WriteLine(set.ReceivedKiloBytes + " KB/s");
                Thread.Sleep(1000);
            }

			Warning ("Closing Numa...");
			Log ("Bye!");
		}

		private static void RepairEnvironment() {
			if (!Directory.Exists(NumaRootDirectory))
				Directory.CreateDirectory (NumaRootDirectory);
			if (!Directory.Exists(NumaRootDirectory + "ConnectionLogs"))
				Directory.CreateDirectory (NumaRootDirectory + "ConnectionLogs");
			if (!Directory.Exists(NumaRootDirectory + "NumaLogs"))
				Directory.CreateDirectory (NumaRootDirectory + "NumaLogs");
		}
	}
}
