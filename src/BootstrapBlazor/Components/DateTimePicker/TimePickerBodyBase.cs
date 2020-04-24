using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// TimePicker 组件基类
    /// </summary>
    public abstract class TimePickerBodyBase : BootstrapComponentBase
    {
        /// <summary>
        /// 获得 组件客户端 DOM 实例
        /// </summary>
        protected ElementReference TimePickerElement { get; set; }

        /// <summary>
        /// 获得/设置 当前时间
        /// </summary>
        protected TimeSpan CurrentTime { get; set; }

        /// <summary>
        /// 获得/设置 样式
        /// </summary>
        protected string? ClassName => CssBuilder.Default("time-panel")
            .AddClassFromAttributes(AdditionalAttributes)
            .Build();

        /// <summary>
        /// Gets or sets the value of the input. This should be used with two-way binding.
        /// </summary>
        /// <example>
        /// @bind-Value="model.PropertyName"
        /// </example>
        [Parameter] public TimeSpan Value { get; set; }

        /// <summary>
        /// Gets or sets a callback that updates the bound value.
        /// </summary>
        [Parameter] public EventCallback<TimeSpan> ValueChanged { get; set; }

        /// <summary>
        /// 获得/设置 时间刻度行高
        /// </summary>
        protected Func<double> ItemHeightCallback { get; set; } = () => 36.594d;

        /// <summary>
        /// 获得/设置 时间值改变时回调此方法
        /// </summary>
        [Parameter] public Action<TimeSpan> OnValueChanged { get; set; } = new Action<TimeSpan>(ts => { });

        /// <summary>
        /// 获得/设置 取消按钮回调委托
        /// </summary>
        [Parameter] public Action? OnClose { get; set; }

        /// <summary>
        /// 获得/设置 确认按钮回调委托
        /// </summary>
        [Parameter] public Action? OnConfirm { get; set; }

        /// <summary>
        /// OnInitialized 方法
        /// </summary>
        protected override void OnInitialized()
        {
            base.OnInitialized();

            // 计算开始与结束时间 每个组件显示 6 周数据
            if (Value == TimeSpan.Zero) Value = DateTime.Now.Subtract(DateTime.Today);
            CurrentTime = Value;
        }

        /// <summary>
        /// OnAfterRender 方法
        /// </summary>
        /// <param name="firstRender"></param>
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            //await base.OnAfterRenderAsync(firstRender);

            if (firstRender && JSRuntime != null)
            {
                var height = await JSRuntime.InvokeAsync<double>(TimePickerElement, "timePicker");
                ItemHeightCallback = () => height;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ts"></param>
        protected void OnCellValueChanged(TimeSpan ts)
        {
            CurrentTime = ts;
        }

        /// <summary>
        /// 点击取消按钮回调此方法
        /// </summary>
        protected void OnClickClose()
        {
            CurrentTime = Value;
            OnClose?.Invoke();
        }

        /// <summary>
        /// 点击确认按钮时回调此方法
        /// </summary>
        protected void OnClickConfirm()
        {
            Value = CurrentTime;
            if (ValueChanged.HasDelegate) ValueChanged.InvokeAsync(Value);
            OnValueChanged?.Invoke(Value);
            OnConfirm?.Invoke();
        }
    }
}
