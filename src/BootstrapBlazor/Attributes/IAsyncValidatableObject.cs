// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

#if !NET11_0_OR_GREATER
namespace System.ComponentModel.DataAnnotations;

/// <summary>
/// <para lang="zh">异步验证对象接口</para>
/// <para lang="en">Interface for asynchronously validating an object</para>
/// </summary>
public interface IAsyncValidatableObject : IValidatableObject
{
    /// <summary>
    /// <para lang="zh">异步验证当前对象</para>
    /// <para lang="en">Asynchronously validates the current object</para>
    /// </summary>
    /// <param name="validationContext"></param>
    /// <param name="cancellationToken"></param>
    IAsyncEnumerable<ValidationResult> ValidateAsync(ValidationContext validationContext, CancellationToken cancellationToken = default);
}
#endif
