// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components.Forms;

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
    private string? _buttonText = "开始合成";
    private WebSpeechSynthesizer _entry = default!;

    private int _count = 2;
    private List<string> _list = ["十", "九", "八"];
    private async Task OnStart()
    {
        if (_buttonText == "开始合成")
        {
            if (!string.IsNullOrEmpty(_text))
            {
                _count = 2;
                if (_entry == null)
                {
                    _entry = await WebSpeechService.CreateSynthesizerAsync();
                    _entry.OnEndAsync = SpeakAsync;
                }
                await _entry.SpeakAsync("八", "zh-CN");
            }
        }
        else
        {
        }
    }

    private async Task SpeakAsync()
    {
        _count--;
        if (_count >= 0)
        {
            await Task.Delay(1000);
            await _entry.SpeakAsync(_list[_count], "zh-CN");
        }
    }

    private Task Synthesizer(SynthesizerStatus status)
    {
        if (status == SynthesizerStatus.Synthesizer)
        {
            _star = true;
            _buttonText = "停止合成";
        }
        else
        {
            _star = false;
            _buttonText = "开始合成";
        }
        StateHasChanged();
        return Task.CompletedTask;
    }
}
