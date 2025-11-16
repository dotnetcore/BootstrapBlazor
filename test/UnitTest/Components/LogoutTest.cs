// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace UnitTest.Components;

public class LogoutTest : BootstrapBlazorTestBase
{
    [Fact]
    public void ChildContent_Ok()
    {
        // 未设置 Items
        var cut = Context.Render<Logout>(pb =>
        {
            pb.Add(a => a.ChildContent, builder => builder.AddContent(0, "test_content"));
        });
        Assert.Contains("test_content", cut.Markup);
    }

    [Fact]
    public void ImageUrl_Ok()
    {
        // 未设置 Items
        var cut = Context.Render<Logout>(pb =>
        {
            pb.Add(a => a.ImageUrl, "test_image_url");
        });
        Assert.Contains("test_image_url", cut.Markup);
    }

    [Fact]
    public void UserName_Ok()
    {
        // 未设置 Items
        var cut = Context.Render<Logout>(pb =>
        {
            pb.Add(a => a.UserName, "admin");
        });
        Assert.Contains("admin", cut.Markup);

        cut.Render(pb =>
        {
            pb.Add(a => a.PrefixUserNameText, "prefix_username");
        });
        Assert.Contains("prefix_username", cut.Markup);
    }

    [Fact]
    public void ShowUserName_Ok()
    {
        // 未设置 Items
        var cut = Context.Render<Logout>(pb =>
        {
            pb.Add(a => a.UserName, "admin");
            pb.Add(a => a.ShowUserName, false);
        });
        Assert.DoesNotContain("logout-text", cut.Markup);
    }

    [Fact]
    public void DisplayName_Ok()
    {
        // 未设置 Items
        var cut = Context.Render<Logout>(pb =>
        {
            pb.Add(a => a.DisplayName, "administrators");
        });
        Assert.Contains("administrators", cut.Markup);

        cut.Render(pb =>
        {
            pb.Add(a => a.PrefixDisplayNameText, "prefix_display_name");
        });
        cut.WaitForAssertion(() => cut.Contains("prefix_display_name"));
    }

    [Fact]
    public void HeaderTemplate_Ok()
    {
        // 未设置 Items
        var cut = Context.Render<Logout>(pb =>
        {
            pb.Add(a => a.HeaderTemplate, new RenderFragment(builder => builder.AddContent(0, "header_template")));
        });
        Assert.Contains("header_template", cut.Markup);
    }

    [Fact]
    public void LinkTemplate_Ok()
    {
        // 未设置 Items
        var cut = Context.Render<Logout>(pb =>
        {
            pb.Add(a => a.LinkTemplate, new RenderFragment(builder => builder.AddContent(0, "link_template")));
        });
        Assert.Contains("link_template", cut.Markup);
    }

    [Fact]
    public void AvatarRadius_Ok()
    {
        // 未设置 Items
        var cut = Context.Render<Logout>(pb =>
        {
            pb.Add(a => a.AvatarRadius, "50%");
        });
        Assert.Contains("--bb-logout-user-avatar-border-radius: 50%;", cut.Markup);
    }
}
