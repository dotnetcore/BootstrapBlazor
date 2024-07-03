// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

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
