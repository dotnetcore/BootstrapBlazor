// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.AspNetCore.Components.Forms;

namespace UnitTest.Extensions;

public class EditContextExtensionsTest
{
    [Fact]
    public async Task ValidateAsync_SynchronousMessages()
    {
        var context = new EditContext(new object());
        var messages = new ValidationMessageStore(context);
        context.OnValidationRequested += (_, _) => messages.Add(context.Field("Name"), "Required");

        Assert.False(context.IsValidationPending());
        Assert.False(context.IsValidationFaulted());
        Assert.False(await context.ValidateAsync(CancellationToken.None));
        Assert.False(context.IsValidationFaulted());
    }

    [Fact]
    public async Task ValidateAsync_AwaitsAllValidators()
    {
        var context = new EditContext(new object());
        var first = new TaskCompletionSource(TaskCreationOptions.RunContinuationsAsynchronously);
        var second = new TaskCompletionSource(TaskCreationOptions.RunContinuationsAsynchronously);
        var messages = new ValidationMessageStore(context);
        var calls = new List<string>();
        var pendingStates = new List<bool>();
        context.OnValidationStateChanged += (_, _) => pendingStates.Add(context.IsValidationPending());
        context.OnValidationRequested += (_, args) =>
        {
            calls.Add("register");
            args.AddAsyncValidator(async _ =>
            {
                calls.Add("first");
                await first.Task;
                messages.Add(context.Field("Name"), "Invalid");
            });
            args.AddAsyncValidator(_ =>
            {
                calls.Add("second");
                return second.Task;
            });
        };

        var validation = context.ValidateAsync(CancellationToken.None);

        Assert.Equal(["register", "first", "second"], calls);
        Assert.True(context.IsValidationPending());
        Assert.False(validation.IsCompleted);
        first.SetResult();
        Assert.False(validation.IsCompleted);
        second.SetResult();

        Assert.False(await validation);
        Assert.False(context.IsValidationPending());
        Assert.Equal([true, false], pendingStates);
        Assert.Equal(["Invalid"], context.GetValidationMessages());
    }

    [Fact]
    public async Task ValidateAsync_UsesFinalMessages()
    {
        var context = new EditContext(new object());
        var messages = new ValidationMessageStore(context);
        var count = 0;
        context.OnValidationRequested += (_, args) =>
        {
            messages.Add(context.Field("Name"), "Old error");
            args.AddAsyncValidator(_ =>
            {
                count++;
                messages.Clear();
                return Task.CompletedTask;
            });
        };

        Assert.True(await context.ValidateAsync(CancellationToken.None));
        Assert.True(await context.ValidateAsync(CancellationToken.None));
        Assert.Equal(2, count);
    }

    [Fact]
    public async Task ValidateAsync_IsolatesForms()
    {
        var first = new EditContext(new object());
        var second = new EditContext(new object());
        var firstCompletion = new TaskCompletionSource(TaskCreationOptions.RunContinuationsAsynchronously);
        var secondCompletion = new TaskCompletionSource(TaskCreationOptions.RunContinuationsAsynchronously);
        var firstMessages = new ValidationMessageStore(first);
        using var firstCancellation = new CancellationTokenSource();
        using var secondCancellation = new CancellationTokenSource();
        first.OnValidationRequested += (_, args) => args.AddAsyncValidator(async token =>
        {
            Assert.Equal(firstCancellation.Token, token);
            await firstCompletion.Task;
            firstMessages.Add(first.Field("Name"), "First form error");
        });
        second.OnValidationRequested += (_, args) => args.AddAsyncValidator(token =>
        {
            Assert.Equal(secondCancellation.Token, token);
            return secondCompletion.Task;
        });

        var firstValidation = first.ValidateAsync(firstCancellation.Token);
        var secondValidation = second.ValidateAsync(secondCancellation.Token);
        secondCompletion.SetResult();

        Assert.True(await secondValidation);
        Assert.False(firstValidation.IsCompleted);
        firstCompletion.SetResult();
        Assert.False(await firstValidation);
        Assert.Empty(second.GetValidationMessages());
    }

    [Fact]
    public async Task ValidateAsync_RestoresNestedScope()
    {
        var outer = new EditContext(new object());
        var inner = new EditContext(new object());
        var completion = new TaskCompletionSource(TaskCreationOptions.RunContinuationsAsynchronously);
        var calls = new List<string>();
        inner.OnValidationRequested += (_, args) => args.AddAsyncValidator(_ =>
        {
            calls.Add("inner");
            return completion.Task;
        });
        outer.OnValidationRequested += (_, args) =>
        {
            var innerValidation = inner.ValidateAsync(CancellationToken.None);
            args.AddAsyncValidator(_ =>
            {
                calls.Add("outer");
                return innerValidation;
            });
        };

        var validation = outer.ValidateAsync(CancellationToken.None);
        Assert.Equal(["inner", "outer"], calls);
        Assert.False(validation.IsCompleted);
        completion.SetResult();
        Assert.True(await validation);
    }

    [Fact]
    public async Task ValidateAsync_FaultWaitsForOtherValidators()
    {
        var context = new EditContext(new object());
        var completion = new TaskCompletionSource(TaskCreationOptions.RunContinuationsAsynchronously);
        var shouldFail = true;
        context.OnValidationRequested += (_, args) =>
        {
            args.AddAsyncValidator(_ => shouldFail
                ? Task.FromException(new InvalidOperationException("Validation failed"))
                : Task.CompletedTask);
            args.AddAsyncValidator(_ => completion.Task);
        };

        var validation = context.ValidateAsync(CancellationToken.None);
        Assert.False(validation.IsCompleted);
        completion.SetResult();
        Assert.False(await validation);
        Assert.True(context.IsValidationFaulted());
        Assert.False(context.IsValidationPending());
        Assert.Empty(context.GetValidationMessages());

        shouldFail = false;
        Assert.True(await context.ValidateAsync(CancellationToken.None));
        Assert.False(context.IsValidationFaulted());
    }

    [Fact]
    public async Task ValidateAsync_CancellationWaitsForOtherValidators()
    {
        var context = new EditContext(new object());
        using var cancellation = new CancellationTokenSource();
        var completion = new TaskCompletionSource(TaskCreationOptions.RunContinuationsAsynchronously);
        context.OnValidationRequested += (_, args) =>
        {
            args.AddAsyncValidator(token =>
            {
                Assert.Equal(cancellation.Token, token);
                return Task.Delay(Timeout.InfiniteTimeSpan, token);
            });
            args.AddAsyncValidator(_ => completion.Task);
        };

        var validation = context.ValidateAsync(cancellation.Token);
        cancellation.Cancel();
        Assert.False(validation.IsCompleted);
        completion.SetResult();

        await Assert.ThrowsAnyAsync<OperationCanceledException>(() => validation);
        Assert.False(context.IsValidationPending());
        Assert.False(context.IsValidationFaulted());
    }

    [Fact]
    public async Task ValidateAsync_PreCancelled()
    {
        var context = new EditContext(new object());
        using var cancellation = new CancellationTokenSource();
        cancellation.Cancel();

        await Assert.ThrowsAnyAsync<OperationCanceledException>(() => context.ValidateAsync(cancellation.Token));
        Assert.False(context.IsValidationPending());
    }

    [Fact]
    public async Task ValidateAsync_UnrelatedCancellationFaults()
    {
        var context = new EditContext(new object());
        using var cancellation = new CancellationTokenSource();
        cancellation.Cancel();
        context.OnValidationRequested += (_, args) =>
            args.AddAsyncValidator(_ => Task.FromCanceled(cancellation.Token));

        Assert.False(await context.ValidateAsync(CancellationToken.None));
        Assert.True(context.IsValidationFaulted());

        await Assert.ThrowsAnyAsync<OperationCanceledException>(() => context.ValidateAsync(cancellation.Token));
        Assert.True(context.IsValidationFaulted());
    }

    [Fact]
    public async Task ValidateAsync_HandlerException()
    {
        var context = new EditContext(new object());
        var called = false;
        EventHandler<ValidationRequestedEventArgs> handler = (_, args) =>
        {
            args.AddAsyncValidator(_ =>
            {
                called = true;
                return Task.CompletedTask;
            });
            throw new InvalidOperationException("Handler failed");
        };
        context.OnValidationRequested += handler;

        await Assert.ThrowsAsync<InvalidOperationException>(() => context.ValidateAsync(CancellationToken.None));
        Assert.False(called);
        Assert.False(context.IsValidationPending());
        context.OnValidationRequested -= handler;
        Assert.True(await context.ValidateAsync(CancellationToken.None));
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public async Task ValidateAsync_InvalidDelegate(bool throws)
    {
        var context = new EditContext(new object());
        EventHandler<ValidationRequestedEventArgs> handler = (_, args) =>
            args.AddAsyncValidator(_ => throws ? throw new InvalidOperationException("Delegate failed") : null!);
        context.OnValidationRequested += handler;

        await Assert.ThrowsAsync<InvalidOperationException>(() => context.ValidateAsync(CancellationToken.None));
        Assert.False(context.IsValidationPending());
        context.OnValidationRequested -= handler;
        Assert.True(await context.ValidateAsync(CancellationToken.None));
    }

    [Fact]
    public void AddAsyncValidator_OutsideValidation()
    {
        Assert.Throws<InvalidOperationException>(() =>
            ValidationRequestedEventArgs.Empty.AddAsyncValidator(_ => Task.CompletedTask));
        Assert.Throws<ArgumentNullException>(() =>
            ValidationRequestedEventArgs.Empty.AddAsyncValidator(null!));
    }

#if !NET11_0_OR_GREATER
    [Fact]
    public async Task ValidateAsync_SerializesSameContext()
    {
        var context = new EditContext(new object());
        var completion = new TaskCompletionSource(TaskCreationOptions.RunContinuationsAsynchronously);
        var count = 0;
        context.OnValidationRequested += (_, args) => args.AddAsyncValidator(_ =>
        {
            count++;
            return completion.Task;
        });

        var first = context.ValidateAsync(CancellationToken.None);
        var second = context.ValidateAsync(CancellationToken.None);
        Assert.Equal(1, count);
        completion.SetResult();

        Assert.True(await first);
        Assert.True(await second);
        Assert.Equal(2, count);
    }

    [Fact]
    public async Task ValidateAsync_CancelsQueuedPass()
    {
        var context = new EditContext(new object());
        var completion = new TaskCompletionSource(TaskCreationOptions.RunContinuationsAsynchronously);
        var count = 0;
        using var cancellation = new CancellationTokenSource();
        context.OnValidationRequested += (_, args) => args.AddAsyncValidator(_ =>
        {
            count++;
            return completion.Task;
        });

        var first = context.ValidateAsync(CancellationToken.None);
        var second = context.ValidateAsync(cancellation.Token);
        cancellation.Cancel();

        await Assert.ThrowsAnyAsync<OperationCanceledException>(() => second);
        Assert.Equal(1, count);
        completion.SetResult();
        Assert.True(await first);
    }

    [Fact]
    public async Task ValidateAsync_RejectsReentrancy()
    {
        var context = new EditContext(new object());
        Task<bool>? nested = null;
        context.OnValidationRequested += (_, _) => nested = context.ValidateAsync(CancellationToken.None);

        Assert.True(await context.ValidateAsync(CancellationToken.None));
        Assert.NotNull(nested);
        await Assert.ThrowsAsync<InvalidOperationException>(() => nested);
    }

    [Fact]
    public async Task AddAsyncValidator_RejectsLateRegistration()
    {
        var context = new EditContext(new object());
        var completion = new TaskCompletionSource(TaskCreationOptions.RunContinuationsAsynchronously);
        Task? registration = null;
        context.OnValidationRequested += (_, args) => registration = RegisterAsync(args);

        Assert.True(await context.ValidateAsync(CancellationToken.None));
        completion.SetResult();
        Assert.NotNull(registration);
        await Assert.ThrowsAsync<InvalidOperationException>(() => registration);

        async Task RegisterAsync(ValidationRequestedEventArgs args)
        {
            await completion.Task;
            args.AddAsyncValidator(_ => Task.CompletedTask);
        }
    }

    [Fact]
    public void AddAsyncValidator_SynchronousValidationThrows()
    {
        var context = new EditContext(new object());
        context.OnValidationRequested += (_, args) => args.AddAsyncValidator(_ => Task.CompletedTask);

        Assert.Throws<InvalidOperationException>(() => context.Validate());
    }

    [Fact]
    public async Task NullArguments()
    {
        await Assert.ThrowsAsync<ArgumentNullException>(() => EditContextExtensions.ValidateAsync(null!, CancellationToken.None));
        Assert.Throws<ArgumentNullException>(() => EditContextExtensions.IsValidationPending(null!));
        Assert.Throws<ArgumentNullException>(() => EditContextExtensions.IsValidationFaulted(null!));
        Assert.Throws<ArgumentNullException>(() =>
            ValidationRequestedEventArgsExtensions.AddAsyncValidator(null!, _ => Task.CompletedTask));
    }
#endif
}
