// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace UnitTest.Components;

public class WaterfallTest : BootstrapBlazorTestBase
{
    [Fact]
    public async Task Template_Ok()
    {
        var cut = Context.RenderComponent<Waterfall>(pb =>
        {
            pb.Add(a => a.OnRequestAsync, item => Task.FromResult(Enumerable.Range(1, 4).Select(i => new WaterfallItem() { Id = $"{i}", Url = $"url_{i}" })));
        });
        cut.MarkupMatches("<div class=\"bb-waterfall\" diff:ignore><div class=\"bb-waterfall-template\"></div><div class=\"bb-waterfall-list\" style=\"--bb-waterfall-item-width: 216px; --bb-waterfall-item-min-height: 316px;\"></div></div>");

        await cut.InvokeAsync(async () =>
        {
            await cut.Instance.OnloadAsync(null);
        });
        cut.Contains("<div class=\"bb-waterfall-item\" data-bb-waterfall-item-id=\"1\"><img alt data-url=\"url_1\" /><div class=\"bb-waterfall-item-loader\"><i class=\"fa-solid fa-circle-notch fa-spin\"></i></div></div>");

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.ItemWidth, 100);
            pb.Add(a => a.ItemMinHeight, 200);
            pb.Add(a => a.ItemTemplate, new RenderFragment<WaterfallItem>(item => new RenderFragment(builder =>
            {
                builder.AddContent(0, item.Url);
            })));
            pb.Add(a => a.Template, new RenderFragment<(WaterfallItem Item, RenderFragment Context)>(v => new RenderFragment(builder =>
            {
                builder.AddContent(0, v.Context);
            })));
        });
        cut.Contains("<div class=\"bb-waterfall-list\" style=\"--bb-waterfall-item-width: 100px; --bb-waterfall-item-min-height: 200px;\"></div>");

        await cut.InvokeAsync(async () =>
        {
            await cut.Instance.OnloadAsync(null);
        });
        cut.Contains("bb-waterfall-item-loader");

        var items = cut.FindAll(".bb-waterfall-item");
        Assert.Equal(4, items.Count);
        cut.Contains("url_1");
        cut.Contains("data-bb-waterfall-item-id=\"1\"");
    }

    [Fact]
    public void LoadTemplate_Ok()
    {
        var cut = Context.RenderComponent<Waterfall>(pb =>
        {
            pb.Add(a => a.OnRequestAsync, item => Task.FromResult(Enumerable.Range(1, 4).Select(i => new WaterfallItem() { Id = $"{i}", Url = $"url_{i}" })));
            pb.Add(a => a.LoadTemplate, new RenderFragment(builder =>
            {
                builder.AddContent(0, "load-template");
            }));
        });
        cut.Contains("load-template");
    }

    [Fact]
    public async Task OnClickItemAsync_Ok()
    {
        WaterfallItem? item = null;
        var cut = Context.RenderComponent<Waterfall>(pb =>
        {
            pb.Add(a => a.OnClickItemAsync, v =>
            {
                item = v;
                return Task.CompletedTask;
            });
        });

        await cut.InvokeAsync(async () =>
        {
            await cut.Instance.OnClickItem(new WaterfallItem() { Id = "0", Url = "test_1" });
        });
        Assert.NotNull(item);
    }
}
