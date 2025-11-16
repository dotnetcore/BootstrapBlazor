// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace UnitTest.Core;

public class TestBase : IDisposable
{
    protected BunitContext Context { get; }

    public TestBase()
    {
        Context = new BunitContext();
        Context.JSInterop.Mode = JSRuntimeMode.Loose;

        Context.Services.AddMockEnvironment();
    }

    public void Dispose()
    {
#pragma warning disable CA2012
        // 由于 bUnit 2.0 继承了 IAsyncDisposable 接口，因此此处调用 DisposeAsync 方法
        Context.DisposeAsync();
#pragma warning restore CA2012    
        GC.SuppressFinalize(this);
    }
}
