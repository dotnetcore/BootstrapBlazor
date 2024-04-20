// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.JSInterop;

namespace UnitTest.Utils;

public class JSModuleTest
{
    [Fact]
    public void JSModule_Error()
    {
        var js = new MockJSObjectReference();
        var module = new JSModule(js);
        Assert.NotNull(module);

        Assert.Throws<ArgumentNullException>(() => new JSModule(null));
    }

    [Fact]
    public async Task InvokeVoidAsync_Ok()
    {
        var js = new MockJSObjectReference();
        var module = new JSModule(js);
        await module.InvokeVoidAsync("Test.init", "bb_id");
        await module.InvokeVoidAsync("Test.init", TimeSpan.Zero, "bb_id");
        await module.InvokeVoidAsync("Test.init", Timeout.InfiniteTimeSpan, "bb_id");
        await module.InvokeVoidAsync("Test.init", CancellationToken.None, "bb_id");
        await module.InvokeAsync<object>("Test.init", "bb_id");
        await module.InvokeAsync<object>("Test.init", TimeSpan.Zero, "bb_id");
        await module.InvokeAsync<object>("Test.init", Timeout.InfiniteTimeSpan, "bb_id");
        await module.InvokeAsync<object>("Test.init", CancellationToken.None, "bb_id");
    }

    [Fact]
    public async Task Dispose_Ok()
    {
        var js = new MockJSObjectReference();
        var module = new JSModule(js);
        await module.DisposeAsync();
    }

    [Fact]
    public async Task Dispose_Error()
    {
        var js = new MockErrorJSObjectReference();
        var module = new JSModule(js);
        await module.DisposeAsync();
    }

    [Fact]
    public async Task JSModule_JSDisconnectedException()
    {
        var js = new MockJSDisconnectedObjectReference();
        var module = new JSModule(js);
        await module.InvokeVoidAsync("test");
        await module.InvokeAsync<int>("test");
    }

    [Fact]
    public async Task JSModule_JSException()
    {
        var js = new MockJSExceptionObjectReference();
        var module = new JSModule(js);
        await Assert.ThrowsAnyAsync<JSException>(async () => await module.InvokeVoidAsync("test"));
        await Assert.ThrowsAnyAsync<JSException>(async () => await module.InvokeAsync<int>("test"));
    }

    [Fact]
    public async Task JSModule_AggregateException()
    {
        var js = new MockAggregateExceptionObjectReference();
        var module = new JSModule(js);
        await Assert.ThrowsAnyAsync<AggregateException>(async () => await module.InvokeVoidAsync("test"));
        await Assert.ThrowsAnyAsync<AggregateException>(async () => await module.InvokeAsync<int>("test"));
    }

    [Fact]
    public async Task JSModule_InvalidOperationException()
    {
        var js = new MockInvalidOperationExceptionObjectReference();
        var module = new JSModule(js);
        await Assert.ThrowsAnyAsync<InvalidOperationException>(async () => await module.InvokeVoidAsync("test"));
        await Assert.ThrowsAnyAsync<InvalidOperationException>(async () => await module.InvokeAsync<int>("test"));
    }

    [Fact]
    public async Task JSModule_ObjectDisposed_Ok()
    {
        var js = new MockObjectDisposedExceptionObjectReference();
        var module = new JSModule(js);
        await module.InvokeVoidAsync("test");
        await module.InvokeAsync<int>("test");
    }

    [Fact]
    public async Task JSModule_TaskCanceledException()
    {
        var js = new MockTaskCanceledObjectReference();
        var module = new JSModule(js);
        await module.InvokeVoidAsync("test");
        await module.InvokeAsync<int>("test");
    }

    private class MockErrorJSObjectReference : MockJSObjectReference
    {
        protected override ValueTask DisposeAsyncCore(bool disposing)
        {
            throw new InvalidOperationException();
        }

        public override ValueTask<TValue> InvokeAsync<TValue>(string identifier, object?[]? args)
        {
            throw new InvalidOperationException();
        }
    }

    private class MockJSObjectReference : IJSObjectReference
    {
        protected virtual ValueTask DisposeAsyncCore(bool disposing)
        {
            return ValueTask.CompletedTask;
        }

        public async ValueTask DisposeAsync()
        {
            await DisposeAsyncCore(true);
            GC.SuppressFinalize(this);
        }

        public virtual ValueTask<TValue> InvokeAsync<TValue>(string identifier, object?[]? args)
        {
            return ValueTask.FromResult<TValue>(default!);
        }

        public ValueTask<TValue> InvokeAsync<TValue>(string identifier, CancellationToken cancellationToken, object?[]? args)
        {
            return ValueTask.FromResult<TValue>(default!);
        }
    }

    private class MockJSDisconnectedObjectReference : IJSObjectReference
    {
        public ValueTask DisposeAsync() => throw new JSDisconnectedException("Test");

        public ValueTask<TValue> InvokeAsync<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors | DynamicallyAccessedMemberTypes.PublicFields | DynamicallyAccessedMemberTypes.PublicProperties)] TValue>(string identifier, object?[]? args) => throw new JSDisconnectedException("Test");

        public ValueTask<TValue> InvokeAsync<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors | DynamicallyAccessedMemberTypes.PublicFields | DynamicallyAccessedMemberTypes.PublicProperties)] TValue>(string identifier, CancellationToken cancellationToken, object?[]? args) => throw new JSDisconnectedException("Test");
    }

    private class MockTaskCanceledObjectReference : IJSObjectReference
    {
        public ValueTask DisposeAsync() => throw new TaskCanceledException("Test");

        public ValueTask<TValue> InvokeAsync<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors | DynamicallyAccessedMemberTypes.PublicFields | DynamicallyAccessedMemberTypes.PublicProperties)] TValue>(string identifier, object?[]? args) => throw new TaskCanceledException("Test");

        public ValueTask<TValue> InvokeAsync<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors | DynamicallyAccessedMemberTypes.PublicFields | DynamicallyAccessedMemberTypes.PublicProperties)] TValue>(string identifier, CancellationToken cancellationToken, object?[]? args) => throw new TaskCanceledException("Test");
    }

    private class MockJSExceptionObjectReference : IJSObjectReference
    {
        public ValueTask DisposeAsync() => throw new TaskCanceledException("Test");

        public ValueTask<TValue> InvokeAsync<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors | DynamicallyAccessedMemberTypes.PublicFields | DynamicallyAccessedMemberTypes.PublicProperties)] TValue>(string identifier, object?[]? args) => throw new JSException("Test");

        public ValueTask<TValue> InvokeAsync<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors | DynamicallyAccessedMemberTypes.PublicFields | DynamicallyAccessedMemberTypes.PublicProperties)] TValue>(string identifier, CancellationToken cancellationToken, object?[]? args) => throw new JSException("Test");
    }

    private class MockAggregateExceptionObjectReference : IJSObjectReference
    {
        public ValueTask DisposeAsync() => throw new TaskCanceledException("Test");

        public ValueTask<TValue> InvokeAsync<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors | DynamicallyAccessedMemberTypes.PublicFields | DynamicallyAccessedMemberTypes.PublicProperties)] TValue>(string identifier, object?[]? args) => throw new AggregateException("Test");

        public ValueTask<TValue> InvokeAsync<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors | DynamicallyAccessedMemberTypes.PublicFields | DynamicallyAccessedMemberTypes.PublicProperties)] TValue>(string identifier, CancellationToken cancellationToken, object?[]? args) => throw new AggregateException("Test");
    }

    private class MockInvalidOperationExceptionObjectReference : IJSObjectReference
    {
        public ValueTask DisposeAsync() => throw new TaskCanceledException("Test");

        public ValueTask<TValue> InvokeAsync<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors | DynamicallyAccessedMemberTypes.PublicFields | DynamicallyAccessedMemberTypes.PublicProperties)] TValue>(string identifier, object?[]? args) => throw new InvalidOperationException("Test");

        public ValueTask<TValue> InvokeAsync<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors | DynamicallyAccessedMemberTypes.PublicFields | DynamicallyAccessedMemberTypes.PublicProperties)] TValue>(string identifier, CancellationToken cancellationToken, object?[]? args) => throw new InvalidOperationException("Test");
    }

    private class MockObjectDisposedExceptionObjectReference : IJSObjectReference
    {
        public ValueTask DisposeAsync() => throw new ObjectDisposedException("Test");

        public ValueTask<TValue> InvokeAsync<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors | DynamicallyAccessedMemberTypes.PublicFields | DynamicallyAccessedMemberTypes.PublicProperties)] TValue>(string identifier, object?[]? args) => throw new ObjectDisposedException("Test");

        public ValueTask<TValue> InvokeAsync<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors | DynamicallyAccessedMemberTypes.PublicFields | DynamicallyAccessedMemberTypes.PublicProperties)] TValue>(string identifier, CancellationToken cancellationToken, object?[]? args) => throw new ObjectDisposedException("Test");
    }
}
