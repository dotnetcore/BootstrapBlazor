// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

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
