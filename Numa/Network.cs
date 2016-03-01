using System;
using System.Diagnostics;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;

namespace Numa
{
	public class Network
	{
		private static NetworkInterface[] nicArr;
		private static long BytesSent = 0;
		private static long BytesReceived = 0;
		public static void InitializeNetworkInterface()
		{
			nicArr = NetworkInterface.GetAllNetworkInterfaces();
		}
		public static void UpdateNetworkInterface()
		{
			for (int i = 0; i < nicArr.Length; i++) {
				Console.WriteLine ("---------------\nNIC " + i);
				NetworkInterface nic = nicArr [i];
				IPv4InterfaceStatistics interfaceStats = nic.GetIPv4Statistics ();
				int bytesSentSpeed = (int)(interfaceStats.BytesSent - BytesSent);
				int bytesReceivedSpeed = (int)(interfaceStats.BytesReceived - BytesReceived);
				Console.WriteLine("SPEED: " + nic.Speed.ToString());
				Console.WriteLine ("INTERFACE TYPE: " + nic.NetworkInterfaceType.ToString ());
				BytesReceived = interfaceStats.BytesReceived;
				BytesSent = interfaceStats.BytesSent;
				if (BytesSent > 0 || BytesReceived > 0 || bytesSentSpeed > 0 || bytesReceivedSpeed > 0)
					throw new Exception ("FOUND IT");
				Console.WriteLine ($"Bytes Sent: {BytesSent}");
				Console.WriteLine ($"Bytes Received: {BytesReceived}");
			}
		}
	}
}

