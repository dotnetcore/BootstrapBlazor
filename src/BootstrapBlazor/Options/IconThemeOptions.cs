// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

#if NET8_0_OR_GREATER
using System.Collections.Frozen;
#endif

namespace BootstrapBlazor.Components;

/// <summary>
/// IconTheme 配置项
/// </summary>
public class IconThemeOptions
{
    /// <summary>
    /// 获得/设置 主题 Icons 集合
    /// </summary>
#if NET8_0_OR_GREATER
    public FrozenDictionary<string, FrozenDictionary<ComponentIcons, string>> Icons { get; private set; }
#else
    public Dictionary<string, Dictionary<ComponentIcons, string>> Icons { get; private set; }
#endif

    /// <summary>
    /// 获得/设置 当前主题键值 默认 fa 使用 font-awesome 图标集
    /// </summary>
    public string ThemeKey { get; set; }

    /// <summary>
    /// 构造函数
    /// </summary>
    public IconThemeOptions()
    {
#if NET8_0_OR_GREATER
        Icons = new Dictionary<string, FrozenDictionary<ComponentIcons, string>>()
        {
            { "fa", FontAwesomeIcons.Icons.ToFrozenDictionary() }
        }.ToFrozenDictionary();
#else
        Icons = new()
        {
            { "fa", FontAwesomeIcons.Icons }
        };
#endif
        ThemeKey = "fa";
    }
}
