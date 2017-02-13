using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;


public static class XmlSerialize
{
    public static void SaveInXml<T>(this object value, string filename)
    {
        if (File.Exists(filename))
            File.Delete(filename);

        System.Xml.Serialization.XmlSerializer serializer =
            new System.Xml.Serialization.XmlSerializer(typeof(T));

        using (FileStream fs = new FileStream(
            filename, FileMode.Create, FileAccess.Write, FileShare.None))
        {
            serializer.Serialize(fs, value);
            fs.Close();
        }
    }

    public static object LoadInXml(this Type type, string filename)
    {
        System.Xml.Serialization.XmlSerializer serializer =
            new System.Xml.Serialization.XmlSerializer(type);

        using (FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.Read))
        {
            return serializer.Deserialize(fs);
        }
    }
}

