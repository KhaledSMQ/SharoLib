using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinAPI.Manager
{
    public class IniFile
    {
        public IniFile(string filePath)
        {
            this.FilePath = filePath;

            if (!File.Exists(FilePath))
                File.Create(filePath).Close();
        }

        public string FilePath { get; set; }

        public string this[string section, string key]
        {
            get
            {
                return this[section, key, null];
            }
            set
            {
                NativeMethod.WritePrivateProfileString(section, key, value, this.FilePath);
            }
        }
        public string this[string section, string key, string def]
        {
            get
            {
                StringBuilder sb = new StringBuilder(1024);
                NativeMethod.GetPrivateProfileString(section, key, def, sb, (uint)sb.Capacity, this.FilePath);
                return sb.ToString();
            }
        }

        public string[] GetSections()
        {
            byte[] ar2 = new byte[1024];
            uint resultSize2 = NativeMethod.GetPrivateProfileStringByByteArray(null, null, "", ar2, (uint)ar2.Length, this.FilePath);
            string result2 = Encoding.Default.GetString(ar2, 0, (int)resultSize2);
            string[] sections = result2.Split(new[] { '\0' }, StringSplitOptions.RemoveEmptyEntries);

            return sections;
        }
        public string[] GetKeys(string section)
        {
            byte[] ar1 = new byte[1024];
            uint resultSize1 = NativeMethod.GetPrivateProfileStringByByteArray(section, null, "", ar1, (uint)ar1.Length, this.FilePath);
            string result1 = Encoding.Default.GetString(ar1, 0, (int)resultSize1);
            string[] keys = result1.Split(new[] { '\0' }, StringSplitOptions.RemoveEmptyEntries);

            return keys;
        }
        public void DeleteSection(string section)
        {
            this[section, null] = null;
        }
        public void DeleteKey(string section, string key)
        {
            this[section, key] = null;
        }
    }
}
