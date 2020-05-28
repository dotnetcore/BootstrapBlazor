namespace BootstrapBlazor.Components
{
    /// <summary>
    /// Toast 弹出窗参数配置类
    /// </summary>
    public class ToastOption : PopupOptionBase
    {
        /// <summary>
        /// 获得/设置 弹出框类型
        /// </summary>
        public ToastCategory Category { get; set; }

        /// <summary>
        /// 获得/设置 显示标题
        /// </summary>
        public string Title { get; set; } = "Toast";
    }
}
