// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Azure;
using Azure.AI.Translation.Text;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace BootstrapBlazor.Components;

/// <summary>
/// AzureTranslatorService 服务实现类
/// </summary>
/// <remarks>
/// 构造函数
/// </remarks>
/// <param name="option"></param>
/// <param name="logger"></param>
class AzureTranslatorService(IOptionsMonitor<AzureTranslatorOption> option, ILogger<AzureTranslatorService> logger) : IAzureTranslatorService
{
    private readonly IOptionsMonitor<AzureTranslatorOption> _option = option;

    private readonly ILogger<AzureTranslatorService> _logger = logger;

    /// <summary>
    /// 文本翻译方法
    /// </summary>
    /// <param name="targetLanguage">目标语言</param>
    /// <param name="inputText">输入文本</param>
    /// <param name="sourceLanguage">源语言</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<IReadOnlyList<TranslatedTextItem>> TranslateAsync(string targetLanguage, string inputText, string? sourceLanguage = null, CancellationToken cancellationToken = default)
    {
        var key = _option.CurrentValue.Key;
        var region = _option.CurrentValue.Region;
        var client = new TextTranslationClient(new AzureKeyCredential(key), region);
        IReadOnlyList<TranslatedTextItem>? ret = null;
        try
        {
            var response = await client.TranslateAsync(targetLanguage, inputText, sourceLanguage, cancellationToken);
            ret = response.Value;
        }
        catch (RequestFailedException ex)
        {
            _logger.LogError(ex, "TranslateAsync method failed target language: {targetLanguage}, input text: {inputText}", targetLanguage, inputText);
        }
        return ret ?? new List<TranslatedTextItem>();
    }

    /// <summary>
    /// 文本翻译方法
    /// </summary>
    /// <param name="targetLanguage">目标语言</param>
    /// <param name="content">输入文本集合</param>
    /// <param name="sourceLanguage">源语言</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<IReadOnlyList<TranslatedTextItem>> TranslateAsync(string targetLanguage, IEnumerable<string> content, string? sourceLanguage = null, CancellationToken cancellationToken = default)
    {
        var key = _option.CurrentValue.Key;
        var region = _option.CurrentValue.Region;
        var client = new TextTranslationClient(new AzureKeyCredential(key), region);
        IReadOnlyList<TranslatedTextItem>? ret = null;
        try
        {
            var response = await client.TranslateAsync(targetLanguage, content, sourceLanguage, cancellationToken);
            ret = response.Value;
        }
        catch (RequestFailedException ex)
        {
            _logger.LogError(ex, "TranslateAsync method failed target language: {targetLanguage}, content: {inputText}", targetLanguage, content);
        }
        return ret ?? new List<TranslatedTextItem>();
    }

    /// <summary>
    /// 文本翻译方法
    /// </summary>
    /// <param name="targetLanguages">目标语言集合</param>
    /// <param name="content">输入文本集合</param>
    /// <param name="sourceLanguage">源语言</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<IReadOnlyList<TranslatedTextItem>> TranslateAsync(IEnumerable<string> targetLanguages, IEnumerable<string> content, string? sourceLanguage = null, CancellationToken cancellationToken = default)
    {
        var key = _option.CurrentValue.Key;
        var region = _option.CurrentValue.Region;
        var client = new TextTranslationClient(new AzureKeyCredential(key), region);
        IReadOnlyList<TranslatedTextItem>? ret = null;
        try
        {
            var response = await client.TranslateAsync(targetLanguages, content, sourceLanguage: sourceLanguage, cancellationToken: cancellationToken);
            ret = response.Value;
        }
        catch (RequestFailedException ex)
        {
            _logger.LogError(ex, "TranslateAsync method failed target language: {targetLanguage}, content: {inputText}", targetLanguages, content);
        }
        return ret ?? new List<TranslatedTextItem>();
    }
}
