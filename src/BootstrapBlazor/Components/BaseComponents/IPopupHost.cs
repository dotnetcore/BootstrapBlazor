// **********************************
// 框架名称：BootstrapBlazor 
// 框架作者：Argo Zhang
// 开源地址：
// Gitee : https://gitee.com/LongbowEnterprise/BootstrapBlazor
// GitHub: https://github.com/ArgoZhang/BootstrapBlazor 
// 开源协议：LGPL-3.0 (https://gitee.com/LongbowEnterprise/BootstrapBlazor/blob/dev/LICENSE)
// **********************************

using Microsoft.AspNetCore.Components;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// IPopupOption 接口定义
    /// </summary>
    public interface IPopupHost
    {
        /// <summary>
        /// 获得/设置 弹窗主体实例 默认为空
        /// </summary>
        public ComponentBase? Host { get; set; }
    }
}
