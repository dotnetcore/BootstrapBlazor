// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// AutoGenerateColumn 标签基类，用于 <see cref="Table{TItem}"/> 标识自动生成列
/// </summary>
public abstract class AutoGenerateBaseAttribute : Attribute
{
    /// <summary>
    /// 获得/设置 当前列是否可编辑 默认为 true 当设置为 false 时自动生成编辑 UI 不生成此列
    /// </summary>
    [Obsolete("已弃用，是否显示使用 Visible 参数，新建时使用 IsVisibleWhenAdd 编辑时使用 IsVisibleWhenEdit; Discarded, use Visible parameter. IsVisibleWhenAdd should be used when creating a new one, and IsVisibleWhenEdit should be used when editing")]
    [ExcludeFromCodeCoverage]
    public bool Editable { get; set; } = true;

    /// <summary>
    /// 获得/设置 当前列是否渲染 默认为 false 当设置为 true 时 UI 不生成此列
    /// </summary>
    public bool Ignore { get; set; }

    /// <summary>
    /// 获得/设置 当前编辑项是否只读 默认为 false
    /// </summary>
    public bool Readonly { get; set; }

    /// <summary>
    /// 获得/设置 当前编辑项是否显示 默认为 true
    /// </summary>
    public bool Visible { get; set; } = true;

    /// <summary>
    /// 获得/设置 是否允许排序 默认为 false
    /// </summary>
    public bool Sortable { get; set; }

    /// <summary>
    /// 获得/设置 是否允许过滤数据 默认为 false
    /// </summary>
    public bool Filterable { get; set; }

    /// <summary>
    /// 获得/设置 是否参与搜索 默认为 false
    /// </summary>
    public bool Searchable { get; set; }

    /// <summary>
    /// 获得/设置 本列是否允许换行 默认为 false
    /// </summary>
    public bool TextWrap { get; set; }

    /// <summary>
    /// 获得/设置 本列文本超出省略 默认为 false
    /// </summary>
    public bool TextEllipsis { get; set; }

    /// <summary>
    /// 获得/设置 文字对齐方式 默认为 Alignment.None
    /// </summary>
    public Alignment Align { get; set; }

    /// <summary>
    /// 获得/设置 字段鼠标悬停提示 默认为 false
    /// </summary>
    public bool ShowTips { get; set; }

    /// <summary>
    /// 获得/设置 是否可以拷贝列 默认 false 不可以
    /// </summary>
    public bool ShowCopyColumn { get; set; }
}
