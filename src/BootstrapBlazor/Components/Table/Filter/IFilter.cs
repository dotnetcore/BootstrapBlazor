namespace BootstrapBlazor.Components
{
    /// <summary>
    /// 过滤器接口
    /// </summary>
    public interface IFilter
    {
        /// <summary>
        /// 显示过滤窗口方法
        /// </summary>
        /// <returns></returns>
        void Show();

        /// <summary>
        /// 获得/设置 本过滤器相关 IFilterAction 实例
        /// </summary>
        IFilterAction? FilterAction { get; set; }
    }
}
