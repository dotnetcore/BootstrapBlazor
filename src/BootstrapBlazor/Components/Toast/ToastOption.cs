namespace BootstrapBlazor.Components
{
    /// <summary>
    /// Toast 弹出窗参数配置类
    /// </summary>
    public class ToastOption
    {
        /// <summary>
        /// 获得/设置 弹出框类型
        /// </summary>
        public ToastCategory Category { get; set; }

        /// <summary>
        /// 获得/设置 显示标题
        /// </summary>
        public string Title { get; set; } = "Toast";

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
        public int Interval { get; set; } = 4000;
    }
}
