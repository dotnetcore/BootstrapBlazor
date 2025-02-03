// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// 粘贴板服务
/// </summary>
public class ClipboardService(IJSRuntime jSRuntime)
{
    [NotNull]
    private JSModule? _module = null;

    private Task<JSModule> LoadModule() => jSRuntime.LoadUtility();

    /// <summary>
    /// 获取剪切板数据方法
    /// </summary>
    public async Task<List<ClipboardItem>> Get(CancellationToken token = default)
    {
        _module ??= await LoadModule();
        return await _module.InvokeAsync<List<ClipboardItem>?>("getAllClipboardContents", token) ?? [];
    }

    /// <summary>
    /// 获得剪切板拷贝文字方法
    /// </summary>
    /// <returns></returns>
    public async Task<string?> GetText(CancellationToken token = default)
    {
        _module ??= await LoadModule();
        return await _module.InvokeAsync<string?>("getTextFromClipboard", token);
    }

    /// <summary>
    /// 将指定文本设置到剪切板方法
    /// </summary>
    /// <param name="text">要拷贝的文字</param>
    /// <param name="callback">拷贝后回调方法</param>
    /// <param name="token"></param>
    /// <returns></returns>
    public async Task Copy(string? text, Func<Task>? callback = null, CancellationToken token = default)
    {
        _module ??= await LoadModule();
        await _module.InvokeAsync<string?>("copy", token, text);
        if (callback != null)
        {
            await callback();
        }
    }
}
