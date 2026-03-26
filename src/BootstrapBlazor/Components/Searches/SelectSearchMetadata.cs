// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">选择类型搜索元数据类</para>
/// <para lang="en">Select type search metadata class</para>
/// </summary>
public class SelectSearchMetadata : StringSearchMetadata
{
    /// <summary>
    /// <para lang="zh">获得/设置 是否显示搜索框 默认 false 不显示</para>
    /// <para lang="en">Gets or sets a value indicating whether to show the search box. Default is false.</para>
    /// </summary>
    public bool ShowSearch { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 搜索文本改变时的回调方法</para>
    /// <para lang="en">Gets or sets the callback method when the search text changes</para>
    /// </summary>
    public Func<string, IEnumerable<SelectedItem>>? OnSearchTextChanged { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 项目模板</para>
    /// <para lang="en">Gets or sets the item template</para>
    /// </summary>
    public RenderFragment<SelectedItem>? ItemTemplate { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 选择项集合</para>
    /// <para lang="en">Gets or sets the collection of select items</para>
    /// </summary>
    /// <returns></returns>
    public IEnumerable<SelectedItem>? Items { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否使用 Popover 渲染下拉框 默认 false</para>
    /// <para lang="en">Gets or sets Whether to use Popover to render dropdown. Default false</para>
    /// </summary>
    public bool IsPopover { get; set; }
}
