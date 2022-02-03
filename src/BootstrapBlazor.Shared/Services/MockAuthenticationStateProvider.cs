// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace BootstrapBlazor.Shared.Services;

/// <summary>
/// 
/// </summary>
internal class MockAuthenticationStateProvider : AuthenticationStateProvider, IHostEnvironmentAuthenticationStateProvider
{
    private Task<AuthenticationState>? _authenticationStateTask;

    private bool IsAuth { get; set; }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        AuthenticationState state = await _authenticationStateTask!;

        if (IsAuth)
        {
            var isAuth = state.User.Identity?.IsAuthenticated ?? false;
            if (!isAuth)
            {
                var claimsIdentity = new ClaimsIdentity(new Claim[]
                {
                        new(ClaimTypes.Name, "BootstrapBlazor"),
                        new(ClaimTypes.Role, "User")
                }, "Blazor");
                state = new AuthenticationState(new ClaimsPrincipal(claimsIdentity));
                _authenticationStateTask = Task.FromResult(state);
                NotifyAuthenticationStateChanged(_authenticationStateTask);
            }
        }
        return state;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="authenticationStateTask"></param>
    /// <exception cref="System.NotImplementedException"></exception>
    public void SetAuthenticationState(Task<AuthenticationState> authenticationStateTask)
    {
        _authenticationStateTask = authenticationStateTask ?? throw new ArgumentNullException(nameof(authenticationStateTask));
        NotifyAuthenticationStateChanged(_authenticationStateTask);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <exception cref="NotImplementedException"></exception>
    public void Logout()
    {
        IsAuth = false;
        _authenticationStateTask = Task.FromResult(new AuthenticationState(new ClaimsPrincipal()));
        NotifyAuthenticationStateChanged(_authenticationStateTask);
    }

    /// <summary>
    /// 
    /// </summary>
    public void Login()
    {
        IsAuth = true;
    }
}
