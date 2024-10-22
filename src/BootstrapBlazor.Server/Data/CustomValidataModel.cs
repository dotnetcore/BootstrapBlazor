// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Server.Data;

/// <summary>
/// 公司模型类
/// </summary>
public class CustomValidataModel : IValidatableObject
{
    /// <summary>
    /// 名称
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// 联系电话1
    /// </summary>
    public string? Telephone1 { get; set; }

    /// <summary>
    /// 联系电话2
    /// </summary>
    public string? Telephone2 { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="validationContext"></param>
    /// <returns></returns>
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (string.Equals(Telephone1, Telephone2, StringComparison.InvariantCultureIgnoreCase))
        {
            var localizer = validationContext.GetRequiredService<IStringLocalizer<CustomValidataModel>>();
            yield return new ValidationResult(localizer["CanNotBeTheSame"], [nameof(Telephone1), nameof(Telephone2)]);
        }

        if (string.IsNullOrEmpty(Name))
        {
            yield return new ValidationResult("Name is required", [nameof(Name)]);
        }
    }
}
