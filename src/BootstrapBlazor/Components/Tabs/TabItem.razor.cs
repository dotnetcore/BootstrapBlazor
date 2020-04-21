namespace BootstrapBlazor.Components
{
    /// <summary>
    /// TabItem 组件
    /// </summary>
    partial class TabItem : TabItemBase
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TabItem() { }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="text"></param>
        /// <param name="active"></param>
        public TabItem(string text, bool active = false) => (Text, IsActive) = (text, active);
    }
}
