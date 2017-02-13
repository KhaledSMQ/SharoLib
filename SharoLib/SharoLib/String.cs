using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static class _ext_string
{
    public static string MD5(this string value)
    {
        //MD5ハッシュ値を計算する文字列
        string s = value;
        //文字列をbyte型配列に変換する
        byte[] data = System.Text.Encoding.UTF8.GetBytes(s);

        //MD5CryptoServiceProviderオブジェクトを作成
        System.Security.Cryptography.MD5CryptoServiceProvider md5 =
            new System.Security.Cryptography.MD5CryptoServiceProvider();
        //または、次のようにもできる
        //System.Security.Cryptography.MD5 md5 =
        //    System.Security.Cryptography.MD5.Create();

        //ハッシュ値を計算する
        byte[] bs = md5.ComputeHash(data);

        //リソースを解放する
        md5.Clear();

        //byte型配列を16進数の文字列に変換
        System.Text.StringBuilder result = new System.Text.StringBuilder();
        foreach (byte b in bs)
        {
            result.Append(b.ToString("x2"));
        }
        //ここの部分は次のようにもできる
        //string result = BitConverter.ToString(bs).ToLower().Replace("-","");

        //結果を表示
        return result.ToString();
    }
    public static string FormatToLower(this string value)
    {
        StringBuilder sb = new StringBuilder(value);
        sb = sb.Replace("ａ", "a");
        sb = sb.Replace("ｂ", "b");
        sb = sb.Replace("ｃ", "c");
        sb = sb.Replace("ｄ", "d");
        sb = sb.Replace("ｅ", "e");
        sb = sb.Replace("ｆ", "f");
        sb = sb.Replace("ｇ", "g");
        sb = sb.Replace("ｈ", "h");
        sb = sb.Replace("ｉ", "i");
        sb = sb.Replace("ｊ", "j");
        sb = sb.Replace("ｋ", "k");
        sb = sb.Replace("ｌ", "l");
        sb = sb.Replace("ｍ", "m");
        sb = sb.Replace("ｎ", "n");
        sb = sb.Replace("ｏ", "o");
        sb = sb.Replace("ｐ", "p");
        sb = sb.Replace("ｑ", "q");
        sb = sb.Replace("ｒ", "r");
        sb = sb.Replace("ｓ", "s");
        sb = sb.Replace("ｔ", "t");
        sb = sb.Replace("ｕ", "u");
        sb = sb.Replace("ｖ", "v");
        sb = sb.Replace("ｗ", "w");
        sb = sb.Replace("ｘ", "x");
        sb = sb.Replace("ｙ", "y");
        sb = sb.Replace("ｚ", "z");

        sb = sb.Replace("Ａ", "A");
        sb = sb.Replace("Ｂ", "B");
        sb = sb.Replace("Ｃ", "C");
        sb = sb.Replace("Ｄ", "D");
        sb = sb.Replace("Ｅ", "E");
        sb = sb.Replace("Ｆ", "F");
        sb = sb.Replace("Ｇ", "G");
        sb = sb.Replace("Ｈ", "H");
        sb = sb.Replace("Ｉ", "I");
        sb = sb.Replace("Ｊ", "J");
        sb = sb.Replace("Ｋ", "K");
        sb = sb.Replace("Ｌ", "L");
        sb = sb.Replace("Ｍ", "M");
        sb = sb.Replace("Ｎ", "N");
        sb = sb.Replace("Ｏ", "O");
        sb = sb.Replace("Ｐ", "P");
        sb = sb.Replace("Ｑ", "Q");
        sb = sb.Replace("Ｒ", "R");
        sb = sb.Replace("Ｓ", "S");
        sb = sb.Replace("Ｔ", "T");
        sb = sb.Replace("Ｕ", "U");
        sb = sb.Replace("Ｖ", "X");
        sb = sb.Replace("Ｗ", "W");
        sb = sb.Replace("Ｘ", "X");
        sb = sb.Replace("Ｙ", "Y");
        sb = sb.Replace("Ｚ", "Z");

        sb = sb.Replace("０", "0");
        sb = sb.Replace("１", "1");
        sb = sb.Replace("２", "2");
        sb = sb.Replace("３", "3");
        sb = sb.Replace("４", "4");
        sb = sb.Replace("５", "5");
        sb = sb.Replace("６", "6");
        sb = sb.Replace("７", "7");
        sb = sb.Replace("８", "8");
        sb = sb.Replace("９", "9");

        return sb.ToString();
    }
    public static string Encrypt(this string value, string password)
    {
        //RijndaelManagedオブジェクトを作成
        System.Security.Cryptography.RijndaelManaged rijndael =
            new System.Security.Cryptography.RijndaelManaged();

        //パスワードから共有キーと初期化ベクタを作成
        byte[] key, iv;
        GenerateKeyFromPassword(
            password, rijndael.KeySize, out key, rijndael.BlockSize, out iv);
        rijndael.Key = key;
        rijndael.IV = iv;

        //文字列をバイト型配列に変換する
        byte[] strBytes = System.Text.Encoding.UTF8.GetBytes(value);

        //対称暗号化オブジェクトの作成
        System.Security.Cryptography.ICryptoTransform encryptor =
            rijndael.CreateEncryptor();
        //バイト型配列を暗号化する
        byte[] encBytes = encryptor.TransformFinalBlock(strBytes, 0, strBytes.Length);
        //閉じる
        encryptor.Dispose();

        //バイト型配列を文字列に変換して返す
        return System.Convert.ToBase64String(encBytes);
    }
    public static string Descrypt(this string value, string password)
    {
        //RijndaelManagedオブジェクトを作成
        System.Security.Cryptography.RijndaelManaged rijndael =
            new System.Security.Cryptography.RijndaelManaged();

        //パスワードから共有キーと初期化ベクタを作成
        byte[] key, iv;
        GenerateKeyFromPassword(
            password, rijndael.KeySize, out key, rijndael.BlockSize, out iv);
        rijndael.Key = key;
        rijndael.IV = iv;

        //文字列をバイト型配列に戻す
        byte[] strBytes = System.Convert.FromBase64String(value);

        //対称暗号化オブジェクトの作成
        System.Security.Cryptography.ICryptoTransform decryptor =
            rijndael.CreateDecryptor();
        //バイト型配列を復号化する
        //復号化に失敗すると例外CryptographicExceptionが発生
        byte[] decBytes = decryptor.TransformFinalBlock(strBytes, 0, strBytes.Length);
        //閉じる
        decryptor.Dispose();

        //バイト型配列を文字列に戻して返す
        return System.Text.Encoding.UTF8.GetString(decBytes);

    }
    public static string ResParse(this string format, params string[] parameter)
    {
        return string.Format(format, parameter);
    }

    private static void GenerateKeyFromPassword(string password,
        int keySize, out byte[] key, int blockSize, out byte[] iv)
    {
        //パスワードから共有キーと初期化ベクタを作成する
        //saltを決める
        byte[] salt = System.Text.Encoding.UTF8.GetBytes("saltは必ず8バイト以上");
        //Rfc2898DeriveBytesオブジェクトを作成する
        System.Security.Cryptography.Rfc2898DeriveBytes deriveBytes =
            new System.Security.Cryptography.Rfc2898DeriveBytes(password, salt);
        //.NET Framework 1.1以下の時は、PasswordDeriveBytesを使用する
        //System.Security.Cryptography.PasswordDeriveBytes deriveBytes =
        //    new System.Security.Cryptography.PasswordDeriveBytes(password, salt);
        //反復処理回数を指定する デフォルトで1000回
        deriveBytes.IterationCount = 1000;

        //共有キーと初期化ベクタを生成する
        key = deriveBytes.GetBytes(keySize / 8);
        iv = deriveBytes.GetBytes(blockSize / 8);
    }
    public static DateTime ParseFrom6Di(this string str) {
        if (str == "0" || string.IsNullOrEmpty(str))
            return DateTime.Now;
        return new DateTime(
            str.Substring(0, 4).ToInt32(),
            str.Substring(4, 2).ToInt32(),
            str.Substring(6, 2).ToInt32());
    }
    public static DateTime ParseFrom4Di(this string str) {
        if (str == "0" || string.IsNullOrEmpty(str))
            return DateTime.Now;
        return new DateTime(
            DateTime.Now.Year,
            str.Substring(0, 2).ToInt32(),
            str.Substring(2, 2).ToInt32());
    }

}