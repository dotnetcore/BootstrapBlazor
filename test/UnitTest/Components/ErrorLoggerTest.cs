// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components.Web;

namespace UnitTest.Components;

public class ErrorLoggerTest : BootstrapBlazorTestBase
{
    [Fact]
    public void OnErrorAsync_Ok()
    {
        var cut = Context.RenderComponent<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<ErrorLogger>(pb =>
            {
                pb.Add(e => e.ShowToast, false);
                pb.AddChildContent<Button>(pb =>
                {
                    pb.Add(b => b.OnClick, () =>
                    {
                        var a = 0;
                        _ = 1 / a;
                    });
                });
            });
        });
        // 无 Swal 弹窗
        Assert.DoesNotContain("<div class=\"toast-header\">", cut.Markup);

        var errorLogger = cut.FindComponent<ErrorLogger>();
        errorLogger.SetParametersAndRender(pb =>
        {
            pb.Add(e => e.ShowToast, true);
        });
        var button = cut.Find("button");
        button.TriggerEvent("onclick", EventArgs.Empty);
    }

    [Fact]
    public async Task OnErrorHandleAsync_Ok()
    {
        var tcs = new TaskCompletionSource<bool>();
        var cut = Context.RenderComponent<ErrorLogger>(pb =>
        {
            pb.Add(e => e.OnErrorHandleAsync, (logger, exception) =>
            {
                tcs.SetResult(true);
                return Task.CompletedTask;
            });
            pb.AddChildContent<Button>(pb =>
            {
                pb.Add(b => b.OnClick, () =>
                {
                    var a = 0;
                    _ = 1 / a;
                });
            });
        });
        var button = cut.Find("button");
        button.TriggerEvent("onclick", EventArgs.Empty);
        var result = await tcs.Task;
        Assert.True(result);
    }

    [Fact]
    public void OnErrorHandleAsync_Tab()
    {
        var cut = Context.RenderComponent<BootstrapBlazorRoot>(pb =>
        {
            pb.Add(a => a.ChildContent, new RenderFragment(builder =>
            {
                builder.OpenComponent<Tab>(0);
                builder.AddAttribute(1, nameof(Tab.ChildContent), new RenderFragment(builder =>
                {
                    builder.OpenComponent<TabItem>(0);
                    builder.AddAttribute(1, nameof(TabItem.ChildContent), new RenderFragment(builder =>
                    {
                        builder.OpenComponent<Button>(0);
                        builder.AddAttribute(1, nameof(Button.Text), "errorlogger-click");
                        builder.AddAttribute(2, nameof(Button.OnClick), EventCallback.Factory.Create<MouseEventArgs>(this, e =>
                        {
                            var a = 0;
                            _ = 1 / a;
                        }));
                        builder.CloseComponent();
                    }));
                    builder.CloseComponent();
                }));
                builder.CloseComponent();
            }));
        });

        cut.Contains("errorlogger-click");
        var button = cut.Find("button");
        button.TriggerEvent("onclick", EventArgs.Empty);

        cut.Contains("<div class=\"tabs-body-content\"><div class=\"error-stack\">TimeStamp:");
    }

    [Fact]
    public void Root_Ok()
    {
        Exception? exception = null;
        var cut = Context.RenderComponent<BootstrapBlazorRoot>(pb =>
        {
            pb.Add(a => a.EnableErrorLogger, true);
            pb.Add(a => a.ShowToast, false);
            pb.Add(a => a.ToastTitle, "Test");
            pb.Add(a => a.OnErrorHandleAsync, (logger, ex) =>
            {
                exception = ex;
                return Task.CompletedTask;
            });
            pb.AddChildContent<Button>(pb =>
            {
                pb.Add(b => b.OnClick, () =>
                {
                    var a = 0;
                    _ = 1 / a;
                });
            });
        });
        var button = cut.Find("button");
        cut.InvokeAsync(() => button.Click());
        Assert.NotNull(exception);
    }
}
