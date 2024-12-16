// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Server.Components.Components;

/// <summary>
/// 
/// </summary>
public sealed partial class State
{
    /// <summary>
    /// 获得/设置 是否为新组件 默认为 false
    /// </summary>
    [Parameter]
    public bool IsNew { get; set; }

    /// <summary>
    /// 获得/设置 是否为更新功能 默认为 false
    /// </summary>
    [Parameter]
    public bool IsUpdate { get; set; }

    /// <summary>
    /// 获得/设置 组件数量
    /// </summary>
    [Parameter]
    public int Count { get; set; }
}
