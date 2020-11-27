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
    /// IValidator 接口
    /// </summary>
    public interface IValidator
    {
        /// <summary>
        /// 获得/设置 错误描述信息
        /// </summary>
        string? ErrorMessage { get; set; }

        /// <summary>
        /// 验证方法
        /// </summary>
        /// <param name="propertyValue"></param>
        /// <param name="context"></param>
        /// <param name="results"></param>
        void Validate(object? propertyValue, ValidationContext context, List<ValidationResult> results);
    }
}
