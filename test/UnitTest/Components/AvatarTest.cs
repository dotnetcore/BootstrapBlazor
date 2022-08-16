// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace UnitTest.Components;

public class AvatarTest : TestBase
{
    [Fact]
    public void IsCircle_Ok()
    {
        var cut = Context.RenderComponent<Avatar>(builder => builder.Add(a => a.IsCircle, true));
        Assert.Contains("avatar-circle", cut.Markup);
    }

    [Fact]
    public void Url_Ok()
    {
        var url = "_content/BootstrapBlazor.Shared/images/Argo-C.png";
        var cut = Context.RenderComponent<Avatar>(builder =>
        {
            builder.Add(a => a.Url, url);
            builder.Add(a => a.IsBorder, true);
        });
        Assert.Contains($"src=\"{url}\"", cut.Markup);

        // handler event
        var img = cut.Find("img");
        img.TriggerEvent("onerror", new EventArgs());
        Assert.Contains("border-danger", cut.Markup);
    }

    [Fact]
    public void IsIcon_Ok()
    {
        var cut = Context.RenderComponent<Avatar>(builder =>
        {
            builder.Add(a => a.IsIcon, true);
            builder.Add(a => a.Icon, "fa fa-fa");
        });
        Assert.Contains("fa fa-fa", cut.Markup);
        Assert.True(cut.Instance.IsIcon);
    }

    [Fact]
    public void IsText_Ok()
    {
        var cut = Context.RenderComponent<Avatar>(builder =>
        {
            builder.Add(a => a.IsText, true);
            builder.Add(a => a.Text, "user");
        });
        Assert.Contains("user", cut.Markup);
        Assert.True(cut.Instance.IsText);
    }

    [Fact]
    public void Size_Ok()
    {
        var cut = Context.RenderComponent<Avatar>(builder => builder.Add(a => a.Size, Size.None));
        Assert.DoesNotContain("avatar-lg", cut.Markup);

        cut = Context.RenderComponent<Avatar>(builder => builder.Add(a => a.Size, Size.Large));
        Assert.Contains("avatar-lg", cut.Markup);
    }

    [Fact]
    public void IsBorder_Ok()
    {
        var cut = Context.RenderComponent<Avatar>(builder => builder.Add(a => a.IsBorder, true));
        Assert.Contains("border", cut.Markup);
    }

    [Fact]
    public void CustomerClass_Ok()
    {
        var cut = Context.RenderComponent<Avatar>(builder => builder.AddUnmatched("class", "is-test"));
        Assert.Contains("<span class=\"avatar is-test\"", cut.Markup);
    }

    [Fact]
    public void GetUrlAsync_Ok()
    {
        var url = "_content/BootstrapBlazor.Shared/images/Argo-C.png";
        var cut = Context.RenderComponent<Avatar>(builder =>
        {
            builder.Add(a => a.GetUrlAsync, () => Task.FromResult(url));
            builder.Add(a => a.IsBorder, true);
        });
        Assert.Contains($"src=\"{url}\"", cut.Markup);

        // handler event
        var img = cut.Find("img");
        img.TriggerEvent("onload", new EventArgs());
        Assert.Contains("border-success", cut.Markup);
    }
}
