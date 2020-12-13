// **********************************
// 框架名称：BootstrapBlazor 
// 框架作者：Argo Zhang
// 开源地址：
// Gitee : https://gitee.com/LongbowEnterprise/BootstrapBlazor
// GitHub: https://github.com/ArgoZhang/BootstrapBlazor 
// 开源协议：LGPL-3.0 (https://gitee.com/LongbowEnterprise/BootstrapBlazor/blob/dev/LICENSE)
// **********************************

using System.ComponentModel;

namespace BootstrapBlazor.Shared.Common
{
    /// <summary>
    /// 方法说明类
    /// </summary>
    public class MethodItem : EventItem
    {
        /// <summary>
        /// 参数
        /// </summary>
        [DisplayName("参数")]
        public string Parameters { get; set; } = "";

        /// <summary>
        /// 返回值
        /// </summary>
        [DisplayName("返回值")]
        public string ReturnValue { get; set; } = "";
    }
}
