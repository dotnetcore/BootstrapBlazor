// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">TabItem 配置属性类</para>
/// <para lang="en">TabItem configuration attribute class</para>
/// </summary>
[AttributeUsage(AttributeTargets.Class)]
public class TabItemOptionAttribute : Attribute
{
    /// <summary>
    /// <para lang="zh">获得/设置 TabItem 文本</para>
    /// <para lang="en">Gets or sets the text of the tab item.</para>
    /// </summary>
    public string? Text { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 当前 TabItem 是否可关闭。默认值为 true。</para>
    /// <para lang="en">Gets or sets whether the current TabItem is closable. Default is true.</para>
    /// </summary>
    public bool Closable { get; set; } = true;

    /// <summary>
    /// <para lang="zh">获得/设置 图标字符串</para>
    /// <para lang="en">Gets or sets the icon string.</para>
    /// </summary>
    public string? Icon { get; set; }
}
