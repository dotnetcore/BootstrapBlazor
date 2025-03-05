// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using System.Globalization;
using System.Reflection;

namespace BootstrapBlazor.Components;

/// <summary>
/// Object 扩展方法
/// </summary>
public static class ObjectExtensions
{
    /// <summary>
    /// 转化为带单位的字符串 [% px] => [% px] [int] => [int]px
    /// </summary>
    /// <param name="val"></param>
    /// <returns></returns>
    public static string ConvertToPercentString(this string? val)
    {
        var ret = "";
        if (!string.IsNullOrEmpty(val))
        {
            if (val.EndsWith('%'))
            {
                ret = val;
            }
            else if (val.EndsWith("px", StringComparison.OrdinalIgnoreCase))
            {
                ret = val;
            }
            else if (int.TryParse(val, out var d))
            {
                ret = $"{d}px";
            }
            else
            {
                ret = val;
            }
        }
        return ret;
    }

    /// <summary>
    /// 检查是否为 Number 数据类型
    /// </summary>
    /// <param name="t"></param>
    /// <returns></returns>
    public static bool IsNumber(this Type t)
    {
        var targetType = Nullable.GetUnderlyingType(t) ?? t;
        return targetType == typeof(int) || targetType == typeof(long) || targetType == typeof(short) ||
            targetType == typeof(float) || targetType == typeof(double) || targetType == typeof(decimal);
    }

    /// <summary>
    /// 检查是否应该渲染成 <see cref="BootstrapInputNumber{TValue}"/>
    /// </summary>
    /// <param name="t"></param>
    /// <returns></returns>
    public static bool IsNumberWithDotSeparator(this Type t)
    {
        var separator = CultureInfo.CurrentUICulture.NumberFormat.NumberDecimalSeparator;
        if (separator != ".")
        {
            return false;
        }
        return t.IsNumber();
    }

    /// <summary>
    /// 检查是否为 Boolean 数据类型
    /// </summary>
    /// <param name="t"></param>
    /// <returns></returns>
    public static bool IsBoolean(this Type t)
    {
        var targetType = Nullable.GetUnderlyingType(t) ?? t;
        return targetType == typeof(Boolean);
    }

    /// <summary>
    /// 检查是否为 DateTime 数据类型
    /// </summary>
    /// <param name="t"></param>
    /// <returns></returns>
    public static bool IsDateTime(this Type t)
    {
        var targetType = Nullable.GetUnderlyingType(t) ?? t;
        var check = targetType == typeof(DateTime) || targetType == typeof(DateTimeOffset);
        return check;
    }

    /// <summary>
    /// 检查是否为 TimeSpan 数据类型
    /// </summary>
    /// <param name="t"></param>
    /// <returns></returns>
    public static bool IsTimeSpan(this Type t)
    {
        var targetType = Nullable.GetUnderlyingType(t) ?? t;
        var check = targetType == typeof(TimeSpan);
        return check;
    }

    /// <summary>
    /// 通过类型获取类型描述文字
    /// </summary>
    /// <param name="t"></param>
    /// <returns></returns>
    public static string GetTypeDesc(this Type t)
    {
        string? ret;
        if (t.IsEnum)
        {
            ret = "枚举";
        }
        else if (t.IsNumber())
        {
            ret = "数字";
        }
        else if (t.IsDateTime())
        {
            ret = "日期";
        }
        else
        {
            ret = "字符串";
        }

        return ret;
    }

    /// <summary>
    /// 字符串类型转换为其他数据类型
    /// </summary>
    /// <returns></returns>
    public static bool TryConvertTo(this string? source, Type type, out object? val)
    {
        var ret = true;
        val = source;
        if (type != typeof(string))
        {
            ret = false;
            var methodInfo = Array.Find(typeof(ObjectExtensions).GetMethods(), m => m is { Name: nameof(TryConvertTo), IsGenericMethod: true });
            if (methodInfo != null)
            {
                methodInfo = methodInfo.MakeGenericMethod(type);
                var v = Activator.CreateInstance(type);
                var args = new object?[] { source, v };
                var b = (bool)methodInfo.Invoke(null, args)!;
                val = b ? args[1] : null;
                ret = b;
            }
        }
        return ret;
    }

    /// <summary>
    /// Tries to convert the string representation of a value to a specified type.
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="source"></param>
    /// <param name="val"></param>
    /// <returns></returns>
    public static bool TryConvertTo<TValue>(this string? source, [MaybeNullWhen(false)] out TValue val)
    {
        var ret = false;
        var type = Nullable.GetUnderlyingType(typeof(TValue)) ?? typeof(TValue);
        if (type == typeof(string))
        {
            val = (TValue)(object)source!;
            ret = true;
        }
        else
        {
            try
            {
                if (source == null)
                {
                    val = default;
                    ret = true;
                }
                else if (source == string.Empty)
                {
                    ret = BindConverter.TryConvertTo(source, CultureInfo.CurrentCulture, out val);
                }
                else
                {
                    var isBoolean = type == typeof(bool);
                    var v = isBoolean ? (object)source.Equals("true", StringComparison.CurrentCultureIgnoreCase) : source;
                    ret = BindConverter.TryConvertTo(v, CultureInfo.CurrentCulture, out val);
                }
            }
            catch
            {
                val = default;
            }
        }
        return ret;
    }

    /// <summary>
    /// Formats the file size into a string with appropriate units
    /// </summary>
    /// <param name="fileSize"></param>
    /// <returns></returns>
    public static string ToFileSizeString(this long fileSize) => fileSize switch
    {
        >= 1024 and < 1024 * 1024 => $"{Math.Round(fileSize / 1024D, 0, MidpointRounding.AwayFromZero)} KB",
        >= 1024 * 1024 and < 1024 * 1024 * 1024 => $"{Math.Round(fileSize / 1024 / 1024D, 0, MidpointRounding.AwayFromZero)} MB",
        >= 1024 * 1024 * 1024 => $"{Math.Round(fileSize / 1024 / 1024 / 1024D, 0, MidpointRounding.AwayFromZero)} GB",
        _ => $"{fileSize} B"
    };

    internal static void Clone<TModel>(this TModel source, TModel item)
    {
        if (item != null)
        {
            var type = typeof(TModel);

            // 20200608 tian_teng@outlook.com 支持字段和只读属性
            foreach (var f in type.GetFields())
            {
                var v = f.GetValue(item);
                f.SetValue(source, v);
            }
            foreach (var p in type.GetRuntimeProperties())
            {
                if (p.IsCanWrite())
                {
                    var v = p.GetValue(item);
                    p.SetValue(source, v);
                }
            }
        }
    }

    /// <summary>
    /// Creates an instance of a type and ensures all class-type properties are initialized.
    /// </summary>
    /// <typeparam name="TItem">The type to create an instance of.</typeparam>
    /// <param name="isAutoInitializeModelProperty">Whether to automatically initialize model properties default value is false.</param>
    /// <returns>An instance of the specified type with initialized properties.</returns>
    public static TItem? CreateInstance<TItem>(bool isAutoInitializeModelProperty = false)
    {
        var instance = Activator.CreateInstance<TItem>();
        if (isAutoInitializeModelProperty)
        {
            instance.EnsureInitialized(isAutoInitializeModelProperty);
        }
        return instance;
    }

    private static object? CreateInstance(Type type, bool isAutoInitializeModelProperty = false)
    {
        var instance = Activator.CreateInstance(type);
        if (isAutoInitializeModelProperty)
        {
            instance.EnsureInitialized();
        }
        return instance;
    }

    /// <summary>
    /// Ensures that all class-type properties of the instance are initialized.
    /// </summary>
    /// <param name="isAutoInitializeModelProperty">Whether to automatically initialize model properties default value is false.</param>
    /// <param name="instance">The instance to initialize properties for.</param>
    private static void EnsureInitialized(this object? instance, bool isAutoInitializeModelProperty = false)
    {
        if (instance is null)
        {
            return;
        }

        // Reflection performance needs to be optimized here
        foreach (var propertyInfo in instance.GetType().GetProperties().Where(p => p.PropertyType.IsClass && p.PropertyType != typeof(string)))
        {
            var type = propertyInfo.PropertyType;
            var value = propertyInfo.GetValue(instance, null);
            if (value is null)
            {
                var pv = CreateInstance(type, isAutoInitializeModelProperty);
                if (pv is not null)
                {
                    propertyInfo.SetValue(instance, pv);
                }
            }
        }
    }
}
