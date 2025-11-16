// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.JSInterop;

namespace UnitTest.Components;

public class BootstrapModuleComponentBaseTest : BootstrapBlazorTestBase
{
    [Fact]
    public void InvokeVoidAsync_Ok()
    {
        var cut = Context.Render<MockComponent>();
        cut.InvokeAsync(() => cut.Instance.InvokeVoidAsyncTest());
        Assert.True(cut.Instance.InvokeVoidRunner);
    }

    [Fact]
    public async Task InvokeAsync_Ok()
    {
        var cut = Context.Render<MockObjectReferenceComponent>();
        await cut.InvokeAsync(() => cut.Instance.InvokeAsyncTest());
        Assert.True(cut.Instance.InvokeRunner);
    }

    [JSModuleAutoLoader("mock.js")]
    class MockComponent : BootstrapModuleComponentBase
    {
        public bool InvokeVoidRunner { get; set; }

        public async Task InvokeVoidAsyncTest()
        {
            await InvokeVoidAsync(Id);
            await InvokeVoidAsync(Id, TimeSpan.FromMinutes(1));

            using CancellationTokenSource cancellationTokenSource = new CancellationTokenSource(1000);
            await InvokeVoidAsync(Id, cancellationTokenSource.Token);

            InvokeVoidRunner = true;
        }
    }

    [JSModuleAutoLoader("mock.js", JSObjectReference = true, AutoInvokeDispose = true, AutoInvokeInit = true)]
    class MockObjectReferenceComponent : Select<string>
    {
        public bool InvokeRunner { get; set; }

        public async Task InvokeAsyncTest()
        {
            await InvokeAsync<string>(Id);
            await InvokeAsync<string>(Id, TimeSpan.FromMinutes(1));

            using CancellationTokenSource cancellationTokenSource = new CancellationTokenSource(1000);
            await InvokeAsync<string>(Id, cancellationTokenSource.Token);

            InvokeRunner = true;
        }
    }
}
