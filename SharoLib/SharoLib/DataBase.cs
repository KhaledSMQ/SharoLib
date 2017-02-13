using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

public static class _ext_IDbConnection
{

    /// <summary>
    /// 新しいデータベースコマンドを発行します。
    /// </summary>
    /// <param name="db">使用するデータベース</param>
    /// <param name="command">使用するSQL文</param>
    /// <returns>発行されたデータベースコマンドを返します。</returns>
    public static System.Data.IDbCommand Create(
        this System.Data.IDbConnection db,
        string command)
    {
        // コマンドを作成
        var com = db.CreateCommand();
        com.CommandText = command;
        return com;
    }
    /// <summary>
    /// パラメータを使用して新しいデータベースコマンドを発行します。
    /// </summary>
    /// <param name="db">使用するデータベース</param>
    /// <param name="command">使用するSQL文。String.Formatのようなフォーマットで指定します。</param>
    /// <param name="parameters">使用するパラメータ</param>
    /// <returns>発行されたデータベースコマンドを返します。</returns>
    public static System.Data.IDbCommand Create(
        this System.Data.IDbConnection db,
        string command,
        params object[] parameters)
    {
        // コマンドを作成
        var com = db.CreateCommand();
        com.CommandText = command;

        // パラメータを置き換える
        List<object> coms = new List<object>();
        for (int i = 0; i < parameters.Length; i++)
            coms.Add("@com" + i);
        com.CommandText = command = string.Format(command, coms.ToArray());

        // 対応するパラメータクラスを検索する
        Type paramType = null;
        Assembly asm = db.GetType().Assembly;
        foreach (Type t in asm.GetTypes())
        {
            //アセンブリ内のすべての型について、
            //プラグインとして有効か調べる
            if (t.IsClass && t.IsPublic && !t.IsAbstract &&
                t.GetInterface(typeof(IDbDataParameter).FullName) != null)
            { paramType = t; break; }
        }
        if (paramType == null)
            throw new Exception("This DBConnection is not support.");

        for (int i = 0; i < coms.Count; i++)
        {
            IDbDataParameter param = asm.CreateInstance(paramType.FullName) as IDbDataParameter;
            param.ParameterName = "@com" + i;
            param.Value = parameters[i];
            com.Parameters.Add(param);
        }

        return com;
    }

    public static void BeginCommand(
        this System.Data.IDbConnection db)
    {
        db.Create("BEGIN;").Execute();
    }
    public static void CommitCommand(
        this System.Data.IDbConnection db)
    {
        db.Create("COMMIT;").Execute();
    }
    public static void RollBackCommand(
        this System.Data.IDbConnection db)
    {
        db.Create("ROLLBACK;").Execute();
    }
}

public static class _ext_IDbCommand
{
    public static string LastCommand { get; private set; }

    public static void Execute(
        this System.Data.IDbCommand command)
    {
        LastCommand = command.CommandText;
        command.ExecuteNonQuery();
        command.Dispose();
    }
    public static void ExecuteReader(
        this System.Data.IDbCommand command,
        DbReadHandler handle) {
        LastCommand = command.CommandText;
        using (var reader = command.ExecuteReader()) {
            if (handle != null)
                handle(reader);
            command.Dispose();
        }
    }
    public static object ExecuteReaderItem(
        this System.Data.IDbCommand command,
        DbOneReadHandler handle) {
        LastCommand = command.CommandText;
        using (command)
        using (var reader = command.ExecuteReader()) {
            if (handle != null) {
                if (reader.Read())
                    return handle(reader);
                else
                    return null;
            } else return null;
        }
    }
    public static bool ExecuteExists(
        this System.Data.IDbCommand command) {
        LastCommand = command.CommandText;
        using (command)
        using (var reader = command.ExecuteReader())
        {
            return reader.Read();
        }
    }
}

public delegate void DbReadHandler(System.Data.IDataReader reader);
public delegate object DbOneReadHandler(System.Data.IDataReader reader);