// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
///  <para lang="zh">查询弹窗配置类</para>
///  <para lang="en">Search Dialog Option Class</para>
/// </summary>
public class SearchDialogOption<TModel> : DialogOption
{
    /// <summary>
    ///  <para lang="zh">构造函数</para>
    ///  <para lang="en">Constructor</para>
    /// </summary>
    public SearchDialogOption()
    {
        ShowCloseButton = false;
        ShowFooter = false;
    }

    /// <summary>
    ///  <para lang="zh">获得/设置 是否显示标签 默认为 true 显示标签</para>
    ///  <para lang="en">Get/Set Whether to Show Label. Default is true</para>
    /// </summary>
    public bool ShowLabel { get; set; } = true;

    /// <summary>
    ///  <para lang="zh">获得/设置 未分组编辑项布局位置 默认 false 在尾部</para>
    ///  <para lang="en">Get/Set Unset Group Items Position. Default is false (at the end)</para>
    /// </summary>
    public bool ShowUnsetGroupItemsOnTop { get; set; }

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
    ///  <para lang="zh">获得/设置 编辑框模型</para>
    ///  <para lang="en">Get/Set Search Model</para>
    /// </summary>
    public TModel? Model { get; set; }

    /// <summary>
    ///  <para lang="zh">获得 搜索条件集合</para>
    ///  <para lang="en">Get Search Items</para>
    /// </summary>
    public IEnumerable<IEditorItem>? Items { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 SearchDialog Body 模板</para>
    ///  <para lang="en">Get/Set SearchDialog Body Template</para>
    /// </summary>
    public RenderFragment<TModel>? DialogBodyTemplate { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 重置按钮文本</para>
    ///  <para lang="en">Get/Set Reset Button Text</para>
    /// </summary>
    public string? ResetButtonText { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 查询按钮文本</para>
    ///  <para lang="en">Get/Set Query Button Text</para>
    /// </summary>
    public string? QueryButtonText { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 重置回调委托</para>
    ///  <para lang="en">Get/Set Reset Callback Delegate</para>
    /// </summary>
    /// <returns></returns>
    public Func<Task>? OnResetSearchClick { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 搜索回调委托</para>
    ///  <para lang="en">Get/Set Search Callback Delegate</para>
    /// </summary>
    /// <returns></returns>
    public Func<Task>? OnSearchClick { get; set; }
}
