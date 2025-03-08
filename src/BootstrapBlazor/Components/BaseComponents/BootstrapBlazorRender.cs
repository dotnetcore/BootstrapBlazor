// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.AspNetCore.Components.Rendering;

namespace BootstrapBlazor.Components;

/// <summary>
/// BootstrapBlazorRender component
/// </summary>
public class BootstrapBlazorRender : IComponent
{
    /// <summary>
    /// Gets or sets the child content
    /// </summary>
    [Parameter, NotNull]
    public RenderFragment? ChildContent { get; set; }

    private static readonly RenderFragment _emptyRenderFragment = _ => { };

    private RenderHandle _renderHandle;

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="renderHandle"></param>
    public void Attach(RenderHandle renderHandle)
    {
        _renderHandle = renderHandle;
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="parameters"></param>
    /// <returns></returns>
    public Task SetParametersAsync(ParameterView parameters)
    {
        parameters.SetParameterProperties(this);

        Render();
        return Task.CompletedTask;
    }
    /// <summary>
    /// Render method
    /// </summary>
    public void Render()
    {
        _renderHandle.Render(BuildRenderTree);
    }

    private void BuildRenderTree(RenderTreeBuilder builder)
    {
        builder.AddContent(0, ChildContent ?? _emptyRenderFragment);
    }
}
