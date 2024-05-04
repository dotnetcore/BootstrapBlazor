// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.Extensions.DependencyInjection;
using Microsoft.JSInterop;

namespace UnitTest.Extensions;

public class JSModuleExtensionsTest : BootstrapBlazorTestBase
{
    [Fact]
    public async Task LoadModule_Ok()
    {
        var jsRuntime = Context.Services.GetRequiredService<IJSRuntime>();
        await jsRuntime.LoadModule("./mock.js", "test");
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

    [Fact]
    public async Task IsMobile_Ok()
    {
        var jsRuntime = Context.Services.GetRequiredService<IJSRuntime>();
        var module = await jsRuntime.LoadUtility();
        await module.IsMobile();
    }

    [Fact]
    public async Task OpenUrl_Ok()
    {
        var jsRuntime = Context.Services.GetRequiredService<IJSRuntime>();
        var module = await jsRuntime.LoadUtility();
        await module.OpenUrl("www.blazor.zone");
    }

    [Fact]
    public async Task Eval_Ok()
    {
        var jsRuntime = Context.Services.GetRequiredService<IJSRuntime>();
        var module = await jsRuntime.LoadUtility();
        await module.Eval("test");
        await module.Eval<string>("test2");
    }

    [Fact]
    public async Task Function_Ok()
    {
        var jsRuntime = Context.Services.GetRequiredService<IJSRuntime>();
        var module = await jsRuntime.LoadUtility();
        await module.Function("test");
        await module.Function<string>("test2");
    }

    [Fact]
    public async Task GenerateId_Ok()
    {
        Context.JSInterop.Setup<string?>("getUID", ["bb"]).SetResult("bb_test");
        var jsRuntime = Context.Services.GetRequiredService<IJSRuntime>();
        var module = await jsRuntime.LoadUtility();
        var id = await module.GenerateId("bb");
        Assert.Equal("bb_test", id);
    }

    [Fact]
    public async Task GetHtml_Ok()
    {
        Context.JSInterop.Setup<string?>("getHtml", v => true).SetResult("bb_test");
        var jsRuntime = Context.Services.GetRequiredService<IJSRuntime>();
        var module = await jsRuntime.LoadUtility();
        var html = await module.GetHtml("bb");
        Assert.Equal("bb_test", html);
    }

    class MockComponent : ComponentBase
    {

    }
}
