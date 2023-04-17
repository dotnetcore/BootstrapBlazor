// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace UnitTest.Components;

public class BootstrapModuleComponentBaseTest : BootstrapBlazorTestBase
{
    [Fact]
    public async Task InvokeVoidAsync_Ok()
    {
        var cut = Context.RenderComponent<MockComponent>();
        await cut.Instance.InvokeVoidAsyncTest();
        Assert.True(cut.Instance.InvokeVoidRunned);
    }

    [Fact]
    public async Task InvokeAsync_Ok()
    {
        var cut = Context.RenderComponent<MockObjectReferenceComponent>();
        await cut.Instance.InvokeAsyncTest();
        Assert.True(cut.Instance.InvokeRunned);
        Assert.Equal("Mock", cut.Instance.GetModuleName());
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

        public string? GetModuleName() => ModuleName;

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

public class BootstrapModule2ComponentBaseTest : BootstrapBlazorTestBase
{
    [Fact]
    public void OnLoadJSModule_Ok()
    {
        var cut = Context.RenderComponent<MockComponent>();
        cut.SetParametersAndRender();
        Assert.True(cut.Instance.Executed);
    }

    [Fact]
    public async Task InvokeExecuteAsync_Ok()
    {
        var cut = Context.RenderComponent<MockComponent>();
        await cut.Instance.InvokeExecuteTest();
        Assert.True(cut.Instance.InvokeExecuted);
    }

    [Fact]
    public async Task InvokeVoidAsync_Ok()
    {
        var cut = Context.RenderComponent<MockComponent>();
        await cut.Instance.InvokeVoidAsyncTest();
        Assert.True(cut.Instance.InvokeVoidRunned);
    }

    [Fact]
    public async Task InvokeAsync_Ok()
    {
        var cut = Context.RenderComponent<MockObjectReferenceComponent>();
        await cut.Instance.InvokeAsyncTest();
        Assert.True(cut.Instance.InvokeRunned);
    }

    [JSModuleAutoLoader]
    class MockComponent : BootstrapModule2ComponentBase
    {
        public bool Executed { get; set; }

        public bool InvokeExecuted { get; set; }

        public bool InvokeVoidRunned { get; set; }

        protected override async Task ModuleExecuteAsync()
        {
            await base.ModuleExecuteAsync();

            Executed = true;
        }

        public async Task InvokeExecuteTest()
        {
            await base.InvokeExecuteAsync();
            InvokeExecuted = true;
        }

        public async Task InvokeVoidAsyncTest()
        {
            await base.InvokeVoidAsync(Id);
            await base.InvokeVoidAsync(Id, TimeSpan.FromMinutes(1));

            using CancellationTokenSource cancellationTokenSource = new CancellationTokenSource(1000);
            await base.InvokeVoidAsync(Id, cancellationTokenSource.Token);

            InvokeVoidRunned = true;
        }
    }

    [JSModuleAutoLoader(JSObjectReference = true, ModuleName = "Mock", Relative = true)]
    class MockObjectReferenceComponent : BootstrapModule2ComponentBase
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
