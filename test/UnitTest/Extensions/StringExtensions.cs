// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace UnitTest.Extensions;

/// <summary>
/// 
/// </summary>
public static class StringExtensions
{
    /// <summary>
    /// SpanSplit 扩展方法
    /// </summary>
    /// <param name="source">源数组</param>
    /// <param name="splitStr">分隔符数组 分割规则作为整体</param>
    /// <param name="stringSplitOptions">StringSplitOptions 选项</param>
    /// <returns>分割后的字符串数组</returns>
    public static List<string> SpanSplit(this string? source, string? splitStr = null, StringSplitOptions stringSplitOptions = StringSplitOptions.None)
    {
        var ret = new List<string>();
        if (string.IsNullOrEmpty(source))
        {
            return ret;
        }

        if (string.IsNullOrEmpty(splitStr))
        {
            splitStr = Environment.NewLine;
        }

        var sourceSpan = source.AsSpan();
        var splitSpan = splitStr.AsSpan();

        do
        {
            var n = sourceSpan.IndexOf(splitSpan);
            if (n == -1)
            {
                n = sourceSpan.Length;
            }

            ret.Add(stringSplitOptions == StringSplitOptions.None
                ? sourceSpan[..n].ToString()
                : sourceSpan[..n].Trim().ToString());
            sourceSpan = sourceSpan[Math.Min(sourceSpan.Length, n + splitSpan.Length)..];
        }
        while (sourceSpan.Length > 0);
        return ret;
    }

    /// <summary>
    /// SpanSplit 扩展方法
    /// </summary>
    /// <param name="source">源数组</param>
    /// <param name="splitStr">分隔符数组 分割规则是任意一个</param>
    /// <param name="stringSplitOptions">StringSplitOptions 选项</param>
    /// <returns>分割后的字符串数组</returns>
    public static List<string> SpanSplitAny(this string? source, string splitStr, StringSplitOptions stringSplitOptions = StringSplitOptions.None)
    {
        var ret = new List<string>();
        if (string.IsNullOrEmpty(source))
        {
            return ret;
        }

        if (string.IsNullOrEmpty(splitStr))
        {
            ret.Add(source);
            return ret;
        }

        var sourceSpan = source.AsSpan();
        var splitSpan = splitStr.AsSpan();

        do
        {
            var n = sourceSpan.IndexOfAny(splitSpan);
            if (n == -1)
            {
                n = sourceSpan.Length;
            }

            if (n > 0)
            {
                ret.Add(stringSplitOptions == StringSplitOptions.None
                 ? sourceSpan[..n].ToString()
                 : sourceSpan[..n].Trim().ToString());
            }

            sourceSpan = sourceSpan[Math.Min(sourceSpan.Length, n + 1)..];
        }
        while (sourceSpan.Length > 0);
        return ret;
    }
}
