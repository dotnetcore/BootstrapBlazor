// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Localization.Json;
using BootstrapBlazor.Shared;
using BootstrapBlazor.Shared.Shared;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.Reflection;
using System.Text.RegularExpressions;

namespace UnitTestDocs;

public partial class MenuTest
{
    private ITestOutputHelper _logger;
    private IServiceProvider _serviceProvider;
    private IEnumerable<Type> _routerTable;

    public MenuTest(ITestOutputHelper logger)
    {
        _logger = logger;
        var serviceCollection = new ServiceCollection();
        var assembly = typeof(App).Assembly;
        serviceCollection.AddBootstrapBlazor(localizationConfigure: option =>
        {
            option.AdditionalJsonAssemblies = new[] { assembly };
        });

        _serviceProvider = serviceCollection.BuildServiceProvider();
        _routerTable = assembly.GetExportedTypes()
            .Where(t => t.IsDefined(typeof(RouteAttribute)) && t.GetCustomAttribute<LayoutAttribute>()?.LayoutType == typeof(ComponentLayout) && (t.FullName?.StartsWith("BootstrapBlazor.Shared.Samples.") ?? false));
    }

    [Fact]
    public void Route_Ok()
    {
        var stream = typeof(App).Assembly.GetManifestResourceStream("BootstrapBlazor.Shared.docs.json");
        Assert.NotNull(stream);

        var builder = new ConfigurationBuilder();
        builder.AddJsonStream(stream);

        var config = builder.Build();
        var urls = config.GetRequiredSection("src").GetChildren();

        // 检查未配置的路由
        var invalidRoute = _routerTable.Where(router =>
        {
            var template = router.GetCustomAttribute<RouteAttribute>()?.Template.TrimStart('/');
            var url = urls.FirstOrDefault(i => i.Key == template)?.Key;
            return string.IsNullOrEmpty(url);
        });
        Assert.Empty(invalidRoute);

        // 检查 docs.json 冗余路由
        var invalidUrls = urls.Where(url => _routerTable.FirstOrDefault(router => router.GetCustomAttribute<RouteAttribute>()?.Template.TrimStart('/') == url.Key) == null);
        Assert.Empty(invalidRoute);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="cultureName"></param>
    [Theory]
    [InlineData("en")]
    [InlineData("zh")]
    public void Localizer_En(string cultureName)
    {
        var result = new List<string>();
        var localizerOption = _serviceProvider.GetRequiredService<IOptions<JsonLocalizationOptions>>();
        var localizer = _serviceProvider.GetRequiredService<IStringLocalizer<NavMenu>>();

        var rootPath = Path.Combine(AppContext.BaseDirectory, "../../../../../", "src/BootstrapBlazor.Shared/Samples/");
        foreach (var router in _routerTable)
        {
            var typeName = router.FullName?.Replace("BootstrapBlazor.Shared.Samples.", "").Replace(".", "/");
            if (!string.IsNullOrEmpty(typeName))
            {
                var file = Path.Combine(rootPath, typeName);

                // razor file
                var razorFile = $"{file}.razor";
                ReplaceContent(razorFile, typeName, RazorRegex());

                var srcFile = $"{file}.razor.cs";
                ReplaceContent(srcFile, typeName, SourceCodeRegex());
            }
        }
        Assert.Empty(result);

        void ReplaceContent(string fileName, string typeName, Regex regex)
        {
            if (File.Exists(fileName))
            {
                var type = typeName.Replace('/', '.');
                var content = File.ReadAllText(fileName);
                Utility.GetJsonStringByTypeName(localizerOption.Value, typeof(App).Assembly, $"BootstrapBlazor.Shared.Samples.{type}", cultureName).ToList().ForEach(l => content = ReplacePayload(content, l)); ;
                content = ReplaceSymbols(content);
                content = RemoveBlockStatement(content, "@inject IStringLocalizer<");

                // 利用这则表达式获取键值
                var matches = regex.Matches(content);
                if (matches.Count > 0)
                {
                    var v = string.Join(",", matches.Select(i => i.Value));
                    _logger.WriteLine($"{Path.GetFileName(fileName)} - {v}");
                    result.Add(v);
                }
            }
        }
    }

    [Theory]
    [InlineData("en", "zh")]
    [InlineData("zh", "en")]
    public void Localizer_Compare(string sourceLanguage, string targetLanguage)
    {
        using var configZh = new ConfigurationManager();
        configZh.AddJsonStream(typeof(App).Assembly.GetManifestResourceStream($"BootstrapBlazor.Shared.Locales.{sourceLanguage}.json")!);

        using var configEn = new ConfigurationManager();
        configEn.AddJsonStream(typeof(App).Assembly.GetManifestResourceStream($"BootstrapBlazor.Shared.Locales.{targetLanguage}.json")!);

        var source = configZh.GetChildren().SelectMany(section => section.GetChildren().Select(i => $"{section.Key} - {i.Key}")).ToList();
        var target = configEn.GetChildren().SelectMany(section => section.GetChildren().Select(i => $"{section.Key} - {i.Key}")).ToList();

        var result = new List<string>();
        source.Where(i => !target.Contains(i)).ToList().ForEach(i =>
        {
            result.Add(i);
            _logger.WriteLine(i);
        });
        Assert.Empty(result);
    }

    static string ReplaceSymbols(string payload) => payload
        .Replace("@@", "@")
        .Replace("&lt;", "<")
        .Replace("&gt;", ">");

    static string ReplacePayload(string payload, LocalizedString l) => payload
        .Replace($"@((MarkupString)Localizer[\"{l.Name}\"].Value)", l.Value)
        .Replace($"@Localizer[\"{l.Name}\"]", l.Value)
        .Replace($"Localizer[\"{l.Name}\"]", $"\"{l.Value}\"");

    static string RemoveBlockStatement(string payload, string removeString)
    {
        var index = payload.IndexOf(removeString);
        if (index > -1)
        {
            var end = payload.IndexOf("\n", index, StringComparison.OrdinalIgnoreCase);
            var target = payload[index..(end + 1)];
            payload = payload.Replace(target, string.Empty);
            payload = payload.TrimStart('\r', '\n');
        }
        return payload;
    }

    [GeneratedRegex("@Localizer\\[\"(\\w+)\"\\]")]
    private static partial Regex RazorRegex();

    [GeneratedRegex("Localizer\\[\"(\\w+)\"\\]")]
    private static partial Regex SourceCodeRegex();
}
