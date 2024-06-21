// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.Extensions.DependencyInjection;

namespace UnitTest.Services;

/// <summary>
/// MaskService 单元测试
/// </summary>
public class MaskServiceTest : TestBase
{
    [Fact]
    public async Task Mask_Ok()
    {
        var context = new TestContext();
        context.JSInterop.Mode = JSRuntimeMode.Loose;
        context.Services.AddBootstrapBlazor();

        var maskService = context.Services.GetRequiredService<MaskService>();
        var cut = context.RenderComponent<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<Button>(pb =>
            {
                pb.Add(a => a.OnClickWithoutRender, async () =>
                {
                    await maskService.Show(new MaskOption()
                    {
                        BackgroundColor = "#000",
                        Opacity = 0.5f,
                        ZIndex = 1050,
                        ChildContent = builder => builder.AddContent(0, "test-mask-content")
                    });
                });
            });
        });
        cut.Contains("<div class=\"bb-mask\"><div class=\"bb-mask-backdrop\"></div></div>");

        // 点击按钮显示
        var button = cut.Find("button");
        await cut.InvokeAsync(() => button.Click());
        cut.Contains("bb-mask-content");

        // 关闭遮罩
        await cut.InvokeAsync(maskService.Close);
        cut.DoesNotContain("bb-mask-content");
    }
}
