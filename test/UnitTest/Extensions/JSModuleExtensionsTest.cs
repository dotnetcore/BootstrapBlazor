// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.Extensions.DependencyInjection;
using Microsoft.JSInterop;

namespace UnitTest.Extensions;

public class JSModuleExtensionsTest : BootstrapBlazorTestBase
{
    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public async Task LoadModule_Ok(bool relative)
    {
        var jsRuntime = Context.Services.GetRequiredService<IJSRuntime>();
        await jsRuntime.LoadModule2("./mock.js", relative);
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public async Task LoadModuleOfType_Ok(bool relative)
    {
        var jsRuntime = Context.Services.GetRequiredService<IJSRuntime>();
        await jsRuntime.LoadModule2<MockComponent>("./mock.js", new MockComponent(), relative);
    }

    [Fact]
    public void GetTypeModuleName_Ok()
    {
        var type1 = typeof(List<string>);
        var name = type1.GetTypeModuleName();
        Assert.Equal("List", name);

        var type2 = typeof(MockComponent);
        name = type2.GetTypeModuleName();
        Assert.Equal("MockComponent", name);
    }

    class MockComponent : ComponentBase
    {

    }
}
