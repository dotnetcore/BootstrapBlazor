using System.Diagnostics.CodeAnalysis;

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
        [NotNull]
        public string? PluginItemName { get; set; }

        /// <summary>
        /// 获取或设置 插件图标
        /// </summary>
        public string? IconClass { get; set; }

        /// <summary>
        /// 获取或设置 插件的提示信息
        /// </summary>
        public string? Tooltip { get; set; }
    }
}
