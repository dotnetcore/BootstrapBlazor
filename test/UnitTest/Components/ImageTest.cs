// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace UnitTest.Components;

public class ImageTest : BootstrapBlazorTestBase
{
    [Fact]
    public void ShowImage_Ok()
    {
        var cut = Context.RenderComponent<ImageViewer>(pb =>
        {
            pb.Add(a => a.PreviewIndex, 0);
            pb.Add(a => a.Url, "https://www.blazor.zone/images/logo.png");
            pb.Add(a => a.ZIndex, 2000);
            pb.Add(a => a.FitMode, ObjectFitMode.Fill);
        });
        cut.Contains("https://www.blazor.zone/images/logo.png");
    }

    [Fact]
    public void Alt_Ok()
    {
        var cut = Context.RenderComponent<ImageViewer>(pb =>
        {
            pb.Add(a => a.Url, "https://www.blazor.zone/images/logo.png");
            pb.Add(a => a.Alt, "alt-test");
        });
        cut.Contains("alt-test");
    }

    [Fact]
    public void ShouldRenderPlaceHolder_Ok()
    {
        var cut = Context.RenderComponent<ImageViewer>(pb =>
        {
            pb.Add(a => a.ShowPlaceHolder, true);
        });

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.ShowPlaceHolder, false);
            pb.Add(a => a.PlaceHolderTemplate, new RenderFragment(builder => builder.AddContent(0, "place-holder")));
        });
        cut.Contains("place-holder");
    }

    [Fact]
    public async Task OnLoad_Ok()
    {
        var load = false;
        var cut = Context.RenderComponent<ImageViewer>(pb =>
        {
            pb.Add(a => a.Url, "https://www.blazor.zone/images/logo.png");
            pb.Add(a => a.ShowPlaceHolder, true);
            pb.Add(a => a.OnLoadAsync, new Func<string, Task>(url =>
            {
                load = true;
                return Task.CompletedTask;
            }));
        });

        // trigger error event
        var img = cut.Find("img");
        await cut.InvokeAsync(() => img.Load());
        Assert.True(load);
    }

    [Fact]
    public void HandleError_Ok()
    {
        var error = false;
        var cut = Context.RenderComponent<ImageViewer>(pb =>
        {
            pb.Add(a => a.Url, "https://www.blazor.zone/images/logo1.png");
            pb.Add(a => a.HandleError, true);
            pb.Add(a => a.OnErrorAsync, new Func<string, Task>(url =>
            {
                error = true;
                return Task.CompletedTask;
            }));
        });
        cut.Contains("d-none");

        // trigger error event
        cut.InvokeAsync(() =>
        {
            var img = cut.Find("img");
            img.Error();
        });
        Assert.True(error);

        error = false;
        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.HandleError, false);
            pb.Add(a => a.ErrorTemplate, new RenderFragment(builder =>
            {
                builder.AddContent(0, "error-template");
            }));
        });

        cut.InvokeAsync(() =>
        {
            var img = cut.Find("img");
            img.Error();
        });
        Assert.True(error);
        cut.Contains("error-template");
    }

    [Fact]
    public void IsAsync_Ok()
    {
        var cut = Context.RenderComponent<ImageViewer>(pb =>
        {
            pb.Add(a => a.Url, "https://www.blazor.zone/images/logo.png");
            pb.Add(a => a.IsAsync, true);
            pb.Add(a => a.PreviewList, ["v1", "v2"]);
        });
        cut.Contains("bb-previewer collapse active");
    }

    [Fact]
    public void IsIntersectionObserver_Ok()
    {
        var cut = Context.RenderComponent<ImageViewer>(pb =>
        {
            pb.Add(a => a.Url, "https://www.blazor.zone/images/logo.png");
            pb.Add(a => a.IsIntersectionObserver, true);
        });
        cut.DoesNotContain("src");
    }

    [Fact]
    public void ImagerViewer_Show()
    {
        var cut = Context.RenderComponent<ImagePreviewer>(pb =>
        {
            pb.Add(a => a.PreviewList, ["v1", "v2"]);
        });
        cut.Instance.Show();
    }
}
