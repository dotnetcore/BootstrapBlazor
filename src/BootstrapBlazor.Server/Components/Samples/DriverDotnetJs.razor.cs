// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Server.Components.Samples;

/// <summary>
/// FocusGuide 组件示例代码
/// </summary>
public partial class DriverDotnetJs
{
    private DriverJs _guide = default!;
    private DriverJs _guidePopover = default!;
    private DriverJs _guidePopoverStyle = default!;
    private DriverJs _guideDestroy = default!;
    private DriverJsConfig _config = default!;
    private DriverJsConfig _configPopover = default!;
    private DriverJsConfig _configPopoverStyle = default!;
    private ConsoleLogger _logger = default!;

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        _config = new()
        {
            ShowProgress = true
        };
        _configPopover = new()
        {
            ShowProgress = true
        };
        _configPopoverStyle = new()
        {
            PopoverClass = "driverjs-theme"
        };
    }

    private Task OnStart() => _guide.Start();

    private Task OnStartPopover() => _guidePopover.Start();

    private Task OnStartPopoverStyle() => _guidePopoverStyle.Start();

    private Task OnStartDestroy() => _guideDestroy.Start();

    private Task<string?> OnBeforeDestroyAsync(DriverJsConfig config, int index)
    {
        _logger.Log($"Trigger OnBeforeDestroyAsync step index {index}");

        string? content = null;
        if (config.Steps.Count != index + 1)
        {
            content = $"Are you sure to destory the tour at step {index + 1}?";
        }
        return Task.FromResult(content);
    }

    private Task OnDestroyedAsync()
    {
        _logger.Log("Trigger OnDestroyedAsync");
        return Task.CompletedTask;
    }
}
