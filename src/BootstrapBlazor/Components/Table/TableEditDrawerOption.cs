// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.AspNetCore.Components.Forms;

namespace BootstrapBlazor.Components;

/// <summary>
///  <para lang="zh">编辑弹窗配置类</para>
///  <para lang="en">编辑弹窗配置类</para>
/// </summary>
public class TableEditDrawerOption<TModel> : ITableEditDialogOption<TModel>
{
    /// <summary>
    ///  <para lang="zh">构造函数</para>
    ///  <para lang="en">构造函数</para>
    /// </summary>
    public TableEditDrawerOption()
    {
        ShowLabel = true;
    }

    /// <summary>
    ///  <para lang="zh">获得/设置 组件是否采用 Tracking 模式对编辑项进行直接更新 默认 false</para>
    ///  <para lang="en">Gets or sets componentwhether采用 Tracking 模式对编辑项进行直接更新 Default is false</para>
    /// </summary>
    public bool IsTracking { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 是否显示标签 默认为 true 显示标签</para>
    ///  <para lang="en">Gets or sets whetherdisplay标签 Default is为 true display标签</para>
    /// </summary>
    public bool ShowLabel { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 实体类编辑模式 Add 还是 Update</para>
    ///  <para lang="en">Gets or sets 实体类编辑模式 Add 还是 Update</para>
    /// </summary>
    public ItemChangedType ItemChangedType { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 每行显示组件数量 默认为 null</para>
    ///  <para lang="en">Gets or sets 每行displaycomponent数量 Default is为 null</para>
    /// </summary>
    public int? ItemsPerRow { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 设置行内组件布局格式 默认 Row 布局</para>
    ///  <para lang="en">Gets or sets Sets行内component布局格式 Default is Row 布局</para>
    /// </summary>
    public RowType RowType { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 设置 <see cref="RowType" /> Inline 模式下标签对齐方式 默认 None 等效于 Left 左对齐</para>
    ///  <para lang="en">Gets or sets Sets <see cref="RowType" /> Inline 模式下标签对齐方式 Default is None 等效于 Left 左对齐</para>
    /// </summary>
    public Alignment LabelAlign { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 查询时是否显示正在加载中动画 默认为 false</para>
    ///  <para lang="en">Gets or sets 查询时whetherdisplay正在加载中动画 Default is为 false</para>
    /// </summary>
    public bool ShowLoading { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 未分组编辑项布局位置 默认 false 在尾部</para>
    ///  <para lang="en">Gets or sets 未分组编辑项布局位置 Default is false 在尾部</para>
    /// </summary>
    public bool ShowUnsetGroupItemsOnTop { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 编辑框模型</para>
    ///  <para lang="en">Gets or sets 编辑框模型</para>
    /// </summary>
    public TModel? Model { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 是否禁用表单内回车自动提交功能 默认 null 未设置</para>
    ///  <para lang="en">Gets or sets whether禁用表单内回车自动提交功能 Default is null 未Sets</para>
    /// </summary>
    public bool? DisableAutoSubmitFormByEnter { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 关闭按钮图标 默认 null 使用当前主题图标</para>
    ///  <para lang="en">Gets or sets 关闭buttonicon Default is null 使用当前主题icon</para>
    /// </summary>
    public string? CloseButtonIcon { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 关闭按钮文本</para>
    ///  <para lang="en">Gets or sets 关闭button文本</para>
    /// </summary>
    public string? CloseButtonText { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 保存按钮图标 默认 null 使用当前主题图标</para>
    ///  <para lang="en">Gets or sets 保存buttonicon Default is null 使用当前主题icon</para>
    /// </summary>
    public string? SaveButtonIcon { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 查询按钮文本</para>
    ///  <para lang="en">Gets or sets 查询button文本</para>
    /// </summary>
    public string? SaveButtonText { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 EditDialog Body 模板</para>
    ///  <para lang="en">Gets or sets EditDialog Body template</para>
    /// </summary>
    public RenderFragment<TModel>? DialogBodyTemplate { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 EditDialog Footer 模板</para>
    ///  <para lang="en">Gets or sets EditDialog Footer template</para>
    /// </summary>
    public RenderFragment<TModel>? DialogFooterTemplate { get; set; }

    /// <summary>
    ///  <para lang="zh">获得 编辑项集合</para>
    ///  <para lang="en">Gets 编辑项collection</para>
    /// </summary>
    public IEnumerable<IEditorItem>? Items { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 关闭弹窗回调方法</para>
    ///  <para lang="en">Gets or sets 关闭弹窗callback method</para>
    /// </summary>
    public Func<Task>? OnCloseAsync { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 保存回调委托</para>
    ///  <para lang="en">Gets or sets 保存回调delegate</para>
    /// </summary>
    public Func<EditContext, Task<bool>>? OnEditAsync { get; set; }
}
