// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// 
    /// </summary>
    public sealed partial class Timer
    {
        /// <summary>
        /// 获得 组件样式字符串
        /// </summary>
        protected override string? ClassString => CssBuilder.Default("circle timer")
            .AddClassFromAttributes(AdditionalAttributes)
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

        private string AlertTime { get; set; } = "";

        private CancellationTokenSource? CancelTokenSource { get; set; }

        private ManualResetEventSlim? ResetEvent { get; set; } = new ManualResetEventSlim(true);

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

        [Inject]
        [NotNull]
        private IStringLocalizer<Timer>? Localizer { get; set; }

        /// <summary>
        /// OnInitialized 方法
        /// </summary>
        protected override void OnInitialized()
        {
            base.OnInitialized();

            PauseText ??= Localizer[nameof(PauseText)];
            ResumeText ??= Localizer[nameof(ResumeText)];
            CancelText ??= Localizer[nameof(CancelText)];
            StarText ??= Localizer[nameof(StarText)];
        }

        /// <summary>
        /// OnAfterRenderAsync 方法
        /// </summary>
        /// <param name="firstRender"></param>
        /// <returns></returns>
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);

            if (Vibrate)
            {
                Vibrate = false;
                await JSRuntime.InvokeVoidAsync("", "bb_vibrate");
            }
        }

        private void OnStart()
        {
            IsPause = false;
            CurrentTimespan = Value;
            AlertTime = DateTime.Now.Add(CurrentTimespan).ToString("HH:mm");

            StateHasChanged();

            Task.Run(async () =>
            {
                if (CancelTokenSource != null)
                {
                    CancelTokenSource.Dispose();
                }

                CancelTokenSource = new CancellationTokenSource();

                try
                {
                    do
                    {
                        await Task.Delay(1000, CancelTokenSource?.Token ?? new CancellationToken(true));

                        if (!(CancelTokenSource?.IsCancellationRequested ?? true))
                        {
                            ResetEvent?.Wait();
                            CurrentTimespan = CurrentTimespan.Subtract(TimeSpan.FromSeconds(1));
                            await InvokeAsync(StateHasChanged);
                        }
                    }
                    while (!(CancelTokenSource?.IsCancellationRequested ?? true) && CurrentTimespan > TimeSpan.Zero);

                    if (CurrentTimespan == TimeSpan.Zero)
                    {
                        await Task.Delay(500, CancelTokenSource?.Token ?? new CancellationToken(true));
                        if (!(CancelTokenSource?.IsCancellationRequested ?? true))
                        {
                            Value = TimeSpan.Zero;
                            await InvokeAsync(() =>
                            {
                                Vibrate = IsVibrate;
                                StateHasChanged();
                                OnTimeout?.Invoke();
                            });
                        }
                    }
                }
                catch (TaskCanceledException) { }
            });
        }

        private Task OnClickPause()
        {
            IsPause = !IsPause;
            if (IsPause)
            {
                ResetEvent?.Reset();
            }
            else
            {
                AlertTime = DateTime.Now.Add(CurrentTimespan).ToString("HH:mm");
                if (!ResetEvent?.IsSet ?? false) ResetEvent?.Set();
            }
            return Task.CompletedTask;
        }

        private string GetPauseText() => IsPause ? ResumeText : PauseText;

        private async Task OnClickCancel()
        {
            Value = TimeSpan.Zero;
            CancelTokenSource?.Cancel();
            if (OnCancel != null) await OnCancel.Invoke();
        }

        /// <summary>
        /// Dispose 方法
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                CancelTokenSource?.Dispose();
                CancelTokenSource = null;

                ResetEvent?.Dispose();
                ResetEvent = null;
            }

            base.Dispose(disposing);
        }
    }
}
