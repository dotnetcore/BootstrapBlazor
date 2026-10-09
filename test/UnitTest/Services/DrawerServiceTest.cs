// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.AspNetCore.Components.Rendering;

namespace UnitTest.Services;

public class DrawerServiceTest : BootstrapBlazorTestBase
{
    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public async Task ShowEditDrawer_CloseFailureStillRunsContainerCleanup(bool failEditClose)
    {
        var editCloseCount = 0;
        var userCloseCount = 0;
        var failure = new InvalidOperationException("Close failed");
        var option = new DrawerOption
        {
            OnCloseAsync = () =>
            {
                userCloseCount++;
                return failEditClose ? Task.CompletedTask : Task.FromException(failure);
            }
        };
        var editOption = new TableEditDrawerOption<Foo>
        {
            Model = new Foo(),
            ShowConfirmCloseSwal = false,
            OnCloseAsync = () =>
            {
                editCloseCount++;
                return failEditClose ? Task.FromException(failure) : Task.CompletedTask;
            }
        };
        var service = Context.Services.GetRequiredService<DrawerService>();
        var cut = Context.Render<BootstrapBlazorRoot>();
        await cut.InvokeAsync(() => service.ShowEditDrawer(editOption, option));
        var drawer = cut.FindComponent<Drawer>().Instance;
        var exception = await Assert.ThrowsAsync<InvalidOperationException>(() => cut.InvokeAsync(option.CloseAsync));
        Assert.Same(failure, exception);
        Assert.Equal(1, editCloseCount);
        Assert.Equal(1, userCloseCount);
        Assert.False(drawer.IsOpen);
        Assert.Empty(cut.FindComponents<Drawer>());
        Assert.Empty(cut.FindComponents<EditDialog<Foo>>());
    }

    [Fact]
    public async Task Show_CloseFailureStillRemovesDrawer()
    {
        var failure = new InvalidOperationException("Close failed");
        var option = new DrawerOption
        {
            OnCloseAsync = () => Task.FromException(failure)
        };
        var service = Context.Services.GetRequiredService<DrawerService>();
        var cut = Context.Render<BootstrapBlazorRoot>();
        await cut.InvokeAsync(() => service.Show(option));
        var exception = await Assert.ThrowsAsync<InvalidOperationException>(() => cut.InvokeAsync(option.CloseAsync));
        Assert.Same(failure, exception);
        Assert.Empty(cut.FindComponents<Drawer>());
    }

    [Fact]
    public async Task ShowEditDrawer_ReusesOptionWithoutAccumulatingCallbacks()
    {
        var closeCount = 0;
        var closingCount = 0;
        var firstEditCloseCount = 0;
        var secondEditCloseCount = 0;
        var content = RenderContent();
        var option = new DrawerOption
        {
            Width = "400px",
            ChildContent = content,
            OnClosingAsync = () =>
            {
                closingCount++;
                return Task.FromResult(true);
            },
            OnCloseAsync = () =>
            {
                closeCount++;
                return Task.CompletedTask;
            }
        };
        var originalClosing = option.OnClosingAsync;
        var originalClose = option.OnCloseAsync;
        var firstEdit = new TableEditDrawerOption<Foo>
        {
            Model = new Foo(),
            ShowConfirmCloseSwal = false,
            OnCloseAsync = () =>
            {
                firstEditCloseCount++;
                return Task.CompletedTask;
            }
        };
        var secondEdit = new TableEditDrawerOption<Foo>
        {
            Model = new Foo(),
            ShowConfirmCloseSwal = false,
            OnCloseAsync = () =>
            {
                secondEditCloseCount++;
                return Task.CompletedTask;
            }
        };
        var service = Context.Services.GetRequiredService<DrawerService>();
        var cut = Context.Render<BootstrapBlazorRoot>();
        await cut.InvokeAsync(() => service.ShowEditDrawer(firstEdit, option));
        var firstDrawer = cut.FindComponent<Drawer>().Instance;
        Assert.Equal("400px", firstDrawer.Width);
        await cut.InvokeAsync(option.CloseAsync);
        Assert.False(firstDrawer.IsOpen);

        option.Width = "500px";
        await cut.InvokeAsync(() => service.ShowEditDrawer(secondEdit, option));
        var secondDrawer = cut.FindComponent<Drawer>().Instance;
        Assert.NotSame(firstDrawer, secondDrawer);
        Assert.Equal("500px", secondDrawer.Width);
        await cut.InvokeAsync(option.CloseAsync);

        Assert.Equal(2, closingCount);
        Assert.Equal(2, closeCount);
        Assert.Equal(1, firstEditCloseCount);
        Assert.Equal(1, secondEditCloseCount);
        Assert.Same(originalClosing, option.OnClosingAsync);
        Assert.Same(originalClose, option.OnCloseAsync);
        Assert.Same(content, option.ChildContent);
        Assert.False(secondDrawer.IsOpen);

        await cut.InvokeAsync(() => service.Show(option));
        Assert.Empty(cut.FindComponents<EditDialog<Foo>>());
        cut.Contains("drawer-content");
        await cut.InvokeAsync(option.CloseAsync);
        Assert.Equal(3, closingCount);
        Assert.Equal(3, closeCount);
        Assert.Equal(1, firstEditCloseCount);
        Assert.Equal(1, secondEditCloseCount);

        await cut.InvokeAsync(() => service.ShowEditDrawer(firstEdit, option));
        Assert.Single(cut.FindComponents<EditDialog<Foo>>());
        await cut.InvokeAsync(option.CloseAsync);
        Assert.Equal(4, closingCount);
        Assert.Equal(4, closeCount);
        Assert.Equal(2, firstEditCloseCount);
        Assert.Equal(1, secondEditCloseCount);
    }

    [Fact]
    public async Task ShowEditDrawer_PreservesClosingAndClosedCallbacks()
    {
        var allowClose = false;
        var closingCount = 0;
        var closeCount = 0;
        var editCloseCount = 0;
        var option = new DrawerOption
        {
            OnClosingAsync = () =>
            {
                closingCount++;
                return Task.FromResult(allowClose);
            },
            OnCloseAsync = () =>
            {
                closeCount++;
                return Task.CompletedTask;
            }
        };
        var editOption = new TableEditDrawerOption<Foo>
        {
            Model = new Foo(),
            ShowConfirmCloseSwal = false,
            OnCloseAsync = () =>
            {
                editCloseCount++;
                return Task.CompletedTask;
            }
        };
        var service = Context.Services.GetRequiredService<DrawerService>();
        var cut = Context.Render<BootstrapBlazorRoot>();
        await cut.InvokeAsync(() => service.ShowEditDrawer(editOption, option));
        await cut.InvokeAsync(cut.FindComponent<Drawer>().Instance.Close);
        Assert.True(cut.FindComponent<Drawer>().Instance.IsOpen);
        Assert.Equal(1, closingCount);
        Assert.Equal(0, closeCount);
        Assert.Equal(0, editCloseCount);

        allowClose = true;
        await cut.InvokeAsync(cut.FindComponent<Drawer>().Instance.Close);
        Assert.Equal(2, closingCount);
        Assert.Equal(1, closeCount);
        Assert.Equal(1, editCloseCount);
    }

    [Fact]
    public async Task Show_Ok()
    {
        var option = new DrawerOption()
        {
            AllowResize = true,
            ChildContent = RenderContent(),
            Height = "100px",
            Width = "100px",
            IsBackdrop = true,
            OnClickBackdrop = () => Task.CompletedTask,
            OnCloseAsync = () => Task.CompletedTask,
            Placement = Placement.Bottom,
            ShowBackdrop = true,
            BodyContext = "test-body-context",
            IsKeyboard = true,
            BodyScroll = true,
            ZIndex = 1066
        };
        var service = Context.Services.GetRequiredService<DrawerService>();
        var cut = Context.Render<BootstrapBlazorRoot>();
        await service.Show(option);
        cut.Contains("data-bb-keyboard=\"true\"");
        cut.Contains("--bb-drawer-zindex: 1066;");
        var button = cut.Find("button");
        await cut.InvokeAsync(() => button.Click());

        option.ChildContent = null;
        option.Component = BootstrapDynamicComponent.CreateComponent<DialogCloseButton>();
        await service.Show(option);
        button = cut.Find("button");
        await cut.InvokeAsync(() => button.Click());

        option.Component = null;
        Assert.Null(option.GetContent());

        await service.Show<DrawerDemo>();
        button = cut.Find("button");
        await cut.InvokeAsync(() => button.Click());

        var type = typeof(DrawerDemo);
        await service.Show(type);
        button = cut.Find("button");
        await cut.InvokeAsync(() => button.Click());

        // 测试 Option 关闭方法
        Context.JSInterop.Setup<bool>("execute", matcher => true).SetResult(true);
        var closed = false;
        option.OnCloseAsync = () =>
        {
            closed = true;
            return Task.CompletedTask;
        };
        await service.Show(option);
        await cut.InvokeAsync(option.CloseAsync);
        Assert.True(closed);
    }

    [Fact]
    public async Task OnClosingAsync_Ok()
    {
        var closing = false;
        var closed = false;
        var option = new DrawerOption
        {
            OnClosingAsync = () =>
            {
                closing = true;
                return Task.FromResult(false);
            },
            OnCloseAsync = () =>
            {
                closed = true;
                return Task.CompletedTask;
            }
        };
        var service = Context.Services.GetRequiredService<DrawerService>();
        var cut = Context.Render<BootstrapBlazorRoot>();

        Assert.IsType<IClosable>(option, exactMatch: false);
        await service.Show(option);
        await cut.InvokeAsync(option.CloseAsync);

        Assert.True(closing);
        Assert.False(closed);
        Assert.Single(cut.FindComponents<Drawer>());
    }

    private static RenderFragment RenderContent() => builder =>
    {
        builder.AddContent(0, "drawer-content");
        builder.OpenComponent<DialogCloseButton>(0);
        builder.CloseComponent();
    };

    class DrawerDemo : ComponentBase
    {
        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            builder.AddContent(0, RenderContent());
        }
    }
}
