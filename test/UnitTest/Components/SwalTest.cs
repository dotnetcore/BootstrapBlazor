// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace UnitTest.Components;

public class SwalTest : SwalTestBase
{
    [Fact]
    public async Task Show_Ok()
    {
        var cut = Context.RenderComponent<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<MockSwalTest>();
        });

        var swal = cut.FindComponent<MockSwalTest>().Instance.SwalService;

        await cut.InvokeAsync(async () => await swal.Show(new SwalOption()
        {
            BodyTemplate = builder => builder.AddContent(0, "Test-BodyTemplate"),
            FooterTemplate = builder => builder.AddContent(0, "Test-FooterTemplate"),
            ButtonTemplate = builder => builder.AddContent(0, "Test-ButtonTemplate"),
            ShowFooter = true,
            ShowClose = true,
            BodyContext = null
        }));

        // 代码覆盖模板单元测试
        Assert.Contains("Test-BodyTemplate", cut.Markup);
        Assert.Contains("Test-FooterTemplate", cut.Markup);
        Assert.Contains("Test-ButtonTemplate", cut.Markup);

        // 测试关闭逻辑
        var modal = cut.FindComponent<Modal>();
        await cut.InvokeAsync(() => modal.Instance.Close());

        // 测试 Category
        await cut.InvokeAsync(() => swal.Show(new SwalOption()
        {
            Content = "I am Eror",
            Category = SwalCategory.Error
        }));

        Assert.Contains("swal2-x-mark-line-left", cut.Markup);

        modal = cut.FindComponent<Modal>();
        await cut.InvokeAsync(() => modal.Instance.Close());

        //测试Content
        await cut.InvokeAsync(() => swal.Show(new SwalOption()
        {
            Content = "I am Swal",
        }));

        Assert.Contains("I am Swal", cut.Markup);

        modal = cut.FindComponent<Modal>();
        await cut.InvokeAsync(() => modal.Instance.Close());

        //测试Title
        await cut.InvokeAsync(() => swal.Show(new SwalOption()
        {
            Content = "I am Title",
        }));

        Assert.Contains("I am Title", cut.Markup);

        modal = cut.FindComponent<Modal>();
        await cut.InvokeAsync(() => modal.Instance.Close());

        //测试Title
        await cut.InvokeAsync(() => swal.Show(new SwalOption()
        {
            ForceDelay = true,
            Delay = 1000
        }));

        modal = cut.FindComponent<Modal>();
        await cut.InvokeAsync(() => modal.Instance.Close());

        //测试Title
        await cut.InvokeAsync(() => swal.Show(new SwalOption()
        {
            ForceDelay = true,
            Delay = 1000,
        }));

        modal = cut.FindComponent<Modal>();
        await cut.InvokeAsync(() => modal.Instance.Close());

        //测试关闭按钮
        await cut.InvokeAsync(() => swal.Show(new SwalOption()
        {
            Content = "I am Swal",
            IsAutoHide = true,
            Delay = 1000
        }));

        var button = cut.Find(".btn-secondary");
        button.Click();

        //测试Modal取消
        var cancel = true;
        _ = cut.InvokeAsync(async () =>
        {
            cancel = await swal.ShowModal(new SwalOption()
            {
                Content = "I am Swal"
            });
        });

        var cancelbutton = cut.Find(".btn-secondary");
        cancelbutton.Click();
        Assert.False(cancel);

        //测试Modal确认
        var confirm = false;
        _ = cut.InvokeAsync(async () =>
        {
            confirm = await swal.ShowModal(new SwalOption()
            {
                Content = "I am Swal"
            });
        });

        var confirmbutton = cut.Find(".btn-danger");
        confirmbutton.Click();
        Assert.True(confirm);

        cut.SetParametersAndRender(pb =>
        {
            pb.AddChildContent<Select<string>>(pb =>
            {
                pb.Add(a => a.OnBeforeSelectedItemChange, item => Task.FromResult(true));
                pb.Add(a => a.SwalFooter, "Test-Swal-Footer");
                pb.Add(a => a.SwalCategory, SwalCategory.Question);
                pb.Add(a => a.SwalTitle, "Test-Swal-Title");
                pb.Add(a => a.SwalContent, "Test-Swal-Content");
                pb.Add(a => a.Items, new SelectedItem[]
                {
                    new SelectedItem("1", "Test1"),
                    new SelectedItem("2", "Test2")
                });
            });
        });
        await cut.InvokeAsync(() => cut.Find(".dropdown-item").Click());
        Assert.Contains("Test-Swal-Title", cut.Markup);
        Assert.Contains("Test-Swal-Content", cut.Markup);
        Assert.Contains("Test-Swal-Footer", cut.Markup);

        await cut.InvokeAsync(() => cut.Find(".swal2-actions button").Click());
        Assert.DoesNotContain("Test-Swal-Content", cut.Markup);

        // 测试自动关闭
        await cut.InvokeAsync(() => swal.Show(new SwalOption()
        {
            Content = "I am Swal",
            IsAutoHide = true,
            Delay = 100
        }));
        while (cut.Markup.Contains("I am Swal"))
        {
            await Task.Delay(100);
        }

        // 不关闭弹窗测试 Dispose
        await cut.InvokeAsync(() => swal.Show(new SwalOption()
        {
            Content = "I am Swal",
            IsAutoHide = true,
            Delay = 1000
        }));
    }


    private class MockSwalTest : ComponentBase
    {
        [Inject]
        [NotNull]
        public SwalService? SwalService { get; set; }
    }
}
