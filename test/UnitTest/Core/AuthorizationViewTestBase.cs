// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Bunit.TestDoubles;

namespace UnitTest.Core;

public class AuthorizationViewTestBase : BootstrapBlazorTestBase
{
    [NotNull]
    protected TestAuthorizationContext? AuthorizationContext { get; private set; }

    protected override void ConfigureServices(IServiceCollection services)
    {
        base.ConfigureServices(services);

        AuthorizationContext = Context.AddTestAuthorization();
    }
}
