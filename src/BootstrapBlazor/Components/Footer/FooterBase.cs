using Microsoft.AspNetCore.Components;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// Footer 组件
    /// </summary>
    public abstract class FooterBase : BootstrapComponentBase
    {
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
    }
}
