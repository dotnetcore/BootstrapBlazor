// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Components;
using BootstrapBlazor.Shared.Common;
using BootstrapBlazor.Shared.Components;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Microsoft.JSInterop;

namespace BootstrapBlazor.Shared.Samples;

/// <summary>
/// Notifications 通知
/// </summary>
public partial class Notifications : IDisposable
{
    private JSInterop<Notifications>? Interop { get; set; }

    [NotNull]
    private BlockLogger? Trace { get; set; }

    [Inject]
    [NotNull]
    private IStringLocalizer<Notifications>? Localizer { get; set; }

    [Inject]
    [NotNull]
    private IJSRuntime? JSRuntime { get; set; }

    private bool Permission { get; set; }

    private NotificationItem Model { get; set; } = new NotificationItem()
    {
        Icon = "_content/BootstrapBlazor.Shared/images/Argo-C.png"
    };

    /// <summary>
    /// OnInitialized 方法
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();
        Model.Title ??= Localizer["TitleSampleText"];
        Model.Message ??= Localizer["MessageSampleText"];
        Model.OnClick ??= nameof(OnClickNotificationCallback);
    }

    /// <summary>
    /// OnAfterRenderAsync 方法
    /// </summary>
    /// <param name="firstRender"></param>
    /// <returns></returns>
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            Interop = new JSInterop<Notifications>(JSRuntime);
            await BrowserNotification.CheckPermission(Interop, this, nameof(GetPermissionCallback), false);
        }
    }

    private async Task CheckPermission()
    {
        Interop ??= new JSInterop<Notifications>(JSRuntime);
        await BrowserNotification.CheckPermission(Interop, this, nameof(GetPermissionCallback));
    }

    private async Task Dispatch()
    {
        Interop ??= new JSInterop<Notifications>(JSRuntime);
        await BrowserNotification.Dispatch(Interop, this, Model, nameof(ShowNotificationCallback));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="result"></param>
    [JSInvokable]
    public void GetPermissionCallback(bool result)
    {
        Permission = result;
        Trace.Log(Localizer["GetPermissionCallbackText"] + (result ? "OK" : "No permission"));
        StateHasChanged();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="result"></param>
    [JSInvokable]
    public void ShowNotificationCallback(bool result)
    {
        Trace.Log($"{Localizer["ShowNotificationCallbackText"]}: {result}");
        StateHasChanged();
    }

    /// <summary>
    /// 
    /// </summary>
    [JSInvokable]
    public void OnClickNotificationCallback()
    {
        Trace.Log($"{Localizer["OnClickText"]}");
        StateHasChanged();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="disposing"></param>
    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            if (Interop != null)
            {
                Interop.Dispose();
                Interop = null;
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    private IEnumerable<AttributeItem> GetNotificationItem() => new AttributeItem[]
    {
        new()
        {
            Name = "Title",
            Description = Localizer["TitleText"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "Message",
            Description = Localizer["MessageText"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new() {
            Name = "Icon",
            Description = Localizer["IconText"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "Silent",
            Description = Localizer["SilentText"],
            Type = "bool",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "Sound",
            Description = Localizer["SoundText"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "OnClick",
            Description = Localizer["OnClickText"],
            Type = "Methods",
            ValueList = " — ",
            DefaultValue = " — "
        },
    };
}
