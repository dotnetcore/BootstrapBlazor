using Microsoft.AspNetCore.Components;
using System;
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

        private string PauseText { get; set; } = "暂停";

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
                await JSRuntime.InvokeVoidAsync("", "bb_timer");
            }
        }

        private void OnStart()
        {
            IsPause = false;
            CurrentTimespan = Value;
            AlertTime = DateTime.Now.Add(CurrentTimespan).ToString("HH:mm");

            if (CancelTokenSource != null)
            {
                CancelTokenSource.Dispose();
            }

            CancelTokenSource = new CancellationTokenSource();

            StateHasChanged();

            Task.Run(async () =>
            {
                do
                {
                    await Task.Delay(1000, CancelTokenSource.Token);

                    ResetEvent?.Wait();

                    CurrentTimespan = CurrentTimespan.Subtract(TimeSpan.FromSeconds(1));
                    await InvokeAsync(StateHasChanged);
                }
                while (!CancelTokenSource.IsCancellationRequested && CurrentTimespan > TimeSpan.Zero);

                if (CurrentTimespan == TimeSpan.Zero)
                {
                    await Task.Delay(1000);
                    Value = TimeSpan.Zero;
                    await InvokeAsync(() =>
                    {
                        Vibrate = IsVibrate;
                        StateHasChanged();
                        OnTimeout?.Invoke();
                    });
                }
            });
        }

        private Task OnClickPause()
        {
            IsPause = !IsPause;
            PauseText = IsPause ? "继续" : "暂停";
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
                ResetEvent?.Dispose();
                ResetEvent = null;
            }

            base.Dispose(disposing);
        }
    }
}
