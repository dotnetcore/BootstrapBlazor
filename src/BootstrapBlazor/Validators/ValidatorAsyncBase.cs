// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
///  <para lang="zh">IValidator 异步实现类基类</para>
///  <para lang="en">IValidator async implementation base class</para>
/// </summary>
public abstract class ValidatorAsyncBase : IValidatorAsync
{
    [ExcludeFromCodeCoverage]
    void IValidator.Validate(object? propertyValue, ValidationContext context, List<ValidationResult> results)
    {

    }

    /// <summary>
    ///  <para lang="zh">数据验证方法</para>
    ///  <para lang="en">Data validation method</para>
    /// </summary>
    /// <param name="propertyValue"></param>
    /// <param name="context"></param>
    /// <param name="results"></param>
    public abstract Task ValidateAsync(object? propertyValue, ValidationContext context, List<ValidationResult> results);
}
