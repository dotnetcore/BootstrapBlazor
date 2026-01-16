// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.Extensions.Localization;

namespace BootstrapBlazor.Components;

internal class NullLocalizationResolve : ILocalizationResolve
{
    /// <summary>
    /// <para lang="zh">获得所有文化信息集合</para>
    /// <para lang="en">Get all culture info collection</para>
    /// </summary>
    /// <param name="includeParentCultures"></param>
    /// <returns></returns>
    public IEnumerable<LocalizedString> GetAllStringsByCulture(bool includeParentCultures) => [];

    /// <summary>
    /// <para lang="zh">获得指定类型文化信息集合</para>
    /// <para lang="en">Get specified type culture info collection</para>
    /// </summary>
    /// <param name="typeName"><para lang="zh">类型名称</para><para lang="en">Type name</para></param>
    /// <param name="includeParentCultures"></param>
    /// <returns></returns>
    public IEnumerable<LocalizedString> GetAllStringsByType(string typeName, bool includeParentCultures) => [];
}
