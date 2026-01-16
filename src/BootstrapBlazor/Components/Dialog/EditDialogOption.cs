// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.AspNetCore.Components.Forms;

namespace BootstrapBlazor.Components;

/// <summary>
///  <para lang="zh">编辑弹窗配置类</para>
///  <para lang="en">Edit Dialog Option Class</para>
/// </summary>
public class EditDialogOption<TModel> : DialogOption, ITableEditDialogOption<TModel>
{
    /// <summary>
    ///  <para lang="zh">构造函数</para>
    ///  <para lang="en">Constructor</para>
    /// </summary>
    public EditDialogOption()
    {
        ShowCloseButton = false;
        ShowFooter = false;
    }

    /// <summary>
    ///  <para lang="zh">获得/设置 组件是否采用 Tracking 模式对编辑项进行直接更新 默认 false</para>
    ///  <para lang="en">Get/Set Whether Component Uses Tracking Mode to Update Editing Items Directly. Default is false</para>
    /// </summary>
    public bool IsTracking { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 是否显示标签 默认为 true 显示标签</para>
    ///  <para lang="en">Get/Set Whether to Show Label. Default is true</para>
    /// </summary>
    public bool ShowLabel { get; set; } = true;

    /// <summary>
    ///  <para lang="zh">获得/设置 实体类编辑模式 Add 还是 Update</para>
    ///  <para lang="en">Get/Set Item Changed Type (Add or Update)</para>
    /// </summary>
    public ItemChangedType ItemChangedType { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 每行显示组件数量 默认为 null</para>
    ///  <para lang="en">Get/Set Items Per Row. Default is null</para>
    /// </summary>
    public int? ItemsPerRow { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 设置行内组件布局格式 默认 Row 布局</para>
    ///  <para lang="en">Get/Set Row Layout. Default is Row</para>
    /// </summary>
    public RowType RowType { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 设置 <see cref="RowType" /> Inline 模式下标签对齐方式 默认 None 等效于 Left 左对齐</para>
    ///  <para lang="en">Get/Set Label Alignment in <see cref="RowType" /> Inline Mode. Default is None, equivalent to Left</para>
    /// </summary>
    public Alignment LabelAlign { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 查询时是否显示正在加载中动画 默认为 false</para>
    ///  <para lang="en">Get/Set Whether to Show Loading Animation When Querying. Default is false</para>
    /// </summary>
    public bool ShowLoading { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 未分组编辑项布局位置 默认 false 在尾部</para>
    ///  <para lang="en">Get/Set Unset Group Items Position. Default is false (at the end)</para>
    /// </summary>
    public bool ShowUnsetGroupItemsOnTop { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 编辑框模型</para>
    ///  <para lang="en">Get/Set Edit Model</para>
    /// </summary>
    public TModel? Model { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 是否禁用表单内回车自动提交功能 默认 null 未设置</para>
    ///  <para lang="en">Get/Set Whether to Disable Auto Submit Form By Enter. Default is null</para>
    /// </summary>
    public bool? DisableAutoSubmitFormByEnter { get; set; }

    /// <summary>
    ///  <para lang="zh">获得 编辑项集合</para>
    ///  <para lang="en">Get Items</para>
    /// </summary>
    public IEnumerable<IEditorItem>? Items { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 EditDialog Body 模板</para>
    ///  <para lang="en">Get/Set EditDialog Body Template</para>
    /// </summary>
    public RenderFragment<TModel>? DialogBodyTemplate { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 EditDialog Footer 模板</para>
    ///  <para lang="en">Get/Set EditDialog Footer Template</para>
    /// </summary>
    public RenderFragment<TModel>? DialogFooterTemplate { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 保存回调委托</para>
    ///  <para lang="en">Get/Set Save Callback Delegate</para>
    /// </summary>
    public Func<EditContext, Task<bool>>? OnEditAsync { get; set; }
}
