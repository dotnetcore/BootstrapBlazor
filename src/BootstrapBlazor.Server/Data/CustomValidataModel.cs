// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Server.Data;

/// <summary>
/// 公司模型类
/// </summary>
public class CustomValidataModel : IValidatableObject, IValidataResult
{
    /// <summary>
    /// 名称
    /// </summary>
    [Display(Name = "名称")]
    public string? Name { get; set; }

    /// <summary>
    /// 联系电话1
    /// </summary>
    [Display(Name = "联系电话1")]
    public string? Telephone1 { get; set; }

    /// <summary>
    /// 联系电话2
    /// </summary>
    [Display(Name = "联系电话2")]
    public string? Telephone2 { get; set; }

    private readonly List<string> _clearMemberNames = [];

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="validationContext"></param>
    /// <returns></returns>
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        _clearMemberNames.Clear();
        if (string.Equals(Telephone1, Telephone2, StringComparison.InvariantCultureIgnoreCase))
        {
            var memberNames = new List<string>([nameof(Telephone1), nameof(Telephone2)]);
            _clearMemberNames.AddRange(memberNames);
            var localizer = validationContext.GetRequiredService<IStringLocalizer<CustomValidataModel>>();
            yield return new ValidationResult(localizer["CanNotBeTheSame"], memberNames);
        }
        if (string.IsNullOrEmpty(Name))
        {
            yield return new ValidationResult("Name is required", [nameof(Name)]);
        }
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <returns></returns>
    public List<string> ClearMembers() => _clearMemberNames;
}
