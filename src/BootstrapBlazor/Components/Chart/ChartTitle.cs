namespace BootstrapBlazor.Components
{
    /// <summary>
    /// Chart 图表组件 Title 配置实体类
    /// </summary>
    public class ChartTitle
    {
        /// <summary>
        /// 获得/设置 Chart 显示名称
        /// </summary>
        public string Text { get; set; } = "未设置";

        /// <summary>
        /// 获得/设置 是否显示 默认为 true
        /// </summary>
        public bool Display { get; set; } = true;
    }
}
