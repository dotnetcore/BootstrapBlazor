// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Shared;
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
    public async ValueTask Dispose_Ok()
    {
        var js = new MockJSObjectReference();
        var module = new JSModule(js);
        await module.DisposeAsync();
    }

    [Fact]
    public void Dispose_Error()
    {
        var js = new MockErrorJSObjectReference();
        var module = new JSModule(js);
        Assert.ThrowsAsync<InvalidOperationException>(async () => await module.DisposeAsync());
    }

    [Fact]
    public async ValueTask JSModuleGeneric_Ok()
    {
        var js = new MockJSObjectReference();
        var module = new JSModule2<object>(js, new Foo());
        Assert.NotNull(module);

        await module.InvokeVoidAsync("Test.init", "bb_id");
        await module.InvokeVoidAsync("Test.init", TimeSpan.Zero, "bb_id");
        await module.InvokeVoidAsync("Test.init", CancellationToken.None, "bb_id");
        await module.InvokeAsync<object>("Test.init", "bb_id");
        await module.InvokeAsync<object>("Test.init", TimeSpan.Zero, "bb_id");
        await module.InvokeAsync<object>("Test.init", CancellationToken.None, "bb_id");
        await module.DisposeAsync();
    }

    [Fact]
    public async ValueTask JSModuleGeneric_Error()
    {
        var js = new MockErrorJSObjectReference();
        var module = new JSModule2<object>(js, new Foo());
        await module.InvokeVoidAsync("Test.init", "bb_id");
        await module.DisposeAsync();
    }

    [Fact]
    public void JSModule_JSDisconnectedException()
    {
        var js = new MockJSDisconnectedObjectReference();
        var module = new JSModule(js);
        Assert.ThrowsAsync<JSDisconnectedException>(() =>
        {
            module.InvokeVoidAsync("test");
            return Task.CompletedTask;
        });
        Assert.ThrowsAsync<JSDisconnectedException>(() =>
        {
            module.InvokeAsync<int>("test");
            return Task.CompletedTask;
        });

        var module2 = new JSModule2<Foo>(js, new Foo());
        Assert.ThrowsAsync<JSDisconnectedException>(() =>
        {
            module2.InvokeVoidAsync("test");
            return Task.CompletedTask;
        });
        Assert.ThrowsAsync<JSDisconnectedException>(() =>
        {
            module2.InvokeAsync<int>("test");
            return Task.CompletedTask;
        });
    }

    [Fact]
    public void JSModule_TaskCanceledException()
    {
        var js = new MockTaskCanceledObjectReference();
        var module = new JSModule(js);
        Assert.ThrowsAsync<TaskCanceledException>(() =>
        {
            module.InvokeVoidAsync("test");
            return Task.CompletedTask;
        });
        Assert.ThrowsAsync<TaskCanceledException>(() =>
        {
            module.InvokeAsync<int>("test");
            return Task.CompletedTask;
        });

        var module2 = new JSModule2<Foo>(js, new Foo());
        Assert.ThrowsAsync<TaskCanceledException>(() =>
        {
            module2.InvokeVoidAsync("test", "args1", "args2");
            return Task.CompletedTask;
        });
        Assert.ThrowsAsync<TaskCanceledException>(() =>
        {
            module2.InvokeAsync<int>("test", "args1", "args2");
            return Task.CompletedTask;
        });
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

        public ValueTask InvokeVoidAsync_JSDisconnected_Test() => throw new JSDisconnectedException("Test");
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
}
