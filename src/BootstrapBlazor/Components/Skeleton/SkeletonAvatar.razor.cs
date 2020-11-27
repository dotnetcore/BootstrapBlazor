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
    /// 
    /// </summary>
    public sealed partial class SkeletonAvatar
    {
        private string? AvatarClassString => CssBuilder.Default("skeleton-avatar")
            .AddClass("circle", Circle)
            .Build();

        /// <summary>
        /// 获得/设置 是否为圆形 默认为 false
        /// </summary>
        [Parameter]
        public bool Circle { get; set; }
    }
}
