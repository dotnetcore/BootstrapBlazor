﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

internal static class MaterialDesignIcons
{
    public static Dictionary<ComponentIcons, string> Icons => new()
    {
        { ComponentIcons.AnchorLinkIcon, "mdi mdi-link-variant" },
        { ComponentIcons.AvatarIcon, "mdi mdi-account" },
        { ComponentIcons.AutoFillIcon, "mdi mdi-chevron-up" },
        { ComponentIcons.AutoCompleteIcon, "mdi mdi-chevron-up" },

        { ComponentIcons.ButtonLoadingIcon, "mdi mdi-loading mdi-spin" },

        { ComponentIcons.CaptchaRefreshIcon, "mdi mdi-refresh" },
        { ComponentIcons.CaptchaBarIcon, "mdi mdi-chevron-right" },
        { ComponentIcons.CameraPlayIcon, "mdi mdi-play-circle-outline" },
        { ComponentIcons.CameraStopIcon, "mdi mdi-stop-circle-outline" },
        { ComponentIcons.CameraPhotoIcon, "mdi mdi-camera-outline" },

        { ComponentIcons.CardCollapseIcon, "mdi mdi-chevron-right-circle" },
        { ComponentIcons.CarouselPreviousIcon, "mdi mdi-chevron-left" },
        { ComponentIcons.CarouselNextIcon, "mdi mdi-chevron-right" },
        { ComponentIcons.CascaderIcon, "mdi mdi-chevron-up" },
        { ComponentIcons.CascaderSubMenuIcon, "mdi mdi-chevron-down" },
        { ComponentIcons.ConsoleClearButtonIcon, "mdi mdi-close" },

        { ComponentIcons.DatePickBodyPreviousYearIcon, "mdi mdi-chevron-double-left" },
        { ComponentIcons.DatePickBodyPreviousMonthIcon, "mdi mdi-chevron-left" },
        { ComponentIcons.DatePickBodyNextMonthIcon, "mdi mdi-chevron-right" },
        { ComponentIcons.DatePickBodyNextYearIcon, "mdi mdi-chevron-double-right" },
        { ComponentIcons.DateTimePickerIcon, "mdi mdi-calendar-outline" },

        { ComponentIcons.TimePickerCellUpIcon, "mdi mdi-chevron-up" },
        { ComponentIcons.TimePickerCellDownIcon, "mdi mdi-chevron-down" },

        { ComponentIcons.DateTimeRangeIcon, "mdi mdi-calendar-range-outline" },
        { ComponentIcons.DateTimeRangeClearIcon, "mdi mdi-close-circle-outline" },

        { ComponentIcons.DialogCloseButtonIcon, "mdi mdi-close" },
        { ComponentIcons.DialogSaveButtonIcon, "mdi mdi-content-save" },
        { ComponentIcons.DialogMaximizeWindowIcon, "mdi mdi-window-maximize" },
        { ComponentIcons.DialogRestoreWindowIcon, "mdi mdi-window-restore" },

        { ComponentIcons.TopMenuDropdownIcon, "mdi mdi-chevron-down" },
        { ComponentIcons.SideMenuDropdownIcon, "mdi mdi-chevron-down" },
        { ComponentIcons.MenuLinkArrowIcon, "mdi mdi-chevron-left" },

        { ComponentIcons.ResultDialogYesIcon, "mdi mdi-check" },
        { ComponentIcons.ResultDialogNoIcon, "mdi mdi-close-circle-outline" },
        { ComponentIcons.ResultDialogCloseIcon, "mdi mdi-close-circle-outline" },

        { ComponentIcons.SearchDialogClearIcon, "mdi mdi-trash-can-outline" },
        { ComponentIcons.SearchDialogSearchIcon, "mdi mdi-magnify-plus-outline" },

        { ComponentIcons.StepIcon, "mdi mdi-check" },
        { ComponentIcons.StepErrorIcon, "mdi mdi-close" },

        { ComponentIcons.FilterButtonFilterIcon, "mdi mdi-filter-outline" },
        { ComponentIcons.FilterButtonClearIcon, "mdi mdi-filter-remove-outline" },

        { ComponentIcons.TableFilterPlusIcon, "mdi mdi-plus" },
        { ComponentIcons.TableFilterMinusIcon, "mdi mdi-minus" },

        { ComponentIcons.FullScreenButtonIcon, "mdi mdi-arrow-expand-all" },
        { ComponentIcons.FullScreenExitButtonIcon, "mdi mdi-fullscreen-exit" },

        { ComponentIcons.GoTopIcon, "mdi mdi-chevron-up" },

        { ComponentIcons.ImagePreviewPreviousIcon, "mdi mdi-chevron-left" },
        { ComponentIcons.ImagePreviewNextIcon, "mdi mdi-chevron-right" },
        { ComponentIcons.ImagePreviewMinusIcon, "mdi mdi-magnify-minus-outline" },
        { ComponentIcons.ImagePreviewPlusIcon, "mdi mdi-magnify-plus-outline" },
        { ComponentIcons.ImagePreviewRotateLeftIcon, "mdi mdi-restore" },
        { ComponentIcons.ImagePreviewRotateRightIcon, "mdi mdi-reload" },

        { ComponentIcons.ImageViewerFileIcon, "mdi mdi-file-image-outline" },

        { ComponentIcons.InputClearIcon, "mdi mdi-close-circle-outline" },

        { ComponentIcons.InputNumberMinusIcon, "mdi mdi-minus-circle-outline" },
        { ComponentIcons.InputNumberPlusIcon, "mdi mdi-plus-circle-outline" },

        { ComponentIcons.LayoutMenuBarIcon, "mdi mdi-menu" },
        { ComponentIcons.TabContextMenuRefreshIcon, "mdi mdi-refresh" },
        { ComponentIcons.TabContextMenuCloseIcon, "mdi mdi-close" },
        { ComponentIcons.TabContextMenuCloseOtherIcon, "mdi mdi-menu" },
        { ComponentIcons.TabContextMenuCloseAllIcon, "mdi mdi-arrow-left-right-bold" },
        { ComponentIcons.TabContextMenuFullScreenIcon, "mdi mdi-arrow-expand-all" },

        { ComponentIcons.LogoutLinkIcon, "mdi mdi-logout" },

        { ComponentIcons.LoadingIcon, "mdi mdi-loading mdi-spin" },

        { ComponentIcons.PaginationPrevPageIcon, "mdi mdi-chevron-left" },
        { ComponentIcons.PaginationNextPageIcon, "mdi mdi-chevron-right" },
        { ComponentIcons.PaginationPrevEllipsisPageIcon, "mdi mdi-dots-horizontal" },
        { ComponentIcons.PaginationNextEllipsisPageIcon, "mdi mdi-dots-horizontal" },

        { ComponentIcons.PopConfirmButtonConfirmIcon, "mdi mdi-alert-circle" },
        { ComponentIcons.PopConfirmButtonContentCloseButtonIcon, "mdi mdi-close" },
        { ComponentIcons.PopConfirmButtonContentConfirmButtonIcon, "mdi mdi-check" },

        { ComponentIcons.RateStarIcon, "mdi mdi-star" },
        { ComponentIcons.RateUnStarIcon, "mdi mdi-star-outline" },

        { ComponentIcons.RibbonTabArrowUpIcon, "mdi mdi-chevron-up" },
        { ComponentIcons.RibbonTabArrowDownIcon, "mdi mdi-chevron-down" },
        { ComponentIcons.RibbonTabArrowPinIcon, "mdi mdi-pin mdi-pin-off" },

        { ComponentIcons.MultiSelectDropdownIcon, "mdi mdi-chevron-up" },
        { ComponentIcons.MultiSelectCloseIcon, "mdi mdi-close" },
        { ComponentIcons.MultiSelectClearIcon, "mdi mdi-close-circle-outline" },

        { ComponentIcons.SelectTreeDropdownIcon, "mdi mdi-chevron-up" },

        { ComponentIcons.SearchClearButtonIcon, "mdi mdi-trash-can-outline" },
        { ComponentIcons.SearchButtonIcon, "mdi mdi-magnify-plus-outline" },
        { ComponentIcons.SearchButtonLoadingIcon, "mdi mdi-loading mdi-spin" },

        { ComponentIcons.SelectDropdownIcon, "mdi mdi-chevron-up" },
        { ComponentIcons.SelectClearIcon, "mdi mdi-close-circle-outline" },
        { ComponentIcons.SelectSearchIcon, "mdi mdi-magnify-plus-outline" },

        { ComponentIcons.SweetAlertCloseIcon, "mdi mdi-close" },
        { ComponentIcons.SweetAlertConfirmIcon, "mdi mdi-check" },

        { ComponentIcons.PrintButtonIcon, "mdi mdi-printer-outline" },

        { ComponentIcons.ToastSuccessIcon, "mdi mdi-check-circle" },
        { ComponentIcons.ToastInformationIcon, "mdi mdi-alert-circle" },
        { ComponentIcons.ToastWarningIcon, "mdi mdi-alert" },
        { ComponentIcons.ToastErrorIcon, "mdi mdi-close-circle" },

        { ComponentIcons.TabPreviousIcon, "mdi mdi-chevron-left" },
        { ComponentIcons.TabNextIcon, "mdi mdi-chevron-right" },
        { ComponentIcons.TabDropdownIcon, "mdi mdi-chevron-down" },
        { ComponentIcons.TabCloseIcon, "mdi mdi-close" },
        { ComponentIcons.TabRefreshButtonIcon, "mdi mdi-reload" },

        { ComponentIcons.TableColumnToolboxIcon, "mdi mdi-cog" },

        { ComponentIcons.TableSortIconAsc, "mdi mdi-sort-ascending" },
        { ComponentIcons.TableSortDesc, "mdi mdi-sort-descending" },
        { ComponentIcons.TableSortIcon, "mdi mdi-sort" },
        { ComponentIcons.TableFilterIcon, "mdi mdi-filter" },
        { ComponentIcons.TableExportButtonIcon, "mdi mdi-download" },

        { ComponentIcons.TableAddButtonIcon, "mdi mdi-plus" },
        { ComponentIcons.TableEditButtonIcon, "mdi mdi-file-edit-outline" },
        { ComponentIcons.TableDeleteButtonIcon, "mdi mdi-close" },
        { ComponentIcons.TableRefreshButtonIcon, "mdi mdi-refresh" },
        { ComponentIcons.TableCardViewButtonIcon, "mdi mdi-menu" },
        { ComponentIcons.TableColumnListButtonIcon, "mdi mdi-format-list-bulleted" },
        { ComponentIcons.TableExportCsvIcon, "mdi mdi-file-table-box" },
        { ComponentIcons.TableExportExcelIcon, "mdi mdi-file-excel-box" },
        { ComponentIcons.TableExportPdfIcon, "mdi mdi-file-pdf-box" },
        { ComponentIcons.TableSearchButtonIcon, "mdi mdi-magnify" },
        { ComponentIcons.TableResetSearchButtonIcon, "mdi mdi-trash-can-outline" },
        { ComponentIcons.TableCloseButtonIcon, "mdi mdi-close" },
        { ComponentIcons.TableCancelButtonIcon, "mdi mdi-close" },
        { ComponentIcons.TableSaveButtonIcon, "mdi mdi-content-save" },
        { ComponentIcons.TableAdvanceButtonIcon, "mdi mdi-magnify-plus-outline" },
        { ComponentIcons.TableAdvancedSortButtonIcon, "mdi mdi-sort-alphabetical-descending" },

        { ComponentIcons.TableTreeIcon, "mdi mdi-menu-right" },
        { ComponentIcons.TableTreeExpandIcon, "mdi mdi-menu-right mdi-rotate-90" },
        { ComponentIcons.TableTreeNodeLoadingIcon, "mdi mdi-loading mdi-spin" },
        { ComponentIcons.TableCopyColumnButtonIcon, "mdi mdi-clipboard-outline" },
        { ComponentIcons.TableGearIcon, "mdi mdi-cog" },

        { ComponentIcons.TransferLeftIcon, "mdi mdi-chevron-left" },
        { ComponentIcons.TransferRightIcon, "mdi mdi-chevron-right" },
        { ComponentIcons.TransferPanelSearchIcon, "mdi mdi-magnify-plus-outline" },

        { ComponentIcons.TimerIcon, "mdi mdi-bell-outline" },

        { ComponentIcons.TreeViewExpandNodeIcon, "mdi mdi-menu-right mdi-rotate-90" },
        { ComponentIcons.TreeViewNodeIcon, "mdi mdi-menu-right" },
        { ComponentIcons.TreeViewSearchIcon, "mdi mdi-magnify" },
        { ComponentIcons.TreeViewResetSearchIcon, "mdi mdi-backspace" },
        { ComponentIcons.TreeViewLoadingIcon, "mdi mdi-loading mdi-spin" },
        { ComponentIcons.TreeViewToolbarEditButton, "mdi mdi-file-edit-outline" },

        // Upload
        { ComponentIcons.AvatarUploadDeleteIcon, "mdi mdi-trash-can-outline" },
        { ComponentIcons.AvatarUploadLoadingIcon, "mdi mdi-loading mdi-spin" },
        { ComponentIcons.AvatarUploadAddIcon, "mdi mdi-plus" },
        { ComponentIcons.AvatarUploadValidStatusIcon, "mdi mdi-check mdi-rotate-315" },
        { ComponentIcons.AvatarUploadInvalidStatusIcon, "mdi mdi-close mdi-rotate-315" },

        { ComponentIcons.ButtonUploadBrowserButtonIcon, "mdi mdi-folder-open" },

        { ComponentIcons.InputUploadBrowserButtonIcon, "mdi mdi-folder-open" },
        { ComponentIcons.InputUploadDeleteButtonIcon, "mdi mdi-trash-can-outline" },

        { ComponentIcons.CardUploadAddIcon, "mdi mdi-plus" },
        { ComponentIcons.CardUploadStatusIcon, "mdi mdi-check mdi-rotate-315" },
        { ComponentIcons.CardUploadRemoveIcon, "mdi mdi-trash-can-outline" },
        { ComponentIcons.CardUploadZoomIcon, "mdi mdi-magnify-plus-outline" },
        { ComponentIcons.UploadCancelIcon, "mdi mdi-cancel" },
        { ComponentIcons.DropUploadIcon, "mdi mdi-cloud-upload" },
        { ComponentIcons.UploadLoadingIcon, "mdi mdi-loading mdi-spin" },
        { ComponentIcons.UploadInvalidStatusIcon, "mdi mdi-close-circle-outline" },
        { ComponentIcons.UploadValidStatusIcon, "mdi mdi-check-circle-outline" },
        { ComponentIcons.UploadDeleteIcon, "mdi mdi-close mdi-rotate-315" },
        { ComponentIcons.UploadDownloadIcon, "mdi mdi-cloud-download-outline" },

        { ComponentIcons.FileIconExcel, "mdi mdi-file-excel-outline" },
        { ComponentIcons.FileIconDocx, "mdi mdi-file-word-outline" },
        { ComponentIcons.FileIconPPT, "mdi mdi-file-powerpoint-outline" },
        { ComponentIcons.FileIconAudio, "mdi mdi-file-music-outline" },
        { ComponentIcons.FileIconVideo, "mdi mdi-file-video-outline" },
        { ComponentIcons.FileIconCode, "mdi mdi-file-code-outline" },
        { ComponentIcons.FileIconPdf, "mdi mdi-file-pdf-box" },
        { ComponentIcons.FileIconZip, "mdi mdi-file-cog-outline" },
        { ComponentIcons.FileIconArchive, "mdi mdi-file-document-check-outline" },
        { ComponentIcons.FileIconImage, "mdi mdi-file-image-outline" },
        { ComponentIcons.FileIconFile, "mdi mdi-file-outline" },

        { ComponentIcons.QueryBuilderPlusIcon, "mdi mdi-plus" },
        { ComponentIcons.QueryBuilderMinusIcon, "mdi mdi-minus" },
        { ComponentIcons.QueryBuilderRemoveIcon, "mdi mdi-close" },

        { ComponentIcons.ThemeProviderAutoModeIcon, "mdi mdi-circle-half-full" },
        { ComponentIcons.ThemeProviderLightModeIcon, "mdi mdi-white-balance-sunny" },
        { ComponentIcons.ThemeProviderDarkModeIcon, "mdi mdi-weather-night" },
        { ComponentIcons.ThemeProviderActiveModeIcon, "mdi mdi-check" }
  };
}
