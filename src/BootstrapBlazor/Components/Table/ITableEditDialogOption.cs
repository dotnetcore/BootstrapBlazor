// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.AspNetCore.Components.Forms;

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">Table 编辑配置接口</para>
/// <para lang="en">Table edit configuration interface</para>
/// </summary>
public interface ITableEditDialogOption<TModel>
{
    /// <summary>
    /// <para lang="zh">获得/设置 组件是否采用 Tracking 模式对编辑项进行直接更新，默认为 false</para>
    /// <para lang="en">Gets or sets whether to use Tracking mode to directly update edited items. Default is false</para>
    /// </summary>
    bool IsTracking { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否显示标签，默认为 true</para>
    /// <para lang="en">Gets or sets whether to display label. Default is true</para>
    /// </summary>
    bool ShowLabel { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 实体类编辑模式 Add 还是 Update</para>
    /// <para lang="en">Gets or sets entity class edit mode, Add or Update</para>
    /// </summary>
    ItemChangedType ItemChangedType { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 每行显示组件数量，默认为 null</para>
    /// <para lang="en">Gets or sets the number of components displayed per row. Default is null</para>
    /// </summary>
    int? ItemsPerRow { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 设置行内组件布局格式，默认为 Row 布局</para>
    /// <para lang="en">Gets or sets the layout format of inline components. Default is Row layout</para>
    /// </summary>
    RowType RowType { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 在 RowType Inline 模式下标签对齐方式，默认为 None（等效于 Left 左对齐）</para>
    /// <para lang="en">Gets or sets the label alignment in RowType Inline mode. Default is None (equivalent to Left)</para>
    /// </summary>
    Alignment LabelAlign { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 查询时是否显示正在加载中动画，默认为 false</para>
    /// <para lang="en">Gets or sets whether to display loading animation when querying. Default is false</para>
    /// </summary>
    bool ShowLoading { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 未分组编辑项布局位置，默认为 false 在尾部</para>
    /// <para lang="en">Gets or sets the layout position of ungrouped edit items. Default is false (at the end)</para>
    /// </summary>
    bool ShowUnsetGroupItemsOnTop { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 编辑框模型</para>
    /// <para lang="en">Gets or sets edit form model</para>
    /// </summary>
    TModel? Model { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否禁用表单内回车自动提交功能，默认为 null 未设置</para>
    /// <para lang="en">Gets or sets whether to disable auto submit form by Enter key. Default is null</para>
    /// </summary>
    bool? DisableAutoSubmitFormByEnter { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 保存按钮文本</para>
    /// <para lang="en">Gets or sets save button text</para>
    /// </summary>
    string? SaveButtonText { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 保存按钮图标 默认 null 使用当前主题图标</para>
    /// <para lang="en">Gets or sets save button icon. Default is null, using the current theme icon</para>
    /// </summary>
    string? SaveButtonIcon { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 关闭按钮文本</para>
    /// <para lang="en">Gets or sets close button text</para>
    /// </summary>
    string? CloseButtonText { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 关闭按钮图标 默认 null 使用当前主题图标</para>
    /// <para lang="en">Gets or sets close button icon. Default is null, using the current theme icon</para>
    /// </summary>
    string? CloseButtonIcon { get; set; }

    /// <summary>
    /// <para lang="zh">获得 编辑项集合</para>
    /// <para lang="en">Gets edit item collection</para>
    /// </summary>
    IEnumerable<IEditorItem>? Items { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 保存回调委托</para>
    /// <para lang="en">Gets or sets save callback delegate</para>
    /// </summary>
    Func<EditContext, Task<bool>>? OnEditAsync { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 关闭弹窗回调方法</para>
    /// <para lang="en">Gets or sets close dialog callback method</para>
    /// </summary>
    Func<Task>? OnCloseAsync { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 EditDialog Body 模板</para>
    /// <para lang="en">Gets or sets EditDialog Body template</para>
    /// </summary>
    RenderFragment<TModel>? DialogBodyTemplate { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 EditDialog Footer 模板</para>
    /// <para lang="en">Gets or sets EditDialog Footer template</para>
    /// </summary>
    RenderFragment<TModel>? DialogFooterTemplate { get; set; }
}
