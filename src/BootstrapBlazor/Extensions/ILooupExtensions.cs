// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <see cref="ILookup"/> 扩展方法
/// </summary>
public static class ILooupExtensions
{
    /// <summary>
    /// 获得 ILookupService 实例
    /// </summary>
    /// <param name="item"></param>
    /// <param name="service"></param>
    /// <returns></returns>
    public static ILookupService GetLookupService(this ILookup item, ILookupService service) => item.LookupService ?? service;
}
