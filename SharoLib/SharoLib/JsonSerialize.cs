using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;

/// <summary>
/// Json形式でのシリアライズ関係のクラス。System.Runtime.Serialization必須。
/// </summary>
public static class _ext_JsonSerialize
{
    public static string SaveInJson<T>(this object obj)
    {
        DataContractJsonSerializer serialize = new DataContractJsonSerializer(typeof(T));
        using (var ms = new MemoryStream())
        {
            serialize.WriteObject(ms, obj);
            ms.Position = 0;
            using (StreamReader sr = new StreamReader(ms))
                return sr.ReadToEnd();
        }
    }
    public static T LoadInJson<T>(this string json)
    {
        DataContractJsonSerializer serialize = new DataContractJsonSerializer(typeof(T));

        using (var ms = new MemoryStream(Encoding.UTF8.GetBytes(json)))
        {
            return (T)serialize.ReadObject(ms);
        }
    }
}