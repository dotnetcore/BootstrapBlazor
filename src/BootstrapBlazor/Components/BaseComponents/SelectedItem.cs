// **********************************
// 框架名称：BootstrapBlazor 
// 框架作者：Argo Zhang
// 开源地址：
// Gitee : https://gitee.com/LongbowEnterprise/BootstrapBlazor
// GitHub: https://github.com/ArgoZhang/BootstrapBlazor 
// 开源协议：LGPL-3.0 (https://gitee.com/LongbowEnterprise/BootstrapBlazor/blob/dev/LICENSE)
// **********************************

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// 选项类
    /// </summary>
    public class SelectedItem
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public SelectedItem() { }

        /// <summary>
        /// 构造函数
        /// </summary>
        public SelectedItem(string value, string text) => (Value, Text) = (value, text);

        /// <summary>
        /// 获得/设置 显示名称
        /// </summary>
        public string Text { get; set; } = "";

        /// <summary>
        /// 获得/设置 选项值
        /// </summary>
        public string Value { get; set; } = "";

        /// <summary>
        /// 获得/设置 是否选中
        /// </summary>
        public bool Active { get; set; }

        /// <summary>
        /// 获得/设置 分组名称
        /// </summary>
        public string GroupName { get; set; } = "";
    }
}
