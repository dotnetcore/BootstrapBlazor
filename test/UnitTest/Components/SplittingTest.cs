// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace UnitTest.Components;

/// <summary>
/// Splitting 组件单元测试
/// </summary>
public class SplittingTest : BootstrapBlazorTestBase
{
    [Fact]
    public void Splitting_Ok()
    {
        var cut = Context.RenderComponent<Splitting>();
        cut.Contains("class=\"splitting\"");
    }

    [Fact]
    public void Splitting_Text()
    {
        var cut = Context.RenderComponent<Splitting>();
        var textElement = cut.Find(".splitting-text");
        Assert.Equal("正在加载 ...", textElement.InnerHtml);

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.Text, "test-load-text");
        });
        Assert.Equal("test-load-text", textElement.InnerHtml);

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.ShowLoadingText, false);
        });
        Assert.DoesNotContain("splitting-text", cut.Markup);
    }

    [Fact]
    public void Splitting_Column()
    {
        var cut = Context.RenderComponent<Splitting>();
        var el = cut.Find(".splitting");
        Assert.Equal("10", el.GetAttribute("data-bb-columns"));

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.Columns, 20);
        });
        Assert.Equal("20", el.GetAttribute("data-bb-columns"));
    }

    [Fact]
    public void Splitting_Repeat()
    {
        var cut = Context.RenderComponent<Splitting>();
        var el = cut.Find(".splitting");
        Assert.Equal("-1", el.GetAttribute("data-bb-repeat"));

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.Repeat, 0);
        });
        Assert.Equal("0", el.GetAttribute("data-bb-repeat"));
    }

    [Fact]
    public void Splitting_Color()
    {
        var cut = Context.RenderComponent<Splitting>();
        var element = cut.Find(".splitting-flip");
        Assert.True(element.ClassList.Contains("bg-primary"));

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.Color, Color.Info);
        });
        Assert.False(element.ClassList.Contains("bg-primary"));
        Assert.True(element.ClassList.Contains("bg-info"));

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.Color, Color.None);
        });
        Assert.True(element.ClassList.Contains("bg-primary"));
    }
}
