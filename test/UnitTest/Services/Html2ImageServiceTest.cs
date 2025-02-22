// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace UnitTest.Services;

public class Html2ImageServiceTest
{
    [Fact]
    public async Task Html2Image_Error()
    {
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddBootstrapBlazor();

        var provider = serviceCollection.BuildServiceProvider();
        var html2ImageService = provider.GetRequiredService<IHtml2Image>();

        await Assert.ThrowsAsync<NotImplementedException>(() => html2ImageService.GetDataAsync(".test", null));
        await Assert.ThrowsAsync<NotImplementedException>(() => html2ImageService.GetStreamAsync(".test", null));
    }
}
