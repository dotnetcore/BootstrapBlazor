// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

internal class DefaultIcon
{
    public static Dictionary<ComponentIcons, string> Icons => new()
    {
        // AnchorLink 组件
        { ComponentIcons.AnchorLinkIcon, "mdi mdi-link-variant" },

        // Avatar 组件
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

        // DateTimePicker 组件
        { ComponentIcons.DatePickBodyPreviousYearIcon, "mdi mdi-chevron-double-left" },
        { ComponentIcons.DatePickBodyPreviousMonthIcon, "mdi mdi-chevron-left" },
        { ComponentIcons.DatePickBodyNextMonthIcon, "mdi mdi-chevron-right" },
        { ComponentIcons.DatePickBodyNextYearIcon, "mdi mdi-chevron-double-right" },
        { ComponentIcons.DateTimePickerIcon, "mdi mdi-calendar-outline" },
        { ComponentIcons.TimePickerCellUpIcon, "mdi mdi-chevron-up" },
        { ComponentIcons.TimePickerCellDownIcon, "mdi mdi-chevron-down" },

        // DateTimeRange 组件
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
        { ComponentIcons.ImagePreviewRotateLeftIcon, "mdi mdi-file-rotate-left-outline" },
        { ComponentIcons.ImagePreviewRotateRightIcon, "mdi mdi-file-rotate-right-outline" },

        { ComponentIcons.ImageViewerFileIcon, "mdi mdi-file-image-outline" },

        { ComponentIcons.InputNumberMinusIcon, "mdi mdi-minus-circle-outline" },
        { ComponentIcons.InputNumberPlusIcon, "mdi mdi-plus-circle-outline" },

        { ComponentIcons.LayoutMenuBarIcon, "mdi mdi-menu" },
        { ComponentIcons.LogoutLinkIcon, "mdi mdi-logout" },

        { ComponentIcons.LoadingIcon, "mdi mdi-loading mdi-spin" },

        { ComponentIcons.PaginationPrevPageIcon, "mdi mdi-chevron-left" },
        { ComponentIcons.PaginationNextPageIcon, "mdi mdi-chevron-right" },
        { ComponentIcons.PaginationPrevEllipsisPageIcon, "mdi mdi-dots-horizontal" },
        { ComponentIcons.PaginationNextEllipsisPageIcon, "mdi mdi-dots-horizontal" },

        { ComponentIcons.PopConfirmButtonConfirmIcon, "mdi mdi-exclamation" },

        { ComponentIcons.RateStarIcon, "mdi mdi-star" },
        { ComponentIcons.RateUnStarIcon, "mdi mdi-star-outline" },

        { ComponentIcons.RibbonTabArrowUpIcon, "mdi mdi-chevron-up" },
        { ComponentIcons.RibbonTabArrowDownIcon, "mdi mdi-chevron-down" },
        { ComponentIcons.RibbonTabArrowPinIcon, "mdi mdi-pin mdi-rotate-90" },

        { ComponentIcons.MultiSelectClearIcon, "mdi mdi-close" },

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
        { ComponentIcons.TreeViewResetSearchIcon, "mdi mdi-trash-can-outline" },

        // Upload
        { ComponentIcons.AvatarUploadDeleteIcon, "mdi mdi-trash-can-outline" },
        { ComponentIcons.AvatarUploadLoadingIcon, "mdi mdi-loading mdi-spin" },
        { ComponentIcons.AvatarUploadAddIcon, "mdi mdi-plus" },
        { ComponentIcons.AvatarUploadValidStatusIcon, "mdi mdi-check mdi-rotate-315" },
        { ComponentIcons.AvatarUploadInvalidStatusIcon, "mdi mdi-close mdi-rotate-315" },

        { ComponentIcons.ButtonUploadBrowserButtonIcon, "mdi mdi-folder-open" },
        { ComponentIcons.ButtonUploadLoadingIcon, "mdi mdi-loading mdi-spin" },
        { ComponentIcons.ButtonUploadInvalidStatusIcon, "mdi mdi-close-circle-outline" },
        { ComponentIcons.ButtonUploadValidStatusIcon, "mdi mdi-check-circle-outline" },
        { ComponentIcons.ButtonUploadDeleteIcon, "mdi mdi-trash-can-outline" },
        { ComponentIcons.ButtonUploadDownloadIcon, "mdi mdi-cloud-download-outline" },

        { ComponentIcons.InputUploadBrowserButtonIcon, "mdi mdi-folder-open" },
        { ComponentIcons.InputUploadDeleteButtonIcon, "mdi mdi-trash-can-outline" },

        { ComponentIcons.CardUploadAddIcon, "mdi mdi-plus" },
        { ComponentIcons.CardUploadStatusIcon, "mdi mdi-check mdi-rotate-315" },
        { ComponentIcons.CardUploadDeleteIcon, "mdi mdi-close mdi-rotate-315" },
        { ComponentIcons.CardUploadRemoveIcon, "mdi mdi-trash-can-outline" },
        { ComponentIcons.CardUploadDownloadIcon, "mdi mdi-cloud-download-outline" },
        { ComponentIcons.CardUploadZoomIcon, "mdi mdi-magnify-plus-outline" },
        { ComponentIcons.UploadCancelIcon, "mdi mdi-cancel" },

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
