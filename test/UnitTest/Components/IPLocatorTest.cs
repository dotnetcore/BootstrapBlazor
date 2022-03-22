// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace UnitTest.Components;

public class IPLocatorTest : BootstrapBlazorTestBase
{
    [Fact]
    public async Task Locator_Ok()
    {
        var cut = Context.RenderComponent<IpLocatorTest>();
        var result = await cut.Instance.IPLocator.Locate("127.0.0.1");
        Assert.Equal("本地连接", result);
        result = await cut.Instance.IPLocator.Locate("");
        Assert.Equal("本地连接", result);
        var widenet = await cut.Instance.IPLocator.Locate("223.91.188.112");
        Assert.NotNull(widenet);
    }

    [Fact]
    public void BaiduIpLocator_Ok()
    {
        var locator = new BaiDuIPLocator
        {
            Status = "0"
        };
        var ret = locator.ToString();
        Assert.Equal("XX XX", ret);

        locator.Data = Array.Empty<LocationInfo>();
        ret = locator.ToString();
        Assert.Equal("XX XX", ret);

        locator.Data = new LocationInfo[]
        {
            new()
        };
        ret = locator.ToString();
        Assert.Equal("XX XX", ret);

        locator.Data = new LocationInfo[]
        {
            new()
            {
                Location = "Test"
            }
        };
        ret = locator.ToString();
        Assert.Equal("Test", ret);

        locator.Status = "1";
        ret = locator.ToString();
        Assert.Equal("Error", ret);
    }

    [Fact]
    public async Task DefaultIPLocator_Ok()
    {
        var locator = new DefaultIPLocator();
        var ret = await locator.Locate(new IPLocatorOption()
        {
            RequestTimeout = 3000
        });
        Assert.Null(ret);
    }

    private class IpLocatorTest : ComponentBase
    {
        [Inject]
        [NotNull]
        public IIPLocatorProvider? IPLocator { get; set; }
    }
}
