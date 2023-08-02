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

public class MenuTest
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
        var json = "D:\\Argo\\src\\BootstrapBlazor\\src\\BootstrapBlazor.Shared\\docs.json";
        var builder = new ConfigurationBuilder();
        builder.AddJsonFile(json);

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

    [Fact]
    public void Localizer_Ok()
    {
        var localizerOption = _serviceProvider.GetRequiredService<IOptions<JsonLocalizationOptions>>();
        var localizer = _serviceProvider.GetRequiredService<IStringLocalizer<NavMenu>>();

        var rootPath = "D:\\Argo\\src\\BootstrapBlazor\\src\\BootstrapBlazor.Shared\\Samples\\";
        foreach (var router in _routerTable)
        {
            var typeName = router.FullName?.Replace("BootstrapBlazor.Shared.Samples.", "").Replace(".", "/");
            if (!string.IsNullOrEmpty(typeName))
            {
                var file = Path.Combine(rootPath, typeName);

                // razor file
                var razorFile = $"{file}.razor";
                var regex = new Regex("@Localizer\\[\"(\\w+)\"\\]");
                ReplaceContent(razorFile, typeName, regex);

                var csharpFile = $"{file}.razor.cs";
                regex = new Regex("Localizer\\[\"(\\w+)\"\\]");
                ReplaceContent(csharpFile, typeName, regex);
            }
        }

        void ReplaceContent(string fileName, string typeName, Regex regex)
        {
            if (File.Exists(fileName))
            {
                var content = File.ReadAllText(fileName);
                Utility.GetJsonStringByTypeName(localizerOption.Value, typeof(App).Assembly, $"BootstrapBlazor.Shared.Samples.{typeName}").ToList().ForEach(l => content = ReplacePayload(content, l)); ;
                content = ReplaceSymbols(content);
                content = RemoveBlockStatement(content, "@inject IStringLocalizer<");

                // 利用这则表达式获取键值
                var matches = regex.Matches(content);
                if (matches.Count > 0)
                {
                    var v = string.Join(",", matches.Select(i => i.Value));
                    _logger.WriteLine($"{fileName} -- {typeName} - {v}");
                }
            }
        }
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
}
