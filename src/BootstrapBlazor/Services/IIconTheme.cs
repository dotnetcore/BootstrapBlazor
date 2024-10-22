// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Author: Argo Zhang (argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// Icon 主题服务
/// </summary>
public interface IIconTheme
{
    /// <summary>
    /// 获得所有图标
    /// </summary>
    /// <returns></returns>
    Dictionary<ComponentIcons, string> GetIcons();
}
