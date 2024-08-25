// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Bunit.TestDoubles;

namespace UnitTest.Core;

public class AuthorizationViewTestBase
{
    protected TestContext Context { get; }

    protected TestAuthorizationContext AuthorizationContext { get; }

    public AuthorizationViewTestBase()
    {
        Context = new TestContext();
        AuthorizationContext = Context.AddTestAuthorization();

        // Mock 脚本
        Context.JSInterop.Mode = JSRuntimeMode.Loose;

        Context.Services.AddBootstrapBlazor();
        Context.Services.AddConfiguration();

        // 渲染 BootstrapBlazorRoot 组件 激活 ICacheManager 接口
        Context.Services.GetRequiredService<ICacheManager>();
    }

    public void Dispose()
    {
        Context.Dispose();
        GC.SuppressFinalize(this);
    }
}
