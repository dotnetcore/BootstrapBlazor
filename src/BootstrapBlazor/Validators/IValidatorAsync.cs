// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using System.ComponentModel.DataAnnotations;

namespace BootstrapBlazor.Components;

/// <summary>
/// 异步验证组件
/// </summary>
public interface IValidatorAsync : IValidator
{
    /// <summary>
    /// 异步验证方法
    /// </summary>
    /// <param name="propertyValue"></param>
    /// <param name="context"></param>
    /// <param name="results"></param>
    Task ValidateAsync(object? propertyValue, ValidationContext context, List<ValidationResult> results);
}
