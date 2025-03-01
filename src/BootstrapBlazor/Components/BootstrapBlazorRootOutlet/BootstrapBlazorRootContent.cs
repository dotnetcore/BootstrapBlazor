// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// BootstrapBlazorRootContent Component
/// </summary>
public class BootstrapBlazorRootContent : IComponent, IDisposable
{
    private object? _registeredIdentifier;

    /// <summary>
    /// Gets or sets the <see cref="string"/> ID that determines which <see cref="BootstrapBlazorRootOutlet"/> instance will render
    /// the content of this instance.
    /// </summary>
    [Parameter] public string? RootName { get; set; }

    /// <summary>
    /// Gets or sets the <see cref="object"/> ID that determines which <see cref="BootstrapBlazorRootOutlet"/> instance will render
    /// the content of this instance.
    /// </summary>
    [Parameter] public object? RootId { get; set; }

    /// <summary>
    /// Gets or sets the content.
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    [Inject]
    private BootstrapBlazorRootRegisterService RootRegisterService { get; set; } = default!;

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="renderHandle"></param>
    void IComponent.Attach(RenderHandle renderHandle)
    {

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
            throw new InvalidOperationException($"{nameof(BootstrapBlazorRootContent)} requires that '{nameof(RootName)}' and '{nameof(RootId)}' cannot both have non-null values.");
        }
        else if (RootName is not null)
        {
            identifier = RootName;
        }
        else if (RootId is not null)
        {
            identifier = RootId;
        }
        identifier ??= BootstrapBlazorRootOutlet.DefaultIdentifier;

        if (!Equals(identifier, _registeredIdentifier))
        {
            if (_registeredIdentifier is not null)
            {
                RootRegisterService.RemoveProvider(_registeredIdentifier, this);
            }

            RootRegisterService.AddProvider(identifier, this);
            _registeredIdentifier = identifier;
        }

        RootRegisterService.NotifyContentProviderChanged(identifier, this);
        return Task.CompletedTask;
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public void Dispose()
    {
        if (_registeredIdentifier is not null)
        {
            RootRegisterService.RemoveProvider(_registeredIdentifier, this);
        }
        GC.SuppressFinalize(this);
    }
}
