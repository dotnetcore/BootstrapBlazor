using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

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
        protected virtual string? ClassName => CssBuilder.Default("toggle btn")
            .AddClass($"btn-{Color.ToDescriptionString()}", Color != Color.None)
            .AddClass("btn-default off", !Value)
            .AddClass("disabled", IsDisabled)
            .AddClassFromAttributes(AdditionalAttributes)
            .Build();

        /// <summary>
        /// 获得 Style 集合
        /// </summary>
        protected virtual string? StyleName => CssBuilder.Default()
            .AddClass($"width: {Width}px;", Width > 0)
            .Build();

        /// <summary>
        /// Gets the <see cref="FieldIdentifier"/> for the bound value.
        /// </summary>
        protected FieldIdentifier? FieldIdentifier { get; set; }

        /// <summary>
        /// 获得/设置 组件宽度
        /// </summary>
        [Parameter]
        public virtual int Width { get; set; } = 120;

        /// <summary>
        /// 获得/设置 是否禁用
        /// </summary>
        [Parameter] public bool IsDisabled { get; set; }

        /// <summary>
        /// 获得/设置 组件 On 时显示文本
        /// </summary>
        [Parameter]
        public virtual string? OnText { get; set; } = "展开";

        /// <summary>
        /// 获得/设置 组件 Off 时显示文本
        /// </summary>
        [Parameter]
        public virtual string? OffText { get; set; } = "收缩";

        /// <summary>
        /// 获得/设置 组件是否处于 On 状态 默认为 Off 状态
        /// </summary>
        [Parameter]
        public bool Value { get; set; }

        /// <summary>
        /// Gets or sets a callback that updates the bound value.
        /// </summary>
        [Parameter]
        public EventCallback<bool> ValueChanged { get; set; }

        /// <summary>
        /// Gets or sets an expression that identifies the bound value.
        /// </summary>
        [Parameter]
        public Expression<Func<bool>>? ValueExpression { get; set; }

        /// <summary>
        /// 获得/设置 组件颜色 默认为 Success 颜色
        /// </summary>
        [Parameter]
        public Color Color { get; set; } = Color.Success;

        /// <summary>
        /// 获得/设置 是否显示前置标签 默认值为 false
        /// </summary>
        [Parameter]
        public bool ShowLabel { get; set; }

        /// <summary>
        /// 获得/设置 显示名称
        /// </summary>
        [Parameter]
        public string? DisplayText { get; set; }

        /// <summary>
        /// 点击控件时触发此方法
        /// </summary>
        protected virtual async Task OnClick()
        {
            if (!IsDisabled)
            {
                Value = !Value;
                if (ValueChanged.HasDelegate) await ValueChanged.InvokeAsync(Value);
            }
        }

        /// <summary>
        /// OnInitialized 方法
        /// </summary>
        protected override void OnInitialized()
        {
            base.OnInitialized();

            if (ValueExpression != null) FieldIdentifier = Microsoft.AspNetCore.Components.Forms.FieldIdentifier.Create(ValueExpression);

            // 内置到验证组件时才使用绑定属性值获取 DisplayName
            if (DisplayText == null && FieldIdentifier != null) DisplayText = FieldIdentifier.Value.GetDisplayName();
        }
    }
}
