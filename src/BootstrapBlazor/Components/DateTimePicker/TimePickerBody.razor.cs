// **********************************
// 框架名称：BootstrapBlazor 
// 框架作者：Argo Zhang
// 开源地址：
// Gitee : https://gitee.com/LongbowEnterprise/BootstrapBlazor
// GitHub: https://github.com/ArgoZhang/BootstrapBlazor 
// 开源协议：LGPL-3.0 (https://gitee.com/LongbowEnterprise/BootstrapBlazor/blob/dev/LICENSE)
// **********************************

using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// TimePicker 组件基类
    /// </summary>
    public sealed partial class TimePickerBody
    {
        /// <summary>
        /// 获得 组件客户端 DOM 实例
        /// </summary>
        private ElementReference TimePickerElement { get; set; }

        /// <summary>
        /// 获得/设置 当前时间
        /// </summary>
        private TimeSpan CurrentTime { get; set; }

        /// <summary>
        /// 获得/设置 样式
        /// </summary>
        private string? ClassName => CssBuilder.Default("time-panel")
            .AddClassFromAttributes(AdditionalAttributes)
            .Build();

        /// <summary>
        /// Gets or sets the value of the input. This should be used with two-way binding.
        /// </summary>
        /// <example>
        /// @bind-Value="model.PropertyName"
        /// </example>
        [Parameter]
        public TimeSpan Value { get; set; }

        /// <summary>
        /// Gets or sets a callback that updates the bound value.
        /// </summary>
        [Parameter]
        public EventCallback<TimeSpan> ValueChanged { get; set; }

        /// <summary>
        /// 获得/设置 取消按钮显示文字
        /// </summary>
        [Parameter]
        [NotNull]
        public string? CancelButtonText { get; set; }

        /// <summary>
        /// 获得/设置 确定按钮显示文字
        /// </summary>
        [Parameter]
        [NotNull]
        public string? ConfirmButtonText { get; set; }

        /// <summary>
        /// 获得/设置 时间刻度行高
        /// </summary>
        private Func<double> ItemHeightCallback { get; set; } = () => 36.594d;

        /// <summary>
        /// 获得/设置 取消按钮回调委托
        /// </summary>
        [Parameter]
        public Action? OnClose { get; set; }

        /// <summary>
        /// 获得/设置 确认按钮回调委托
        /// </summary>
        [Parameter]
        public Action? OnConfirm { get; set; }

        [Inject]
        [NotNull]
        private IStringLocalizer<DateTimePicker<DateTime>>? Localizer { get; set; }

        /// <summary>
        /// OnInitialized 方法
        /// </summary>
        protected override void OnInitialized()
        {
            base.OnInitialized();

            CurrentTime = Value;
            CancelButtonText ??= Localizer[nameof(CancelButtonText)];
            ConfirmButtonText ??= Localizer[nameof(ConfirmButtonText)];
        }

        /// <summary>
        /// OnAfterRender 方法
        /// </summary>
        /// <param name="firstRender"></param>
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            //await base.OnAfterRenderAsync(firstRender);

            if (firstRender)
            {
                var height = await JSRuntime.InvokeAsync<double>(TimePickerElement, "timePicker");
                ItemHeightCallback = () => height;
            }
        }

        /// <summary>
        /// 点击取消按钮回调此方法
        /// </summary>
        private Task OnClickClose()
        {
            CurrentTime = Value;
            OnClose?.Invoke();
            return Task.CompletedTask;
        }

        /// <summary>
        /// 点击确认按钮时回调此方法
        /// </summary>
        private async Task OnClickConfirm()
        {
            Value = CurrentTime;
            if (ValueChanged.HasDelegate)
            {
                await ValueChanged.InvokeAsync(Value);
            }

            OnConfirm?.Invoke();
        }
    }
}
