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

        await Assert.ThrowsAsync<NotImplementedException>(() => html2ImageService.GetDataAsync(".test", new()));
        await Assert.ThrowsAsync<NotImplementedException>(() => html2ImageService.GetStreamAsync(".test", new()));
    }

    [Fact]
    public void Html2ImageOptions_Ok()
    {
        var options = new Html2ImageOptions()
        {
            BackgroundColor = "red",
            Height = 100,
            Width = 100,
            Quality = 0.5,
            CanvasWidth = 10,
            CanvasHeight = 10,
            PixelRatio = 2,
            Type = "image/png",
            IncludeStyleProperties = ["background", "color"]
        };
        Assert.Equal("red", options.BackgroundColor);
        Assert.Equal(100, options.Height);
        Assert.Equal(100, options.Width);
        Assert.Equal(0.5, options.Quality);
        Assert.Equal(10, options.CanvasWidth);
        Assert.Equal(10, options.CanvasHeight);
        Assert.Equal(2, options.PixelRatio);
        Assert.Equal("image/png", options.Type);
        Assert.NotNull(options.IncludeStyleProperties);
    }
}
