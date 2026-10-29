// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Shared.Services;

/// <summary>
/// <para lang="zh">Server 与 WebAssembly 共用的翻译示例服务。</para>
/// <para lang="en">The translation sample service shared by Server and WebAssembly.</para>
/// </summary>
public interface ISampleTranslationService
{
    /// <summary>
    /// <para lang="zh">将英语文本翻译为所选目标语言。</para>
    /// <para lang="en">Translates English text into the selected target languages.</para>
    /// </summary>
    /// <param name="request">
    /// <para lang="zh">翻译请求。</para>
    /// <para lang="en">The translation request.</para>
    /// </param>
    /// <param name="cancellationToken">
    /// <para lang="zh">取消令牌。</para>
    /// <para lang="en">The cancellation token.</para>
    /// </param>
    Task<List<TranslationResult>> TranslateAsync(TranslationRequest request, CancellationToken cancellationToken = default);
}
