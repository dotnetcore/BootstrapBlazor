// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

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
        // 创建配置 ConfigurationBuilder
        var builder = new ConfigurationBuilder();

        // 获取程序集中的资源文件
        var assemblies = new List<Assembly>() { assembly };

        // 获得主程序集资源文件
        // 支持合并操作
        var entryAssembly = GetEntryAssembly();
        if (assembly != entryAssembly)
        {
            assemblies.Add(entryAssembly);
        }

        if (option.AdditionalJsonAssemblies != null)
        {
            assemblies.AddRange(option.AdditionalJsonAssemblies);
        }

        var streams = assemblies.SelectMany(i => option.GetResourceStream(i, cultureName)).ToList();

        // 添加 Json 文件流到配置
        foreach (var s in streams)
        {
            builder.AddJsonStream(s);
        }

        // 获得配置外置资源文件
        if (option.AdditionalJsonFiles != null)
        {
            var files = option.AdditionalJsonFiles.Where(f =>
            {
                var fileName = Path.GetFileNameWithoutExtension(f);
                return fileName.Equals(cultureName, StringComparison.OrdinalIgnoreCase);
            });
            foreach (var file in files)
            {
                builder.AddJsonFile(file, true, false);
            }
        }

        // 生成 IConfigurationRoot
        var config = builder.Build();

        // dispose json stream
        foreach (var s in streams)
        {
            s.Dispose();
        }

        return config.GetChildren();

        [ExcludeFromCodeCoverage]
        Assembly GetEntryAssembly() => Assembly.GetEntryAssembly() ?? assembly;
    }

    private static List<Stream> GetResourceStream(this JsonLocalizationOptions option, Assembly assembly, string cultureName)
    {
        var resourceNames = assembly.GetManifestResourceNames();
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
            var json = resourceNames.FirstOrDefault(i => i.Contains($".{name}.json"));
            if (json != null)
            {
                var stream = assembly.GetManifestResourceStream(json);
                if (stream != null)
                {
                    ret.Add(stream);
                }
            }
        }

        // 开启回落机制并且当前文化信息与回落语言相同
        bool EqualFallbackCulture(string name) => option.EnableFallbackCulture && option.FallbackCulture == name;
    }

    static StringSegment GetParentCultureName(StringSegment cultureInfoName)
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
