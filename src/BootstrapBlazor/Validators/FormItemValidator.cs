// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

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
