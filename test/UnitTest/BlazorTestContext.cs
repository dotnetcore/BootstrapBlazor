// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Components;
using Bunit;
using Microsoft.Extensions.DependencyInjection;
using UnitTest.Extensions;
using UnitTest.Services;

namespace UnitTest
{
    internal class BlazorTestContext : TestContext
    {
        public BlazorTestContext() : base()
        {
            Services.AddBootstrapBlazor();
            Services.AddConfiguration();
            Services.AddFallbackServiceProvider(new FallbackServiceProvider());
        }
    }
}
