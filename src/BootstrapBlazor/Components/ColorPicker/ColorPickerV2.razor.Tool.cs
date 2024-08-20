// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// 色彩混合使用rgb格式、渐变插值使用hsl格式，其余格式（cmyk，hsv，hex）仅用于显示
/// </summary>
public partial class ColorPickerV2
{
    /// <summary>
    /// 小数转百分比
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    private static string DoubleToPercentage(double source)
        => $"{(source * 100):F2}%";

    /// <summary>
    /// hsl格式转rgb格式。为了减少计算误差，结果为0-1。前端最终显示时需要Math.Round(value * 255)
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    private static (double r, double g, double b) HslToRgb(
        (double h, double s, double l) source)
    {
        double c = (1 - Math.Abs(2 * source.l - 1)) * source.s;
        double x = c * (1 - Math.Abs((source.h / 60) % 2 - 1));
        double m = source.l - c / 2;
        var (r, g, b) = source.h switch
        {
            >= 0 and < 60 => (c, x, 0.0),
            >= 60 and < 120 => (x, c, 0.0),
            >= 120 and < 180 => (0.0, c, x),
            >= 180 and < 240 => (0.0, x, c),
            >= 240 and < 300 => (x, 0.0, c),
            >= 300 and <= 360 => (c, 0.0, x),
            _ => throw new Exception("")
        };
        return ((r + m), (g + m), (b + m));
    }

    /// <summary>
    /// rgb格式的乘法混合模式
    /// </summary>
    /// <param name="rgbX">X方向上的rgb</param>
    /// <param name="rgbY">Y方向上的rgb</param>
    /// <returns></returns>
    private static (double r, double g, double b) MultiplyBlend(
        (double r, double g, double b) rgbX,
        (double r, double g, double b) rgbY)
    {
        return (
            r: rgbX.r * rgbY.r,
            g: rgbX.g * rgbY.g,
            b: rgbX.b * rgbY.b
        );
    }

    /// <summary>
    /// rgb转回hsl格式
    /// </summary>
    /// <param name="r"></param>
    /// <param name="g"></param>
    /// <param name="b"></param>
    /// <returns></returns>
    private static (double h, double s, double l) RgbToHsl(
        double r, double g, double b)
    {
        double max = Math.Max(r, Math.Max(g, b));
        double min = Math.Min(r, Math.Min(g, b));
        double delta = max - min;

        double h = 0;
        if (delta != 0)
        {
            if (Math.Abs(max - r) < 0.0000000001)
                h = ((g - b) / delta + (g < b ? 6 : 0)) * 60;
            else if (Math.Abs(max - g) < 0.0000000001)
                h = ((b - r) / delta + 2) * 60;
            else
                h = ((r - g) / delta + 4) * 60;
        }
        double l = (max + min) / 2;
        double s = delta == 0 ? 0 : delta / (1 - Math.Abs(2 * l - 1));
        return (h, s, l);
    }

    private static string RgbToHex((double r, double g, double b) source)
    {
        int red = (int)Math.Round(source.r * 255);
        int green = (int)Math.Round(source.g * 255);
        int blue = (int)Math.Round(source.b * 255);
        return $"#{red:X2}{green:X2}{blue:X2}";
    }

    private static string FormatRgb((double r, double g, double b) source)
    {
        // 将每个 RGB 分量从 0-1 缩放到 0-255 并转换为整数
        int red = (int)Math.Round(source.r * 255);
        int green = (int)Math.Round(source.g * 255);
        int blue = (int)Math.Round(source.b * 255);
        return $"rgb({red}, {green}, {blue})";
    }

    private static string RgbToCmyk((double r, double g, double b) source)
    {
        double c = 1 - source.r;
        double m = 1 - source.g;
        double y = 1 - source.b;
        double k = Math.Min(c, Math.Min(m, y));
        if (Math.Abs(k - 1) < 0.0000000001)
            return "(0%, 0%, 0%, 100%)";
        double finalC = (c - k) / (1 - k);
        double finalM = (m - k) / (1 - k);
        double finalY = (y - k) / (1 - k);
        return $"cmyk({DoubleToPercentage(finalC)}, {DoubleToPercentage(finalM)}, {DoubleToPercentage(finalY)}, {DoubleToPercentage(k)})";
    }

    private string GetFormatColor(double[] source)
    {
        var hsl = (h: source[0], s: source[1], l: source[2]);
        var alpha = source[3];
        return FormatType switch
        {
            ColorPickerV2FormatType.Hex => NeedAlpha
                ? RgbToHex(HslToRgb(hsl)).Insert(1, $"{(int)Math.Round(alpha * 255):X2}")
                : RgbToHex(HslToRgb(hsl)),
            ColorPickerV2FormatType.GRB => NeedAlpha
                ? FormatRgb(HslToRgb(hsl)).Replace("rgb", "rgba").Replace(")", $", {alpha:F4})")
                : FormatRgb(HslToRgb(hsl)),
            ColorPickerV2FormatType.HSL => NeedAlpha
                ? $"hsla({hsl.h}, {DoubleToPercentage(hsl.s)}, {DoubleToPercentage(hsl.l)}, {alpha})"
                : $"hsl({hsl.h}, {DoubleToPercentage(hsl.s)}, {DoubleToPercentage(hsl.l)})",
            ColorPickerV2FormatType.CMYK => RgbToCmyk(HslToRgb(hsl)),
            _ => throw new ArgumentOutOfRangeException()
        };
    }
}

/// <summary>
/// 输入框显示的颜色格式类型
/// </summary>
public enum ColorPickerV2FormatType
{
    Hex,
    GRB,
    HSL,
    CMYK,
}
