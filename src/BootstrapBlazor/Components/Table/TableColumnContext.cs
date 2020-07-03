namespace BootstrapBlazor.Components
{
    /// <summary>
    /// TableColumn 上下文类
    /// </summary>
    public class TableColumnContext<TModel, TValue>
    {
#nullable disable
        /// <summary>
        /// 获得/设置 行数据实例
        /// </summary>
        public TModel Row { get; set; }

        /// <summary>
        /// 获得/设置 当前绑定字段数据实例
        /// </summary>
        public TValue Value { get; set; }
#nullable restore
    }
}
