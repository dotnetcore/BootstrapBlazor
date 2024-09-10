// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Server.Data;

/// <summary>
/// 公司模型类
/// </summary>
public class Company : IValidatableObject
{
    /// <summary>
    /// 名称
    /// </summary>
    [Required(ErrorMessage = "{0}不能为空")]
    [Display(Name = "名称")]
    public string? Name { get; set; }

    /// <summary>
    /// 联系电话1
    /// </summary>
    [Required(ErrorMessage = "{0}不能为空")]
    [Display(Name = "联系电话1")]
    public string? Telephone1 { get; set; }

    /// <summary>
    /// 联系电话2
    /// </summary>
    [Display(Name = "联系电话2")]
    public string? Telephone2 { get; set; }

    /// <inheritdoc/>
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (string.Equals(Telephone1, Telephone2, StringComparison.InvariantCultureIgnoreCase))
        {
            var localizer = validationContext.GetRequiredService<IStringLocalizer<Company>>();
            yield return new ValidationResult(localizer["Telephone1AndTelephone2.CanNotBeTheSame"], [nameof(Telephone1), nameof(Telephone2)]);
        }
        if (string.IsNullOrEmpty(Name))
        {
            yield return new ValidationResult("Name is required", [nameof(Name)]);
        }
    }
}
