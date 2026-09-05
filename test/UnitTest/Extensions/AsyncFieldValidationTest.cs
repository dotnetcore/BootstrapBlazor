// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.AspNetCore.Components.Forms;

namespace UnitTest.Extensions;

public class AsyncFieldValidationTest
{
    [Fact]
    public void CompletedValidation()
    {
        var model = new Foo();
        var context = new EditContext(model);
        var field = context.Field(nameof(model.Name));
        var notifications = 0;
        context.OnValidationStateChanged += (_, _) => notifications++;

        Assert.False(context.IsValidationPending(field));
        Assert.False(context.IsValidationFaulted(field));
        Assert.False(context.IsValidationPending(() => model.Name));
        Assert.False(context.IsValidationFaulted(() => model.Name));

        context.RegisterAsyncFieldValidator(field, _ => Task.CompletedTask);

        Assert.False(context.IsValidationPending(field));
        Assert.False(context.IsValidationFaulted(field));
        Assert.Equal(0, notifications);
    }

    [Fact]
    public void IsValidationFaulted_UnregisteredField()
    {
        var context = new EditContext(new object());
        var registeredField = context.Field("Name");
        var unregisteredField = context.Field("Age");

        Assert.False(context.IsValidationFaulted(unregisteredField));

        context.RegisterAsyncFieldValidator(registeredField,
            _ => Task.FromException(new InvalidOperationException("Validation failed")));

        Assert.True(context.IsValidationFaulted(registeredField));
        Assert.False(context.IsValidationFaulted(unregisteredField));
    }

    [Fact]
    public async Task PendingValidation()
    {
        var model = new Foo();
        var context = new EditContext(model);
        var field = context.Field(nameof(model.Name));
        var completion = new TaskCompletionSource(TaskCreationOptions.RunContinuationsAsynchronously);
        var states = new List<bool>();
        context.OnValidationStateChanged += (_, _) => states.Add(context.IsValidationPending(field));
        var settled = ObserveCompletion(context, field);
        context.RegisterAsyncFieldValidator(field, _ => completion.Task);

        Assert.True(context.IsValidationPending(field));
        Assert.True(context.IsValidationPending(() => model.Name));
        Assert.False(context.IsValidationPending());
        completion.SetResult();
        await settled.Task.WaitAsync(CancellationToken.None);

        Assert.False(context.IsValidationPending(field));
        Assert.False(context.IsValidationFaulted(field));
        Assert.Equal([true, false], states);
    }

    [Fact]
    public async Task ReplacementCancelsPreviousValidation()
    {
        var context = new EditContext(new object());
        var field = context.Field("Name");
        var previousCompletion = new TaskCompletionSource(TaskCreationOptions.RunContinuationsAsynchronously);
        var completion = new TaskCompletionSource(TaskCreationOptions.RunContinuationsAsynchronously);
        var settled = ObserveCompletion(context, field);
        CancellationToken previousToken = default;
        context.RegisterAsyncFieldValidator(field, token =>
        {
            previousToken = token;
            return previousCompletion.Task;
        });
        context.RegisterAsyncFieldValidator(field, _ => completion.Task);

        Assert.True(previousToken.IsCancellationRequested);
        Assert.True(context.IsValidationPending(field));
        // The old task still owns a usable token source until it settles.
        Assert.True(previousToken.WaitHandle.WaitOne(0));
        previousCompletion.SetException(new InvalidOperationException("Stale failure"));
        completion.SetResult();
        await settled.Task.WaitAsync(CancellationToken.None);

        Assert.False(context.IsValidationPending(field));
        Assert.False(context.IsValidationFaulted(field));
    }

    [Fact]
    public async Task FieldsAndContextsAreIndependent()
    {
        var first = new EditContext(new object());
        var second = new EditContext(first.Model);
        var name = first.Field("Name");
        var age = first.Field("Age");
        var completion = new TaskCompletionSource(TaskCreationOptions.RunContinuationsAsynchronously);
        var nameSettled = ObserveCompletion(first, name);
        var ageSettled = ObserveCompletion(first, age);
        var secondSettled = ObserveCompletion(second, name);
        var tokens = new List<CancellationToken>();
        Task Validate(CancellationToken token)
        {
            tokens.Add(token);
            return completion.Task;
        }

        first.RegisterAsyncFieldValidator(name, Validate);
        first.RegisterAsyncFieldValidator(age, Validate);
        second.RegisterAsyncFieldValidator(name, Validate);

        Assert.All(tokens, token => Assert.False(token.IsCancellationRequested));
        Assert.Equal(3, tokens.Distinct().Count());
        completion.SetResult();
        await Task.WhenAll(nameSettled.Task, ageSettled.Task, secondSettled.Task);
        Assert.False(first.IsValidationPending(name));
        Assert.False(first.IsValidationPending(age));
        Assert.False(second.IsValidationPending(name));
    }

    [Fact]
    public async Task FaultIsObservableAndClearedBySuccess()
    {
        var model = new Foo();
        var context = new EditContext(model);
        var field = context.Field(nameof(model.Name));
        var completion = new TaskCompletionSource(TaskCreationOptions.RunContinuationsAsynchronously);
        var settled = ObserveCompletion(context, field);
        context.RegisterAsyncFieldValidator(field, _ => completion.Task);
        completion.SetException(new InvalidOperationException("Validation failed"));
        await settled.Task.WaitAsync(CancellationToken.None);

        Assert.True(context.IsValidationFaulted(field));
        Assert.True(context.IsValidationFaulted(() => model.Name));
        Assert.False(context.IsValidationFaulted());
        context.RegisterAsyncFieldValidator(field, _ => Task.CompletedTask);
        Assert.False(context.IsValidationFaulted(field));
    }

    [Fact]
    public void UnrelatedCancellationFaults()
    {
        var context = new EditContext(new object());
        var field = context.Field("Name");
        using var cancellation = new CancellationTokenSource();
        cancellation.Cancel();
        context.RegisterAsyncFieldValidator(field, _ => Task.FromCanceled(cancellation.Token));

        Assert.False(context.IsValidationPending(field));
        Assert.True(context.IsValidationFaulted(field));
    }

    [Fact]
    public async Task OwnedCancellationDoesNotFaultReplacement()
    {
        var context = new EditContext(new object());
        var field = context.Field("Name");
        Task? cancelledTask = null;
        context.RegisterAsyncFieldValidator(field, token =>
            cancelledTask = Task.Delay(Timeout.InfiniteTimeSpan, token));

        context.RegisterAsyncFieldValidator(field, _ => Task.CompletedTask);

        Assert.NotNull(cancelledTask);
        await Assert.ThrowsAnyAsync<OperationCanceledException>(() => cancelledTask);
        Assert.False(context.IsValidationPending(field));
        Assert.False(context.IsValidationFaulted(field));
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public async Task InvalidDelegateClearsPreviousValidation(bool throws)
    {
        var context = new EditContext(new object());
        var field = context.Field("Name");
        var completion = new TaskCompletionSource(TaskCreationOptions.RunContinuationsAsynchronously);
        CancellationToken previousToken = default;
        context.RegisterAsyncFieldValidator(field, token =>
        {
            previousToken = token;
            return completion.Task;
        });

        Assert.Throws<InvalidOperationException>(() => context.RegisterAsyncFieldValidator(field,
            _ => throws ? throw new InvalidOperationException("Delegate failed") : null!));

        Assert.True(previousToken.IsCancellationRequested);
        Assert.False(context.IsValidationPending(field));
        Assert.False(context.IsValidationFaulted(field));
        completion.SetResult();
        await completion.Task;
        context.RegisterAsyncFieldValidator(field, _ => Task.CompletedTask);
        Assert.False(context.IsValidationPending(field));
    }

    [Fact]
    public async Task FormValidationCancelsFieldsAndClearsFaults()
    {
        var context = new EditContext(new object());
        var name = context.Field("Name");
        var age = context.Field("Age");
        var completion = new TaskCompletionSource(TaskCreationOptions.RunContinuationsAsynchronously);
        CancellationToken fieldToken = default;
        context.RegisterAsyncFieldValidator(name, token =>
        {
            fieldToken = token;
            return completion.Task;
        });
        context.RegisterAsyncFieldValidator(age, _ => Task.FromException(new InvalidOperationException("Old failure")));
        Assert.True(context.IsValidationFaulted(age));
        context.OnValidationRequested += (_, args) =>
        {
            Assert.True(fieldToken.IsCancellationRequested);
            Assert.False(context.IsValidationPending(name));
            Assert.False(context.IsValidationFaulted(age));
            args.AddAsyncValidator(_ => Task.CompletedTask);
        };

        Assert.True(await context.ValidateAsync(CancellationToken.None));
        Assert.True(fieldToken.WaitHandle.WaitOne(0));
        completion.SetResult();
        await completion.Task;
        Assert.False(context.IsValidationPending(name));
        Assert.False(context.IsValidationFaulted(name));
    }

    [Fact]
    public void NullContext()
    {
        var field = new FieldIdentifier(new object(), "Name");
        Assert.Throws<ArgumentNullException>(() => EditContextExtensions.RegisterAsyncFieldValidator(null!, field, _ => Task.CompletedTask));
        Assert.Throws<ArgumentNullException>(() => EditContextExtensions.IsValidationPending(null!, field));
        Assert.Throws<ArgumentNullException>(() => EditContextExtensions.IsValidationFaulted(null!, field));
    }

    private static TaskCompletionSource ObserveCompletion(EditContext context, FieldIdentifier field)
    {
        var settled = new TaskCompletionSource(TaskCreationOptions.RunContinuationsAsynchronously);
        var wasPending = false;
        context.OnValidationStateChanged += (_, _) =>
        {
            if (context.IsValidationPending(field))
            {
                wasPending = true;
            }
            else if (wasPending)
            {
                settled.TrySetResult();
            }
        };
        return settled;
    }
}
