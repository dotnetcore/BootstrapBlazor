// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using System.ComponentModel.DataAnnotations;

namespace BootstrapBlazor.Components;

/// <summary>
/// 自定义验证类
/// </summary>
public class FormItemValidator : ValidatorBase
{
    /// <summary>
    /// 获得 ValidationAttribute 实例
    /// </summary>
    public ValidationAttribute Validator { get; }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="attribute"></param>
    public FormItemValidator(ValidationAttribute attribute)
    {
        Validator = attribute;
    }

    /// <summary>
    /// 验证方法
    /// </summary>
    /// <param name="propertyValue">待校验值</param>
    /// <param name="context">ValidateContext 实例</param>
    /// <param name="results">ValidateResult 集合实例</param>
    public override void Validate(object? propertyValue, ValidationContext context, List<ValidationResult> results)
    {
        var result = Validator.GetValidationResult(propertyValue, context);
        if (result != null)
        {
            results.Add(result);
        }
    }
}
