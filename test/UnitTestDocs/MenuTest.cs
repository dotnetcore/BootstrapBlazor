// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.Reflection;
using System.Text.RegularExpressions;

namespace UnitTestDocs;

public partial class MenuTest
{
    private readonly ITestOutputHelper _logger;
    private readonly IServiceProvider _serviceProvider;
    private readonly IEnumerable<Type> _routerTable;

    public MenuTest(ITestOutputHelper logger)
    {
        _logger = logger;
        var serviceCollection = new ServiceCollection();
        var assembly = typeof(App).Assembly;
        serviceCollection.AddBootstrapBlazor();

        _serviceProvider = serviceCollection.BuildServiceProvider();
        _serviceProvider.GetRequiredService<ICacheManager>();
        _routerTable = assembly.GetExportedTypes()
            .Where(t => t.IsDefined(typeof(RouteAttribute)) && t.IsComponentLayout() && (t.FullName?.StartsWith("BootstrapBlazor.Server.Components.Samples.") ?? false));
    }

    [Fact]
    public void Route_Ok()
    {
        var stream = typeof(App).Assembly.GetManifestResourceStream("BootstrapBlazor.Server.docs.json");
        Assert.NotNull(stream);

        var builder = new ConfigurationBuilder();
        builder.AddJsonStream(stream);

        var config = builder.Build();
        var menus = config.GetRequiredSection("src").GetChildren();

        // 检查未配置的路由
        var invalidRoute = _routerTable.Where(router =>
        {
            var template = router.GetCustomAttribute<RouteAttribute>()?.Template.TrimStart('/');
            var url = menus.FirstOrDefault(i => i.Key == template)?.Key;
            return string.IsNullOrEmpty(url);
        });
        Assert.Empty(invalidRoute);

        // 检查 docs.json 配置的无效路由
        var invalidMenus = menus.Where(url => _routerTable.FirstOrDefault(router => router.GetCustomAttribute<RouteAttribute>()?.Template.TrimStart('/') == url.Key) == null);
        Assert.Empty(invalidMenus);
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

        var rootPath = Path.Combine(AppContext.BaseDirectory, "../../../../../", "src/BootstrapBlazor.Server.Components/Samples/");
        foreach (var router in _routerTable)
        {
            var typeName = router.FullName?.Replace("BootstrapBlazor.Server.Components.Samples.", "").Replace(".", "/");
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
                Utility.GetJsonStringByTypeName(localizerOption.Value, typeof(App).Assembly, $"BootstrapBlazor.Server.Components.Samples.{type}", cultureName).ToList().ForEach(l => content = ReplacePayload(content, l)); ;
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
    [InlineData("en-US", "zh-CN")]
    [InlineData("zh-CN", "en-US")]
    public void Localizer_Compare(string sourceLanguage, string targetLanguage)
    {
        using var configZh = new ConfigurationManager();
        configZh.AddJsonStream(typeof(App).Assembly.GetManifestResourceStream($"BootstrapBlazor.Server.Locales.{sourceLanguage}.json")!);

        using var configEn = new ConfigurationManager();
        configEn.AddJsonStream(typeof(App).Assembly.GetManifestResourceStream($"BootstrapBlazor.Server.Locales.{targetLanguage}.json")!);

        var source = configZh.GetChildren().SelectMany(section => section.GetChildren().Select(i => $"{section.Key} - {i.Key}")).ToList();
        var target = configEn.GetChildren().SelectMany(section => section.GetChildren().Select(i => $"{section.Key} - {i.Key}")).ToList();

        var result = new List<string>();
        source.Except(target).ToList().ForEach(i =>
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
        var index = payload.IndexOf(removeString, StringComparison.Ordinal);
        if (index > -1)
        {
            var end = payload.IndexOf("\n", index, StringComparison.OrdinalIgnoreCase);
            var target = payload[index..(end + 1)];
            payload = payload.Replace(target, string.Empty);
            payload = payload.TrimStart('\r', '\n');
        }
        return payload;
    }

    [Fact]
    public void Sass_Ok()
    {
        var rootPath = Path.Combine(AppContext.BaseDirectory, "../../../../../", "src/BootstrapBlazor/Components/");
        if (Directory.Exists(rootPath))
        {
            var files = Directory.EnumerateFiles(rootPath, "*.scss", SearchOption.AllDirectories);
            foreach (var file in files)
            {
                _logger.WriteLine(file);
            }
        }
    }

    [GeneratedRegex("@Localizer\\[\"(\\w+)\"\\]")]
    private static partial Regex RazorRegex();

    [GeneratedRegex("Localizer\\[\"(\\w+)\"\\]")]
    private static partial Regex SourceCodeRegex();
}
