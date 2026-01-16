// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

#if NET8_0_OR_GREATER
using System.Collections.Frozen;
#endif

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">Icon 主题服务</para>
/// <para lang="en">Icon Theme Service</para>
/// </summary>
public interface IIconTheme
{
    /// <summary>
    /// <para lang="zh">获得所有图标</para>
    /// <para lang="en">Get All Icons</para>
    /// </summary>
    /// <returns></returns>
#if NET8_0_OR_GREATER
    FrozenDictionary<ComponentIcons, string> GetIcons();
#else
    Dictionary<ComponentIcons, string> GetIcons();
#endif
}
