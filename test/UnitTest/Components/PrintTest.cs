// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.Extensions.DependencyInjection;
using System.Web;

namespace UnitTest.Components;

public class PrintTest : BootstrapBlazorTestBase
{
    [Fact]
    public void PrintButton_Ok()
    {
        var cut = Context.RenderComponent<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<PrintButton>();
        });
        Assert.Contains("<a onclick=\"$.bb_printview(this)\" class=\"btn btn-primary\" role=\"button\"><i class=\"fa fa-print\"></i><span>打印</span></a>", HttpUtility.HtmlDecode(cut.Markup));

        var button = cut.FindComponent<PrintButton>();
        button.SetParametersAndRender(pb =>
        {
            pb.Add(b => b.PreviewUrl, "/PrintTest");
        });
        Assert.Contains("href=\"/PrintTest\"", button.Markup);
    }

    [Fact]
    public void PrintService_Error()
    {
        var cut = Context.RenderComponent<BootstrapBlazorRoot>();
        var printService = cut.Services.CreateScope().ServiceProvider.GetRequiredService<PrintService>();
        Assert.ThrowsAsync<InvalidOperationException>(() => printService.PrintAsync<Button>(op =>
        {
            // 弹窗配置
            op.Title = "数据查询窗口";
            // 弹窗组件所需参数
            return new Dictionary<string, object?>();
        }));
    }

    [Fact]
    public void PrintService_Ok()
    {
        var cut = Context.RenderComponent<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<MockPrintButton>();
        });
        var button = cut.FindComponent<MockPrintButton>();
        cut.InvokeAsync(() => button.Instance.PrintAsync());
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
