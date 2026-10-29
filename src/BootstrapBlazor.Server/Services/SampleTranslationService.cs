// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Server.Services;

/// <summary>
/// <para lang="zh">保留 Server 示例原有的 Azure 翻译调用。</para>
/// <para lang="en">Preserves the Server sample's existing Azure translation call.</para>
/// </summary>
public sealed class SampleTranslationService(IAzureTranslatorService translator) : ISampleTranslationService
{
    /// <inheritdoc />
    public async Task<List<TranslationResult>> TranslateAsync(TranslationRequest request, CancellationToken cancellationToken = default)
    {
        var results = await translator.TranslateAsync(request.TargetLanguages, [request.Text], "en-US", cancellationToken);
        if (results.Count == 0 || results[0].Translations.Count == 0)
        {
            throw new InvalidOperationException("The translation provider did not return results.");
        }
        return results[0].Translations.Select(translation => new TranslationResult
        {
            TargetLanguage = translation.TargetLanguage,
            Text = translation.Text
        }).ToList();
    }
}
