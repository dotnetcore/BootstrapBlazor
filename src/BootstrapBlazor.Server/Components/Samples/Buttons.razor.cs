// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.AspNetCore.Components.Web;

namespace BootstrapBlazor.Server.Components.Samples;

/// <summary>
/// 
/// </summary>
public sealed partial class Buttons
{
    [NotNull]
    private ConsoleLogger? NormalLogger { get; set; }

    private void ButtonClick(MouseEventArgs e)
    {
        NormalLogger.Log($"Button Clicked");
    }

    private bool IsDisable { get; set; }

    [NotNull]
    private Button? ButtonDisableDemo { get; set; }

    private void ClickButton1()
    {
        IsDisable = !IsDisable;
        StateHasChanged();
    }

    private Task ClickButton2()
    {
        IsDisable = false;
        ButtonDisableDemo.SetDisable(false);
        return Task.CompletedTask;
    }

    private string ButtonText { get; set; } = "";

    private Task ClickButtonShowText(string text)
    {
        ButtonText = text;
        StateHasChanged();
        return Task.CompletedTask;
    }

    private static Task ClickAsyncButton() => Task.Delay(5000);
}
