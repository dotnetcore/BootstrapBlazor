// **********************************
// 框架名称：BootstrapBlazor 
// 框架作者：Argo Zhang
// 开源地址：
// Gitee : https://gitee.com/LongbowEnterprise/BootstrapBlazor
// GitHub: https://github.com/ArgoZhang/BootstrapBlazor 
// 开源协议：LGPL-3.0 (https://gitee.com/LongbowEnterprise/BootstrapBlazor/blob/dev/LICENSE)
// **********************************

using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Linq;

namespace BootstrapBlazor.Components
{

    /// <summary>
    /// Breadcrumb 组件
    /// </summary>
    public sealed partial class Breadcrumb
    {
        /// <summary>
        /// 获得/设置 数据集
        /// </summary>
        [Parameter]
        public IEnumerable<BreadcrumbItem> Value { get; set; } = Enumerable.Empty<BreadcrumbItem>();

        /// <summary>
        /// 获得/设置 面包屑渲染模式是否进行导航 默认 false 不进行导航
        /// </summary>
        [Parameter]
        public bool ActiveLink { get; set; }

        private string? GetItemClassName(BreadcrumbItem item) => CssBuilder.Default("breadcrumb-item")
            .Build();

        private string? CurrentPage(BreadcrumbItem item) => CssBuilder.Default()
            .Build();
    }
}
