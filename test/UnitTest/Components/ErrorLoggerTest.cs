// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;

namespace UnitTest.Components;

public class ErrorLoggerTest : BootstrapBlazorTestBase
{
    [Fact]
    public void OnErrorAsync_Ok()
    {
        var cut = Context.Render<BootstrapBlazorRoot>(pb =>
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
        errorLogger.Render(pb =>
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

        var cut = Context.Render<BootstrapBlazorRoot>(pb =>
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
        var cut = Context.Render<ErrorLogger>(pb =>
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
        var cut = Context.Render<BootstrapBlazorRoot>(pb =>
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

        // TabItem 内显示异常信息
        cut.Contains("error-stack");
    }

    [Fact]
    public void Root_Ok()
    {
        Exception? exception = null;
        var cut = Context.Render<BootstrapBlazorRoot>(pb =>
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
        var cut = Context.Render<BootstrapBlazorRoot>(pb =>
        {
            pb.Add(a => a.EnableErrorLogger, true);
            pb.Add(a => a.ShowToast, true);
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
        errorLogger.Render(pb =>
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

    [Fact]
    public async Task TabItem_Error()
    {
        var cut = Context.Render<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<Tab>(pb =>
            {
                pb.AddChildContent<TabItem>(pb =>
                {
                    pb.Add(a => a.Text, "Text1");
                    pb.Add(a => a.ChildContent, builder => builder.AddContent(0, RenderButton()));
                });
            });
        });

        var button = cut.Find("button");
        await cut.InvokeAsync(() => button.Click());

        // 页面不崩溃，由弹窗显示异常信息
        cut.Contains("<div class=\"error-stack\">TimeStamp:");

        // 单元测试覆盖 TabItemContent Dispose 方法
        var handler = Activator.CreateInstance("BootstrapBlazor", "BootstrapBlazor.Components.TabItemContent");
        Assert.NotNull(handler);
        var content = handler.Unwrap();
        Assert.NotNull(content);

        Assert.IsType<IDisposable>(content, exactMatch: false);
        ((IDisposable)content).Dispose();
    }

    [Fact]
    public async Task TabItem_Production_Error()
    {
        var context = new BunitContext();
        context.JSInterop.Mode = JSRuntimeMode.Loose;
        context.Services.AddSingleton<IHostEnvironment, MockProductionEnironment>();
        context.Services.AddBootstrapBlazor();
        context.Services.GetRequiredService<ICacheManager>();

        var cut = context.Render<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<Tab>(pb =>
            {
                pb.AddChildContent<TabItem>(pb =>
                {
                    pb.Add(a => a.Text, "Text1");
                    pb.Add(a => a.ChildContent, builder => builder.AddContent(0, RenderButton()));
                });
            });
        });
        var button = cut.Find("button");
        await cut.InvokeAsync(() => button.Click());
    }

    private RenderFragment RenderButton() => builder =>
    {
        builder.OpenComponent<Button>(0);
        builder.AddAttribute(2, nameof(Button.OnClick), EventCallback.Factory.Create<MouseEventArgs>(this, e =>
        {
            var a = 0;
            _ = 1 / a;
        }));
        builder.CloseComponent();
    };

    class MockProductionEnironment : IHostEnvironment
    {
        public string EnvironmentName { get; set; } = "Production";

        public string ApplicationName { get; set; } = "Test";

        public string ContentRootPath { get; set; } = "UniTest";

        public IFileProvider ContentRootFileProvider { get; set; } = null!;
    }
}
