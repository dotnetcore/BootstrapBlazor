using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// InputNumber 组件
    /// </summary>
    public abstract class BootstrapInputNumberBase<TValue> : BootstrapInput<TValue>
    {
        /// <summary>
        /// 获得 按钮样式
        /// </summary>
        protected string? ButtonClassString => CssBuilder.Default("btn")
           .AddClass("btn-outline-secondary", Color == Color.None)
           .AddClass($"btn-outline-{Color.ToDescriptionString()}", Color != Color.None)
           .Build();

        /// <summary>
        /// 获得 文本框样式
        /// </summary>
        protected string? InputClassString => CssBuilder.Default("form-control")
           .AddClass("input-number-fix", ShowButton)
           .AddClass($"border-{Color.ToDescriptionString()} shadow-{Color.ToDescriptionString()}", Color != Color.None)
           .Build();

#nullable disable
        /// <summary>
        /// 获得/设置 数值步进步长
        /// </summary>
        [Parameter]
        public TValue Step { get; set; }

        /// <summary>
        /// 获得/设置 数值增加时回调委托
        /// </summary>
        [Parameter]
        public Func<TValue, Task> OnIncrement { get; set; }

        /// <summary>
        /// 获得/设置 数值减少时回调委托
        /// </summary>
        [Parameter]
        public Func<TValue, Task> OnDecrement { get; set; }
#nullable restore

        /// <summary>
        /// 获得/设置 最小值
        /// </summary>
        [Parameter]
        public string? Min { get; set; }

        /// <summary>
        /// 获得/设置 最大值
        /// </summary>
        [Parameter]
        public string? Max { get; set; }

        /// <summary>
        /// 获得/设置 是否显示加减按钮
        /// </summary>
        [Parameter]
        public bool ShowButton { get; set; }

        /// <summary>
        /// 获得/设置 按钮颜色
        /// </summary>
        [Parameter]
        public Color Color { get; set; }

        /// <summary>
        /// OnInitialized 方法
        /// </summary>
        protected override void OnInitialized()
        {
            if (!typeof(TValue).IsNumber()) throw new InvalidOperationException($"The type '{typeof(TValue)}' is not a supported numeric type.");

            base.OnInitialized();

            // 本组件接受的类型均不可为空
            if (Step.ToString() == "0") Step = (TValue)(object)1;

            if (AdditionalAttributes == null) AdditionalAttributes = new Dictionary<string, object>(100);

            if (!string.IsNullOrEmpty(Max))
            {
                AdditionalAttributes["max"] = Max;
            }
            if (!string.IsNullOrEmpty(Min))
            {
                AdditionalAttributes["min"] = Min;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="result"></param>
        /// <param name="validationErrorMessage"></param>
        /// <returns></returns>
        protected override bool TryParseValueFromString(string value, out TValue result, out string? validationErrorMessage)
        {
            if (value.TryParse<TValue>(out var v))
            {
                result = v.Range(Min, Max);
                validationErrorMessage = null;
                return true;
            }
            return base.TryParseValueFromString(value, out result, out validationErrorMessage);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected async Task OnClickDec()
        {
            CurrentValue = CurrentValue.Subtract(Step).Range(Min, Max);
            if (OnDecrement != null) await OnDecrement(CurrentValue);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected async Task OnClickInc()
        {
            CurrentValue = CurrentValue.Add(Step).Range(Min, Max);
            if (OnIncrement != null) await OnIncrement(CurrentValue);
        }
    }
}
