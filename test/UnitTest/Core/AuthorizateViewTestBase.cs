// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Bunit.TestDoubles;
using Microsoft.Extensions.DependencyInjection;

namespace UnitTest.Core;

[Collection("AuthorizateViewContext")]
public class AuthorizateViewTestBase
{
    protected TestContext Context { get; }

    protected TestAuthorizationContext AuthorizationContext { get; }

    public AuthorizateViewTestBase()
    {
        Context = AuthorizateViewTestHost.Instance;
        AuthorizationContext = AuthorizateViewTestHost.AuthorizationContext;
    }
}

[CollectionDefinition("AuthorizateViewContext")]
public class AuthorizateViewTestCollection : ICollectionFixture<AuthorizateViewTestHost>
{

}

public class AuthorizateViewTestHost : IDisposable
{
    [NotNull]
    internal static TestContext? Instance { get; private set; }

    [NotNull]
    internal static TestAuthorizationContext? AuthorizationContext { get; private set; }

    public AuthorizateViewTestHost()
    {
        Instance = new TestContext();
        Instance.JSInterop.Mode = JSRuntimeMode.Loose;
        AuthorizationContext = Instance.AddTestAuthorization();

        // Mock 脚本
        Instance.JSInterop.Mode = JSRuntimeMode.Loose;

        Instance.Services.AddBootstrapBlazor();
        Instance.Services.AddConfiguration();

        // 渲染 BootstrapBlazorRoot 组件 激活 ICacheManager 接口
        Instance.Services.GetRequiredService<ICacheManager>();
    }

    public void Dispose()
    {
        Instance.Dispose();
        GC.SuppressFinalize(this);
    }
}
