// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components.Web;

namespace UnitTest.Components;

public class ErrorHandlerTest : BootstrapBlazorTestBase
{
    [Fact]
    public async Task HandlerException_Ok()
    {
        var cut = Context.RenderComponent<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<MockDialogTest>();
        });
        var dialog = cut.FindComponent<MockDialogTest>().Instance.DialogService;

        await cut.InvokeAsync(() => dialog.Show(new DialogOption()
        {
            BodyTemplate = BootstrapDynamicComponent.CreateComponent<ErrorComponent>().Render()
        }));
        var errorButton = cut.Find(".btn-error");
        await cut.InvokeAsync(() => errorButton.Click());
        Assert.Contains("<div class=\"modal-body\"><div class=\"error-stack\">", cut.Markup);

        // 关闭弹窗
        var btn = cut.Find(".modal-header .btn-close");
        await cut.InvokeAsync(() => btn.Click());
    }

    [Fact]
    public async Task ShowToast_Ok()
    {
        var cut = Context.RenderComponent<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<ErrorComponent>();
        });
        var errorButton = cut.Find(".btn-error");
        await cut.InvokeAsync(() => errorButton.Click());
        cut.Contains("<div class=\"toast-body\">test error logger</div>");

        // 关闭 Toast
        var toast = cut.FindComponent<Toast>().Instance;
        await cut.InvokeAsync(() => toast.Close());
        cut.DoesNotContain("<div class=\"toast-body\">test error logger</div>");

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.ShowToast, false);
        });
        errorButton = cut.Find(".btn-error");
        await cut.InvokeAsync(() => errorButton.Click());
        cut.DoesNotContain("<div class=\"toast-body\">test error logger</div>");
    }

    private class MockDialogTest : ComponentBase
    {
        [Inject]
        [NotNull]
        public DialogService? DialogService { get; set; }
    }

    private class ErrorComponent : ComponentBase
    {
        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            builder.OpenElement(0, "div");
            builder.AddContent(1, pb =>
            {
                pb.OpenComponent<Button>(1);
                pb.AddAttribute(2, nameof(Button.Text), "Error");
                pb.AddAttribute(3, "class", "btn-error");
                pb.AddAttribute(4, nameof(Button.OnClick), EventCallback.Factory.Create<MouseEventArgs>(this, e =>
                {
                    throw new Exception("test error logger");
                }));
                pb.CloseComponent();
            });
            builder.CloseElement();
        }
    }
}
