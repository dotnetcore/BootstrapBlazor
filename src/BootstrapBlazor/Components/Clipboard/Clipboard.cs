// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// Clipboard 组件部分类
/// </summary>
[BootstrapModuleAutoLoader(ModuleName = "utility", AutoInvokeInit = false, AutoInvokeDispose = false)]
public class Clipboard : BootstrapModuleComponentBase
{
    /// <summary>
    /// DialogServices 服务实例
    /// </summary>
    [Inject]
    [NotNull]
    private ClipboardService? ClipboardService { get; set; }

    /// <summary>
    /// OnInitialized 方法
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        // 注册 ClipboardService 回调方法
        ClipboardService.Register(this, Copy);
        ClipboardService.RegisterGetText(GetText);
        ClipboardService.RegisterGetAllContents(GetAllClipboardContentsAsync);
    }

    /// <summary>
    /// 复制方法
    /// </summary>
    /// <param name="option"></param>
    /// <returns></returns>
    private async Task Copy(ClipboardOption option)
    {
        await InvokeVoidAsync("copy", option.Text);

        if (option.Callback != null)
        {
            await option.Callback();
        }
    }

    /// <summary>
    /// 读取剪切板拷贝文字方法
    /// </summary>
    /// <returns></returns>
    private Task<string?> GetText() => InvokeAsync<string?>("getTextFromClipboard");

    /// <summary>
    /// 读取剪切板内容方法
    /// </summary>
    /// <returns></returns>
    private Task<List<ClipboardItem>?> GetAllClipboardContentsAsync() => InvokeAsync<List<ClipboardItem>?>("getAllClipboardContents");

    /// <summary>
    /// Dispose 方法
    /// </summary>
    /// <param name="disposing"></param>
    protected override async ValueTask DisposeAsync(bool disposing)
    {
        await base.DisposeAsync(disposing);

        if (disposing)
        {
            ClipboardService.UnRegister(this);
            ClipboardService.UnRegisterGetText();
            ClipboardService.UnRegisterGetAllContents();
        }
    }
}
