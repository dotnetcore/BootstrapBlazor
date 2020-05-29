using Microsoft.AspNetCore.Components;
using System;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// BootstrapInputTextBase 组件
    /// </summary>
    public abstract class BootstrapInputBase<TItem> : ValidateInputBase<TItem>
    {
        /// <summary>
        /// 获得 class 样式集合
        /// </summary>
        protected string? ClassName => CssBuilder.Default("form-control")
            .AddClass(CssClass).AddClass(ValidCss)
            .Build();

        /// <summary>
        /// 获得/设置 是否显示前置标签 默认值为 true
        /// </summary>
        [Parameter]
        public bool ShowLabel { get; set; } = true;

        /// <summary>
        /// 获得/设置 格式化字符串
        /// </summary>
        [Parameter]
        public Func<TItem, string>? Formatter { get; set; }

        /// <summary>
        /// OnInitialized 方法
        /// </summary>
        protected override void OnInitialized()
        {
            base.OnInitialized();

            // TODO: 此处应该检查 html5 type 类型检查
            if (AdditionalAttributes != null)
            {
                if (!AdditionalAttributes.TryGetValue("type", out var _))
                {
                    AdditionalAttributes.Add("type", "text");
                }
            }
        }

        /// <summary>
        /// 数值格式化委托方法
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        protected override string? FormatValueAsString(TItem value)
        {
            return Formatter != null ? Formatter.Invoke(Value) : base.FormatValueAsString(value);
        }
    }
}
