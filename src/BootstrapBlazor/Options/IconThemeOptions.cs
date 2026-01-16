// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

#if NET8_0_OR_GREATER
using System.Collections.Frozen;
#endif

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">IconTheme 配置项</para>
/// <para lang="en">IconTheme configuration options</para>
/// </summary>
public class IconThemeOptions
{
    /// <summary>
    /// <para lang="zh">获得/设置 主题 Icons 集合</para>
    /// <para lang="en">Get/Set theme Icons collection</para>
    /// </summary>
#if NET8_0_OR_GREATER
    public FrozenDictionary<string, FrozenDictionary<ComponentIcons, string>> Icons { get; private set; }
#else
    public Dictionary<string, Dictionary<ComponentIcons, string>> Icons { get; private set; }
#endif

    /// <summary>
    /// <para lang="zh">获得/设置 当前主题键值 默认 fa 使用 font-awesome 图标集</para>
    /// <para lang="en">Get/Set current theme key default fa using font-awesome icon set</para>
    /// </summary>
    public string ThemeKey { get; set; }

    /// <summary>
    /// <para lang="zh">构造函数</para>
    /// <para lang="en">Constructor</para>
    /// </summary>
    public IconThemeOptions()
    {
#if NET8_0_OR_GREATER
        Icons = new Dictionary<string, FrozenDictionary<ComponentIcons, string>>()
        {
            { "fa", FontAwesomeIcons.Icons.ToFrozenDictionary() },
            { "bootstrap", BootstrapIcons.Icons.ToFrozenDictionary() },
            { "mdi", MaterialDesignIcons.Icons.ToFrozenDictionary() }
        }.ToFrozenDictionary();
#else
        Icons = new()
        {
            { "fa", FontAwesomeIcons.Icons },
            { "bootstrap", BootstrapIcons.Icons },
            { "mdi", MaterialDesignIcons.Icons }
        };
#endif
        ThemeKey = "fa";
    }

#if NET8_0_OR_GREATER
    /// <summary>
    /// <para lang="zh">尝试通过指定键值添加图标集合</para>
    /// <para lang="en">Try to add icon collection by specified key</para>
    /// </summary>
    /// <param name="key"></param>
    /// <param name="icons"></param>
    public void TryAddIcons(string key, FrozenDictionary<ComponentIcons, string> icons)
    {
        var originalIcons = Icons.ToDictionary();
        if (originalIcons.TryAdd(key, icons))
        {
            Icons = originalIcons.ToFrozenDictionary();
        }
    }
#endif
}
