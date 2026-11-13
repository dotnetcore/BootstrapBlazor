// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using System.Text.Json;

namespace UnitTest.Components;

public class WatermarkTest : BootstrapBlazorTestBase
{
    private static readonly JsonSerializerOptions _option = new(JsonSerializerDefaults.Web);

    [Theory]
    [InlineData(Alignment.Center, "center")]
    [InlineData(Alignment.Left, "start")]
    [InlineData(Alignment.Right, "end")]
    [InlineData(Alignment.None, "center")]
    [InlineData((Alignment)(-1), "center")]
    public void Alignment_Ok(Alignment alignment, string expected)
    {
        var module = Context.JSInterop.SetupModule("./_content/BootstrapBlazor/Components/Watermark/Watermark.razor.js");
        var cut = Context.Render<Watermark>();

        AssertTextAlign(module.VerifyInvoke("init").Arguments[1]!, "center");

        cut.Render(pb => pb.Add(a => a.Alignment, alignment));

        AssertTextAlign(module.VerifyInvoke("update").Arguments[1]!, expected);

        static void AssertTextAlign(object options, string expected)
        {
            var json = System.Text.Json.JsonSerializer.Serialize(options, _option);
            using var document = System.Text.Json.JsonDocument.Parse(json);

            Assert.Equal(expected, document.RootElement.GetProperty("textAlign").GetString());
        }
    }

    [Fact]
    public void Watermark_Ok()
    {
        var cut = Context.Render<Watermark>(pb =>
        {
            pb.Add(a => a.Text, null);
            pb.Add(a => a.FontSize, 16);
            pb.Add(a => a.Gap, 40);
            pb.Add(a => a.Color, "#0000000D");
            pb.Add(a => a.Rotate, 40);
            pb.Add(a => a.ZIndex, 10);
            pb.Add(a => a.ChildContent, b => b.AddMarkupContent(0, "<span>Test</span>"));
        });
        cut.MarkupMatches("<div id:ignore class=\"bb-watermark\"><span>Test</span></div>");

        var ex = Assert.ThrowsAny<InvalidOperationException>(() => cut.Render(pb =>
        {
            pb.Add(a => a.IsPage, true);
        }));
        Assert.Equal($"IsPage is true, ChildContent cannot be set.", ex.Message);

        cut.Render(pb =>
        {
            pb.Add(a => a.IsPage, true);
            pb.Add(a => a.ChildContent, (RenderFragment?)null);
        });
        cut.MarkupMatches("<div id:ignore class=\"bb-watermark is-page\"></div>");
    }
}
