// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

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
}
