namespace BootstrapBlazor.Components
{
    /// <summary>
    /// Message 组件配置类
    /// </summary>
    public class MessageOption
    {
        /// <summary>
        /// 获得/设置 Toast Body 子组件
        /// </summary>
        public string? Content { get; set; }

        /// <summary>
        /// 获得/设置 是否自动隐藏
        /// </summary>
        public bool IsAutoHide { get; set; } = true;

        /// <summary>
        /// 获得/设置 自动隐藏时间间隔
        /// </summary>
        public int Delay { get; set; } = 4000;
        /// <summary>
        /// 获得/设置 颜色
        /// </summary>
        public Color Color { get; set; } = Color.Primary;

        /// <summary>
        /// 获得/设置 是否显示关闭按钮
        /// </summary>
        public bool ShowDismiss { get; set; }

        /// <summary>
        /// 获得/设置 显示图标
        /// </summary>
        public string? Icon { get; set; }

        /// <summary>
        /// 获得/设置 是否显示左侧 Bar
        /// </summary>
        public bool ShowBar { get; set; }
    }
}
