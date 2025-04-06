// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

internal class LayoutHeader : IComponent
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

    /// <summary>
    /// render tab header method.
    /// </summary>
    public void Render(RenderFragment renderFragment)
    {
        _renderHandle.Render(builder =>
        {
            builder.AddContent(0, renderFragment);
        });
    }
}
