namespace BootstrapBlazor.Components
{
    /// <summary>
    /// 富文本编辑器参数
    /// </summary>
    public class EditorEventArgs : System.EventArgs
    {
        /// <summary>
        /// 编辑器内容
        /// </summary>
        public string? Content { get; set; }

        /// <summary>
        /// 插件名称
        /// </summary>
        public string? PluginName { get; set; }

        /// <summary>
        /// 返回值
        /// </summary>
        public string? ResultValue { get; set; }
    }
}
