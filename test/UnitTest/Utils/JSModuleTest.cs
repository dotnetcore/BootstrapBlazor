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
        await module.InvokeVoidAsync("Test.init", "bb_id", TimeSpan.Zero);
        await module.InvokeVoidAsync("Test.init", "bb_id", CancellationToken.None);
        await module.InvokeAsync<object>("Test.init", "bb_id");
        await module.InvokeAsync<object>("Test.init", "bb_id", TimeSpan.Zero);
        await module.InvokeAsync<object>("Test.init", "bb_id", CancellationToken.None);
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
        var module = new JSModule<object>(js, new Foo());
        Assert.NotNull(module);

        await module.InvokeVoidAsync("Test.init", "bb_id");
        await module.InvokeVoidAsync("Test.init", "bb_id", TimeSpan.Zero);
        await module.InvokeVoidAsync("Test.init", "bb_id", CancellationToken.None);
        await module.InvokeAsync<object>("Test.init", "bb_id");
        await module.InvokeAsync<object>("Test.init", "bb_id", TimeSpan.Zero);
        await module.InvokeAsync<object>("Test.init", "bb_id", CancellationToken.None);
        await module.DisposeAsync();
    }

    [Fact]
    public async ValueTask JSModuleGeneric_Error()
    {
        var js = new MockErrorJSObjectReference();
        var module = new JSModule<object>(js, new Foo());
        await module.InvokeVoidAsync("Test.init", "bb_id");
        await module.DisposeAsync();
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
}
