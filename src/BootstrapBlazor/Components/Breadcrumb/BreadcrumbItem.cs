namespace BootstrapBlazor.Components
{
    /// <summary>
    /// BreadcrumbItem 实体类
    /// </summary>
    public class BreadcrumbItem
    {
        /// <summary>
        /// 获得/设置 导航地址
        /// </summary>
        public string? Url { get; }

        /// <summary>
        /// 获得/设置 显示文字
        /// </summary>
        public string Text { get; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="text"></param>
        /// <param name="url"></param>
        public BreadcrumbItem(string text, string? url = null)
        {
            Text = text;
            Url = url;
        }
    }
}
