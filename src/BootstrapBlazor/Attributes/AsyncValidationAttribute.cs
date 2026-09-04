// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

#if !NET11_0_OR_GREATER
namespace System.ComponentModel.DataAnnotations;

/// <summary>
/// <para lang="zh">异步验证标签基类</para>
/// <para lang="en">Base class for asynchronous validation attributes</para>
/// </summary>
public abstract class AsyncValidationAttribute : ValidationAttribute
{
    /// <summary>
    /// <para lang="zh">构造函数</para>
    /// <para lang="en">Constructor</para>
    /// </summary>
    protected AsyncValidationAttribute()
    {

    }

    /// <summary>
    /// <para lang="zh">构造函数</para>
    /// <para lang="en">Constructor</para>
    /// </summary>
    /// <param name="errorMessage"></param>
    protected AsyncValidationAttribute(string errorMessage) : base(errorMessage)
    {

    }

    /// <summary>
    /// <para lang="zh">构造函数</para>
    /// <para lang="en">Constructor</para>
    /// </summary>
    /// <param name="errorMessageAccessor"></param>
    protected AsyncValidationAttribute(Func<string> errorMessageAccessor) : base(errorMessageAccessor)
    {

    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected abstract override ValidationResult? IsValid(object? value, ValidationContext validationContext);

    /// <summary>
    /// <para lang="zh">异步判断是否验证通过</para>
    /// <para lang="en">Asynchronously determines whether the value is valid</para>
    /// </summary>
    /// <param name="value"></param>
    /// <param name="validationContext"></param>
    /// <param name="cancellationToken"></param>
    protected abstract Task<ValidationResult?> IsValidAsync(object? value, ValidationContext validationContext, CancellationToken cancellationToken);

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public sealed override bool IsValid(object? value) => IsValid(value, null!) == ValidationResult.Success;

    /// <summary>
    /// <para lang="zh">异步获取验证结果</para>
    /// <para lang="en">Asynchronously gets the validation result</para>
    /// </summary>
    /// <param name="value"></param>
    /// <param name="validationContext"></param>
    /// <param name="cancellationToken"></param>
    public async Task<ValidationResult?> GetValidationResultAsync(object? value, ValidationContext validationContext, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(validationContext);

        var result = await IsValidAsync(value, validationContext, cancellationToken).ConfigureAwait(false);
        if (result != null && string.IsNullOrEmpty(result.ErrorMessage))
        {
            result = new ValidationResult(FormatErrorMessage(validationContext.DisplayName), result.MemberNames);
        }
        return result;
    }
}
#endif
