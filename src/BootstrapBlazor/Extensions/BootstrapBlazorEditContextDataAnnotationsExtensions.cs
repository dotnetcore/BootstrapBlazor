// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Author: Argo Zhang (argo@live.ca) Website: https://www.blazor.zone

using Microsoft.AspNetCore.Components.Forms;

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
    /// <param name="provider"></param>
    public static EditContext AddEditContextDataAnnotationsValidation(this EditContext editContext, ValidateForm editForm, IServiceProvider provider)
    {
        var messages = new ValidationMessageStore(editContext);

        editContext.OnValidationRequested += async (sender, eventArgs) => await editForm.ValidateModel(sender as EditContext, messages, provider);
        editContext.OnFieldChanged += async (sender, eventArgs) => await editForm.ValidateField(editContext, messages, eventArgs, provider);
        return editContext;
    }

    private static async Task ValidateModel(this ValidateForm editForm, EditContext? editContext, ValidationMessageStore messages, IServiceProvider provider)
    {
        if (editContext != null)
        {
            var validationContext = new ValidationContext(editContext.Model, provider, null);
            var validationResults = new List<ValidationResult>();
            await editForm.ValidateObject(validationContext, validationResults);

            messages.Clear();
            foreach (var validationResult in validationResults.Where(v => !string.IsNullOrEmpty(v.ErrorMessage)))
            {
                foreach (var memberName in validationResult.MemberNames)
                {
                    if (!string.IsNullOrEmpty(memberName))
                    {
                        messages.Add(editContext.Field(memberName), validationResult.ErrorMessage!);
                    }
                }
            }
            editContext.NotifyValidationStateChanged();
        }
    }

    private static async Task ValidateField(this ValidateForm editForm, EditContext editContext, ValidationMessageStore messages, FieldChangedEventArgs args, IServiceProvider provider)
    {
        // 获取验证消息
        var validationResults = new List<ValidationResult>();
        var validationContext = new ValidationContext(args.FieldIdentifier.Model, provider, null)
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
