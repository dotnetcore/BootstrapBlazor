// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace UnitTest.Components;

public class ColorPickerTest : BootstrapBlazorTestBase
{
    [Fact]
    public void DisplayText_OK()
    {
        var cut = Context.RenderComponent<ColorPicker>(builder =>
        {
            builder.Add(a => a.ShowLabel, true);
            builder.Add(a => a.DisplayText, "Test_Color");
        });

        Assert.Equal("Test_Color", cut.Find("label").TextContent);
    }

    [Fact]
    public void Template_OK()
    {
        var cut = Context.RenderComponent<ColorPicker>(builder =>
        {
            builder.Add(a => a.Template, v => builder => builder.AddContent(0, $"Test-{v}"));
            builder.Add(a => a.Value, "#AABBCC");
        });

        cut.Contains("Test-#AABBCC");
    }

    [Fact]
    public async Task Formatter_OK()
    {
        var cut = Context.RenderComponent<ColorPicker>(builder =>
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
}
