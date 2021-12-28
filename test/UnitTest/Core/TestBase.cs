// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Bunit;
using System;
using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace UnitTest.Core;

[Collection("TestContext")]
public class TestBase
{
    protected TestContext Context { get; }

    public TestBase()
    {
        Context = TestHost.Instance;
    }
}

[CollectionDefinition("TestContext")]
public class TestCollection : ICollectionFixture<TestHost>
{

}

public class TestHost : IDisposable
{
    [NotNull]
    internal static TestContext? Instance { get; private set; }

    public TestHost()
    {
        Instance = new TestContext();
        Instance.JSInterop.Mode = JSRuntimeMode.Loose;
    }

    public void Dispose()
    {
        Instance.Dispose();
        GC.SuppressFinalize(this);
    }
}
