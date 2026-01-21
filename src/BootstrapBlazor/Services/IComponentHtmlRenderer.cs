// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">IComponentHtmlRenderer 接口</para>
/// <para lang="en">IComponentHtmlRenderer Interface</para>
/// </summary>
public interface IComponentHtmlRenderer
{
    /// <summary>
    /// <para lang="zh">转化成 Html 片段方法</para>
    /// <para lang="en">Render to HTML Fragment Method</para>
    /// </summary>
    /// <typeparam name="TComponent"></typeparam>
    /// <param name="parameters"></param>
    Task<string> RenderAsync<TComponent>(IDictionary<string, object?>? parameters = null) where TComponent : IComponent;

    /// <summary>
    /// <para lang="zh">转化成 Html 片段方法</para>
    /// <para lang="en">Render to HTML Fragment Method</para>
    /// </summary>
    /// <param name="type"><para lang="zh">Blazor 组件类型</para><para lang="en">Blazor componenttype</para></param>
    /// <param name="parameters"></param>
    Task<string> RenderAsync(Type type, IDictionary<string, object?>? parameters = null);
}
