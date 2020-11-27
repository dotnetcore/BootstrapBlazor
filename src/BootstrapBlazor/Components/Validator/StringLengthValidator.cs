// **********************************
// 框架名称：BootstrapBlazor 
// 框架作者：Argo Zhang
// 开源地址：
// Gitee : https://gitee.com/LongbowEnterprise/BootstrapBlazor
// GitHub: https://github.com/ArgoZhang/BootstrapBlazor 
// 开源协议：LGPL-3.0 (https://gitee.com/LongbowEnterprise/BootstrapBlazor/blob/dev/LICENSE)
// **********************************

using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// 
    /// </summary>
    public class StringLengthValidator : ValidatorComponentBase
    {
        /// <summary>
        /// 
        /// </summary>
        [Inject]
        [NotNull]
        private IStringLocalizer<StringLengthValidator>? Localizer { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Parameter]
        public int Length { get; set; } = 50;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="propertyValue"></param>
        /// <param name="context"></param>
        /// <param name="results"></param>
        public override void Validate(object? propertyValue, ValidationContext context, List<ValidationResult> results)
        {
            var val = propertyValue?.ToString() ?? "";
            if (val.Length > Length)
            {
                ErrorMessage = Localizer[nameof(ErrorMessage), Length];
                results.Add(new ValidationResult(ErrorMessage, string.IsNullOrEmpty(context.MemberName) ? null : new string[] { context.MemberName }));
            }
        }
    }
}
