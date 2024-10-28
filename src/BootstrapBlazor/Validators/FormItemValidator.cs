// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// 自定义验证类
/// </summary>
/// <param name="attribute"></param>
public class FormItemValidator(ValidationAttribute attribute) : ValidatorBase
{
    /// <summary>
    /// 验证方法
    /// </summary>
    /// <param name="propertyValue">待校验值</param>
    /// <param name="context">ValidateContext 实例</param>
    /// <param name="results">ValidateResult 集合实例</param>
    public override void Validate(object? propertyValue, ValidationContext context, List<ValidationResult> results)
    {
        var result = attribute.GetValidationResult(propertyValue, context);
        if (result != null)
        {
            results.Add(result);
        }
    }

    /// <summary>
    /// 是否为 RequiredAttribute 标签特性
    /// </summary>
    public bool IsRequired => attribute is RequiredAttribute;
}
