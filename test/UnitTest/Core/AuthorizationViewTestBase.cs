// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Bunit.TestDoubles;

namespace UnitTest.Core;

public class AuthorizationViewTestBase : BootstrapBlazorTestBase
{
    [NotNull]
    protected BunitAuthorizationContext? AuthorizationContext { get; private set; }

    protected override void ConfigureServices(IServiceCollection services)
    {
        base.ConfigureServices(services);

        AuthorizationContext = Context.AddAuthorization();
    }
}
