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
    private ToastService? ToastService { get; set; }

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

    private bool _starRecognition;
    private WebSpeechRecognition _recognition = default!;
    private string? _buttonRecognitionText;
    private string? _result;

    private bool _starRecognitionContinuous;
    private string? _buttonRecognitionContinuousText;
    private string? _finalResult;
    private string? _tempResult;
    private WebSpeechRecognition _recognitionContinuous = default!;

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <returns></returns>
    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();

        // create synthesizer
        _entry = await WebSpeechService.CreateSynthesizerAsync();
        _entry.OnEndAsync = SpeakAsync;

        var voices = await _entry.GetVoices();
        if (voices != null)
        {
            _speechVoices.AddRange(voices);
        }

        _voices.AddRange(_speechVoices.Select(i => new SelectedItem($"{i.Name}({i.Lang})", $"{i.Name}({i.Lang})")));
        var voice = _speechVoices.Find(i => i.Lang == CultureInfo.CurrentUICulture.Name);
        if (voice != null)
        {
            _voiceName = $"{voice.Name}({voice.Lang})";
        }

        _text = Localizer["WebSpeechText"];
        _buttonText = Localizer["WebSpeechSpeakButtonText"];
        _buttonStopText = Localizer["WebSpeechStopButtonText"];

        // create recognition
        _buttonRecognitionText = Localizer["WebSpeechRecognitionButtonText"];
        _recognition = await WebSpeechService.CreateRecognitionAsync();
        _recognition.OnSpeechStartAsync = () =>
        {
            _starRecognition = true;
            StateHasChanged();
            return Task.CompletedTask;
        };
        _recognition.OnSpeechEndAsync = () =>
        {
            _starRecognition = false;
            StateHasChanged();
            return Task.CompletedTask;
        };
        _recognition.OnErrorAsync = async e =>
        {
            e.ParseErrorMessage(Localizer);
            await ToastService.Error("Recognition", e.Message);
        };
        _recognition.OnResultAsync = e =>
        {
            _result = e.Transcript;
            StateHasChanged();
            return Task.CompletedTask;
        };

        // create recognition continuous
        _buttonRecognitionContinuousText = Localizer["WebSpeechRecognitionContinuousButtonText"];
        _recognitionContinuous = await WebSpeechService.CreateRecognitionAsync();
        _recognitionContinuous.OnSpeechStartAsync = () =>
        {
            _starRecognitionContinuous = true;
            StateHasChanged();
            return Task.CompletedTask;
        };
        _recognitionContinuous.OnSpeechEndAsync = () =>
        {
            _starRecognitionContinuous = false;
            StateHasChanged();
            return Task.CompletedTask;
        };
        _recognitionContinuous.OnErrorAsync = async e =>
        {
            e.ParseErrorMessage(Localizer);
            await ToastService.Error("Recognition", e.Message);
        };
        _recognitionContinuous.OnResultAsync = e =>
        {
            if (e.IsFinal)
            {
                _finalResult += e.Transcript;
                _tempResult = string.Empty;
            }
            else
            {
                _tempResult = e.Transcript;
            }
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

            await _entry.SpeakAsync(_text, _speechVoices.Find(i => $"{i.Name}({i.Lang})" == _voiceName));
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
        await _recognition.StartAsync(CultureInfo.CurrentUICulture.Name);
        StateHasChanged();
    }

    private async Task OnStartContinuousRecognition()
    {
        _tempResult = "";
        _finalResult = "";
        await _recognitionContinuous.StartAsync(new WebSpeechRecognitionOption()
        {
            Lang = CultureInfo.CurrentUICulture.Name,
            Continuous = true,
            InterimResults = true
        });
        StateHasChanged();
    }

    private async Task OnStopContinuousRecognition()
    {
        await _recognitionContinuous.StopAsync();
        StateHasChanged();
    }
}
