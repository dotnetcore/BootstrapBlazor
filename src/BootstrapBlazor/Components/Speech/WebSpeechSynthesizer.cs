// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// WebSpeechSynthesizer 类
/// </summary>
public class WebSpeechSynthesizer(JSModule module, IComponentIdGenerator componentIdGenerator)
{
    private DotNetObjectReference<WebSpeechSynthesizer>? _interop;

    private string? _id;

    /// <summary>
    /// 开始朗读方法
    /// </summary>
    /// <param name="text"></param>
    /// <param name="lang"></param>
    public async Task SpeakAsync(string? text, string? lang = null)
    {
        _id = componentIdGenerator.Generate(this);
        _interop = DotNetObjectReference.Create(this);
        await module.InvokeVoidAsync("speak", _id, _interop, new { text, lang });
    }

    /// <summary>
    /// 暂停朗读方法
    /// </summary>
    /// <returns></returns>
    public async Task Pause()
    {
        await module.InvokeVoidAsync("pause", _id);
    }

    /// <summary>
    /// 恢复朗读方法
    /// </summary>
    /// <returns></returns>
    public async Task Resume()
    {
        await module.InvokeVoidAsync("resume", _id);
    }
}
