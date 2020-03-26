using BootstrapBlazor.Utils;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System.Threading.Tasks;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// Alert 警告框组件
    /// </summary>
    public abstract class AlertBase : BootstrapComponentBase
    {
        /// <summary>
        /// 获得 样式集合
        /// </summary>
        /// <returns></returns>
        protected override string? ClassName => CssBuilder.Default("alert")
            .AddClass($"alert-{Color.ToDescriptionString()}", Color != Color.None)
            .AddClass(Class)
            .AddClassFromAttributes(AdditionalAttributes)
            .Build();

        /// <summary>
        /// 获得/设置 自定义样式
        /// </summary>
        protected string? Class { get; set; }

        /// <summary>
        /// 获得/设置 颜色
        /// </summary>
        [Parameter] public Color Color { get; set; } = Color.Primary;

        /// <summary>
        /// 是否显示关闭按钮
        /// </summary>
        [Parameter] public bool ShowDismiss { get; set; }

        /// <summary>
        /// 子组件
        /// </summary>
        [Parameter] public RenderFragment? ChildContent { get; set; }

        /// <summary>
        /// 关闭警告框回调方法
        /// </summary>
        [Parameter]
        public EventCallback<MouseEventArgs> OnDismiss { get; set; }

        /// <summary>
        /// OnInitializedAsync 方法
        /// </summary>
        /// <returns></returns>
        protected override Task OnInitializedAsync()
        {
            if (ShowDismiss)
            {
                var callback = OnDismiss;
                OnDismiss = EventCallback.Factory.Create<MouseEventArgs>(this, e =>
                {
                    Class = "collapse";
                    if (callback.HasDelegate) callback.InvokeAsync(e);
                });
            }
            return Task.CompletedTask;
        }
    }
}
