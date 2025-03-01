// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.AspNetCore.Components.Rendering;

namespace BootstrapBlazor.Components;

/// <summary>
/// BootstrapBlazorRootOutlet Component
/// </summary>
public class BootstrapBlazorRootOutlet : IComponent, IDisposable
{
    private static readonly RenderFragment _emptyRenderFragment = _ => { };
    private object? _subscribedIdentifier;
    private RenderHandle _renderHandle;

    /// <summary>
    /// Gets the default identifier that can be used to subscribe to all <see cref="BootstrapBlazorRootContent"/> instances.
    /// </summary>
    public static readonly object DefaultIdentifier = new();

    [Inject]
    private BootstrapBlazorRootRegisterService RootRegisterService { get; set; } = default!;

    /// <summary>
    /// Gets or sets the <see cref="string"/> ID that determines which <see cref="BootstrapBlazorRootContent"/> instances will provide
    /// content to this instance.
    /// </summary>
    [Parameter]
    public string? RootName { get; set; }

    /// <summary>
    /// Gets or sets the <see cref="object"/> ID that determines which <see cref="BootstrapBlazorRootContent"/> instances will provide
    /// content to this instance.
    /// </summary>
    [Parameter]
    public object? RootId { get; set; }

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

        object? identifier = null;

        if (RootName is not null && RootId is not null)
        {
            throw new InvalidOperationException($"{nameof(BootstrapBlazorRootOutlet)} requires that '{nameof(RootName)}' and '{nameof(RootId)}' cannot both have non-null values.");
        }
        else if (RootName is not null)
        {
            identifier = RootName;
        }
        else if (RootId is not null)
        {
            identifier = RootId;
        }
        identifier ??= DefaultIdentifier;

        if (!Equals(identifier, _subscribedIdentifier))
        {
            if (_subscribedIdentifier is not null)
            {
                RootRegisterService.Unsubscribe(_subscribedIdentifier);
            }

            RootRegisterService.Subscribe(identifier, this);
            _subscribedIdentifier = identifier;
        }

        RenderContent();
        return Task.CompletedTask;
    }

    internal void ContentUpdated(BootstrapBlazorRootContent? provider)
    {
        RenderContent();
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
        if (_subscribedIdentifier is not null)
        {
            foreach (var content in RootRegisterService.GetProviders(_subscribedIdentifier))
            {
                builder.OpenComponent<BootstrapBlazorRootOutletContentRenderer>(0);
                builder.SetKey(content);
                builder.AddAttribute(1, BootstrapBlazorRootOutletContentRenderer.ContentParameterName, content.ChildContent ?? _emptyRenderFragment);
                builder.CloseComponent();
            }
        }
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public void Dispose()
    {
        if (_subscribedIdentifier is not null)
        {
            RootRegisterService.Unsubscribe(_subscribedIdentifier);
        }
        GC.SuppressFinalize(this);
    }

    internal sealed class BootstrapBlazorRootOutletContentRenderer : IComponent
    {
        public const string ContentParameterName = "content";

        private RenderHandle _renderHandle;

        public void Attach(RenderHandle renderHandle)
        {
            _renderHandle = renderHandle;
        }

        public Task SetParametersAsync(ParameterView parameters)
        {
            var fragment = parameters.GetValueOrDefault<RenderFragment>(ContentParameterName)!;
            _renderHandle.Render(fragment);
            return Task.CompletedTask;
        }
    }
}
