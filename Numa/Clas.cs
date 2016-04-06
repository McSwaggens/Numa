using System;
using System.Collections.Generic;
namespace Numa {
    public class Clas {
        
        public static bool DO_EXECUTE = true;

        public static Dictionary<string, bool> Flags = new Dictionary<string, bool>()
        {
            {"server",   false},
            {"nolog", false},
            {"view", false},
        };
        
        public static Dictionary<string, object> Setters  = new Dictionary<string, object>() {
            {"vsize", DataSize.KBIT},
            {"nic", 1}
        };
        
        public static Dictionary<List<string>, string> TextArguments = new Dictionary<List<string>, string>() {
            {new List<string>{"h", "help"}, 
            @"
--------------------[Numa Help]--------------------

[Usage]

Numa [[- | --] argument ] ...
Numa [[- | --] setting (value)]

[Description]
Numa is a network usage monitor,
the software can be ran as a server for clients to connect to via TCP.
This program is intended for server environments but can be used anywhere including
desktop environments.

You can directly view the network usage by using the argument --view


[Command Line Arguments]
Type these in for the desired effect.

-view
    View the network usage in real time.

-vsize
    Set amount of data viewed per second.
    Can be one of the following;
        BIT, BYTE, KBYTE, KBIT, MBYTE, MBIT, GBIT, GBYTE, TBIT, TBYTE

[Github]
http://www.Github.com/McSwaggens/Numa

Numa was created by Daniel Jones as free software.

--------------------[Numa Help]--------------------
"},
            {new List<string>{"v", "version"}, 
$@"
Numa v1.0
Numa was created by Daniel Jones as free software.
"}
        };
        
        
        public static void LoadParams(string[] args) {
            if (args.Length > 0)
            {
                for (int i = 0; i < args.Length; i++)
                {
                    if (args[i].StartsWith("-"))
                    {
                        
                        ///TODO: make difference between - and --
                        string flag = args[i].StartsWith("--")
                            ? args[i].TrimStart("--".ToCharArray())
                            : args[i].TrimStart("-".ToCharArray());
                        
                        if (string.IsNullOrWhiteSpace(flag))
                            continue;
                        
                        string TextOut;
                        if (ContainsTextArgKey(flag, out TextOut)) {
                            Console.WriteLine(TextOut);
                            DO_EXECUTE = false;
                            return;
                        }
                        if (ContainsSetterArgKey(flag)) {
                            i++;
                            int outint;
                            if (int.TryParse(args[i], out outint))
                                Setters[flag] = outint;
                            else Setters[flag] = flag;
                        }
                        else
                        if (Flags.ContainsKey(flag))
                            Flags[flag] = !Flags[flag];
                        else {
                            Logger.Error("Unknown flag: " + flag);
                            Logger.Warning("Aborting...");
                            return;
                        }
                    }
                }
            }
        }
        
        private static bool ContainsTextArgKey(string key, out string value) {
            value = "";
            foreach (KeyValuePair<List<string>, string> pair in TextArguments) {
                foreach (string tKey in pair.Key) if (tKey == key) { value = pair.Value; return true; }
            }
            return false;
        }
        
        private static bool ContainsSetterArgKey(string key) {
            foreach (KeyValuePair<string, object> pair in Setters) {
                if (pair.Key == key) return true;
            }
            return false;
        }
    }
}