using Microsoft.AspNetCore.Components;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// Footer 组件
    /// </summary>
    public abstract class FooterBase : BootstrapComponentBase
    {
        /// <summary>
        /// 获得/设置 Footer DOM 实例
        /// </summary>
        protected ElementReference FooterElement { get; set; }

        /// <summary>
        /// 获得/设置 Footer 显示文字
        /// </summary>
        [Parameter]
        public string? Text { get; set; }

        /// <summary>
        /// 获得/设置 Footer 组件中返回顶端按钮控制的滚动条所在组件
        /// </summary>
        [Parameter]
        public string? Target { get; set; }

        /// <summary>
        /// OnAfterRender 方法
        /// </summary>
        /// <param name="firstRender"></param>
        protected override void OnAfterRender(bool firstRender)
        {
            base.OnAfterRender(firstRender);

            if (firstRender) JSRuntime.Invoke(FooterElement, "footer", Target);
        }
    }
}
