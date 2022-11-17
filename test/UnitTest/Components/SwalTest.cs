// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace UnitTest.Components;

public class SwalTest : SwalTestBase
{
    [Fact]
    public void Show_Ok()
    {
        var cut = Context.RenderComponent<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<MockSwalTest>();
        });

        var swal = cut.FindComponent<MockSwalTest>().Instance.SwalService;

        cut.InvokeAsync(() => swal.Show(new SwalOption()
        {
            BodyTemplate = builder => builder.AddContent(0, "Test-BodyTemplate"),
            FooterTemplate = builder => builder.AddContent(0, "Test-FooterTemplate"),
            ButtonTemplate = builder => builder.AddContent(0, "Test-ButtonTemplate"),
            ShowFooter = true,
            ShowClose = true,
            CloseButtonIcon = "test-close-icon",
            CloseButtonText = "test-button-text-Cancel"
        }));

        // 代码覆盖模板单元测试
        Assert.Contains("Test-BodyTemplate", cut.Markup);
        Assert.Contains("Test-FooterTemplate", cut.Markup);
        Assert.Contains("Test-ButtonTemplate", cut.Markup);
        Assert.Contains("test-close-icon", cut.Markup);
        Assert.Contains("test-button-text-Cancel", cut.Markup);

        // 测试关闭逻辑
        var modals = cut.FindComponents<Modal>();
        var modal = modals[modals.Count - 1];
        cut.InvokeAsync(() => modal.Instance.CloseCallback());

        // 测试 Category
        cut.InvokeAsync(() => swal.Show(new SwalOption()
        {
            Content = "I am Eror",
            Category = SwalCategory.Error
        }));
        Assert.Contains("swal2-x-mark-line-left", cut.Markup);
        cut.InvokeAsync(() => modal.Instance.CloseCallback());

        // 测试 Category
        cut.InvokeAsync(() => swal.Show(new SwalOption()
        {
            Content = "I am Eror",
            Category = SwalCategory.Information
        }));
        Assert.Contains("swal2-info", cut.Markup);
        cut.InvokeAsync(() => modal.Instance.CloseCallback());

        // 测试 Category
        cut.InvokeAsync(() => swal.Show(new SwalOption()
        {
            Content = "I am Eror",
            Category = SwalCategory.Warning
        }));
        Assert.Contains("swal2-warning", cut.Markup);
        cut.InvokeAsync(() => modal.Instance.CloseCallback());

        // 测试 Category
        cut.InvokeAsync(() => swal.Show(new SwalOption()
        {
            Content = "I am Eror",
            Category = SwalCategory.Question
        }));
        Assert.Contains("swal2-question", cut.Markup);
        cut.InvokeAsync(() => modal.Instance.CloseCallback());

        //测试 Content
        cut.InvokeAsync(() => swal.Show(new SwalOption()
        {
            Content = "I am Swal",
        }));
        Assert.Contains("I am Swal", cut.Markup);
        cut.InvokeAsync(() => modal.Instance.CloseCallback());

        //测试 Title
        cut.InvokeAsync(() => swal.Show(new SwalOption()
        {
            Title = "I am Title",
        }));
        Assert.Contains("I am Title", cut.Markup);
        cut.InvokeAsync(() => modal.Instance.CloseCallback());

        //测试 Title
        cut.InvokeAsync(() => swal.Show(new SwalOption()
        {
            Title = "I am Title",
            Content = "I am Swal",
        }));
        Assert.Contains("I am Title", cut.Markup);
        cut.InvokeAsync(() => modal.Instance.CloseCallback());

        //测试关闭按钮
        cut.InvokeAsync(() => swal.Show(new SwalOption()
        {
            Content = "I am Swal",
            IsAutoHide = true,
            Delay = 1000
        }));
        var button = cut.Find(".btn-secondary");
        cut.InvokeAsync(() => button.Click());
        cut.InvokeAsync(() => modal.Instance.CloseCallback());

        // auto close
        cut.InvokeAsync(() => swal.Show(new SwalOption()
        {
            Content = "I am auto hide",
            IsAutoHide = true,
            Delay = 100
        }));
        Thread.Sleep(150);
        // 弹窗显示
        cut.Contains("I am auto hide");
        Thread.Sleep(150);
        // 模拟关闭
        cut.InvokeAsync(() => modal.Instance.CloseCallback());
        cut.DoesNotContain("I am auto hide");

        // 模态框
        bool result = false;
        Task.Run(async () => await cut.InvokeAsync(async () =>
        {
            result = await swal.ShowModal(new SwalOption()
            {
                Content = "I am Modal Swal",
                CancelButtonText = "test-cancel-text",
                ConfirmButtonIcon = "test-confirm-icon",
                ConfirmButtonText = "test-confirm-text"
            });
        }));

        var tick = DateTime.Now;
        while (!cut.Markup.Contains("test-cancel-text"))
        {
            Thread.Sleep(100);
            if (DateTime.Now > tick.AddSeconds(1))
            {
                break;
            }
        }
        cut.Contains("test-cancel-text");
        cut.Contains("I am Modal Swal");
        cut.Contains("test-confirm-icon");
        cut.Contains("test-confirm-text");

        // 触发确认按钮
        button = cut.Find(".btn-danger");
        cut.InvokeAsync(() => button.Click());
        cut.InvokeAsync(() => modal.Instance.CloseCallback());

        // 自动隐藏时间未到时触发 Disposing
        cut.InvokeAsync(() => swal.Show(new SwalOption()
        {
            Content = "I am auto hide",
            IsAutoHide = true,
            Delay = 4000
        }));
        Thread.Sleep(150);
        // 弹窗显示
        cut.Contains("I am auto hide");
        var alert = cut.FindComponent<SweetAlert>();
        alert.Dispose();
    }

    private class MockSwalTest : ComponentBase
    {
        [Inject]
        [NotNull]
        public SwalService? SwalService { get; set; }
    }
}
