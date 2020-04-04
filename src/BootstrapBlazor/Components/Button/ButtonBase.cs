using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Web;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// Button 按钮组件
    /// </summary>
    public abstract class ButtonBase : TooltipComponentBase
    {
        /// <summary>
        /// 获得 按钮样式集合
        /// </summary>
        /// <returns></returns>
        protected string? ClassName => CssBuilder.Default("btn")
            .AddClass($"btn-outline-{Color.ToDescriptionString()}", IsOutline)
            .AddClass($"btn-{Color.ToDescriptionString()}", Color != Color.None && !IsOutline)
            .AddClass($"btn-{Size.ToDescriptionString()}", Size != Size.None)
            .AddClass("btn-block", IsBlock)
            .AddClass("disabled", ButtonType == ButtonType.Link && IsDisabled)
            .AddClassFromAttributes(AdditionalAttributes)
            .Build();

        /// <summary>
        /// 获得/设置 按钮 disabled 属性
        /// </summary>
        protected string? Tag { get; set; } = "button";

        /// <summary>
        /// 获得 按钮 disabled 属性
        /// </summary>
        protected string? Disabled => IsDisabled ? "true" : null;

        /// <summary>
        /// 获得 按钮 tabindex 属性
        /// </summary>
        protected string? Tab => IsDisabled ? "-1" : null;

        /// <summary>
        /// 获得 EditContext 实例
        /// </summary>
        [CascadingParameter] protected EditContext? EditContext { get; set; }

        /// <summary>
        /// OnClick 事件
        /// </summary>
        [Parameter] public EventCallback<MouseEventArgs> OnClick { get; set; }

        /// <summary>
        /// 获得/设置 按钮颜色
        /// </summary>
        [Parameter] public Color Color { get; set; } = Color.Primary;

        private ButtonType _buttonType = ButtonType.Button;
        /// <summary>
        ///
        /// </summary>
        [Parameter]
        public ButtonType ButtonType
        {
            get => _buttonType;
            set
            {
                _buttonType = value;
                Tag = _buttonType switch
                {
                    ButtonType.Link => "a",
                    ButtonType.Input => "input",
                    ButtonType.Reset => "input",
                    _ => "button"
                };
            }
        }

        /// <summary>
        /// 获得/设置 Outline 样式
        /// </summary>
        [Parameter] public bool IsOutline { get; set; }

        /// <summary>
        /// 获得/设置 Size 大小
        /// </summary>
        [Parameter] public Size Size { get; set; } = Size.None;

        /// <summary>
        /// 获得/设置 Block 模式
        /// </summary>
        [Parameter] public bool IsBlock { get; set; }

        /// <summary>
        /// 获得/设置 是否禁用
        /// </summary>
        [Parameter] public bool IsDisabled { get; set; }

        /// <summary>
        /// 获得/设置 是否触发客户端验证 默认为 true 触发
        /// </summary>
        [Parameter] public bool IsTriggerValidate { get; set; } = true;

        /// <summary>
        /// 获得/设置 RenderFragment 实例
        /// </summary>
        [Parameter] public RenderFragment? ChildContent { get; set; }

        /// <summary>
        /// OnInitialized 方法
        /// </summary>
        protected override void OnInitialized()
        {
            base.OnInitialized();

            var onClick = OnClick;
            OnClick = EventCallback.Factory.Create<MouseEventArgs>(this, e =>
            {
                var valid = IsTriggerValidate && (EditContext?.Validate() ?? true);
                if (valid) onClick.InvokeAsync(e);
            });
        }
    }
}
