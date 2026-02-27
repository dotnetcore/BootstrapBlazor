// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using System.ComponentModel;
using System.Reflection;

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">Enum 枚举扩展方法</para>
/// <para lang="en">Enum extensions</para>
/// </summary>
public static class EnumExtensions
{
    /// <summary>
    /// <para lang="zh">获得 枚举值的 <see cref="DescriptionAttribute"/> 标签值</para>
    /// <para lang="en">Gets the <see cref="DescriptionAttribute"/> value of the enum value</para>
    /// </summary>
    /// <param name="val"></param>
    public static string ToDescriptionString<TEnum>(this TEnum val) where TEnum : Enum => typeof(TEnum).ToDescriptionString(val.ToString());

    /// <summary>
    /// <para lang="zh">获得 指定字段的 <see cref="DescriptionAttribute"/> 标签值</para>
    /// <para lang="en">Gets the <see cref="DescriptionAttribute"/> value by field name</para>
    /// </summary>
    /// <param name="type"></param>
    /// <param name="fieldName"></param>
    public static string ToDescriptionString(this Type? type, string? fieldName)
    {
        var ret = string.Empty;
        if (type != null && !string.IsNullOrEmpty(fieldName))
        {
            var t = Nullable.GetUnderlyingType(type) ?? type;
            var attributes = t.GetField(fieldName)?.GetCustomAttribute<DescriptionAttribute>(true);
            ret = attributes?.Description ?? fieldName;
        }
        return ret;
    }

    /// <summary>
    /// <para lang="zh">获得 枚举值的显示名称</para>
    /// <para lang="en">Gets the display name of the enum value</para>
    /// </summary>
    /// <typeparam name="TEnum"></typeparam>
    /// <param name="enum"></param>
    public static string ToDisplayName<TEnum>(this TEnum @enum) where TEnum : Enum => Utility.GetDisplayName<TEnum>(@enum.ToString());

    /// <summary>
    /// <para lang="zh">获得 指定枚举类型的枚举值集合</para>
    /// <para lang="en">Gets the enum value collection for the specified enum type</para>
    /// </summary>
    /// <param name="type"></param>
    /// <param name="additionalItem"></param>
    public static List<SelectedItem> ToSelectList(this Type type, SelectedItem? additionalItem = null)
    {
        var ret = new List<SelectedItem>();
        if (additionalItem != null)
        {
            ret.Add(additionalItem);
        }

        if (type.IsEnum())
        {
            var t = Nullable.GetUnderlyingType(type) ?? type;
            ret.AddRange(from field in Enum.GetNames(t) let desc = Utility.GetDisplayName(t, field) select new SelectedItem(field, desc));
        }
        return ret;
    }

    /// <summary>
    /// <para lang="zh">获得 指定枚举类型的枚举值集合</para>
    /// <para lang="en">Gets the enum value collection for the specified enum type</para>
    /// </summary>
    /// <param name="type"></param>
    /// <param name="additionalItem"></param>
    public static List<SelectedItem<TValue>> ToSelectList<TValue>(this Type type, SelectedItem<TValue>? additionalItem = null)
    {
        var ret = new List<SelectedItem<TValue>>();
        if (additionalItem != null)
        {
            ret.Add(additionalItem);
        }

        if (type.IsEnum())
        {
            var t = Nullable.GetUnderlyingType(type) ?? type;
            foreach (var field in Enum.GetNames(t))
            {
                var desc = Utility.GetDisplayName(t, field);
                var val = (TValue)Enum.Parse(t, field);
                ret.Add(new SelectedItem<TValue>(val, desc));
            }
        }
        return ret;
    }

    /// <summary>
    /// <para lang="zh">获得 类型是否为枚举类型</para>
    /// <para lang="en">Gets whether the type is an enumeration type</para>
    /// </summary>
    /// <param name="type"></param>
    public static bool IsEnum(this Type? type)
    {
        var ret = false;
        if (type != null)
        {
            var t = Nullable.GetUnderlyingType(type) ?? type;
            ret = t.IsEnum;
        }
        return ret;
    }

    /// <summary>
    /// <para lang="zh">获得 类型是否为 <see cref="FlagsAttribute"/> 枚举类型</para>
    /// <para lang="en">Gets whether the type is a <see cref="FlagsAttribute"/> enumeration type</para>
    /// </summary>
    /// <param name="type"></param>
    public static bool IsFlagEnum(this Type? type) => type != null && IsEnum(type) && type.GetCustomAttribute<FlagsAttribute>() != null;

    /// <summary>
    /// <para lang="zh">将 <see cref="IEnumerable{T}"/> 集合转换为 Flag 枚举值</para>
    /// <para lang="en">Convert <see cref="IEnumerable{T}"/> collection to Flag enumeration value</para>
    /// </summary>
    /// <param name="items"></param>
    /// <param name="type"></param>
    internal static object? ParseFlagEnum<TValue>(this IEnumerable<SelectedItem> items, Type type)
    {
        TValue? v = default;
        if (type.IsFlagEnum())
        {
            foreach (var item in items)
            {
                if (Enum.TryParse(type, item.Value, true, out var val))
                {
                    v = (TValue)Enum.ToObject(type, Convert.ToInt32(v) | Convert.ToInt32(val));
                }
            }
        }
        return v;
    }
}
