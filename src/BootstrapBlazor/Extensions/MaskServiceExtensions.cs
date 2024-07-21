// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// MaskService 扩展方法
/// </summary>
public static class MaskServiceExtensions
{
    /// <summary>
    /// Show 扩展方法
    /// </summary>
    /// <param name="maskService"></param>
    /// <param name="parameters"></param>
    /// <param name="containerId"></param>
    /// <param name="backgroundColor"></param>
    /// <param name="opacity"></param>
    /// <param name="zIndex"></param>
    /// <param name="mask"></param>
    /// <returns></returns>
    public static Task Show<TComponent>(this MaskService maskService, IDictionary<string, object?>? parameters = null, string? containerId = null, string? backgroundColor = null, float opacity = 0.5f, int zIndex = 1050, Mask? mask = null) where TComponent : ComponentBase => maskService.Show(new MaskOption()
    {
        BackgroundColor = backgroundColor,
        Opacity = opacity,
        ZIndex = zIndex,
        ContainerId = containerId,
        ChildContent = BootstrapDynamicComponent.CreateComponent<TComponent>(parameters).Render()
    }, mask);

    /// <summary>
    /// Show 扩展方法
    /// </summary>
    /// <param name="maskService"></param>
    /// <param name="type"></param>
    /// <param name="parameters"></param>
    /// <param name="containerId"></param>
    /// <param name="backgroundColor"></param>
    /// <param name="opacity"></param>
    /// <param name="zIndex"></param>
    /// <param name="mask"></param>
    /// <returns></returns>
    public static Task Show(this MaskService maskService, Type type, IDictionary<string, object?>? parameters = null, string? containerId = null, string? backgroundColor = null, float opacity = 0.5f, int zIndex = 1050, Mask? mask = null) => maskService.Show(new MaskOption()
    {
        BackgroundColor = backgroundColor,
        Opacity = opacity,
        ZIndex = zIndex,
        ContainerId = containerId,
        ChildContent = BootstrapDynamicComponent.CreateComponent(type, parameters).Render()
    }, mask);
}
