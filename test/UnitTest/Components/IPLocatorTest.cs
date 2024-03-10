// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace UnitTest.Components;

public class IPLocatorTest : BootstrapBlazorTestBase
{
    [Fact]
    public async Task BaiduIPLocatorProviderV2_Ok()
    {
        var result = "";
        var cut = Context.RenderComponent<IpLocatorTest>();
        var factory = cut.Instance.IPLocatorFactory;
        var provider = factory.Create();

        result = await provider.Locate("127.0.0.1");
        Assert.Equal("本地连接", result);

        result = await provider.Locate("");
        Assert.Equal("本地连接", result);

        result = await provider.Locate("223.91.188.112");
        Assert.Equal("河南省漯河市舞阳县 中国移动", result);
    }

    [Fact]
    public async Task BaiduIPLocatorProvider_Ok()
    {
        var result = "";
        var cut = Context.RenderComponent<IpLocatorTest>();
        var factory = cut.Instance.IPLocatorFactory;
        var provider = factory.Create("BaiduIPLocatorProvider");

        result = await provider.Locate("127.0.0.1");
        Assert.Equal("本地连接", result);

        result = await provider.Locate("");
        Assert.Equal("本地连接", result);

        result = await provider.Locate("223.91.188.112");
        Assert.Equal("河南省漯河市 移动", result);
    }

    [Fact]
    public void Factory_Error()
    {
        var cut = Context.RenderComponent<IpLocatorTest>();
        var factory = cut.Instance.IPLocatorFactory;
        Assert.Throws<InvalidOperationException>(() => factory.Create("BaiduIPLocatorProviderV0"));
    }

    [Fact]
    public void GetProvider_Error()
    {
        var factory = Context.Services.GetRequiredService<IIPLocatorFactory>();
        // 利用反射调用 GetProvider 方法
        var method = factory.GetType().GetMethod("GetProvider", BindingFlags.NonPublic | BindingFlags.Instance);
        Assert.NotNull(method);

        // KeyNotFoundException
        Assert.Throws<TargetInvocationException>(() => method.Invoke(factory, ["BaiduIPLocatorProviderV0"]));
    }

    private class IpLocatorTest : ComponentBase
    {
        [Inject]
        [NotNull]
        public IIPLocatorFactory? IPLocatorFactory { get; set; }
    }
}
