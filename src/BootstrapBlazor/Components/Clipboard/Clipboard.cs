// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

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

        // 注册 ClipboardService 弹窗事件
        ClipboardService.Register(this, Copy);
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
    /// 根据 MimeType 读取剪切板内容方法
    /// </summary>
    /// <param name="mimeType"></param>
    /// <returns></returns>
    private Task<List<ClipboardItem>?> GetAllClipboardContentsAsync() => InvokeAsync<List<ClipboardItem>?>("getAllClipboardContents");

    /// <summary>
    /// Dispose 方法
    /// </summary>
    /// <param name="disposing"></param>
    protected override async ValueTask DisposeAsync(bool disposing)
    {
        if (disposing)
        {
            ClipboardService.UnRegister(this);
            ClipboardService.UnRegisterGetAllContents();
        }
        await base.DisposeAsync(disposing);
    }
}
