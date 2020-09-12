using Microsoft.AspNetCore.Components;

namespace BootstrapBlazor.Components
{
    partial class Table<TItem>
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
    }
}
