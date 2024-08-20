// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using System.Text.RegularExpressions;

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
            return "cmyk(0%, 0%, 0%, 100%)";
        double finalC = (c - k) / (1 - k);
        double finalM = (m - k) / (1 - k);
        double finalY = (y - k) / (1 - k);
        return $"cmyk({DoubleToPercentage(finalC)}, {DoubleToPercentage(finalM)}, {DoubleToPercentage(finalY)}, {DoubleToPercentage(k)})";
    }

    private string GetFormatColorString(double[] source)
    {
        var hsl = (h: source[0], s: source[1], l: source[2]);
        var alpha = source[3];
        return FormatType switch
        {
            ColorPickerV2FormatType.Hex => NeedAlpha
                ? string.Concat(RgbToHex(HslToRgb(hsl)), $"{(int)Math.Round(alpha * 255):X2}")
                : RgbToHex(HslToRgb(hsl)),
            ColorPickerV2FormatType.Rgb => NeedAlpha
                ? FormatRgb(HslToRgb(hsl)).Replace("rgb", "rgba").Replace(")", $", {alpha:F2})")
                : FormatRgb(HslToRgb(hsl)),
            ColorPickerV2FormatType.Hsl => NeedAlpha
                ? $"hsla({hsl.h}, {DoubleToPercentage(hsl.s)}, {DoubleToPercentage(hsl.l)}, {alpha})"
                : $"hsl({hsl.h}, {DoubleToPercentage(hsl.s)}, {DoubleToPercentage(hsl.l)})",
            ColorPickerV2FormatType.Cmyk => RgbToCmyk(HslToRgb(hsl)),
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    private (double H, double S, double L, double A) GetFormatColorValue(string source)
    {
        try
        {
            source = source.ToLower();
            if (source.StartsWith("hsl("))
            {
                var s = source
                    .Replace("hsl(", "").Replace(")", "").Split(',')
                    .Select(r => r.Trim()).Select(r => r.Contains('%')
                        ? double.Parse(r.Replace("%", "")) / 100
                        : double.Parse(r)).ToArray();
                return (s[0], s[1], s[2], 1);
            }
            if (source.StartsWith("hsla("))
            {
                var s = source
                    .Replace("hsla(", "").Replace(")", "").Split(',')
                    .Select(r => r.Trim()).Select(r => r.Contains('%')
                        ? double.Parse(r.Replace("%", "")) / 100
                        : double.Parse(r)).ToArray();
                return (s[0], s[1] / 100, s[2] / 100, s[3]);
            }
            (double r, double g, double b, double a) rgb;
            if (source.StartsWith("#"))
            {
                rgb = HexToRgba(source);
            }
            else if (source.StartsWith("rgb("))
            {
                var s = source
                    .Replace("rgb(", "").Replace(")", "").Split(',')
                    .Select(r => r.Trim()).Select(r => r.Contains('%')
                        ? double.Parse(r.Replace("%", "")) / 100
                        : double.Parse(r) / 255).ToArray();
                rgb = (s[0], s[1], s[2], 1);
            }
            else if (source.StartsWith("rgba("))
            {
                var s = source
                    .Replace("rgba(", "").Replace(")", "").Split(',')
                    .Select(r => r.Trim()).Select(r => r.Contains('%')
                        ? double.Parse(r.Replace("%", "")) / 100
                        : double.Parse(r) / 255).ToArray();
                rgb = (s[0], s[1], s[2], s[3] * 255);
            }
            else if (source.StartsWith("cmyk("))
            {
                var s = source
                    .Replace("cmyk(", "").Replace(")", "").Split(',')
                    .Select(r => r.Trim()).Select(r => r.Contains('%')
                        ? double.Parse(r.Replace("%", "")) / 100
                        : double.Parse(r)).ToArray();

                rgb = GetRgbFromCmyk(s[0], s[1], s[2], s[3]);
            }
            else
            {
                rgb = (0, 0, 0, 1);
            }
            return GetHslaFromRgba(rgb);
        }
        catch (Exception e)
        {
            return (0, 0, 0, 1);
        }
    }

    private (double r, double g, double b, double a) GetRgbFromCmyk(
        double c, double m, double y, double k)
    {
        c = Math.Min(Math.Max(c, 0), 1);
        m = Math.Min(Math.Max(m, 0), 1);
        y = Math.Min(Math.Max(y, 0), 1);
        k = Math.Min(Math.Max(k, 0), 1);
        return ((1 - c) * (1 - k), (1 - m) * (1 - k), (1 - y) * (1 - k), 1);
    }


    private (double H, double S, double L, double A) GetHslaFromRgba(
        (double r, double g, double b, double a) source)
    {
        var hsl = RgbToHsl(source.r, source.g, source.b);
        return (hsl.h, hsl.s, hsl.l, source.a);
    }

    private (double r, double g, double b, double a) HexToRgba(string hexString)
    {
        try
        {
            if (!Regex.IsMatch(hexString, @"^#[0-9a-fA-F]{6}$|^#[0-9a-fA-F]{3}$|^#[0-9a-fA-F]{8}$"))
                return (0, 0, 0, 1);
            hexString = hexString[1..];
            if (hexString.Length == 3)
            {
                hexString = hexString[0].ToString() + hexString[0] +
                            hexString[1] + hexString[1] +
                            hexString[2] + hexString[2];
            }
            return hexString.Length switch
            {
                8 => (
                    Convert.ToInt32(hexString[..2], 16) / 255.0,
                    Convert.ToInt32(hexString.Substring(2, 2), 16) / 255.0,
                    Convert.ToInt32(hexString.Substring(4, 2), 16) / 255.0,
                    Convert.ToInt32(hexString.Substring(6, 2), 16) / 255.0),
                6 => (
                    Convert.ToInt32(hexString[..2], 16) / 255.0,
                    Convert.ToInt32(hexString.Substring(2, 2), 16) / 255.0,
                    Convert.ToInt32(hexString.Substring(4, 2), 16) / 255.0,
                    1.0),
                _ => throw new ArgumentException("Invalid hex string length.")
            };
        }
        catch (Exception e)
        {
            return (0, 0, 0, 1);
        }
    }
}

/// <summary>
/// 输入框显示的颜色格式类型
/// </summary>
public enum ColorPickerV2FormatType
{
    /// <summary>
    /// hex from rgb(0-1)->(0-255)
    /// </summary>
    Hex,
    /// <summary>
    /// https://en.wikipedia.org/wiki/RGB_color_model
    /// </summary>
    Rgb,
    /// <summary>
    /// https://en.wikipedia.org/wiki/HSL_and_HSV
    /// </summary>
    Hsl,
    /// <summary>
    /// https://en.wikipedia.org/wiki/CMYK_color_model
    /// </summary>
    Cmyk,
}
