using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// BootstrapInputTextBase 组件
    /// </summary>
    public abstract class BootstrapInputBase<TValue> : ValidateBase<TValue>
    {
        /// <summary>
        /// 获得 class 样式集合
        /// </summary>
        protected string? ClassName => CssBuilder.Default("form-control")
            .AddClass(CssClass).AddClass(ValidCss)
            .Build();

        /// <summary>
        /// 获得/设置 是否为密码输入框 默认为 false
        /// </summary>
        [Parameter]
        public bool IsPassword { get; set; }

        /// <summary>
        /// 获得/设置 是否为 Input-Group 组合
        /// </summary>
        [Parameter]
        public bool IsGroup { get; set; }

        /// <summary>
        /// 获得/设置 格式化字符串
        /// </summary>
        [Parameter]
        public Func<TValue, string>? Formatter { get; set; }

        /// <summary>
        /// 获得/设置 格式化字符串 如时间类型设置 yyyy-MM-dd
        /// </summary>
        [Parameter]
        public string? FormatString { get; set; }

        /// <summary>
        /// OnInitialized 方法
        /// </summary>
        protected override void OnInitialized()
        {
            base.OnInitialized();

            if (AdditionalAttributes == null) AdditionalAttributes = new Dictionary<string, object>();

            if (!AdditionalAttributes.TryGetValue("type", out var _))
            {
                // 设置 Number 类型
                if (typeof(TValue).IsNumber())
                {
                    AdditionalAttributes.Add("type", "number");
                }
                else
                {
                    AdditionalAttributes.Add("type", IsPassword ? "password" : "text");
                }
            }
        }

        /// <summary>
        /// 数值格式化委托方法
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        protected override string? FormatValueAsString(TValue value)
        {
            return Formatter != null
                ? Formatter.Invoke(Value)
                : (!string.IsNullOrEmpty(FormatString) && value != null
                    ? ((object)value).Format(FormatString)
                    : base.FormatValueAsString(value));
        }
    }
}
