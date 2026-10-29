// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using System.ComponentModel.DataAnnotations;

namespace BootstrapBlazor.Shared.Data;

/// <summary>
/// <para lang="zh">翻译示例的请求，源语言为英语。</para>
/// <para lang="en">A translation sample request with English as the source language.</para>
/// </summary>
public sealed class TranslationRequest
{
    /// <summary>
    /// <para lang="zh">待翻译文本，最多 5000 个字符。</para>
    /// <para lang="en">The text to translate, up to 5000 characters.</para>
    /// </summary>
    [Required]
    [StringLength(5000)]
    public string Text { get; set; } = "";

    /// <summary>
    /// <para lang="zh">示例中选择的目标语言。</para>
    /// <para lang="en">The target languages selected in the sample.</para>
    /// </summary>
    [Required]
    [MinLength(1)]
    [MaxLength(3)]
    public string[] TargetLanguages { get; set; } = [];
}
