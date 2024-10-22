// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Server.Data;

/// <summary>
/// 公司模型类
/// </summary>
public class CustomValidateCollectionModel : IValidateCollection
{
    /// <summary>
    /// 联系电话1
    /// </summary>
    public string? Telephone1 { get; set; }

    /// <summary>
    /// 联系电话2
    /// </summary>
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
    public List<string> GetValidMemberNames() => _validMemberNames;

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <returns></returns>
    public List<ValidationResult> GetInvalidMemberNames() => _invalidMemberNames;
}
