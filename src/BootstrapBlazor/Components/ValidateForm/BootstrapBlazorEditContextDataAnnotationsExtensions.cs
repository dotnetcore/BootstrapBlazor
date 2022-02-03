// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components.Forms;
using System.ComponentModel.DataAnnotations;

namespace BootstrapBlazor.Components;

/// <summary>
/// BootstrapBlazorEditContextDataAnnotationsExtensions 扩展操作类
/// </summary>
internal static class BootstrapBlazorEditContextDataAnnotationsExtensions
{
    /// <summary>
    /// 添加数据合规检查
    /// </summary>
    /// <param name="editContext">The <see cref="EditContext"/>.</param>
    /// <param name="editForm"></param>
    public static EditContext AddEditContextDataAnnotationsValidation(this EditContext editContext, ValidateForm editForm)
    {
        var messages = new ValidationMessageStore(editContext);

        editContext.OnValidationRequested +=
            (sender, eventArgs) => ValidateModel(sender as EditContext, messages, editForm);

        editContext.OnFieldChanged +=
            (sender, eventArgs) => ValidateField(editContext, messages, eventArgs.FieldIdentifier, editForm);

        return editContext;
    }

    private static void ValidateModel(EditContext? editContext, ValidationMessageStore messages, ValidateForm editForm)
    {
        if (editContext != null)
        {
            var validationContext = new ValidationContext(editContext.Model);
            var validationResults = new List<ValidationResult>();
            editForm.ValidateObject(validationContext, validationResults);

            messages.Clear();
            foreach (var validationResult in validationResults.Where(v => !string.IsNullOrEmpty(v.ErrorMessage)))
            {
                if (!validationResult.MemberNames.Any())
                {
                    messages.Add(new FieldIdentifier(editContext.Model, fieldName: string.Empty), validationResult.ErrorMessage!);
                    continue;
                }

                foreach (var memberName in validationResult.MemberNames)
                {
                    messages.Add(editContext.Field(memberName), validationResult.ErrorMessage!);
                }
            }
            editContext.NotifyValidationStateChanged();
        }
    }

    private static void ValidateField(EditContext editContext, ValidationMessageStore messages, in FieldIdentifier fieldIdentifier, ValidateForm editForm)
    {
        // 获取验证消息
        var results = new List<ValidationResult>();
        var validationContext = new ValidationContext(fieldIdentifier.Model)
        {
            MemberName = fieldIdentifier.FieldName,
            DisplayName = fieldIdentifier.GetDisplayName()
        };

        editForm.ValidateField(validationContext, results, fieldIdentifier);

        messages.Clear(fieldIdentifier);
        messages.Add(fieldIdentifier, results.Where(v => !string.IsNullOrEmpty(v.ErrorMessage)).Select(result => result.ErrorMessage!));

        editContext.NotifyValidationStateChanged();
    }
}
