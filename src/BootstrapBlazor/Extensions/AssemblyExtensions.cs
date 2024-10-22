﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

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
