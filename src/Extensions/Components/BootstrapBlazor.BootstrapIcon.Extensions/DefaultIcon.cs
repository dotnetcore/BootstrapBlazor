// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

internal class DefaultIcon
{
    public static Dictionary<ComponentIcons, string> Icons => new()
    {
        // AnchorLink 组件
        { ComponentIcons.AnchorLinkIcon, "bi bi-link-45deg" },

        // Avatar 组件
        { ComponentIcons.AvatarIcon, "bi bi-person-fill" },
        { ComponentIcons.AutoFillIcon, "bi bi-chevron-up" },
        { ComponentIcons.AutoCompleteIcon, "bi bi-chevron-up" },

        { ComponentIcons.ButtonLoadingIcon, "bi bi-arrow-clockwise bi-spin" },

        { ComponentIcons.CaptchaRefreshIcon, "bi bi-arrow-clockwise" },
        { ComponentIcons.CaptchaBarIcon, "bi bi-chevron-right" },
        { ComponentIcons.CameraPlayIcon, "bi bi-play-circle-outline" },
        { ComponentIcons.CameraStopIcon, "bi bi-stop-circle-outline" },
        { ComponentIcons.CameraPhotoIcon, "bi bi-camera-outline" },
        { ComponentIcons.CardCollapseIcon, "bi bi-chevron-right-circle" },
        { ComponentIcons.CarouselPreviousIcon, "bi bi-chevron-left" },
        { ComponentIcons.CarouselNextIcon, "bi bi-chevron-right" },
        { ComponentIcons.CascaderIcon, "bi bi-chevron-up" },
        { ComponentIcons.CascaderSubMenuIcon, "bi bi-chevron-down" },
        { ComponentIcons.ConsoleClearButtonIcon, "bi bi-close" },

        // DateTimePicker 组件
        { ComponentIcons.DatePickBodyPreviousYearIcon, "bi bi-chevron-double-left" },
        { ComponentIcons.DatePickBodyPreviousMonthIcon, "bi bi-chevron-left" },
        { ComponentIcons.DatePickBodyNextMonthIcon, "bi bi-chevron-right" },
        { ComponentIcons.DatePickBodyNextYearIcon, "bi bi-chevron-double-right" },
        { ComponentIcons.DateTimePickerIcon, "bi bi-calendar-outline" },
        { ComponentIcons.TimePickerCellUpIcon, "bi bi-chevron-up" },
        { ComponentIcons.TimePickerCellDownIcon, "bi bi-chevron-down" },

        // DateTimeRange 组件
        { ComponentIcons.DateTimeRangeIcon, "bi bi-calendar-range-outline" },
        { ComponentIcons.DateTimeRangeClearIcon, "bi bi-close-circle-outline" },

        { ComponentIcons.DialogCloseButtonIcon, "bi bi-close" },
        { ComponentIcons.DialogSaveButtonIcon, "bi bi-content-save" },
        { ComponentIcons.DialogMaximizeWindowIcon, "bi bi-window-maximize" },
        { ComponentIcons.DialogRestoreWindowIcon, "bi bi-window-restore" },

        { ComponentIcons.TopMenuDropdownIcon, "bi bi-chevron-down" },
        { ComponentIcons.SideMenuDropdownIcon, "bi bi-chevron-down" },
        { ComponentIcons.MenuLinkArrowIcon, "bi bi-chevron-left" },

        { ComponentIcons.ResultDialogYesIcon, "bi bi-check" },
        { ComponentIcons.ResultDialogNoIcon, "bi bi-close-circle-outline" },
        { ComponentIcons.ResultDialogCloseIcon, "bi bi-close-circle-outline" },

        { ComponentIcons.SearchDialogClearIcon, "bi bi-trash-can-outline" },
        { ComponentIcons.SearchDialogSearchIcon, "bi bi-magnify-plus-outline" },

        { ComponentIcons.StepIcon, "bi bi-check" },
        { ComponentIcons.StepErrorIcon, "bi bi-close" },

        { ComponentIcons.FilterButtonFilterIcon, "bi bi-filter-outline" },
        { ComponentIcons.FilterButtonClearIcon, "bi bi-filter-remove-outline" },

        { ComponentIcons.TableFilterPlusIcon, "bi bi-plus" },
        { ComponentIcons.TableFilterMinusIcon, "bi bi-minus" },

        { ComponentIcons.FullScreenButtonIcon, "bi bi-fullscreen" },

        { ComponentIcons.GoTopIcon, "bi bi-chevron-up" },

        { ComponentIcons.ImagePreviewPreviousIcon, "bi bi-chevron-left" },
        { ComponentIcons.ImagePreviewNextIcon, "bi bi-chevron-right" },
        { ComponentIcons.ImagePreviewMinusIcon, "bi bi-magnify-minus-outline" },
        { ComponentIcons.ImagePreviewPlusIcon, "bi bi-magnify-plus-outline" },
        { ComponentIcons.ImagePreviewRotateLeftIcon, "bi bi-file-rotate-left-outline" },
        { ComponentIcons.ImagePreviewRotateRightIcon, "bi bi-file-rotate-right-outline" },

        { ComponentIcons.ImageViewerFileIcon, "bi bi-file-image-outline" },

        { ComponentIcons.InputNumberMinusIcon, "bi bi-minus-circle-outline" },
        { ComponentIcons.InputNumberPlusIcon, "bi bi-plus-circle-outline" },

        { ComponentIcons.LayoutMenuBarIcon, "bi bi-menu" },
        { ComponentIcons.LogoutLinkIcon, "bi bi-logout" },

        { ComponentIcons.LoadingIcon, "bi bi-loading bi-spin" },

        { ComponentIcons.PaginationPrevPageIcon, "bi bi-chevron-left" },
        { ComponentIcons.PaginationNextPageIcon, "bi bi-chevron-right" },
        { ComponentIcons.PaginationPrevEllipsisPageIcon, "bi bi-dots-horizontal" },
        { ComponentIcons.PaginationNextEllipsisPageIcon, "bi bi-dots-horizontal" },

        { ComponentIcons.PopConfirmButtonConfirmIcon, "bi bi-exclamation" },

        { ComponentIcons.RateStarIcon, "bi bi-star" },
        { ComponentIcons.RateUnStarIcon, "bi bi-star-outline" },

        { ComponentIcons.RibbonTabArrowUpIcon, "bi bi-chevron-up" },
        { ComponentIcons.RibbonTabArrowDownIcon, "bi bi-chevron-down" },
        { ComponentIcons.RibbonTabArrowPinIcon, "bi bi-pin bi-rotate-90" },

        { ComponentIcons.MultiSelectClearIcon, "bi bi-close" },

        { ComponentIcons.SelectTreeDropdownIcon, "bi bi-chevron-up" },

        { ComponentIcons.SearchClearButtonIcon, "bi bi-trash-can-outline" },
        { ComponentIcons.SearchButtonIcon, "bi bi-magnify-plus-outline" },
        { ComponentIcons.SearchButtonLoadingIcon, "bi bi-loading bi-spin" },

        { ComponentIcons.SelectDropdownIcon, "bi bi-chevron-up" },
        { ComponentIcons.SelectClearIcon, "bi bi-close-circle-outline" },
        { ComponentIcons.SelectSearchIcon, "bi bi-magnify-plus-outline" },

        { ComponentIcons.SweetAlertCloseIcon, "bi bi-close" },
        { ComponentIcons.SweetAlertConfirmIcon, "bi bi-check" },

        { ComponentIcons.PrintButtonIcon, "bi bi-printer-outline" },

        { ComponentIcons.ToastSuccessIcon, "bi bi-check-circle" },
        { ComponentIcons.ToastInformationIcon, "bi bi-alert-circle" },
        { ComponentIcons.ToastWarningIcon, "bi bi-alert" },
        { ComponentIcons.ToastErrorIcon, "bi bi-close-circle" },

        { ComponentIcons.TabPreviousIcon, "bi bi-chevron-left" },
        { ComponentIcons.TabNextIcon, "bi bi-chevron-right" },
        { ComponentIcons.TabDropdownIcon, "bi bi-chevron-down" },
        { ComponentIcons.TabCloseIcon, "bi bi-close" },

        { ComponentIcons.TableSortIconAsc, "bi bi-sort-ascending" },
        { ComponentIcons.TableSortDesc, "bi bi-sort-descending" },
        { ComponentIcons.TableSortIcon, "bi bi-sort" },
        { ComponentIcons.TableFilterIcon, "bi bi-filter" },
        { ComponentIcons.TableExportButtonIcon, "bi bi-download" },

        { ComponentIcons.TableAddButtonIcon, "bi bi-plus" },
        { ComponentIcons.TableEditButtonIcon, "bi bi-file-edit-outline" },
        { ComponentIcons.TableDeleteButtonIcon, "bi bi-close" },
        { ComponentIcons.TableRefreshButtonIcon, "bi bi-arrow-clockwise" },
        { ComponentIcons.TableCardViewButtonIcon, "bi bi-menu" },
        { ComponentIcons.TableColumnListButtonIcon, "bi bi-format-list-bulleted" },
        { ComponentIcons.TableExportCsvIcon, "bi bi-file-table-box" },
        { ComponentIcons.TableExportExcelIcon, "bi bi-file-excel-box" },
        { ComponentIcons.TableExportPdfIcon, "bi bi-file-pdf-box" },
        { ComponentIcons.TableSearchButtonIcon, "bi bi-magnify" },
        { ComponentIcons.TableResetSearchButtonIcon, "bi bi-trash-can-outline" },
        { ComponentIcons.TableCloseButtonIcon, "bi bi-close" },
        { ComponentIcons.TableCancelButtonIcon, "bi bi-close" },
        { ComponentIcons.TableSaveButtonIcon, "bi bi-content-save" },
        { ComponentIcons.TableAdvanceButtonIcon, "bi bi-magnify-plus-outline" },
        { ComponentIcons.TableAdvancedSortButtonIcon, "bi bi-sort-alphabetical-descending" },

        { ComponentIcons.TableTreeIcon, "bi bi-menu-right" },
        { ComponentIcons.TableTreeExpandIcon, "bi bi-menu-right bi-rotate-90" },
        { ComponentIcons.TableTreeNodeLoadingIcon, "bi bi-loading bi-spin" },
        { ComponentIcons.TableCopyColumnButtonIcon, "bi bi-clipboard-outline" },
        { ComponentIcons.TableGearIcon, "bi bi-cog" },

        { ComponentIcons.TransferLeftIcon, "bi bi-chevron-left" },
        { ComponentIcons.TransferRightIcon, "bi bi-chevron-right" },
        { ComponentIcons.TransferPanelSearchIcon, "bi bi-magnify-plus-outline" },

        { ComponentIcons.TimerIcon, "bi bi-bell-outline" },

        { ComponentIcons.TreeViewExpandNodeIcon, "bi bi-menu-right bi-rotate-90" },
        { ComponentIcons.TreeViewNodeIcon, "bi bi-menu-right" },
        { ComponentIcons.TreeViewSearchIcon, "bi bi-magnify" },
        { ComponentIcons.TreeViewResetSearchIcon, "bi bi-trash-can-outline" },

        // Upload
        { ComponentIcons.AvatarUploadDeleteIcon, "bi bi-trash-can-outline" },
        { ComponentIcons.AvatarUploadLoadingIcon, "bi bi-loading bi-spin" },
        { ComponentIcons.AvatarUploadAddIcon, "bi bi-plus" },
        { ComponentIcons.AvatarUploadValidStatusIcon, "bi bi-check bi-rotate-315" },
        { ComponentIcons.AvatarUploadInvalidStatusIcon, "bi bi-close bi-rotate-315" },

        { ComponentIcons.ButtonUploadBrowserButtonIcon, "bi bi-folder-open" },
        { ComponentIcons.ButtonUploadLoadingIcon, "bi bi-loading bi-spin" },
        { ComponentIcons.ButtonUploadInvalidStatusIcon, "bi bi-close-circle-outline" },
        { ComponentIcons.ButtonUploadValidStatusIcon, "bi bi-check-circle-outline" },
        { ComponentIcons.ButtonUploadDeleteIcon, "bi bi-trash-can-outline" },
        { ComponentIcons.ButtonUploadDownloadIcon, "bi bi-cloud-download-outline" },

        { ComponentIcons.InputUploadBrowserButtonIcon, "bi bi-folder-open" },
        { ComponentIcons.InputUploadDeleteButtonIcon, "bi bi-trash-can-outline" },

        { ComponentIcons.CardUploadAddIcon, "bi bi-plus" },
        { ComponentIcons.CardUploadStatusIcon, "bi bi-check bi-rotate-315" },
        { ComponentIcons.CardUploadDeleteIcon, "bi bi-close bi-rotate-315" },
        { ComponentIcons.CardUploadRemoveIcon, "bi bi-trash-can-outline" },
        { ComponentIcons.CardUploadDownloadIcon, "bi bi-cloud-download-outline" },
        { ComponentIcons.CardUploadZoomIcon, "bi bi-magnify-plus-outline" },
        { ComponentIcons.UploadCancelIcon, "bi bi-cancel" },

        { ComponentIcons.FileIconExcel, "bi bi-file-excel-outline" },
        { ComponentIcons.FileIconDocx, "bi bi-file-word-outline" },
        { ComponentIcons.FileIconPPT, "bi bi-file-powerpoint-outline" },
        { ComponentIcons.FileIconAudio, "bi bi-file-music-outline" },
        { ComponentIcons.FileIconVideo, "bi bi-file-video-outline" },
        { ComponentIcons.FileIconCode, "bi bi-file-code-outline" },
        { ComponentIcons.FileIconPdf, "bi bi-file-pdf-box" },
        { ComponentIcons.FileIconZip, "bi bi-file-cog-outline" },
        { ComponentIcons.FileIconArchive, "bi bi-file-document-check-outline" },
        { ComponentIcons.FileIconImage, "bi bi-file-image-outline" },
        { ComponentIcons.FileIconFile, "bi bi-file-outline" },

        { ComponentIcons.QueryBuilderPlusIcon, "bi bi-plus" },
        { ComponentIcons.QueryBuilderMinusIcon, "bi bi-minus" },
        { ComponentIcons.QueryBuilderRemoveIcon, "bi bi-close" }
    };
}
