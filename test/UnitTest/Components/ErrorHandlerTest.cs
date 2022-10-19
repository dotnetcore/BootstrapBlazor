// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components.Web;

namespace UnitTest.Components;

public class ErrorHandlerTest : ErrorLoggerTestBase
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
        var btn = cut.Find(".btn-close");
        await cut.InvokeAsync(() => btn.Click());

        cut.Contains("<div class=\"toast-body\">test error logger</div>");

        // 关闭 Toast
        var toast = cut.FindComponent<Toast>().Instance;
        await cut.InvokeAsync(() => toast.Close());
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
