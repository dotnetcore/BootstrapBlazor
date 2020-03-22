using BootstrapBlazor.Utils;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System.Collections.Generic;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// Button 按钮组件
    /// </summary>
    public abstract class ButtonBase : ComponentBase
    {
        /// <summary>
        /// 获得/设置 用户自定义属性
        /// </summary>
        /// <returns></returns>
        [Parameter(CaptureUnmatchedValues = true)]
        public IDictionary<string, object>? AdditionalAttributes { get; set; }

        /// <summary>
        /// 获得 按钮样式集合
        /// </summary>
        /// <returns></returns>
        protected string ClassName => new CssBuilder("btn")
          .AddClass($"btn-outline-{Color.ToDescriptionString()}", IsOutline)
          .AddClass($"btn-{Color.ToDescriptionString()}", Color != Color.None && !IsOutline)
          .AddClass($"btn-{Size.ToDescriptionString()}", Size != Size.None)
          .AddClass("btn-block", IsBlock)
          .AddClass("disabled", ButtonType == ButtonType.Link && IsDisabled)
          .AddClass(Class)
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
        /// 获得 按钮类型
        /// </summary>
        protected string type => ButtonType switch
        {
            ButtonType.Submit => "submit",
            ButtonType.Reset => "reset",
            _ => "button"
        };

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
        ///
        /// </summary>
        [Parameter] public bool IsOutline { get; set; }

        /// <summary>
        ///
        /// </summary>
        [Parameter] public Size Size { get; set; } = Size.None;

        /// <summary>
        ///
        /// </summary>
        [Parameter] public bool IsBlock { get; set; }

        /// <summary>
        ///
        /// </summary>
        [Parameter] public string? Value { get; set; }

        /// <summary>
        ///
        /// </summary>
        [Parameter] public bool IsDisabled { get; set; }

        /// <summary>
        ///
        /// </summary>
        [Parameter] public string Class { get; set; } = "";

        /// <summary>
        ///
        /// </summary>
        [Parameter] public RenderFragment? ChildContent { get; set; }
    }
}
