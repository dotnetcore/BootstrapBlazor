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
        var cut = Context.Render<Navbar>(pb =>
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
                    pb.Add(a => a.IsScrolling, true);
                    pb.AddChildContent<NavbarItem>(pb =>
                    {
                        pb.AddChildContent("<a class=\"nav-link active\" aria-current=\"page\" href=\"\">Home</a>");
                    });
                    pb.AddChildContent<NavbarLink>(pb =>
                    {
                        pb.Add(a => a.Url, "#");
                        pb.Add(a => a.ImageUrl, "https://example.com/logo.png");
                        pb.Add(a => a.ImageCss, "logo-class");
                        pb.Add(a => a.Target, "_blank");
                        pb.AddChildContent("HomeLink");
                    });
                    pb.AddChildContent<NavbarDropdown>(pb =>
                    {
                        pb.Add(a => a.Direction, Direction.Dropup);
                        pb.Add(a => a.MenuAlignment, Alignment.Right);
                        pb.Add(a => a.Text, "Dropdown");
                        pb.AddChildContent<NavbarDropdownItem>(pb =>
                        {
                            pb.AddChildContent("<a class=\"dropdown-item\" href=\"#\">Action</a>");
                        });
                        pb.AddChildContent<NavbarDropdownDivider>();
                        pb.AddChildContent<NavbarDropdownItem>(pb =>
                        {
                            pb.Add(a => a.Url, "#");
                            pb.Add(a => a.Target, "_blank");
                        });
                    });
                });
            });
        });
        Assert.Contains("<a href=\"#\">NavbarBrand</a>", cut.Markup);
        Assert.Contains("data-bs-target=\"#testId", cut.Markup);
        Assert.Contains("collapse navbar-collapse", cut.Markup);
        Assert.Contains("navbar-nav", cut.Markup);
        Assert.Contains("<a class=\"nav-link active\" aria-current=\"page\" href=\"\">Home</a>", cut.Markup);
        Assert.Contains("<img src=\"https://example.com/logo.png\" class=\"logo-class\" />HomeLink", cut.Markup);

        var toggle = cut.FindComponent<NavbarToggleButton>();
        Assert.NotNull(toggle);

        toggle.Render(pb =>
        {
            pb.AddChildContent("<a href=\"#\">ToggleButton</a>");
        });
        Assert.Contains("<a href=\"#\">ToggleButton</a>", cut.Markup);
    }

    [Fact]
    public async Task NavbarLink_Ok()
    {
        var cut = Context.Render<NavbarLink>(pb =>
        {
            pb.Add(a => a.Icon, "fa-test");
            pb.Add(a => a.Text, "Home");
            pb.Add(a => a.IsAsync, true);
            pb.Add(a => a.OnClick, async () =>
            {
                await Task.Yield();
            });
        });

        Assert.Contains("<i class=\"fa-test\"></i><span>Home</span>", cut.Markup);

        var link = cut.Find("a");
        Assert.NotNull(link);

        await cut.InvokeAsync(() => link.Click());
    }
}
