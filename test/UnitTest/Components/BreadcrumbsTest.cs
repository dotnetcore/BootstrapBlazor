﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace UnitTest.Components;

public class BreadcrumbsTest : BootstrapBlazorTestBase
{
    [Fact]
    public void ButtonStyle_Ok()
    {
        var DataSource = new List<BreadcrumbItem>
        {
            new("Library"),
            new("Data")
        };

        var cut = Context.RenderComponent<Breadcrumb>(pb =>
        {
            pb.Add(b => b.Value, DataSource);
        });
        Assert.Contains("Library", cut.Markup);
        Assert.DoesNotContain("href", cut.Markup);

        DataSource.Add(new BreadcrumbItem("Home", "https://www.blazor.zone/"));
        cut.SetParametersAndRender(pb =>
        {
            pb.Add(b => b.Value, DataSource);
        });
        Assert.Contains("Home", cut.Markup);
        Assert.Contains("href", cut.Markup);

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(b => b.AdditionalAttributes, new Dictionary<string, object>() { ["tag"] = "tagok" });
        });
    }
}
