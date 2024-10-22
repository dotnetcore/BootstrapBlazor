﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// 语音识别波形图组件
/// </summary>
public partial class SpeechWave : IDisposable
{
    /// <summary>
    /// 获得/设置 是否显示波形图 默认 false
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
    /// 获得/设置 总时长 默认 60000 毫秒
    /// </summary>
    [Parameter]
    public int TotalTime { get; set; } = 60000;

    private TimeSpan UsedTimeSpan { get; set; }

    private CancellationTokenSource? Token { get; set; }

    private string? ClassString => CssBuilder.Default("speech-wave")
        .AddClass("invisible", !Show)
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    private string? LineClassString => CssBuilder.Default("speech-wave-line")
        .AddClass("line", Show)
        .Build();

    private string? TotalTimeSpanString => $"{TimeSpan.FromMilliseconds(TotalTime):mm\\:ss}";

    private string? UsedTimeSpanString => $"{UsedTimeSpan:mm\\:ss}";

    /// <summary>
    /// OnParametersSet 方法
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        if (Show)
        {
            Run();
        }
        else
        {
            Cancel();
        }
    }

    private bool IsRun { get; set; }

    private void Run() => Task.Run(async () =>
    {
        if (!IsRun)
        {
            IsRun = true;
            UsedTimeSpan = TimeSpan.Zero;
            Token ??= new CancellationTokenSource();
            while (IsShow)
            {
                try
                {
                    await Task.Delay(1000, Token.Token);
                    UsedTimeSpan = UsedTimeSpan.Add(TimeSpan.FromSeconds(1));
                    if (UsedTimeSpan.TotalMilliseconds >= TotalTime)
                    {
                        Show = false;
                        if (OnTimeout != null)
                        {
                            await OnTimeout();
                        }
                    }
                    await InvokeAsync(StateHasChanged);
                }
                catch (TaskCanceledException)
                {
                    break;
                }
            }
            IsRun = false;
        }
    });

    private void Cancel()
    {
        if (Token != null)
        {
            Token.Cancel();
            Token.Dispose();
            Token = null;
        }
    }

    private bool IsShow => Token != null && !Token.IsCancellationRequested;

    /// <summary>
    /// Dispose 方法
    /// </summary>
    /// <param name="disposing"></param>
    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            Cancel();
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
