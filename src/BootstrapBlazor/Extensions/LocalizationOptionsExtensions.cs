// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;
using System.Reflection;

namespace BootstrapBlazor.Components;

/// <summary>
///  <para lang="zh">JsonLocalizationOptions 扩展方法</para>
///  <para lang="en">JsonLocalizationOptions extension methods</para>
/// </summary>
internal static class LocalizationOptionsExtensions
{
    /// <summary>
    ///  <para lang="zh">通过系统 JsonLocalizationOptions 获取当前 Json 格式资源配置集合</para>
    ///  <para lang="en">Get the current Json format resource configuration collection through the system JsonLocalizationOptions</para>
    /// </summary>
    /// <param name="option"></param>
    /// <param name="assembly"></param>
    /// <param name="cultureName"></param>
    /// <returns></returns>
    public static IEnumerable<IConfigurationSection> GetJsonStringFromAssembly(this JsonLocalizationOptions option, Assembly assembly, string cultureName)
    {
        // <para lang="zh">创建配置 ConfigurationBuilder</para>
        // <para lang="en">Create configuration ConfigurationBuilder</para>
        var builder = new ConfigurationBuilder();

        // <para lang="zh">获取程序集中的资源文件</para>
        // <para lang="en">Get the resource file in the assembly</para>
        var assemblies = new List<Assembly>() { assembly };

        // <para lang="zh">获得主程序集资源文件</para>
        // <para lang="en">Get the main assembly resource file</para>
        // <para lang="zh">支持合并操作</para>
        // <para lang="en">Support merge operation</para>
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

        // <para lang="zh">添加 Json 文件流到配置</para>
        // <para lang="en">Add Json file stream to configuration</para>
        foreach (var s in streams)
        {
            builder.AddJsonStream(s);
        }

        // <para lang="zh">获得配置外置资源文件</para>
        // <para lang="en">Get configuration external resource file</para>
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

        // <para lang="zh">生成 IConfigurationRoot</para>
        // <para lang="en">Generate IConfigurationRoot</para>
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

        // <para lang="zh">如果开启回落机制优先增加回落语言</para>
        // <para lang="en">If the fallback mechanism is enabled, priority is given to increasing the fallback language</para>
        if (option.EnableFallbackCulture)
        {
            AddStream(option.FallbackCulture);
        }

        // <para lang="zh">查找父资源</para>
        // <para lang="en">Find parent resources</para>
        var parentName = GetParentCultureName(cultureName).Value;
        if (!string.IsNullOrEmpty(parentName) && !EqualFallbackCulture(parentName))
        {
            AddStream(parentName);
        }

        // <para lang="zh">当前文化资源</para>
        // <para lang="en">Current culture resources</para>
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

        // <para lang="zh">开启回落机制并且当前文化信息与回落语言相同</para>
        // <para lang="en">Open the fallback mechanism and the current cultural information is the same as the fallback language</para>
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
