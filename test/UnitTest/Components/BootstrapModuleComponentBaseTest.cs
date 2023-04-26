// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.Extensions.DependencyInjection;
using Microsoft.JSInterop;

namespace UnitTest.Components;

public class BootstrapModuleComponentBaseTest : BootstrapBlazorTestBase
{
    [Fact]
    public void InvokeVoidAsync_Ok()
    {
        var cut = Context.RenderComponent<MockComponent>();
        cut.InvokeAsync(() => cut.Instance.InvokeVoidAsyncTest());
        Assert.True(cut.Instance.InvokeVoidRunned);
    }

    [Fact]
    public void InvokeAsync_Ok()
    {
        var cut = Context.RenderComponent<MockObjectReferenceComponent>();
        cut.InvokeAsync(() => cut.Instance.InvokeAsyncTest());
        Assert.True(cut.Instance.InvokeRunned);
    }

    [Fact]
    public void LoadModule()
    {
        var jsRuntime = Context.Services.GetRequiredService<IJSRuntime>();
        _ = jsRuntime.LoadModule("test.js", false);
        _ = jsRuntime.LoadModule3<MockClass>("test", new MockClass(), false);
        _ = jsRuntime.LoadModule3<MockClass>("test", new MockClass(), true);
    }

    class MockClass
    {

    }

    [JSModuleAutoLoader]
    class MockComponent : BootstrapModuleComponentBase
    {
        public bool InvokeVoidRunned { get; set; }

        public async Task InvokeVoidAsyncTest()
        {
            await base.InvokeVoidAsync(Id);
            await base.InvokeVoidAsync(Id, TimeSpan.FromMinutes(1));

            using CancellationTokenSource cancellationTokenSource = new CancellationTokenSource(1000);
            await base.InvokeVoidAsync(Id, cancellationTokenSource.Token);

            InvokeVoidRunned = true;
        }
    }

    [JSModuleAutoLoader(JSObjectReference = true, ModuleName = "Mock", Relative = true, AutoInvokeDispose = true, AutoInvokeInit = true)]
    class MockObjectReferenceComponent : BootstrapModuleComponentBase
    {
        public bool InvokeRunned { get; set; }

        public async Task InvokeAsyncTest()
        {
            await base.InvokeAsync<string>(Id);
            await base.InvokeAsync<string>(Id, TimeSpan.FromMinutes(1));

            using CancellationTokenSource cancellationTokenSource = new CancellationTokenSource(1000);
            await base.InvokeAsync<string>(Id, cancellationTokenSource.Token);

            InvokeRunned = true;
        }
    }
}
