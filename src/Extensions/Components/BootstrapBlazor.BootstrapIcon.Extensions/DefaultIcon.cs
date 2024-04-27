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
        { ComponentIcons.CameraPlayIcon, "bi bi-circle-play" },
        { ComponentIcons.CameraStopIcon, "bi bi-circle-stop" },
        { ComponentIcons.CameraPhotoIcon, "bi bi-camera" },
        { ComponentIcons.CardCollapseIcon, "bi bi-arrow-right-circle-fill" },
        { ComponentIcons.CarouselPreviousIcon, "bi bi-chevron-left" },
        { ComponentIcons.CarouselNextIcon, "bi bi-chevron-right" },
        { ComponentIcons.CascaderIcon, "bi bi-chevron-up" },
        { ComponentIcons.CascaderSubMenuIcon, "bi bi-chevron-down" },
        { ComponentIcons.ConsoleClearButtonIcon, "bi bi-x" },

        // DateTimePicker 组件
        { ComponentIcons.DatePickBodyPreviousYearIcon, "bi bi-chevron-double-left" },
        { ComponentIcons.DatePickBodyPreviousMonthIcon, "bi bi-chevron-left" },
        { ComponentIcons.DatePickBodyNextMonthIcon, "bi bi-chevron-right" },
        { ComponentIcons.DatePickBodyNextYearIcon, "bi bi-chevron-double-right" },
        { ComponentIcons.DateTimePickerIcon, "bi bi-calendar" },
        { ComponentIcons.TimePickerCellUpIcon, "bi bi-chevron-up" },
        { ComponentIcons.TimePickerCellDownIcon, "bi bi-chevron-down" },

        // DateTimeRange 组件
        { ComponentIcons.DateTimeRangeIcon, "bi bi-calendar-range" },
        { ComponentIcons.DateTimeRangeClearIcon, "bi bi-x-circle" },

        { ComponentIcons.DialogCloseButtonIcon, "bi bi-x" },
        { ComponentIcons.DialogSaveButtonIcon, "bi bi-content-save" },
        { ComponentIcons.DialogMaximizeWindowIcon, "bi bi-window-maximize" },
        { ComponentIcons.DialogRestoreWindowIcon, "bi bi-window-restore" },

        { ComponentIcons.TopMenuDropdownIcon, "bi bi-chevron-down" },
        { ComponentIcons.SideMenuDropdownIcon, "bi bi-chevron-down" },
        { ComponentIcons.MenuLinkArrowIcon, "bi bi-chevron-left" },

        { ComponentIcons.ResultDialogYesIcon, "bi bi-check" },
        { ComponentIcons.ResultDialogNoIcon, "bi bi-x-circle" },
        { ComponentIcons.ResultDialogCloseIcon, "bi bi-x-circle" },

        { ComponentIcons.SearchDialogClearIcon, "bi bi-trash-can" },
        { ComponentIcons.SearchDialogSearchIcon, "bi bi-magnify-plus" },

        { ComponentIcons.StepIcon, "bi bi-check" },
        { ComponentIcons.StepErrorIcon, "bi bi-x" },

        { ComponentIcons.FilterButtonFilterIcon, "bi bi-filter" },
        { ComponentIcons.FilterButtonClearIcon, "bi bi-filter-remove" },

        { ComponentIcons.TableFilterPlusIcon, "bi bi-plus" },
        { ComponentIcons.TableFilterMinusIcon, "bi bi-minus" },

        { ComponentIcons.FullScreenButtonIcon, "bi bi-fullscreen" },

        { ComponentIcons.GoTopIcon, "bi bi-chevron-up" },

        { ComponentIcons.ImagePreviewPreviousIcon, "bi bi-chevron-left" },
        { ComponentIcons.ImagePreviewNextIcon, "bi bi-chevron-right" },
        { ComponentIcons.ImagePreviewMinusIcon, "bi bi-magnify-minus" },
        { ComponentIcons.ImagePreviewPlusIcon, "bi bi-magnify-plus" },
        { ComponentIcons.ImagePreviewRotateLeftIcon, "bi bi-file-rotate-left" },
        { ComponentIcons.ImagePreviewRotateRightIcon, "bi bi-file-rotate-right" },

        { ComponentIcons.ImageViewerFileIcon, "bi bi-file-image" },

        { ComponentIcons.InputNumberMinusIcon, "bi bi-minus-circle" },
        { ComponentIcons.InputNumberPlusIcon, "bi bi-plus-circle" },

        { ComponentIcons.LayoutMenuBarIcon, "bi bi-menu" },
        { ComponentIcons.LogoutLinkIcon, "bi bi-logout" },

        { ComponentIcons.LoadingIcon, "bi bi-loading bi-spin" },

        { ComponentIcons.PaginationPrevPageIcon, "bi bi-chevron-left" },
        { ComponentIcons.PaginationNextPageIcon, "bi bi-chevron-right" },
        { ComponentIcons.PaginationPrevEllipsisPageIcon, "bi bi-dots-horizontal" },
        { ComponentIcons.PaginationNextEllipsisPageIcon, "bi bi-dots-horizontal" },

        { ComponentIcons.PopConfirmButtonConfirmIcon, "bi bi-exclamation" },

        { ComponentIcons.RateStarIcon, "bi bi-star" },
        { ComponentIcons.RateUnStarIcon, "bi bi-star" },

        { ComponentIcons.RibbonTabArrowUpIcon, "bi bi-chevron-up" },
        { ComponentIcons.RibbonTabArrowDownIcon, "bi bi-chevron-down" },
        { ComponentIcons.RibbonTabArrowPinIcon, "bi bi-pin bi-rotate-90" },

        { ComponentIcons.MultiSelectClearIcon, "bi bi-x" },

        { ComponentIcons.SelectTreeDropdownIcon, "bi bi-chevron-up" },

        { ComponentIcons.SearchClearButtonIcon, "bi bi-trash-can" },
        { ComponentIcons.SearchButtonIcon, "bi bi-magnify-plus" },
        { ComponentIcons.SearchButtonLoadingIcon, "bi bi-loading bi-spin" },

        { ComponentIcons.SelectDropdownIcon, "bi bi-chevron-up" },
        { ComponentIcons.SelectClearIcon, "bi bi-x-circle" },
        { ComponentIcons.SelectSearchIcon, "bi bi-magnify-plus" },

        { ComponentIcons.SweetAlertCloseIcon, "bi bi-x" },
        { ComponentIcons.SweetAlertConfirmIcon, "bi bi-check" },

        { ComponentIcons.PrintButtonIcon, "bi bi-printer" },

        { ComponentIcons.ToastSuccessIcon, "bi bi-check-circle" },
        { ComponentIcons.ToastInformationIcon, "bi bi-alert-circle" },
        { ComponentIcons.ToastWarningIcon, "bi bi-alert" },
        { ComponentIcons.ToastErrorIcon, "bi bi-x-circle" },

        { ComponentIcons.TabPreviousIcon, "bi bi-chevron-left" },
        { ComponentIcons.TabNextIcon, "bi bi-chevron-right" },
        { ComponentIcons.TabDropdownIcon, "bi bi-chevron-down" },
        { ComponentIcons.TabCloseIcon, "bi bi-x" },

        { ComponentIcons.TableSortIconAsc, "bi bi-sort-ascending" },
        { ComponentIcons.TableSortDesc, "bi bi-sort-descending" },
        { ComponentIcons.TableSortIcon, "bi bi-sort" },
        { ComponentIcons.TableFilterIcon, "bi bi-filter" },
        { ComponentIcons.TableExportButtonIcon, "bi bi-download" },

        { ComponentIcons.TableAddButtonIcon, "bi bi-plus" },
        { ComponentIcons.TableEditButtonIcon, "bi bi-file-edit" },
        { ComponentIcons.TableDeleteButtonIcon, "bi bi-x" },
        { ComponentIcons.TableRefreshButtonIcon, "bi bi-arrow-clockwise" },
        { ComponentIcons.TableCardViewButtonIcon, "bi bi-menu" },
        { ComponentIcons.TableColumnListButtonIcon, "bi bi-format-list-bulleted" },
        { ComponentIcons.TableExportCsvIcon, "bi bi-file-table-box" },
        { ComponentIcons.TableExportExcelIcon, "bi bi-file-excel-box" },
        { ComponentIcons.TableExportPdfIcon, "bi bi-file-pdf-box" },
        { ComponentIcons.TableSearchButtonIcon, "bi bi-magnify" },
        { ComponentIcons.TableResetSearchButtonIcon, "bi bi-trash-can" },
        { ComponentIcons.TableCloseButtonIcon, "bi bi-x" },
        { ComponentIcons.TableCancelButtonIcon, "bi bi-x" },
        { ComponentIcons.TableSaveButtonIcon, "bi bi-content-save" },
        { ComponentIcons.TableAdvanceButtonIcon, "bi bi-magnify-plus" },
        { ComponentIcons.TableAdvancedSortButtonIcon, "bi bi-sort-alphabetical-descending" },

        { ComponentIcons.TableTreeIcon, "bi bi-menu-right" },
        { ComponentIcons.TableTreeExpandIcon, "bi bi-menu-right bi-rotate-90" },
        { ComponentIcons.TableTreeNodeLoadingIcon, "bi bi-loading bi-spin" },
        { ComponentIcons.TableCopyColumnButtonIcon, "bi bi-clipboard" },
        { ComponentIcons.TableGearIcon, "bi bi-cog" },

        { ComponentIcons.TransferLeftIcon, "bi bi-chevron-left" },
        { ComponentIcons.TransferRightIcon, "bi bi-chevron-right" },
        { ComponentIcons.TransferPanelSearchIcon, "bi bi-magnify-plus" },

        { ComponentIcons.TimerIcon, "bi bi-bell" },

        { ComponentIcons.TreeViewExpandNodeIcon, "bi bi-menu-right bi-rotate-90" },
        { ComponentIcons.TreeViewNodeIcon, "bi bi-menu-right" },
        { ComponentIcons.TreeViewSearchIcon, "bi bi-magnify" },
        { ComponentIcons.TreeViewResetSearchIcon, "bi bi-trash-can" },

        // Upload
        { ComponentIcons.AvatarUploadDeleteIcon, "bi bi-trash-can" },
        { ComponentIcons.AvatarUploadLoadingIcon, "bi bi-loading bi-spin" },
        { ComponentIcons.AvatarUploadAddIcon, "bi bi-plus" },
        { ComponentIcons.AvatarUploadValidStatusIcon, "bi bi-check bi-rotate-315" },
        { ComponentIcons.AvatarUploadInvalidStatusIcon, "bi bi-x bi-rotate-315" },

        { ComponentIcons.ButtonUploadBrowserButtonIcon, "bi bi-folder-open" },
        { ComponentIcons.ButtonUploadLoadingIcon, "bi bi-loading bi-spin" },
        { ComponentIcons.ButtonUploadInvalidStatusIcon, "bi bi-x-circle" },
        { ComponentIcons.ButtonUploadValidStatusIcon, "bi bi-check-circle" },
        { ComponentIcons.ButtonUploadDeleteIcon, "bi bi-trash-can" },
        { ComponentIcons.ButtonUploadDownloadIcon, "bi bi-cloud-download" },

        { ComponentIcons.InputUploadBrowserButtonIcon, "bi bi-folder-open" },
        { ComponentIcons.InputUploadDeleteButtonIcon, "bi bi-trash-can" },

        { ComponentIcons.CardUploadAddIcon, "bi bi-plus" },
        { ComponentIcons.CardUploadStatusIcon, "bi bi-check bi-rotate-315" },
        { ComponentIcons.CardUploadDeleteIcon, "bi bi-x bi-rotate-315" },
        { ComponentIcons.CardUploadRemoveIcon, "bi bi-trash-can" },
        { ComponentIcons.CardUploadDownloadIcon, "bi bi-cloud-download" },
        { ComponentIcons.CardUploadZoomIcon, "bi bi-magnify-plus" },
        { ComponentIcons.UploadCancelIcon, "bi bi-cancel" },

        { ComponentIcons.FileIconExcel, "bi bi-file-excel" },
        { ComponentIcons.FileIconDocx, "bi bi-file-word" },
        { ComponentIcons.FileIconPPT, "bi bi-file-powerpoint" },
        { ComponentIcons.FileIconAudio, "bi bi-file-music" },
        { ComponentIcons.FileIconVideo, "bi bi-file-video" },
        { ComponentIcons.FileIconCode, "bi bi-file-code" },
        { ComponentIcons.FileIconPdf, "bi bi-file-pdf-box" },
        { ComponentIcons.FileIconZip, "bi bi-file-cog" },
        { ComponentIcons.FileIconArchive, "bi bi-file-document-check" },
        { ComponentIcons.FileIconImage, "bi bi-file-image" },
        { ComponentIcons.FileIconFile, "bi bi-file" },

        { ComponentIcons.QueryBuilderPlusIcon, "bi bi-plus" },
        { ComponentIcons.QueryBuilderMinusIcon, "bi bi-minus" },
        { ComponentIcons.QueryBuilderRemoveIcon, "bi bi-x" }
    };
}
