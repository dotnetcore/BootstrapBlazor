// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Bunit;
using Bunit.TestDoubles;
using System;
using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace UnitTest.Core;

[Collection("AuthorizationContext")]
public class AuthorizationTestBase
{
    protected TestContext Context { get; }

    protected TestAuthorizationContext AuthorizationContext { get; }

    public AuthorizationTestBase()
    {
        Context = AuthorizationTestHost.Instance;
        AuthorizationContext = AuthorizationTestHost.AuthorizationContext;
    }
}

[CollectionDefinition("AuthorizationContext")]
public class AuthorizationTestCollection : ICollectionFixture<AuthorizationTestHost>
{

}

public class AuthorizationTestHost : IDisposable
{
    [NotNull]
    internal static TestContext? Instance { get; private set; }

    [NotNull]
    internal static TestAuthorizationContext? AuthorizationContext { get; private set; }

    public AuthorizationTestHost()
    {
        Instance = new TestContext();
        Instance.JSInterop.Mode = JSRuntimeMode.Loose;
        AuthorizationContext = Instance.AddTestAuthorization();
    }

    public void Dispose()
    {
        Instance.Dispose();
        GC.SuppressFinalize(this);
    }
}
