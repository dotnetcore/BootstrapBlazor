// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace UnitTest.Components;

public class SwalTest : BootstrapBlazorTestBase
{
    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public async Task ShowModal_ReturnsConfirmationResult_Ok(bool expected)
    {
        var cut = Context.Render<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<MockSwalTest>();
        });
        var swal = cut.FindComponent<MockSwalTest>().Instance.SwalService;
        var option = new SwalOption();
        var modalTask = Task.FromResult(false);

        await cut.InvokeAsync(() =>
        {
            modalTask = swal.ShowModal(option);
        });

        var modal = cut.FindComponents<Modal>().Last();
        await cut.InvokeAsync(() => cut.Find(expected ? ".btn-danger" : ".btn-secondary").Click());
        await cut.InvokeAsync(() => modal.Instance.CloseCallback());

        Assert.Equal(expected, await modalTask);
        Assert.True(option.IsConfirm);
    }

    [Fact]
    public async Task ShowModal_IgnoresRequestsWhileBusy_Ok()
    {
        var cut = Context.Render<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<MockSwalTest>();
        });
        var swal = cut.FindComponent<MockSwalTest>().Instance.SwalService;
        var option = new SwalOption()
        {
            Content = "Active Swal"
        };
        var modalTask = Task.FromResult(false);

        await cut.InvokeAsync(() =>
        {
            modalTask = swal.ShowModal(option);
        });

        var newOption = new SwalOption()
        {
            Content = "Ignored Swal"
        };
        var newRequestTask = Task.FromResult(true);
        await cut.InvokeAsync(() =>
        {
            newRequestTask = swal.ShowModal(newOption);
        });

        Assert.False(await newRequestTask);
        Assert.False(newOption.IsConfirm);

        var repeatedRequestTask = Task.FromResult(true);
        await cut.InvokeAsync(() =>
        {
            repeatedRequestTask = swal.ShowModal(option);
        });

        Assert.False(await repeatedRequestTask);
        Assert.True(option.IsConfirm);
        Assert.Contains("Active Swal", cut.Markup);
        Assert.DoesNotContain("Ignored Swal", cut.Markup);

        var modal = cut.FindComponents<Modal>().Last();
        await cut.InvokeAsync(() => cut.Find(".btn-danger").Click());
        await cut.InvokeAsync(() => modal.Instance.CloseCallback());

        Assert.True(await modalTask);
    }

    [Fact]
    public async Task CloseAsync_IgnoresConcurrentCall_Ok()
    {
        var callbackStarted = new TaskCompletionSource(TaskCreationOptions.RunContinuationsAsynchronously);
        var continueCallback = new TaskCompletionSource(TaskCreationOptions.RunContinuationsAsynchronously);
        var callbackCount = 0;
        var cut = Context.Render<SweetAlertBody>(pb =>
        {
            pb.Add(a => a.IsConfirm, true);
            pb.Add(a => a.OnConfirmAsync, async () =>
            {
                callbackCount++;
                callbackStarted.TrySetResult();
                await continueCallback.Task;
            });
        });

        var firstClick = cut.InvokeAsync(() => cut.Find(".btn-danger").Click());
        try
        {
            await callbackStarted.Task.WaitAsync(TimeSpan.FromSeconds(5), TestContext.Current.CancellationToken);
            await cut.InvokeAsync(() => cut.Find(".btn-danger").Click());

            Assert.Equal(1, callbackCount);
        }
        finally
        {
            continueCallback.TrySetResult();
        }

        await firstClick;
    }

    [Fact]
    public void Show_Ok()
    {
        var cut = Context.Render<BootstrapBlazorRoot>(pb =>
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
            CloseButtonText = "test-button-text-Cancel",
            Class = "dialog-swal-test"
        }));

        // 代码覆盖模板单元测试
        Assert.Contains("Test-BodyTemplate", cut.Markup);
        Assert.Contains("Test-FooterTemplate", cut.Markup);
        Assert.Contains("Test-ButtonTemplate", cut.Markup);
        Assert.Contains("test-close-icon", cut.Markup);
        Assert.Contains("test-button-text-Cancel", cut.Markup);
        Assert.Contains("dialog-swal-test", cut.Markup);

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
            Category = SwalCategory.Information,
            ShowFooter = true,
            Footer = "I am Footer"
        }));
        Assert.Contains("swal2-info", cut.Markup);
        Assert.Contains("I am Footer", cut.Markup);
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
        cut.InvokeAsync(() =>
        {
            var button = cut.Find(".btn-secondary");
            button.Click();
        });
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
        bool confirmed = false;
        Task.Run(async () => await cut.InvokeAsync(async () =>
        {
            result = await swal.ShowModal(new SwalOption()
            {
                Content = "I am Modal Swal",
                CancelButtonText = "test-cancel-text",
                ConfirmButtonIcon = "test-confirm-icon",
                ConfirmButtonText = "test-confirm-text",
                OnConfirmAsync = () =>
                {
                    confirmed = true;
                    return Task.CompletedTask;
                }
            });
        }), CancellationToken.None);

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
        cut.InvokeAsync(() =>
        {
            var button = cut.Find(".btn-danger");
            button.Click();
            Assert.True(result);
            Assert.True(confirmed);
        });
        cut.InvokeAsync(() => modal.Instance.CloseCallback());

        // OnCloseAsync 测试
        Task.Run(async () => await cut.InvokeAsync(async () =>
        {
            result = await swal.ShowModal(new SwalOption()
            {
                Content = "I am Modal Swal",
                CancelButtonText = "test-cancel-text",
                ConfirmButtonIcon = "test-confirm-icon",
                ConfirmButtonText = "test-confirm-text",
                OnCloseAsync = () =>
                {
                    return Task.CompletedTask;
                }
            });
        }), CancellationToken.None);

        tick = DateTime.Now;
        while (!cut.Markup.Contains("test-cancel-text"))
        {
            Thread.Sleep(100);
            if (DateTime.Now > tick.AddSeconds(1))
            {
                break;
            }
        }

        // 触发关闭按钮
        cut.InvokeAsync(() =>
        {
            var button = cut.Find(".btn-secondary");
            button.Click();
        });
        cut.InvokeAsync(() => modal.Instance.CloseCallback());

        // 带确认框的 Select
        cut.Render(pb =>
        {
            pb.AddChildContent<Select<string>>(pb =>
            {
                pb.Add(a => a.Items, new List<SelectedItem>()
                {
                    new("1", "Test1"),
                    new("2", "Test2") { IsDisabled = true }
                });
                pb.Add(a => a.SwalCategory, SwalCategory.Question);
                pb.Add(a => a.SwalTitle, "Swal-Title");
                pb.Add(a => a.SwalContent, "Swal-Content");
                pb.Add(a => a.OnBeforeSelectedItemChange, item => Task.FromResult(true));
                pb.Add(a => a.OnSelectedItemChanged, item => Task.CompletedTask);
                pb.Add(a => a.SwalFooter, "test-swal-footer");
            });
        });

        Task.Run(() => cut.InvokeAsync(() => cut.FindComponent<Select<string>>().Instance.ConfirmSelectedItem(0)), CancellationToken.None);
        tick = DateTime.Now;
        while (!cut.Markup.Contains("test-swal-footer"))
        {
            Thread.Sleep(100);
            if (DateTime.Now > tick.AddSeconds(2))
            {
                break;
            }
        }
        cut.InvokeAsync(() =>
        {
            var button = cut.Find(".btn-danger");
            button.Click();
        });
        cut.InvokeAsync(() => modal.Instance.CloseCallback());

        // test force
        var forceOption = new SwalOption()
        {
            Content = "I am auto hide",
            ForceDelay = true,
            Delay = 1234
        };
        cut.InvokeAsync(() => swal.Show(forceOption));
        cut.InvokeAsync(() => modal.Instance.CloseCallback());
        Assert.NotEqual(4000, forceOption.Delay);
        Assert.Equal(1234, forceOption.Delay);

        forceOption.ForceDelay = false;
        cut.InvokeAsync(() => swal.Show(forceOption));
        cut.InvokeAsync(async () =>
        {
            await modal.Instance.CloseCallback();
            Assert.Equal(4000, forceOption.Delay);
        });

        // 自动关闭
        cut.InvokeAsync(() => swal.Show(new SwalOption()
        {
            Content = "I am auto hide",
            IsAutoHide = true,
            ForceDelay = true,
            Delay = 500,
            OnCloseAsync = () =>
            {
                return Task.CompletedTask;
            }
        }));
        Thread.Sleep(150);
        // 弹窗显示
        cut.Contains("I am auto hide");
        Thread.Sleep(1000);
        cut.InvokeAsync(() => modal.Instance.CloseCallback());

        // CloseAsync 方法测试
        var op = new SwalOption()
        {
            Content = "I am close swal",
            ShowClose = false
        };
        cut.InvokeAsync(() => swal.Show(op));
        cut.DoesNotContain("<button ");
        cut.InvokeAsync(() => op.CloseAsync());
        cut.InvokeAsync(() => modal.Instance.CloseCallback());

        // IsConfirm CloseAsync
        cut.InvokeAsync(() => swal.ShowModal(op));
        cut.InvokeAsync(() => op.CloseAsync());
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
        cut.InvokeAsync(async () => await alert.Instance.DisposeAsync());
        cut.InvokeAsync(() => modal.Instance.CloseCallback());
    }

    private class MockSwalTest : ComponentBase
    {
        [Inject]
        [NotNull]
        public SwalService? SwalService { get; set; }
    }
}
