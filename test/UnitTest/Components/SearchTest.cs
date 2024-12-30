// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace UnitTest.Components;

public class SearchTest : BootstrapBlazorTestBase
{
    [Fact]
    public void Items_Ok()
    {
        var cut = Context.RenderComponent<Search<string>>();
        Assert.Contains("<div class=\"search auto-complete\"", cut.Markup);
        var menus = cut.FindAll(".dropdown-item");
        Assert.Single(menus);

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.ShowNoDataTip, false);
        });
        menus = cut.FindAll(".dropdown-item");
        Assert.Empty(menus);
    }

    [Fact]
    public async Task ItemTemplate_Ok()
    {
        var items = new List<Foo>() { new() { Name = "test1", Address = "Address 1" }, new() { Name = "test2", Address = "Address 2" } };
        var cut = Context.RenderComponent<Search<Foo>>(pb =>
        {
            pb.Add(a => a.ItemTemplate, item => builder =>
            {
                builder.AddContent(0, $"Template-{item.Name}-{item.Address}");
            });
            pb.Add(a => a.OnSearch, async v =>
            {
                await Task.Delay(1);
                return items;
            });
        });

        await cut.InvokeAsync(() => cut.Instance.TriggerOnChange("t"));
        await Task.Delay(20);

        Assert.Contains("Template-test1-Address 1", cut.Markup);
        Assert.Contains("Template-test2-Address 2", cut.Markup);
    }

    [Fact]
    public async Task OnGetDisplayText_Ok()
    {
        var items = new List<Foo?>() { null, new() { Name = "test1", Address = "Address 1" }, new() { Name = "test2", Address = "Address 2" } };
        var cut = Context.RenderComponent<Search<Foo?>>(pb =>
        {
            pb.Add(a => a.OnSearch, async v =>
            {
                await Task.Delay(1);
                return items;
            });
            pb.Add(a => a.OnGetDisplayText, foo => foo?.Name);
        });

        await cut.InvokeAsync(() => cut.Instance.TriggerOnChange("t"));
        await Task.Delay(20);

        Assert.Contains("test1", cut.Markup);
        Assert.Contains("test2", cut.Markup);
    }

    [Fact]
    public void IsOnInputTrigger_Ok()
    {
        var cut = Context.RenderComponent<Search<string>>(builder =>
        {
            builder.Add(s => s.IsOnInputTrigger, true);
        });
        cut.DoesNotContain("data-bb-input");

        cut.SetParametersAndRender(pb => pb.Add(a => a.IsOnInputTrigger, false));
        cut.Contains("data-bb-input=\"false\"");
    }

    [Fact]
    public async Task OnSearchClick_Ok()
    {
        string? val = null;
        var items = new List<string>() { "test1", "test2" };
        var cut = Context.RenderComponent<Search<string>>(builder =>
        {
            builder.Add(s => s.SearchButtonIcon, "fa-fw fa-solid fa-magnifying-glass");
            builder.Add(s => s.SearchButtonText, "SearchText");
            builder.Add(s => s.SearchButtonColor, Color.Warning);
            builder.Add(s => s.IsOnInputTrigger, false);
            builder.Add(s => s.IsAutoClearAfterSearch, true);
            builder.Add(a => a.OnSearch, async v =>
            {
                val = v;
                await Task.Delay(1);
                return items;
            });
        });
        var menus = cut.FindAll(".dropdown-item");
        Assert.Single(menus);

        var button = cut.Find(".fa-magnifying-glass");
        await cut.InvokeAsync(() => button.Click());
        await Task.Delay(10);

        menus = cut.FindAll(".dropdown-item");
        Assert.Equal(2, menus.Count);
    }

    [Fact]
    public async Task OnClearClick_Ok()
    {
        var ret = false;
        var cut = Context.RenderComponent<Search<string>>(builder =>
        {
            builder.Add(s => s.Value, "1");
            builder.Add(s => s.ShowClearButton, true);
            builder.Add(s => s.ClearButtonColor, Color.Secondary);
            builder.Add(s => s.ClearButtonIcon, "test-icon");
            builder.Add(s => s.ClearButtonText, "Clear");
            builder.Add(s => s.OnClear, v =>
            {
                ret = true;
                return Task.CompletedTask;
            });
        });
        cut.Contains("test-icon");
        cut.Contains("Clear");

        var button = cut.Find(".btn-secondary");
        await cut.InvokeAsync(() => button.Click());
        Assert.True(ret);
    }
}
