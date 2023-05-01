// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using System.Reflection;

namespace BootstrapBlazor.Components;

internal static class PropertyInfoExtensions
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
}
