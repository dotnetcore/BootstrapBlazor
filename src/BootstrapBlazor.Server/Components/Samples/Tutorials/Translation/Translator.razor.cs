// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.Extensions.Options;
using System.Data;
using System.Globalization;

namespace BootstrapBlazor.Server.Components.Samples.Tutorials.Translation;

/// <summary>
/// UniTranslator 组件
/// </summary>
public partial class Translator
{
    private readonly List<SelectedItem> _supportedLanguageItems = [];

    private readonly List<SelectedItem> _jsonFiles = [];

    private List<string> _selectedLanguages = [];

    private DataTableDynamicContext? _dataTableDynamicContext;

    private string? _currentJsonFile;

    private int _progress;

    private bool _showProcess;

    private LanguageDataTable _languageTable = default!;

    private bool _showAll = true;

    private string StateText => _showAll ? Localizer["Hide"] : Localizer["Show"];

    private string StateClassString => _showAll ? "fa-solid fa-eye-slash" : "fa-solid fa-eye";

    [Inject]
    [NotNull]
    private ToastService? ToastService { get; set; }

    private string? ProgressClassString => CssBuilder.Default()
        .AddClass("d-none", !_showProcess)
        .Build();

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        var items = Configuration.GetSection("SupportedLanguages").Get<IEnumerable<string>?>();
        if (items != null)
        {
            _supportedLanguageItems.AddRange(items.Select(i =>
            {
                var cultureInfo = new CultureInfo(i);
                return new SelectedItem(cultureInfo.Name, cultureInfo.NativeName);
            }));
            _selectedLanguages.AddRange(items);

            var files = SearchResource();
            if (files.Count > 0)
            {
                _languageTable = new LanguageDataTable(_selectedLanguages)
                {
                    OnUpdate = ReloadTable
                };
                _dataTableDynamicContext = _languageTable.CreateContext();

                // 搜索文件
                _jsonFiles.AddRange(files.Select(i => new SelectedItem(i, i)));
            }
        }
    }

    private Task ReloadTable()
    {
        _dataTableDynamicContext = _languageTable.CreateContext();
        StateHasChanged();
        return Task.CompletedTask;
    }

    private Task OnClickLoadAsync() => LoadAsync(true);

    private async Task LoadAsync(bool showToast)
    {
        _languageTable = new LanguageDataTable(_selectedLanguages) { OnUpdate = ReloadTable };
        await _languageTable.LoadAsync(Path.GetDirectoryName(_currentJsonFile)!);
        if (showToast)
        {
            await ToastService.Success(Localizer["LoadTitle"], Localizer["LoadContent"]);
        }
        _dataTableDynamicContext = _languageTable.CreateContext();
    }

    private async Task OnClickTranslateAsync()
    {
        _showProcess = true;
        _progress = 10;
        if (!string.IsNullOrEmpty(_currentJsonFile))
        {
            await TranslateLanguage();
            await LoadAsync(false);
            _progress = 100;
            await Task.Delay(100);
        }
        _showProcess = false;
    }

    private static List<string> SearchResource()
    {
        var ret = new List<string>();
        var path = Path.Combine(AppContext.BaseDirectory, "../../../../BootstrapBlazor/Locales");
        Search(path);

        path = Path.Combine(AppContext.BaseDirectory, "../../../../BootstrapBlazor.Server/Locales");
        Search(path);

        return ret;

        void Search(string path)
        {
            if (Directory.Exists(path))
            {
                var sourceFiles = Directory.EnumerateFiles(path, "en-US.json", new EnumerationOptions()
                {
                    MatchCasing = MatchCasing.CaseInsensitive
                }).Select(i => new FileInfo(i).FullName);
                ret.AddRange(sourceFiles);
            }
        }
    }

    private async Task TranslateLanguage()
    {
        var fileName = _currentJsonFile;
        if (File.Exists(fileName))
        {
            // 解析 en-US.json 文件 逐条翻译，生成对应多语言文件
            var progress = 80 / _selectedLanguages.Count;

            var manager = new ConfigurationManager();
            manager.AddJsonFile(fileName);

            // 生成对应语言资源文件
            foreach (var language in _selectedLanguages)
            {
                var targetLanguageFolder = Path.GetDirectoryName(fileName)!;
                await AzureTranslatorService.TranslateLanguageAsync(manager.GetChildren(), targetLanguageFolder, language);

                _progress += progress;
                await InvokeAsync(StateHasChanged);
            }
        }
    }

    private async Task OnClickSaveAsync()
    {
        var folder = Path.GetDirectoryName(_currentJsonFile)!;
        await _languageTable.SaveAsync(folder);
        await ToastService.Success(Localizer["SaveTitle"], Localizer["SaveContent"]);
        await LoadAsync(false);
    }

    private async Task OnToggleShow()
    {
        _showAll = !_showAll;
        await _languageTable.LoadAsync(Path.GetDirectoryName(_currentJsonFile)!);
        _dataTableDynamicContext = _languageTable.CreateContext();
    }

    private string? FilterRow(DynamicObject item)
    {
        var result = _showAll || _languageTable.FilterRow(item);
        return result ? null : "d-none";
    }
}
