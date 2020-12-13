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
    /// Chart 图表组件 Title 配置实体类
    /// </summary>
    public class ChartTitle
    {
        /// <summary>
        /// 获得/设置 Chart 显示名称
        /// </summary>
        public string Text { get; set; } = "Chart";

        /// <summary>
        /// 获得/设置 是否显示 默认为 true
        /// </summary>
        public bool Display { get; set; } = true;
    }
}
