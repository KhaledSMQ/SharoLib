using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

public static class _ext_binary
{
    public static object LoadInBinary(this Type t, string path)
    {
        using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
        {
            BinaryFormatter f = new BinaryFormatter();

            object obj = f.Deserialize(fs);
            fs.Close();

            return obj;
        }
    }

    /// <summary>
    /// オブジェクトの内容をファイルに保存する
    /// </summary>
    /// <param name="obj">保存するオブジェクト</param>
    /// <param name="path">保存先のファイル名</param>
    public static void SaveInBinary(this object obj, string path)
    {
        using (FileStream fs = new FileStream(path, FileMode.Create, FileAccess.Write))
        {
            BinaryFormatter bf = new BinaryFormatter();

            bf.Serialize(fs, obj);
            fs.Close();
        }
    }
}