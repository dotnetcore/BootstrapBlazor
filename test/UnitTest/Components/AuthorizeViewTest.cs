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
        nav.NavigateTo("/Dog?CLASS=test&value=10&tags=one&tags=two");
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
        cut.Contains("data-value=\"10\"");
        cut.Contains("data-tags=\"one,two\"");
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

    [Fact]
    public void NoQueryParameters_Ok()
    {
        AuthorizationContext.SetAuthorized("admin");
        RenderComponent(typeof(NoQueryParametersComponent));
    }

    [Fact]
    public void DuplicateQueryParameter_Throws()
    {
        AuthorizationContext.SetAuthorized("admin");
        var exception = Assert.Throws<InvalidOperationException>(() => RenderComponent(typeof(DuplicateQueryParameterComponent)));
        Assert.Contains("declares more than one mapping for the query parameter", exception.Message);
    }

    [Fact]
    public void UnsupportedQueryParameterType_Throws()
    {
        AuthorizationContext.SetAuthorized("admin");
        var exception = Assert.Throws<NotSupportedException>(() => RenderComponent(typeof(UnsupportedQueryParameterComponent)));
        Assert.Contains("System.Int16", exception.Message);
    }

    private void RenderComponent(Type componentType)
    {
        Context.Render<CascadingAuthenticationState>(pb =>
        {
            pb.AddChildContent<BootstrapBlazorAuthorizeView>(pb => pb.Add(a => a.Type, componentType));
        });
    }

    private sealed class NoQueryParametersComponent : ComponentBase
    {
        public string? Value { get; set; }

        [Parameter]
        public string? Parameter { get; set; }
    }

    private sealed class DuplicateQueryParameterComponent : ComponentBase
    {
        [Parameter]
        [SupplyParameterFromQuery(Name = "value")]
        public string? First { get; set; }

        [Parameter]
        [SupplyParameterFromQuery(Name = "VALUE")]
        public string? Second { get; set; }
    }

    private sealed class UnsupportedQueryParameterComponent : ComponentBase
    {
        [Parameter]
        [SupplyParameterFromQuery]
        public short Value { get; set; }
    }
}
