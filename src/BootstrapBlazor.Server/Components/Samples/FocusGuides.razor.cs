// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Server.Components.Samples;

/// <summary>
/// FocusGuide 组件示例代码
/// </summary>
public partial class FocusGuides
{
    private FocusGuide _guide1 = default!;

    private FocusGuideConfig _config1 = default!;

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        _config1 = new FocusGuideConfig()
        {
            ShowProgress = true
        };
    }

    private async Task OnStart()
    {
        await _guide1.Start();
    }
}
