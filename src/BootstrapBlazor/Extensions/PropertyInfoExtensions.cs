﻿// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using System.Reflection;

namespace BootstrapBlazor.Components;

/// <summary>
/// PropertyInfo 扩展方法
/// </summary>
public static class PropertyInfoExtensions
{
    /// <summary>
    /// 判断属性是否为静态属性
    /// </summary>
    /// <param name="p"></param>
    /// <returns></returns>
    public static bool IsStatic(this PropertyInfo p)
    {
        var mi = p.GetMethod ?? p.SetMethod;
        return mi!.IsStatic;
    }

    /// <summary>
    /// 判断是否为 Init 扩展方法
    /// </summary>
    /// <param name="property"></param>
    /// <returns></returns>
    public static bool IsInitOnly(this PropertyInfo property)
    {
        var isInit = false;
        if (property.CanWrite)
        {
            var setMethod = property.SetMethod;
            if (setMethod != null)
            {
                var setMethodReturnParameterModifiers = setMethod.ReturnParameter.GetRequiredCustomModifiers();
                isInit = setMethodReturnParameterModifiers.Contains(typeof(System.Runtime.CompilerServices.IsExternalInit));
            }
        }
        return isInit;
    }
}
