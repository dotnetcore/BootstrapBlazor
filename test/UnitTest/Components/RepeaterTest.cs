// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace UnitTest.Components;

public class RepeaterTest : BootstrapBlazorTestBase
{
    [Fact]
    public void Css_Ok()
    {
        var cut = Context.Render<Repeater<Foo>>(pb =>
        {
            pb.Add(a => a.Items, null);
            pb.Add(a => a.AdditionalAttributes, new Dictionary<string, object>() { { "class", "class_repeater_test" } });
        });
        cut.Contains("class_repeater_test");
    }
    [Fact]
    public void ShowLoading_Ok()
    {
        var cut = Context.Render<Repeater<Foo>>(pb =>
        {
            pb.Add(a => a.Items, null);
        });
        cut.Contains("repeater-loading");

        cut.Render(pb =>
        {
            pb.Add(a => a.LoadingTemplate, builder => builder.AddContent(0, "Loading-Template"));
        });
        cut.Contains("Loading-Template");

        cut.Render(pb =>
        {
            pb.Add(a => a.ShowLoading, false);
        });
        cut.DoesNotContain("repeater-loading");
        cut.DoesNotContain("Loading-Template");
    }

    [Fact]
    public void ShowEmpty_Ok()
    {
        var cut = Context.Render<Repeater<Foo>>(pb =>
        {
            pb.Add(a => a.Items, new List<Foo>());
        });
        Assert.NotNull(cut.Instance.EmptyText);

        cut.Render(pb =>
        {
            pb.Add(a => a.EmptyTemplate, builder => builder.AddContent(0, "Empty-Template"));
        });
        cut.Contains("Empty-Template");

        cut.Render(pb =>
        {
            pb.Add(a => a.ShowEmpty, false);
        });
        cut.DoesNotContain("Empty-Template");
    }

    [Fact]
    public void ItemTemplate_Ok()
    {
        var cut = Context.Render<Repeater<Foo>>(pb =>
        {
            pb.Add(a => a.Items, new List<Foo>() { new() { Name = "Test1" } });
            pb.Add(a => a.ItemTemplate, new RenderFragment<Foo>(foo => builder => builder.AddContent(0, foo.Name)));
        });

        cut.Contains("Test1");
    }

    [Fact]
    public void ContainerTemplate_Ok()
    {
        var cut = Context.Render<Repeater<Foo>>(pb =>
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
