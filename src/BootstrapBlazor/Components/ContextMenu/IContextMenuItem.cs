// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">IContextMenuItem 接口</para>
/// <para lang="en">The interface for a menu item in a context menu</para>
/// </summary>
public interface IContextMenuItem
{
    /// <summary>
    /// <para lang="zh">获得/设置 是否显示，默认为 true 显示</para>
    /// <para lang="en">Gets or sets whether to display. Default is true</para>
    /// </summary>
    /// <remarks>一般是通过业务逻辑判断是否显示</remarks>
    bool IsShow { get; set; }
}
