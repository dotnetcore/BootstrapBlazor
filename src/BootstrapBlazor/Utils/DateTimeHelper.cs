// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using System.Globalization;

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">日期时间相关帮助类</para>
/// <para lang="en">DateTime related helper class</para>
/// </summary>
public static class DateTimeHelper
{
    /// <summary>
    /// 无分隔符等标准解析无法识别的紧凑格式，需要显式列出
    /// </summary>
    private static readonly string[] CompactFormats =
    [
        "yyyyMMdd",
        "yyyyMMddHHmm",
        "yyyyMMddHHmmss",
        "yyyyMMddHHmmssfff",
        "yyyyMMdd HHmmss",
        "yyyyMMdd HH:mm:ss",
        "yyyyMMdd HH:mm"
    ];

    /// <summary>
    /// <para lang="zh">将字符串解析为 <see cref="DateTime"/>，无法解析时返回 <see langword="null"/></para>
    /// <para lang="en">Parse the string to <see cref="DateTime"/>, return <see langword="null"/> if parsing fails</para>
    /// </summary>
    /// <param name="value">
    ///  <para lang="zh">要解析的字符串</para>
    ///  <para lang="en">The string to parse</para>
    /// </param>
    public static DateTime? ToDateTime(string value)
        => TryToDateTime(value, out var result) ? result : null;

    /// <summary>
    /// <para lang="zh">将字符串解析为 <see cref="DateTime"/>，无法解析时返回 <paramref name="defaultValue"/></para>
    /// <para lang="en">Parse the string to <see cref="DateTime"/>, return <paramref name="defaultValue"/> if parsing fails</para>
    /// </summary>
    /// <param name="value">
    ///  <para lang="zh">要解析的字符串</para>
    ///  <para lang="en">The string to parse</para>
    /// </param>
    /// <param name="defaultValue">
    ///   <para lang="zh">解析失败时返回的默认值</para>
    ///   <para lang="en">The default value to return if parsing fails</para>
    /// </param>
    public static DateTime ToDateTime(string value, DateTime defaultValue)
            => TryToDateTime(value, out var result) ? result : defaultValue;

    /// <summary>
    /// <para lang="zh">尝试将字符串解析为 <see cref="DateTime"/></para>
    /// <para lang="en">Try to parse the string to <see cref="DateTime"/></para>
    /// </summary>
    /// <param name="value">
    ///  <para lang="zh">要解析的字符串</para>
    ///  <para lang="en">The string to parse</para>
    /// </param>
    /// <param name="result">
    ///   <para lang="zh">解析成功时的结果，失败时为 <see cref="DateTime.MinValue"/></para>
    ///   <para lang="en">The result when parsing succeeds, otherwise <see cref="DateTime.MinValue"/></para>
    /// </param>
    public static bool TryToDateTime(string value, out DateTime result)
    {
        result = DateTime.MinValue;
        if (string.IsNullOrWhiteSpace(value))
        {
            return false;
        }

        var text = value.Trim();

        // 先尝试无分隔符等紧凑格式（如 20260501），这类格式标准解析无法识别
        if (DateTime.TryParseExact(text, CompactFormats, CultureInfo.InvariantCulture, DateTimeStyles.None, out result))
        {
            return true;
        }

        // 再回退到通用解析，覆盖 2026-5-1、2026/5/1、2026-05-01 13:04:05 等带分隔符写法
        return DateTime.TryParse(text, CultureInfo.InvariantCulture, DateTimeStyles.AllowWhiteSpaces, out result);
    }

    /// <summary>
    /// <para lang="zh">将字符串解析为 <see cref="DateTimeOffset"/>，无法解析时返回 <see langword="null"/></para>
    /// <para lang="en">Parse the string to <see cref="DateTimeOffset"/>, return <see langword="null"/> if parsing fails</para>
    /// </summary>
    /// <param name="value">
    ///  <para lang="zh">要解析的字符串</para>
    ///  <para lang="en">The string to parse</para>
    /// </param>
    public static DateTimeOffset? ToDateTimeOffset(string value)
    {
        if (TryToDateTime(value, out var dateTime))
        {
            // 紧凑/无时区字符串解析出的 Kind 为 Unspecified，按本地时区补齐偏移
            return dateTime.Kind == DateTimeKind.Unspecified
                ? new DateTimeOffset(DateTime.SpecifyKind(dateTime, DateTimeKind.Local))
                : new DateTimeOffset(dateTime);
        }
        return null;
    }
}
