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
        /// 获得 组件 DOM 实例
        /// </summary>
        protected ElementReference Picker { get; set; }

        /// <summary>
        /// 获得/设置 时间格式化字符串 默认值为 "yyyy-MM-dd"
        /// </summary>
        [Parameter] public string Format { get; set; } = "yyyy-MM-dd";

        /// <summary>
        /// 获得/设置 弹窗位置 默认为 Auto
        /// </summary>
        [Parameter] public Placement Placement { get; set; } = Placement.Auto;

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
            return value.ToString(Format);
        }
    }
}
