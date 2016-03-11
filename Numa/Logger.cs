using System;
using System.Globalization;
using System.Collections.Generic;
using System.IO;
namespace Numa
{
	public class Logger
	{
		public static LogMode logMode = LogMode.Cache;
		public static List<string> CachedLogs = new List<string> ();
		public static bool Save = true;
		public static readonly string FileLocation = Numa.NumaRootDirectory + "NumaLogs/";
		public static string FileName => DateTime.Today.ToString().Replace('/', '_').Split(' ')[0] + ".log";
		public static int LogSize => (int)new FileInfo(FileLocation + FileName).Length;

		public static void Log(string text) {
			text = "LOG\t" + text;
			WriteColor (text, ConsoleColor.White);
			Write (text);
		}

		public static void Warning(string text) {
			text = "WARN\t" + text;
			WriteColor (text, ConsoleColor.Yellow);
			Write (text);
		}

		public static void Error(string text) {
			text = "ERROR\t" + text;
			WriteColor (text, ConsoleColor.Red);
			Write (text);
		}

		private static void WriteColor(string text, ConsoleColor color) {
			ConsoleColor bfr = Console.ForegroundColor;
			Console.ForegroundColor = color;
			Console.WriteLine (text);
			Console.ForegroundColor = bfr;
		}

		private static void Write (string text) {
			string TimeStampedText = $"[{DateTime.Now}]\t{text}";

			if (logMode == LogMode.Write) {
				TimeStampedText += "\n";
				File.AppendAllText(FileLocation + FileName, TimeStampedText);
			}
			else CachedLogs.Add(TimeStampedText);
		}

		//Push the cached logs
		public static void Push() {
			foreach (string text in CachedLogs) {
				string t = text + "\n";
				File.AppendAllText(FileLocation + FileName, t);
			}
		}

		public enum LogMode {
			Cache, Write
		}
	}
}

