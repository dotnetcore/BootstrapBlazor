// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace UnitTest.Components;
public class CaptchaTest : BootstrapBlazorTestBase
{
    [Fact]
    public async Task Verify_Ok()
    {
        var verify = false;
        var cut = Context.Render<Captcha>(pb =>
        {
            pb.Add(a => a.SideLength, 42);
            pb.Add(a => a.Diameter, 9);
            pb.Add(a => a.Width, 280);
            pb.Add(a => a.Height, 150);
            pb.Add(a => a.Offset, 1000);
            pb.Add(a => a.OnValidAsync, b =>
            {
                verify = b;
                return Task.CompletedTask;
            });
        });
        await cut.InvokeAsync(() => cut.Instance.Verify(10, [1, 2, 3, 4, 1]));
        Assert.True(verify);

        await cut.InvokeAsync(() => cut.Instance.Verify(10, [1, 2, 3, 4]));
        Assert.False(verify);

        cut.Render(pb =>
        {
            pb.Add(a => a.Offset, 5);
            pb.Add(a => a.OnValidAsync, null);
        });
        await cut.InvokeAsync(() => cut.Instance.Verify(10, [1, 2, 3, 4, 1]));
    }

    [Fact]
    public async Task Reset_Ok()
    {
        var cut = Context.Render<Captcha>(pb =>
        {
            pb.Add(a => a.ImagesPath, "images");
            pb.Add(a => a.ImagesName, "Pic.jpg");
            pb.Add(a => a.Max, 8);
            pb.Add(a => a.GetImageName, () => "test.jpg");
        });
        await cut.InvokeAsync(() => cut.Find(".captcha-refresh").Click());
    }

    [Fact]
    public void Option_Ok()
    {
        var option = new CaptchaOption()
        {
            OffsetY = 100,
            ImageUrl = "test.jpg"
        };
        Assert.Equal(100, option.OffsetY);
        Assert.Equal("test.jpg", option.ImageUrl);
    }
}
