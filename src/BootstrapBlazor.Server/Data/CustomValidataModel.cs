// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Server.Data;

/// <summary>
/// 公司模型类
/// </summary>
public class CustomValidataModel : IValidatableObject
{
    /// <summary>
    /// 名称
    /// </summary>
    [Display(Name = "名称")]
    public string? Name { get; set; }

    /// <summary>
    /// 联系电话1
    /// </summary>
    [Display(Name = "联系电话1")]
    public string? Telephone1 { get; set; }

    /// <summary>
    /// 联系电话2
    /// </summary>
    [Display(Name = "联系电话2")]
    public string? Telephone2 { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="validationContext"></param>
    /// <returns></returns>
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (string.Equals(Telephone1, Telephone2, StringComparison.InvariantCultureIgnoreCase))
        {
            var localizer = validationContext.GetRequiredService<IStringLocalizer<CustomValidataModel>>();
            yield return new ValidationResult(localizer["CanNotBeTheSame"], [nameof(Telephone1), nameof(Telephone2)]);
        }
        if (string.IsNullOrEmpty(Name))
        {
            yield return new ValidationResult("Name is required", [nameof(Name)]);
        }
    }
}
