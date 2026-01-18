// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.AspNetCore.Components.Forms;

namespace BootstrapBlazor.Components;

/// <summary>
///  <para lang="zh">BootstrapBlazorDataAnnotationsValidator 验证组件</para>
///  <para lang="en">BootstrapBlazorDataAnnotationsValidator Validation Component</para>
/// </summary>
public class BootstrapBlazorDataAnnotationsValidator : ComponentBase, IDisposable
{
    /// <summary>
    ///  <para lang="zh">获得/设置 当前编辑数据上下文</para>
    ///  <para lang="en">Gets or sets Current Edit Data Context</para>
    /// </summary>
    [CascadingParameter]
    [NotNull]
    private EditContext? CurrentEditContext { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 当前编辑窗体上下文</para>
    ///  <para lang="en">Gets or sets 当前编辑窗体上下文</para>
    /// </summary>
    [CascadingParameter]
    [NotNull]
    private ValidateForm? ValidateForm { get; set; }

    [Inject]
    [NotNull]
    private IServiceProvider? Provider { get; set; }

    [NotNull]
    private ValidationMessageStore? _message = null;

    /// <summary>
    ///  <para lang="zh">初始化方法</para>
    ///  <para lang="en">初始化方法</para>
    /// </summary>
    protected override void OnInitialized()
    {
        if (ValidateForm == null)
        {
            throw new InvalidOperationException($"{nameof(BootstrapBlazorDataAnnotationsValidator)} requires a cascading parameter of type {nameof(Components.ValidateForm)}. For example, you can use {nameof(BootstrapBlazorDataAnnotationsValidator)} inside an {nameof(Components.ValidateForm)}.");
        }

        _message = new ValidationMessageStore(CurrentEditContext);
        AddEditContextDataAnnotationsValidation();
    }

    private TaskCompletionSource<bool>? _tcs;
    /// <summary>
    ///  <para lang="zh">手动验证表单方法</para>
    ///  <para lang="en">手动验证表单方法</para>
    /// </summary>
    /// <returns></returns>
    internal async Task<bool> ValidateAsync()
    {
        _tcs = new(false);
        var ret = CurrentEditContext.Validate();
        var valid = await _tcs.Task;
        return ret && valid;
    }

    /// <summary>
    ///  <para lang="zh">手动验证表单方法</para>
    ///  <para lang="en">手动验证表单方法</para>
    /// </summary>
    [ExcludeFromCodeCoverage]
    internal bool Validate() => CurrentEditContext.Validate();

    private void AddEditContextDataAnnotationsValidation()
    {
        CurrentEditContext.OnValidationRequested += OnValidationRequested;
        CurrentEditContext.OnFieldChanged += OnFieldChanged;
    }

    private void RemoveEditContextDataAnnotationsValidation()
    {
        CurrentEditContext.OnValidationRequested -= OnValidationRequested;
        CurrentEditContext.OnFieldChanged -= OnFieldChanged;
    }

    private void OnValidationRequested(object? sender, ValidationRequestedEventArgs args)
    {
        _ = ValidateModel(CurrentEditContext, _message, Provider);
    }

    private void OnFieldChanged(object? sender, FieldChangedEventArgs args)
    {
        _ = ValidateField(CurrentEditContext, _message, args.FieldIdentifier, Provider);
    }

    private async Task ValidateModel(EditContext editContext, ValidationMessageStore messages, IServiceProvider provider)
    {
        var validationContext = new ValidationContext(editContext.Model, provider, null);
        var validationResults = new List<ValidationResult>();
        await ValidateForm.ValidateObject(validationContext, validationResults);

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

        if (_tcs != null)
        {
            _tcs.TrySetResult(validationResults.Count == 0);
        }
    }

    private async Task ValidateField(EditContext editContext, ValidationMessageStore messages, FieldIdentifier field, IServiceProvider provider)
    {
        // 获取验证消息
        var validationResults = new List<ValidationResult>();
        var validationContext = new ValidationContext(field.Model, provider, null)
        {
            MemberName = field.FieldName,
            DisplayName = field.GetDisplayName()
        };

        await ValidateForm.ValidateFieldAsync(validationContext, validationResults);

        messages.Clear(field);
        messages.Add(field, validationResults.Where(v => !string.IsNullOrEmpty(v.ErrorMessage)).Select(result => result.ErrorMessage!));

        editContext.NotifyValidationStateChanged();
    }

    private void Dispose(bool disposing)
    {
        if (disposing)
        {
            RemoveEditContextDataAnnotationsValidation();
        }
    }

    /// <summary>
    ///  <para lang="zh"><inheritdoc/></para>
    ///  <para lang="en"><inheritdoc/></para>
    /// </summary>
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}
