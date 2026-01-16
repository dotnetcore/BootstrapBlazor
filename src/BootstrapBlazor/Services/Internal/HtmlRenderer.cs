// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

#if !NET8_0_OR_GREATER
using Microsoft.AspNetCore.Components.HtmlRendering.Infrastructure;
using Microsoft.AspNetCore.Components.Web.HtmlRendering;
using Microsoft.Extensions.Logging;

#pragma warning disable BL0006 // Do not use RenderTree types

namespace Microsoft.AspNetCore.Components.Web;

/// <summary>
/// <para lang="zh">Provides a mechanism for rendering components non-interactively as HTML markup.</para>
/// <para lang="en">Provides a mechanism for rendering components non-interactively as HTML markup.</para>
/// </summary>
[ExcludeFromCodeCoverage]
sealed class HtmlRenderer : IDisposable, IAsyncDisposable
{
    private readonly StaticHtmlRenderer _passiveHtmlRenderer;

    /// <summary>
    /// <para lang="zh">Constructs an 实例 of <see cref="HtmlRenderer"/>.</para>
    /// <para lang="en">Constructs an instance of <see cref="HtmlRenderer"/>.</para>
    /// </summary>
    /// <param name="services">The services to use when rendering components.</param>
    /// <param name="loggerFactory">The logger factory to use.</param>
    public HtmlRenderer(IServiceProvider services, ILoggerFactory loggerFactory)
    {
        _passiveHtmlRenderer = new StaticHtmlRenderer(services, loggerFactory);
    }

    /// <summary>
    /// <para lang="zh"><inheritdoc/></para>
    /// <para lang="en"><inheritdoc/></para>
    /// </summary>
    public void Dispose()
        => _passiveHtmlRenderer.Dispose();

    /// <summary>
    /// <para lang="zh"><inheritdoc/></para>
    /// <para lang="en"><inheritdoc/></para>
    /// </summary>
    public ValueTask DisposeAsync()
        => _passiveHtmlRenderer.DisposeAsync();

    /// <summary>
    /// <para lang="zh">获得 the <see cref="Components.Dispatcher" /> associated with this 实例. Any calls to <see cref="RenderComponentAsync{TComponent}()"/> or <see cref="BeginRenderingComponent{TComponent}()"/> must be performed using this <see cref="Components.Dispatcher" />.</para>
    /// <para lang="en">Gets the <see cref="Components.Dispatcher" /> associated with this instance. Any calls to <see cref="RenderComponentAsync{TComponent}()"/> or <see cref="BeginRenderingComponent{TComponent}()"/> must be performed using this <see cref="Components.Dispatcher" />.</para>
    /// </summary>
    public Dispatcher Dispatcher => _passiveHtmlRenderer.Dispatcher;

    /// <summary>
    /// <para lang="zh">Adds an 实例 of the specified component and instructs it to render. resulting 内容 represents the initial synchronous rendering output, which may later change. To wait for the component hierarchy to complete any asynchronous operations such as loading, await <see cref="HtmlRootComponent.QuiescenceTask"/> before reading 内容 from the <see cref="HtmlRootComponent"/>.</para>
    /// <para lang="en">Adds an instance of the specified component and instructs it to render. The resulting content represents the initial synchronous rendering output, which may later change. To wait for the component hierarchy to complete any asynchronous operations such as loading, await <see cref="HtmlRootComponent.QuiescenceTask"/> before reading content from the <see cref="HtmlRootComponent"/>.</para>
    /// </summary>
    /// <typeparam name="TComponent">The component type.</typeparam>
    /// <returns>An <see cref="HtmlRootComponent"/> instance representing the render output.</returns>
    public HtmlRootComponent BeginRenderingComponent<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)] TComponent>() where TComponent : IComponent
        => _passiveHtmlRenderer.BeginRenderingComponent(typeof(TComponent), ParameterView.Empty);

    /// <summary>
    /// <para lang="zh">Adds an 实例 of the specified component and instructs it to render. resulting 内容 represents the initial synchronous rendering output, which may later change. To wait for the component hierarchy to complete any asynchronous operations such as loading, await <see cref="HtmlRootComponent.QuiescenceTask"/> before reading 内容 from the <see cref="HtmlRootComponent"/>.</para>
    /// <para lang="en">Adds an instance of the specified component and instructs it to render. The resulting content represents the initial synchronous rendering output, which may later change. To wait for the component hierarchy to complete any asynchronous operations such as loading, await <see cref="HtmlRootComponent.QuiescenceTask"/> before reading content from the <see cref="HtmlRootComponent"/>.</para>
    /// </summary>
    /// <typeparam name="TComponent">The component type.</typeparam>
    /// <param name="parameters">Parameters for the component.</param>
    /// <returns>An <see cref="HtmlRootComponent"/> instance representing the render output.</returns>
    public HtmlRootComponent BeginRenderingComponent<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)] TComponent>(
        ParameterView parameters) where TComponent : IComponent
        => _passiveHtmlRenderer.BeginRenderingComponent(typeof(TComponent), parameters);

    /// <summary>
    /// <para lang="zh">Adds an 实例 of the specified component and instructs it to render. resulting 内容 represents the initial synchronous rendering output, which may later change. To wait for the component hierarchy to complete any asynchronous operations such as loading, await <see cref="HtmlRootComponent.QuiescenceTask"/> before reading 内容 from the <see cref="HtmlRootComponent"/>.</para>
    /// <para lang="en">Adds an instance of the specified component and instructs it to render. The resulting content represents the initial synchronous rendering output, which may later change. To wait for the component hierarchy to complete any asynchronous operations such as loading, await <see cref="HtmlRootComponent.QuiescenceTask"/> before reading content from the <see cref="HtmlRootComponent"/>.</para>
    /// </summary>
    /// <param name="componentType">The component type. This must implement <see cref="IComponent"/>.</param>
    /// <returns>An <see cref="HtmlRootComponent"/> instance representing the render output.</returns>
    public HtmlRootComponent BeginRenderingComponent(
        [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)] Type componentType)
        => _passiveHtmlRenderer.BeginRenderingComponent(componentType, ParameterView.Empty);

    /// <summary>
    /// <para lang="zh">Adds an 实例 of the specified component and instructs it to render. resulting 内容 represents the initial synchronous rendering output, which may later change. To wait for the component hierarchy to complete any asynchronous operations such as loading, await <see cref="HtmlRootComponent.QuiescenceTask"/> before reading 内容 from the <see cref="HtmlRootComponent"/>.</para>
    /// <para lang="en">Adds an instance of the specified component and instructs it to render. The resulting content represents the initial synchronous rendering output, which may later change. To wait for the component hierarchy to complete any asynchronous operations such as loading, await <see cref="HtmlRootComponent.QuiescenceTask"/> before reading content from the <see cref="HtmlRootComponent"/>.</para>
    /// </summary>
    /// <param name="componentType">The component type. This must implement <see cref="IComponent"/>.</param>
    /// <param name="parameters">Parameters for the component.</param>
    /// <returns>An <see cref="HtmlRootComponent"/> instance representing the render output.</returns>
    public HtmlRootComponent BeginRenderingComponent(
        [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)] Type componentType,
        ParameterView parameters)
        => _passiveHtmlRenderer.BeginRenderingComponent(componentType, parameters);

    /// <summary>
    /// <para lang="zh">Adds an 实例 of the specified component and instructs it to render, waiting for the component hierarchy to complete asynchronous tasks such as loading.</para>
    /// <para lang="en">Adds an instance of the specified component and instructs it to render, waiting for the component hierarchy to complete asynchronous tasks such as loading.</para>
    /// </summary>
    /// <typeparam name="TComponent">The component type.</typeparam>
    /// <returns>A task that completes with <see cref="HtmlRootComponent"/> once the component hierarchy has completed any asynchronous tasks such as loading.</returns>
    public Task<HtmlRootComponent> RenderComponentAsync<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)] TComponent>() where TComponent : IComponent
        => RenderComponentAsync<TComponent>(ParameterView.Empty);

    /// <summary>
    /// <para lang="zh">Adds an 实例 of the specified component and instructs it to render, waiting for the component hierarchy to complete asynchronous tasks such as loading.</para>
    /// <para lang="en">Adds an instance of the specified component and instructs it to render, waiting for the component hierarchy to complete asynchronous tasks such as loading.</para>
    /// </summary>
    /// <param name="componentType">The component type. This must implement <see cref="IComponent"/>.</param>
    /// <returns>A task that completes with <see cref="HtmlRootComponent"/> once the component hierarchy has completed any asynchronous tasks such as loading.</returns>
    public Task<HtmlRootComponent> RenderComponentAsync(
        [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)] Type componentType)
        => RenderComponentAsync(componentType, ParameterView.Empty);

    /// <summary>
    /// <para lang="zh">Adds an 实例 of the specified component and instructs it to render, waiting for the component hierarchy to complete asynchronous tasks such as loading.</para>
    /// <para lang="en">Adds an instance of the specified component and instructs it to render, waiting for the component hierarchy to complete asynchronous tasks such as loading.</para>
    /// </summary>
    /// <typeparam name="TComponent">The component type.</typeparam>
    /// <param name="parameters">Parameters for the component.</param>
    /// <returns>A task that completes with <see cref="HtmlRootComponent"/> once the component hierarchy has completed any asynchronous tasks such as loading.</returns>
    public Task<HtmlRootComponent> RenderComponentAsync<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)] TComponent>(
        ParameterView parameters) where TComponent : IComponent
        => RenderComponentAsync(typeof(TComponent), parameters);

    /// <summary>
    /// <para lang="zh">Adds an 实例 of the specified component and instructs it to render, waiting for the component hierarchy to complete asynchronous tasks such as loading.</para>
    /// <para lang="en">Adds an instance of the specified component and instructs it to render, waiting for the component hierarchy to complete asynchronous tasks such as loading.</para>
    /// </summary>
    /// <param name="componentType">The component type. This must implement <see cref="IComponent"/>.</param>
    /// <param name="parameters">Parameters for the component.</param>
    /// <returns>A task that completes with <see cref="HtmlRootComponent"/> once the component hierarchy has completed any asynchronous tasks such as loading.</returns>
    public async Task<HtmlRootComponent> RenderComponentAsync(
        [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)] Type componentType,
        ParameterView parameters)
    {
        var content = BeginRenderingComponent(componentType, parameters);
        await content.QuiescenceTask;
        return content;
    }
}
#pragma warning restore BL0006 // Do not use RenderTree types
#endif
