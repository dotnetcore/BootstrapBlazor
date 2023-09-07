// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// DateTimeRange 验证
/// </summary>
public class DateTimeRangeRequiredValidator : RequiredValidator
{
    /// <inheritdoc/>
    public override void Validate(object? propertyValue, ValidationContext context, List<ValidationResult> results)
    {
        if (propertyValue is DateTimeRangeValue d && (CheckStart() || CheckEnd()))
        {
            propertyValue = null;
        }
        var errorMessage = GetLocalizerErrorMessage(context, LocalizerFactory, Options);
        var memberNames = string.IsNullOrEmpty(context.MemberName) ? null : new string[] { context.MemberName };
        if (propertyValue == null)
        {
            results.Add(new ValidationResult(errorMessage, memberNames));
        }

        bool CheckStart() => d.Start == null || d.Start.Value == DateTime.MinValue;

        bool CheckEnd() => d.End == null || d.End == DateTime.MinValue;
    }
}
