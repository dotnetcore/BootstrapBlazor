// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using System.Text.RegularExpressions;

namespace UniTest.Sass;

public partial class FluentThemeTest
{
    private static readonly string ThemeFile = Path.Combine(AppContext.BaseDirectory, "../../../../../src/BootstrapBlazor/wwwroot/css/fluent.css");

    private static readonly string MinifiedThemeFile = Path.Combine(AppContext.BaseDirectory, "../../../../../src/BootstrapBlazor/wwwroot/css/fluent.min.css");

    // Fluent 主题必须覆盖的 Bootstrap 语义变量(含 -rgb 配套变量)
    private static readonly string[] SemanticVariables =
    [
        "--bs-emphasis-color",
        "--bs-emphasis-color-rgb",
        "--bs-secondary-color",
        "--bs-secondary-color-rgb",
        "--bs-tertiary-color",
        "--bs-tertiary-color-rgb",
        "--bs-secondary-bg",
        "--bs-secondary-bg-rgb",
        "--bs-tertiary-bg",
        "--bs-tertiary-bg-rgb"
    ];

    [Fact]
    public void FluentTheme_BalancedBraces_Ok()
    {
        // 检查样式文件大括号配对且嵌套顺序正确
        var css = ReadThemeWithoutComments();
        var depth = 0;
        foreach (var ch in css)
        {
            if (ch == '{')
            {
                depth++;
            }
            else if (ch == '}')
            {
                depth--;
            }
            Assert.True(depth >= 0, "主题文件存在多余的右大括号");
        }
        Assert.Equal(0, depth);
    }

    [Fact]
    public void FluentTheme_LightSemanticVariables_Ok()
    {
        // 检查 Light 主题块语义变量是否定义
        var css = ReadThemeWithoutComments();
        var block = ExtractBlock(css, ":root, [data-bs-theme='light']");
        AssertSemanticVariables(block);
    }

    [Fact]
    public void FluentTheme_DarkSemanticVariables_Ok()
    {
        // 检查 Dark 主题块语义变量是否定义
        var css = ReadThemeWithoutComments();
        var block = ExtractBlock(css, "[data-bs-theme='dark']");
        AssertSemanticVariables(block);
    }

    [Fact]
    public void FluentTheme_VariableReferences_Ok()
    {
        // 检查所有 var() 引用的变量均已定义或属于 Bootstrap 核心变量
        var css = ReadThemeWithoutComments();

        var defined = DefinitionRegex().Matches(css)
            .Select(m => m.Groups[1].Value)
            .ToHashSet(StringComparer.Ordinal);
        Assert.NotEmpty(defined);

        // Bootstrap 核心库已定义的变量,主题文件无需重复定义
        var bootstrapCoreVariables = new HashSet<string>(StringComparer.Ordinal)
        {
            "--bs-border-width"
        };

        var missing = ReferenceRegex().Matches(css)
            .Select(m => m.Groups[1].Value)
            .Where(v => !defined.Contains(v) && !bootstrapCoreVariables.Contains(v))
            .Distinct(StringComparer.Ordinal)
            .ToList();
        Assert.True(missing.Count == 0, $"主题文件引用了未定义的变量: {string.Join(", ", missing)}");
    }

    [Theory]
    [InlineData(".dropdown-menu")]
    [InlineData(".popover")]
    [InlineData(".tooltip")]
    [InlineData(".modal")]
    [InlineData(".switch")]
    [InlineData(".table")]
    [InlineData(".pagination")]
    [InlineData(".form-check")]
    [InlineData(".form-range")]
    public void FluentTheme_ComponentSections_Ok(string selector)
    {
        // 检查关键组件样式节是否存在
        var css = ReadThemeWithoutComments();
        Assert.Matches($"{Regex.Escape(selector)}[\\s\\{{\\.:#,\\[]", css);
    }

    [Fact]
    public void FluentTheme_SizingLayer_Ok()
    {
        // 检查尺寸与间距层关键规则存在(Fluent medium 32px 控件密度)
        var css = ReadThemeWithoutComments();
        Assert.Contains("--bs-btn-padding-y: 5px", css);
        Assert.Contains("--bs-btn-line-height: 20px", css);
        Assert.Contains("min-height: 32px", css);
        Assert.Contains("--bb-height: 32px", css);
        Assert.Contains("padding-left: 28px", css);
        Assert.Contains("--bs-modal-padding: 24px", css);
    }

    [Theory]
    [InlineData("--bb-disabled-bg")]
    [InlineData("--bb-border-focus-color")]
    [InlineData("--bb-border-hover-color")]
    [InlineData("--bb-shadow")]
    [InlineData("--bb-hover-shadow")]
    public void FluentTheme_DarkBbVariables_Ok(string variable)
    {
        // 检查主题自定义 bb 根变量的暗色对等定义(bundle 未定义这些变量,漏配暗色无回退)
        var css = ReadThemeWithoutComments();
        var block = ExtractBlock(css, "[data-bs-theme='dark']");
        Assert.Matches($"{Regex.Escape(variable)}\\s*:", block);
    }

    [Fact]
    public void FluentTheme_NoLegacyLiterals_Ok()
    {
        // 检查 Bootstrap 默认蓝与 Motronic 主题标志性色值不残留(注释除外)
        var css = ReadThemeWithoutComments();
        string[] legacyLiterals =
        [
            "#0d6efd",
            "#009ef7",
            "#7239ea",
            "#50cd89",
            "#ffc700",
            "#f1416c",
            "#181c32",
            "#7e8299",
            "#f5f8fa",
            "#e4e6ef",
            "#b5b5c3",
            "#a1a5b7",
            "#5e6278",
            "#3f4254",
            "motronic"
        ];
        var hits = legacyLiterals.Where(i => css.Contains(i, StringComparison.OrdinalIgnoreCase)).ToList();
        Assert.True(hits.Count == 0, $"主题文件残留旧主题色值: {string.Join(", ", hits)}");
    }

    [Fact]
    public void FluentTheme_MinifiedFile_Ok()
    {
        // 检查压缩文件存在且为真正的压缩格式(横幅注释之外正文为单行)
        Assert.True(File.Exists(MinifiedThemeFile), $"压缩主题文件不存在: {MinifiedThemeFile}");
        var min = File.ReadAllText(MinifiedThemeFile);
        var source = File.ReadAllText(ThemeFile);

        Assert.True(min.Length < source.Length, "压缩文件体积未小于源文件");

        var banner = BannerRegex().Match(source).Value;
        var body = min[banner.Length..];

        // 正文为单行(不存在多行规则)且平均行长远超可读版本
        var bodyLines = body.Split('\n', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
        Assert.Single(bodyLines);
        Assert.True(bodyLines[0].Length > 10000, "压缩文件正文疑似未被压缩");

        // 横幅之外不允许残留注释
        Assert.DoesNotContain("/*", body);
    }

    [Fact]
    public void FluentTheme_MinifiedBanner_Ok()
    {
        // 检查压缩文件顶部完整保留许可证/用法/令牌来源横幅注释
        var banner = BannerRegex().Match(File.ReadAllText(ThemeFile)).Value;
        Assert.False(string.IsNullOrEmpty(banner), "源文件缺少横幅注释");
        var min = File.ReadAllText(MinifiedThemeFile);
        Assert.True(min.StartsWith(banner, StringComparison.Ordinal), "压缩文件未保留顶部横幅注释");
    }

    [Fact]
    public void FluentTheme_MinifiedVariablesMatch_Ok()
    {
        // 检查压缩文件与源文件包含完全相同的 --bs-* 自定义属性定义集合
        var sourceDefinitions = DefinitionRegex().Matches(ReadThemeWithoutComments())
            .Select(m => m.Groups[1].Value)
            .ToHashSet(StringComparer.Ordinal);

        var min = CommentRegex().Replace(File.ReadAllText(MinifiedThemeFile), "");
        var minifiedDefinitions = DefinitionRegex().Matches(min)
            .Select(m => m.Groups[1].Value)
            .ToHashSet(StringComparer.Ordinal);

        Assert.True(sourceDefinitions.SetEquals(minifiedDefinitions),
            $"压缩文件变量定义集合不一致, 源文件独有: {string.Join(", ", sourceDefinitions.Except(minifiedDefinitions))}; 压缩文件独有: {string.Join(", ", minifiedDefinitions.Except(sourceDefinitions))}");
    }

    private static void AssertSemanticVariables(string block)
    {
        foreach (var variable in SemanticVariables)
        {
            Assert.Matches($"{Regex.Escape(variable)}\\s*:", block);
        }
    }

    private static string ReadThemeWithoutComments()
    {
        Assert.True(File.Exists(ThemeFile), $"主题文件不存在: {ThemeFile}");
        var css = File.ReadAllText(ThemeFile);
        return CommentRegex().Replace(css, "");
    }

    private static string ExtractBlock(string css, string selector)
    {
        // 定位独立选择器块(跳过 [data-bs-theme='dark'] .layout 之类的复合选择器)并提取配对大括号内的内容
        var index = 0;
        while (true)
        {
            index = css.IndexOf(selector, index, StringComparison.Ordinal);
            Assert.True(index >= 0, $"选择器 {selector} 不存在");

            var position = index + selector.Length;
            while (position < css.Length && char.IsWhiteSpace(css[position]))
            {
                position++;
            }

            if (position < css.Length && css[position] == '{')
            {
                var start = position + 1;
                var depth = 1;
                position = start;
                while (position < css.Length && depth > 0)
                {
                    if (css[position] == '{')
                    {
                        depth++;
                    }
                    else if (css[position] == '}')
                    {
                        depth--;
                    }
                    position++;
                }
                Assert.True(depth == 0, $"选择器 {selector} 块未闭合");
                return css[start..(position - 1)];
            }
            index = position;
        }
    }

    [GeneratedRegex(@"/\*.*?\*/", RegexOptions.Singleline)]
    private static partial Regex CommentRegex();

    [GeneratedRegex(@"\A/\*[\s\S]*?\*/")]
    private static partial Regex BannerRegex();

    [GeneratedRegex(@"(--b[bs]-[\w-]+)\s*:")]
    private static partial Regex DefinitionRegex();

    [GeneratedRegex(@"var\(\s*(--b[bs]-[\w-]+)")]
    private static partial Regex ReferenceRegex();
}
