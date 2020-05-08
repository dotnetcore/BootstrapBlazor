namespace BootstrapBlazor.Components
{
    /// <summary>
    /// 时间线选项
    /// </summary>
    public class TimelineItem
    {
        /// <summary>
        /// 获得 / 设置 时间线内容
        /// </summary>
        public string? Content { get; set; }

        /// <summary>
        /// 获得 / 设置 时间线时间
        /// </summary>
        public string? DateTime { get; set; }

        /// <summary>
        /// 获得 / 设置 时间线颜色
        /// </summary>
        public Color? Color { get; set; }

        /// <summary>
        /// 获得 / 设置 时间线尺寸
        /// </summary>
        public Size? Size { get; set; }

        /// <summary>
        /// 获得 / 设置 时间线图标
        /// </summary>
        public string? Icon { get; set; }
    }
}
