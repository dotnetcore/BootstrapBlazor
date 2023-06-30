// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// IComponentHtmlRenderer interface
/// </summary>
public interface IComponentHtmlRenderer
{
    /// <summary>
    /// 转化成 Html 片段方法
    /// </summary>
    /// <typeparam name="TComponent"></typeparam>
    /// <param name="parameters"></param>
    /// <returns></returns>
    Task<string> RenderAsync<TComponent>(IDictionary<string, object?>? parameters = null) where TComponent : IComponent;

    /// <summary>
    /// 转化成 Html 片段方法
    /// </summary>
    /// <param name="type">Blazor 组件类型</param>
    /// <param name="parameters"></param>
    /// <returns></returns>
    Task<string> RenderAsync(Type type, IDictionary<string, object?>? parameters = null);
}
