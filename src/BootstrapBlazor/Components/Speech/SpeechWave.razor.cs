// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;

namespace BootstrapBlazor.Components;

/// <summary>
/// 语音识别波形图组件
/// </summary>
public partial class SpeechWave : IDisposable
{
    /// <summary>
    /// 获得/设置 是否开始 默认 false
    /// </summary>
    [Parameter]
    public bool Show { get; set; }

    /// <summary>
    /// 获得/设置 是否显示已用时长 默认 true
    /// </summary>
    [Parameter]
    public bool ShowUsedTime { get; set; } = true;

    /// <summary>
    /// 获得/设置 倒计时结束时回调委托
    /// </summary>
    [Parameter]
    public Func<Task>? OnTimeout { get; set; }

    /// <summary>
    /// 获得/设置 总时长 默认 60 秒
    /// </summary>
    [Parameter]
    public int TotalTimeSecond { get; set; } = 60;

    private TimeSpan UsedTimeSpan { get; set; }

    private CancellationTokenSource? Token { get; set; }

    private string? ClassString => CssBuilder.Default("speech-wave")
        .AddClass("invisible", !Show)
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    private string? LineClassString => CssBuilder.Default("speech-wave-line")
        .AddClass("line", Show)
        .Build();

    private string? TotalTimeSpanString => $"{TimeSpan.FromSeconds(TotalTimeSecond):mm\\:ss}";

    private string? UsedTimeSpanString => $"{UsedTimeSpan:mm\\:ss}";

    /// <summary>
    /// OnParametersSet 方法
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        if (Show && ShowUsedTime)
        {
            if (!IsRun)
            {
                _ = Run();
            }
        }
        else
        {
            if (Token != null)
            {
                Token.Cancel();
                Token.Dispose();
                Token = null;
            }
        }
    }

    private bool IsRun { get; set; }

    private async Task Run()
    {
        IsRun = true;
        UsedTimeSpan = TimeSpan.Zero;
        Token ??= new CancellationTokenSource();
        while (Token != null && !Token.IsCancellationRequested)
        {
            try
            {
                await Task.Delay(1000, Token.Token);
                UsedTimeSpan = UsedTimeSpan.Add(TimeSpan.FromSeconds(1));
                if (UsedTimeSpan.TotalSeconds >= TotalTimeSecond)
                {
                    Show = false;
                    if (OnTimeout != null)
                    {
                        await OnTimeout();
                    }
                }
                if (Show)
                {
                    await InvokeAsync(StateHasChanged);
                }
            }
            catch
            {
                break;
            }
        }
        IsRun = false;
    }

    /// <summary>
    /// Dispose 方法
    /// </summary>
    /// <param name="disposing"></param>
    protected void Dispose(bool disposing)
    {
        if (disposing)
        {
            if (Token != null)
            {
                Token.Cancel();
                Token.Dispose();
                Token = null;
            }
        }
    }

    /// <summary>
    /// Dispose 方法
    /// </summary>
    /// <returns></returns>
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}
