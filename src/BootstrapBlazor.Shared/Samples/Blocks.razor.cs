// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Shared.Services;

using Microsoft.AspNetCore.Components.Authorization;

namespace BootstrapBlazor.Shared.Samples;

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

    private IEnumerable<AttributeItem> GetAttributes() => new AttributeItem[]
    {
        new AttributeItem()
        {
            Name = nameof(Block.OnQueryCondition),
            Description = Localizer["OnQueryCondition"],
            Type = "Func<Task<bool>>",
            ValueList = " — ",
            DefaultValue = "true"
        },
        new AttributeItem()
        {
            Name = nameof(Block.ChildContent),
            Description = Localizer["ChildContent"],
            Type = "RenderFragment",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new AttributeItem()
        {
            Name = nameof(Block.Authorized),
            Description = Localizer["Authorized"],
            Type = "RenderFragment",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new AttributeItem()
        {
            Name = nameof(Block.NotAuthorized),
            Description = Localizer["NotAuthorized"],
            Type = "RenderFragment",
            ValueList = " — ",
            DefaultValue = " — "
        }
    };
}
