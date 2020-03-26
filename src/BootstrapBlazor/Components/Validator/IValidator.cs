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
        string ErrorMessage { get; set; }

        /// <summary>
        /// 验证方法
        /// </summary>
        /// <param name="propertyValue"></param>
        /// <param name="context"></param>
        /// <param name="results"></param>
        void Validate(object? propertyValue, ValidationContext context, List<ValidationResult> results);
    }
}
