// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.Extensions.Hosting;

namespace BootstrapBlazor.Components;

/// <summary>
/// <see cref="IHostEnvironment"/> 扩展方法"
/// </summary>
public static class HostEnvironmentExtensions
{
    /// <summary>
    /// 当前程序是否为 WebAssembly 环境
    /// </summary>
    /// <param name="hostEnvironment"></param>
    /// <returns></returns>
    public static bool IsWasm(this IHostEnvironment hostEnvironment) => hostEnvironment is MockWasmHostEnvironment;
}
