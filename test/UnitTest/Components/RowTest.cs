// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace UnitTest.Components;

public class RowTest : BootstrapBlazorTestBase
{
    [Fact]
    public void RowType_Normal()
    {
        var cut = Context.Render<Row>(pb =>
        {
            pb.Add(a => a.RowType, RowType.Normal);
            pb.Add(a => a.ChildContent, CreateContent());
            pb.Add(a => a.AdditionalAttributes, new Dictionary<string, object>()
            {
                { "class", "test-row" }
            });
        });

        Assert.Contains("data-bb-type=\"row\"", cut.Markup);
        Assert.Contains("class=\"d-none test-row\"", cut.Markup);
    }

    [Fact]
    public void RowType_Inline()
    {
        var cut = Context.Render<Row>(pb =>
        {
            pb.Add(a => a.RowType, RowType.Inline);
            pb.Add(a => a.ChildContent, CreateContent());
        });

        Assert.Contains("data-bb-type=\"inline\"", cut.Markup);
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
        var cut = Context.Render<Row>(pb =>
        {
            pb.Add(a => a.ItemsPerRow, items);
            pb.Add(a => a.ChildContent, CreateContent());
        });

        Assert.Contains($"data-bb-items=\"{cols}\"", cut.Markup);
    }

    [Fact]
    public void ColSpan_Ok()
    {
        var cut = Context.Render<Row>(pb =>
        {
            pb.Add(a => a.ColSpan, 1);
            pb.Add(a => a.ChildContent, CreateContent());
        });

        Assert.Contains($"data-bb-colspan=\"1\"", cut.Markup);
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
