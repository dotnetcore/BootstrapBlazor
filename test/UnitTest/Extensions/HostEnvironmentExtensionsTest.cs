// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;

namespace UnitTest.Extensions;

public class HostEnvironmentExtensionsTest
{
    [Fact]
    public void IsWasm_Ok()
    {
        var hostEnvironment = new MockWasmHostEnvironment();
        Assert.False(hostEnvironment.IsWasm());
    }

    class MockWasmHostEnvironment : IHostEnvironment
    {
        public string EnvironmentName { get; set; } = "Development";

        public string ApplicationName { get; set; } = "BootstrapBlazor";

        public string ContentRootPath { get; set; } = "";

        public IFileProvider ContentRootFileProvider { get; set; } = null!;
    }
}
