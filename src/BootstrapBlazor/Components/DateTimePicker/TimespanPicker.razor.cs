// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

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
    public sealed partial class TimespanPicker<TValue>
    {
        /// <summary>
        /// 获得 组件样式名称
        /// </summary>
        private string? ClassString => CssBuilder.Default("timespan-picker")
            .AddClass(ValidCss)
            .AddClassFromAttributes(AdditionalAttributes)
            .Build();

        /// <summary>
        /// 获得/设置 组件样式
        /// </summary>
        private string? PickerBodyClassName => CssBuilder.Default("picker-panel timespan-picker")
            .AddClass("d-none", !IsShown)
            .Build();

        /// <summary>
        /// 获得 组件文本框样式名称
        /// </summary>
        private string? InputClassName => CssBuilder.Default("form-control timespan-picker-input")
            .AddClass(ValidCss)
            .Build();

        /// <summary>
        /// 获得 组件小图标样式
        /// </summary>
        private string? DateTimePickerIconClassString => CssBuilder.Default("timespan-picker-input-icon")
            .AddClass("disabled", IsDisabled)
            .Build();

        /// <summary>
        /// 获得 组件弹窗位置
        /// </summary>
        private string? PlacementString => CssBuilder.Default()
            .AddClass("top", Placement == Placement.Top)
            .AddClass("bottom", Placement == Placement.Bottom)
            .AddClass("left", Placement == Placement.Left)
            .AddClass("right", Placement == Placement.Right)
            .Build();

        /// <summary>
        /// 获得 Placeholder 显示字符串
        /// </summary>
        private string? PlaceholderString => TimePlaceHolder;

        /// <summary>
        /// 获得/设置 是否允许为空 默认 false 不允许为空
        /// </summary>
        private bool AllowNull { get; set; }

        /// <summary>
        /// 获得/设置 组件时间
        /// </summary>
        private TimeSpan ComponentValue
        {
            get
            {
                var v = TimeSpan.Zero;
                if (AllowNull)
                {
                    var t = Value as TimeSpan?;
                    if (t.HasValue) v = t.Value;
                }
                else
                {
                    var t = Value as TimeSpan?;
                    if (t.HasValue) v = t.Value;
                }

                return v;
            }
            set
            {
                CurrentValue = (TValue)(object)value;
            }
        }

        /// <summary>
        /// 获得 组件 DOM 实例
        /// </summary>
        private ElementReference Picker { get; set; }


        /// <summary>
        /// 获得/设置 是否显示本组件默认为 false 不显示
        /// </summary>
        [Parameter]
        public bool IsShown { get; set; }

        /// <summary>
        /// 获得/设置 时间格式化字符串 默认值为 "HH:mm:ss"
        /// </summary>
        [Parameter]
        public string? Format { get; set; }

        /// <summary>
        /// 获得/设置 弹窗位置 默认为 Auto
        /// </summary>
        [Parameter]
        public Placement Placement { get; set; } = Placement.Auto;


        /// <summary>
        /// 获得/设置 当前日期最大值
        /// </summary>
        [Parameter]
        public TimeSpan? MaxValue { get; set; }

        /// <summary>
        /// 获得/设置 当前日期最小值
        /// </summary>
        [Parameter]
        public TimeSpan? MinValue { get; set; }

        /// <summary>
        /// 获得/设置 当前日期变化时回调委托方法
        /// </summary>
        [Parameter]
        public Func<TValue, Task>? OnDateTimeChanged { get; set; }


        /// <summary>
        /// 这里用的是 DateTimePicker 的语言资源
        /// </summary>
        [Inject]
        [NotNull]
        private IStringLocalizer<DateTimePicker<DateTime>>? Localizer { get; set; }


        [NotNull]
        private string? TimePlaceHolder { get; set; }

        [NotNull]
        private string? GenericTypeErroMessage { get; set; }

        [NotNull]
        private string? TimespanFormat { get; set; }



        /// <summary>
        /// OnInitialized
        /// </summary>
        protected override void OnInitialized()
        {
            base.OnInitialized();

            TimePlaceHolder ??= Localizer[nameof(TimePlaceHolder)];
            GenericTypeErroMessage ??= Localizer[nameof(GenericTypeErroMessage)];
            TimespanFormat ??= "c";// "hh\':\'mm\':\'ss"; //Localizer[nameof(TimespanFormat)];

            // 判断泛型类型
            var isTimeSpan = typeof(TValue) == typeof(TimeSpan)
                            || typeof(TValue) == typeof(TimeSpan?)
                            ;

            if (!isTimeSpan) throw new InvalidOperationException(GenericTypeErroMessage);

            // 泛型设置为可为空
            AllowNull = typeof(TValue) == typeof(TimeSpan?);


            // 不允许为空时设置 Value 默认值
            if (!AllowNull && Value == null)
            {
                CurrentValue = (TValue)(object)TimeSpan.Zero;
            }

            // Value 为 MinValue 时 设置 Value 默认值
            if (Value?.ToString() == TimeSpan.Zero.ToString())
            {
                CurrentValue = (TValue)(object)TimeSpan.Zero;
            }
        }

        /// <summary>
        /// OnAfterRender 方法
        /// </summary>
        /// <param name="firstRender"></param>
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);

            if (firstRender)
            {
                await JSRuntime.InvokeVoidAsync(Picker, "bb_timespanPicker");
            }
        }

        /// <summary>
        /// 格式化数值方法
        /// </summary>
        protected override string FormatValueAsString(TValue value)
        {
            var ret = "";
            if (value != null)
            {
                var format = Format;
                if (string.IsNullOrEmpty(format))
                {
                    format = TimespanFormat;
                }

                ret = ComponentValue.ToString(format);
            }
            return ret;
        }


        /// <summary>
        /// 确认按钮点击时回调此方法
        /// </summary>
        private async Task OnConfirm()
        {
            await JSRuntime.InvokeVoidAsync(Picker, "bb_timespanPicker", "hide");
            if (OnDateTimeChanged != null)
            {
                await OnDateTimeChanged(Value);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private async Task OnClose()
        {
            await JSRuntime.InvokeVoidAsync(Picker, "bb_timespanPicker", "hide");
        }
    }
}
