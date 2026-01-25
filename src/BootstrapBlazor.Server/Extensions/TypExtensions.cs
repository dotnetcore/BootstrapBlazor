// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using System.Reflection;

namespace BootstrapBlazor.Server.Extensions;

/// <summary>
/// Type 类型扩展方法
/// </summary>
public static class TypExtensions
{
    /// <summary>
    /// 判断是否为布局组件
    /// </summary>
    /// <param name="t"></param>
    public static bool IsComponentLayout(this Type t)
    {
        return t.GetCustomAttribute<LayoutAttribute>()?.LayoutType == typeof(ComponentLayout)
            || t.GetCustomAttribute<LayoutAttribute>()?.LayoutType == typeof(DockLayout);
    }

}
