// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Bunit;

namespace UnitTest.Components;

public class IconDialogTest : BootstrapBlazorTestBase
{
    [Fact]
    public void IconName_Ok()
    {
        var cut = Context.RenderComponent<IconDialog>(pb =>
        {
            pb.Add(a => a.IconName, "fa-icon-name");
        });
        cut.Contains("fa-icon-name");
    }

    [Fact]
    public void Text_Ok()
    {
        var cut = Context.RenderComponent<IconDialog>(pb =>
        {
            pb.Add(a => a.LabelText, "test-label-text");
            pb.Add(a => a.LabelFullText, "test-label-full-text");
            pb.Add(a => a.ButtonText, "test-button-text");
        });
        cut.Contains("test-label-text");
        cut.Contains("test-label-full-text");
        cut.Contains("test-button-text");
    }

    [Fact]
    public async Task IconStyle_Ok()
    {
        var cut = Context.RenderComponent<IconDialog>(pb =>
        {
            pb.Add(a => a.IconName, "fas fa-bell");
        });

        var list = cut.FindComponent<RadioList<string>>();
        await cut.InvokeAsync(() => list.Instance.SetValue("regular"));
        Assert.Equal("fa-regular fa-bell", cut.Instance.IconName);

        await cut.InvokeAsync(() => list.Instance.SetValue("solid"));
        Assert.Equal("fa-solid fa-bell", cut.Instance.IconName);
    }
}
