﻿using System;
using System.IO;
using Numa;
using static Numa.Logger;
using System.Threading;

namespace Numa
{
	public class Numa
	{
		public static string NumaRootDirectory = $"/Users/{Environment.UserName}/.Numa/";
		public static bool isRootDirectoryHealthy() {
			return (Directory.Exists (NumaRootDirectory) || Directory.Exists(NumaRootDirectory + "ConnectionLogs/") || Directory.Exists(NumaRootDirectory + "NumaLogs/"));
		}

		public static void Main (string[] args)
		{
			Log ("Starting Numa...");
			Log ("Checking environment...");
			if (!isRootDirectoryHealthy ()) {
				Warning ("Nuna environment is eithr not healthy or is being booted for the first time...");
				Log ("Repairing environment");
				RepairEnvironment ();
				Log ("Successfully repaired environment...");
			}

			Log ("Pushing logs to live mode");

			Push ();
			logMode = LogMode.Write;

            Log("Initializing Network Interfaces...");

            int nic = 1;

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

		public static void RepairEnvironment() {
			if (!Directory.Exists(NumaRootDirectory))
				Directory.CreateDirectory (NumaRootDirectory);
			if (!Directory.Exists(NumaRootDirectory + "ConnectionLogs"))
				Directory.CreateDirectory (NumaRootDirectory + "ConnectionLogs");
			if (!Directory.Exists(NumaRootDirectory + "NumaLogs"))
				Directory.CreateDirectory (NumaRootDirectory + "NumaLogs");
		}
	}
}
