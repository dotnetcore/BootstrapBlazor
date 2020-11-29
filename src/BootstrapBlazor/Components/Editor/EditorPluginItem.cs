namespace BootstrapBlazor.Components
{
    /// <summary>
    /// 富文本框插件信息
    /// </summary>
    public class EditorPluginItem
    {
        /// <summary>
        /// 获取或设置 插件名称
        /// </summary>
        public string? PluginName { get; set; }

        private string? _iconClass;

        /// <summary>
        /// 获取或设置 插件图标
        /// </summary>
        public string? IconClass
        {
            get
            {
                return $"<i class=\"{_iconClass}\">";
            }
            set
            {
                _iconClass = value;
            }
        }

        /// <summary>
        /// 获取或设置 插件的提示信息
        /// </summary>
        public string? Tooltip { get; set; }
    }
}
