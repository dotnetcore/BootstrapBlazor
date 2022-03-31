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

        editContext.OnValidationRequested += async (sender, eventArgs) => await editForm.ValidateModel(sender as EditContext, messages);

        editContext.OnFieldChanged += async (sender, eventArgs) => await editForm.ValidateField(editContext, messages, eventArgs);

        return editContext;
    }

    private static async Task ValidateModel(this ValidateForm editForm, EditContext? editContext, ValidationMessageStore messages)
    {
        if (editContext != null)
        {
            var validationContext = new ValidationContext(editContext.Model);
            var validationResults = new List<ValidationResult>();
            await editForm.ValidateObject(validationContext, validationResults);

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

    private static async Task ValidateField(this ValidateForm editForm, EditContext editContext, ValidationMessageStore messages, FieldChangedEventArgs args)
    {
        // 获取验证消息
        var validationResults = new List<ValidationResult>();
        var validationContext = new ValidationContext(args.FieldIdentifier.Model)
        {
            MemberName = args.FieldIdentifier.FieldName,
            DisplayName = args.FieldIdentifier.GetDisplayName()
        };

        await editForm.ValidateFieldAsync(validationContext, validationResults);

        messages.Clear(args.FieldIdentifier);
        messages.Add(args.FieldIdentifier, validationResults.Where(v => !string.IsNullOrEmpty(v.ErrorMessage)).Select(result => result.ErrorMessage!));

        editContext.NotifyValidationStateChanged();
    }
}
