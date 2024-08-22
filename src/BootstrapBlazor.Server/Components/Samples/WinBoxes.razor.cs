// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

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
        Left = "50px"
    };

    private async Task OpenWinBox()
    {
        var option = DefaultOptions;
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
        option.OnShownAsync = () =>
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
        await WinBoxService.Show<Counter>("Test", option: option);
    }
}
