// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.Configuration;

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
    public async Task DetailedErrors_False()
    {
        var config = Context.Services.GetRequiredService<IConfiguration>();
        config["DetailedErrors"] = "false";

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
        var button = cut.Find("button");
        await cut.InvokeAsync(() => button.Click());
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
                        builder.AddAttribute(1, nameof(Button.Text), "errorLogger-click");
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

        cut.Contains("errorLogger-click");
        var button = cut.Find("button");
        button.TriggerEvent("onclick", EventArgs.Empty);
        cut.Contains("<div class=\"modal-body\"><div class=\"error-stack\">TimeStamp:");
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

    [Fact]
    public void ErrorContent_Ok()
    {
        var cut = Context.RenderComponent<BootstrapBlazorRoot>(pb =>
        {
            pb.Add(a => a.EnableErrorLogger, true);
            pb.Add(a => a.ShowToast, false);
            pb.AddChildContent<Button>(pb =>
            {
                pb.Add(b => b.OnClick, () =>
                {
                    var a = 0;
                    _ = 1 / a;
                });
            });
        });

        var errorLogger = cut.FindComponent<ErrorLogger>();
        errorLogger.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.ErrorContent, new RenderFragment<Exception>(ex => builder =>
            {
                builder.AddContent(0, ex.Message);
                builder.AddContent(1, "error_content_template");
            }));
        });
        var button = cut.Find("button");
        cut.InvokeAsync(() => button.Click());
        cut.Contains("Attempted to divide by zero.error_content_template");
    }
}
