// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.AspNetCore.Components.Forms;
using Microsoft.Extensions.Logging;

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">BootstrapBlazorDataAnnotationsValidator 验证组件</para>
/// <para lang="en">BootstrapBlazorDataAnnotationsValidator validation component</para>
/// </summary>
public class BootstrapBlazorDataAnnotationsValidator : ComponentBase, IDisposable
{
    /// <summary>
    /// <para lang="zh">获得/设置 当前编辑数据上下文</para>
    /// <para lang="en">Gets or sets the current edit context</para>
    /// </summary>
    [CascadingParameter]
    [NotNull]
    private EditContext? CurrentEditContext { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 当前编辑窗体上下文</para>
    /// <para lang="en">Gets or sets the current validation form</para>
    /// </summary>
    [CascadingParameter]
    [NotNull]
    private ValidateForm? ValidateForm { get; set; }

    [Inject]
    [NotNull]
    private IServiceProvider? Provider { get; set; }

    [Inject]
    [NotNull]
    private ILogger<BootstrapBlazorDataAnnotationsValidator>? Logger { get; set; }

    [NotNull]
    private ValidationMessageStore? _message = null;

    /// <summary>
    /// <para lang="zh">初始化方法</para>
    /// <para lang="en">Initializes the component</para>
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

    internal Task<bool> ValidateAsync(CancellationToken cancellationToken = default) => CurrentEditContext.ValidateAsync(cancellationToken);

    private void OnValidationRequested(object? sender, ValidationRequestedEventArgs args)
    {
        args.AddAsyncValidator(ValidateModelAsync);
    }

    private void OnFieldChanged(object? sender, FieldChangedEventArgs args)
    {
        var fieldIdentifier = args.FieldIdentifier;
        CurrentEditContext.RegisterAsyncFieldValidator(fieldIdentifier, cancellationToken => ValidateFieldAsync(CurrentEditContext, _message, fieldIdentifier, Provider, cancellationToken));
    }

    private async Task ValidateModelAsync(CancellationToken cancellationToken)
    {
        try
        {
            await ValidateModelAsync(CurrentEditContext, _message, Provider, cancellationToken);
        }
        catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
        {
            throw;
        }
        catch (Exception exception)
        {
            Logger.LogError(exception, "An exception occurred while validating the form.");
            throw;
        }
    }

    private async Task ValidateModelAsync(EditContext editContext, ValidationMessageStore messages, IServiceProvider provider, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        var validationContext = new ValidationContext(editContext.Model, provider, null);
        var validationResults = new List<ValidationResult>();

        messages.Clear();
        editContext.NotifyValidationStateChanged();

        await ValidateForm.ValidateObjectAsync(validationContext, validationResults, cancellationToken);
        cancellationToken.ThrowIfCancellationRequested();

        foreach (var validationResult in validationResults.Where(v => !string.IsNullOrEmpty(v.ErrorMessage)))
        {
            var hasMemberNames = false;
            foreach (var memberName in validationResult.MemberNames)
            {
                if (!string.IsNullOrEmpty(memberName))
                {
                    hasMemberNames = true;
                    messages.Add(editContext.Field(memberName), validationResult.ErrorMessage!);
                }
            }
            if (!hasMemberNames)
            {
                messages.Add(new FieldIdentifier(editContext.Model, string.Empty), validationResult.ErrorMessage!);
            }
        }
        editContext.NotifyValidationStateChanged();
    }

    private async Task ValidateFieldAsync(EditContext editContext, ValidationMessageStore messages, FieldIdentifier field, IServiceProvider provider, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        var validationResults = new List<ValidationResult>();
        var validationContext = new ValidationContext(field.Model, provider, null)
        {
            MemberName = field.FieldName,
            DisplayName = field.GetDisplayName()
        };

        messages.Clear(field);
        editContext.NotifyValidationStateChanged();

        try
        {
            await ValidateForm.ValidateFieldAsync(field, validationContext, validationResults, cancellationToken);
            cancellationToken.ThrowIfCancellationRequested();
        }
        catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
        {
            return;
        }
        catch (Exception exception)
        {
            Logger.LogError(exception, "An exception occurred while validating the field.");
            throw;
        }

        messages.Add(field, validationResults.Where(v => !string.IsNullOrEmpty(v.ErrorMessage)).Select(result => result.ErrorMessage!));
        editContext.NotifyValidationStateChanged();
    }

    private void Dispose(bool disposing)
    {
        if (disposing)
        {
#if !NET11_0_OR_GREATER
            CurrentEditContext.CancelAsyncFieldValidations();
#endif
            RemoveEditContextDataAnnotationsValidation();
        }
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}
