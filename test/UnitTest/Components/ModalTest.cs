// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.AspNetCore.Components.Rendering;

namespace UnitTest.Components;

public class ModalTest : BootstrapBlazorTestBase
{
    [Fact]
    public void IsBackdrop_Ok()
    {
        var cut = Context.Render<BootstrapBlazorRoot>(builder =>
        {
            builder.AddChildContent<Modal>(pb =>
            {
                pb.Add(m => m.IsBackdrop, true);
                pb.Add(m => m.IsFade, false);
                pb.Add(m => m.AdditionalAttributes, new Dictionary<string, object> { { "class", "backdrop" } });
            });
        });

        var modal = cut.Find(".backdrop");
        Assert.Null(modal.GetAttribute("data-bs-backdrop"));

        var m = cut.FindComponent<Modal>();
        m.Render(pb =>
        {
            pb.Add(m => m.IsBackdrop, false);
        });
        modal = cut.Find(".backdrop");
        Assert.Equal("static", modal.GetAttribute("data-bs-backdrop"));
    }

    [Fact]
    public async Task Toggle_Ok()
    {
        var container = Context.Render<BootstrapBlazorRootOutlet>();
        var cut = Context.Render<Modal>(pb =>
        {
            pb.AddChildContent<ModalDialog>();
            pb.Add(m => m.AdditionalAttributes, new Dictionary<string, object> { { "class", "backdrop" } });
        });

        await cut.InvokeAsync(cut.Instance.Toggle);
        Assert.Contains("modal fade backdrop", container.Markup);
    }

    [Fact]
    public async Task Close_Ok()
    {
        var cut = Context.Render<BootstrapBlazorRoot>(builder =>
        {
            builder.AddChildContent<Modal>(pb =>
            {
                pb.Add(m => m.AdditionalAttributes, new Dictionary<string, object> { { "class", "backdrop" } });
            });
        });

        var modal = cut.FindComponent<Modal>();
        await cut.InvokeAsync(modal.Instance.Close);

        modal.Render(pb =>
        {
            pb.AddChildContent<ModalDialog>();
        });
        await cut.InvokeAsync(modal.Instance.Close);

        // 多弹窗
        modal.Render(pb =>
        {
            pb.AddChildContent(builder =>
            {
                builder.OpenComponent<ModalDialog>(0);
                builder.CloseComponent();
                builder.OpenComponent<ModalDialog>(1);
                builder.CloseComponent();
            });
        });
        await modal.InvokeAsync(modal.Instance.Close);
    }

    [Fact]
    public async Task SetHeaderText_Ok()
    {
        var cut = Context.Render<BootstrapBlazorRoot>(builder =>
        {
            builder.AddChildContent<Modal>(pb =>
            {
                pb.AddChildContent<ModalDialog>();
                pb.Add(m => m.AdditionalAttributes, new Dictionary<string, object> { { "class", "backdrop" } });
            });
        });

        var header = cut.Find(".backdrop .modal-title");
        Assert.Equal("", header.TextContent);

        var modal = cut.FindComponent<Modal>();
        await cut.InvokeAsync(() => modal.Instance.SetHeaderText("Test-Header"));
        header = cut.Find(".backdrop .modal-title");
        Assert.Equal("Test-Header", header.TextContent);
    }

    [Fact]
    public void SetHeaderText_Null()
    {
        var cut = Context.Render<BootstrapBlazorRoot>(builder =>
        {
            builder.AddChildContent<MockModal>(pb =>
            {
                pb.AddChildContent<ModalDialog>();
                pb.Add(m => m.AdditionalAttributes, new Dictionary<string, object> { { "class", "backdrop" } });
            });
        });

        var modal = cut.FindComponent<MockModal>();
        modal.Instance.TestSetHeaderText();
    }

    [Fact]
    public async Task ShownCallbackAsync_Ok()
    {
        var cut = Context.Render<BootstrapBlazorRoot>(builder =>
        {
            builder.AddChildContent<MockComponent>();
        });

        var mock = Context.Render<MockComponent>();
        var modal = mock.FindComponent<MockModal>();
        await cut.InvokeAsync(() => modal.Instance.Show_Test());

        Assert.True(mock.Instance.Value);
    }

    [Fact]
    public void FirstAfterRenderAsync_Ok()
    {
        var render = false;
        var cut = Context.Render<BootstrapBlazorRoot>(builder =>
        {
            builder.AddChildContent<Modal>(pb =>
            {
                pb.Add(a => a.FirstAfterRenderCallbackAsync, modal =>
                {
                    render = true;
                    return Task.CompletedTask;
                });
                pb.AddChildContent<ModalDialog>();
            });
        });
        Assert.True(render);
    }

    [Fact]
    public async Task RegisterShownCallback_Ok()
    {
        var cut = Context.Render<BootstrapBlazorRoot>(builder =>
        {
            builder.AddChildContent<Modal>(pb =>
            {
                pb.AddChildContent<MockFocusComponent>();
            });
        });

        var modal = cut.FindComponent<Modal>();
        var component = cut.FindComponent<MockFocusComponent>();
        Assert.False(component.Instance.Pass);

        await cut.InvokeAsync(modal.Instance.ShownCallback);
        Assert.True(component.Instance.Pass);
    }

    [Fact]
    public async Task OnClosingAsync_Ok()
    {
        var closing = false;
        var cut = Context.Render<Modal>(builder =>
        {
            builder.Add(a => a.OnClosingAsync, async () =>
            {
                closing = true;
                await Task.Yield();
                return true;
            });
            builder.AddChildContent<ModalDialog>(pb =>
            {

            });
        });
        await cut.InvokeAsync(() => cut.Instance.Close());
        Assert.True(closing);
    }

    [Fact]
    public async Task RegisterOnClosingAsync_Ok()
    {
        var closing = false;
        var cut = Context.Render<Modal>(builder =>
        {
            builder.AddChildContent<ModalDialog>(pb =>
            {

            });
        });

        var closingHandler = async () =>
        {
            closing = true;
            await Task.Yield();
            return true;
        };
        cut.Instance.RegisterOnClosingCallback(closingHandler);
        await cut.InvokeAsync(() => cut.Instance.Close());
        cut.Instance.UnRegisterOnClosingCallback(closingHandler);
        Assert.True(closing);
    }

    private class MockComponent : ComponentBase
    {
        public bool Value { get; set; }

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            builder.OpenComponent<MockModal>(0);
            builder.AddAttribute(1, nameof(Modal.OnShownAsync), () =>
            {
                Value = true;
                return Task.CompletedTask;
            });
            builder.CloseComponent();
        }
    }

    private class MockModal : Modal
    {
        public async Task Show_Test()
        {
            await base.ShownCallback();
        }

        public void TestSetHeaderText()
        {
            Dialogs.Clear();
            base.SetHeaderText("");
        }
    }

    private class MockFocusComponent : ComponentBase, IDisposable
    {
        [CascadingParameter, NotNull]
        private Modal? Modal { get; set; }

        protected override void OnInitialized()
        {
            base.OnInitialized();

            Modal?.RegisterShownCallback(this, TestCallback);
            Modal?.RegisterShownCallback(this, TestCallback);
        }

        public bool Pass { get; set; }

        public Task TestCallback()
        {
            Pass = true;
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            Modal?.UnRegisterShownCallback(this);
            GC.SuppressFinalize(this);
        }
    }
}
