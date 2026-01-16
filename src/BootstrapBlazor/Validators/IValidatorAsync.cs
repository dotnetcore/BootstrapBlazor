// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">异步验证组件</para>
/// <para lang="en">Async validation component</para>
/// </summary>
public interface IValidatorAsync : IValidator
{
    /// <summary>
    /// <para lang="zh">异步验证方法</para>
    /// <para lang="en">Async validation method</para>
    /// </summary>
    /// <param name="propertyValue"></param>
    /// <param name="context"></param>
    /// <param name="results"></param>
    Task ValidateAsync(object? propertyValue, ValidationContext context, List<ValidationResult> results);
}
