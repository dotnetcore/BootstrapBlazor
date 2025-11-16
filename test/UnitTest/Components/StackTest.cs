// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace UnitTest.Components;

public class StackTest : BootstrapBlazorTestBase
{
    [Theory]
    [InlineData(true, false, "flex-row")]
    [InlineData(true, true, "flex-row flex-row-reverse")]
    [InlineData(false, false, "flex-column")]
    [InlineData(false, true, "flex-column flex-column-reverse")]
    public void Stack_IsRow_IsReverse(bool isRow, bool isReverse, string expected)
    {
        var cut = Context.Render<Stack>(pb =>
        {
            pb.Add(a => a.IsRow, isRow);
            pb.Add(a => a.IsReverse, isReverse);
            pb.AddChildContent(pb =>
            {
                pb.AddMarkupContent(0, "<StackItem><div class=\"stack-item-demo\">Item 1</div></StackItem>");
                pb.AddMarkupContent(1, "<StackItem><div class=\"stack-item-demo\">Item 2</div></StackItem>");
            });
        });
        cut.Contains(expected);
    }

    [Theory]
    [InlineData(true, false, "flex-wrap")]
    [InlineData(true, true, "flex-wrap flex-wrap-reverse")]
    [InlineData(false, false, "flex-nowrap")]
    [InlineData(false, true, "flex-nowrap")]
    public void Stack_IsWrap_IsReverse(bool isWrap, bool isReverse, string expected)
    {
        var cut = Context.Render<Stack>(pb =>
        {
            pb.Add(a => a.IsWrap, isWrap);
            pb.Add(a => a.IsReverse, isReverse);
            pb.AddChildContent(pb =>
            {
                pb.AddMarkupContent(0, "<StackItem><div class=\"stack-item-demo\">Item 1</div></StackItem>");
                pb.AddMarkupContent(1, "<StackItem><div class=\"stack-item-demo\">Item 2</div></StackItem>");
            });
        });
        cut.Contains(expected);
    }

    [Theory]
    [InlineData(StackJustifyContent.Between)]
    [InlineData(StackJustifyContent.End)]
    [InlineData(StackJustifyContent.Around)]
    [InlineData(StackJustifyContent.Start)]
    [InlineData(StackJustifyContent.Center)]
    [InlineData(StackJustifyContent.Evenly)]
    public void Stack_Justify(StackJustifyContent justify)
    {
        var cut = Context.Render<Stack>(pb =>
        {
            pb.Add(a => a.Justify, justify);
            pb.AddChildContent(pb =>
            {
                pb.AddMarkupContent(0, "<StackItem><div class=\"stack-item-demo\">Item 1</div></StackItem>");
                pb.AddMarkupContent(1, "<StackItem><div class=\"stack-item-demo\">Item 2</div></StackItem>");
            });
        });
        cut.Contains(justify.ToDescriptionString());
    }

    [Theory]
    [InlineData(StackAlignItems.Baseline)]
    [InlineData(StackAlignItems.End)]
    [InlineData(StackAlignItems.Stretch)]
    [InlineData(StackAlignItems.Start)]
    [InlineData(StackAlignItems.Center)]
    public void Stack_AlignItems(StackAlignItems align)
    {
        var cut = Context.Render<Stack>(pb =>
        {
            pb.Add(a => a.AlignItems, align);
            pb.AddChildContent<StackItem>(pb =>
            {
                pb.Add(a => a.ChildContent, builder => builder.AddContent(0, "<div class=\"stack-item-demo\">Item 1</div>"));
            });
            pb.AddChildContent<StackItem>(pb =>
            {
                pb.Add(a => a.ChildContent, builder => builder.AddContent(0, "<div class=\"stack-item-demo\">Item 2</div>"));
            });
        });
        cut.Contains(align.ToDescriptionString());
    }

    [Fact]
    public void StackItem_IsFill()
    {
        var cut = Context.Render<StackItem>(pb =>
        {
            pb.Add(a => a.IsFill, true);
            pb.Add(a => a.ChildContent, builder => builder.AddContent(0, new MarkupString("<div class=\"stack-item-demo\">Item 1</div>")));
        });
        cut.MarkupMatches("<div class=\"stack-item-demo\">Item 1</div>");
    }

    [Theory]
    [InlineData(StackAlignItems.Baseline)]
    [InlineData(StackAlignItems.End)]
    [InlineData(StackAlignItems.Start)]
    [InlineData(StackAlignItems.Center)]
    public void StackItem_AlignSelf(StackAlignItems align)
    {
        var cut = Context.Render<Stack>(pb =>
        {
            pb.AddChildContent<StackItem>(pb =>
            {
                pb.Add(a => a.ChildContent, builder => builder.AddContent(0, "<div class=\"stack-item-demo\">Item 1</div>"));
            });
            pb.AddChildContent<StackItem>(pb =>
            {
                pb.Add(a => a.AlignSelf, align);
                pb.Add(a => a.ChildContent, builder => builder.AddContent(0, "<div class=\"stack-item-demo\">Item 2</div>"));
            });
        });
        cut.Contains($"bb-stack-item {align.ToDescriptionString().Replace("align-items", "align-self")}");
    }
}
