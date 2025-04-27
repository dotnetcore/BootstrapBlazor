// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.Extensions.Localization;

namespace BootstrapBlazor.Components;

/// <summary>
/// Timer 组件
/// </summary>
[BootstrapModuleAutoLoader(ModuleName = "utility", AutoInvokeInit = false, AutoInvokeDispose = false)]
public partial class Timer
{
    /// <summary>
    /// 获得 组件样式字符串
    /// </summary>
    protected override string? ClassString => CssBuilder.Default("timer")
        .AddClass(base.ClassString)
        .Build();

    private string? PauseClassString => CssBuilder.Default("btn")
        .AddClass("btn-warning", !IsPause)
        .AddClass("btn-success", IsPause)
        .Build();

    /// <summary>
    /// 获得/设置 当前进度值
    /// </summary>
    private string? ValueString => $"{Math.Round(((1 - CurrentTimespan.TotalSeconds * 1.0 / Value.TotalSeconds) * CircleLength), 2)}";

    private TimeSpan CurrentTimespan { get; set; }

    private bool IsPause { get; set; }

    /// <summary>
    /// 获得/设置 Title 字符串
    /// </summary>
    private string ValueTitleString => CurrentTimespan.Hours == 0 ? $"{CurrentTimespan:mm\\:ss}" : $"{CurrentTimespan:hh\\:mm\\:ss}";

    private string? AlertTime { get; set; }

    private CancellationTokenSource CancelTokenSource { get; set; } = new();

    private bool Vibrate { get; set; }

    /// <summary>
    /// 获得/设置 当前值
    /// </summary>
    [Parameter]
    public TimeSpan Value { get; set; }

    /// <summary>
    /// 获得/设置 文件预览框宽度
    /// </summary>
    [Parameter]
    public override int Width { get; set; } = 300;

    /// <summary>
    /// 获得/设置 倒计时结束时回调委托
    /// </summary>
    [Parameter]
    public Func<Task>? OnTimeout { get; set; }

    /// <summary>
    /// 获得/设置 取消时回调委托
    /// </summary>
    [Parameter]
    public Func<Task>? OnCancel { get; set; }

    /// <summary>
    /// 获得/设置 进度条宽度 默认为 2
    /// </summary>
    [Parameter]
    public override int StrokeWidth { get; set; } = 6;

    /// <summary>
    /// 获得/设置 倒计时结束时设备震动
    /// </summary>
    [Parameter]
    public bool IsVibrate { get; set; } = true;

    /// <summary>
    /// 获得/设置 暂停按钮文字
    /// </summary>
    [Parameter]
    [NotNull]
    public string? PauseText { get; set; }

    /// <summary>
    /// 获得/设置 继续按钮文字
    /// </summary>
    [Parameter]
    [NotNull]
    public string? ResumeText { get; set; }

    /// <summary>
    /// 获得/设置 取消按钮文字
    /// </summary>
    [Parameter]
    [NotNull]
    public string? CancelText { get; set; }

    /// <summary>
    /// 获得/设置 取消按钮文字
    /// </summary>
    [Parameter]
    [NotNull]
    public string? StarText { get; set; }

    /// <summary>
    /// 获得/设置 Alert 图标
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
    /// <returns></returns>
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (!firstRender)
        {
            await Timeout();
        }
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <returns></returns>
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
    /// Dispose 方法
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
