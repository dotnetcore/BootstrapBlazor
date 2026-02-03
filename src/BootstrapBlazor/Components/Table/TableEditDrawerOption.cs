// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.AspNetCore.Components.Forms;

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">编辑抽屉配置类</para>
/// <para lang="en">Edit drawer configuration class</para>
/// </summary>
public class TableEditDrawerOption<TModel> : ITableEditDialogOption<TModel>
{
    /// <summary>
    /// <inheritdoc cref="ITableEditDialogOption{TModel}.IsTracking"/>
    /// </summary>
    public bool IsTracking { get; set; }

    /// <summary>
    /// <inheritdoc cref="ITableEditDialogOption{TModel}.ShowLabel"/>
    /// </summary>
    public bool ShowLabel { get; set; } = true;

    /// <summary>
    /// <inheritdoc cref="ITableEditDialogOption{TModel}.ItemChangedType"/>
    /// </summary>
    public ItemChangedType ItemChangedType { get; set; }

    /// <summary>
    /// <inheritdoc cref="ITableEditDialogOption{TModel}.ItemsPerRow"/>
    /// </summary>
    public int? ItemsPerRow { get; set; }

    /// <summary>
    /// <inheritdoc cref="ITableEditDialogOption{TModel}.RowType"/>
    /// </summary>
    public RowType RowType { get; set; }

    /// <summary>
    /// <inheritdoc cref="ITableEditDialogOption{TModel}.LabelAlign"/>
    /// </summary>
    public Alignment LabelAlign { get; set; }

    /// <summary>
    /// <inheritdoc cref="ITableEditDialogOption{TModel}.ShowLoading"/>
    /// </summary>
    public bool ShowLoading { get; set; }

    /// <summary>
    /// <inheritdoc cref="ITableEditDialogOption{TModel}.ShowUnsetGroupItemsOnTop"/>
    /// </summary>
    public bool ShowUnsetGroupItemsOnTop { get; set; }

    /// <summary>
    /// <inheritdoc cref="ITableEditDialogOption{TModel}.Model"/>
    /// </summary>
    public TModel? Model { get; set; }

    /// <summary>
    /// <inheritdoc cref="ITableEditDialogOption{TModel}.DisableAutoSubmitFormByEnter"/>
    /// </summary>
    public bool? DisableAutoSubmitFormByEnter { get; set; }

    /// <summary>
    /// <inheritdoc cref="ITableEditDialogOption{TModel}.LabelWidth"/>
    /// </summary>
    public int? LabelWidth { get; set; } = 120;

    /// <summary>
    /// <inheritdoc cref="ITableEditDialogOption{TModel}.CloseButtonIcon"/>
    /// </summary>
    public string? CloseButtonIcon { get; set; }

    /// <summary>
    /// <inheritdoc cref="ITableEditDialogOption{TModel}.CloseButtonText"/>
    /// </summary>
    public string? CloseButtonText { get; set; }

    /// <summary>
    /// <inheritdoc cref="ITableEditDialogOption{TModel}.SaveButtonIcon"/>
    /// </summary>
    public string? SaveButtonIcon { get; set; }

    /// <summary>
    /// <inheritdoc cref="ITableEditDialogOption{TModel}.SaveButtonText"/>
    /// </summary>
    public string? SaveButtonText { get; set; }

    /// <summary>
    /// <inheritdoc cref="ITableEditDialogOption{TModel}.DialogBodyTemplate"/>
    /// </summary>
    public RenderFragment<TModel>? DialogBodyTemplate { get; set; }

    /// <summary>
    /// <inheritdoc cref="ITableEditDialogOption{TModel}.DialogFooterTemplate"/>
    /// </summary>
    public RenderFragment<TModel>? DialogFooterTemplate { get; set; }

    /// <summary>
    /// <inheritdoc cref="ITableEditDialogOption{TModel}.Items"/>
    /// </summary>
    public IEnumerable<IEditorItem>? Items { get; set; }

    /// <summary>
    /// <inheritdoc cref="ITableEditDialogOption{TModel}.OnCloseAsync"/>
    /// </summary>
    public Func<Task>? OnCloseAsync { get; set; }

    /// <summary>
    /// <inheritdoc cref="ITableEditDialogOption{TModel}.OnEditAsync"/>
    /// </summary>
    public Func<EditContext, Task<bool>>? OnEditAsync { get; set; }
}
