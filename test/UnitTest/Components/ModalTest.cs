// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components.Rendering;

namespace UnitTest.Components;

public class ModalTest : BootstrapBlazorTestBase
{
    [Fact]
    public void IsBackdrop_Ok()
    {
        var cut = Context.RenderComponent<Modal>(pb =>
        {
            pb.Add(m => m.IsBackdrop, true);
            pb.Add(m => m.IsFade, false);
        });
        Assert.DoesNotContain("static", cut.Markup);

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(m => m.IsBackdrop, false);
        });
        Assert.Contains("static", cut.Markup);
    }

    [Fact]
    public void Toggle_Ok()
    {
        var cut = Context.RenderComponent<Modal>(pb =>
        {
            pb.AddChildContent<ModalDialog>();
        });
        cut.InvokeAsync(async () => await cut.Instance.Toggle());
        Assert.Contains("modal-dialog", cut.Markup);
    }

    [Fact]
    public void Close_Ok()
    {
        var cut = Context.RenderComponent<Modal>();
        cut.InvokeAsync(async () => await cut.Instance.Close());

        cut.SetParametersAndRender(pb =>
        {
            pb.AddChildContent<ModalDialog>();
        });
        cut.InvokeAsync(async () => await cut.Instance.Close());

        // 多弹窗
        cut.SetParametersAndRender(pb =>
        {
            pb.AddChildContent(builder =>
            {
                builder.OpenComponent<ModalDialog>(0);
                builder.CloseComponent();
                builder.OpenComponent<ModalDialog>(1);
                builder.CloseComponent();
            });
        });
        cut.InvokeAsync(async () => await cut.Instance.Close());
    }

    [Fact]
    public async Task SetHeaderText_Ok()
    {
        var cut = Context.RenderComponent<Modal>(pb =>
        {
            pb.AddChildContent<ModalDialog>();
        });
        var header = cut.Find(".modal-title");
        Assert.Equal("", header.TextContent);

        await cut.InvokeAsync(() => cut.Instance.SetHeaderText("Test-Header"));
        header = cut.Find(".modal-title");
        Assert.Equal("Test-Header", header.TextContent);
    }

    [Fact]
    public void SetHeaderText_Null()
    {
        var cut = Context.RenderComponent<MockModal>(pb =>
        {
            pb.AddChildContent<ModalDialog>();
        });
        cut.Instance.TestSetHeaderText();
    }

    [Fact]
    public async Task ShownCallbackAsync_Ok()
    {
        var cut = Context.RenderComponent<MockComponent>();
        var modal = cut.FindComponent<MockModal>();
        await cut.InvokeAsync(() => modal.Instance.Show_Test());

        Assert.True(cut.Instance.Value);
    }

    [Fact]
    public void FirstAfterRenderAsync_Ok()
    {
        var render = false;
        var cut = Context.RenderComponent<Modal>(pb =>
        {
            pb.Add(a => a.FirstAfterRenderCallbackAsync, modal =>
            {
                render = true;
                return Task.CompletedTask;
            });
            pb.AddChildContent<ModalDialog>();
        });
        Assert.True(render);

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
}
