// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.Extensions.Localization;

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">Timer 组件</para>
/// <para lang="en">Timer Component</para>
/// </summary>
[BootstrapModuleAutoLoader(ModuleName = "utility", AutoInvokeInit = false, AutoInvokeDispose = false)]
public partial class Timer
{
    /// <summary>
    /// <para lang="zh">获得 组件样式字符串</para>
    /// <para lang="en">Gets the component style string</para>
    /// </summary>
    protected override string? ClassString => CssBuilder.Default("timer")
        .AddClass(base.ClassString)
        .Build();

    private string? PauseClassString => CssBuilder.Default("btn")
        .AddClass("btn-warning", !IsPause)
        .AddClass("btn-success", IsPause)
        .Build();

    private string? ValueString => $"{Math.Round(((1 - CurrentTimespan.TotalSeconds * 1.0 / Value.TotalSeconds) * CircleLength), 2)}";

    private TimeSpan CurrentTimespan { get; set; }

    private bool IsPause { get; set; }

    private string ValueTitleString => CurrentTimespan.Hours == 0 ? $"{CurrentTimespan:mm\\:ss}" : $"{CurrentTimespan:hh\\:mm\\:ss}";

    private string? AlertTime { get; set; }

    private CancellationTokenSource CancelTokenSource { get; set; } = new();

    private bool Vibrate { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 当前值</para>
    /// <para lang="en">Gets or sets the current value</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public TimeSpan Value { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 组件宽度</para>
    /// <para lang="en">Gets or sets the component width</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public override int Width { get; set; } = 300;

    /// <summary>
    /// <para lang="zh">获得/设置 倒计时结束时的回调委托</para>
    /// <para lang="en">Gets or sets the callback delegate when the countdown ends</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public Func<Task>? OnTimeout { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 取消时的回调委托</para>
    /// <para lang="en">Gets or sets the callback delegate when cancelled</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public Func<Task>? OnCancel { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 进度条宽度，默认为 6</para>
    /// <para lang="en">Gets or sets the progress bar width. Default is 6.</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public override int StrokeWidth { get; set; } = 6;

    /// <summary>
    /// <para lang="zh">获得/设置 倒计时结束时是否设备震动</para>
    /// <para lang="en">Gets or sets whether the device vibrates when the countdown ends</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public bool IsVibrate { get; set; } = true;

    /// <summary>
    /// <para lang="zh">获得/设置 暂停按钮显示文字</para>
    /// <para lang="en">Gets or sets the pause button display text</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    [NotNull]
    public string? PauseText { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 继续按钮显示文字</para>
    /// <para lang="en">Gets or sets the resume button display text</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    [NotNull]
    public string? ResumeText { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 取消按钮显示文字</para>
    /// <para lang="en">Gets or sets the cancel button display text</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    [NotNull]
    public string? CancelText { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 开始按钮显示文字</para>
    /// <para lang="en">Gets or sets the start button display text</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    [NotNull]
    public string? StarText { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 Alert 图标</para>
    /// <para lang="en">Gets or sets the alert icon</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public string? Icon { get; set; }

    [Inject]
    [NotNull]
    private IStringLocalizer<Timer>? Localizer { get; set; }

    [Inject]
    [NotNull]
    private IIconTheme? IconTheme { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        PauseText ??= Localizer[nameof(PauseText)];
        ResumeText ??= Localizer[nameof(ResumeText)];
        CancelText ??= Localizer[nameof(CancelText)];
        StarText ??= Localizer[nameof(StarText)];

        Icon ??= IconTheme.GetIconByKey(ComponentIcons.TimerIcon);
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="firstRender"></param>
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (!firstRender)
        {
            await Timeout();
        }
    }

    private async Task Timeout()
    {
        if (Vibrate)
        {
            Vibrate = false;
            await InvokeVoidAsync("vibrate");
        }
    }

    private async Task OnStart(TimeSpan val)
    {
        Value = val;
        IsPause = false;
        CurrentTimespan = Value;
        AlertTime = DateTime.Now.Add(CurrentTimespan).ToString("HH:mm:ss");

        StateHasChanged();
        await Task.Yield();

        // 点击 Cancel 后重新设置再点击 Star
        if (CancelTokenSource.IsCancellationRequested)
        {
            CancelTokenSource.Dispose();
            CancelTokenSource = new CancellationTokenSource();
        }

        while (CancelTokenSource is { IsCancellationRequested: false } && CurrentTimespan > TimeSpan.Zero)
        {
            try
            {
                await Task.Delay(1000, CancelTokenSource.Token);

                if (IsPause)
                {
                    AlertTime = DateTime.Now.Add(CurrentTimespan).ToString("HH:mm:ss");
                }
                else
                {
                    CurrentTimespan = CurrentTimespan.Subtract(TimeSpan.FromSeconds(1));
                    StateHasChanged();
                }
            }
            catch (TaskCanceledException) { }
        }

        if (CurrentTimespan == TimeSpan.Zero)
        {
            await Task.Delay(500, CancelTokenSource.Token);
            if (!CancelTokenSource.IsCancellationRequested)
            {
                Value = TimeSpan.Zero;
                Vibrate = IsVibrate;
                StateHasChanged();
                if (OnTimeout != null)
                {
                    await OnTimeout();
                }
            }
        }
    }

    private void OnClickPause()
    {
        IsPause = !IsPause;
    }

    private string GetPauseText() => IsPause ? ResumeText : PauseText;

    private async Task OnClickCancel()
    {
        Value = TimeSpan.Zero;
        CancelTokenSource.Cancel();
        if (OnCancel != null)
        {
            await OnCancel();
        }
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="disposing"></param>
    protected override async ValueTask DisposeAsync(bool disposing)
    {
        if (disposing)
        {
            CancelTokenSource.Cancel();
            CancelTokenSource.Dispose();

            if (Module != null)
            {
                await Module.DisposeAsync();
                Module = null;
            }
        }
    }
}
