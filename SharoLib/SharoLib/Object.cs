using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static class _ext_object
{
    public static int ToInt32(this object value) {
        if (value is string) {
            string str = "";
            foreach (char c in value.ToString())
                if (c >= '0' && c <= '9' || c == '-') str += c;
            value = str;
        }
        return Convert.ToInt32(value);
    }
    public static int ToInt32(this object value, int defaultValue) {
        if (value == null) return defaultValue;

        if (value is string) {
            string str = "";
            foreach (char c in value.ToString())
                if (c >= '0' && c <= '9' || c == '-') str += c;
            value = str;
        }
        int i;
        if (!int.TryParse(value.ToString(), out i))
            return defaultValue;
        return Convert.ToInt32(value);
    }
    public static string ToString(this object value, string defaultValue) {
        if (value == null) return defaultValue;
        else
            return value.ToString();
    }
    public static float ToFloat(this object value) {
        return Convert.ToSingle(value);
    }
    public static float ToFloat(this object value, float defaultValue) {
        if (value == null)
            return defaultValue;
        float f;
        if (!float.TryParse(value.ToString(), out f))
            return defaultValue;

        return Convert.ToSingle(value);
    }
    public static int ToInt32ByUpperNumber(this string value)
    {
        if (string.IsNullOrEmpty(value))return 0;

        value = value.Replace('０', '0');
        value = value.Replace('１', '1');
        value = value.Replace('２', '2');
        value = value.Replace('３', '3');
        value = value.Replace('４', '4');
        value = value.Replace('５', '5');
        value = value.Replace('６', '6');
        value = value.Replace('７', '7');
        value = value.Replace('８', '8');
        value = value.Replace('９', '9');
        return value.ToInt32();
    }
}