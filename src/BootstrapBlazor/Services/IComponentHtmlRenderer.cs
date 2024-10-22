// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

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
