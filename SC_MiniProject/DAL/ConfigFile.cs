using JsonConfig;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace SC_MiniProject.DAL
{
    public class ConfigFile
    {
        protected static void ScanFiles()
        {
            string[] prefixes = new string[] { "../", "../../", "../../../" };
            foreach (var prefix in prefixes)
            {
                string path = Path.GetFullPath(prefix + "settings.conf");
                if (System.IO.File.Exists(path))
                {
                    string json = File.ReadAllText(path);
                    Config.User =  Config.ParseJson(json);
                    path = Path.GetFullPath(prefix + "default.conf");
                    if (!File.Exists(path)) break;
                    json = File.ReadAllText(path);
                    Config.Default = Config.ParseJson(json);
                    break;
                }
            }
        }

        public static string[] GetFruits()
        {
            ScanFiles();
            var v = Config.User.Fruits;
            return v;
        }

        public static string[] Sentences()
        {
            ScanFiles();
            return Config.User.Sentences;
        }

        public static object[] Questions()
        {
            ScanFiles();
            return Config.User.Questions;
        }
    }
}