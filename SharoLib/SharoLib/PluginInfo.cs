using System;
using System.Collections.Generic;
using System.IO;

public class PluginInfo
{
    private PluginInfo(string filename)
    {
        this.FileName = filename;
    }

    public string FileName { get; private set; }

    public static PluginInfo CreatePluginInfo(string filename)
    {
        return new PluginInfo(filename);
    }
    public static PluginInfo[] FindPlugin<T>(string folder)
    {
        return FindPlugin(typeof(T), folder);
    }
    public static PluginInfo[] FindPlugin(Type hasType, string folder)
    {
        Type t = hasType;
        List<PluginInfo> res = new List<PluginInfo>();

        foreach (string file in Directory.GetFiles(folder, "*", SearchOption.AllDirectories))
        {
            try
            {
                //アセンブリとして読み込む
                System.Reflection.Assembly asm =
                    System.Reflection.Assembly.LoadFrom(file);
                foreach (Type type in asm.GetTypes())
                {
                    // プラグインの型が定義されていない場合はすべて返す
                    //アセンブリ内のすべての型について、
                    //プラグインとして有効か調べる
                    if ( t == null || 
                        (type.IsClass && t.IsPublic && !type.IsAbstract &&
                        (type.GetInterface(t.FullName) != null || type.IsSubclassOf(t))))
                    {
                        //PluginInfoをコレクションに追加する
                        res.Add(new PluginInfo(file));
                        break;
                    }
                }
            }
            catch { }
        }

        return res.ToArray();
    }

    public T[] GetMembers<T>()
    {
        Type t = typeof(T);
        List<T> res = new List<T>();

        //アセンブリを読み込む
        System.Reflection.Assembly asm = System.Reflection.Assembly.LoadFrom(this.FileName);

        foreach (Type type in asm.GetTypes())
        {
            try
            {
                if (type.IsClass && t.IsPublic && !type.IsAbstract &&
                    (type.GetInterface(t.FullName) != null || type.IsSubclassOf(t)))
                {

                    //クラス名からインスタンスを作成する
                    res.Add((T)asm.CreateInstance(type.FullName));
                }
            }
            catch
            {
            }
        }

        return res.ToArray();
    }
}