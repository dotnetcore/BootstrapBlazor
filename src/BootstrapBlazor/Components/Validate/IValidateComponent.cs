// **********************************
// 框架名称：BootstrapBlazor 
// 框架作者：Argo Zhang
// 开源地址：
// Gitee : https://gitee.com/LongbowEnterprise/BootstrapBlazor
// GitHub: https://github.com/ArgoZhang/BootstrapBlazor 
// 开源协议：LGPL-3.0 (https://gitee.com/LongbowEnterprise/BootstrapBlazor/blob/dev/LICENSE)
// **********************************

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// IValidComponent 接口
    /// </summary>
    public interface IValidateComponent
    {
        /// <summary>
        /// 数据验证方法
        /// </summary>
        /// <param name="propertyValue"></param>
        /// <param name="context"></param>
        /// <param name="results"></param>
        void ValidateProperty(object? propertyValue, ValidationContext context, List<ValidationResult> results);

        /// <summary>
        /// 显示或者隐藏提示信息方法
        /// </summary>
        /// <param name="results"></param>
        /// <param name="validProperty">是否为模型验证 true 为属性验证 false 为整个模型验证</param>
        void ToggleMessage(IEnumerable<ValidationResult> results, bool validProperty);
    }
}
