// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

internal static class BootstrapIcons
{
    public static Dictionary<ComponentIcons, string> Icons => new()
    {
        { ComponentIcons.AnchorLinkIcon, "bi bi-link-45deg" },
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

        { ComponentIcons.DatePickBodyPreviousYearIcon, "bi bi-chevron-double-left" },
        { ComponentIcons.DatePickBodyPreviousMonthIcon, "bi bi-chevron-left" },
        { ComponentIcons.DatePickBodyNextMonthIcon, "bi bi-chevron-right" },
        { ComponentIcons.DatePickBodyNextYearIcon, "bi bi-chevron-double-right" },
        { ComponentIcons.DateTimePickerIcon, "bi bi-calendar" },

        { ComponentIcons.TimePickerCellUpIcon, "bi bi-chevron-up" },
        { ComponentIcons.TimePickerCellDownIcon, "bi bi-chevron-down" },

        { ComponentIcons.DateTimeRangeIcon, "bi bi-calendar-range" },
        { ComponentIcons.DateTimeRangeClearIcon, "bi bi-x-circle" },

        { ComponentIcons.DialogCloseButtonIcon, "bi bi-x" },
        { ComponentIcons.DialogSaveButtonIcon, "bi bi-check" },
        { ComponentIcons.DialogMaximizeWindowIcon, "bi bi-window" },
        { ComponentIcons.DialogRestoreWindowIcon, "bi bi-window-stack" },

        { ComponentIcons.TopMenuDropdownIcon, "bi bi-chevron-down" },
        { ComponentIcons.SideMenuDropdownIcon, "bi bi-chevron-down" },
        { ComponentIcons.MenuLinkArrowIcon, "bi bi-chevron-left" },

        { ComponentIcons.ResultDialogYesIcon, "bi bi-check" },
        { ComponentIcons.ResultDialogNoIcon, "bi bi-x" },
        { ComponentIcons.ResultDialogCloseIcon, "bi bi-x" },

        { ComponentIcons.SearchDialogClearIcon, "bi bi-trash3" },
        { ComponentIcons.SearchDialogSearchIcon, "bi bi-search" },

        { ComponentIcons.StepIcon, "bi bi-check" },
        { ComponentIcons.StepErrorIcon, "bi bi-x" },

        { ComponentIcons.FilterButtonFilterIcon, "bi bi-funnel-fill" },
        { ComponentIcons.FilterButtonClearIcon, "bi bi-funnel" },

        { ComponentIcons.TableFilterPlusIcon, "bi bi-plus" },
        { ComponentIcons.TableFilterMinusIcon, "bi bi-dash" },

        { ComponentIcons.FullScreenButtonIcon, "bi bi-arrows-fullscreen" },
        { ComponentIcons.FullScreenExitButtonIcon, "bi bi-fullscreen-exit" },

        { ComponentIcons.GoTopIcon, "bi bi-chevron-up" },

        { ComponentIcons.ImagePreviewPreviousIcon, "bi bi-chevron-left" },
        { ComponentIcons.ImagePreviewNextIcon, "bi bi-chevron-right" },
        { ComponentIcons.ImagePreviewMinusIcon, "bi bi-search" },
        { ComponentIcons.ImagePreviewPlusIcon, "bi bi-search" },
        { ComponentIcons.ImagePreviewRotateLeftIcon, "bi bi-arrow-counterclockwise" },
        { ComponentIcons.ImagePreviewRotateRightIcon, "bi bi-arrow-clockwise" },

        { ComponentIcons.ImageViewerFileIcon, "bi bi-file-image" },

        { ComponentIcons.InputClearIcon, "bi bi-x-circle" },

        { ComponentIcons.InputNumberMinusIcon, "bi bi-dash-circle" },
        { ComponentIcons.InputNumberPlusIcon, "bi bi-plus-circle" },

        { ComponentIcons.LayoutMenuBarIcon, "bi bi-list" },
        { ComponentIcons.TabContextMenuRefreshIcon, "bi bi-arrow-clockwise" },
        { ComponentIcons.TabContextMenuCloseIcon, "bi bi-x" },
        { ComponentIcons.TabContextMenuCloseOtherIcon, "bi bi-arrow" },
        { ComponentIcons.TabContextMenuCloseAllIcon, "bi bi-arrow-left-right" },
        { ComponentIcons.TabContextMenuFullScreenIcon, "bi bi-arrows-fullscreen" },

        { ComponentIcons.LogoutLinkIcon, "bi bi-box-arrow-right" },

        { ComponentIcons.LoadingIcon, "bi bi-arrow-clockwise bi-spin" },

        { ComponentIcons.PaginationPrevPageIcon, "bi bi-chevron-left" },
        { ComponentIcons.PaginationNextPageIcon, "bi bi-chevron-right" },
        { ComponentIcons.PaginationPrevEllipsisPageIcon, "bi bi-three-dots" },
        { ComponentIcons.PaginationNextEllipsisPageIcon, "bi bi-three-dots" },

        { ComponentIcons.PopConfirmButtonConfirmIcon, "bi bi-exclamation-circle-fill" },
        { ComponentIcons.PopConfirmButtonContentCloseButtonIcon, "bi bi-x" },
        { ComponentIcons.PopConfirmButtonContentConfirmButtonIcon, "bi bi-check" },

        { ComponentIcons.RateStarIcon, "bi bi-star-fill" },
        { ComponentIcons.RateUnStarIcon, "bi bi-star" },

        { ComponentIcons.RibbonTabArrowUpIcon, "bi bi-chevron-up" },
        { ComponentIcons.RibbonTabArrowDownIcon, "bi bi-chevron-down" },
        { ComponentIcons.RibbonTabArrowPinIcon, "bi bi-pin bi-pin-angle" },

        { ComponentIcons.MultiSelectDropdownIcon, "bi bi-chevron-up" },
        { ComponentIcons.MultiSelectCloseIcon, "bi bi-x" },
        { ComponentIcons.MultiSelectClearIcon, "bi bi-x-circle" },

        { ComponentIcons.SelectTreeDropdownIcon, "bi bi-chevron-up" },

        { ComponentIcons.SearchClearButtonIcon, "bi bi-trash3" },
        { ComponentIcons.SearchButtonIcon, "bi bi-search" },
        { ComponentIcons.SearchButtonLoadingIcon, "bi bi-arrow-clockwise bi-spin" },

        { ComponentIcons.SelectDropdownIcon, "bi bi-chevron-up" },
        { ComponentIcons.SelectClearIcon, "bi bi-x-circle" },
        { ComponentIcons.SelectSearchIcon, "bi bi-search" },

        { ComponentIcons.SweetAlertCloseIcon, "bi bi-x" },
        { ComponentIcons.SweetAlertConfirmIcon, "bi bi-check" },

        { ComponentIcons.PrintButtonIcon, "bi bi-printer" },

        { ComponentIcons.ToastSuccessIcon, "bi bi-check-circle" },
        { ComponentIcons.ToastInformationIcon, "bi bi-exclamation-circle" },
        { ComponentIcons.ToastWarningIcon, "bi bi-exclamation-triangle" },
        { ComponentIcons.ToastErrorIcon, "bi bi-x-circle" },

        { ComponentIcons.TabPreviousIcon, "bi bi-chevron-left" },
        { ComponentIcons.TabNextIcon, "bi bi-chevron-right" },
        { ComponentIcons.TabDropdownIcon, "bi bi-chevron-down" },
        { ComponentIcons.TabCloseIcon, "bi bi-x" },
        { ComponentIcons.TabRefreshButtonIcon, "bi bi-arrow-clockwise" },

        { ComponentIcons.TableColumnToolboxIcon, "bi bi-gear" },

        { ComponentIcons.TableSortIconAsc, "bi bi-sort-up-alt" },
        { ComponentIcons.TableSortDesc, "bi bi-sort-down" },
        { ComponentIcons.TableSortIcon, "bi bi-filter" },
        { ComponentIcons.TableFilterIcon, "bi bi-funnel" },
        { ComponentIcons.TableExportButtonIcon, "bi bi-download" },

        { ComponentIcons.TableAddButtonIcon, "bi bi-plus" },
        { ComponentIcons.TableEditButtonIcon, "bi bi-check2-square" },
        { ComponentIcons.TableDeleteButtonIcon, "bi bi-x" },
        { ComponentIcons.TableRefreshButtonIcon, "bi bi-arrow-clockwise" },
        { ComponentIcons.TableCardViewButtonIcon, "bi bi-list" },
        { ComponentIcons.TableColumnListButtonIcon, "bi bi-list-check" },
        { ComponentIcons.TableExportCsvIcon, "bi bi-filetype-csv" },
        { ComponentIcons.TableExportExcelIcon, "bi bi-filetype-xlsx" },
        { ComponentIcons.TableExportPdfIcon, "bi bi-filetype-pdf" },
        { ComponentIcons.TableSearchButtonIcon, "bi bi-search" },
        { ComponentIcons.TableResetSearchButtonIcon, "bi bi-trash3" },
        { ComponentIcons.TableCloseButtonIcon, "bi bi-x" },
        { ComponentIcons.TableCancelButtonIcon, "bi bi-x" },
        { ComponentIcons.TableSaveButtonIcon, "bi bi-check" },
        { ComponentIcons.TableAdvanceButtonIcon, "bi bi-search" },
        { ComponentIcons.TableAdvancedSortButtonIcon, "bi bi-sort-alpha-up" },

        { ComponentIcons.TableTreeIcon, "bi bi-caret-right-fill" },
        { ComponentIcons.TableTreeExpandIcon, "bi bi-caret-right-fill bi-rotate-90" },
        { ComponentIcons.TableTreeNodeLoadingIcon, "bi bi-arrow-clockwise bi-spin" },
        { ComponentIcons.TableCopyColumnButtonIcon, "bi bi-clipboard" },
        { ComponentIcons.TableGearIcon, "bi bi-gear" },

        { ComponentIcons.TransferLeftIcon, "bi bi-chevron-left" },
        { ComponentIcons.TransferRightIcon, "bi bi-chevron-right" },
        { ComponentIcons.TransferPanelSearchIcon, "bi bi-search" },

        { ComponentIcons.TimerIcon, "bi bi-bell" },

        { ComponentIcons.TreeViewExpandNodeIcon, "bi bi-caret-right-fill bi-rotate-90" },
        { ComponentIcons.TreeViewNodeIcon, "bi bi-caret-right-fill" },
        { ComponentIcons.TreeViewSearchIcon, "bi bi-search" },
        { ComponentIcons.TreeViewResetSearchIcon, "bi bi-backspace" },
        { ComponentIcons.TreeViewLoadingIcon, "bi bi-arrow-clockwise bi-spin" },
        { ComponentIcons.TreeViewToolbarEditButton, "bi bi-check2-square" },

        // Upload
        { ComponentIcons.AvatarUploadDeleteIcon, "bi bi-trash3" },
        { ComponentIcons.AvatarUploadLoadingIcon, "bi bi-arrow-clockwise bi-spin" },
        { ComponentIcons.AvatarUploadAddIcon, "bi bi-plus" },
        { ComponentIcons.AvatarUploadValidStatusIcon, "bi bi-check bi-rotate-315" },
        { ComponentIcons.AvatarUploadInvalidStatusIcon, "bi bi-x bi-rotate-315" },

        { ComponentIcons.ButtonUploadBrowserButtonIcon, "bi bi-folder2-open" },
        { ComponentIcons.ButtonUploadLoadingIcon, "bi bi-arrow-clockwise bi-spin" },
        { ComponentIcons.ButtonUploadInvalidStatusIcon, "bi bi-x-circle" },
        { ComponentIcons.ButtonUploadValidStatusIcon, "bi bi-check-circle" },
        { ComponentIcons.ButtonUploadDeleteIcon, "bi bi-trash3" },
        { ComponentIcons.ButtonUploadDownloadIcon, "bi bi-cloud-download" },

        { ComponentIcons.InputUploadBrowserButtonIcon, "bi bi-folder-open" },
        { ComponentIcons.InputUploadDeleteButtonIcon, "bi bi-trash3" },

        { ComponentIcons.CardUploadAddIcon, "bi bi-plus" },
        { ComponentIcons.CardUploadStatusIcon, "bi bi-check bi-rotate-315" },
        { ComponentIcons.CardUploadDeleteIcon, "bi bi-x bi-rotate-315" },
        { ComponentIcons.CardUploadRemoveIcon, "bi bi-trash3" },
        { ComponentIcons.CardUploadDownloadIcon, "bi bi-cloud-download" },
        { ComponentIcons.CardUploadZoomIcon, "bi bi-search" },
        { ComponentIcons.UploadCancelIcon, "bi bi-cancel" },
        { ComponentIcons.DropUploadIcon, "bi bi-cloud-arrow-up-fill" },

        { ComponentIcons.FileIconExcel, "bi bi-filetype-xlsx" },
        { ComponentIcons.FileIconDocx, "bi bi-filetype-docx" },
        { ComponentIcons.FileIconPPT, "bi bi-filetype-pptx" },
        { ComponentIcons.FileIconAudio, "bi bi-filetype-mp4" },
        { ComponentIcons.FileIconVideo, "bi bi-filetype-mov" },
        { ComponentIcons.FileIconCode, "bi bi-filetype-cs" },
        { ComponentIcons.FileIconPdf, "bi bi-filetype-pdf" },
        { ComponentIcons.FileIconZip, "bi bi-filetype-raw" },
        { ComponentIcons.FileIconArchive, "bi bi-filetype-aac" },
        { ComponentIcons.FileIconImage, "bi bi-filetype-png" },
        { ComponentIcons.FileIconFile, "bi bi-filetype-txt" },

        { ComponentIcons.QueryBuilderPlusIcon, "bi bi-plus" },
        { ComponentIcons.QueryBuilderMinusIcon, "bi bi-dash" },
        { ComponentIcons.QueryBuilderRemoveIcon, "bi bi-x" },

        { ComponentIcons.ThemeProviderAutoModeIcon, "bi bi-circle-half" },
        { ComponentIcons.ThemeProviderLightModeIcon, "bi bi-sun-fill" },
        { ComponentIcons.ThemeProviderDarkModeIcon, "bi bi-moon-stars-fill" },
        { ComponentIcons.ThemeProviderActiveModeIcon, "bi bi-check2" }
    };
}
