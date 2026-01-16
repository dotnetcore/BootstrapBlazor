// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">MaskService 扩展方法</para>
/// <para lang="en">MaskService extension methods</para>
/// </summary>
public static class MaskServiceExtensions
{

    /// <summary>
    /// <para lang="zh">Show 扩展方法</para>
    /// <para lang="en">Show extension method</para>
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
    /// <para lang="zh">Show 扩展方法</para>
    /// <para lang="en">Show extension method</para>
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
