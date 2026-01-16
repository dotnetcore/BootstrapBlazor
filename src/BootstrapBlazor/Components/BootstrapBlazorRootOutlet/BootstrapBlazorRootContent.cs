// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">BootstrapBlazorRootContent Component
///</para>
/// <para lang="en">BootstrapBlazorRootContent Component
///</para>
/// </summary>
public class BootstrapBlazorRootContent : IComponent, IDisposable
{
    private object? _registeredIdentifier;

    /// <summary>
    /// <para lang="zh">获得/设置 the <see cref="string"/> ID that determines which <see cref="BootstrapBlazorRootOutlet"/> 实例 will render
    /// the 内容 of this 实例.
    ///</para>
    /// <para lang="en">Gets or sets the <see cref="string"/> ID that determines which <see cref="BootstrapBlazorRootOutlet"/> instance will render
    /// the content of this instance.
    ///</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter] public string? RootName { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 the <see cref="object"/> ID that determines which <see cref="BootstrapBlazorRootOutlet"/> 实例 will render
    /// the 内容 of this 实例.
    ///</para>
    /// <para lang="en">Gets or sets the <see cref="object"/> ID that determines which <see cref="BootstrapBlazorRootOutlet"/> instance will render
    /// the content of this instance.
    ///</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter] public object? RootId { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 the 内容.
    ///</para>
    /// <para lang="en">Gets or sets the content.
    ///</para>
    /// <para><version>10.2.2</version></para>
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
