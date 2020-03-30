namespace BootstrapBlazor.Components
{
    /// <summary>
    /// ITooltipHost 接口 
    /// </summary>
    public interface ITooltipHost
    {
        /// <summary>
        /// 获得/设置 ITooltip 实例
        /// </summary>
        ITooltip? Tooltip { get; set; }
    }
}
