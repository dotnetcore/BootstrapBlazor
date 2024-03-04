// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.JSInterop;

namespace BootstrapBlazor.Server.Components.Samples;

/// <summary>
/// 图标库
/// </summary>
public partial class FAIcons
{
    [NotNull]
    private Modal? IconModal { get; set; }

    private string? IconClass { get; set; }

    private string? Tag { get; set; }

    private string? IconName { get; set; }

    private bool ShowCopyDialog { get; set; }

    private string DisplayText => ShowCopyDialog ? Localizer["SwitchButtonTextOn"] : Localizer["SwitchButtonTextOff"];

    [Inject]
    [NotNull]
    private IJSRuntime? JsRunTime { get; set; }

    //private Task ShowModal(IconTest icon)
    //{
    //    IconClass = icon.IconClass;
    //    IconName = icon.IconName;
    //    Tag = $"<i class=\"{IconClass}\" aria-hidden=\"true\"></i>";
    //    StateHasChanged();
    //    IconModal.Toggle();
    //    return Task.CompletedTask;
    //}

    //private void WriteClipboard(string text)
    //{
    //    JsRunTime.InvokeVoidAsync("navigator.clipboard.writeText", text);
    //}
}
