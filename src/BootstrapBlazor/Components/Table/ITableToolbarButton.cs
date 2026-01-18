// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
///  <para lang="zh">ITableToolbarButton 接口</para>
///  <para lang="en">ITableToolbarButton interface</para>
/// </summary>
public interface ITableToolbarButton<TItem> : IToolbarComponent
{
    /// <summary>
    ///  <para lang="zh">获得/设置 选中一行时启用按钮，默认为 false</para>
    ///  <para lang="en">Gets or sets whether to enable button when one row is selected. Default is false.</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    bool IsEnableWhenSelectedOneRow { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 按钮是否被禁用的回调方法</para>
    ///  <para lang="en">Gets or sets the callback method for button disabled state</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    Func<IEnumerable<TItem>, bool>? IsDisabledCallback { get; set; }
}
