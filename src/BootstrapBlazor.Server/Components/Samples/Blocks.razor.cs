// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.AspNetCore.Components.Authorization;

namespace BootstrapBlazor.Server.Components.Samples;

/// <summary>
/// 
/// </summary>
public partial class Blocks
{
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

    private async Task ToggleAuthor()
    {
        if (AuthenticationStateProvider is MockAuthenticationStateProvider mock)
        {
            if (!IsAuth)
            {
                mock.Login();
                var state = await mock.GetAuthenticationStateAsync();
                UserName = state.User.Identity?.Name;
                IsAuth = state.User.Identity?.IsAuthenticated ?? false;
            }
            else
            {
                mock.Logout();
                IsAuth = false;
                UserName = "";
            }
        }
    }

    private IEnumerable<string> Roles { get; } = new string[] { "User" };

    private Task<bool> OnQueryCondition2(string name) => Task.FromResult(IsShow2);

    private Task<bool> OnQueryUser(string name) => Task.FromResult(IsAuth);
}
