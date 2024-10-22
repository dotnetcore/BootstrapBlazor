// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Author: Argo Zhang (argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// 抽屉服务扩展方法
/// </summary>
public static class DrawerServiceExtensions
{
    /// <summary>
    /// 弹出搜索对话框
    /// </summary>
    /// <param name="service">DrawerService 服务实例</param>
    /// <param name="parameters"></param>
    /// <param name="option">DrawerOption 配置类实例</param>
    public static async Task Show<TComponent>(this DrawerService service, DrawerOption? option = null, IDictionary<string, object?>? parameters = null) where TComponent : IComponent
    {
        option ??= new DrawerOption();
        option.ChildContent = BootstrapDynamicComponent.CreateComponent<TComponent>(parameters).Render();
        await service.Show(option);
    }

    /// <summary>
    /// 弹出搜索对话框
    /// </summary>
    /// <param name="service">DrawerService 服务实例</param>
    /// <param name="type"></param>
    /// <param name="parameters"></param>
    /// <param name="option">DrawerOption 配置类实例</param>
    public static async Task Show(this DrawerService service, Type type, DrawerOption? option = null, IDictionary<string, object?>? parameters = null)
    {
        option ??= new DrawerOption();
        option.ChildContent = BootstrapDynamicComponent.CreateComponent(type, parameters).Render();
        await service.Show(option);
    }
}
