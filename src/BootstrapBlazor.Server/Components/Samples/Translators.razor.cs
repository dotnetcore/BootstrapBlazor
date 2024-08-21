﻿// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Azure.AI.Translation.Text;
using System.Globalization;

namespace BootstrapBlazor.Server.Components.Samples;

/// <summary>
/// 翻译示例
/// </summary>
public partial class Translators
{
    private readonly List<SelectedItem> _languages = [];

    private List<string> _selectedLanguages = [];

    private static readonly string[] sourceArray = ["zh-CN", "en-US", "ru-RU"];

    private string _input = "";

    private IReadOnlyList<TranslatedTextItem> _results = new List<TranslatedTextItem>();

    [Inject]
    [NotNull]
    private IAzureTranslatorService? TranslatorService { get; set; }

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
        _results = await TranslatorService.TranslateAsync(_selectedLanguages, [_input], "en-US");
    }

    private static string FormatResult(TranslationText translation)
    {
        var culture = new CultureInfo(translation.TargetLanguage);
        return $"{culture.NativeName}: {translation.Text}";
    }

    /// <summary>
    /// 获得属性方法
    /// </summary>
    /// <returns></returns>
    protected MethodItem[] GetMethods() =>
    [
        new()
        {
            Name = nameof(IAzureTranslatorService.TranslateAsync),
            Description = Localizer[nameof(IAzureTranslatorService.TranslateAsync)],
            Parameters = " - ",
            ReturnValue = "IReadOnlyList<TranslatedTextItem>"
        },
    ];
}
