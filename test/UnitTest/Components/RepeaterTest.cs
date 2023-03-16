// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Shared;

namespace UnitTest.Components;

public class RepeaterTest : BootstrapBlazorTestBase
{
    [Fact]
    public void Css_Ok()
    {
        var cut = Context.RenderComponent<Repeater<Foo>>(pb =>
        {
            pb.Add(a => a.Items, null);
            pb.Add(a => a.AdditionalAttributes, new Dictionary<string, object>() { { "class", "class_repeater_test" } });
        });
        cut.Contains("class_repeater_test");
    }
    [Fact]
    public void ShowLoading_Ok()
    {
        var cut = Context.RenderComponent<Repeater<Foo>>(pb =>
        {
            pb.Add(a => a.Items, null);
        });
        cut.Contains("repeater-loading");

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.LoadingTemplate, builder => builder.AddContent(0, "Loading-Template"));
        });
        cut.Contains("Loading-Template");

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.ShowLoading, false);
        });
        cut.DoesNotContain("repeater-loading");
        cut.DoesNotContain("Loading-Template");
    }

    [Fact]
    public void ShowEmpty_Ok()
    {
        var cut = Context.RenderComponent<Repeater<Foo>>(pb =>
        {
            pb.Add(a => a.Items, new List<Foo>());
        });
        Assert.NotNull(cut.Instance.EmptyText);

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.EmptyTemplate, builder => builder.AddContent(0, "Empty-Template"));
        });
        cut.Contains("Empty-Template");

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.ShowEmpty, false);
        });
        cut.DoesNotContain("Empty-Template");
    }

    [Fact]
    public void ItemTemplate_Ok()
    {
        var cut = Context.RenderComponent<Repeater<Foo>>(pb =>
        {
            pb.Add(a => a.Items, new List<Foo>() { new() { Name = "Test1" } });
            pb.Add(a => a.ItemTemplate, new RenderFragment<Foo>(foo => builder => builder.AddContent(0, foo.Name)));
        });

        cut.Contains("Test1");
    }

    [Fact]
    public void ContainerTemplate_Ok()
    {
        var cut = Context.RenderComponent<Repeater<Foo>>(pb =>
        {
            pb.Add(a => a.Items, new List<Foo>() { new() { Name = "Test1" } });
            pb.Add(a => a.ItemTemplate, new RenderFragment<Foo>(foo => builder => builder.AddContent(0, foo.Name)));
            pb.Add(a => a.ContainerTemplate, new RenderFragment<RenderFragment>(f => builder =>
            {
                builder.AddContent(0, "Container-Template");
                builder.AddContent(1, f);
            }));
        });
        cut.Contains("Container-Template");
        cut.Contains("Test1");
    }
}
