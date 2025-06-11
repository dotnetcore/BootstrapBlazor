// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;

namespace BootstrapBlazor.Components;

[ExcludeFromCodeCoverage]
class MockWasmHostEnvironment : IHostEnvironment
{
#if DEBUG
    public string EnvironmentName { get; set; } = "Development";
#else
    public string EnvironmentName { get; set; } = "Production";
#endif

    public string ApplicationName { get; set; } = "BootstrapBlazor";

    public string ContentRootPath { get; set; } = "";

    public IFileProvider ContentRootFileProvider { get; set; } = null!;
}
