// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// ITableToolbarButton 接口
/// </summary>
public interface ITableToolbarButton<TItem> : IToolbarComponent
{
    /// <summary>
    /// 获得/设置 选中一行时启用按钮 默认 false 均可用
    /// </summary>
    bool IsEnableWhenSelectedOneRow { get; set; }

    /// <summary>
    /// 获得/设置 按钮是否被禁用回调方法
    /// </summary>
    Func<IEnumerable<TItem>, bool>? IsDisabledCallback { get; set; }
}
