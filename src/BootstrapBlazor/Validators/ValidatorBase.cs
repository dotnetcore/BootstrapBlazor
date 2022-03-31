// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using System.ComponentModel.DataAnnotations;

namespace BootstrapBlazor.Components;

/// <summary>
/// IValidator 实现类基类
/// </summary>
public abstract class ValidatorBase : IValidator
{
    /// <summary>
    /// 获得/设置 错误描述信息 默认为 null 需要赋值
    /// </summary>
    public string? ErrorMessage { get; set; }

    /// <summary>
    /// 数据验证方法
    /// </summary>
    /// <param name="propertyValue"></param>
    /// <param name="context"></param>
    /// <param name="results"></param>
    protected virtual void Validate(object? propertyValue, ValidationContext context, List<ValidationResult> results)
    {

    }

    /// <summary>
    /// 异步数据验证方法
    /// </summary>
    /// <param name="propertyValue"></param>
    /// <param name="context"></param>
    /// <param name="results"></param>
    public virtual Task ValidateAsync(object? propertyValue, ValidationContext context, List<ValidationResult> results)
    {
        Validate(propertyValue, context, results);
        return Task.CompletedTask;
    }
}
