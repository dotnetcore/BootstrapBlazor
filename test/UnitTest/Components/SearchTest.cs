// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace UnitTest.Components;

public class SearchTest : BootstrapBlazorTestBase
{
    [Fact]
    public async Task OnBlurAsync_Ok()
    {
        string? val = null;
        var items = new List<string>() { "test1", "test2" };
        var cut = Context.RenderComponent<Search<string>>(pb =>
        {
            pb.Add(a => a.OnSearch, v =>
            {
                return Task.FromResult(items.AsEnumerable());
            });
            pb.Add(a => a.OnBlurAsync, v =>
            {
                val = v;
                return Task.CompletedTask;
            });
        });
        await cut.InvokeAsync(() => cut.Instance.TriggerFilter("t"));
        await Task.Delay(20);

        var item = cut.Find(".dropdown-item");
        await cut.InvokeAsync(() => item.Click());
        Assert.NotNull(val);
        Assert.Equal("test1", val);
    }

    [Fact]
    public void Items_Ok()
    {
        var cut = Context.RenderComponent<Search<string>>();
        Assert.Contains("<div class=\"search auto-complete\"", cut.Markup);
        var menus = cut.FindAll(".dropdown-item");
        Assert.Single(menus);
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

        await cut.InvokeAsync(() => cut.Instance.TriggerFilter("t"));
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
        await cut.InvokeAsync(() => cut.Instance.TriggerFilter("t"));
        Assert.Contains("test1", cut.Markup);
        Assert.Contains("test2", cut.Markup);
    }

    [Fact]
    public void IsTriggerSearchByInput_Ok()
    {
        var cut = Context.RenderComponent<Search<string>>(builder =>
        {
            builder.Add(s => s.IsTriggerSearchByInput, true);
        });
        cut.DoesNotContain("data-bb-input");

        cut.SetParametersAndRender(pb => pb.Add(a => a.IsTriggerSearchByInput, false));
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
            builder.Add(s => s.IsTriggerSearchByInput, false);
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
        cut.WaitForState(() =>
        {
            menus = cut.FindAll(".dropdown-item");
            return menus.Count == 2;
        });
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

    [Fact]
    public async Task OnSelectedItemChanged_Ok()
    {
        var items = new List<string?>() { null, "test1", "test2" };
        var selectedItem = "";
        var cut = Context.RenderComponent<Search<string?>>(pb =>
        {
            pb.Add(a => a.OnSelectedItemChanged, v => { selectedItem = v; return Task.CompletedTask; });
            pb.Add(a => a.OnSearch, async v =>
            {
                await Task.Delay(1);
                return items;
            });
        });

        await cut.InvokeAsync(() => cut.Instance.TriggerFilter("t"));
        await Task.Delay(20);

        var item = cut.Find(".dropdown-item");
        await cut.InvokeAsync(() => item.Click());
        Assert.Null(selectedItem);
    }

    [Fact]
    public void ShowPrefixIcon_Ok()
    {
        var items = new List<string?>() { null, "test1", "test2" };
        var cut = Context.RenderComponent<Search<string?>>(pb =>
        {
            pb.Add(a => a.ShowPrefixIcon, true);
        });
        cut.Contains("<div class=\"search-prefix-icon\"><i class=\"fa-solid fa-magnifying-glass\"></i></div>");
    }

    [Fact]
    public void PrefixIconTemplate_Ok()
    {
        var items = new List<string?>() { null, "test1", "test2" };
        var cut = Context.RenderComponent<Search<string?>>(pb =>
        {
            pb.Add(a => a.ShowPrefixIcon, true);
            pb.Add(a => a.PrefixIconTemplate, context => b => b.AddContent(0, "test-prefix-icon-template"));
        });
        cut.Contains("<div class=\"search-prefix-icon\">test-prefix-icon-template</div>");
    }

    [Fact]
    public void IconTemplate_Ok()
    {
        var items = new List<string?>() { null, "test1", "test2" };
        var cut = Context.RenderComponent<Search<string?>>(pb =>
        {
            pb.Add(a => a.IconTemplate, context => b => b.AddContent(0, "test-icon-template"));
        });
        cut.Contains("test-icon-template");
    }

    [Fact]
    public void ShowSearchButton_Ok()
    {
        var items = new List<string?>() { null, "test1", "test2" };
        var cut = Context.RenderComponent<Search<string?>>(pb =>
        {
            pb.Add(a => a.ShowSearchButton, false);
        });
        cut.DoesNotContain("aria-label=\"Search\"");
    }

    [Fact]
    public void IsClearable_Ok()
    {
        var cut = Context.RenderComponent<Search<string?>>(pb =>
        {
            pb.Add(a => a.IsClearable, true);
        });
        cut.Contains("<div class=\"search-icon search-clear-icon\">");
    }

    [Fact]
    public void ButtonTemplate_Ok()
    {
        SearchContext<string?>? val = null;
        var cut = Context.RenderComponent<Search<string?>>(pb =>
        {
            pb.Add(a => a.ButtonTemplate, context => builder =>
            {
                val = context;
                builder.AddContent(0, "button-template");
            });
        });
        cut.Contains("button-template");
        Assert.NotNull(val);
        Assert.NotNull(val.Search);
        Assert.Equal(cut.Instance, val.Search);
        Assert.NotNull(val.OnSearchAsync);
        Assert.NotNull(val.OnClearAsync);

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.PrefixButtonTemplate, context => builder => builder.AddContent(0, "prefix-button-template"));
        });
        cut.Contains("prefix-button-template");
    }
}
