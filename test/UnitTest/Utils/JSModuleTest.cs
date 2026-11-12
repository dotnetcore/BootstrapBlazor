// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

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

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public async Task Dispose_Error(bool asyncDispose)
    {
        var js = new MockErrorJSObjectReference(asyncDispose);
        var module = new JSModule(js);
        var exception = await Record.ExceptionAsync(async () => await module.DisposeAsync());
        Assert.Null(exception);
        Assert.Equal(1, js.DisposeCallCount);
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

    [Fact]
    public async Task JSModule_SetMemorial()
    {
        var js = new MockTaskCanceledObjectReference();
        var module = new JSModule(js);
        await module.SetMemorialModeAsync(true);
    }

    [Fact]
    public async Task JSModule_GetMemorial()
    {
        var js = new MockTaskCanceledObjectReference();
        var module = new JSModule(js);
        await module.GetMemorialModeAsync();
    }

    [Fact]
    public async Task JSModule_ToggleMemorial()
    {
        var js = new MockTaskCanceledObjectReference();
        var module = new JSModule(js);
        await module.ToggleMemorialModeAsync();
    }

    private class MockErrorJSObjectReference(bool asyncDispose) : MockJSObjectReference
    {
        public int DisposeCallCount { get; private set; }

        public override ValueTask DisposeAsync()
        {
            DisposeCallCount++;
            if (asyncDispose)
            {
                return ValueTask.FromException(new InvalidOperationException());
            }
            throw new InvalidOperationException();
        }
    }

    private class MockJSObjectReference : IJSObjectReference
    {
        public virtual ValueTask DisposeAsync() => ValueTask.CompletedTask;

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
