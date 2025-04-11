﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.Extensions.Localization;

namespace BootstrapBlazor.Components;

/// <summary>
/// 
/// </summary>
/// <typeparam name="TItem"></typeparam>
public partial class Table<TItem>
{
    /// <summary>
    /// 获得/设置 新建按钮文本
    /// </summary>
    [Parameter]
    [NotNull]
    public string? AddButtonText { get; set; }

    /// <summary>
    /// 获得/设置 新建按钮图标
    /// </summary>
    [Parameter]
    public string? AddButtonIcon { get; set; }

    /// <summary>
    /// 获得/设置 编辑按钮文本
    /// </summary>
    [Parameter]
    [NotNull]
    public string? EditButtonText { get; set; }

    /// <summary>
    /// 获得/设置 编辑按钮图标
    /// </summary>
    [Parameter]
    public string? EditButtonIcon { get; set; }

    /// <summary>
    /// 获得/设置 更新按钮文本
    /// </summary>
    [Parameter]
    [NotNull]
    public string? UpdateButtonText { get; set; }

    /// <summary>
    /// 获得/设置 取消按钮文本
    /// </summary>
    [Parameter]
    [NotNull]
    public string? CancelButtonText { get; set; }

    /// <summary>
    /// 获得/设置 取消按钮图标
    /// </summary>
    [Parameter]
    public string? CancelButtonIcon { get; set; }

    /// <summary>
    /// 获得/设置 删除按钮文本
    /// </summary>
    [Parameter]
    [NotNull]
    public string? DeleteButtonText { get; set; }

    /// <summary>
    /// 获得/设置 删除按钮图标
    /// </summary>
    [Parameter]
    public string? DeleteButtonIcon { get; set; }

    /// <summary>
    /// 获得/设置 取消删除按钮文本
    /// </summary>
    [Parameter]
    [NotNull]
    public string? CancelDeleteButtonText { get; set; }

    /// <summary>
    /// 获得/设置 保存按钮文本
    /// </summary>
    [Parameter]
    [NotNull]
    public string? SaveButtonText { get; set; }

    /// <summary>
    /// 获得/设置 保存按钮图标
    /// </summary>
    [Parameter]
    public string? SaveButtonIcon { get; set; }

    /// <summary>
    /// 获得/设置 保存按钮文本
    /// </summary>
    [Parameter]
    [NotNull]
    public string? CloseButtonText { get; set; }

    /// <summary>
    /// 获得/设置 保存按钮图标
    /// </summary>
    [Parameter]
    public string? CloseButtonIcon { get; set; }

    /// <summary>
    /// 获得/设置 确认删除按钮文本
    /// </summary>
    [Parameter]
    [NotNull]
    public string? ConfirmDeleteButtonText { get; set; }

    /// <summary>
    /// 获得/设置 确认删除弹窗文本
    /// </summary>
    [Parameter]
    [NotNull]
    public string? ConfirmDeleteContentText { get; set; }

    /// <summary>
    /// 获得/设置 刷新按钮文本
    /// </summary>
    [Parameter]
    [NotNull]
    public string? RefreshButtonText { get; set; }

    /// <summary>
    /// 获得/设置 刷新按钮图标
    /// </summary>
    [Parameter]
    public string? RefreshButtonIcon { get; set; }

    /// <summary>
    /// 获得/设置 视图按钮文本
    /// </summary>
    [Parameter]
    [NotNull]
    public string? CardViewButtonText { get; set; }

    /// <summary>
    /// 获得/设置 视图按钮图标
    /// </summary>
    [Parameter]
    public string? CardViewButtonIcon { get; set; }

    /// <summary>
    /// 获得/设置 列显示隐藏按钮提示信息文本
    /// </summary>
    [Parameter]
    [NotNull]
    public string? ColumnButtonTitleText { get; set; }

    /// <summary>
    /// 获得/设置 列按钮文本
    /// </summary>
    [Parameter]
    [NotNull]
    public string? ColumnButtonText { get; set; }

    /// <summary>
    /// 获得/设置 列拷贝按钮图标
    /// </summary>
    [Parameter]
    public string? CopyColumnButtonIcon { get; set; }

    /// <summary>
    /// 获得/设置 导出按钮文本
    /// </summary>
    [Parameter]
    [NotNull]
    public string? ExportButtonText { get; set; }

    /// <summary>
    /// 获得/设置 搜索栏 Placeholder 文本
    /// </summary>
    [Parameter]
    [NotNull]
    public string? SearchPlaceholderText { get; set; }

    /// <summary>
    /// 获得/设置 搜索按钮文本
    /// </summary>
    [Parameter]
    [NotNull]
    public string? SearchButtonText { get; set; }

    /// <summary>
    /// 获得/设置 搜索按钮图标
    /// </summary>
    [Parameter]
    public string? SearchButtonIcon { get; set; }

    /// <summary>
    /// 获得/设置 搜索弹窗文本
    /// </summary>
    [Parameter]
    [NotNull]
    public string? SearchModalTitle { get; set; }

    /// <summary>
    /// 获得/设置 获得/设置 搜索框提示文字
    /// </summary>
    [Parameter]
    [NotNull]
    public string? SearchTooltip { get; set; }

    /// <summary>
    /// 获得/设置 重置按钮文本
    /// </summary>
    [Parameter]
    [NotNull]
    public string? ResetSearchButtonText { get; set; }

    /// <summary>
    /// 获得/设置 重置按钮图标
    /// </summary>
    [Parameter]
    public string? ResetSearchButtonIcon { get; set; }

    /// <summary>
    /// 获得/设置 高级搜索按钮文本
    /// </summary>
    [Parameter]
    [NotNull]
    public string? AdvanceButtonText { get; set; }

    /// <summary>
    /// 获得/设置 高级搜索按钮图标
    /// </summary>
    [Parameter]
    public string? AdvanceButtonIcon { get; set; }

    /// <summary>
    /// 获得/设置 新增按钮 Toast 提示 Title 文字
    /// </summary>
    [Parameter]
    [NotNull]
    public string? AddButtonToastTitle { get; set; }

    /// <summary>
    /// 获得/设置 新增按钮 Toast 提示 Content 文字
    /// </summary>
    [Parameter]
    [NotNull]
    public string? AddButtonToastContent { get; set; }

    /// <summary>
    /// 获得/设置 编辑按钮 Toast 提示 Title 文字
    /// </summary>
    [Parameter]
    [NotNull]
    public string? EditButtonToastTitle { get; set; }

    /// <summary>
    /// 获得/设置 编辑按钮 Toast 未选择时提示 Content 文字
    /// </summary>
    [Parameter]
    [NotNull]
    public string? EditButtonToastNotSelectContent { get; set; }

    /// <summary>
    /// 获得/设置 编辑按钮 Toast 选择项设置不可编辑时提示 Content 文字
    /// </summary>
    [Parameter]
    [NotNull]
    public string? EditButtonToastReadonlyContent { get; set; }

    /// <summary>
    /// 获得/设置 编辑按钮 Toast 多项选择时提示 Content 文字
    /// </summary>
    [Parameter]
    [NotNull]
    public string? EditButtonToastMoreSelectContent { get; set; }

    /// <summary>
    /// 获得/设置 编辑按钮 Toast 未提供 Save 方法时提示 Content 文字
    /// </summary>
    [Parameter]
    [NotNull]
    public string? EditButtonToastNoSaveMethodContent { get; set; }

    /// <summary>
    /// 获得/设置 保存按钮 Toast 提示 Title 文字
    /// </summary>
    [Parameter]
    [NotNull]
    public string? SaveButtonToastTitle { get; set; }

    /// <summary>
    /// 获得/设置 保存按钮 Toast 提示 Content 文字
    /// </summary>
    [Parameter]
    [NotNull]
    public string? SaveButtonToastContent { get; set; }

    /// <summary>
    /// 获得/设置 保存按钮结果 Toast 提示 Content 文字
    /// </summary>
    [Parameter]
    [NotNull]
    public string? SaveButtonToastResultContent { get; set; }

    /// <summary>
    /// 获得/设置 保存成功文字
    /// </summary>
    [Parameter]
    [NotNull]
    public string? SuccessText { get; set; }

    /// <summary>
    /// 获得/设置 保存失败
    /// </summary>
    [Parameter]
    [NotNull]
    public string? FailText { get; set; }

    /// <summary>
    /// 获得/设置 删除按钮 Toast 提示 Title 文字
    /// </summary>
    [Parameter]
    [NotNull]
    public string? DeleteButtonToastTitle { get; set; }

    /// <summary>
    /// 获得/设置 删除按钮选项中有无法删除项时 Toast 提示文字
    /// </summary>
    [Parameter]
    [NotNull]
    public string? DeleteButtonToastCanNotDeleteContent { get; set; }

    /// <summary>
    /// 获得/设置 删除按钮 Toast 提示 Content 文字
    /// </summary>
    [Parameter]
    [NotNull]
    public string? DeleteButtonToastContent { get; set; }

    /// <summary>
    /// 获得/设置 删除按钮结果 Toast 提示 Content 文字
    /// </summary>
    [Parameter]
    [NotNull]
    public string? DeleteButtonToastResultContent { get; set; }

    /// <summary>
    /// 获得/设置 高级排序按钮文本
    /// </summary>
    [Parameter]
    [NotNull]
    public string? AdvancedSortButtonText { get; set; }

    /// <summary>
    /// 获得/设置 高级排序弹窗文本
    /// </summary>
    [Parameter]
    [NotNull]
    public string? AdvancedSortModalTitle { get; set; }

    [Inject]
    [NotNull]
    private IStringLocalizer<Table<TItem>>? Localizer { get; set; }

    private void OnInitLocalization()
    {
        AddButtonText ??= Localizer[nameof(AddButtonText)];
        EditButtonText ??= Localizer[nameof(EditButtonText)];
        UpdateButtonText ??= Localizer[nameof(UpdateButtonText)];
        DeleteButtonText ??= Localizer[nameof(DeleteButtonText)];
        CancelButtonText ??= Localizer[nameof(CancelButtonText)];
        SaveButtonText ??= Localizer[nameof(SaveButtonText)];
        CloseButtonText ??= Localizer[nameof(CloseButtonText)];
        CancelDeleteButtonText ??= Localizer[nameof(CancelDeleteButtonText)];
        ConfirmDeleteButtonText ??= Localizer[nameof(ConfirmDeleteButtonText)];
        ConfirmDeleteContentText ??= Localizer[nameof(ConfirmDeleteContentText)];
        RefreshButtonText ??= Localizer[nameof(RefreshButtonText)];
        CardViewButtonText ??= Localizer[nameof(CardViewButtonText)];
        ColumnButtonTitleText ??= Localizer[nameof(ColumnButtonTitleText)];
        ColumnButtonText ??= Localizer[nameof(ColumnButtonText)];
        ExportButtonText ??= Localizer[nameof(ExportButtonText)];
        SearchPlaceholderText ??= Localizer[nameof(SearchPlaceholderText)];
        SearchButtonText ??= Localizer[nameof(SearchButtonText)];
        ResetSearchButtonText ??= Localizer[nameof(ResetSearchButtonText)];
        AdvanceButtonText ??= Localizer[nameof(AdvanceButtonText)];
        CheckboxDisplayText ??= Localizer[nameof(CheckboxDisplayText)];
        EditModalTitle ??= Localizer[nameof(EditModalTitle)];
        AddModalTitle ??= Localizer[nameof(AddModalTitle)];
        ColumnButtonTemplateHeaderText ??= Localizer[nameof(ColumnButtonTemplateHeaderText)];
        SearchTooltip ??= Localizer[nameof(SearchTooltip)];
        SearchModalTitle ??= Localizer[nameof(SearchModalTitle)];
        AddButtonToastTitle ??= Localizer[nameof(AddButtonToastTitle)];
        AddButtonToastContent ??= Localizer[nameof(AddButtonToastContent)];
        EditButtonToastTitle ??= Localizer[nameof(EditButtonToastTitle)];
        EditButtonToastNotSelectContent ??= Localizer[nameof(EditButtonToastNotSelectContent)];
        EditButtonToastMoreSelectContent ??= Localizer[nameof(EditButtonToastMoreSelectContent)];
        EditButtonToastNoSaveMethodContent ??= Localizer[nameof(EditButtonToastNoSaveMethodContent)];
        EditButtonToastReadonlyContent ??= Localizer[nameof(EditButtonToastReadonlyContent)];
        SaveButtonToastTitle ??= Localizer[nameof(SaveButtonToastTitle)];
        SaveButtonToastContent ??= Localizer[nameof(SaveButtonToastContent)];
        SaveButtonToastResultContent ??= Localizer[nameof(SaveButtonToastResultContent)];
        SuccessText ??= Localizer[nameof(SuccessText)];
        FailText ??= Localizer[nameof(FailText)];
        DeleteButtonToastTitle ??= Localizer[nameof(DeleteButtonToastTitle)];
        DeleteButtonToastContent ??= Localizer[nameof(DeleteButtonToastContent)];
        DeleteButtonToastResultContent ??= Localizer[nameof(DeleteButtonToastResultContent)];
        DeleteButtonToastCanNotDeleteContent ??= Localizer[nameof(DeleteButtonToastCanNotDeleteContent)];
        DataServiceInvalidOperationText ??= Localizer[nameof(DataServiceInvalidOperationText), typeof(TItem).FullName!];
        NotSetOnTreeExpandErrorMessage = Localizer[nameof(NotSetOnTreeExpandErrorMessage)];
        UnsetText ??= Localizer[nameof(UnsetText)];
        SortAscText ??= Localizer[nameof(SortAscText)];
        SortDescText ??= Localizer[nameof(SortDescText)];
        EmptyText ??= Localizer[nameof(EmptyText)];
        LineNoText ??= Localizer[nameof(LineNoText)];
        ExportToastTitle ??= Localizer[nameof(ExportToastTitle)];
        ExportToastContent ??= Localizer[nameof(ExportToastContent)];
        ExportToastInProgressContent ??= Localizer[nameof(ExportToastInProgressContent)];
        ExportCsvDropdownItemText ??= Localizer[nameof(ExportCsvDropdownItemText)];
        ExportExcelDropdownItemText ??= Localizer[nameof(ExportExcelDropdownItemText)];
        ExportPdfDropdownItemText ??= Localizer[nameof(ExportPdfDropdownItemText)];
        CopyColumnTooltipText ??= Localizer[nameof(CopyColumnTooltipText)];
        CopyColumnCopiedTooltipText ??= Localizer[nameof(CopyColumnCopiedTooltipText)];

        AdvancedSortButtonText ??= Localizer[nameof(AdvancedSortButtonText)];
        AdvancedSortModalTitle ??= Localizer[nameof(AdvancedSortModalTitle)];

        ColumnWidthTooltipPrefix ??= Localizer[nameof(ColumnWidthTooltipPrefix)];

        AlignLeftText ??= Localizer[nameof(AlignLeftText)];
        AlignLeftTooltipText ??= Localizer[nameof(AlignLeftTooltipText)];
        AlignCenterText ??= Localizer[nameof(AlignCenterText)];
        AlignCenterTooltipText ??= Localizer[nameof(AlignCenterTooltipText)];
        AlignRightText ??= Localizer[nameof(AlignRightText)];
        AlignRightTooltipText ??= Localizer[nameof(AlignRightTooltipText)];
    }
}
