namespace BootstrapBlazor.Components
{
    /// <summary>
    /// ITooltip 接口
    /// </summary>
    public interface ITooltip
    {
        /// <summary>
        /// 获得/设置 位置
        /// </summary>
        Placement Placement { get; set; }

        /// <summary>
        /// 获得/设置 显示文字
        /// </summary>
        string Title { get; set; }

        /// <summary>
        /// 获得/设置 显示内容
        /// </summary>
        string Content { get; set; }

        /// <summary>
        /// 获得/设置 内容是否为 Html
        /// </summary>
        bool IsHtml { get; set; }

        /// <summary>
        /// 获得/设置 弹出方式 默认为 Tooltip
        /// </summary>
        PopoverType PopoverType { get; set; }
    }
}
