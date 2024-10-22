// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// 标签与菜单捆绑配置项
/// </summary>
public class TabItemBindOptions
{
    /// <summary>
    /// 获得/设置 集合
    /// </summary>
    public Dictionary<string, TabItemOptionAttribute> Binders { get; set; } = [];
}
