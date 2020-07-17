namespace BootstrapBlazor.Components
{
    /// <summary>
    /// TableToolbar 组件接口
    /// </summary>
    public interface ITableToolbar
    {
        /// <summary>
        /// 添加 TableToolbarButton 组件到 Toolbar 方法
        /// </summary>
        /// <param name="button"></param>
        void AddButtons(ButtonBase button);
    }
}
