using BootstrapBlazor.Utils;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Threading.Tasks;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// Switch 开关组件
    /// </summary>
    public abstract class SwitchBase : BootstrapComponentBase
    {
        /// <summary>
        /// 获得 样式集合
        /// </summary>
        /// <returns></returns>
        protected override string? ClassName => CssBuilder.Default("switch")
            .AddClass($"on-{OnColor.ToDescriptionString()}", OnColor != Color.None)
            .AddClass($"off-{OffColor.ToDescriptionString()}", OffColor != Color.None)
            .AddClass($"btn-{Size.ToDescriptionString()}", Size != Size.None) //设置大小生效，但是字体有问题       
            .AddClass("custom-control custom-switch")
            .AddClass(Class)
        .Build();

        /// <summary>
        /// 获得/设置 开颜色
        /// </summary>
        [Parameter] public Color OnColor { get; set; } = Color.Info;

        /// <summary>
        /// 获得/设置 关颜色
        /// </summary>
        [Parameter] public Color OffColor { get; set; }

        /// <summary>
        ///获得/设置 按钮大小
        /// </summary>
        [Parameter] public Size Size { get; set; }

        /// <summary>
        /// 获得/设置 是否禁用
        /// </summary>
        [Parameter]
        public bool IsDisabled { get; set; }

        /// <summary>
        /// 获得/设置 样式名称
        /// </summary>
        [Parameter] public string? Class { get; set; }

        /// <summary>
        /// 获得/设置 组件 On 时显示文本
        /// </summary>
        [Parameter]
        public string OnText { get; set; } = "展开";

        /// <summary>
        /// 获得/设置 组件 Off 时显示文本
        /// </summary>
        [Parameter]
        public string OffText { get; set; } = "收缩";

        /// <summary>
        /// 获得/设置 组件是否处于 On 状态 默认为 Off 状态
        /// </summary>
        [Parameter]
        public bool Value { get; set; } = true;

        /// <summary>
        /// 获得 开关 disabled 属性
        /// </summary>
        protected string? Disabled => IsDisabled ? "true" : null;

        /// <summary>
        /// 获得/设置 Value 值改变时回调事件
        /// </summary>
        [Parameter]
        public EventCallback<bool> ValueChanged { get; set; }

        /// <summary>
        /// 转换开关事件
        /// </summary>
        protected EventCallback<MouseEventArgs> SwitchClick { get; set; }

        /// <summary>
        /// 获得 当前组件 Id
        /// </summary>
        [Parameter] public override string? Id { get; set; } = Guid.NewGuid().ToString();

        /// <summary>
        /// OnInitializedAsync 方法
        /// </summary>
        protected override Task OnInitializedAsync()
        {
            if (!SwitchClick.HasDelegate)
            {
                SwitchClick = EventCallback.Factory.Create<MouseEventArgs>(this, () =>
                {
                    Value = !Value;
                    ValueChanged.InvokeAsync(Value);
                });
            }
            return Task.CompletedTask;
        }

        /// <summary>
        /// 子组件
        /// </summary>
        [Parameter] public RenderFragment? ChildContent { get; set; }
    }
}
