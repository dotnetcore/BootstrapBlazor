namespace BootstrapBlazor.Components
{
    /// <summary>
    /// 查询条件实体类
    /// </summary>
    public class QueryPageOptions
    {
        /// <summary>
        /// 每页数据数量 默认 20 行
        /// </summary>
        internal const int DefaultPageItems = 20;

        /// <summary>
        /// 获得/设置 查询关键字
        /// </summary>
        public string? SearchText { get; set; }

        /// <summary>
        /// 获得/设置 排序字段名称
        /// </summary>
        public string? SortName { get; set; }

        /// <summary>
        /// 获得/设置 排序方式
        /// </summary>
        public SortOrder SortOrder { get; set; }

        /// <summary>
        /// 获得/设置 当前页码 首页为 第一页
        /// </summary>
        public int PageIndex { get; set; } = 1;

        /// <summary>
        /// 获得/设置 每页条目数量
        /// </summary>
        public int PageItems { get; set; } = DefaultPageItems;
    }
}
