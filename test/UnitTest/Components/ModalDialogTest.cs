// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Shared;
using System.Web;

namespace UnitTest.Components;

public class ModalDialogTest : BootstrapBlazorTestBase
{
    [Fact]
    public void ShowPrintButton_Ok()
    {
        var cut = Context.RenderComponent<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<Modal>(pb =>
            {
                pb.AddChildContent<ModalDialog>(pb =>
                {
                    pb.Add(d => d.ShowPrintButton, true);
                });
            });
        });
        // 显示在 Footer
        Assert.NotNull(cut.FindComponent<PrintButton>());

        var dialog = cut.FindComponent<ModalDialog>();
        dialog.SetParametersAndRender(pb =>
        {
            pb.Add(d => d.ShowPrintButtonInHeader, true);
        });
        // 显示在 Header
        Assert.NotNull(cut.FindComponent<PrintButton>());
    }

    [Fact]
    public void IsDraggable_Ok()
    {
        var cut = Context.RenderComponent<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<Modal>(pb =>
            {
                pb.AddChildContent<ModalDialog>(pb =>
                {
                    pb.Add(d => d.IsDraggable, true);
                    pb.Add(d => d.Class, "test_class");
                });
            });
        });
        Assert.Contains("is-draggable", cut.Markup);
        Assert.Contains("test_class", cut.Markup);
    }

    [Fact]
    public void ShowMaximizeButton_Ok()
    {
        var cut = Context.RenderComponent<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<Modal>(pb =>
            {
                pb.AddChildContent<ModalDialog>(pb =>
                {
                    pb.Add(d => d.ShowMaximizeButton, true);
                });
            });
        });
        Assert.Contains("btn-maximize", cut.Markup);

        var button = cut.Find(".btn-maximize");
        button.Click();
        Assert.Contains("modal-fullscreen", cut.Markup);

        button.Click();
        Assert.DoesNotContain("modal-fullscreen", cut.Markup);
    }

    [Fact]
    public void HeaderTemplate_Ok()
    {
        var cut = Context.RenderComponent<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<Modal>(pb =>
            {
                pb.AddChildContent<ModalDialog>(pb =>
                {
                    pb.Add(d => d.HeaderTemplate, builder => builder.AddContent(0, "HeaderTemplate"));
                });
            });
        });
        Assert.Contains("HeaderTemplate", cut.Markup);
    }

    [Fact]
    public void FooterTemplate_Ok()
    {
        var cut = Context.RenderComponent<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<Modal>(pb =>
            {
                pb.AddChildContent<ModalDialog>(pb =>
                {
                    pb.Add(d => d.FooterTemplate, builder => builder.AddContent(0, "FooterTemplate"));
                });
            });
        });
        Assert.Contains("FooterTemplate", cut.Markup);
    }

    [Fact]
    public void BodyContext_Ok()
    {
        var cut = Context.RenderComponent<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<Modal>(pb =>
            {
                pb.AddChildContent<ModalDialog>(pb =>
                {
                    pb.Add(d => d.BodyContext, new Foo() { Name = "Test_BodyContext" });
                    pb.Add(d => d.BodyTemplate, BootstrapDynamicComponent.CreateComponent<MockModalDialogContentComponent>().Render());
                });
            });
        });
        var content = cut.FindComponent<MockModalDialogContentComponent>().Instance;
        var f = content.Context as Foo;
        Assert.Equal("Test_BodyContext", f!.Name);
    }

    [Fact]
    public void OnSaveAsync_Ok()
    {
        var cut = Context.RenderComponent<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<Modal>(pb =>
            {
                pb.AddChildContent<ModalDialog>(pb =>
                {
                    pb.Add(d => d.ShowSaveButton, true);
                    pb.Add(d => d.IsAutoCloseAfterSave, true);
                    pb.Add(d => d.OnSaveAsync, () => Task.FromResult(true));
                });
            });
        });
        Assert.Contains("保存", HttpUtility.HtmlDecode(cut.Markup));

        var b = cut.FindComponents<Button>().Last();
        cut.InvokeAsync(() => b.Instance.OnClickWithoutRender!());
    }

    private class MockModalDialogContentComponent : ComponentBase
    {
        [CascadingParameter(Name = "BodyContext")]
        public object? Context { get; set; }
    }
}
