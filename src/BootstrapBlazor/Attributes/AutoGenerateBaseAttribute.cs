// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">AutoGenerateColumn 属性基类，用于标记 <see cref="Table{TItem}"/> 中的自动生成列</para>
/// <para lang="en">Base class for AutoGenerateColumn attribute, used to mark auto-generated columns in <see cref="Table{TItem}"/></para>
/// </summary>
public abstract class AutoGenerateBaseAttribute : Attribute
{
    /// <summary>
    /// <para lang="zh">获得/设置 当前列是否可编辑。默认为 true。当设置为 false 时，自动编辑 UI 不会生成此列。</para>
    /// <para lang="en">Gets or sets whether the current column is editable. Default is true. When set to false, the auto-generated edit UI will not generate this column.</para>
    /// </summary>
    [Obsolete("Deprecated. If it is editable, use the Readonly parameter. If it is visible, use the Ignore parameter.")]
    [ExcludeFromCodeCoverage]
    public bool Editable { get; set; } = true;

    /// <summary>
    /// <para lang="zh">获得/设置 当前列是否渲染。默认为 false。当设置为 true 时，UI 不会生成此列。</para>
    /// <para lang="en">Gets or sets whether the current column is rendered. Default is false. When set to true, the UI will not generate this column.</para>
    /// </summary>
    public bool Ignore { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 当前编辑项是否只读。默认为 false。</para>
    /// <para lang="en">Gets or sets whether the current edit item is read-only. Default is false.</para>
    /// </summary>
    public bool Readonly { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 当前编辑项是否可见。默认为 true。</para>
    /// <para lang="en">Gets or sets whether the current edit item is visible. Default is true.</para>
    /// </summary>
    public bool Visible { get; set; } = true;

    /// <summary>
    /// <para lang="zh">获得/设置 是否允许排序。默认为 false。</para>
    /// <para lang="en">Gets or sets whether sorting is allowed. Default is false.</para>
    /// </summary>
    public bool Sortable { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否允许数据筛选。默认为 false。</para>
    /// <para lang="en">Gets or sets whether data filtering is allowed. Default is false.</para>
    /// </summary>
    public bool Filterable { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 此列是否参与搜索。默认为 false。</para>
    /// <para lang="en">Gets or sets whether the column participates in search. Default is false.</para>
    /// </summary>
    public bool Searchable { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 此列是否允许文本换行。默认为 false。</para>
    /// <para lang="en">Gets or sets whether text wrapping is allowed in this column. Default is false.</para>
    /// </summary>
    public bool TextWrap { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 此列文本溢出时是否显示省略号。默认为 false。</para>
    /// <para lang="en">Gets or sets whether text overflow is ellipsis in this column. Default is false.</para>
    /// </summary>
    public bool TextEllipsis { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 文本对齐方式。默认为 Alignment.None。</para>
    /// <para lang="en">Gets or sets the text alignment. Default is Alignment.None.</para>
    /// </summary>
    public Alignment Align { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 鼠标悬停时是否显示提示信息。默认为 false。</para>
    /// <para lang="en">Gets or sets whether to show tooltips on mouse hover. Default is false.</para>
    /// </summary>
    public bool ShowTips { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 列是否可以被复制。默认为 false。</para>
    /// <para lang="en">Gets or sets whether the column can be copied. Default is false.</para>
    /// </summary>
    public bool ShowCopyColumn { get; set; }
}
