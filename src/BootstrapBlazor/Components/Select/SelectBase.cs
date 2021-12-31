// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace BootstrapBlazor.Components;

/// <summary>
/// Select 组件基类
/// </summary>
public abstract class SelectBase<TValue> : ValidateBase<TValue>
{
    /// <summary>
    /// 当前选择项实例
    /// </summary>
    protected SelectedItem? SelectedItem { get; set; }

    /// <summary>
    /// 获得/设置 按钮颜色
    /// </summary>
    [Parameter]
    public Color Color { get; set; } = Color.None;

    /// <summary>
    /// 获得/设置 SWal 图标
    /// </summary>
    [Parameter]
    public SwalCategory SwalCategory { get; set; } = SwalCategory.Question;

    /// <summary>
    /// 获得/设置 Swal 标题
    /// </summary>
    [Parameter]
    public string? SwalTitle { get; set; }

    /// <summary>
    /// 获得/设置 Swal 内容
    /// </summary>
    [Parameter]
    public string? SwalContent { get; set; }

    /// <summary>
    /// 获得/设置 是否显示 Swal Footer
    /// </summary>
    [Parameter]
    public string? SwalFooter { get; set; }

    /// <summary>
    /// 获得/设置 下拉框项目改变前回调委托方法 返回 true 时选项值改变，否则选项值不变
    /// </summary>
    [Parameter]
    public Func<SelectedItem, Task<bool>>? OnBeforeSelectedItemChange { get; set; }

    /// <summary>
    /// 获得/设置 绑定数据集
    /// </summary>
    [Parameter]
    [NotNull]
    public IEnumerable<SelectedItem>? Items { get; set; }

    /// <summary>
    /// 获得/设置 选项模板
    /// </summary>
    [Parameter]
    public RenderFragment<SelectedItem>? ItemTemplate { get; set; }

    /// <summary>
    /// SelectedItemChanged 方法
    /// </summary>
    [Parameter]
    public Func<SelectedItem, Task>? OnSelectedItemChanged { get; set; }
}
