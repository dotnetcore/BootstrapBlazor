// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.AspNetCore.Components.Authorization;

namespace BootstrapBlazor.Server.Components.Samples;

/// <summary>
/// 
/// </summary>
public partial class Blocks : IAsyncDisposable
{
    private bool _isDisposed;
    private long _authenticationStateVersion;

    private bool IsShow { get; set; } = true;

    private string GetIcon() => IsShow ? "fa-solid fa-eye-slash" : "fa-solid fa-eye";

    private string GetText() => IsShow ? Localizer["IsHide"] : Localizer["IsShow"];

    private void ToggleCondition() => IsShow = !IsShow;

    private Task<bool> OnQueryCondition(string name) => Task.FromResult(IsShow);

    private bool IsShow2 { get; set; } = true;

    private void ToggleCondition2() => IsShow2 = !IsShow2;

    private string GetIcon2() => IsShow2 ? "fa-solid fa-eye-slash" : "fa-solid fa-eye";

    private string GetText2() => IsShow2 ? Localizer["IsHide"] : Localizer["IsShow"];

    [Inject]
    [NotNull]
    private AuthenticationStateProvider? AuthenticationStateProvider { get; set; }

    private bool IsAuth { get; set; }

    private IEnumerable<string> Users { get; } = new string[] { "BootstrapBlazor" };

    [NotNull]
    private string? UserName { get; set; }

    private string GetUser() => IsAuth ? "fa-solid fa-user-secret" : "fa-solid fa-user";

    private string GetUserText() => IsAuth ? Localizer["Logout"] : Localizer["Login"];

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();

        AuthenticationStateProvider.AuthenticationStateChanged += OnAuthenticationStateChanged;
        await UpdateAuthenticationStateAsync(AuthenticationStateProvider.GetAuthenticationStateAsync());
    }

    private void OnAuthenticationStateChanged(Task<AuthenticationState> authenticationStateTask)
        => _ = UpdateAuthenticationStateAsync(authenticationStateTask);

    private async Task UpdateAuthenticationStateAsync(Task<AuthenticationState> authenticationStateTask)
    {
        var version = Interlocked.Increment(ref _authenticationStateVersion);
        try
        {
            await InvokeAsync(async () =>
            {
                if (_isDisposed || version != Interlocked.Read(ref _authenticationStateVersion))
                {
                    return;
                }

                var state = await authenticationStateTask;
                if (_isDisposed || version != Interlocked.Read(ref _authenticationStateVersion))
                {
                    return;
                }

                IsAuth = state.User.Identity?.IsAuthenticated ?? false;
                UserName = IsAuth ? state.User.Identity?.Name ?? "" : "";
                StateHasChanged();
            });
        }
        catch (Exception ex)
        {
            await DispatchExceptionAsync(ex);
        }
    }

    private async Task ToggleAuthor()
    {
        if (AuthenticationStateProvider is MockAuthenticationStateProvider mock)
        {
            if (!IsAuth)
            {
                mock.Login();
                await mock.GetAuthenticationStateAsync();
            }
            else
            {
                mock.Logout();
            }
        }
    }

    private IEnumerable<string> Roles { get; } = new string[] { "User" };

    private Task<bool> OnQueryCondition2(string name) => Task.FromResult(IsShow2);

    private Task<bool> OnQueryUser(string name) => Task.FromResult(IsAuth);

    private ValueTask DisposeAsync(bool disposing)
    {
        if (disposing && !_isDisposed)
        {
            _isDisposed = true;
            AuthenticationStateProvider.AuthenticationStateChanged -= OnAuthenticationStateChanged;
        }

        return ValueTask.CompletedTask;
    }

    async ValueTask IAsyncDisposable.DisposeAsync()
    {
        await DisposeAsync(true);
        GC.SuppressFinalize(this);
    }
}
