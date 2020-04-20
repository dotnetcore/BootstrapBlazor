using Microsoft.AspNetCore.Components;
using System;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// TimePicker 组件基类
    /// </summary>
    public abstract class TimePickerBase : ValidateInputBase<TimeSpan>
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
        /// 获得/设置 时间值改变时回调此方法
        /// </summary>
        [Parameter] public Action<TimeSpan> OnValueChanged { get; set; } = new Action<TimeSpan>(ts => { });

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
        protected override void OnAfterRender(bool firstRender)
        {
            base.OnAfterRender(firstRender);

            if (firstRender) JSRuntime.Invoke(TimePickerElement, "timepicker");
        }

        /// <summary>
        /// 点击取消按钮回调此方法
        /// </summary>
        protected void OnClose()
        {
            CurrentTime = Value;
        }

        /// <summary>
        /// 点击确认按钮时回调此方法
        /// </summary>
        protected void OnConfirmClick()
        {
            Value = CurrentTime;
            if (ValueChanged.HasDelegate) ValueChanged.InvokeAsync(Value);
        }
    }
}
