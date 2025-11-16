// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace UnitTest.Components;

public class ColorPickerTest : BootstrapBlazorTestBase
{
    [Fact]
    public void DisplayText_OK()
    {
        var cut = Context.Render<ColorPicker>(builder =>
        {
            builder.Add(a => a.ShowLabel, true);
            builder.Add(a => a.DisplayText, "Test_Color");
        });

        Assert.Equal("Test_Color", cut.Find("label").TextContent);
    }

    [Fact]
    public void Template_OK()
    {
        var cut = Context.Render<ColorPicker>(builder =>
        {
            builder.Add(a => a.Template, v => builder => builder.AddContent(0, $"Test-{v}"));
            builder.Add(a => a.Value, "#AABBCC");
        });

        cut.Contains("Test-#AABBCC");
    }

    [Fact]
    public async Task Formatter_OK()
    {
        var cut = Context.Render<ColorPicker>(builder =>
        {
            builder.Add(a => a.Formatter, async v =>
            {
                await Task.Delay(0);
                return $"test-color-value{v}";
            });
            builder.Add(a => a.Value, "#AABBCC");
        });

        cut.Contains("test-color-value#AABBCC");

        var input = cut.Find("input");
        await cut.InvokeAsync(() => input.Change("#000000"));
        cut.Contains("test-color-value#000000");
    }

    [Fact]
    public async Task IsSupportOpacity_Ok()
    {
        var cut = Context.Render<ColorPicker>(builder =>
        {
            builder.Add(a => a.IsSupportOpacity, true);
            builder.Add(a => a.Value, "#AABBCCDD");
            builder.Add(a => a.Swatches, ["rgba(123, 1, 2, 1)", "rgba(1, 255,10, 1)"]);
        });
        cut.Contains("<div class=\"bb-color-picker-body\" style=\"--bb-color-pick-val: #AABBCCDD\"></div>");

        await cut.InvokeAsync(() => cut.Instance.OnColorChanged("#123456"));
        Assert.Equal("#123456", cut.Instance.Value);

        await cut.InvokeAsync(() => cut.Instance.SetValue("#333333"));

        cut.Render(pb =>
        {
            pb.Add(a => a.IsSupportOpacity, false);
        });

        cut.Render(pb =>
        {
            pb.Add(a => a.IsDisabled, true);
        });
    }
}
