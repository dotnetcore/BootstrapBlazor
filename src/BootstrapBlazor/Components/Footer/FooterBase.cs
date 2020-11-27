// **********************************
// 框架名称：BootstrapBlazor 
// 框架作者：Argo Zhang
// 开源地址：
// Gitee : https://gitee.com/LongbowEnterprise/BootstrapBlazor
// GitHub: https://github.com/ArgoZhang/BootstrapBlazor 
// 开源协议：LGPL-3.0 (https://gitee.com/LongbowEnterprise/BootstrapBlazor/blob/dev/LICENSE)
// **********************************

using Microsoft.AspNetCore.Components;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// Footer 组件
    /// </summary>
    public abstract class FooterBase : BootstrapComponentBase
    {
        /// <summary>
        /// 获得 按钮样式集合
        /// </summary>
        /// <returns></returns>
        protected string? ClassName => CssBuilder.Default("footer")
            .AddClassFromAttributes(AdditionalAttributes)
            .Build();

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
