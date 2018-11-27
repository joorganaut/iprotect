using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace CoreDataAccess
{
    public class ScriptLoader
    {
        static Dictionary<string, string> QueryHashSet = new Dictionary<string, string>();
        public static string GetString(string name, Assembly assembly)
        {
            string data = null;
            if (!QueryHashSet.TryGetValue(name, out data))
            {
                string resName = assembly.GetManifestResourceNames().FirstOrDefault(mf => mf.EndsWith(name));
                if (resName != null)
                {
                    using (StreamReader sr = new StreamReader(assembly.GetManifestResourceStream(resName)))
                    {
                        data = sr.ReadToEnd();
                        sr.Close();
                        QueryHashSet.Add(name, data);
                    }
                }                
            }
            return data;
        }
        public static string GetString(string name)
        {
            return ScriptLoader.GetString(name, typeof(ScriptLoader).Assembly);
        }
    }
}
