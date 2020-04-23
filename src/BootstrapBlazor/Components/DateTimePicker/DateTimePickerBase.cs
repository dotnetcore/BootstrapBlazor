using Microsoft.AspNetCore.Components;
using System;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// DateTimePicker 组件基类
    /// </summary>
    public class DateTimePickerBase : ValidateInputBase<DateTime>
    {
        /// <summary>
        /// 获得 组件样式名称
        /// </summary>
        protected string? ClassName => CssBuilder.Default("form-control datetime-picker-input")
            .AddClass(CssClass).AddClass(ValidCss)
            .Build();

        /// <summary>
        /// 获得 组件弹窗位置
        /// </summary>
        protected string? PlacementString => CssBuilder.Default()
            .AddClass("top", Placement == Placement.Top)
            .AddClass("bottom", Placement == Placement.Bottom)
            .AddClass("left", Placement == Placement.Left)
            .AddClass("right", Placement == Placement.Right)
            .Build();

        /// <summary>
        /// 获得 Placeholder 显示字符串
        /// </summary>
        protected string? PlaceholderString => ViewModel switch
        {
            DatePickerViewModel.DateTime => "请选择日期时间",
            _ => "请选择日期"
        };

        /// <summary>
        /// 获得 组件 DOM 实例
        /// </summary>
        protected ElementReference Picker { get; set; }

        /// <summary>
        /// 获得/设置 时间格式化字符串 默认值为 "yyyy-MM-dd"
        /// </summary>
        [Parameter] public string? Format { get; set; }

        /// <summary>
        /// 获得/设置 弹窗位置 默认为 Auto
        /// </summary>
        [Parameter] public Placement Placement { get; set; } = Placement.Auto;

        /// <summary>
        /// 获得/设置 组件显示模式 默认为显示年月日模式
        /// </summary>
        [Parameter] public DatePickerViewModel ViewModel { get; set; }

        /// <summary>
        /// 获得/设置 组件值改变时回调方法委托
        /// </summary>
        [Parameter] public Action<DateTime> OnValueChanged { get; set; } = new Action<DateTime>(_ => { });

        /// <summary>
        /// 获得/设置 是否显示本组件 Footer 区域 默认不显示
        /// </summary>
        [Parameter] public bool ShowFooter { get; set; } = true;

        /// <summary>
        /// OnInitialized
        /// </summary>
        protected override void OnInitialized()
        {
            base.OnInitialized();

            if (Value == DateTime.MinValue) Value = DateTime.Now;
        }

        /// <summary>
        /// OnAfterRender 方法
        /// </summary>
        /// <param name="firstRender"></param>
        protected override void OnAfterRender(bool firstRender)
        {
            base.OnAfterRender(firstRender);

            if (firstRender) JSRuntime.Invoke(Picker, "datetimePicker");
        }

        /// <summary>
        /// 格式化数值方法
        /// </summary>
        protected override string FormatValueAsString(DateTime value)
        {
            var format = Format;
            if (string.IsNullOrEmpty(format)) format = ViewModel == DatePickerViewModel.DateTime ? "yyyy-MM-dd HH:mm:ss" : "yyyy-MM-dd";
            return value.ToString(format);
        }

        /// <summary>
        /// 确认按钮点击时回调此方法
        /// </summary>
        protected void OnClickConfirm()
        {
            JSRuntime.Invoke(Picker, "datetimePicker", "hide");
        }
    }
}
