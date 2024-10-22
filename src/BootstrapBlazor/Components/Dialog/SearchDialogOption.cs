﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// 查询弹窗配置类
/// </summary>
public class SearchDialogOption<TModel> : DialogOption
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public SearchDialogOption()
    {
        ShowCloseButton = false;
        ShowFooter = false;
    }

    /// <summary>
    /// 获得/设置 是否显示标签 默认为 true 显示标签
    /// </summary>
    public bool ShowLabel { get; set; } = true;

    /// <summary>
    /// 获得/设置 未分组编辑项布局位置 默认 false 在尾部
    /// </summary>
    public bool ShowUnsetGroupItemsOnTop { get; set; }

    /// <summary>
    /// 获得/设置 每行显示组件数量 默认为 null
    /// </summary>
    public int? ItemsPerRow { get; set; }

    /// <summary>
    /// 获得/设置 设置行内组件布局格式 默认 Row 布局
    /// </summary>
    public RowType RowType { get; set; }

    /// <summary>
    /// 获得/设置 设置 <see cref="RowType" /> Inline 模式下标签对齐方式 默认 None 等效于 Left 左对齐
    /// </summary>
    public Alignment LabelAlign { get; set; }

    /// <summary>
    /// 获得/设置 编辑框模型
    /// </summary>
    public TModel? Model { get; set; }

    /// <summary>
    /// 获得 搜索条件集合
    /// </summary>
    public IEnumerable<IEditorItem>? Items { get; set; }

    /// <summary>
    /// 获得/设置 SearchDialog Body 模板
    /// </summary>
    public RenderFragment<TModel>? DialogBodyTemplate { get; set; }

    /// <summary>
    /// 获得/设置 重置按钮文本
    /// </summary>
    public string? ResetButtonText { get; set; }

    /// <summary>
    /// 获得/设置 查询按钮文本
    /// </summary>
    public string? QueryButtonText { get; set; }

    /// <summary>
    /// 获得/设置 重置回调委托
    /// </summary>
    /// <returns></returns>
    public Func<Task>? OnResetSearchClick { get; set; }

    /// <summary>
    /// 获得/设置 搜索回调委托
    /// </summary>
    /// <returns></returns>
    public Func<Task>? OnSearchClick { get; set; }
}
