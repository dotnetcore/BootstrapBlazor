// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Bunit;
using UnitTest.Core;
using BootstrapBlazor.Components;
using Microsoft.AspNetCore.Components;

namespace UnitTest.Components;

public class LogoutTest : BootstrapBlazorTestBase
{
    [Fact]
    public void ImageUrl_Ok()
    {
        // 未设置 Items
        var cut = Context.RenderComponent<Logout>(pb =>
        {
            pb.Add(a => a.ImageUrl, "test_image_url");
        });
        Assert.Contains("test_image_url", cut.Markup);
    }

    [Fact]
    public void UserName_Ok()
    {
        // 未设置 Items
        var cut = Context.RenderComponent<Logout>(pb =>
        {
            pb.Add(a => a.UserName, "admin");
        });
        Assert.Contains("admin", cut.Markup);

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.PrefixUserNameText, "prefix_username");
        });
        Assert.Contains("prefix_username", cut.Markup);
    }

    [Fact]
    public void DisplayName_Ok()
    {
        // 未设置 Items
        var cut = Context.RenderComponent<Logout>(pb =>
        {
            pb.Add(a => a.DisplayName, "administrators");
        });
        Assert.Contains("administrators", cut.Markup);

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.PrefixDisplayNameText, "prefix_displayname");
        });
        Assert.Contains("prefix_displayname", cut.Markup);
    }

    [Fact]
    public void HeaderTemplate_Ok()
    {
        // 未设置 Items
        var cut = Context.RenderComponent<Logout>(pb =>
        {
            pb.Add(a => a.HeaderTemplate, new RenderFragment(builder => builder.AddContent(0, "header_template")));
        });
        Assert.Contains("header_template", cut.Markup);
    }

    [Fact]
    public void LinkTemplate_Ok()
    {
        // 未设置 Items
        var cut = Context.RenderComponent<Logout>(pb =>
        {
            pb.Add(a => a.LinkTemplate, new RenderFragment(builder => builder.AddContent(0, "link_template")));
        });
        Assert.Contains("link_template", cut.Markup);
    }
}
