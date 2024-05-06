// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// IconTheme 配置项
/// </summary>
public class IconThemeOptions
{
    /// <summary>
    /// 获得/设置 主题 Icons 集合
    /// </summary>
    public Dictionary<string, Dictionary<ComponentIcons, string>> Icons { get; private set; }

    /// <summary>
    /// 获得/设置 当前主题键值 默认 fa 使用 font-awesome 图标集
    /// </summary>
    public string ThemeKey { get; set; }

    /// <summary>
    /// 构造函数
    /// </summary>
    public IconThemeOptions()
    {
        Icons = new()
        {
            { "fa", FAIcons }
        };

        ThemeKey = "fa";
    }

    private Dictionary<ComponentIcons, string> FAIcons { get; } = new Dictionary<ComponentIcons, string>()
    {
        { ComponentIcons.AnchorLinkIcon, "fa-solid fa-link" },
        { ComponentIcons.AvatarIcon, "fa-solid fa-user" },
        { ComponentIcons.AutoFillIcon, "fa-solid fa-angle-up" },
        { ComponentIcons.AutoCompleteIcon, "fa-solid fa-angle-up" },

        { ComponentIcons.ButtonLoadingIcon, "fa-solid fa-spin fa-spinner" },

        { ComponentIcons.CaptchaRefreshIcon, "fa-solid fa-arrows-rotate" },
        { ComponentIcons.CaptchaBarIcon, "fa-solid fa-arrow-right" },
        { ComponentIcons.CameraPlayIcon, "fa-solid fa-circle-play" },
        { ComponentIcons.CameraStopIcon, "fa-solid fa-circle-stop" },
        { ComponentIcons.CameraPhotoIcon, "fa-solid fa-camera-retro" },
        { ComponentIcons.CardCollapseIcon, "fa-solid fa-circle-chevron-right" },
        { ComponentIcons.CarouselPreviousIcon, "fa-solid fa-angle-left" },
        { ComponentIcons.CarouselNextIcon, "fa-solid fa-angle-right" },
        { ComponentIcons.CascaderIcon, "fa-solid fa-angle-up" },
        { ComponentIcons.CascaderSubMenuIcon, "fa-solid fa-angle-down" },
        { ComponentIcons.ConsoleClearButtonIcon, "fa-solid fa-xmark" },

        { ComponentIcons.DatePickBodyPreviousYearIcon, "fa-solid fa-angles-left" },
        { ComponentIcons.DatePickBodyPreviousMonthIcon, "fa-solid fa-angle-left" },
        { ComponentIcons.DatePickBodyNextMonthIcon, "fa-solid fa-angle-right" },
        { ComponentIcons.DatePickBodyNextYearIcon, "fa-solid fa-angles-right" },

        { ComponentIcons.DateTimePickerIcon, "fa-regular fa-calendar-days" },

        { ComponentIcons.TimePickerCellUpIcon, "fa-solid fa-angle-up" },
        { ComponentIcons.TimePickerCellDownIcon, "fa-solid fa-angle-down" },

        { ComponentIcons.DateTimeRangeIcon, "fa-regular fa-calendar-days" },
        { ComponentIcons.DateTimeRangeClearIcon, "fa-solid fa-circle-xmark" },

        { ComponentIcons.DialogCloseButtonIcon, "fa-solid fa-xmark" },
        { ComponentIcons.DialogSaveButtonIcon, "fa-solid fa-floppy-disk" },
        { ComponentIcons.DialogMaximizeWindowIcon, "fa-regular fa-window-maximize" },
        { ComponentIcons.DialogRestoreWindowIcon, "fa-regular fa-window-restore" },

        { ComponentIcons.TopMenuDropdownIcon, "fa-solid fa-angle-down" },
        { ComponentIcons.SideMenuDropdownIcon, "fa-solid fa-angle-down" },
        { ComponentIcons.MenuLinkArrowIcon, "fa-solid fa-angle-left" },

        { ComponentIcons.ResultDialogYesIcon, "fa-solid fa-check" },
        { ComponentIcons.ResultDialogNoIcon, "fa-solid fa-xmark" },
        { ComponentIcons.ResultDialogCloseIcon, "fa-solid fa-xmark" },

        { ComponentIcons.SearchDialogClearIcon, "fa-regular fa-trash-can" },
        { ComponentIcons.SearchDialogSearchIcon, "fa-solid fa-magnifying-glass" },

        { ComponentIcons.StepIcon, "fa-solid fa-check" },
        { ComponentIcons.StepErrorIcon, "fa-solid fa-xmark" },

        { ComponentIcons.FilterButtonFilterIcon, "fa-solid fa-filter" },
        { ComponentIcons.FilterButtonClearIcon, "fa-solid fa-ban" },

        { ComponentIcons.TableFilterPlusIcon, "fa-solid fa-plus" },
        { ComponentIcons.TableFilterMinusIcon, "fa-solid fa-minus" },

        { ComponentIcons.FullScreenButtonIcon, "fa-solid fa-maximize" },
        { ComponentIcons.FullScreenExitButtonIcon, "fa-solid fa-minimize" },

        { ComponentIcons.GoTopIcon, "fa-solid fa-angle-up" },

        { ComponentIcons.ImagePreviewPreviousIcon, "fa-solid fa-angle-left" },
        { ComponentIcons.ImagePreviewNextIcon, "fa-solid fa-angle-right" },
        { ComponentIcons.ImagePreviewMinusIcon, "fa-solid fa-magnifying-glass-minus" },
        { ComponentIcons.ImagePreviewPlusIcon, "fa-solid fa-magnifying-glass-plus" },
        { ComponentIcons.ImagePreviewRotateLeftIcon, "fa-solid fa-rotate-left" },
        { ComponentIcons.ImagePreviewRotateRightIcon, "fa-solid fa-rotate-right" },

        { ComponentIcons.ImageViewerFileIcon, "fa-regular fa-file-image" },

        { ComponentIcons.InputNumberMinusIcon, "fa-solid fa-circle-minus" },
        { ComponentIcons.InputNumberPlusIcon, "fa-solid fa-circle-plus" },

        { ComponentIcons.LayoutMenuBarIcon, "fa-solid fa-bars" },
        { ComponentIcons.LogoutLinkIcon, "fa-solid fa-key" },

        { ComponentIcons.LoadingIcon, "fa-solid fa-fw fa-spin fa-spinner" },

        { ComponentIcons.PaginationPrevPageIcon, "fa-solid fa-angle-left" },
        { ComponentIcons.PaginationNextPageIcon, "fa-solid fa-angle-right" },
        { ComponentIcons.PaginationPrevEllipsisPageIcon, "fa-solid fa-ellipsis" },
        { ComponentIcons.PaginationNextEllipsisPageIcon, "fa-solid fa-ellipsis" },

        { ComponentIcons.PopConfirmButtonConfirmIcon, "fa-solid fa-circle-exclamation" },

        { ComponentIcons.RateStarIcon, "fa-solid fa-star" },
        { ComponentIcons.RateUnStarIcon, "fa-regular fa-star" },

        { ComponentIcons.RibbonTabArrowUpIcon, "fa-solid fa-angle-up" },
        { ComponentIcons.RibbonTabArrowDownIcon, "fa-solid fa-angle-down" },
        { ComponentIcons.RibbonTabArrowPinIcon, "fa-solid fa-thumbtack fa-rotate-90" },

        { ComponentIcons.MultiSelectClearIcon, "fa-solid fa-xmark" },

        { ComponentIcons.SelectTreeDropdownIcon, "fa-solid fa-angle-up" },

        { ComponentIcons.SearchClearButtonIcon, "fa-regular fa-trash-can" },
        { ComponentIcons.SearchButtonIcon, "fa-solid fa-magnifying-glass" },
        { ComponentIcons.SearchButtonLoadingIcon, "fa-solid fa-spin fa-spinner" },

        { ComponentIcons.SelectDropdownIcon, "fa-solid fa-angle-up" },
        { ComponentIcons.SelectClearIcon, "fa-regular fa-circle-xmark" },
        { ComponentIcons.SelectSearchIcon, "fa-solid fa-magnifying-glass" },

        { ComponentIcons.SweetAlertCloseIcon, "fa-solid fa-xmark" },
        { ComponentIcons.SweetAlertConfirmIcon, "fa-solid fa-check" },

        { ComponentIcons.PrintButtonIcon, "fa-solid fa-print" },

        { ComponentIcons.ToastSuccessIcon, "fa-solid fa-check-circle" },
        { ComponentIcons.ToastInformationIcon, "fa-solid fa-exclamation-circle" },
        { ComponentIcons.ToastWarningIcon, "fa-solid fa-exclamation-triangle" },
        { ComponentIcons.ToastErrorIcon, "fa-solid fa-xmark-circle" },

        { ComponentIcons.TabPreviousIcon, "fa-solid fa-chevron-left" },
        { ComponentIcons.TabNextIcon, "fa-solid fa-chevron-right" },
        { ComponentIcons.TabDropdownIcon, "fa-solid fa-chevron-down" },
        { ComponentIcons.TabCloseIcon, "fa-solid fa-xmark" },

        { ComponentIcons.TableSortIconAsc, "fa-solid fa-sort-up" },
        { ComponentIcons.TableSortDesc, "fa-solid fa-sort-down" },
        { ComponentIcons.TableSortIcon, "fa-solid fa-sort" },
        { ComponentIcons.TableFilterIcon, "fa-solid fa-filter" },
        { ComponentIcons.TableExportButtonIcon, "fa-solid fa-download" },

        { ComponentIcons.TableAddButtonIcon, "fa-solid fa-plus" },
        { ComponentIcons.TableEditButtonIcon, "fa-regular fa-pen-to-square" },
        { ComponentIcons.TableDeleteButtonIcon, "fa-solid fa-xmark" },
        { ComponentIcons.TableRefreshButtonIcon, "fa-solid fa-arrows-rotate" },
        { ComponentIcons.TableCardViewButtonIcon, "fa-solid fa-bars" },
        { ComponentIcons.TableColumnListButtonIcon, "fa-solid fa-table-list" },
        { ComponentIcons.TableExportCsvIcon, "fa-solid fa-fw fa-file-csv" },
        { ComponentIcons.TableExportExcelIcon, "fa-solid fa-fw fa-file-excel" },
        { ComponentIcons.TableExportPdfIcon, "fa-solid fa-fw fa-file-pdf" },
        { ComponentIcons.TableSearchButtonIcon, "fa-solid fa-magnifying-glass" },
        { ComponentIcons.TableResetSearchButtonIcon, "fa-regular fa-trash-can" },
        { ComponentIcons.TableCloseButtonIcon, "fa-solid fa-xmark" },
        { ComponentIcons.TableCancelButtonIcon, "fa-solid fa-xmark" },
        { ComponentIcons.TableSaveButtonIcon, "fa-solid fa-floppy-disk" },
        { ComponentIcons.TableAdvanceButtonIcon, "fa-solid fa-magnifying-glass-plus" },
        { ComponentIcons.TableAdvancedSortButtonIcon, "fa-solid fa-arrow-up-a-z" },

        { ComponentIcons.TableTreeIcon, "fa-solid fa-caret-right" },
        { ComponentIcons.TableTreeExpandIcon, "fa-solid fa-caret-right fa-rotate-90" },
        { ComponentIcons.TableTreeNodeLoadingIcon, "fa-solid fa-spin fa-spinner" },
        { ComponentIcons.TableCopyColumnButtonIcon, "fa-regular fa-clipboard" },
        { ComponentIcons.TableGearIcon, "fa-solid fa-gear" },

        { ComponentIcons.TransferLeftIcon, "fa-solid fa-angle-left" },
        { ComponentIcons.TransferRightIcon, "fa-solid fa-angle-right" },
        { ComponentIcons.TransferPanelSearchIcon, "fa-solid fa-magnifying-glass" },

        { ComponentIcons.TimerIcon, "fa-solid fa-bell" },

        { ComponentIcons.TreeViewExpandNodeIcon, "fa-solid fa-caret-right fa-rotate-90" },
        { ComponentIcons.TreeViewNodeIcon, "fa-solid fa-caret-right" },
        { ComponentIcons.TreeViewSearchIcon, "fa-solid fa-magnifying-glass" },
        { ComponentIcons.TreeViewResetSearchIcon, "fa-regular fa-trash-can" },

        // Upload
        { ComponentIcons.AvatarUploadDeleteIcon, "fa-regular fa-trash-can" },
        { ComponentIcons.AvatarUploadLoadingIcon, "fa-solid fa-spinner fa-spin" },
        { ComponentIcons.AvatarUploadAddIcon, "fa-solid fa-plus" },
        { ComponentIcons.AvatarUploadValidStatusIcon, "fa-solid fa-check" },
        { ComponentIcons.AvatarUploadInvalidStatusIcon, "fa-solid fa-xmark" },

        { ComponentIcons.ButtonUploadBrowserButtonIcon, "fa-regular fa-folder-open" },
        { ComponentIcons.ButtonUploadLoadingIcon, "fa-solid fa-spinner fa-spin" },
        { ComponentIcons.ButtonUploadInvalidStatusIcon, "fa-regular fa-circle-xmark" },
        { ComponentIcons.ButtonUploadValidStatusIcon, "fa-regular fa-circle-check" },
        { ComponentIcons.ButtonUploadDeleteIcon, "fa-regular fa-trash-can" },
        { ComponentIcons.ButtonUploadDownloadIcon, "fa-solid fa-download" },

        { ComponentIcons.InputUploadBrowserButtonIcon, "fa-regular fa-folder-open" },
        { ComponentIcons.InputUploadDeleteButtonIcon, "fa-regular fa-trash-can" },

        { ComponentIcons.CardUploadAddIcon, "fa-solid fa-plus" },
        { ComponentIcons.CardUploadStatusIcon, "fa-solid fa-check" },
        { ComponentIcons.CardUploadDeleteIcon, "fa-solid fa-xmark" },
        { ComponentIcons.CardUploadRemoveIcon, "fa-regular fa-trash-can" },
        { ComponentIcons.CardUploadDownloadIcon, "fa-solid fa-download" },
        { ComponentIcons.CardUploadZoomIcon, "fa-solid fa-magnifying-glass-plus" },
        { ComponentIcons.UploadCancelIcon, "fa-solid fa-ban" },

        { ComponentIcons.FileIconExcel, "fa-regular fa-file-excel" },
        { ComponentIcons.FileIconDocx, "fa-regular fa-file-word" },
        { ComponentIcons.FileIconPPT, "fa-regular fa-file-powerpoint" },
        { ComponentIcons.FileIconAudio, "fa-regular fa-file-audio" },
        { ComponentIcons.FileIconVideo, "fa-regular fa-file-video" },
        { ComponentIcons.FileIconCode, "fa-regular fa-file-code" },
        { ComponentIcons.FileIconPdf, "fa-regular fa-file-pdf" },
        { ComponentIcons.FileIconZip, "fa-regular fa-file-archive" },
        { ComponentIcons.FileIconArchive, "fa-regular fa-file-text" },
        { ComponentIcons.FileIconImage, "fa-regular fa-file-image" },
        { ComponentIcons.FileIconFile, "fa-regular fa-file" },

        { ComponentIcons.QueryBuilderPlusIcon, "fa-solid fa-plus" },
        { ComponentIcons.QueryBuilderMinusIcon, "fa-solid fa-minus" },
        { ComponentIcons.QueryBuilderRemoveIcon, "fa-solid fa-xmark" },

        { ComponentIcons.ThemeProviderAutoModeIcon, "fa-solid fa-circle-half-stroke" },
        { ComponentIcons.ThemeProviderLightModeIcon, "fa-solid fa-sun" },
        { ComponentIcons.ThemeProviderDarkModeIcon, "fa-solid fa-moon" },
        { ComponentIcons.ThemeProviderActiveModeIcon, "fa-solid fa-check" }
    };
}
