// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using AngleSharp.Css.Dom;

namespace UnitTest.Components;

public class ColorPickerV2Test : BootstrapBlazorTestBase
{
    [Fact]
    public void NeedAlpha_Ok()
    {
        var cutActive = Context.RenderComponent<ColorPickerV2>(builder =>
        {
            builder.Add(a => a.NeedAlpha, true);
        });
        cutActive.Contains("class=\"alpha-slider\" style=\"display: block;\"");
        var cutInactive = Context.RenderComponent<ColorPickerV2>(builder =>
        {
            builder.Add(a => a.NeedAlpha, false);
        });
        cutInactive.Contains("class=\"alpha-slider\" style=\"display: none;\"");
    }

    [Fact]
    public void FormatType_Ok()
    {
        var cutRgb = Context.RenderComponent<ColorPickerV2>(builder =>
        {
            builder.Add(a => a.FormatType, ColorPickerV2FormatType.Rgb);
        });
        cutRgb.Contains("rgb(");
        var cutHex = Context.RenderComponent<ColorPickerV2>(builder =>
        {
            builder.Add(a => a.FormatType, ColorPickerV2FormatType.Hex);
        });
        cutHex.Contains("#");
        var cutHsl = Context.RenderComponent<ColorPickerV2>(builder =>
        {
            builder.Add(a => a.FormatType, ColorPickerV2FormatType.Hsl);
        });
        cutHsl.Contains("hsl(");
        var cutCmyk = Context.RenderComponent<ColorPickerV2>(builder =>
        {
            builder.Add(a => a.FormatType, ColorPickerV2FormatType.Cmyk);
        });
        cutCmyk.Contains("cmyk(");

        var cutRgba = Context.RenderComponent<ColorPickerV2>(builder =>
        {
            builder.Add(a => a.NeedAlpha, true);
            builder.Add(a => a.FormatType, ColorPickerV2FormatType.Rgb);
        });
        cutRgba.Contains("rgba(");
        var cutHexa = Context.RenderComponent<ColorPickerV2>(builder =>
        {
            builder.Add(a => a.NeedAlpha, true);
            builder.Add(a => a.FormatType, ColorPickerV2FormatType.Hex);
        });
        cutHexa.Contains("#");
        var cutHsla = Context.RenderComponent<ColorPickerV2>(builder =>
        {
            builder.Add(a => a.NeedAlpha, true);
            builder.Add(a => a.FormatType, ColorPickerV2FormatType.Hsl);
        });
        cutHsla.Contains("hsla(");
    }
}
