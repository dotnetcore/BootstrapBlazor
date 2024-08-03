// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Server.Components.Samples;

/// <summary>
/// FocusGuide 组件示例代码
/// </summary>
public partial class FocusGuides
{
    private FocusGuide _guide = default!;
    private FocusGuide _guidePopover = default!;
    private FocusGuideConfig _config = default!;
    private FocusGuideConfig _configPopover = default!;

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
    }

    private async Task OnStart()
    {
        await _guide.Start();
    }

    private async Task OnStartPopover()
    {
        await _guidePopover.Start();
    }
}
