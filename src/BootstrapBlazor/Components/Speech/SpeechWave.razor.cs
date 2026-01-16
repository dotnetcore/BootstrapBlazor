// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">语音识别波形图组件</para>
/// <para lang="en">Speech Recognition Waveform Component</para>
/// </summary>
public partial class SpeechWave : IDisposable
{
    /// <summary>
    /// <para lang="zh">获得/设置 是否显示波形图 默认 false</para>
    /// <para lang="en">Get/Set Whether to show waveform. Default false</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public bool Show { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否显示已用时长 默认 true</para>
    /// <para lang="en">Get/Set Whether to show used time. Default true</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public bool ShowUsedTime { get; set; } = true;

    /// <summary>
    /// <para lang="zh">获得/设置 倒计时结束时回调委托</para>
    /// <para lang="en">Get/Set Callback delegate when countdown ends</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public Func<Task>? OnTimeout { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 总时长 默认 60 000 毫秒</para>
    /// <para lang="en">Get/Set Total Time. Default 60000 ms</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public int TotalTime { get; set; } = 60 * 1000;

    private string? ClassString => CssBuilder.Default("speech-wave")
        .AddClass("invisible", !Show)
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    private string? LineClassString => CssBuilder.Default("speech-wave-line")
        .AddClass("line", Show)
        .Build();

    private string? TotalTimeSpanString => $"{TimeSpan.FromMilliseconds(TotalTime):mm\\:ss}";

    private string? UsedTimeSpanString => $"{_usedTimeSpan:mm\\:ss}";

    private bool _run;
    private TimeSpan _usedTimeSpan;
    private CancellationTokenSource? _token;

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <returns></returns>
    protected override async Task OnParametersSetAsync()
    {

        await base.OnParametersSetAsync();

        if (Show)
        {
            await Run();
        }
        else
        {
            Stop();
        }
    }

    private bool IsShow => _token is { IsCancellationRequested: false };

    private async Task Run()
    {
        if (!_run)
        {
            _run = true;
            _usedTimeSpan = TimeSpan.Zero;
            _token ??= new CancellationTokenSource();
            while (IsShow)
            {
                try
                {
                    await Task.Delay(1000, _token.Token);
                    _usedTimeSpan = _usedTimeSpan.Add(TimeSpan.FromSeconds(1));
                    if (_usedTimeSpan.TotalMilliseconds >= TotalTime)
                    {
                        Show = false;
                        if (OnTimeout != null)
                        {
                            await OnTimeout();
                        }
                    }

                    if (ShowUsedTime || Show == false)
                    {
                        StateHasChanged();
                    }
                }
                catch (TaskCanceledException)
                {
                    break;
                }
            }
            _run = false;
        }
    }

    private void Stop()
    {
        if (_token != null)
        {
            _token.Cancel();
            _token.Dispose();
            _token = null;
        }
    }

    /// <summary>
    /// <para lang="zh">Dispose 方法</para>
    /// <para lang="en">Dispose 方法</para>
    /// </summary>
    /// <param name="disposing"></param>
    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            Stop();
        }
    }

    /// <summary>
    /// <para lang="zh">Dispose 方法</para>
    /// <para lang="en">Dispose 方法</para>
    /// </summary>
    /// <returns></returns>
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}
