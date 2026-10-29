// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.AspNetCore.Mvc;

namespace BootstrapBlazor.Controllers.Api;

/// <summary>
/// <para lang="zh">为 WebAssembly 翻译示例提供后台接口。</para>
/// <para lang="en">Provides the backend API for the WebAssembly translation sample.</para>
/// </summary>
[ApiController]
public sealed class TranslatorController(ISampleTranslationService translator) : ControllerBase
{
    /// <summary>
    /// <para lang="zh">翻译受限长度的文本，仅接受示例支持的目标语言。</para>
    /// <para lang="en">Translates bounded text into only the target languages supported by the sample.</para>
    /// </summary>
    [HttpPost("api/translator")]
    [Consumes("application/json")]
    [RequestSizeLimit(64 * 1024)]
    public async Task<ActionResult<List<TranslationResult>>> Translate(TranslationRequest request, CancellationToken cancellationToken)
    {
        if (request.TargetLanguages.Any(language => language is not ("zh-CN" or "en-US" or "ru-RU")) ||
            request.TargetLanguages.Distinct().Count() != request.TargetLanguages.Length)
        {
            return Problem(detail: "Select distinct supported target languages.", statusCode: 400);
        }
        Response.Headers.CacheControl = "no-store";
        return await translator.TranslateAsync(request, cancellationToken);
    }
}
