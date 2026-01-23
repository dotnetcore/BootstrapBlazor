// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;
using System.Reflection;

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">JsonLocalizationOptions 扩展方法</para>
/// <para lang="en">JsonLocalizationOptions extension methods</para>
/// </summary>
internal static class LocalizationOptionsExtensions
{
    /// <summary>
    /// <para lang="zh">通过系统 <see cref="JsonLocalizationOptions"/> 获取当前 Json 格式资源配置集合</para>
    /// <para lang="en">Get the current Json format resource configuration collection through the system <see cref="JsonLocalizationOptions"/></para>
    /// </summary>
    /// <param name="option"></param>
    /// <param name="assembly"></param>
    /// <param name="cultureName"></param>
    public static IEnumerable<IConfigurationSection> GetJsonStringFromAssembly(this JsonLocalizationOptions option, Assembly assembly, string cultureName)
    {
        var builder = new ConfigurationBuilder();
        var assemblies = new List<Assembly>() { assembly };
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

        foreach (var s in streams)
        {
            builder.AddJsonStream(s);
        }

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

        var config = builder.Build();

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

        if (option.EnableFallbackCulture)
        {
            AddStream(option.FallbackCulture);
        }

        var parentName = GetParentCultureName(cultureName).Value;
        if (!string.IsNullOrEmpty(parentName) && !EqualFallbackCulture(parentName))
        {
            AddStream(parentName);
        }

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
