using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// DateTimePicker 组件基类
    /// </summary>
    public abstract class DateTimePickerBase<TValue> : ValidateBase<TValue>
    {
        /// <summary>
        /// 获得 组件样式名称
        /// </summary>
        protected string? ClassName => CssBuilder.Default("form-control datetime-picker-input")
            .AddClass(ValidCss)
            .Build();

        /// <summary>
        /// 获得 组件小图标样式
        /// </summary>
        protected string? DateTimePickerIconClassString => CssBuilder.Default("datetime-picker-input-icon")
            .AddClass("disabled", IsDisabled)
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
        /// 获得/设置 是否允许为空 默认 false 不允许为空
        /// </summary>
        protected bool AllowNull { get; set; }

        /// <summary>
        /// 获得/设置 组件时间
        /// </summary>
        protected DateTime ComponentValue
        {
            get
            {
                DateTime v = DateTime.Now;
                if (AllowNull)
                {
                    var t = Value as DateTime?;
                    if (t.HasValue) v = t.Value;
                }
                else
                {
#nullable disable
                    var t = (DateTime)(object)Value;
#nullable restore
                    v = t;
                }
                return ViewModel == DatePickerViewModel.Date ? v.Date : v;
            }
            set
            {
                CurrentValue = (TValue)(object)value;
            }
        }

        /// <summary>
        /// 获得 组件 DOM 实例
        /// </summary>
        protected ElementReference Picker { get; set; }

        /// <summary>
        /// 获得/设置 时间格式化字符串 默认值为 "yyyy-MM-dd"
        /// </summary>
        [Parameter]
        public string? Format { get; set; }

        /// <summary>
        /// 获得/设置 弹窗位置 默认为 Auto
        /// </summary>
        [Parameter]
        public Placement Placement { get; set; } = Placement.Auto;

        /// <summary>
        /// 获得/设置 组件显示模式 默认为显示年月日模式
        /// </summary>
        [Parameter]
        public DatePickerViewModel ViewModel { get; set; }

        /// <summary>
        /// OnInitialized
        /// </summary>
        protected override void OnInitialized()
        {
            base.OnInitialized();

            // 判断泛型类型
            var isDateTime = typeof(TValue) == typeof(DateTime) || typeof(TValue) == typeof(DateTime?);
            if (!isDateTime) throw new InvalidOperationException($"DateTimePicker 组件仅支持绑定泛型为 DateTime 或者 DateTime?");

            // 泛型设置为可为空
            AllowNull = typeof(TValue) == typeof(DateTime?);

            // 不允许为空时设置 Value 默认值
            if (!AllowNull && Value == null)
            {
                Value = (TValue)(object)DateTime.Now;
            }

            // Value 为 MinValue 时 设置 Value 默认值
            if (Value?.ToString() == DateTime.MinValue.ToString())
            {
                Value = (TValue)(object)DateTime.Now;
            }
        }

        /// <summary>
        /// OnAfterRender 方法
        /// </summary>
        /// <param name="firstRender"></param>
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);

            if (firstRender && JSRuntime != null)
            {
                await JSRuntime.Invoke(Picker, "datetimePicker");
            }
        }

        /// <summary>
        /// 格式化数值方法
        /// </summary>
        protected override string FormatValueAsString(TValue value)
        {
            var ret = "";
            if (Value != null)
            {
                var format = Format;
                if (string.IsNullOrEmpty(format))
                {
                    format = ViewModel == DatePickerViewModel.DateTime ? "yyyy-MM-dd HH:mm:ss" : "yyyy-MM-dd";
                }

                ret = ComponentValue.ToString(format);
            }
            return ret;
        }

        /// <summary>
        /// 清空按钮点击时回调此方法
        /// </summary>
        /// <returns></returns>
        protected async Task OnClear()
        {
#nullable disable
            CurrentValue = default;
#nullable restore
            StateHasChanged();
            if (JSRuntime != null) await JSRuntime.Invoke(Picker, "datetimePicker", "hide");
        }

        /// <summary>
        /// 确认按钮点击时回调此方法
        /// </summary>
        protected async Task OnConfirm()
        {
            if (JSRuntime != null) await JSRuntime.Invoke(Picker, "datetimePicker", "hide");
        }
    }
}
