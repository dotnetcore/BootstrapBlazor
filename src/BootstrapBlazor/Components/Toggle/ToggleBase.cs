using BootstrapBlazor.Utils;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// Toggle 开关组件
    /// </summary>
    public class ToggleBase : BootstrapComponentBase
    {
        /// <summary>
        /// 获得 样式集合
        /// </summary>
        protected override string? ClassName => CssBuilder.Default("toggle btn")
            .AddClass($"btn-{Color.ToDescriptionString()}", Color != Color.None)
            .AddClass("btn-default off", !Value)
            .AddClass("disabled", IsDisabled)
            .Build();

        /// <summary>
        /// 获得 Style 集合
        /// </summary>
        protected string? StyleName => CssBuilder.Default()
            .AddClass($"width: {Width}px;", Width > 0)
            .Build();

        /// <summary>
        /// 获得/设置 组件高度
        /// </summary>
        [Parameter]
        public int Width { get; set; } = 120;

        /// <summary>
        /// 获得/设置 组件 On 时显示文本
        /// </summary>
        [Parameter]
        public string OnText { get; set; } = "展开";

        /// <summary>
        /// 获得/设置 是否禁用
        /// </summary>
        [Parameter] public bool IsDisabled { get; set; }

        /// <summary>
        /// 获得/设置 组件 Off 时显示文本
        /// </summary>
        [Parameter]
        public string OffText { get; set; } = "收缩";

        /// <summary>
        /// 获得/设置 组件是否处于 On 状态 默认为 Off 状态
        /// </summary>
        [Parameter]
        public bool Value { get; set; } = false;

        /// <summary>
        /// 获得/设置 组件颜色 默认为 Success 颜色
        /// </summary>
        [Parameter]
        public Color Color { get; set; } = Color.Success;

        /// <summary>
        /// 点击控件时触发此方法
        /// </summary>
        protected void OnClick()
        {
            if (!IsDisabled)
            {
                Value = !Value;
                OnValueChanged?.Invoke(Value);
            }
        }

        /// <summary>
        /// 获得/设置 控件值变化时触发此事件
        /// </summary>
        [Parameter]
        public Action<bool>? OnValueChanged { get; set; }
    }
}
