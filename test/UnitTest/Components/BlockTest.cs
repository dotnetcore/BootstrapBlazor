// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

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

    internal static RenderFragment BuildComponent() => builder =>
    {
        builder.OpenElement(0, "div");
        builder.AddContent(1, "test");
        builder.CloseElement();
    };
}

public class BlockAuthorizationTest : AuthorizationViewTestBase
{
    [Fact]
    public void User_Ok()
    {
        AuthorizationContext.SetAuthorized("Admin");

        var cut = Context.Render<Block>(builder =>
        {
            builder.Add(a => a.Users, new List<string>() { "Admin" });
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
            builder.Add(a => a.Roles, new List<string>() { "Administrators" });
            builder.Add(a => a.ChildContent, BlockTest.BuildComponent());
        });
        Assert.Equal("<div>test</div>", cut.Markup);
    }
}
