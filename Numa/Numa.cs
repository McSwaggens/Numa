using System;
using System.IO;
using Numa;
using static Numa.Logger;
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
			Logger.Log ("Starting Numa...");
			Logger.Log ("Checking environment...");
			if (!isRootDirectoryHealthy ()) {
				Logger.Warning ("Nuna environment is eithr not healthy or is being booted for the first time...");
				Logger.Log ("Repairing environment");
				RepairEnvironment ();
				Logger.Log ("Successfully repaired environment...");
			}

			Logger.Log ("Pushing logs to live mode");

			Logger.Push ();
			Logger.mode = Logger.LogMode.Write;

			Network.InitializeNetworkInterface ();
			while (true)
				Network.UpdateNetworkInterface ();

			Logger.Warning ("Closing Numa...");
			Logger.Log ("Bye!");
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
