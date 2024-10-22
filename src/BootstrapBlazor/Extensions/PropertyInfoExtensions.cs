﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

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
    /// 判断属性是否只读扩展方法
    /// </summary>
    /// <param name="p"></param>
    /// <returns></returns>
    public static bool IsCanWrite(this PropertyInfo p) => p.CanWrite && !p.IsInit();

    /// <summary>
    /// 判断是否为 Init 扩展方法
    /// </summary>
    /// <param name="p"></param>
    /// <returns></returns>
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
}
