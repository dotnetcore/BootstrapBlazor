// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components.Web;

namespace BootstrapBlazor.Components;

class TabHeader : IComponent
{
    [Parameter]
    public string? Url { get; set; }

    [Parameter]
    public string? Class { get; set; }

    [Parameter]
    public Func<Task>? OnClick { get; set; }

    [Parameter]
    public RenderFragment? ChildContent { get; set; }

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
        parameters.SetParameterProperties(this);

        RenderContent();
        return Task.CompletedTask;
    }
    private void RenderContent()
    {
        _renderHandle.Render(BuildRenderTree);
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="builder"></param>
    private void BuildRenderTree(RenderTreeBuilder builder)
    {
        builder.OpenElement(0, "a");
        builder.AddAttribute(10, "class", Class);
        builder.AddAttribute(20, "href", Url);
        builder.AddAttribute(30, "onclick", EventCallback.Factory.Create<MouseEventArgs>(this, e => OnClickCallback()));
        builder.AddContent(40, ChildContent);
        builder.CloseElement();
    }

    private async Task OnClickCallback()
    {
        if (OnClick != null)
        {
            await OnClick();
        }
    }
}
