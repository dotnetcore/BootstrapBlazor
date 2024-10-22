// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.Extensions.Localization;

namespace BootstrapBlazor.Components;

/// <summary>
/// ILocalizationResolve 服务
/// </summary>
public interface ILocalizationResolve
{
    /// <summary>
    /// 获得所有文化信息集合
    /// </summary>
    /// <param name="includeParentCultures"></param>
    /// <returns></returns>
    IEnumerable<LocalizedString> GetAllStringsByCulture(bool includeParentCultures);
}
