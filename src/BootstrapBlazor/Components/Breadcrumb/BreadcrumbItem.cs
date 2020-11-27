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
    /// BreadcrumbItem 实体类
    /// </summary>
    public class BreadcrumbItem
    {
        /// <summary>
        /// 获得/设置 导航地址
        /// </summary>
        public string? Url { get; }

        /// <summary>
        /// 获得/设置 显示文字
        /// </summary>
        public string Text { get; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="text"></param>
        /// <param name="url"></param>
        public BreadcrumbItem(string text, string? url = null)
        {
            Text = text;
            Url = url;
        }
    }
}
