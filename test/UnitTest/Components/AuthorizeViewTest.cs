// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Bunit.TestDoubles;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.DependencyInjection;
using UnitTest.Pages;

namespace UnitTest.Components;

public class AuthorizeViewTest : AuthorizateViewTestBase
{
    [Fact]
    public void NotAuthorized_Ok()
    {
        AuthorizationContext.SetNotAuthorized();
        var cut = Context.RenderComponent<CascadingAuthenticationState>(pb =>
        {
            pb.AddChildContent<BootstrapBlazorAuthorizeView>(pb =>
            {
                pb.Add(a => a.Type, typeof(Dog));
                pb.Add(a => a.Parameters, new Dictionary<string, object>()
                {
                    [nameof(Dog.Parameter1)] = "Dog"
                });
                pb.Add(a => a.NotAuthorized, new RenderFragment(builder =>
                {
                    builder.AddContent(0, "NotAuthorized");
                }));
            });
        });
        cut.Contains("NotAuthorized");
    }

    [Fact]
    public void Authorized_Ok()
    {
        AuthorizationContext.SetAuthorized("admin");
        var nav = Context.Services.GetRequiredService<FakeNavigationManager>();
        nav.NavigateTo("/Dog?class=test");
        var cut = Context.RenderComponent<CascadingAuthenticationState>(pb =>
        {
            pb.AddChildContent<BootstrapBlazorAuthorizeView>(pb =>
            {
                pb.Add(a => a.Type, typeof(Dog));
                pb.Add(a => a.Parameters, new Dictionary<string, object>()
                {
                    [nameof(Dog.Parameter1)] = "Dog"
                });
            });
        });
        cut.Contains("Dog");
        cut.Contains("class=\"test\"");
    }

    [Fact]
    public void Resource_Ok()
    {
        AuthorizationContext.SetAuthorized("admin");
        var nav = Context.Services.GetRequiredService<FakeNavigationManager>();
        nav.NavigateTo("/Dog");
        var cut = Context.RenderComponent<CascadingAuthenticationState>(pb =>
        {
            pb.AddChildContent<BootstrapBlazorAuthorizeView>(pb =>
            {
                pb.Add(a => a.Type, typeof(Dog));
                pb.Add(a => a.Resource, typeof(Dog));
            });
        });
    }
}
