// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using System.ComponentModel.DataAnnotations;

namespace BootstrapBlazor.Components;

/// <summary>
/// IValidator 异步实现类基类
/// </summary>
public abstract class ValidatorAsyncBase : IValidatorAsync
{
    [ExcludeFromCodeCoverage]
    void IValidator.Validate(object? propertyValue, ValidationContext context, List<ValidationResult> results)
    {

    }

    /// <summary>
    /// 数据验证方法
    /// </summary>
    /// <param name="propertyValue"></param>
    /// <param name="context"></param>
    /// <param name="results"></param>
    public abstract Task ValidateAsync(object? propertyValue, ValidationContext context, List<ValidationResult> results);
}
