// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">选项类</para>
/// <para lang="en">Selected item class</para>
/// </summary>
public class SelectedItem
{
    /// <summary>
    /// <para lang="zh">构造函数</para>
    /// <para lang="en">Constructor</para>
    /// </summary>
    public SelectedItem() { }

    /// <summary>
    /// <para lang="zh">构造函数</para>
    /// <para lang="en">构造函数</para>
    /// </summary>
    public SelectedItem(string value, string text)
    {
        Value = value;
        Text = text;
    }

    /// <summary>
    /// <para lang="zh">获得/设置 显示名称</para>
    /// <para lang="en">Get/Set display text</para>
    /// </summary>
    public string Text { get; set; } = "";

    /// <summary>
    /// <para lang="zh">获得/设置 选项值</para>
    /// <para lang="en">Get/Set item value</para>
    /// </summary>
    public string Value { get; set; } = "";

    /// <summary>
    /// <para lang="zh">获得/设置 是否选中</para>
    /// <para lang="en">Get/Set whether active</para>
    /// </summary>
    public bool Active { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否禁用</para>
    /// <para lang="en">Get/Set whether disabled</para>
    /// </summary>
    public bool IsDisabled { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 分组名称</para>
    /// <para lang="en">Get/Set group name</para>
    /// </summary>
    public string GroupName { get; set; } = "";
}
