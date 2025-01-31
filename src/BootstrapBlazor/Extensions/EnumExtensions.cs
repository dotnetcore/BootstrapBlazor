// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using System.ComponentModel;
using System.Reflection;

namespace BootstrapBlazor.Components;

/// <summary>
/// 枚举类型扩展方法
/// </summary>
public static class EnumExtensions
{
    /// <summary>
    /// 获取 DescriptionAttribute 标签方法
    /// </summary>
    /// <param name="val"></param>
    /// <returns></returns>
    public static string ToDescriptionString<TEnum>(this TEnum val) where TEnum : Enum => typeof(TEnum).ToDescriptionString(val.ToString());

    /// <summary>
    /// 通过字段名称获取 DescriptionAttribute 标签值
    /// </summary>
    /// <param name="type"></param>
    /// <param name="fieldName"></param>
    /// <returns></returns>
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
    /// 通过字段名称获取 DisplayAttribute/DescriptionAttribute 标签值
    /// </summary>
    /// <typeparam name="TEnum"></typeparam>
    /// <param name="enum"></param>
    /// <returns></returns>
    public static string ToDisplayName<TEnum>(this TEnum @enum) where TEnum : Enum => Utility.GetDisplayName<TEnum>(@enum.ToString());

    /// <summary>
    /// 获取指定枚举类型的枚举值集合，默认通过 DisplayAttribute DescriptionAttribute 标签显示 DisplayName 支持资源文件 回退机制显示字段名称
    /// </summary>
    /// <param name="type"></param>
    /// <param name="additionalItem"></param>
    /// <returns></returns>
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
    /// 获取指定枚举类型的枚举值集合，默认通过 DisplayAttribute DescriptionAttribute 标签显示 DisplayName 支持资源文件 回退机制显示字段名称
    /// </summary>
    /// <param name="type"></param>
    /// <param name="additionalItem"></param>
    /// <returns></returns>
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
    /// 判断类型是否为枚举类型
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
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
    /// 判断类型是否为 Flag 枚举类型
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public static bool IsFlagEnum(this Type? type) => type != null && IsEnum(type) && type.GetCustomAttribute<FlagsAttribute>() != null;

    /// <summary>
    /// 将 <see cref="IEnumerable{T}"/> 集合转换为 Flag 枚举值
    /// </summary>
    /// <param name="items"></param>
    /// <param name="type"></param>
    /// <returns></returns>
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
