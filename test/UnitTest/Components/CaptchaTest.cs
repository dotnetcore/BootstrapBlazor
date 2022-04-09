// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace UnitTest.Components;
public class CaptchaTest : BootstrapBlazorTestBase
{
    [Fact]
    public async Task Verify_Ok()
    {
        var verify = false;
        var cut = Context.RenderComponent<Captcha>(pb =>
        {
            pb.Add(a => a.SideLength, 42);
            pb.Add(a => a.Diameter, 9);
            pb.Add(a => a.Width, 280);
            pb.Add(a => a.Height, 150);
            pb.Add(a => a.Offset, 1000);
            pb.Add(a => a.OnValid, b =>
            {
                verify = b;
            });
        });
        await cut.InvokeAsync(() => cut.Instance.Verify(10, new int[] { 1, 2, 3, 4, 1 }));
        Assert.True(verify);

        await cut.InvokeAsync(() => cut.Instance.Verify(10, new int[] { 1, 2, 3, 4 }));
        Assert.False(verify);

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.Offset, 5);
            pb.Add(a => a.OnValid, null);
        });
        await cut.InvokeAsync(() => cut.Instance.Verify(10, new int[] { 1, 2, 3, 4, 1 }));
    }

    [Fact]
    public async Task Reset_Ok()
    {
        var cut = Context.RenderComponent<Captcha>(pb =>
        {
            pb.Add(a => a.ImagesPath, "images");
            pb.Add(a => a.ImagesName, "Pic.jpg");
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
