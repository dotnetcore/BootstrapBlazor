// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.Extensions.Configuration;

namespace BootstrapBlazor.Components;

class ErrorRender : IComponent
{
    [Inject]
    [NotNull]
    private IConfiguration? Configuration { get; set; }

    private RenderHandle _renderHandle;

    void IComponent.Attach(RenderHandle renderHandle)
    {
        _renderHandle = renderHandle;
    }

    private Exception? _ex;

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="parameters"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public Task SetParametersAsync(ParameterView parameters)
    {
        _ex = parameters.GetValueOrDefault<Exception>("Exception");
        _renderHandle.Render(BuildRenderTree);
        return Task.CompletedTask;
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="builder"></param>
    private void BuildRenderTree(RenderTreeBuilder builder)
    {
        if (_ex is not null)
        {
            builder.OpenElement(0, "div");
            builder.AddAttribute(1, "class", "error-stack");
            builder.AddContent(2, GetErrorContentMarkupString(_ex));
            builder.CloseElement();
        }
    }

    private bool? _errorDetails;
    private MarkupString GetErrorContentMarkupString(Exception ex)
    {
        _errorDetails ??= Configuration.GetValue("DetailedErrors", false);
        return _errorDetails is true
            ? ex.FormatMarkupString(Configuration.GetEnvironmentInformation())
            : new MarkupString(ex.Message);
    }
}
