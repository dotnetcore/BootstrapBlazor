// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">抽屉服务扩展方法</para>
/// <para lang="en">Drawer Service Extensions</para>
/// </summary>
public static class DrawerServiceExtensions
{
    /// <summary>
    /// <para lang="zh">弹出搜索对话框</para>
    /// <para lang="en">Show search dialog</para>
    /// </summary>
    /// <param name="service"><para lang="zh">DrawerService 服务实例</para><para lang="en">DrawerService instance</para></param>
    /// <param name="parameters"></param>
    /// <param name="option"><para lang="zh">DrawerOption 配置类实例</para><para lang="en">DrawerOption instance</para></param>
    public static async Task Show<TComponent>(this DrawerService service, DrawerOption? option = null, IDictionary<string, object?>? parameters = null) where TComponent : IComponent
    {
        option ??= new DrawerOption();
        option.ChildContent = BootstrapDynamicComponent.CreateComponent<TComponent>(parameters).Render();
        await service.Show(option);
    }

    /// <summary>
    /// <para lang="zh">弹出搜索对话框</para>
    /// <para lang="en">Show search dialog</para>
    /// </summary>
    /// <param name="service"><para lang="zh">DrawerService 服务实例</para><para lang="en">DrawerService instance</para></param>
    /// <param name="type"></param>
    /// <param name="parameters"></param>
    /// <param name="option"><para lang="zh">DrawerOption 配置类实例</para><para lang="en">DrawerOption instance</para></param>
    public static async Task Show(this DrawerService service, Type type, DrawerOption? option = null, IDictionary<string, object?>? parameters = null)
    {
        option ??= new DrawerOption();
        option.ChildContent = BootstrapDynamicComponent.CreateComponent(type, parameters).Render();
        await service.Show(option);
    }
}
