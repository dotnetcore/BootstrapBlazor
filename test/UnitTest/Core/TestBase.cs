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
        _ = Context.DisposeAsync();
        GC.SuppressFinalize(this);
    }
}
