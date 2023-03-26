// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

internal class DefaultIconTheme : IIconTheme
{
    private Dictionary<ComponentIcons, string> Icons { get; }

    public DefaultIconTheme()
    {
        Icons = new Dictionary<ComponentIcons, string>()
        {
            { ComponentIcons.AnchorLinkIcon, "fa-solid fa-link" },
            { ComponentIcons.AvatarIcon, "fa-solid fa-user" },

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
            { ComponentIcons.DialogMaxminzeWindowIcon, "fa-regular fa-window-maximize" },
            { ComponentIcons.DialogRestoreWindowIcon, "fa-regular fa-window-restore" },

            { ComponentIcons.TopMenuDropdownIcon, "fa-solid fa-angle-down" },
            { ComponentIcons.SideMenuDropdownIcon, "fa-solid fa-angle-down" },

            { ComponentIcons.ResultDialogYesIcon, "fa-solid fa-check" },
            { ComponentIcons.ResultDialogNoIcon, "fa-regular fa-circle-xmark" },
            { ComponentIcons.ResultDialogCloseIcon, "fa-regular fa-circle-xmark" },

            { ComponentIcons.SearchDialogClearIcon, "fa-regular fa-trash-can" },
            { ComponentIcons.SearchDialogSearchIcon, "fa-solid fa-magnifying-glass" },

            { ComponentIcons.StepIcon, "fa-solid fa-check" },
            { ComponentIcons.StepErrorIcon, "fa-solid fa-xmark" },

            { ComponentIcons.FilterButtonFilterIcon, "fa-solid fa-filter" },
            { ComponentIcons.FilterButtonClearIcon, "fa-solid fa-ban" },

            { ComponentIcons.TableFilterPlusIcon, "fa-solid fa-plus" },
            { ComponentIcons.TableFilterMinusIcon, "fa-solid fa-minus" },

            { ComponentIcons.FullScreenButtonIcon, "fa-solid fa-maximize" },

            { ComponentIcons.GoTopIcon, "fa-solid fa-angle-up" },

            { ComponentIcons.ImagePreviewPreviousIcon, "fa-solid fa-angle-left fa-2x" },
            { ComponentIcons.ImagePreviewNextIcon, "fa-solid fa-angle-right fa-2x" },
            { ComponentIcons.ImagePreviewMinusIcon, "fa-solid fa-magnifying-glass-minus" },
            { ComponentIcons.ImagePreviewPlusIcon, "fa-solid fa-magnifying-glass-plus" },
            { ComponentIcons.ImagePreviewRotateLeftIcon, "fa-solid fa-rotate-left" },
            { ComponentIcons.ImagePreviewRotateRightIcon, "fa-solid fa-rotate-right" },

            { ComponentIcons.ImageViewerFileIcon, "fa-regular fa-file-image fa-2x" },

            { ComponentIcons.InputNumberMinusIcon, "fa-solid fa-circle-minus" },
            { ComponentIcons.InputNumberPlusIcon, "fa-solid fa-circle-plus" },

            { ComponentIcons.LayoutMenuBarIcon, "fa-solid fa-bars" },
            { ComponentIcons.LogoutLinkIcon, "fa-solid fa-key" },

            { ComponentIcons.LoadingIcon, "fa-solid fa-fw fa-spin fa-spinner" },

            { ComponentIcons.PaginationPrevPageIcon, "fa-solid fa-angle-left" },
            { ComponentIcons.PaginationNextPageIcon, "fa-solid fa-angle-right" },
            { ComponentIcons.PaginationPrevEllipsisPageIcon, "fa-solid fa-ellipsis" },
            { ComponentIcons.PaginationNextEllipsisPageIcon, "fa-solid fa-ellipsis" },

            { ComponentIcons.PopConfirmButtonConfirmIcon, "fa-solid fa-circle-exclamation text-info" },
            { ComponentIcons.PopConfirmButtonContentIcon, "fa-solid fa-exclamation-circle text-info" },

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
            { ComponentIcons.SelectSearchIcon, "fa-solid fa-magnifying-glass" },

            { ComponentIcons.SweetAlertCloseIcon, "fa-solid fa-xmark" },
            { ComponentIcons.SweetAlertConfirmIcon, "fa-solid fa-check" },

            { ComponentIcons.PrintButtonIcon, "fa-solid fa-print" },

            { ComponentIcons.ToastSuccessIcon, "fa-solid fa-check-circle text-success" },
            { ComponentIcons.ToastInformationIcon, "fa-solid fa-exclamation-circle text-info" },
            { ComponentIcons.ToastWarningIcon, "fa-solid fa-exclamation-triangle text-warning" },
            { ComponentIcons.ToastErrorIcon, "fa-solid fa-xmark-circle text-danger" },

            { ComponentIcons.TabPreviousIcon, "fa-solid fa-chevron-left" },
            { ComponentIcons.TabNextIcon, "fa-solid fa-chevron-left" },
            { ComponentIcons.TabDropdownIcon, "fa-solid fa-chevron-left" },
            { ComponentIcons.TabCloseIcon, "fa-solid fa-xmark" },

            { ComponentIcons.TableSortIconAsc, "fa-solid fa-sort-up" },
            { ComponentIcons.TableSortDesc, "fa-solid fa-sort-down" },
            { ComponentIcons.TableSortIcon, "fa-solid fa-sort" },
            { ComponentIcons.TableFilterIcon, "fa-solid fa-filter" },
            { ComponentIcons.TableExportButtonIcon, "fa-solid fa-download" },

            { ComponentIcons.TableAddButtonIcon, "fa-solid fa-plus" },
            { ComponentIcons.TableEditButtonIcon, "fa-solid fa-pencil" },
            { ComponentIcons.TableDeleteButtonIcon, "fa-solid fa-xmark" },
            { ComponentIcons.TableRefreshButtonIcon, "fa-solid fa-arrows-rotate" },
            { ComponentIcons.TableCardViewButtonIcon, "fa-solid fa-bars" },
            { ComponentIcons.TableColumnListButtonIcon, "fa-solid fa-table-list" },
            { ComponentIcons.TableExcelExportIcon, "fa-regular fa-file-excel" },
            { ComponentIcons.TableSearchButtonIcon, "fa-solid fa-magnifying-glass" },
            { ComponentIcons.TableResetSearchButtonIcon, "fa-regular fa-trash-can" },
            { ComponentIcons.TableCloseButtonIcon, "fa-solid fa-xmark" },
            { ComponentIcons.TableCancelButtonIcon, "fa-solid fa-xmark" },
            { ComponentIcons.TableSaveButtonIcon, "fa-solid fa-floppy-disk" },
            { ComponentIcons.TableAdvanceButtonIcon, "fa-solid fa-magnifying-glass-plus" },

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
            { ComponentIcons.TreeViewNodeIcon, "fa-solid fa-caret-right" }
        };
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public Dictionary<ComponentIcons, string> GetIcons() => Icons;
}
