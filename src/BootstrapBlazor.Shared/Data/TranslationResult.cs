// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Shared.Data;

/// <summary>
/// <para lang="zh">不依赖 Azure SDK 的翻译结果。</para>
/// <para lang="en">A translation result independent of the Azure SDK.</para>
/// </summary>
public sealed class TranslationResult
{
    /// <summary>
    /// <para lang="zh">目标语言。</para>
    /// <para lang="en">The target language.</para>
    /// </summary>
    public string TargetLanguage { get; set; } = "";

    /// <summary>
    /// <para lang="zh">翻译后的文本。</para>
    /// <para lang="en">The translated text.</para>
    /// </summary>
    public string Text { get; set; } = "";
}
