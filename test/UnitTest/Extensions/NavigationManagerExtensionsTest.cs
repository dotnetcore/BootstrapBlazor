// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Bunit.TestDoubles;

namespace UnitTest.Extensions;

public class NavigationManagerExtensionsTest : BootstrapBlazorTestBase
{
    [Fact]
    public void NavigateTo_Ok()
    {
        var nav = Context.Services.GetRequiredService<BunitNavigationManager>();
        var provider = Context.Services.GetRequiredService<IServiceProvider>();
        nav.NavigateTo(provider, "/Cat", "Cat", "fa-solid fa-font-awesome", false);
    }

    [Fact]
    public void ToBaseRelativePathWithoutQueryAndFragment_Ok()
    {
        var nav = Context.Services.GetRequiredService<BunitNavigationManager>();
        nav.NavigateTo("/test?test1=1");
        Assert.Equal("test", nav.ToBaseRelativePathWithoutQueryAndFragment());

        nav.NavigateTo("/test#1234");
        Assert.Equal("test", nav.ToBaseRelativePathWithoutQueryAndFragment());
    }
}
