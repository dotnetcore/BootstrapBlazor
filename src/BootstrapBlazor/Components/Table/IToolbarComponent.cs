// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
///  <para lang="zh">工具栏按钮接口</para>
///  <para lang="en">工具栏button接口</para>
/// </summary>
public interface IToolbarComponent
{
    /// <summary>
    ///  <para lang="zh">获得/设置 是否显示 默认 true 显示</para>
    ///  <para lang="en">Gets or sets whetherdisplay Default is true display</para>
    /// </summary>
    bool IsShow { get; set; }
}
