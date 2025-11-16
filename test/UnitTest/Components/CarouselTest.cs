// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace UnitTest.Components;

public class CarouselTest : BootstrapBlazorTestBase
{
    [Fact]
    public void Images_Ok()
    {
        var cut = Context.Render<Carousel>();
        Assert.Contains("carousel slide", cut.Markup);
    }

    [Fact]
    public void Width_Ok()
    {
        var cut = Context.Render<Carousel>(pb =>
        {
            pb.Add(b => b.Width, "100");
        });

        Assert.Contains("width: 100px;", cut.Markup);

        cut.Render(pb =>
        {
            pb.Add(b => b.Width, "100%");
        });
        Assert.Contains("width: 100%;", cut.Markup);

        cut.Render(pb =>
        {
            pb.Add(b => b.Width, "auto");
        });
        Assert.Contains("width: auto", cut.Markup);
    }

    [Fact]
    public void Interval_Ok()
    {
        var cut = Context.Render<Carousel>(pb =>
        {
            pb.Add(b => b.ChildContent, new RenderFragment(builder =>
            {
                builder.OpenComponent<CarouselItem>(0);
                builder.AddAttribute(1, nameof(CarouselItem.ChildContent), new RenderFragment(builder => builder.AddContent(0, "Test-1")));
                builder.AddAttribute(2, nameof(CarouselItem.Interval), 5000);
                builder.CloseComponent();

                builder.OpenComponent<CarouselItem>(2);
                builder.AddAttribute(3, nameof(CarouselItem.ChildContent), new RenderFragment(builder => builder.AddContent(0, "Test-2")));
                builder.AddAttribute(4, nameof(CarouselItem.Interval), 2000);
                builder.CloseComponent();
            }));
        });

        Assert.DoesNotContain("data-bs-interval=\"5000\"", cut.Markup);
        Assert.Contains("data-bs-interval=\"2000\"", cut.Markup);
    }

    [Fact]
    public void DisableTouchSwiping_Ok()
    {
        var cut = Context.Render<Carousel>(pb =>
        {
            pb.Add(b => b.DisableTouchSwiping, true);
        });

        Assert.Contains("data-bs-touch=\"false\"", cut.Markup);
    }

    [Fact]
    public void IsFade_Ok()
    {
        var cut = Context.Render<Carousel>(pb =>
        {
            pb.Add(b => b.IsFade, false);
        });
        Assert.DoesNotContain("carousel-fade", cut.Markup);

        cut.Render(pb =>
        {
            pb.Add(b => b.IsFade, true);
        });
        Assert.Contains("carousel-fade", cut.Markup);
    }

    [Fact]
    public void ShowControls_Ok()
    {
        var cut = Context.Render<Carousel>(pb =>
        {
            pb.Add(b => b.ShowControls, false);
        });
        Assert.DoesNotContain("carousel-control-prev", cut.Markup);
        Assert.DoesNotContain("carousel-control-next", cut.Markup);
    }

    [Fact]
    public void ShowIndicators_Ok()
    {
        var cut = Context.Render<Carousel>(pb =>
        {
            pb.Add(b => b.ShowIndicators, false);
        });
        Assert.DoesNotContain("carousel-indicators", cut.Markup);
    }

    [Fact]
    public void Caption_Ok()
    {
        var cut = Context.Render<Carousel>(pb =>
        {
            pb.Add(b => b.ChildContent, new RenderFragment(builder =>
            {
                builder.OpenComponent<CarouselItem>(0);
                builder.AddAttribute(1, nameof(CarouselItem.ChildContent), new RenderFragment(builder => builder.AddContent(0, "Test-1")));
                builder.AddAttribute(2, nameof(CarouselItem.Caption), "test-item1-caption");
                builder.AddAttribute(2, nameof(CarouselItem.CaptionClass), "test-item1-class-caption");
                builder.CloseComponent();

                builder.OpenComponent<CarouselItem>(10);
                builder.AddAttribute(11, nameof(CarouselItem.ChildContent), new RenderFragment(builder => builder.AddContent(0, "Test-2")));
                builder.AddAttribute(12, nameof(CarouselItem.CaptionTemplate), new RenderFragment(builder => builder.AddContent(0, "test-item2-caption-template")));
                builder.CloseComponent();
            }));
        });
        cut.Contains("test-item1-caption");
        cut.Contains("test-item1-class-caption");
        cut.Contains("test-item2-caption-template");
    }

    [Fact]
    public void OnClick_Ok()
    {
        var url = "";
        var cut = Context.Render<Carousel>(pb =>
        {
            pb.Add(b => b.Images, new List<string>() { "test1.jpg", null!, "test3.jpg", "test4.jpg" });
        });
        cut.InvokeAsync(() => cut.Find("img").Click());

        cut.Render(pb =>
        {
            pb.Add(b => b.OnClick, v =>
            {
                url = v;
                return Task.CompletedTask;
            });
        });
        cut.InvokeAsync(() => cut.Find("img").Click());
        Assert.Equal("test1.jpg", url);

        cut.InvokeAsync(() => cut.FindAll("img")[1].Click());
        Assert.Equal("", url);
    }

    [Fact]
    public void OnSlideChanged_Ok()
    {
        var index = 0;
        var cut = Context.Render<Carousel>(pb =>
        {
            pb.Add(b => b.Images, new List<string>() { "test1.jpg", null!, "test3.jpg", "test4.jpg" });
            pb.Add(b => b.OnSlideChanged, i =>
            {
                index = i;
                return Task.CompletedTask;
            });
        });
        cut.InvokeAsync(() => cut.Instance.TriggerSlideChanged(1));
        Assert.Equal(1, index);
    }

    [Fact]
    public void CarouselItem_Ok()
    {
        var cut = Context.Render<Carousel>(pb =>
        {
            pb.Add(b => b.ChildContent, new RenderFragment(builder =>
            {
                builder.OpenComponent<CarouselItem>(0);
                builder.AddAttribute(1, nameof(CarouselItem.ChildContent), new RenderFragment(builder => builder.AddContent(0, "Test-1")));
                builder.CloseComponent();

                builder.OpenComponent<CarouselItem>(2);
                builder.AddAttribute(3, nameof(CarouselItem.ChildContent), new RenderFragment(builder => builder.AddContent(0, "Test-2")));
                builder.CloseComponent();
            }));
        });

        Assert.Contains("Test-1", cut.Markup);
        Assert.Contains("Test-2", cut.Markup);

        cut.Render(pb =>
        {
            pb.Add(b => b.ChildContent, new RenderFragment(builder =>
            {
                builder.OpenComponent<CarouselItem>(0);
                builder.AddAttribute(1, nameof(CarouselItem.ChildContent), new RenderFragment(builder => builder.AddContent(0, "Test-1")));
                builder.CloseComponent();
            }));
        });

        Assert.Contains("Test-1", cut.Markup);
        Assert.DoesNotContain("Test-2", cut.Markup);
    }

    [Fact]
    public void CarouselItem_Dispose()
    {
        var cut = Context.Render<CarouselItem>();
        Assert.Equal("", cut.Markup);
    }

    [Fact]
    public void Carousel_HoverPause()
    {
        var cut = Context.Render<Carousel>(pb =>
        {
            pb.Add(a => a.HoverPause, true);
            pb.Add(b => b.ChildContent, new RenderFragment(builder =>
            {
                builder.OpenComponent<CarouselItem>(0);
                builder.AddAttribute(1, nameof(CarouselItem.ChildContent), new RenderFragment(builder => builder.AddContent(0, "Test-1")));
                builder.CloseComponent();

                builder.OpenComponent<CarouselItem>(2);
                builder.AddAttribute(3, nameof(CarouselItem.ChildContent), new RenderFragment(builder => builder.AddContent(0, "Test-2")));
                builder.CloseComponent();
            }));
        });
        cut.Contains("data-bs-pause=\"hover\"");

        cut.Render(pb =>
        {
            pb.Add(a => a.HoverPause, false);
        });
        cut.WaitForAssertion(() => cut.Contains("data-bs-pause=\"false\""));
    }

    [Fact]
    public void Carousel_PlayMode()
    {
        var cut = Context.Render<Carousel>(pb =>
        {
            pb.Add(a => a.PlayMode, CarouselPlayMode.AutoPlayOnload);
            pb.Add(b => b.ChildContent, new RenderFragment(builder =>
            {
                builder.OpenComponent<CarouselItem>(0);
                builder.AddAttribute(1, nameof(CarouselItem.ChildContent), new RenderFragment(builder => builder.AddContent(0, "Test-1")));
                builder.CloseComponent();

                builder.OpenComponent<CarouselItem>(2);
                builder.AddAttribute(3, nameof(CarouselItem.ChildContent), new RenderFragment(builder => builder.AddContent(0, "Test-2")));
                builder.CloseComponent();
            }));
        });
        cut.Contains("data-bs-ride=\"carousel\"");

        cut.Render(pb =>
        {
            pb.Add(a => a.PlayMode, CarouselPlayMode.AutoPlayAfterManually);
        });
        cut.WaitForAssertion(() => cut.Contains("data-bs-ride=\"true\""));

        cut.Render(pb =>
        {
            pb.Add(a => a.PlayMode, CarouselPlayMode.Manually);
        });
        cut.WaitForAssertion(() => cut.Contains("data-bs-ride=\"false\""));
    }
}
