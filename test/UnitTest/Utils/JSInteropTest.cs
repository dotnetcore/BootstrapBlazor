// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.Extensions.DependencyInjection;
using Microsoft.JSInterop;

namespace UnitTest.Utils;

public class JSInteropTest : BootstrapBlazorTestBase
{
    [Fact]
    public async Task InvokeVoidAsync_Ok()
    {
        var jsRuntime = Context.Services.GetRequiredService<IJSRuntime>();
        var interop = new JSInterop<Foo>(jsRuntime);
        await interop.InvokeVoidAsync("bb_test", new Foo(), "1");
    }

    private class Foo : ComponentBase
    {

    }
}
