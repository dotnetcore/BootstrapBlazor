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
    public async Task OnErrorAsync_Ok()
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
        await cut.InvokeAsync(() => button.Click());
    }

    [Theory]
    [InlineData("true")]
    [InlineData("false")]
    public async Task DetailedErrors_Ok(string detailedError)
    {
        var config = Context.Services.GetRequiredService<IConfiguration>();
        config["DetailedErrors"] = detailedError;

        var cut = Context.Render<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<ErrorLogger>(pb =>
            {
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
        var cut = Context.Render<BootstrapBlazorRoot>(pb =>
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
        await cut.InvokeAsync(() => button.Click());
        var result = await tcs.Task;
        Assert.True(result);

        // 由于自定义 OnErrorHandleAsync 组件对异常未处理
        cut.DoesNotContain("Attempted to divide by zero.");
    }

    [Fact]
    public async Task Tab_Error()
    {
        var cut = Context.Render<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<Tab>(pb =>
            {
                pb.AddChildContent<TabItem>(pb =>
                {
                    pb.AddChildContent<Button>(pb =>
                    {
                        pb.Add(a => a.Text, "errorLogger-click");
                        pb.Add(a => a.OnClick, EventCallback.Factory.Create<MouseEventArgs>(this, e =>
                        {
                            var a = 0;
                            _ = 1 / a;
                        }));
                    });
                });
            });
        });

        cut.Contains("errorLogger-click");
        var button = cut.Find("button");
        await cut.InvokeAsync(() => button.Click());

        // TabItem 内显示异常信息
        cut.Contains("Attempted to divide by zero.");
    }

    [Fact]
    public async Task Root_Ok()
    {
        Exception? exception = null;
        var cut = Context.Render<BootstrapBlazorRoot>(pb =>
        {
            pb.Add(a => a.EnableErrorLogger, true);
            pb.Add(a => a.EnableErrorLoggerILogger, true);
            pb.Add(a => a.ShowErrorLoggerToast, false);
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
        await cut.InvokeAsync(() => button.Click());
        Assert.NotNull(exception);

        cut.Render(pb =>
        {
            pb.Add(a => a.EnableErrorLogger, false);
        });
        button = cut.Find("button");
        await Assert.ThrowsAsync<DivideByZeroException>(async () => await cut.InvokeAsync(() => button.Click()));
    }

    [Fact]
    public async Task Toast_ErrorContent_Ok()
    {
        var cut = Context.Render<BootstrapBlazorRoot>(pb =>
        {
            pb.Add(a => a.EnableErrorLogger, true);
            pb.Add(a => a.ShowErrorLoggerToast, true);
            pb.AddChildContent<Button>(pb =>
            {
                pb.Add(b => b.OnClick, () =>
                {
                    var a = 0;
                    _ = 1 / a;
                });
            });
        });

        // ErrorContent 不为空与 Toast 内容合并显示
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
        await cut.InvokeAsync(() => button.Click());
        cut.Contains("Attempted to divide by zero.error_content_template");
    }

    [Fact]
    public async Task ErrorContent_Ok()
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

        var tab = cut.FindComponent<Tab>();
        var logger = tab.FindComponent<ErrorLogger>();
        logger.Render(pb =>
        {
            pb.Add(a => a.ErrorContent, new RenderFragment<Exception>(ex => builder =>
            {
                builder.AddContent(0, ex.Message);
                builder.AddContent(1, "error_content_template");
            }));
        });

        // 触发异常
        var button = cut.Find("button");
        await cut.InvokeAsync(() => button.Click());
        cut.Contains("Attempted to divide by zero.error_content_template");
    }

    [Fact]
    public async Task TabItem_Error()
    {
        var cut = Context.Render<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<Tab>(pb =>
            {
                pb.Add(a => a.EnableErrorLogger, true);
                pb.Add(a => a.EnableErrorLoggerILogger, true);
                pb.Add(a => a.ShowErrorLoggerToast, true);
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
        cut.Contains("Attempted to divide by zero.");
    }

    [Fact]
    public async Task TabItem_Production_Error()
    {
        var context = new BunitContext();
        context.JSInterop.Mode = JSRuntimeMode.Loose;
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

    [Fact]
    public void OnInitialize_Error()
    {
        var cut = Context.Render<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<MockInitializedError>();
        });

        cut.Contains("Attempted to divide by zero.");
    }

    [Fact]
    public void OnInitializeAsync_Error()
    {
        var cut = Context.Render<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<MockInitializedAsyncError>();
        });

        cut.Contains("Attempted to divide by zero.");
    }

    [Fact]
    public void OnParameterSet_Error()
    {
        var cut = Context.Render<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<MockOnParametersSetError>();
        });

        cut.Contains("Attempted to divide by zero.");
    }

    [Fact]
    public void OnParameterSetAsync_Error()
    {
        var cut = Context.Render<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<MockOnParameterSetAsyncError>();
        });

        cut.Contains("Attempted to divide by zero.");
    }

    [Fact]
    public void OnAfterRender_Error()
    {
        var cut = Context.Render<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<MockOnAfterRenderError>();
        });

        cut.Contains("Attempted to divide by zero.");
    }

    [Fact]
    public void OnAfterRenderAsync_Error()
    {
        var cut = Context.Render<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<MockOnAfterRenderAsyncError>();
        });

        cut.Contains("Attempted to divide by zero.");
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

    class MockInitializedError : ComponentBase
    {
        protected override void OnInitialized()
        {
            base.OnInitialized();

            ThrowError();
        }
    }

    class MockInitializedAsyncError : ComponentBase
    {
        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();

            ThrowError();
        }
    }

    class MockOnParametersSetError : ComponentBase
    {
        protected override void OnParametersSet()
        {
            base.OnParametersSet();

            ThrowError();
        }
    }

    class MockOnParameterSetAsyncError : ComponentBase
    {
        protected override async Task OnParametersSetAsync()
        {
            await base.OnParametersSetAsync();

            ThrowError();
        }
    }

    class MockOnAfterRenderError : ComponentBase
    {
        protected override void OnAfterRender(bool firstRender)
        {
            base.OnAfterRender(firstRender);

            ThrowError();
        }
    }

    class MockOnAfterRenderAsyncError : ComponentBase
    {
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);

            ThrowError();
        }
    }

    private static void ThrowError()
    {
        var a = 1;
        var b = 0;
        var c = a / b;
    }
}
