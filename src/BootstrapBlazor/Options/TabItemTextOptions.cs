// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">菜单与标签捆绑配置类</para>
/// <para lang="en">Menu and tab binding configuration class</para>
/// </summary>
internal class TabItemTextOptions
{
    /// <summary>
    /// <para lang="zh">获得/设置 Tab 标签文本</para>
    /// <para lang="en">Get/Set Tab label text</para>
    /// </summary>
    public string? Text { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 图标字符串</para>
    /// <para lang="en">Get/Set icon string</para>
    /// </summary>
    public string? Icon { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否激活 默认为 true</para>
    /// <para lang="en">Get/Set whether active default true</para>
    /// </summary>
    /// <value></value>
    public bool IsActive { get; set; } = true;

    /// <summary>
    /// <para lang="zh">获得/设置 当前 TabItem 是否可关闭 默认为 true</para>
    /// <para lang="en">Get/Set whether current TabItem is closable default true</para>
    /// </summary>
    public bool Closable { get; set; } = true;

    /// <summary>
    /// <para lang="zh">重置方法</para>
    /// <para lang="en">Reset method</para>
    /// </summary>
    public void Reset()
    {
        Text = null;
        Icon = null;
        IsActive = true;
        Closable = true;
    }

    /// <summary>
    /// <para lang="zh">是否可用方法</para>
    /// <para lang="en">Is valid method</para>
    /// </summary>
    /// <returns></returns>
    public bool Valid() => Text != null;
}
