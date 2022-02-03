// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Localization.Json;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;
using System.Globalization;
using System.Reflection;

namespace BootstrapBlazor.Components;

/// <summary>
/// 
/// </summary>
public static class LocalizationOptionsExtensions
{
    /// <summary>
    /// 通过系统 JsonLocalizationOptions 获取当前 Json 格式资源配置集合
    /// </summary>
    /// <param name="option"></param>
    /// <param name="assembly"></param>
    /// <returns></returns>
    public static IEnumerable<IConfigurationSection> GetJsonStringConfig(this JsonLocalizationOptions option, Assembly assembly)
    {
        var cultureName = CultureInfo.CurrentUICulture.Name;
        var langHandler = GetLangHandlers(cultureName);

        var builder = new ConfigurationBuilder();
        foreach (var h in langHandler)
        {
            builder.AddJsonStream(h);
        }

        // 获得配置外置资源文件
        if (option.AdditionalJsonFiles != null)
        {
            var file = option.AdditionalJsonFiles.FirstOrDefault(f =>
            {
                var fileName = Path.GetFileNameWithoutExtension(f);
                return fileName.Equals(cultureName, StringComparison.OrdinalIgnoreCase);
            });
            if (!string.IsNullOrEmpty(file))
            {
                builder.AddJsonFile(file, true, true);
            }
        }

        var config = builder.Build();

        // dispose json stream
        foreach (var h in langHandler)
        {
            h.Dispose();
        }
        return config.GetChildren();

        List<Stream> GetLangHandlers(string cultureInfoName)
        {
            // 获取程序集中的资源文件
            var langHandler = GetResourceStream(assembly, cultureInfoName);

            // 获取外部设置程序集中的资源文件
            if (option.AdditionalJsonAssemblies != null)
            {
                langHandler.AddRange(option.AdditionalJsonAssemblies
                    .SelectMany(i => GetResourceStream(i, cultureInfoName)));
            }
            return langHandler;
        }

        List<Stream> GetResourceStream(Assembly assembly, string cultureInfoName)
        {
            var ret = new List<Stream>();
            if (option.FallBackToParentUICultures)
            {
                // 查找回落资源
                var parentName = GetParentCultureName(cultureInfoName).Value;
                if (!string.IsNullOrEmpty(parentName))
                {
                    var fallbackJson = $"{assembly.GetName().Name}.{option.ResourcesPath}.{parentName}.json";
                    var stream = assembly.GetManifestResourceStream(fallbackJson);
                    if (stream != null)
                    {
                        ret.Add(stream);
                    }
                }
            }

            // 当前文化资源
            var json = $"{assembly.GetName().Name}.{option.ResourcesPath}.{cultureInfoName}.json";
            var s = assembly.GetManifestResourceStream(json);
            if (s != null)
            {
                ret.Add(s);
            }
            return ret;
        }

        StringSegment GetParentCultureName(StringSegment cultureInfoName)
        {
            var ret = new StringSegment();
            var index = cultureInfoName.IndexOf('-');
            if (index > 0)
            {
                ret = cultureInfoName.Subsegment(0, index);
            }
            return ret;
        }
    }
}
