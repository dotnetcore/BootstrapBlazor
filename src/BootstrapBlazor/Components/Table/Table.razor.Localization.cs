// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using System.Diagnostics.CodeAnalysis;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TItem"></typeparam>
    public partial class Table<TItem>
    {
        /// <summary>
        /// 新建按钮文本
        /// </summary>
        [Parameter]
        [NotNull]
        public string? AddButtonText { get; set; }

        /// <summary>
        /// 编辑按钮文本
        /// </summary>
        [Parameter]
        [NotNull]
        public string? EditButtonText { get; set; }

        /// <summary>
        /// 删除按钮文本
        /// </summary>
        [Parameter]
        [NotNull]
        public string? DeleteButtonText { get; set; }

        /// <summary>
        /// 取消删除按钮文本
        /// </summary>
        [Parameter]
        [NotNull]
        public string? CancelDeleteButtonText { get; set; }

        /// <summary>
        /// 保存按钮文本
        /// </summary>
        [Parameter]
        [NotNull]
        public string? SaveButtonText { get; set; }

        /// <summary>
        /// 确认删除按钮文本
        /// </summary>
        [Parameter]
        [NotNull]
        public string? ConfirmDeleteButtonText { get; set; }

        /// <summary>
        /// 确认删除弹窗文本
        /// </summary>
        [Parameter]
        [NotNull]
        public string? ConfirmDeleteContentText { get; set; }

        /// <summary>
        /// 刷新按钮文本
        /// </summary>
        [Parameter]
        [NotNull]
        public string? RefreshButtonText { get; set; }

        /// <summary>
        /// 列显示隐藏按钮提示信息文本
        /// </summary>
        [Parameter]
        [NotNull]
        public string? ColumnButtonTitleText { get; set; }

        /// <summary>
        /// 列按钮文本
        /// </summary>
        [Parameter]
        [NotNull]
        public string? ColumnButtonText { get; set; }

        /// <summary>
        /// 导出按钮文本
        /// </summary>
        [Parameter]
        [NotNull]
        public string? ExportButtonText { get; set; }

        /// <summary>
        /// 搜索栏 Placeholder 文本
        /// </summary>
        [Parameter]
        [NotNull]
        public string? SearchPlaceholderText { get; set; }

        /// <summary>
        /// 搜索按钮文本
        /// </summary>
        [Parameter]
        [NotNull]
        public string? SearchButtonText { get; set; }

        /// <summary>
        /// 搜索按钮文本
        /// </summary>
        [Parameter]
        [NotNull]
        public string? SearchModalTitle { get; set; }

        /// <summary>
        /// 获得/设置 搜索框提示文字
        /// </summary>
        [Parameter]
        [NotNull]
        public string? SearchTooltip { get; set; }

        /// <summary>
        /// 重置按钮文本
        /// </summary>
        [Parameter]
        [NotNull]
        public string? ResetButtonText { get; set; }

        /// <summary>
        /// 高级搜索按钮文本
        /// </summary>
        [Parameter]
        [NotNull]
        public string? AdvanceButtonText { get; set; }

        /// <summary>
        /// 新增按钮 Toast 提示 Title 文字
        /// </summary>
        [Parameter]
        [NotNull]
        public string? AddButtonToastTitle { get; set; }

        /// <summary>
        /// 新增按钮 Toast 提示 Content 文字
        /// </summary>
        [Parameter]
        [NotNull]
        public string? AddButtonToastContent { get; set; }

        /// <summary>
        /// 编辑按钮 Toast 提示 Title 文字
        /// </summary>
        [Parameter]
        [NotNull]
        public string? EditButtonToastTitle { get; set; }

        /// <summary>
        /// 编辑按钮 Toast 未选择时提示 Content 文字
        /// </summary>
        [Parameter]
        [NotNull]
        public string? EditButtonToastNotSelectContent { get; set; }

        /// <summary>
        /// 编辑按钮 Toast 多项选择时提示 Content 文字
        /// </summary>
        [Parameter]
        [NotNull]
        public string? EditButtonToastMoreSelectContent { get; set; }

        /// <summary>
        /// 编辑按钮 Toast 未提供 Save 方法时提示 Content 文字
        /// </summary>
        [Parameter]
        [NotNull]
        public string? EditButtonToastNoSaveMethodContent { get; set; }

        /// <summary>
        /// 保存按钮 Toast 提示 Title 文字
        /// </summary>
        [Parameter]
        [NotNull]
        public string? SaveButtonToastTitle { get; set; }

        /// <summary>
        /// 保存按钮 Toast 提示 Content 文字
        /// </summary>
        [Parameter]
        [NotNull]
        public string? SaveButtonToastContent { get; set; }

        /// <summary>
        /// 保存按钮结果 Toast 提示 Content 文字
        /// </summary>
        [Parameter]
        [NotNull]
        public string? SaveButtonToastResultContent { get; set; }

        /// <summary>
        /// 保存成功文字
        /// </summary>
        [Parameter]
        [NotNull]
        public string? SuccessText { get; set; }

        /// <summary>
        /// 保存失败
        /// </summary>
        [Parameter]
        [NotNull]
        public string? FailText { get; set; }

        /// <summary>
        /// 删除按钮 Toast 提示 Title 文字
        /// </summary>
        [Parameter]
        [NotNull]
        public string? DeleteButtonToastTitle { get; set; }

        /// <summary>
        /// 删除按钮 Toast 提示 Content 文字
        /// </summary>
        [Parameter]
        [NotNull]
        public string? DeleteButtonToastContent { get; set; }

        /// <summary>
        /// 删除按钮结果 Toast 提示 Content 文字
        /// </summary>
        [Parameter]
        [NotNull]
        public string? DeleteButtonToastResultContent { get; set; }

        [Inject]
        [NotNull]
        private IStringLocalizer<Table<TItem>>? Localizer { get; set; }

        private void OnInitLocalization()
        {
            AddButtonText ??= Localizer[nameof(AddButtonText)];
            EditButtonText ??= Localizer[nameof(EditButtonText)];
            DeleteButtonText ??= Localizer[nameof(DeleteButtonText)];
            SaveButtonText ??= Localizer[nameof(SaveButtonText)];
            CancelDeleteButtonText ??= Localizer[nameof(CancelDeleteButtonText)];
            ConfirmDeleteButtonText ??= Localizer[nameof(ConfirmDeleteButtonText)];
            ConfirmDeleteContentText ??= Localizer[nameof(ConfirmDeleteContentText)];
            RefreshButtonText ??= Localizer[nameof(RefreshButtonText)];
            ColumnButtonTitleText ??= Localizer[nameof(ColumnButtonTitleText)];
            ColumnButtonText ??= Localizer[nameof(ColumnButtonText)];
            ExportButtonText ??= Localizer[nameof(ExportButtonText)];
            SearchPlaceholderText ??= Localizer[nameof(SearchPlaceholderText)];
            SearchButtonText ??= Localizer[nameof(SearchButtonText)];
            ResetButtonText ??= Localizer[nameof(ResetButtonText)];
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
            SaveButtonToastTitle ??= Localizer[nameof(SaveButtonToastTitle)];
            SaveButtonToastContent ??= Localizer[nameof(SaveButtonToastContent)];
            SaveButtonToastResultContent ??= Localizer[nameof(SaveButtonToastResultContent)];
            SuccessText ??= Localizer[nameof(SuccessText)];
            FailText ??= Localizer[nameof(FailText)];
            DeleteButtonToastTitle ??= Localizer[nameof(DeleteButtonToastTitle)];
            DeleteButtonToastContent ??= Localizer[nameof(DeleteButtonToastContent)];
            DeleteButtonToastResultContent ??= Localizer[nameof(DeleteButtonToastResultContent)];
            DataServiceInvalidOperationText ??= Localizer[nameof(DataServiceInvalidOperationText), typeof(TItem).FullName!];
        }
    }
}
