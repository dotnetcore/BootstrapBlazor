// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// FocusGuide 组件步骤配置类
/// </summary>
public class FocusGuideStep
{
    /// <summary>
    /// The popover configuration for this step.
    /// </summary>
    public FocusGuidePopover? Popover { get; set; }

    /// <summary>
    /// Callback when the current step is deselected
    /// </summary>
    /// <param name="step"></param>
    /// <param name="config"></param>
    /// <param name="state"></param>
    /// <returns></returns>
    public Task OnDeselected(FocusGuideStep step, FocusGuideConfig config, FocusGuideState state)
    {
        return Task.CompletedTask;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="step"></param>
    /// <param name="config"></param>
    /// <param name="state"></param>
    /// <returns></returns>
    public Task OnHighlightStarted(FocusGuideStep step, FocusGuideConfig config, FocusGuideState state)
    {
        return Task.CompletedTask;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="step"></param>
    /// <param name="config"></param>
    /// <param name="state"></param>
    /// <returns></returns>
    public Task OnHighlighted(FocusGuideStep step, FocusGuideConfig config, FocusGuideState state)
    {
        return Task.CompletedTask;
    }
}
