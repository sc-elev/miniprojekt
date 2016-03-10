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
        public static string[] GetFruits()
        {
            string[] prefixes = new string[] { "../", "../../", "../../../" };
            foreach (var prefix in prefixes)
            {
                string path = Path.GetFullPath(prefix + "settings.conf");
                if (System.IO.File.Exists(path))
                {
                    Config.User =
                        Config.ApplyJsonFromPath(path, Config.User);
                    path = Path.GetFullPath(prefix + "default.conf");
                    Config.Default =
                        Config.ApplyJsonFromPath(path, Config.Default);
                    break;
                }
            }
            var v = Config.User.Fruits;
            return v;
        }
    }
}