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
    /// 控制台消息实体类
    /// </summary>
    public class ConsoleMessageItem
    {
        /// <summary>
        /// 获得/设置 控制台输出消息
        /// </summary>
        public string Message { get; set; } = "";

        /// <summary>
        /// 获得/设置 控制台消息颜色 默认为 White 白色
        /// </summary>
        public Color Color { get; set; } = Color.None;
    }
}
