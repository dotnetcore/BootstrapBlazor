// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components.Routing;
using System.Collections.Concurrent;
using System.Collections.ObjectModel;
using System.Reflection;

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">BootstrapBlazorAuthorizeView 组件</para>
/// <para lang="en">BootstrapBlazorAuthorizeView Component</para>
/// </summary>
public class BootstrapBlazorAuthorizeView : ComponentBase
{
    private static readonly ConcurrentDictionary<Type, QueryParameterMapping[]> _queryParameterMappings = [];

    /// <summary>
    /// <para lang="zh">获得/设置 与路由关联的类型，默认为 null</para>
    /// <para lang="en">Gets or sets the type associated with the route. Default is null</para>
    /// </summary>
    [Parameter]
    [NotNull]
    [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties)]
    public Type? Type { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 与路由关联的参数，默认为 null</para>
    /// <para lang="en">Gets or sets the parameters associated with the route. Default is null</para>
    /// </summary>
    [Parameter]
    public IReadOnlyDictionary<string, object>? Parameters { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 用户未授权时显示的模板，默认为 null</para>
    /// <para lang="en">Gets or sets the template to display when the user is not authorized. Default is null</para>
    /// </summary>
    [Parameter]
    public RenderFragment? NotAuthorized { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 访问控制的资源，默认为 null</para>
    /// <para lang="en">Gets or sets the resource to which access is being controlled. Default is null</para>
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
            BuildQueryParameters(builder, Type, ref index);
            builder.CloseComponent();
        }
        else
        {
            builder.AddContent(0, NotAuthorized);
        }
    }

    private void BuildQueryParameters(
        RenderTreeBuilder builder,
        [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties)] Type componentType,
        ref int sequence)
    {
        var mappings = _queryParameterMappings.GetOrAdd(componentType, CreateQueryParameterMappings);
        if (mappings.Length == 0)
        {
            return;
        }

        var query = QueryHelper.ParseQuery(NavigationManager.ToAbsoluteUri(NavigationManager.Uri).Query);
        foreach (var mapping in mappings)
        {
            query.TryGetValue(mapping.QueryParameterName, out var values);

            object? value;
            if (mapping.IsArray)
            {
                value = mapping.Parser.ParseMultiple(values, mapping.ComponentParameterName);
            }
            else
            {
                value = values.Count == 0
                    ? null
                    : mapping.Parser.Parse(values[values.Count - 1].AsSpan(), mapping.ComponentParameterName);
            }

            builder.AddAttribute(sequence++, mapping.ComponentParameterName, value);
        }
    }

    private static QueryParameterMapping[] CreateQueryParameterMappings(
        [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties)] Type componentType)
    {
        var mappings = new List<QueryParameterMapping>();
        var queryParameterNames = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        foreach (var property in componentType.GetProperties(BindingFlags.Public | BindingFlags.Instance))
        {
            if (!property.IsDefined(typeof(ParameterAttribute)) ||
                property.GetCustomAttribute<SupplyParameterFromQueryAttribute>() is not { } attribute)
            {
                continue;
            }

            var queryParameterName = string.IsNullOrEmpty(attribute.Name) ? property.Name : attribute.Name;
            if (!queryParameterNames.Add(queryParameterName))
            {
                throw new InvalidOperationException(
                    $"The component '{componentType}' declares more than one mapping for the query parameter '{queryParameterName}'.");
            }

            var isArray = property.PropertyType.IsArray;
            var targetType = isArray ? property.PropertyType.GetElementType()! : property.PropertyType;
            if (!RouteValueConverter.TryGet(targetType, out var parser))
            {
                throw new NotSupportedException($"Querystring values cannot be parsed as type '{property.PropertyType}'.");
            }

            mappings.Add(new(property.Name, queryParameterName, parser, isArray));
        }

        return [.. mappings];
    }

    private sealed record QueryParameterMapping(
        string ComponentParameterName,
        string QueryParameterName,
        RouteValueConverter Parser,
        bool IsArray);
}
