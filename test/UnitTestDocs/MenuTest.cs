// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Localization.Json;
using BootstrapBlazor.Shared;
using BootstrapBlazor.Shared.Extensions;
using BootstrapBlazor.Shared.Services;
using BootstrapBlazor.Shared.Shared;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.Reflection;

namespace UnitTestDocs;

public class MenuTest
{
    private ITestOutputHelper _logger;

    public MenuTest(ITestOutputHelper logger) => _logger = logger;

    [Fact]
    public void Menu_Ok()
    {
        var serviceCollection = new ServiceCollection();
        var assembly = typeof(App).Assembly;
        serviceCollection.AddBootstrapBlazor(localizationConfigure: option =>
        {
            option.AdditionalJsonAssemblies = new[] { assembly };
        });

        var provider = serviceCollection.BuildServiceProvider();
        var localizerOption = provider.GetRequiredService<IOptions<JsonLocalizationOptions>>();
        var localizer = provider.GetRequiredService<IStringLocalizer<NavMenu>>();

        var routers = assembly.GetExportedTypes()
            .Where(t => t.IsDefined(typeof(RouteAttribute)) && t.GetCustomAttribute<LayoutAttribute>()?.LayoutType == typeof(ComponentLayout) && (t.FullName?.StartsWith("BootstrapBlazor.Shared.Samples.") ?? false));

        var rootPath = "D:\\Argo\\src\\BootstrapBlazor\\src\\BootstrapBlazor.Shared\\Samples\\";
        foreach (var router in routers)
        {
            var typeName = router.FullName?.Replace("BootstrapBlazor.Shared.Samples.", "").Replace(".", "/");
            if (!string.IsNullOrEmpty(typeName))
            {
                var file = Path.Combine(rootPath, typeName);

                // razor file
                var razorFile = $"{file}.razor";
                ReplaceContent(razorFile, typeName, "@Localizer[\"");

                var csharpFile = $"{file}.razor.cs";
                ReplaceContent(csharpFile, typeName, "Localizer[\"");
            }
        }

        void ReplaceContent(string fileName, string typeName, string key)
        {
            if (File.Exists(fileName))
            {
                var content = File.ReadAllText(fileName);
                Utility.GetJsonStringByTypeName(localizerOption.Value, assembly, $"BootstrapBlazor.Shared.Samples.{typeName}").ToList().ForEach(l => content = ReplacePayload(content, l)); ;
                content = ReplaceSymbols(content);
                content = RemoveBlockStatement(content, "@inject IStringLocalizer<");
                if (content.Contains(key))
                {
                    _logger.WriteLine($"{fileName} -- {typeName}");
                }
            }
        }

        //using var fs = new StreamWriter(File.OpenWrite("d:\\argo\\src\\docs.json"));
        //fs.WriteLine("{");
        //foreach (var router in routers)
        //{
        //    var url = router.GetCustomAttributes<RouteAttribute>().FirstOrDefault()?.Template.TrimStart('/');
        //    var typeName = router.FullName!.Replace("BootstrapBlazor.Shared.Samples.", "").Replace(".", "\\\\");
        //    fs.WriteLine($"\"{url}\": \"{typeName}\",");

        //    // 错误检查
        //    var menu = menus.FirstOrDefault(i => i == url);
        //    if (menu == null)
        //    {
        //        fs.WriteLine($"\"{menu}\": \"\",");
        //    }
        //}
        //fs.WriteLine("}");
        //fs.Close();
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
