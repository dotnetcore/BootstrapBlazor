// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using System.Reflection;

namespace BootstrapBlazor.Components;

static class AssemblyExtensions
{
    /// <summary>
    /// 获得唯一类型名称方法
    /// </summary>
    /// <param name="assembly"></param>
    /// <returns></returns>
    public static string GetUniqueName(this Assembly assembly) => assembly.IsCollectible
        ? $"{assembly.GetName().Name}-{assembly.GetHashCode()}"
        : $"{assembly.GetName().Name}";
}
