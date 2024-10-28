﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

#if !NET8_0_OR_GREATER
using Microsoft.AspNetCore.Components.RenderTree;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.Web.HtmlRendering;
using Microsoft.Extensions.Logging;
using System.Runtime.ExceptionServices;

namespace Microsoft.AspNetCore.Components.HtmlRendering.Infrastructure;

#pragma warning disable BL0006 // Do not use RenderTree types

/// <summary>
/// A <see cref="Renderer"/> subclass that is intended for static HTML rendering. Application
/// developers should not normally use this class directly. Instead, use
/// <see cref="HtmlRenderer"/> for a more convenient API.
/// </summary>
[ExcludeFromCodeCoverage]
partial class StaticHtmlRenderer : Renderer
{
    private static readonly Task CanceledRenderTask = Task.FromCanceled(new CancellationToken(canceled: true));

    /// <summary>
    /// Constructs an instance of <see cref="StaticHtmlRenderer"/>.
    /// </summary>
    /// <param name="serviceProvider">The <see cref="IServiceProvider"/> to be used when initializing components.</param>
    /// <param name="loggerFactory">The <see cref="ILoggerFactory"/>.</param>
    public StaticHtmlRenderer(IServiceProvider serviceProvider, ILoggerFactory loggerFactory)
        : base(serviceProvider, loggerFactory)
    {
    }

    /// <inheritdoc/>
    public override Dispatcher Dispatcher { get; } = Dispatcher.CreateDefault();

    /// <summary>
    /// Adds a root component of the specified type and begins rendering it.
    /// </summary>
    /// <param name="componentType">The component type. This must implement <see cref="IComponent"/>.</param>
    /// <param name="initialParameters">Parameters for the component.</param>
    /// <returns>An <see cref="HtmlRootComponent"/> that can be used to obtain the rendered HTML.</returns>
    public HtmlRootComponent BeginRenderingComponent(
        [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)] Type componentType,
        ParameterView initialParameters)
    {
        var component = InstantiateComponent(componentType);
        return BeginRenderingComponent(component, initialParameters);
    }

    /// <summary>
    /// Adds a root component and begins rendering it.
    /// </summary>
    /// <param name="component">The root component instance to be added and rendered. This must not already be associated with any renderer.</param>
    /// <param name="initialParameters">Parameters for the component.</param>
    /// <returns>An <see cref="HtmlRootComponent"/> that can be used to obtain the rendered HTML.</returns>
    public HtmlRootComponent BeginRenderingComponent(
        IComponent component,
        ParameterView initialParameters)
    {
        var componentId = AssignRootComponentId(component);
        var quiescenceTask = RenderRootComponentAsync(componentId, initialParameters);

        if (quiescenceTask.IsFaulted)
        {
            ExceptionDispatchInfo.Capture(quiescenceTask.Exception!.InnerException ?? quiescenceTask.Exception).Throw();
        }

        return new HtmlRootComponent(this, componentId, quiescenceTask);
    }

    /// <inheritdoc/>
    protected override void HandleException(Exception exception)
        => ExceptionDispatchInfo.Capture(exception).Throw();

    /// <inheritdoc/>
    protected override Task UpdateDisplayAsync(in RenderBatch renderBatch)
    {
        // By default we return a canceled task. This has the effect of making it so that the
        // OnAfterRenderAsync callbacks on components don't run by default.
        // This way, by default prerendering gets the correct behavior and other renderers
        // override the UpdateDisplayAsync method already, so those components can
        // either complete a task when the client acknowledges the render, or return a canceled task
        // when the renderer gets disposed.

        // We believe that returning a canceled task is the right behavior as we expect that any class
        // that subclasses this class to provide an implementation for a given rendering scenario respects
        // the contract that OnAfterRender should only be called when the display has successfully been updated
        // and the application is interactive. (Element and component references are populated and JavaScript interop
        // is available).

        return CanceledRenderTask;
    }

    internal new ArrayRange<RenderTreeFrame> GetCurrentRenderTreeFrames(int componentId)
        => base.GetCurrentRenderTreeFrames(componentId);
}

#pragma warning restore BL0006 // Do not use RenderTree types
#endif
