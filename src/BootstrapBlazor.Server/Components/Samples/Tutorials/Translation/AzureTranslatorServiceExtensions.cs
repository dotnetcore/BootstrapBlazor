// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using System.Text.Json;

namespace BootstrapBlazor.Server.Components.Samples.Tutorials.Translation;

static class AzureTranslatorServiceExtensions
{
    /// <summary>
    /// 保存到内存流中
    /// </summary>
    /// <param name="service"></param>
    /// <param name="sections"></param>
    /// <param name="targetLanguageFolder"></param>
    /// <param name="language"></param>
    /// <returns></returns>
    public static async Task TranslateLanguageAsync(this IAzureTranslatorService service, IEnumerable<IConfigurationSection> sections, string targetLanguageFolder, string language)
    {
        // 标志位 判断是否有翻译项
        var hasItem = false;

        // 读取现有目标文件
        var targetLanguageManager = new ConfigurationManager();

        var jsonFile = Path.Combine(targetLanguageFolder, $"{language}.json");
        if (File.Exists(jsonFile))
        {
            targetLanguageManager.AddJsonFile(jsonFile);
        }

        var tempFile = Path.Combine(targetLanguageFolder, $"{language}.temp");
        if (File.Exists(tempFile))
        {
            targetLanguageManager.AddJsonFile(tempFile);
        }

        using var stream = new MemoryStream();
        await using var writer = new Utf8JsonWriter(stream, LanguageWriter.EncoderOptions);
        writer.WriteStartObject();

        foreach (var section in sections)
        {
            var sectionName = section.Key;

            // 得到当前 Section 所有键值
            var kv = section.GetChildren();

            // 查找目标文件中键值是否未翻译
            // 如果未翻译时，请求翻译服务进行补充
            var sectionItems = kv.ToDictionary(i => i.Key, i => string.Empty);
            var languageSection = targetLanguageManager.GetSection(sectionName);
            if (languageSection.Exists())
            {
                // 获得所有子键值
                foreach (var item in languageSection.GetChildren())
                {
                    if (!string.IsNullOrEmpty(item.Value))
                    {
                        sectionItems[item.Key] = item.Value;
                    }
                };
            }

            var emptyItems = sectionItems.Where(i => i.Value == string.Empty).ToDictionary(i => i.Key, i => i.Value);
            if (emptyItems.Count > 0)
            {
                hasItem = true;

                var content = kv.Where(key => emptyItems.Any(i => i.Key == key.Key));
                // 将未翻译的进行翻译
                var translateResults = await service.TranslateAsync(language, content.Select(i => i.Value!), "en-US");

                // 回写值到 kv 变量中
                for (var index = 0; index < translateResults.Count; index++)
                {
                    var translate = translateResults[index].Translations[0];
                    if (translate != null)
                    {
                        var itemKey = content.ElementAt(index).Key;
                        emptyItems[itemKey] = translate.Text;
                    }
                }

                writer.WriteStartObject(sectionName);
                foreach (var item in emptyItems)
                {
                    writer.WriteString(item.Key, item.Value);
                }
                writer.WriteEndObject();
            }
        }

        writer.WriteEndObject();
        await writer.FlushAsync();

        stream.Position = 0;
        if (hasItem)
        {
            if (File.Exists(jsonFile))
            {
                tempFile = Path.Combine(targetLanguageFolder, $"{language}.fix");
            }

            await using var fileWriter = File.CreateText(tempFile);
            await stream.CopyToAsync(fileWriter.BaseStream);
            await fileWriter.WriteLineAsync();
            fileWriter.Close();
        }
    }
}
