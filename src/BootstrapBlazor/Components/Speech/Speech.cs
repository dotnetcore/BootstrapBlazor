// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;

namespace BootstrapBlazor.Components;

/// <summary>
/// Speech 组件
/// </summary>
public class Speech : BootstrapComponentBase, IAsyncDisposable
{
    [Inject]
    [NotNull]
    private SpeechService? SpeechService { get; set; }

    /// <summary>
    /// OnInitialized 方法
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        // 注册 Dialog 弹窗事件
        SpeechService.Register(this, Invoke);
    }

    /// <summary>
    /// Invoke 方法
    /// </summary>
    /// <param name="option"></param>
    private static async Task Invoke(SpeechOption option)
    {
        if (option.Provider != null)
        {
            await option.Provider.InvokeAsync(option);
        }
    }

    /// <summary>
    /// DisposeAsync 方法
    /// </summary>
    /// <param name="disposing"></param>
    protected virtual ValueTask DisposeAsync(bool disposing)
    {
        if (disposing)
        {
            SpeechService.UnRegister(this);
        }
        return ValueTask.CompletedTask;
    }

    /// <summary>
    /// DisposeAsync 方法
    /// </summary>
    public async ValueTask DisposeAsync()
    {
        await DisposeAsync(true);
        GC.SuppressFinalize(this);
    }
}
