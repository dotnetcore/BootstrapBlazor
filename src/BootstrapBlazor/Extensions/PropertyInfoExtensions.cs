// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using System.Reflection;

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">PropertyInfo 扩展方法</para>
/// <para lang="en">PropertyInfo extension methods</para>
/// </summary>
public static class PropertyInfoExtensions
{
    /// <summary>
    /// <para lang="zh">判断属性是否为静态属性</para>
    /// <para lang="en">Determines whether the property is static</para>
    /// </summary>
    /// <param name="p"></param>
    public static bool IsStatic(this PropertyInfo p)
    {
        var mi = p.GetMethod ?? p.SetMethod;
        return mi!.IsStatic;
    }

    /// <summary>
    /// <para lang="zh">判断属性是否可以写入扩展方法</para>
    /// <para lang="en">Determines whether the property can be written to</para>
    /// </summary>
    /// <param name="p"></param>
    public static bool IsCanWrite(this PropertyInfo p) => p.CanWrite && !p.IsInit();

    private static bool IsInit(this PropertyInfo p)
    {
        var isInit = false;
        if (p.CanWrite)
        {
            var setMethod = p.SetMethod;
            if (setMethod != null)
            {
                var setMethodReturnParameterModifiers = setMethod.ReturnParameter.GetRequiredCustomModifiers();
                isInit = setMethodReturnParameterModifiers.Contains(typeof(System.Runtime.CompilerServices.IsExternalInit));
            }
        }
        return isInit;
    }

    /// <summary>
    /// <para lang="zh">判断属性是否有指定类型的 Parameter 特性</para>
    /// <para lang="en">Determines whether the property has a Parameter attribute of the specified type</para>
    /// </summary>
    /// <param name="modelProperty"></param>
    /// <param name="type"></param>
    public static bool HasParameterAttribute(this PropertyInfo? modelProperty, Type type)
    {
        if (modelProperty is null)
        {
            return false;
        }

        // 必须带有 Parameter 特性
        if (!modelProperty.IsDefined(typeof(ParameterAttribute), inherit: true))
        {
            return false;
        }

        // 处理可空类型，并使用可赋值性检查类型兼容性
        var propertyType = Nullable.GetUnderlyingType(modelProperty.PropertyType) ?? modelProperty.PropertyType;
        var targetType = Nullable.GetUnderlyingType(type) ?? type;

        return targetType.IsAssignableFrom(propertyType);
    }
}
