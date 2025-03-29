// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.AspNetCore.Components.Rendering;

namespace BootstrapBlazor.Components;

internal class TabItemContent : IComponent
{
    /// <summary>
    /// Gets or sets the component content. Default is null
    /// </summary>
    [Parameter, NotNull]
    public TabItem? Item { get; set; }

    /// <summary>
    /// Gets <see cref="IComponentIdGenerator"/> instrance
    /// </summary>
    [Inject]
    [NotNull]
    private IComponentIdGenerator? ComponentIdGenerator { get; set; }

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
        builder.AddAttribute(6, "id", ComponentIdGenerator.Generate(Item));
        builder.AddContent(10, Item.ChildContent);
        builder.CloseElement();
    }

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
