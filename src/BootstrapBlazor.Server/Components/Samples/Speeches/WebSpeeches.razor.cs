// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using System.Globalization;

namespace BootstrapBlazor.Server.Components.Samples.Speeches;

/// <summary>
/// WebSpeech 组件示例代码
/// </summary>
public partial class WebSpeeches
{
    [Inject, NotNull]
    private WebSpeechService? WebSpeechService { get; set; }

    [Inject, NotNull]
    private IStringLocalizer<WebSpeeches>? Localizer { get; set; }

    private bool _star;
    private string? _text;
    private string? _buttonText;
    private string? _buttonStopText;
    private WebSpeechSynthesizer _entry = default!;
    private TaskCompletionSource? _tcs;
    private string? _voiceName;
    private readonly List<SelectedItem> _voices = [];
    private readonly List<WebSpeechSynthesisVoice> _speechVoices = [];

    private WebSpeechRecognition _recognition = default!;
    private string? _result;

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <returns></returns>
    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();

        _entry = await WebSpeechService.CreateSynthesizerAsync();
        _entry.OnEndAsync = SpeakAsync;

        var voices = await _entry.GetVoices();
        if (voices != null)
        {
            _speechVoices.AddRange(voices);
        }
        _voices.AddRange(_speechVoices.Select(i => new SelectedItem(i.Name!, $"{i.Name}({i.Lang})")));
        _voiceName = _speechVoices.Find(i => i.Lang == CultureInfo.CurrentUICulture.Name)?.Name;

        _text = Localizer["WebSpeechText"];
        _buttonText = Localizer["WebSpeechSpeakButtonText"];
        _buttonStopText = Localizer["WebSpeechStopButtonText"];

        _recognition = await WebSpeechService.CreateRecognitionAsync();
        _recognition.OnResultAsync = e =>
        {
            _result = e.Results;
            StateHasChanged();
            return Task.CompletedTask;
        };
    }

    private async Task OnStart()
    {
        if (!string.IsNullOrEmpty(_text))
        {
            _tcs ??= new();
            _star = true;
            StateHasChanged();

            await _entry.SpeakAsync(_text, _speechVoices.Find(i => i.Name == _voiceName));
            await _tcs.Task;
            _star = false;
            _tcs = null;
            StateHasChanged();
        }
    }

    private Task SpeakAsync()
    {
        _tcs?.TrySetResult();
        return Task.CompletedTask;
    }

    private async Task OnStop()
    {
        await _entry.CancelAsync();
        _tcs?.TrySetResult();
    }

    private async Task OnStartRecognition()
    {
        _result = "";
        await _recognition.StartAsync();
        StateHasChanged();
    }
}
