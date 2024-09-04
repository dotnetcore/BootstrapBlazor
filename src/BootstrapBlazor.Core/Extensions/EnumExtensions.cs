// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

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
}
