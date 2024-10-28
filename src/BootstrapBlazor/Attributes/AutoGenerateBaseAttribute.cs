﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// AutoGenerateColumn 标签基类，用于 <see cref="Table{TItem}"/> 标识自动生成列
/// </summary>
public abstract class AutoGenerateBaseAttribute : Attribute
{
    /// <summary>
    /// 获得/设置 当前列是否可编辑 默认为 true 当设置为 false 时自动生成编辑 UI 不生成此列
    /// </summary>
    [Obsolete("已弃用，是否可编辑改用 Readonly 参数，是否可见改用 Ignore 参数; Deprecated If it is editable, use the Readonly parameter. If it is visible, use the Ignore parameter.")]
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
