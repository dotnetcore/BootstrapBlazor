// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">搜索表单项本地化配置类</para>
/// <para lang="en">Search form item localization configuration class</para>
/// </summary>
public readonly record struct SearchFormLocalizerOptions
{
    /// <summary>
    /// <para lang="zh">全选文本</para>
    /// <para lang="en">Select all text</para>
    /// </summary>
    public string SelectAllText { get; init; }

    /// <summary>
    /// <para lang="zh">布尔值全部文本</para>
    /// <para lang="en">Boolean all text</para>
    /// </summary>
    public string BooleanAllText { get; init; }

    /// <summary>
    /// <para lang="zh">布尔值为真文本</para>
    /// <para lang="en">Boolean true text</para>
    /// </summary>
    public string BooleanTrueText { get; init; }

    /// <summary>
    /// <para lang="zh">布尔值为假文本</para>
    /// <para lang="en">Boolean false text</para>
    /// </summary>
    public string BooleanFalseText { get; init; }

    /// <summary>
    /// <para lang="zh">数字起始值标签文本</para>
    /// <para lang="en">Number start value label text</para>
    /// </summary>
    public string NumberStartValueLabelText { get; init; }

    /// <summary>
    /// <para lang="zh">数字结束值标签文本</para>
    /// <para lang="en">Number end value label text</para>
    /// </summary>
    public string NumberEndValueLabelText { get; init; }
}
