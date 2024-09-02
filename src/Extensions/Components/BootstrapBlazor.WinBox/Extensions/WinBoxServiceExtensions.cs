// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;

namespace BootstrapBlazor.Components;

/// <summary>
/// <see cref="WinBoxService"/> 扩展方法
/// </summary>
public static class WinBoxServiceExtensions
{
    /// <summary>
    /// <see cref="WinBoxService"/> 扩展方法
    /// </summary>
    /// <typeparam name="TComponent">内容组件</typeparam>
    /// <param name="service"><see cref="WinBoxService"/> 实例</param>
    /// <param name="title">指定窗口标题</param>
    /// <param name="parameters">指定组件参数集合</param>
    /// <param name="option">额外参数集合</param>
    public static Task Show<TComponent>(this WinBoxService service, string title, IDictionary<string, object?>? parameters = null, WinBoxOption? option = null) where TComponent : ComponentBase => Show(service, title, typeof(TComponent), parameters, option);

    /// <summary>
    /// <see cref="WinBoxService"/> 扩展方法
    /// </summary>
    /// <param name="service"><see cref="WinBoxService"/> 实例</param>
    /// <param name="title">指定窗口标题</param>
    /// <param name="type">指定内容组件类型</param>
    /// <param name="parameters">指定组件参数集合</param>
    /// <param name="option">额外参数集合</param>
    public static async Task Show(this WinBoxService service, string title, Type type, IDictionary<string, object?>? parameters = null, WinBoxOption? option = null)
    {
        option ??= new WinBoxOption();
        option.Title = title;
        option.ContentTemplate = BootstrapDynamicComponent.CreateComponent(type, parameters).Render();
        await service.Show(option);
    }

    /// <summary>
    /// <see cref="WinBoxService"/> 扩展方法
    /// </summary>
    /// <typeparam name="TComponent">内容组件</typeparam>
    /// <param name="service"><see cref="WinBoxService"/> 实例</param>
    /// <param name="title">指定窗口标题</param>
    /// <param name="parameters">指定组件参数集合</param>
    /// <param name="option">额外参数集合</param>
    public static Task ShowModal<TComponent>(this WinBoxService service, string title, IDictionary<string, object?>? parameters = null, WinBoxOption? option = null) where TComponent : ComponentBase => ShowModal(service, title, typeof(TComponent), parameters, option);

    /// <summary>
    /// <see cref="WinBoxService"/> 扩展方法
    /// </summary>
    /// <param name="service"><see cref="WinBoxService"/> 实例</param>
    /// <param name="title">指定窗口标题</param>
    /// <param name="type">指定内容组件类型</param>
    /// <param name="parameters">指定组件参数集合</param>
    /// <param name="option">额外参数集合</param>
    public static async Task ShowModal(this WinBoxService service, string title, Type type, IDictionary<string, object?>? parameters = null, WinBoxOption? option = null)
    {
        option ??= new WinBoxOption();
        option.Title = title;
        option.Modal = true;
        option.ContentTemplate = BootstrapDynamicComponent.CreateComponent(type, parameters).Render();
        await service.Show(option);
    }
}
