using BootstrapBlazor.Utils;
using Microsoft.AspNetCore.Components;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// Switch 开关组件
    /// </summary>
    public abstract class SwitchBase : ToggleBase
    {
        /// <summary>
        /// 获得 样式集合
        /// </summary>
        protected override string? ClassName => CssBuilder.Default("switch")
            .AddClass("is-checked", Value)
            .AddClass("is-disabled", IsDisabled)
            .AddClass(Class)
            .Build();

        /// <summary>
        /// 获得 开关样式集合
        /// </summary>
        protected string? CoreClassName => CssBuilder.Default("switch-core")
            .AddClass($"border-{OnColor.ToDescriptionString()}", OnColor != Color.None && Value)
            .AddClass($"bg-{OnColor.ToDescriptionString()}", OnColor != Color.None && Value)
            .AddClass($"border-{OffColor.ToDescriptionString()}", OffColor != Color.None && !Value)
            .AddClass($"bg-{OffColor.ToDescriptionString()}", OffColor != Color.None && !Value)
            .Build();

        /// <summary>
        /// 获得 显示文字
        /// </summary>
        protected string? Text => Value ? OnText : OffText;

        /// <summary>
        /// 获得 开关 disabled 属性
        /// </summary>
        protected string? Disabled => IsDisabled ? "true" : null;

        /// <summary>
        /// 获得 Style 集合
        /// </summary>
        protected override string? StyleName => CssBuilder.Default()
            .AddClass($"width: {Width}px;", Width > 0)
            .AddClass($"height: {Height}px;", Height >= 20)
            .Build();

        /// <summary>
        /// 获得/设置 开颜色
        /// </summary>
        [Parameter] public Color OnColor { get; set; } = Color.Success;

        /// <summary>
        /// 获得/设置 关颜色
        /// </summary>
        [Parameter] public Color OffColor { get; set; }

        /// <summary>
        ///获得/设置 按钮大小
        /// </summary>
        [Parameter] public Size Size { get; set; }

        /// <summary>
        /// 获得/设置 样式名称
        /// </summary>
        [Parameter] public string? Class { get; set; }

        /// <summary>
        /// 获得/设置 组件宽度 默认 40
        /// </summary>
        [Parameter]
        public override int Width { get; set; } = 40;

        /// <summary>
        /// 获得/设置 控件高度默认 20px
        /// </summary>
        [Parameter] public int Height { get; set; } = 20;

        /// <summary>
        /// 获得/设置 组件 On 时显示文本
        /// </summary>
        [Parameter]
        public override string? OnText { get; set; }

        /// <summary>
        /// 获得/设置 组件 Off 时显示文本
        /// </summary>
        [Parameter]
        public override string? OffText { get; set; }

        /// <summary>
        /// 点击控件时触发此方法
        /// </summary>
        protected override void OnClick()
        {
            if (!IsDisabled)
            {
                Value = !Value;
                if (ValueChanged.HasDelegate) ValueChanged.InvokeAsync(Value);
                OnValueChanged?.Invoke(Value);
            }
        }
    }
}
