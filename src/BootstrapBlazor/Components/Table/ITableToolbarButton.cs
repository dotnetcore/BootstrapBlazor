// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">ITableToolbarButton 接口
///</para>
/// <para lang="en">ITableToolbarButton 接口
///</para>
/// </summary>
public interface ITableToolbarButton<TItem> : IToolbarComponent
{
    /// <summary>
    /// <para lang="zh">获得/设置 选中一行时启用按钮 默认 false 均可用
    ///</para>
    /// <para lang="en">Gets or sets 选中一行时启用button Default is false 均可用
    ///</para>
    /// </summary>
    bool IsEnableWhenSelectedOneRow { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 按钮是否被禁用回调方法
    ///</para>
    /// <para lang="en">Gets or sets buttonwhether被禁用callback method
    ///</para>
    /// </summary>
    Func<IEnumerable<TItem>, bool>? IsDisabledCallback { get; set; }
}
