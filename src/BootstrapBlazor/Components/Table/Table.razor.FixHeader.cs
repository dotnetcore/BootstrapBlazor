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
    public partial class Table<TItem>
    {
        /// <summary>
        /// 获得 wrapper 样式表集合
        /// </summary>
        protected string? FixedHeaderStyleName => CssBuilder.Default()
            .AddClass($"height: {Height}px;", Height.HasValue && !IsPagination)
            .AddClass($"max-height: {Height}px;", Height.HasValue && IsPagination)
            .Build();

        /// <summary>
        /// 获得/设置 Table 组件引用
        /// </summary>
        /// <value></value>
        protected ElementReference TableElement { get; set; }

        /// <summary>
        /// 获得/设置 Table 高度
        /// </summary>
        [Parameter] public int? Height { get; set; }

        /// <summary>
        /// 获得/设置 多表头模板
        /// </summary>
        [Parameter]
        public RenderFragment? MultiHeaderTemplate { get; set; }
    }
}
