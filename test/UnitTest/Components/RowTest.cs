// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace UnitTest.Components;

public class RowTest : TestBase
{
    [Fact]
    public void RowType_Normal()
    {
        var cut = Context.RenderComponent<Row>(pb =>
        {
            pb.Add(a => a.RowType, RowType.Normal);
            pb.Add(a => a.ChildContent, CreateContent());
        });

        Assert.Contains("data-type=\"row\"", cut.Markup);
    }

    [Fact]
    public void RowType_Inline()
    {
        var cut = Context.RenderComponent<Row>(pb =>
        {
            pb.Add(a => a.RowType, RowType.Inline);
            pb.Add(a => a.ChildContent, CreateContent());
        });

        Assert.Contains("data-type=\"inline\"", cut.Markup);
    }

    [Theory]
    [InlineData(ItemsPerRow.One, 12)]
    [InlineData(ItemsPerRow.Two, 6)]
    [InlineData(ItemsPerRow.Three, 4)]
    [InlineData(ItemsPerRow.Four, 3)]
    [InlineData(ItemsPerRow.Six, 2)]
    [InlineData(ItemsPerRow.Twelve, 1)]
    public void ItemsPerRow_Ok(ItemsPerRow items, int cols)
    {
        var cut = Context.RenderComponent<Row>(pb =>
        {
            pb.Add(a => a.ItemsPerRow, items);
            pb.Add(a => a.ChildContent, CreateContent());
        });

        Assert.Contains($"data-items=\"{cols}\"", cut.Markup);
    }

    [Fact]
    public void ColSpan_Ok()
    {
        var cut = Context.RenderComponent<Row>(pb =>
        {
            pb.Add(a => a.ColSpan, 1);
            pb.Add(a => a.ChildContent, CreateContent());
        });

        Assert.Contains($"data-colspan=\"1\"", cut.Markup);
    }

    private static RenderFragment CreateContent() => new(builder =>
    {
        builder.OpenElement(0, "div");
        builder.AddContent(1, "test-content-1");
        builder.CloseElement();

        builder.OpenElement(2, "div");
        builder.AddContent(3, "test-content-2");
        builder.CloseElement();
    });
}
