// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

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
