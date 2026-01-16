// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Server.Components.Samples;

/// <summary>
/// WinBox 示例组件
/// </summary>
public partial class WinBoxes
{
    [Inject, NotNull]
    private WinBoxService? WinBoxService { get; set; }

    private ConsoleLogger _logger = default!;

    private static WinBoxOption DefaultOptions => new()
    {
        Top = "50px",
        Class = "bb-win-box",
        Border = 2,
        Background = "var(--bb-primary-color)"
    };

    private Task OpenModalWinBox()
    {
        var option = DefaultOptions;
        option.Modal = true;
        return OpenWinBox(option);
    }

    private async Task OpenWinBox(WinBoxOption? option)
    {
        option ??= DefaultOptions;
        option.OnCloseAsync = () =>
        {
            _logger.Log($"WinBoxOption({option.Id}) Trigger OnCloseAsync");
            return Task.CompletedTask;
        };
        option.OnCreateAsync = () =>
        {
            _logger.Log($"WinBoxOption({option.Id}) Trigger OnCreateAsync");
            return Task.CompletedTask;
        };
        option.OnShowAsync = () =>
        {
            _logger.Log($"WinBoxOption({option.Id}) Trigger OnShownAsync");
            return Task.CompletedTask;
        };
        option.OnHideAsync = () =>
        {
            _logger.Log($"WinBoxOption({option.Id}) Trigger OnHideAsync");
            return Task.CompletedTask;
        };
        option.OnFocusAsync = () =>
        {
            _logger.Log($"WinBoxOption({option.Id}) Trigger OnFocusAsync");
            return Task.CompletedTask;
        };
        option.OnBlurAsync = () =>
        {
            _logger.Log($"WinBoxOption({option.Id}) Trigger OnBlurAsync");
            return Task.CompletedTask;
        };
        option.OnFullscreenAsync = () =>
        {
            _logger.Log($"WinBoxOption({option.Id}) Trigger OnFullscreenAsync");
            return Task.CompletedTask;
        };
        option.OnRestoreAsync = () =>
        {
            _logger.Log($"WinBoxOption({option.Id}) Trigger OnRestoreAsync");
            return Task.CompletedTask;
        };
        option.OnMaximizeAsync = () =>
        {
            _logger.Log($"WinBoxOption({option.Id}) Trigger OnMaximizeAsync");
            return Task.CompletedTask;
        };
        option.OnMinimizeAsync = () =>
        {
            _logger.Log($"WinBoxOption({option.Id}) Trigger OnMinimizeAsync");
            return Task.CompletedTask;
        };

        await WinBoxService.Show<CustomWinBoxContent>("Test", new Dictionary<string, object?>() {
            { "Option", option }
        }, option);
    }
}
