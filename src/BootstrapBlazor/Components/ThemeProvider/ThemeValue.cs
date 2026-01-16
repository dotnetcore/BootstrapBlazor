// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using System.ComponentModel;

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">主题选项</para>
/// <para lang="en">主题选项</para>
/// </summary>
[JsonEnumConverter(true)]
public enum ThemeValue
{
    /// <summary>
    /// <para lang="zh">自动</para>
    /// <para lang="en">自动</para>
    /// </summary>
    [Description("auto")]
    Auto,

    /// <summary>
    /// <para lang="zh">明亮主题</para>
    /// <para lang="en">明亮主题</para>
    /// </summary>
    [Description("light")]
    Light,

    /// <summary>
    /// <para lang="zh">暗黑主题</para>
    /// <para lang="en">暗黑主题</para>
    /// </summary>
    [Description("dark")]
    Dark,

    /// <summary>
    /// <para lang="zh">使用本地保存选项</para>
    /// <para lang="en">使用本地保存选项</para>
    /// </summary>
    [Description("useLocalStorage")]
    UseLocalStorage,
}
