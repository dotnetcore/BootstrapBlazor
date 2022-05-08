// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Authorization;
using System.Reflection;

namespace BootstrapBlazor.Components;

internal static class TypeEextensions
{
    public static PropertyInfo? GetPropertyByName(this Type type, string propertyName) => type.GetRuntimeProperties().FirstOrDefault(p => p.Name == propertyName);

    public static FieldInfo? GetFieldByName(this Type type, string fieldName) => type.GetRuntimeFields().FirstOrDefault(p => p.Name == fieldName);

    public static async Task<bool> IsAuthorizedAsync(this Type type, Task<AuthenticationState>? authenticateState, IAuthorizationPolicyProvider? authorizePolicy, IAuthorizationService? authorizeService, object? resource = null)
    {
        var ret = true;
        var authorizeData = AttributeAuthorizeDataCache.GetAuthorizeDataForType(type);
        if (authorizeData != null)
        {
            EnsureNoAuthenticationSchemeSpecified();

            if (authenticateState != null && authorizePolicy != null && authorizeService != null)
            {
                var currentAuthenticationState = await authenticateState;
                var user = currentAuthenticationState.User;
                var policy = await AuthorizationPolicy.CombineAsync(authorizePolicy, authorizeData);
                if (policy != null)
                {
                    var result = await authorizeService.AuthorizeAsync(user, resource, policy);
                    ret = result.Succeeded;
                }
            }
        }
        return ret;

        void EnsureNoAuthenticationSchemeSpecified()
        {
            // It's not meaningful to specify a nonempty scheme, since by the time Components
            // authorization runs, we already have a specific ClaimsPrincipal (we're stateful).
            // To avoid any confusion, ensure the developer isn't trying to specify a scheme.
            for (var i = 0; i < authorizeData.Length; i++)
            {
                var entry = authorizeData[i];
                if (!string.IsNullOrEmpty(entry.AuthenticationSchemes))
                {
                    throw new NotSupportedException($"The authorization data specifies an authentication scheme with value '{entry.AuthenticationSchemes}'. Authentication schemes cannot be specified for components.");
                }
            }
        }
    }

    public static IEnumerable<IFilterAction> ToSearchs(this IEnumerable<ITableColumn> columns, string? searchText)
    {
        var searchs = new List<IFilterAction>();
        if (!string.IsNullOrEmpty(searchText))
        {
            foreach (var col in columns)
            {
                var type = Nullable.GetUnderlyingType(col.PropertyType) ?? col.PropertyType;
                if (type == typeof(bool) && bool.TryParse(searchText, out var @bool))
                {
                    searchs.Add(new SearchFilterAction(col.GetFieldName(), @bool, FilterAction.Equal));
                }
                else if (type.IsEnum && Enum.TryParse(type, searchText, true, out object? @enum))
                {
                    searchs.Add(new SearchFilterAction(col.GetFieldName(), @enum, FilterAction.Equal));
                }
                else if (type == typeof(string))
                {
                    searchs.Add(new SearchFilterAction(col.GetFieldName(), searchText));
                }
                else if (type == typeof(int) && int.TryParse(searchText, out var @int))
                {
                    searchs.Add(new SearchFilterAction(col.GetFieldName(), @int, FilterAction.Equal));
                }
                else if (type == typeof(long) && long.TryParse(searchText, out var @long))
                {
                    searchs.Add(new SearchFilterAction(col.GetFieldName(), @long, FilterAction.Equal));
                }
                else if (type == typeof(short) && long.TryParse(searchText, out var @short))
                {
                    searchs.Add(new SearchFilterAction(col.GetFieldName(), @short, FilterAction.Equal));
                }
                else if (type == typeof(double) && double.TryParse(searchText, out var @double))
                {
                    searchs.Add(new SearchFilterAction(col.GetFieldName(), @double, FilterAction.Equal));
                }
                else if (type == typeof(float) && float.TryParse(searchText, out var @float))
                {
                    searchs.Add(new SearchFilterAction(col.GetFieldName(), @float, FilterAction.Equal));
                }
            }
        }
        return searchs;
    }
}
