using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/// <summary>
/// System.Linqにあったら便利だと思うメソッド郡
/// </summary>
public static class _ext_IEnumerable
{
    public static void Each<T>(this IEnumerable<T> items, Action<T> act)
    {
        foreach (T item in items) act(item);
    }
}