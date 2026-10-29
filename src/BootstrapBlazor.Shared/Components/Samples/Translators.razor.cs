// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using System.Globalization;

namespace BootstrapBlazor.Shared.Components.Samples;

/// <summary>
/// <para lang="zh">翻译示例</para>
/// <para lang="en">Translation sample.</para>
/// </summary>
public partial class Translators
{
    private readonly List<SelectedItem> _languages = [];

    private List<string> _selectedLanguages = [];

    private static readonly string[] sourceArray = ["zh-CN", "en-US", "ru-RU"];

    private string _input = "";

    private List<TranslationResult> _results = [];

    [Inject]
    [NotNull]
    private ISampleTranslationService? TranslatorService { get; set; }

    [Inject]
    [NotNull]
    private IStringLocalizer<Translators>? Localizer { get; set; }

    [Inject]
    [NotNull]
    private ToastService? ToastService { get; set; }

    [Inject]
    [NotNull]
    private ILogger<Translators>? Logger { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        _languages.AddRange(sourceArray.Select(i => new SelectedItem(i, new CultureInfo(i).NativeName)));
        _selectedLanguages.AddRange(sourceArray);
    }

    private async Task OnClickTranslate()
    {
        if (string.IsNullOrWhiteSpace(_input) || _input.Length > 5000 || _selectedLanguages.Count == 0)
        {
            await ToastService.Information(Localizer["TranslatorsTitle"], Localizer["TranslatorsValidation"]);
            return;
        }
        _results = [];
        try
        {
            _results = await TranslatorService.TranslateAsync(new TranslationRequest
            {
                Text = _input,
                TargetLanguages = [.. _selectedLanguages]
            });
        }
        catch (Exception ex) when (ex is HttpRequestException or InvalidOperationException)
        {
            Logger.LogError(ex, "Translation sample request failed.");
            await ToastService.Error(Localizer["TranslatorsTitle"], Localizer["TranslatorsFailed"]);
        }
    }

    private static string FormatResult(TranslationResult translation)
    {
        var culture = new CultureInfo(translation.TargetLanguage);
        return $"{culture.NativeName}: {translation.Text}";
    }
}
