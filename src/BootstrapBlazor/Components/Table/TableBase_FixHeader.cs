using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace BootstrapBlazor.Components
{
    partial class TableBase<TItem>
    {
        /// <summary>
        /// 获得 wrapper 样式表集合
        /// </summary>
        protected string? WrapperStyleName => CssBuilder.Default()
            .AddClass($"height: {Height}px; overflow: auto;", Height.HasValue && !IsPagination)
            .AddClass($"max-height: {Height}px; overflow: auto;", Height.HasValue && IsPagination)
            .Build();

        /// <summary>
        /// 获得/设置 TableWrapper 引用
        /// </summary>
        /// <value></value>
        protected ElementReference TableWrapper { get; set; }

        /// <summary>
        /// 获得/设置 IJSRuntime 实例
        /// </summary>
        /// <value></value>
        [Inject] protected IJSRuntime? JSRuntime { get; set; }

        /// <summary>
        /// 获得/设置 Table 高度
        /// </summary>
        [Parameter] public int? Height { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="firstRender"></param>
        protected override void OnAfterRender(bool firstRender)
        {
            if (firstRender && Height.HasValue)
            {
                // 固定表头脚本关联
                JSRuntime.InvokeRun(TableWrapper, "fixTableHeader", "");
            }
        }
    }
}
