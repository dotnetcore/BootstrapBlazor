// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Components;
using Bunit;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnitTest.Core;
using Xunit;

namespace UnitTest.Components;

public class BlockTest : TestBase
{
    [Fact]
    public void Show_Ok()
    {
        var cut = Context.RenderComponent<Block>(builder =>
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
        var cut = Context.RenderComponent<Block>(builder =>
        {
            builder.Add(a => a.Condition, true);
            builder.Add(a => a.Authorized, b => b.AddContent(0, "Authorizated"));
        });
        Assert.Equal("Authorizated", cut.Markup);
    }

    [Fact]
    public void NotAuthorized_Ok()
    {
        var cut = Context.RenderComponent<Block>(builder =>
        {
            builder.Add(a => a.Condition, false);
            builder.Add(a => a.NotAuthorized, b => b.AddContent(0, "NotAuthorized"));
        });
        Assert.Equal("NotAuthorized", cut.Markup);
    }

    [Fact]
    public void Hide_Ok()
    {
        var cut = Context.RenderComponent<Block>(builder =>
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

public class BlockAuthorizationTest : AuthorizationTestBase
{
    [Fact]
    public void User_Ok()
    {
        AuthorizationContext.SetAuthorized("Admin");

        var cut = Context.RenderComponent<Block>(builder =>
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

        var cut = Context.RenderComponent<Block>(builder =>
        {
            builder.Add(a => a.Roles, new List<string>() { "Administrators" });
            builder.Add(a => a.ChildContent, BlockTest.BuildComponent());
        });
        Assert.Equal("<div>test</div>", cut.Markup);
    }
}
