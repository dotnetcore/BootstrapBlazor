// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace BootstrapBlazor.Components;

/// <summary>
/// 最小值验证实现类
/// </summary>
class MinValidator : MaxValidator
{
    /// <summary>
    /// 验证方法
    /// </summary>
    /// <param name="propertyValue">待校验值</param>
    /// <param name="context">ValidateContext 实例</param>
    /// <param name="results">ValidateResult 集合实例</param>
    public override void Validate(object? propertyValue, ValidationContext context, List<ValidationResult> results)
    {
        if (propertyValue != null && propertyValue is int v)
        {
            if (v < Value)
            {
                var errorMessage = string.Format(CultureInfo.CurrentCulture, ErrorMessage ?? "", Value);
                results.Add(new ValidationResult(errorMessage, new string[] { context.MemberName ?? "" }));
            }
        }
    }
}
