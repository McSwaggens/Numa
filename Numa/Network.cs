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
		public static NetworkInterfaceStatus fetchNetworkStatus(int NIC)
		{
			NetworkInterface nic = nicArr [NIC];
			IPv4InterfaceStatistics interfaceStats = nic.GetIPv4Statistics ();
			long bytesSentSpeed = interfaceStats.BytesSent - BytesSent;
			long bytesReceivedSpeed = interfaceStats.BytesReceived - BytesReceived;
            BytesSent = interfaceStats.BytesSent;
            BytesReceived = interfaceStats.BytesReceived;
            NetworkInterfaceStatus dataSet = new NetworkInterfaceStatus();
            dataSet.SentBytes = bytesSentSpeed;
            dataSet.ReceivedBytes = bytesReceivedSpeed;
            return dataSet;
		}
	}
}

