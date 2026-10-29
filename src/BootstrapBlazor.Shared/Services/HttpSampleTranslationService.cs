// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Shared.Services;

/// <summary>
/// <para lang="zh">通过同源后台接口翻译文本，不向浏览器公开 Azure 密钥。</para>
/// <para lang="en">Translates through the same-origin API without exposing Azure keys to the browser.</para>
/// </summary>
public sealed class HttpSampleTranslationService(HttpClient client) : ISampleTranslationService
{
    /// <inheritdoc />
    public async Task<List<TranslationResult>> TranslateAsync(TranslationRequest request, CancellationToken cancellationToken = default)
    {
        using var response = await client.PostAsJsonAsync("api/translator", request, cancellationToken);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<List<TranslationResult>>(cancellationToken)
            ?? throw new InvalidOperationException("The server did not return translation results.");
    }
}
