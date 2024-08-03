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
    private DriverJsConfig _config = default!;
    private DriverJsConfig _configPopover = default!;
    private DriverJsConfig _configPopoverStyle = default!;

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

    private async Task OnStart()
    {
        await _guide.Start();
    }

    private async Task OnStartPopover()
    {
        await _guidePopover.Start();
    }

    private async Task OnStartPopoverStyle()
    {
        await _guidePopoverStyle.Start();
    }
}
