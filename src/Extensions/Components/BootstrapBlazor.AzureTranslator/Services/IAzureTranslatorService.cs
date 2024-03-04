// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Azure.AI.Translation.Text;

namespace BootstrapBlazor.Components;

/// <summary>
/// IAzureTranslatorService 文本翻译服务
/// </summary>
public interface IAzureTranslatorService
{
    /// <summary>
    /// 翻译方法
    /// </summary>
    /// <param name="targetLanguage"></param>
    /// <param name="inputText"></param>
    /// <param name="sourceLanguage"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<IReadOnlyList<TranslatedTextItem>> TranslateAsync(string targetLanguage, string inputText, string? sourceLanguage = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// 翻译方法
    /// </summary>
    /// <param name="targetLanguage"></param>
    /// <param name="content"></param>
    /// <param name="sourceLanguage"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<IReadOnlyList<TranslatedTextItem>> TranslateAsync(string targetLanguage, IEnumerable<string> content, string? sourceLanguage = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// 翻译方法
    /// </summary>
    /// <param name="targetLanguages"></param>
    /// <param name="content"></param>
    /// <param name="sourceLanguage"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<IReadOnlyList<TranslatedTextItem>> TranslateAsync(IEnumerable<string> targetLanguages, IEnumerable<string> content, string? sourceLanguage = null, CancellationToken cancellationToken = default);
}
