// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Localization.Json;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;
using System.Reflection;

namespace BootstrapBlazor.Components;

/// <summary>
/// JsonLocalizationOptions 扩展方法
/// </summary>
internal static class LocalizationOptionsExtensions
{
    /// <summary>
    /// 通过系统 JsonLocalizationOptions 获取当前 Json 格式资源配置集合
    /// </summary>
    /// <param name="option"></param>
    /// <param name="assembly"></param>
    /// <param name="cultureName"></param>
    /// <returns></returns>
    public static IEnumerable<IConfigurationSection> GetJsonStringFromAssembly(this JsonLocalizationOptions option, Assembly assembly, string cultureName)
    {
        // 获得程序集内 Json 文件流集合
        var langHandlers = option.GetJsonHanlders(assembly, cultureName).ToList();

        // 创建配置 ConfigurationBuilder
        var builder = new ConfigurationBuilder();

        // 添加 Json 文件流到配置
        foreach (var h in langHandlers)
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

        // 生成 IConfigurationRoot
        var config = builder.Build();

        // dispose json stream
        foreach (var h in langHandlers)
        {
            h.Dispose();
        }
        return config.GetChildren();
    }

    private static IEnumerable<Stream> GetJsonHanlders(this JsonLocalizationOptions option, Assembly assembly, string cultureName)
    {
        // 获取程序集中的资源文件
        var assemblies = new List<Assembly>()
        {
            assembly
        };
        if (option.AdditionalJsonAssemblies != null)
        {
            assemblies.AddRange(option.AdditionalJsonAssemblies);
        }
        return assemblies.SelectMany(i => option.GetResourceStream(i, cultureName));
    }

    private static List<Stream> GetResourceStream(this JsonLocalizationOptions option, Assembly assembly, string cultureName)
    {
        var ret = new List<Stream>();

        // 如果开启回落机制优先增加回落语言
        if (option.EnableFallbackCulture)
        {
            AddStream(option.FallbackCulture);
        }

        // 查找父资源
        var parentName = GetParentCultureName(cultureName).Value;
        if (!string.IsNullOrEmpty(parentName) && !EqualFallbackCulture(parentName))
        {
            AddStream(parentName);
        }

        // 当前文化资源
        if (!EqualFallbackCulture(cultureName))
        {
            AddStream(cultureName);
        }

        return ret;

        void AddStream(string name)
        {
            var json = $"{assembly.GetName().Name}.{option.ResourcesPath}.{name}.json";
            var stream = assembly.GetManifestResourceStream(json);
            if (stream != null)
            {
                ret.Add(stream);
            }
        }

        // 开启回落机制并且当前文化信息与回落语言相同
        bool EqualFallbackCulture(string name) => option.EnableFallbackCulture && option.FallbackCulture == name;

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
