// **********************************
// 框架名称：BootstrapBlazor 
// 框架作者：Argo Zhang
// 开源地址：
// Gitee : https://gitee.com/LongbowEnterprise/BootstrapBlazor
// GitHub: https://github.com/ArgoZhang/BootstrapBlazor 
// 开源协议：LGPL-3.0 (https://gitee.com/LongbowEnterprise/BootstrapBlazor/blob/dev/LICENSE)
// **********************************

using System.Collections.Generic;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// IRules 接口
    /// </summary>
    public interface IValidateRules
    {
        /// <summary>
        /// 获得 Rules 集合
        /// </summary>
        ICollection<IValidator> Rules { get; }

        /// <summary>
        /// 验证组件添加时回调此方法
        /// </summary>
        /// <param name="validator"></param>
        void OnRuleAdded(IValidator validator);
    }
}
