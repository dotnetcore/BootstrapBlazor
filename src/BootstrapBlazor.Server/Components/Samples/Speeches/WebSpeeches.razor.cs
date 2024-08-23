// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

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
    private string? _text = "开始朗读一段文字";
    private string? _buttonText = "开始合成";
    private WebSpeechSynthesizer _entry = default!;
    private TaskCompletionSource? _tcs;
    private WebSpeechSynthesisVoice? _voice;
    private readonly List<SelectedItem> _voices = [];

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
        _voices.AddRange(voices.Select(i => new SelectedItem(i.voiceURI!, $"{i.Name}({i.Lang})")));
        StateHasChanged();
    }

    private async Task OnStart()
    {
        if (!string.IsNullOrEmpty(_text))
        {
            _tcs ??= new();
            _star = true;
            StateHasChanged();

            await _entry.SpeakAsync(_text, "zh-CN");
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
}
