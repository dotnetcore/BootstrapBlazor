// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Bunit.TestDoubles;
using Microsoft.Extensions.DependencyInjection;

namespace UnitTest.Extensions;

public class NavigationManagerExtensionsTest : BootstrapBlazorTestBase
{
    [Fact]
    public void NavigateTo_Ok()
    {
        var nav = Context.Services.GetRequiredService<FakeNavigationManager>();
        var provider = Context.Services.GetRequiredService<IServiceProvider>();
        nav.NavigateTo(provider, "/Cat", "Cat", "fa-solid fa-font-awesome", false);
    }
}
