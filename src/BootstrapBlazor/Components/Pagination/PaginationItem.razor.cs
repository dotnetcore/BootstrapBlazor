using Microsoft.AspNetCore.Components;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// PaginationItem 组件
    /// </summary>
    public partial class PaginationItem
    {
        private string? ClassName => CssBuilder.Default("page-item")
            .AddClass("active", Active)
            .Build();

        private string? LabelString => $"第 {PageIndex} 页";

        /// <summary>
        /// 获得/设置 是否 Active 状态
        /// </summary>
        [Parameter] public bool Active { get; set; }

        /// <summary>
        /// 获得/设置 页码
        /// </summary>
        [Parameter] public int PageIndex { get; set; } = 1;

        /// <summary>
        /// 获得/设置 点击页码时回调方法
        /// </summary>
        [Parameter] public EventCallback<int> OnClick { get; set; }
    }
}