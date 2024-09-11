// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Server.Data;

/// <summary>
/// 公司模型类
/// </summary>
public class CustomValidataModel : IValidateCollection
{
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

    private readonly List<string> _validMemberNames = [];

    private readonly List<ValidationResult> _invalidMemberNames = [];

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="validationContext"></param>
    /// <returns></returns>
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        _validMemberNames.Clear();
        _invalidMemberNames.Clear();
        if (string.Equals(Telephone1, Telephone2, StringComparison.InvariantCultureIgnoreCase))
        {
            var localizer = validationContext.GetRequiredService<IStringLocalizer<CustomValidataModel>>();
            var errorMessage = localizer["CanNotBeTheSame"];
            if (validationContext.MemberName == nameof(Telephone1))
            {
                _invalidMemberNames.Add(new ValidationResult(errorMessage, [nameof(Telephone2)]));
            }
            else if (validationContext.MemberName == nameof(Telephone2))
            {
                _invalidMemberNames.Add(new ValidationResult(errorMessage, [nameof(Telephone1)]));
            }
            yield return new ValidationResult(errorMessage, [validationContext.MemberName!]);
        }
        else if (validationContext.MemberName == nameof(Telephone1))
        {
            _validMemberNames.Add(nameof(Telephone2));

        }
        else if (validationContext.MemberName == nameof(Telephone2))
        {
            _validMemberNames.Add(nameof(Telephone1));
        }
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <returns></returns>
    public List<string> ValidMemberNames() => _validMemberNames;

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <returns></returns>
    public List<ValidationResult> InvalidMemberNames() => _invalidMemberNames;
}
