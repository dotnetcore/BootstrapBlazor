// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Bunit.TestDoubles;
using Microsoft.AspNetCore.Components.Authorization;
using UnitTest.Pages;

namespace UnitTest.Components;

public class AuthorizeViewTest : AuthorizationViewTestBase
{
    [Fact]
    public void NotAuthorized_Ok()
    {
        AuthorizationContext.SetNotAuthorized();
        var cut = Context.Render<CascadingAuthenticationState>(pb =>
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
        var nav = Context.Services.GetRequiredService<BunitNavigationManager>();
        nav.NavigateTo("/Dog?class=test");
        var cut = Context.Render<CascadingAuthenticationState>(pb =>
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
        var nav = Context.Services.GetRequiredService<BunitNavigationManager>();
        nav.NavigateTo("/Dog");
        var cut = Context.Render<CascadingAuthenticationState>(pb =>
        {
            pb.AddChildContent<BootstrapBlazorAuthorizeView>(pb =>
            {
                pb.Add(a => a.Type, typeof(Dog));
                pb.Add(a => a.Resource, typeof(Dog));
            });
        });
    }
}
