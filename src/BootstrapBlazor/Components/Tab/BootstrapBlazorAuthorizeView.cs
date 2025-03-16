// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components.Routing;
using System.Collections.ObjectModel;

namespace BootstrapBlazor.Components;

/// <summary>
/// BootstrapBlazorAuthorizeView component
/// </summary>
public class BootstrapBlazorAuthorizeView : ComponentBase
{
    /// <summary>
    /// Gets or sets the type associated with the route. default is null
    /// </summary>
    [Parameter]
    [NotNull]
    public Type? Type { get; set; }

    /// <summary>
    /// Gets or sets the parameters associated with the route. default is null
    /// </summary>
    [Parameter]
    public IReadOnlyDictionary<string, object>? Parameters { get; set; }

    /// <summary>
    /// Gets or sets the template to display when the user is not authorized. default is null
    /// </summary>
    [Parameter]
    public RenderFragment? NotAuthorized { get; set; }

    /// <summary>
    /// Gets or sets the resource to which access is being controlled. default is null
    /// </summary>
    [Parameter]
    public object? Resource { get; set; }

    [CascadingParameter]
    private Task<AuthenticationState>? AuthenticationState { get; set; }

    [Inject]
    [NotNull]
    private NavigationManager? NavigationManager { get; set; }

    [Inject, NotNull]
    private IServiceProvider? ServiceProvider { get; set; }

    private bool Authorized { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <returns>A task that represents the asynchronous operation.</returns>
    protected override async Task OnInitializedAsync()
    {
        Authorized = Type == null || await Type.IsAuthorizedAsync(ServiceProvider, AuthenticationState, Resource);
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        // Check if authorization is enabled
        if (Authorized && Type != null)
        {
            var index = 0;
            builder.OpenComponent(index++, Type);
            foreach (var kv in (Parameters ?? new ReadOnlyDictionary<string, object>(new Dictionary<string, object>())))
            {
                builder.AddAttribute(index++, kv.Key, kv.Value);
            }
            BuildQueryParameters();
            builder.CloseComponent();
        }
        else
        {
            builder.AddContent(0, NotAuthorized);
        }

        void BuildQueryParameters()
        {
            var queryParameterSupplier = QueryParameterValueSupplier.ForType(Type);
            if (queryParameterSupplier is not null)
            {
                // Since this component does accept some parameters from query, we must supply values for all of them,
                // even if the querystring in the URI is empty. So don't skip the following logic.
                var url = NavigationManager.Uri;
                var queryStartPos = url.IndexOf('?');
                var query = queryStartPos < 0 ? default : url.AsMemory(queryStartPos);
                queryParameterSupplier.RenderParametersFromQueryString(builder, query);
            }
        }
    }
}
