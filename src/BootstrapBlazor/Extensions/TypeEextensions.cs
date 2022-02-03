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
}
