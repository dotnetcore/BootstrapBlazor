// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.AspNetCore.Components.Rendering;

namespace BootstrapBlazor.Components;

class TabItemContent : IComponent
{
    /// <summary>
    /// Gets or sets the component content. Default is null
    /// </summary>
    [Parameter, NotNull]
    public TabItem? Item { get; set; }

    [CascadingParameter, NotNull]
    private Tab? TabSet { get; set; }

    private RenderHandle _renderHandle;

    void IComponent.Attach(RenderHandle renderHandle)
    {
        _renderHandle = renderHandle;
    }

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

    private object _key = new();

    private void BuildRenderTree(RenderTreeBuilder builder)
    {
        builder.OpenElement(0, "div");
        builder.SetKey(_key);
        builder.AddAttribute(5, "class", ClassString);
        builder.AddAttribute(6, "id", Item.Id);
        builder.AddContent(10, RenderItemContent(Item.ChildContent));
        builder.CloseElement();
    }

    private RenderFragment RenderItemContent(RenderFragment? content) => builder =>
    {
        builder.OpenComponent<ErrorLogger>(0);
        builder.AddAttribute(1, nameof(ErrorLogger.ChildContent), content);

        var enableErrorLogger = TabSet.EnableErrorLogger;
        var showToast = TabSet.ShowErrorLoggerToast;
        builder.AddAttribute(2, nameof(ErrorLogger.EnableErrorLogger), enableErrorLogger);
        builder.AddAttribute(3, nameof(ErrorLogger.ShowToast), showToast);
        builder.AddAttribute(4, nameof(ErrorLogger.ToastTitle), TabSet.ErrorLoggerToastTitle);
        builder.CloseComponent();
    };

    private string? ClassString => CssBuilder.Default("tabs-body-content")
        .AddClass("d-none", !Item.IsActive)
        .Build();

    /// <summary>
    /// Render method
    /// </summary>
    public void Render()
    {
        _key = new object();
        RenderContent();
    }
}
