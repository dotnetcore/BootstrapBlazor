// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
///  <para lang="zh">粘贴板服务</para>
///  <para lang="en">Clipboard Service</para>
/// </summary>
public class ClipboardService(IJSRuntime jSRuntime)
{
    [NotNull]
    private JSModule? _module = null;

    private Task<JSModule> LoadModule() => jSRuntime.LoadUtility();

    /// <summary>
    ///  <para lang="zh">获取剪切板数据方法</para>
    ///  <para lang="en">Get Clipboard Data Method</para>
    /// </summary>
    public async Task<List<ClipboardItem>> Get(CancellationToken token = default)
    {
        _module ??= await LoadModule();
        return await _module.InvokeAsync<List<ClipboardItem>?>("getAllClipboardContents", token) ?? [];
    }

    /// <summary>
    ///  <para lang="zh">获得剪切板拷贝文字方法</para>
    ///  <para lang="en">Get Clipboard Text Method</para>
    /// </summary>
    /// <returns></returns>
    public async Task<string?> GetText(CancellationToken token = default)
    {
        _module ??= await LoadModule();
        return await _module.InvokeAsync<string?>("getTextFromClipboard", token);
    }

    /// <summary>
    ///  <para lang="zh">将指定文本设置到剪切板方法</para>
    ///  <para lang="en">Set Copy Text to Clipboard Method</para>
    /// </summary>
    /// <param name="text"><para lang="zh">要拷贝的文字</para><para lang="en">要拷贝的文字</para></param>
    /// <param name="callback"><para lang="zh">拷贝后回调方法</para><para lang="en">拷贝后callback method</para></param>
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
