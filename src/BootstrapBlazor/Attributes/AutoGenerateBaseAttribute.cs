// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// 
/// </summary>
public abstract class AutoGenerateBaseAttribute : Attribute
{
    /// <summary>
    /// 获得/设置 当前列是否可编辑 默认为 true 当设置为 false 时自动生成编辑 UI 不生成此列
    /// </summary>
    public bool Editable { get; set; } = true;

    /// <summary>
    /// 获得/设置 当前列编辑时是否只读 默认为 false
    /// </summary>
    public bool Readonly { get; set; }

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
    /// 获得/设置 字段鼠标悬停提示
    /// </summary>
    public bool ShowTips { get; set; }
}
