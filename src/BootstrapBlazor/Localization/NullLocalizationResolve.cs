// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.Extensions.Localization;

namespace BootstrapBlazor.Components;

internal class NullLocalizationResolve : ILocalizationResolve
{
    /// <summary>
    /// 获得所有文化信息集合
    /// </summary>
    /// <param name="includeParentCultures"></param>
    /// <returns></returns>
    public IEnumerable<LocalizedString> GetAllStringsByCulture(bool includeParentCultures) => [];

    /// <summary>
    /// 获得指定类型文化信息集合
    /// </summary>
    /// <param name="includeParentCultures"></param>
    /// <param name="typeName">类型名称</param>
    /// <returns></returns>
    public IEnumerable<LocalizedString> GetAllStringsByType(bool includeParentCultures, string typeName) => [];
}
