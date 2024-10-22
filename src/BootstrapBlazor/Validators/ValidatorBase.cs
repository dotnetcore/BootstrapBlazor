// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// IValidator 实现类基类
/// </summary>
public abstract class ValidatorBase : IValidator
{
    /// <summary>
    /// 数据验证方法
    /// </summary>
    /// <param name="propertyValue"></param>
    /// <param name="context"></param>
    /// <param name="results"></param>
    public abstract void Validate(object? propertyValue, ValidationContext context, List<ValidationResult> results);
}
