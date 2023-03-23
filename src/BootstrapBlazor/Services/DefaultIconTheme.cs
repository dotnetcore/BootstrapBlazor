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

            { ComponentIcons.ButtonLoadingIcon, "fa-solid fa-fw fa-spin fa-spinner" },

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

            { ComponentIcons.ResultDialogYesIcon, "fa-solid fa-check" },
            { ComponentIcons.ResultDialogNoIcon, "fa-regular fa-circle-xmark" },
            { ComponentIcons.ResultDialogCloseIcon, "fa-regular fa-circle-xmark" },

            { ComponentIcons.SearchDialogClearIcon, "fa-regular fa-trash-can" },
            { ComponentIcons.SearchDialogSearchIcon, "fa-solid fa-magnifying-glass" },

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

            { ComponentIcons.LoadingIcon, "fa-solid fa-fw fa-spin fa-spinner" },

            { ComponentIcons.PopConfirmButtonConfirmIcon, "fa-solid fa-circle-exclamation text-info" },
            { ComponentIcons.PopConfirmButtonContentIcon, "fa-solid fa-exclamation-circle text-info" },

            { ComponentIcons.TableSortIconAsc, "fa-solid fa-sort-up" },
            { ComponentIcons.TableSortDesc, "fa-solid fa-sort-down" },
            { ComponentIcons.TableSortIcon, "fa-solid fa-sort" },
            { ComponentIcons.TableFilterIcon, "fa-solid fa-filter" },
            { ComponentIcons.TableExportButtonIcon, "fa-solid fa-download" },

            { ComponentIcons.SearchClearButtonIcon, "fa-regular fa-trash-can" },
            { ComponentIcons.SearchButtonIcon, "fa-solid fa-magnifying-glass" },
            { ComponentIcons.SearchButtonLoadingIcon, "fa-solid fa-fw fa-spin fa-spinner" },

            { ComponentIcons.SelectDropdownIcon, "fa-solid fa-angle-up" },
            { ComponentIcons.SelectSearchIcon, "fa-solid fa-magnifying-glass" },

            { ComponentIcons.PrintButtonIcon, "fa-solid fa-fw fa-print" }
        };
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public Dictionary<ComponentIcons, string> GetIcons() => Icons;
}
