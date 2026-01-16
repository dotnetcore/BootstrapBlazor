// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">自定义验证类</para>
/// <para lang="en">Custom validation class</para>
/// </summary>
/// <param name="attribute"></param>
public class FormItemValidator(ValidationAttribute attribute) : ValidatorBase
{
    /// <summary>
    /// <para lang="zh">验证方法</para>
    /// <para lang="en">Validation method</para>
    /// </summary>
    /// <param name="propertyValue"><para lang="zh">待校验值</para><para lang="en">Value to be validated</para></param>
    /// <param name="context"><para lang="zh">ValidateContext 实例</para><para lang="en">ValidateContext instance</para></param>
    /// <param name="results"><para lang="zh">ValidateResult 集合实例</para><para lang="en">ValidateResult collection instance</para></param>
    public override void Validate(object? propertyValue, ValidationContext context, List<ValidationResult> results)
    {
        var result = attribute.GetValidationResult(propertyValue, context);
        if (result != null)
        {
            results.Add(result);
        }
    }

    /// <summary>
    /// <para lang="zh">是否为 RequiredAttribute 标签特性</para>
    /// <para lang="en">Whether it is RequiredAttribute tag attribute</para>
    /// </summary>
    public bool IsRequired => attribute is RequiredAttribute;
}
