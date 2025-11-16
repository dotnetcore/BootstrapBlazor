// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.Extensions.Options;

namespace UnitTest.Components;

public class ToastTest : BootstrapBlazorTestBase
{
    [Fact]
    public void Placement_Ok()
    {
        var options = Context.Services.GetRequiredService<IOptionsMonitor<BootstrapBlazorOptions>>();
        options.CurrentValue.ToastPlacement = Placement.TopStart;

        var cut = Context.Render<BootstrapBlazorRoot>();

        var service = Context.Services.GetRequiredService<ToastService>();
        service.Success("Test", "test content");

        // 恢复设置
        options.CurrentValue.ToastPlacement = Placement.Auto;

        Assert.NotNull(cut.Instance.ToastContainer);
    }

    [Theory]
    [InlineData(Placement.TopStart, "top-0 start-0")]
    [InlineData(Placement.TopCenter, "top-0 start-50 translate-middle-x")]
    [InlineData(Placement.TopEnd, "top-0 end-0")]
    [InlineData(Placement.MiddleStart, "top-50 start-0 translate-middle-y")]
    [InlineData(Placement.MiddleCenter, "top-50 start-50 translate-middle")]
    [InlineData(Placement.MiddleEnd, "top-50 end-0 translate-middle-y")]
    [InlineData(Placement.BottomStart, "bottom-0 start-0")]
    [InlineData(Placement.BottomCenter, "bottom-0 start-50 translate-middle-x")]
    [InlineData(Placement.BottomEnd, "bottom-0 end-0")]
    public void SetPlacement_Ok(Placement placement, string css)
    {
        var cut = Context.Render<ToastContainer>();
        cut.InvokeAsync(() =>
        {
            cut.Instance.SetPlacement(placement);
            Assert.Contains(css, cut.Markup);
        });
    }

    [Fact]
    public async Task Options_Ok()
    {
        Context.Render<ToastContainer>();

        var service = Context.Services.GetRequiredService<ToastService>();
        var option = Context.Services.GetRequiredService<IOptionsMonitor<BootstrapBlazorOptions>>();
        await service.Show(new ToastOption()
        {
            ForceDelay = true
        });

        await service.Success("Test", "test content");

        await service.Error("Test", "test content");

        await service.Information("Test", "test content");

        option.CurrentValue.ToastDelay = 2000;
        await service.Warning("Test", "test content");
    }

    [Fact]
    public async Task PreventDuplicates_Ok()
    {
        Context.Render<ToastContainer>();

        var service = Context.Services.GetRequiredService<ToastService>();
        await service.Show(new ToastOption()
        {
            PreventDuplicates = true,
            Content = "Content"
        });
        await service.Show(new ToastOption()
        {
            PreventDuplicates = true,
            Title = "Title",
        });
        await service.Show(new ToastOption()
        {
            PreventDuplicates = true,
            Title = "Title",
            Content = "Content"
        });
        await service.Show(new ToastOption()
        {
            PreventDuplicates = true,
            Title = "Title",
            Content = "Content"
        });
    }

    [Fact]
    public async Task AutoHide_Ok()
    {
        var cut = Context.Render<ToastContainer>();
        var service = Context.Services.GetRequiredService<ToastService>();
        var option = new ToastOption()
        {
            IsAutoHide = false
        };
        await service.Show(option);
        await cut.InvokeAsync(() => option.Close());
    }

    [Fact]
    public async Task Animation_Ok()
    {
        Context.Render<ToastContainer>();
        var service = Context.Services.GetRequiredService<ToastService>();
        var option = new ToastOption()
        {
            Animation = false
        };
        await service.Show(option);
    }

    [Fact]
    public async Task ChildContent_Ok()
    {
        var cut = Context.Render<ToastContainer>();
        var service = Context.Services.GetRequiredService<ToastService>();
        var option = new ToastOption()
        {
            ChildContent = new RenderFragment(builder =>
            {
                builder.AddContent(0, "Toast ChildContent");
            })
        };
        await service.Show(option);
        await cut.InvokeAsync(() => option.Close());
    }

    [Fact]
    public async Task Close_Ok()
    {
        var cut = Context.Render<Toast>(pb =>
        {
            pb.Add(a => a.Options, new ToastOption());
        });
        await cut.InvokeAsync(() => cut.Instance.Close());

        var option = new ToastOption();
        await option.Close();
    }

    [Fact]
    public void ShowHeader_Ok()
    {
        var cut = Context.Render<Toast>(pb =>
        {
            pb.Add(a => a.Options, new ToastOption()
            {
                HeaderTemplate = builder =>
                {
                    builder.AddContent(0, "header-template");
                },
                ShowHeader = true
            });
        });
        Assert.Contains("toast-header", cut.Markup);
    }

    [Fact]
    public void ShowHeader_False()
    {
        var cut = Context.Render<Toast>(pb =>
        {
            pb.Add(a => a.Options, new ToastOption()
            {
                ShowHeader = false
            });
        });
        Assert.DoesNotContain("toast-header", cut.Markup);
    }

    [Fact]
    public void SuccessIcon_Ok()
    {
        var cut = Context.Render<Toast>(pb =>
        {
            pb.Add(a => a.Options, new ToastOption()
            {
                SuccessIcon = "success-icon"
            });
        });
        Assert.Contains("success-icon", cut.Markup);
    }

    [Fact]
    public void InfoIcon_Ok()
    {
        var cut = Context.Render<Toast>(pb =>
        {
            pb.Add(a => a.Options, new ToastOption()
            {
                Category = ToastCategory.Information,
                InformationIcon = "info-icon"
            });
        });
        Assert.Contains("info-icon", cut.Markup);
    }

    [Fact]
    public void WarningIcon_Ok()
    {
        var cut = Context.Render<Toast>(pb =>
        {
            pb.Add(a => a.Options, new ToastOption()
            {
                Category = ToastCategory.Warning,
                WarningIcon = "warning-icon"
            });
        });
        Assert.Contains("warning-icon", cut.Markup);
    }

    [Fact]
    public void ErrorIcon_Ok()
    {
        var cut = Context.Render<Toast>(pb =>
        {
            pb.Add(a => a.Options, new ToastOption()
            {
                Category = ToastCategory.Error,
                ErrorIcon = "error-icon"
            });
        });
        Assert.Contains("error-icon", cut.Markup);
    }

    [Fact]
    public async Task OnCloseAsync_Ok()
    {
        var close = false;
        var cut = Context.Render<ToastContainer>();
        var service = Context.Services.GetRequiredService<ToastService>();
        var option = new ToastOption()
        {
            OnCloseAsync = () =>
            {
                close = true;
                return Task.CompletedTask;
            }
        };
        await service.Show(option);
        await cut.InvokeAsync(() => option.Close());
        Assert.True(close);
    }
}
