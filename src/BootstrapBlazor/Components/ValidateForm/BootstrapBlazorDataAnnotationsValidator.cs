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

#if NET11_0_OR_GREATER
    internal Task<bool> ValidateAsync(CancellationToken cancellationToken = default) => CurrentEditContext.ValidateAsync(cancellationToken);

    private void OnValidationRequested(object? sender, ValidationRequestedEventArgs args)
    {
        args.AddAsyncValidator(cancellationToken => ValidateModelAsync(CurrentEditContext, _message, Provider, cancellationToken));
    }

    private void OnFieldChanged(object? sender, FieldChangedEventArgs args)
    {
        var fieldIdentifier = args.FieldIdentifier;
        CurrentEditContext.RegisterAsyncFieldValidator(fieldIdentifier, cancellationToken => ValidateFieldAsync(CurrentEditContext, _message, fieldIdentifier, Provider, cancellationToken));
    }
#else
#if NET9_0_OR_GREATER
    private readonly Lock _fieldValidationLock = new();
#else
    private readonly object _fieldValidationLock = new();
#endif

    private readonly SemaphoreSlim _validationLock = new(1, 1);
    private readonly Dictionary<FieldIdentifier, FieldValidationOperation> _fieldValidationOperations = [];
    private bool _suppressValidationRequested;

    internal async Task<bool> ValidateAsync(CancellationToken cancellationToken = default)
    {
        await _validationLock.WaitAsync(cancellationToken);
        try
        {
            CancelFieldValidations();
            _message.Clear();

            bool synchronousValid;
            _suppressValidationRequested = true;
            try
            {
                // NET10 只有同步方法
                synchronousValid = CurrentEditContext.Validate();
            }
            finally
            {
                _suppressValidationRequested = false;
            }

            // 通过 _suppressValidationRequested 控制 OnValidationRequested 事件中的 ValidateModelAsync 不被调用，避免重复验证
            var valid = await ValidateModelAsync(CurrentEditContext, _message, Provider, cancellationToken);
            return synchronousValid && valid && !CurrentEditContext.GetValidationMessages().Any();
        }
        catch (OperationCanceledException)
        {
            throw;
        }
        catch (Exception exception)
        {
            Logger.LogError(exception, "An exception occurred while validating the form.");
            return false;
        }
        finally
        {
            _validationLock.Release();
        }
    }

    private async void OnValidationRequested(object? sender, ValidationRequestedEventArgs args)
    {
        if (!_suppressValidationRequested)
        {
            try
            {
                await ValidateModelAsync(CurrentEditContext, _message, Provider, CancellationToken.None);
            }
            catch (Exception exception)
            {
                Logger.LogError(exception, "An exception occurred while validating the form.");
            }
        }
    }

    private void OnFieldChanged(object? sender, FieldChangedEventArgs args)
    {
        var fieldIdentifier = args.FieldIdentifier;

        FieldValidationOperation? previousOperation;
        FieldValidationOperation operation;
        lock (_fieldValidationLock)
        {
            _fieldValidationOperations.Remove(fieldIdentifier, out previousOperation);
            operation = new FieldValidationOperation();
            _fieldValidationOperations.Add(fieldIdentifier, operation);
        }
        previousOperation?.Cancel();
        _ = ValidateFieldAndCleanupAsync(fieldIdentifier, operation);
    }

    private async Task ValidateFieldAndCleanupAsync(FieldIdentifier fieldIdentifier, FieldValidationOperation operation)
    {
        try
        {
            await ValidateFieldAsync(CurrentEditContext, _message, fieldIdentifier, Provider, operation.Token);
        }
        catch (OperationCanceledException) when (operation.IsCancellationRequested)
        {
        }
        finally
        {
            lock (_fieldValidationLock)
            {
                if (_fieldValidationOperations.TryGetValue(fieldIdentifier, out var currentOperation)
                    && ReferenceEquals(currentOperation, operation))
                {
                    _fieldValidationOperations.Remove(fieldIdentifier);
                }
            }
            operation.Complete();
        }
    }

    private void CancelFieldValidations()
    {
        FieldValidationOperation[] operations;
        lock (_fieldValidationLock)
        {
            operations = [.. _fieldValidationOperations.Values];
            _fieldValidationOperations.Clear();
        }
        foreach (var operation in operations)
        {
            operation.Cancel();
        }
    }

    private sealed class FieldValidationOperation
    {
        private CancellationTokenSource? _tokenSource;

        public CancellationToken Token { get; }

        public bool IsCancellationRequested => Token.IsCancellationRequested;

        public FieldValidationOperation()
        {
            _tokenSource = new();
            Token = _tokenSource.Token;
        }

        public void Cancel()
        {
            var tokenSource = Interlocked.Exchange(ref _tokenSource, null);
            if (tokenSource != null)
            {
                try
                {
                    tokenSource.Cancel();
                }
                finally
                {
                    tokenSource.Dispose();
                }
            }
        }

        public void Complete()
        {
            Interlocked.Exchange(ref _tokenSource, null)?.Dispose();
        }
    }
#endif

    private async Task<bool> ValidateModelAsync(EditContext editContext, ValidationMessageStore messages, IServiceProvider provider, CancellationToken cancellationToken)
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
        return validationResults.Count == 0;
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

        messages.Add(field, validationResults.Where(v => !string.IsNullOrEmpty(v.ErrorMessage)).Select(result => result.ErrorMessage!));

        editContext.NotifyValidationStateChanged();
    }

    private void Dispose(bool disposing)
    {
        if (disposing)
        {
#if !NET11_0_OR_GREATER
            CancelFieldValidations();
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
