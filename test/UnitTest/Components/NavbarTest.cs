// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace UnitTest.Components;

public class NavbarTest : BootstrapBlazorTestBase
{
    [Fact]
    public void Navbar_OK()
    {
        var cut = Context.RenderComponent<Navbar>(pb =>
        {
            pb.Add(s => s.Size, Size.Medium);
            pb.Add(s => s.BackgroundColorCssClass, "bg-primary");
            pb.AddChildContent<NavbarBrand>(pb =>
            {
                pb.AddChildContent("<a href=\"#\">NavbarBrand</a>");
            });
            pb.AddChildContent<NavbarToggleButton>(pb =>
            {
                pb.Add(a => a.Target, "#testId");
            });
            pb.AddChildContent<NavbarCollapse>(pb =>
            {
                pb.Add(a => a.Id, "testId");
                pb.AddChildContent<NavbarGroup>(pb =>
                {
                    pb.AddChildContent<NavbarItem>(pb =>
                    {
                        pb.AddChildContent("<a class=\"nav-link active\" aria-current=\"page\" href=\"\">Home</a>");
                    });
                });
            });
        });
        Assert.Contains("<a href=\"#\">NavbarBrand</a>", cut.Markup);
        Assert.Contains("data-bs-target=\"#testId", cut.Markup);
        Assert.Contains("collapse navbar-collapse", cut.Markup);
        Assert.Contains("navbar-nav", cut.Markup);
        Assert.Contains("<a class=\"nav-link active\" aria-current=\"page\" href=\"\">Home</a>", cut.Markup);

        var toggle = cut.FindComponent<NavbarToggleButton>();
        Assert.NotNull(toggle);

        toggle.SetParametersAndRender(pb =>
        {
            pb.AddChildContent("<a href=\"#\">ToggleButton</a>");
        });
        Assert.Contains("<a href=\"#\">ToggleButton</a>", cut.Markup);
    }
}
