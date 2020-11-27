// **********************************
// 框架名称：BootstrapBlazor 
// 框架作者：Argo Zhang
// 开源地址：
// Gitee : https://gitee.com/LongbowEnterprise/BootstrapBlazor
// GitHub: https://github.com/ArgoZhang/BootstrapBlazor 
// 开源协议：LGPL-3.0 (https://gitee.com/LongbowEnterprise/BootstrapBlazor/blob/dev/LICENSE)
// **********************************

using System.ComponentModel;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// 步骤状态枚举
    /// </summary>
    public enum StepStatus
    {
        /// <summary>
        /// 未开始
        /// </summary>
        [Description("wait")]
        Wait,

        /// <summary>
        /// 进行中
        /// </summary>
        [Description("process")]
        Process,

        /// <summary>
        /// 
        /// </summary>
        [Description("finish")]
        Finish,

        /// <summary>
        /// 已完成
        /// </summary>
        [Description("success")]
        Success,

        /// <summary>
        /// 
        /// </summary>
        [Description("error")]
        Error,
    }
}
