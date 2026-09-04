// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace UnitTest.Components;

public class BlockTest : TestBase
{
    [Fact]
    public void Show_Ok()
    {
        var cut = Context.Render<Block>(builder =>
        {
            builder.Add(a => a.Name, "Test");
            builder.Add(a => a.OnQueryCondition, new Func<string, Task<bool>>(name => Task.FromResult(name == "Test")));
            builder.Add(a => a.ChildContent, BuildComponent());
        });
        Assert.Equal("<div>test</div>", cut.Markup);
    }

    [Fact]
    public void Authorized_Ok()
    {
        var cut = Context.Render<Block>(builder =>
        {
            builder.Add(a => a.Condition, true);
            builder.Add(a => a.Authorized, b => b.AddContent(0, "Authorized"));
        });
        Assert.Equal("Authorized", cut.Markup);
    }

    [Fact]
    public void NotAuthorized_Ok()
    {
        var cut = Context.Render<Block>(builder =>
        {
            builder.Add(a => a.Condition, false);
            builder.Add(a => a.NotAuthorized, b => b.AddContent(0, "NotAuthorized"));
        });
        Assert.Equal("NotAuthorized", cut.Markup);
    }

    [Fact]
    public void Hide_Ok()
    {
        var cut = Context.Render<Block>(builder =>
        {
            builder.Add(a => a.OnQueryCondition, new Func<string, Task<bool>>(_ => Task.FromResult(false)));
            builder.Add(a => a.ChildContent, BuildComponent());
        });
        Assert.Equal("", cut.Markup);
    }

    [Fact]
    public void ResetCondition_Ok()
    {
        var cut = Context.Render<Block>(builder =>
        {
            builder.Add(a => a.Condition, true);
            builder.Add(a => a.ChildContent, BuildComponent());
        });

        cut.Render(parameters => parameters
            .Add(a => a.Condition, null)
            .Add(a => a.ChildContent, BuildComponent()));

        Assert.Equal("", cut.Markup);
    }

    [Fact]
    public void NullIdentity_HidesContent_Ok()
    {
        Context.Services.AddSingleton<AuthenticationStateProvider, NullIdentityAuthenticationStateProvider>();

        var cut = Context.Render<Block>(builder =>
        {
            builder.Add(a => a.Users, ["Admin"]);
            builder.Add(a => a.ChildContent, BuildComponent());
        });

        Assert.Equal("", cut.Markup);
    }

    internal static RenderFragment BuildComponent() => builder =>
    {
        builder.OpenElement(0, "div");
        builder.AddContent(1, "test");
        builder.CloseElement();
    };

    private sealed class NullIdentityAuthenticationStateProvider : AuthenticationStateProvider
    {
        public override Task<AuthenticationState> GetAuthenticationStateAsync() => Task.FromResult(new AuthenticationState(new ClaimsPrincipal()));
    }
}

public class BlockAuthorizationTest : AuthorizationViewTestBase
{
    [Fact]
    public void User_Ok()
    {
        AuthorizationContext.SetAuthorized("Admin");

        var cut = Context.Render<Block>(builder =>
        {
            builder.Add(a => a.Users, ["Admin"]);
            builder.Add(a => a.ChildContent, BlockTest.BuildComponent());
        });
        Assert.Equal("<div>test</div>", cut.Markup);
    }

    [Fact]
    public void Role_Ok()
    {
        AuthorizationContext.SetRoles("Administrators");

        var cut = Context.Render<Block>(builder =>
        {
            builder.Add(a => a.Roles, ["Administrators"]);
            builder.Add(a => a.ChildContent, BlockTest.BuildComponent());
        });
        Assert.Equal("<div>test</div>", cut.Markup);
    }

    [Fact]
    public void Users_NotEmpty_Ok()
    {
        AuthorizationContext.SetAuthorized("Admin");

        var cut = Context.Render<Block>(builder =>
        {
            builder.Add(a => a.Users, ["User", "Admin"]);
            builder.Add(a => a.ChildContent, BlockTest.BuildComponent());
        });

        Assert.Equal("<div>test</div>", cut.Markup);
    }

    [Fact]
    public void Roles_NotEmpty_Ok()
    {
        AuthorizationContext.SetRoles("Administrators");

        var cut = Context.Render<Block>(builder =>
        {
            builder.Add(a => a.Roles, ["Users", "Administrators"]);
            builder.Add(a => a.ChildContent, BlockTest.BuildComponent());
        });

        Assert.Equal("<div>test</div>", cut.Markup);
    }

    [Fact]
    public void UsersAndRoles_Match_Ok()
    {
        AuthorizationContext.SetAuthorized("Admin");
        AuthorizationContext.SetRoles("Administrators");

        var cut = Context.Render<Block>(builder =>
        {
            builder.Add(a => a.Users, ["Admin"]);
            builder.Add(a => a.Roles, ["Administrators"]);
            builder.Add(a => a.ChildContent, BlockTest.BuildComponent());
        });

        Assert.Equal("<div>test</div>", cut.Markup);
    }

    [Fact]
    public void UsersAndRoles_RequireBoth_Ok()
    {
        AuthorizationContext.SetAuthorized("Admin");

        var cut = Context.Render<Block>(builder =>
        {
            builder.Add(a => a.Users, ["Admin"]);
            builder.Add(a => a.Roles, ["Administrators"]);
            builder.Add(a => a.ChildContent, BlockTest.BuildComponent());
        });

        Assert.Equal("", cut.Markup);
    }

    [Fact]
    public void UserMismatch_HidesContent_Ok()
    {
        AuthorizationContext.SetAuthorized("Admin");

        var cut = Context.Render<Block>(builder =>
        {
            builder.Add(a => a.Users, ["User"]);
            builder.Add(a => a.ChildContent, BlockTest.BuildComponent());
        });

        Assert.Equal("", cut.Markup);
    }

    [Fact]
    public void RoleMismatch_HidesContent_Ok()
    {
        AuthorizationContext.SetRoles("Users");

        var cut = Context.Render<Block>(builder =>
        {
            builder.Add(a => a.Roles, ["Administrators"]);
            builder.Add(a => a.ChildContent, BlockTest.BuildComponent());
        });

        Assert.Equal("", cut.Markup);
    }

    [Fact]
    public void UnauthorizedUser_HidesContent_Ok()
    {
        AuthorizationContext.SetNotAuthorized();

        var cut = Context.Render<Block>(builder =>
        {
            builder.Add(a => a.Users, ["Admin"]);
            builder.Add(a => a.ChildContent, BlockTest.BuildComponent());
        });

        Assert.Equal("", cut.Markup);
    }

    [Fact]
    public void EmptyUsers_DoesNotRestrict_Ok()
    {
        AuthorizationContext.SetAuthorized("Admin");

        var cut = Context.Render<Block>(builder =>
        {
            builder.Add(a => a.Users, Array.Empty<string>());
            builder.Add(a => a.ChildContent, BlockTest.BuildComponent());
        });

        Assert.Equal("<div>test</div>", cut.Markup);
    }

    [Fact]
    public void Users_EnumeratedOnce_Ok()
    {
        AuthorizationContext.SetAuthorized("Admin");
        var enumerationCount = 0;

        IEnumerable<string> Users()
        {
            enumerationCount++;
            yield return "Admin";
        }

        var cut = Context.Render<Block>(builder =>
        {
            builder.Add(a => a.Users, Users());
            builder.Add(a => a.ChildContent, BlockTest.BuildComponent());
        });

        Assert.Equal("<div>test</div>", cut.Markup);
        Assert.Equal(1, enumerationCount);
    }
}
