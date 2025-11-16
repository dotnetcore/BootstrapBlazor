// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace UnitTest.Components;

public class PrintTest : BootstrapBlazorTestBase
{
    [Fact]
    public void PrintButton_Ok()
    {
        var cut = Context.Render<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<PrintButton>();
        });
        Assert.Contains("fa-solid fa-print", cut.Markup);

        var button = cut.FindComponent<PrintButton>();
        button.Render(pb =>
        {
            pb.Add(b => b.PreviewUrl, "/PrintTest");
        });
        Assert.Contains("href=\"/PrintTest\"", button.Markup);
    }

    [Fact]
    public async Task PrintService_Error()
    {
        var cut = Context.Render<BootstrapBlazorRoot>();
        var printService = cut.Services.CreateScope().ServiceProvider.GetRequiredService<PrintService>();
        await Assert.ThrowsAsync<InvalidOperationException>(() => printService.PrintAsync<Button>(op =>
        {
            // 弹窗配置
            op.Title = "数据查询窗口";
            // 弹窗组件所需参数
            return new Dictionary<string, object?>();
        }));
    }

    [Fact]
    public async Task PrintService_Ok()
    {
        var cut = Context.Render<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<MockPrintButton>();
        });
        var button = cut.FindComponent<MockPrintButton>();
        await cut.InvokeAsync(() => button.Instance.PrintAsync());
    }

    private class MockPrintButton : ComponentBase
    {
        [Inject]
        [NotNull]
        private PrintService? PrintService { get; set; }

        public Task PrintAsync() => PrintService.PrintAsync<Button>(op =>
        {
            op.Title = "打印按钮测试";
            return new Dictionary<string, object?>();
        });
    }
}
