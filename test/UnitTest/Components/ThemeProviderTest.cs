// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace UnitTest.Components;

public class ThemeProviderTest : BootstrapBlazorTestBase
{
    [Fact]
    public void ThemeProvider_Ok()
    {
        var cut = Context.RenderComponent<ThemeProvider>(pb =>
        {
            pb.Add(a => a.ShowShadow, false);
            pb.Add(a => a.Alignment, Alignment.Center);
        });
        cut.Contains("dropdown bb-theme-mode");
        cut.DoesNotContain("dropdown-menu-center shadow");
    }
}
