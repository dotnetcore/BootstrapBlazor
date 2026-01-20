// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.AspNetCore.Components.Forms;

namespace BootstrapBlazor.Server.Components.Samples;

/// <summary>
/// PopoverConfirms
/// </summary>
public sealed partial class PopoverConfirms
{
    private static Task OnAsyncConfirm() => Task.Delay(3000);

    [NotNull]
    private ConsoleLogger? Logger { get; set; }

    [NotNull]
    private Foo? Model { get; set; }

    [NotNull]
    private ConsoleLogger? FormLogger { get; set; }

    /// <summary>
    /// OnInitialized
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        Model = new() { Name = "Name", Education = EnumEducation.Primary, DateTime = DateTime.Now };
    }

    private Task OnClose()
    {
        // This method is called back when the confirm button is clicked, and this method will not be called when the cancel button is clicked
        Logger.Log("OnClose Trigger");
        return Task.CompletedTask;
    }

    private Task OnConfirm()
    {
        // This method is called back when the confirm button is clicked, and this method will not be called when the cancel button is clicked
        Logger.Log("OnConfirm Trigger");
        return Task.CompletedTask;
    }

    private async Task OnAsyncSubmit()
    {
        FormLogger.Log("异步提交");
        await Task.Delay(3000);
    }

    private async Task OnValidSubmit(EditContext context)
    {
        await Task.Delay(3000);
        FormLogger.Log("数据合规");
    }

    private Task OnInValidSubmit(EditContext context)
    {
        FormLogger.Log("数据非法");
        return Task.CompletedTask;
    }

    /// <summary>
    /// Get event method
    /// </summary>
    /// <returns></returns>
    private static EventItem[] GetEvents() =>
    [
        new()
        {
            Name = "OnConfirm",
            Description="Callback method when confirm is clicked",
            Type ="Func<Task>"
        },
        new()
        {
            Name = "OnClose",
            Description="Callback method when click close",
            Type ="Func<Task>"
        },
        new()
        {
            Name = "OnBeforeClick",
            Description="Click the callback method before confirming the pop-up window",
            Type ="Func<Task<bool>>"
        }
    ];
}
