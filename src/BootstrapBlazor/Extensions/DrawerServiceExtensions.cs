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
    /// <param name="drawerContainer">指定弹窗组件 默认为 null 使用 <see cref="BootstrapBlazorRoot"/> 组件内置弹窗组件</param>
    public static async Task Show<TComponent>(this DrawerService service, IDictionary<string, object?>? parameters = null, DrawerOption? option = null, DrawerContainer? drawerContainer = null) where TComponent : IComponent
    {
        option ??= new DrawerOption();
        option.ChildContent = BootstrapDynamicComponent.CreateComponent<TComponent>(parameters).Render();
        await service.Show(option, drawerContainer);
    }

    /// <summary>
    /// 弹出搜索对话框
    /// </summary>
    /// <param name="service">DrawerService 服务实例</param>
    /// <param name="type"></param>
    /// <param name="parameters"></param>
    /// <param name="option">DrawerOption 配置类实例</param>
    /// <param name="drawerContainer">指定弹窗组件 默认为 null 使用 <see cref="BootstrapBlazorRoot"/> 组件内置弹窗组件</param>
    public static async Task Show(this DrawerService service, Type type, IDictionary<string, object?>? parameters = null, DrawerOption? option = null, DrawerContainer? drawerContainer = null)
    {
        option ??= new DrawerOption();
        option.ChildContent = BootstrapDynamicComponent.CreateComponent(type, parameters).Render();
        await service.Show(option, drawerContainer);
    }
}
