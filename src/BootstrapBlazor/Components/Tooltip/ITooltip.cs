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
    }
}
