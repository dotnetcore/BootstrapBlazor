// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.AspNetCore.Components.Rendering;

namespace BootstrapBlazor.Components;

internal class LayoutTabHeader : IComponent
{
    private RenderHandle _renderHandle;

    void IComponent.Attach(RenderHandle renderHandle)
    {
        _renderHandle = renderHandle;
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="parameters"></param>
    /// <returns></returns>
    Task IComponent.SetParametersAsync(ParameterView parameters)
    {
        return Task.CompletedTask;
    }

    private RenderFragment? _renderFragment;

    /// <summary>
    /// render tab header method.
    /// </summary>
    public void RenderHeader(RenderFragment renderFragment)
    {
        _renderFragment = renderFragment;
        _renderHandle.Render(BuildRenderTree);
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="builder"></param>
    private void BuildRenderTree(RenderTreeBuilder builder)
    {
        if (_renderFragment != null)
        {
            builder.AddContent(0, _renderFragment);
        }
    }
}
