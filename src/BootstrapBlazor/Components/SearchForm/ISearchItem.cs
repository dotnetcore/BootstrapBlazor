// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">SearchItem 接口</para>
/// <para lang="en">SearchItem interface</para>
/// </summary>
public interface ISearchItem
{
    /// <summary>
    /// <para lang="zh">获得 绑定列的类型</para>
    /// <para lang="en">Gets the type of the bound column</para>
    /// </summary>
    Type PropertyType { get; }

    /// <summary>
    /// <para lang="zh">获得/设置 表头显示文本</para>
    /// <para lang="en">Gets or sets the header display text</para>
    /// </summary>
    string? Text { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 字段名称</para>
    /// <para lang="en">Gets or sets the field name</para>
    /// </summary>
    string FieldName { get; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否显示前置标签 默认为 null 未设置时默认显示标签</para>
    /// <para lang="en">Gets or sets Whether to Show Label. Default is null, show label if not set</para>
    /// </summary>
    public bool? ShowLabel { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否显示标签提示，常用于标签文本过长被截断时，默认为 null</para>
    /// <para lang="en">Gets or sets whether to show the label tooltip, usually when the label text is too long and truncated. Default is null</para>
    /// </summary>
    bool? ShowLabelTooltip { get; set; }
    /// <summary>
    /// <para lang="zh">获得/设置 当前属性的分组名称</para>
    /// <para lang="en">Gets or sets the group name of the current property</para>
    /// </summary>
    string? GroupName { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 当前属性的分组顺序，默认为 0</para>
    /// <para lang="en">Gets or sets the group order of the current property. Default is 0</para>
    /// </summary>
    int GroupOrder { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 顺序号</para>
    /// <para lang="en">Gets or sets the order number</para>
    /// </summary>
    int Order { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 字段的列跨度，默认为 0</para>
    /// <para lang="en">Gets or sets the field column span. Default is 0</para>
    /// </summary>
    int Cols { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 搜索元数据</para>
    /// <para lang="en">Gets or sets the search metadata</para>
    /// </summary>
    public ISearchMetaData? MetaData { get; set; }

    /// <summary>
    /// <para lang="zh">获得 过滤器实例</para>
    /// <para lang="en">Gets the filter instance</para>
    /// </summary>
    /// <returns>
    ///   <para lang="zh">过滤器实例</para>
    ///   <para lang="en">Filter instance</para>
    /// </returns>
    public FilterKeyValueAction? GetFilter() => MetaData?.GetFilter(FieldName);
}
