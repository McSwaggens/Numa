using System;

namespace Numa
{
	public class NumaUtils
	{
		public static byte[] GetBytes(string str)
		{
			byte[] data = new byte[str.Length];
			char[] carr = str.ToCharArray ();
			for (int i = 0; i < carr.Length; i++) {
				data [i] = BitConverter.GetBytes (carr [i])[0];
			}
			return data;
		}
	}
}

