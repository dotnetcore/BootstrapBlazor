// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

#if !NET11_0_OR_GREATER
using Microsoft.AspNetCore.Components.Forms;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">EditContext 异步验证兼容扩展</para>
/// <para lang="en">Async validation compatibility extensions for EditContext</para>
/// </summary>
public static class EditContextExtensions
{
    private static readonly AsyncLocal<ValidationScope?> CurrentScope = new();
    private static readonly ConditionalWeakTable<EditContext, ValidationState> States = new();

    /// <summary>
    /// <para lang="zh">请求验证并等待本次注册的所有异步验证完成</para>
    /// <para lang="en">Requests validation and awaits all async validators registered for this pass</para>
    /// </summary>
    /// <param name="editContext"><para lang="zh">编辑上下文</para><para lang="en">The edit context</para></param>
    /// <param name="cancellationToken"><para lang="zh">取消令牌</para><para lang="en">The cancellation token</para></param>
    /// <returns><para lang="zh">无验证消息且异步验证未发生异常时返回 true</para><para lang="en">True if there are no validation messages or async validation faults</para></returns>
    /// <remarks>
    /// <para lang="zh">同一上下文的验证按顺序执行，并在请求验证前取消字段验证、清除字段异常状态。不支持在验证处理程序中重入验证同一上下文。异步任务异常通过 IsValidationFaulted 和验证状态通知报告；事件处理程序同步异常直接传播。</para>
    /// <para lang="en">Passes for the same context are serialized. Pending field validations are cancelled and field faults are cleared before requesting validation. Reentrant validation of the same context is unsupported. Async task faults are reported through IsValidationFaulted and validation state notifications; synchronous handler exceptions propagate.</para>
    /// </remarks>
    public static async Task<bool> ValidateAsync(this EditContext editContext, CancellationToken cancellationToken = default)
    {
        var previousScope = CurrentScope.Value;
        for (var parent = previousScope; parent != null; parent = parent.Parent)
        {
            if (parent.IsActive && ReferenceEquals(parent.EditContext, editContext))
            {
                throw new InvalidOperationException("Reentrant validation of the same EditContext is not supported.");
            }
        }

        var state = GetState(editContext);
        await state.Semaphore.WaitAsync(cancellationToken);
        var scope = new ValidationScope(editContext, previousScope);
        CurrentScope.Value = scope;
        try
        {
            try
            {
                CancelAsyncFieldValidations(editContext);
                // Older frameworks share ValidationRequestedEventArgs.Empty across all edit contexts.
                // Collect in this pass's scope instead of attaching state to the event arguments.
                editContext.Validate();
            }
            finally
            {
                scope.IsCollecting = false;
            }

            var tasks = new List<Task>();
            foreach (var validator in scope.Validators)
            {
                var task = validator(cancellationToken)
                    ?? throw new InvalidOperationException("The async validator returned a null task.");
                if (!task.IsCompletedSuccessfully)
                {
                    tasks.Add(task);
                }
            }

            state.IsPending = tasks.Exists(task => !task.IsCompleted);
            if (state.IsPending)
            {
                editContext.NotifyValidationStateChanged();
            }

            var faulted = false;
            foreach (var task in tasks)
            {
                try
                {
                    await task;
                }
                catch (Exception) when (task.IsFaulted || task.IsCanceled)
                {
                    // Match NET11: task faults invalidate the pass, but all started tasks must settle.
                    faulted = true;
                }
            }

            cancellationToken.ThrowIfCancellationRequested();
            state.IsFaulted = faulted;
            return !faulted && !editContext.GetValidationMessages().Any();
        }
        finally
        {
            scope.IsActive = false;
            CurrentScope.Value = previousScope;
            state.IsPending = false;
            state.Semaphore.Release();
            editContext.NotifyValidationStateChanged();
        }
    }

    /// <summary>
    /// <para lang="zh">获得当前窗体验证是否正在等待异步任务</para>
    /// <para lang="en">Gets whether the current form validation is awaiting async tasks</para>
    /// </summary>
    /// <param name="editContext"><para lang="zh">编辑上下文</para><para lang="en">The edit context</para></param>
    public static bool IsValidationPending(this EditContext editContext)
    {
        return States.TryGetValue(editContext, out var state) && state.IsPending;
    }

    /// <summary>
    /// <para lang="zh">获得最近一次窗体异步验证是否发生异常</para>
    /// <para lang="en">Gets whether the most recent async form validation faulted</para>
    /// </summary>
    /// <param name="editContext"><para lang="zh">编辑上下文</para><para lang="en">The edit context</para></param>
    public static bool IsValidationFaulted(this EditContext editContext)
    {
        return States.TryGetValue(editContext, out var state) && state.IsFaulted;
    }

    /// <summary>
    /// <para lang="zh">立即执行字段异步验证并取消该字段上一次验证</para>
    /// <para lang="en">Immediately starts async field validation and cancels the previous validation for that field</para>
    /// </summary>
    /// <param name="editContext"><para lang="zh">编辑上下文</para><para lang="en">The edit context</para></param>
    /// <param name="fieldIdentifier"><para lang="zh">字段标识</para><para lang="en">The field identifier</para></param>
    /// <param name="validator"><para lang="zh">异步验证委托</para><para lang="en">The async validation delegate</para></param>
    /// <remarks>
    /// <para lang="zh">取消令牌由编辑上下文管理，验证任务结束后释放。任务异常通过字段 IsValidationFaulted 和验证状态通知报告；委托同步异常直接传播。</para>
    /// <para lang="en">The edit context owns the cancellation token source and disposes it after the task settles. Task faults are reported through the field's IsValidationFaulted state and validation state notifications; synchronous delegate exceptions propagate.</para>
    /// </remarks>
    public static void RegisterAsyncFieldValidator(this EditContext editContext, in FieldIdentifier fieldIdentifier, Func<CancellationToken, Task> validator)
    {
        var state = GetState(editContext);
        var operation = new FieldValidationOperation();
        FieldValidationState fieldState;
        FieldValidationOperation? previousOperation;
        bool previouslyChanged;
        lock (state.FieldLock)
        {
            if (!state.Fields.TryGetValue(fieldIdentifier, out fieldState!))
            {
                fieldState = new();
                state.Fields.Add(fieldIdentifier, fieldState);
            }
            previousOperation = fieldState.Operation;
            previouslyChanged = previousOperation != null || fieldState.IsFaulted;
            fieldState.Operation = operation;
            fieldState.IsFaulted = false;
        }

        Task task;
        try
        {
            previousOperation?.Cancel();
            task = validator(operation.Token)
                ?? throw new InvalidOperationException("The async validator returned a null task.");
        }
        catch
        {
            operation.Complete();
            if (SettleFieldValidation(state, fieldState, operation, false) && previouslyChanged)
            {
                editContext.NotifyValidationStateChanged();
            }
            throw;
        }

        _ = ObserveFieldValidationAsync(editContext, state, fieldState, operation, task, previouslyChanged);
    }

    /// <summary>
    /// <para lang="zh">获得指定字段是否有尚未完成的异步验证</para>
    /// <para lang="en">Gets whether the specified field has an unsettled async validation</para>
    /// </summary>
    /// <param name="editContext"><para lang="zh">编辑上下文</para><para lang="en">The edit context</para></param>
    /// <param name="fieldIdentifier"><para lang="zh">字段标识</para><para lang="en">The field identifier</para></param>
    public static bool IsValidationPending(this EditContext editContext, in FieldIdentifier fieldIdentifier)
    {
        if (States.TryGetValue(editContext, out var state))
        {
            lock (state.FieldLock)
            {
                return state.Fields.TryGetValue(fieldIdentifier, out var field) && field.Operation != null;
            }
        }
        return false;
    }

    /// <summary>
    /// <para lang="zh">获得表达式指定字段是否有尚未完成的异步验证</para>
    /// <para lang="en">Gets whether the field identified by the expression has an unsettled async validation</para>
    /// </summary>
    /// <typeparam name="TField"><para lang="zh">字段类型</para><para lang="en">The field type</para></typeparam>
    /// <param name="editContext"><para lang="zh">编辑上下文</para><para lang="en">The edit context</para></param>
    /// <param name="accessor"><para lang="zh">字段表达式</para><para lang="en">The field expression</para></param>
    public static bool IsValidationPending<TField>(this EditContext editContext, Expression<Func<TField>> accessor)
        => editContext.IsValidationPending(FieldIdentifier.Create(accessor));

    /// <summary>
    /// <para lang="zh">获得指定字段最近一次异步验证是否发生异常</para>
    /// <para lang="en">Gets whether the specified field's most recent async validation faulted</para>
    /// </summary>
    /// <param name="editContext"><para lang="zh">编辑上下文</para><para lang="en">The edit context</para></param>
    /// <param name="fieldIdentifier"><para lang="zh">字段标识</para><para lang="en">The field identifier</para></param>
    public static bool IsValidationFaulted(this EditContext editContext, in FieldIdentifier fieldIdentifier)
    {
        if (States.TryGetValue(editContext, out var state))
        {
            lock (state.FieldLock)
            {
                return state.Fields.TryGetValue(fieldIdentifier, out var field) && field.IsFaulted;
            }
        }
        return false;
    }

    /// <summary>
    /// <para lang="zh">获得表达式指定字段最近一次异步验证是否发生异常</para>
    /// <para lang="en">Gets whether the field identified by the expression most recently faulted</para>
    /// </summary>
    /// <typeparam name="TField"><para lang="zh">字段类型</para><para lang="en">The field type</para></typeparam>
    /// <param name="editContext"><para lang="zh">编辑上下文</para><para lang="en">The edit context</para></param>
    /// <param name="accessor"><para lang="zh">字段表达式</para><para lang="en">The field expression</para></param>
    public static bool IsValidationFaulted<TField>(this EditContext editContext, Expression<Func<TField>> accessor)
        => editContext.IsValidationFaulted(FieldIdentifier.Create(accessor));

    internal static void CancelAsyncFieldValidations(this EditContext editContext)
    {
        if (States.TryGetValue(editContext, out var state))
        {
            var operations = new List<FieldValidationOperation>();
            var changed = false;
            lock (state.FieldLock)
            {
                foreach (var field in state.Fields.Values)
                {
                    if (field.Operation != null)
                    {
                        operations.Add(field.Operation);
                        field.Operation = null;
                        changed = true;
                    }
                    changed |= field.IsFaulted;
                    field.IsFaulted = false;
                }
            }
            foreach (var operation in operations)
            {
                operation.Cancel();
            }
            if (changed)
            {
                editContext.NotifyValidationStateChanged();
            }
        }
    }

    private static async Task ObserveFieldValidationAsync(EditContext editContext, ValidationState state, FieldValidationState field, FieldValidationOperation operation, Task task, bool previouslyChanged)
    {
        var completedSynchronously = task.IsCompleted;
        try
        {
            if (!completedSynchronously)
            {
                editContext.NotifyValidationStateChanged();
            }

            var faulted = false;
            try
            {
                await task;
            }
            catch (OperationCanceledException) when (operation.Token.IsCancellationRequested)
            {
            }
            catch (Exception) when (task.IsFaulted || task.IsCanceled)
            {
                faulted = true;
            }

            if (SettleFieldValidation(state, field, operation, faulted)
                && (!completedSynchronously || previouslyChanged || faulted))
            {
                editContext.NotifyValidationStateChanged();
            }
        }
        finally
        {
            operation.Complete();
        }
    }

    private static bool SettleFieldValidation(ValidationState state, FieldValidationState field, FieldValidationOperation operation, bool faulted)
    {
        lock (state.FieldLock)
        {
            // An older task must not clear the pending state or fault of its replacement.
            if (!ReferenceEquals(field.Operation, operation))
            {
                return false;
            }
            field.Operation = null;
            field.IsFaulted = faulted;
            return true;
        }
    }

    internal static void AddAsyncValidator(Func<CancellationToken, Task> validator)
    {
        var scope = CurrentScope.Value;
        if (scope == null || !scope.IsCollecting)
        {
            throw new InvalidOperationException("Asynchronous validation requires an EditContext.ValidateAsync call. Register validators synchronously in OnValidationRequested.");
        }
        scope.Validators.Add(validator);
    }

    private static ValidationState GetState(EditContext editContext) => States.GetValue(editContext, static _ => new());

    private sealed class ValidationState
    {
        public SemaphoreSlim Semaphore { get; } = new(1, 1);

#if NET9_0_OR_GREATER
        public Lock FieldLock { get; } = new();
#else
        public object FieldLock { get; } = new();
#endif

        public Dictionary<FieldIdentifier, FieldValidationState> Fields { get; } = [];

        public bool IsPending { get; set; }

        public bool IsFaulted { get; set; }
    }

    private sealed class FieldValidationState
    {
        public FieldValidationOperation? Operation { get; set; }

        public bool IsFaulted { get; set; }
    }

    private sealed class FieldValidationOperation
    {
        private CancellationTokenSource? _tokenSource = new();

#if NET9_0_OR_GREATER
        private readonly Lock _lock = new();
#else
        private readonly object _lock = new();
#endif

        public CancellationToken Token { get; }

        public FieldValidationOperation() => Token = _tokenSource.Token;

        public void Cancel()
        {
            lock (_lock)
            {
                if (_tokenSource != null)
                {
                    _tokenSource.Cancel();
                }
            }
        }

        public void Complete()
        {
            lock (_lock)
            {
                if (_tokenSource != null)
                {
                    _tokenSource.Dispose();
                    _tokenSource = null;
                }
            }
        }
    }

    private sealed class ValidationScope(EditContext editContext, ValidationScope? parent)
    {
        public EditContext EditContext { get; } = editContext;

        public ValidationScope? Parent { get; } = parent;

        public List<Func<CancellationToken, Task>> Validators { get; } = [];

        public bool IsCollecting { get; set; } = true;

        public bool IsActive { get; set; } = true;
    }
}
#endif
