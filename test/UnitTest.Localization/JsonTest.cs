// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using BootstrapBlazor.Server.Components;
using Microsoft.Extensions.Configuration;
using System.Text.Json;

namespace UnitTest.Localization;

public class JsonTest
{
    [Theory]
    [InlineData("zh-CN.json")]
    [InlineData("en-US.json")]
    public void Update_Localizer_Ok(string localeFileName)
    {
        // 拼接资源文件
        var localizerFile = Path.Combine(AppContext.BaseDirectory, "../../../../../", "src/BootstrapBlazor.Server/Locales/", localeFileName);

        if (!File.Exists(localizerFile))
        {
            return;
        }

        var configuration = CreateConfiguration(localizerFile);
        var components = GetComponents();

        // 设置根路径
        var rootPath = Path.Combine(AppContext.BaseDirectory, "../../../../../", "src/BootstrapBlazor.Server/Components/");
        foreach (var router in components)
        {
            var sectionName = router.FullName;
            if (string.IsNullOrEmpty(sectionName) || sectionName == "BootstrapBlazor.Server.Components.Pages.Online")
            {
                continue;
            }

            var section = configuration.GetSection(sectionName);
            if (section == null)
            {
                continue;
            }

            var typeName = router.FullName?.Replace("BootstrapBlazor.Server.Components.", "").Replace(".", "/");
            if (string.IsNullOrEmpty(typeName))
            {
                continue;
            }

            var file = Path.Combine(rootPath, typeName);
            var razorFile = $"{file}.razor";
            var srcFile = $"{file}.razor.cs";
            foreach (var c in section.GetChildren())
            {
                var key = c.Key;
                if (!string.IsNullOrEmpty(key))
                {
                    var found = FindLocalizerByKey(razorFile, key);
                    if (found)
                    {
                        continue;
                    }

                    found = FindLocalizerByKey(srcFile, key);
                    if (found)
                    {
                        continue;
                    }

                    if (sectionName == "BootstrapBlazor.Server.Components.Layout.NavMenu")
                    {
                        found = FindLocalizerByContent(GetNavMenuLocalizerContent(rootPath), key);
                        if (found)
                        {
                            continue;
                        }
                    }

                    c.Value = null;
                }
            }
        }

        SaveConfiguration(configuration, localizerFile);
    }

    private static string _navMenuLocalizerContent = "";

    private static string GetNavMenuLocalizerContent(string rootPath)
    {
        if (!string.IsNullOrEmpty(_navMenuLocalizerContent))
        {
            return _navMenuLocalizerContent;
        }

        var fileName = Path.Combine(rootPath, "..", "Extensions", "MenusLocalizerExtensions.cs");
        _navMenuLocalizerContent = File.ReadAllText(fileName);
        return _navMenuLocalizerContent;
    }

    private static void SaveConfiguration(IConfiguration configuration, string outputFile)
    {
        // 循环 Configuration 更新 Json 文件
        using var outputStream = File.Create(outputFile);
        WriteToJsonStream(configuration, outputStream);
        outputStream.Write(new byte[] { 0x0D, 0x0A });
    }

    private static List<Type> GetComponents()
    {
        var assembly = typeof(App).Assembly;
        return assembly.GetExportedTypes()
                       .Where(t => (t.FullName?.StartsWith("BootstrapBlazor.Server.Components.") ?? false))
                       .ToList();
    }

    private static IConfigurationRoot CreateConfiguration(string localizerFile)
    {
        var builder = new ConfigurationBuilder();
        builder.AddJsonFile(localizerFile, false, false);
        return builder.Build();
    }

    private static bool FindLocalizerByKey(string fileName, string key)
    {
        if (!File.Exists(fileName))
        {
            return false;
        }

        var content = File.ReadAllText(fileName);
        return FindLocalizerByContent(content, key);
    }

    private static bool FindLocalizerByContent(string content, string key)
    {
        if (string.IsNullOrEmpty(content))
        {
            return false;
        }

        if (content.Contains($"Localizer[\"{key}\"]"))
        {
            return true;
        }

        if (content.Contains($"Localizer[nameof({key})]"))
        {
            return true;
        }

        if (content.Contains($"Localizer[\"{key}\","))
        {
            return true;
        }

        if (content.Contains($"Localizer[nameof({key}),"))
        {
            return true;
        }

        return false;
    }

    private static void WriteToJsonStream(IConfiguration configuration, Stream outputStream)
    {
        using var writer = new Utf8JsonWriter(outputStream, new JsonWriterOptions { Indented = true, Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping });
        writer.WriteStartObject();
        foreach (var section in configuration.GetChildren())
        {
            writer.WritePropertyName(section.Key);
            writer.WriteStartObject();
            foreach (var child in section.GetChildren())
            {
                if (child.Value is not null)
                {
                    writer.WritePropertyName(child.Key);
                    writer.WriteStringValue(child.Value);
                }
            }
            writer.WriteEndObject();
        }
        writer.WriteEndObject();
        writer.Flush();
    }
}
