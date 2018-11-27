using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace CoreBusinessLogic
{
    public class FileLoader
    {
        public static string GetString(string name)
        {
            string data = null;

            using (StreamReader sr = new StreamReader(name))
            {
                data = sr.ReadToEnd();
                sr.Close();
            }
            return data;
        }

        public static byte[] GetImageBytes(string name)
        {
            return GetImageBytes(name, typeof(FileLoader).Assembly);
        }
        static Dictionary<string, byte[]> ImageHashTable = new Dictionary<string, byte[]>();
        public static byte[] GetImageBytes(string name, Assembly assembly)
        {
            byte[] data = null;
            if (!ImageHashTable.TryGetValue(name, out data))
            {
                string resName = assembly.GetManifestResourceNames().FirstOrDefault(mf => mf.EndsWith(name));
                if (resName != null)
                {
                    using (Stream sr = (assembly.GetManifestResourceStream(resName)))
                    {
                        using (MemoryStream ms = new MemoryStream())
                        {
                            sr.CopyTo(ms);
                            data = ms.ToArray();
                            sr.Close();
                            ImageHashTable.Add(name, data);
                        }
                    }
                }
            }
            return data;
        }
    }
}
