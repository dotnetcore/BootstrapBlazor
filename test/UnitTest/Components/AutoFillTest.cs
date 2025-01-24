// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.Localization;

namespace UnitTest.Components;

public class AutoFillTest : BootstrapBlazorTestBase
{
    private IStringLocalizer<Foo> Localizer { get; }

    private Foo Model { get; }

    private List<Foo> Items { get; }

    public AutoFillTest()
    {
        Localizer = Context.Services.GetRequiredService<IStringLocalizer<Foo>>();
        Model = Foo.Generate(Localizer);
        Items = Foo.GenerateFoo(Localizer, 2);
    }

    [Fact]
    public void Items_Ok()
    {
        var cut = Context.RenderComponent<AutoFill<Foo>>();
        Assert.Contains("<div class=\"auto-complete auto-fill\"", cut.Markup);
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
    public void ShowLabel_Ok()
    {
        var cut = Context.RenderComponent<AutoFill<Foo>>(pb =>
        {
            pb.Add(a => a.ShowLabel, true);
            pb.Add(a => a.Value, Model);
            pb.Add(a => a.Items, Items);
            pb.Add(a => a.DisplayText, "AutoFillDisplayText");
        });
        Assert.Contains("AutoFillDisplayText", cut.Markup);
    }

    [Fact]
    public void ItemTemplate_Ok()
    {
        var items = new List<Foo>() { new() { Name = "test1" }, new() { Name = "test2" } };
        var cut = Context.RenderComponent<AutoFill<Foo>>(pb =>
        {
            pb.Add(a => a.Items, items);
            pb.Add(a => a.ItemTemplate, item => builder =>
            {
                builder.AddContent(0, $"Template-{item.Name}");
            });
        });

        Assert.Contains("Template-test1", cut.Markup);
        Assert.Contains("Template-test2", cut.Markup);
    }

    [Fact]
    public async Task OnCustomFilter_Ok()
    {
        var filtered = false;
        var cut = Context.RenderComponent<AutoFill<Foo>>(pb =>
        {
            pb.Add(a => a.Value, Model);
            pb.Add(a => a.Items, Items);
            pb.Add(a => a.OnCustomFilter, key =>
            {
                filtered = true;
                var items = Foo.GenerateFoo(Localizer, 3);
                return Task.FromResult(items.AsEnumerable());
            });
        });
        await cut.InvokeAsync(() => cut.Instance.TriggerFilter("t"));
        Assert.True(filtered);

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(pb => pb.OnCustomFilter, null!);
        });
        await cut.InvokeAsync(() => cut.Instance.TriggerFilter(""));
        var items = cut.FindAll(".dropdown-item");
        Assert.Equal(2, items.Count);
    }

    [Fact]
    public void SkipEnter_Ok()
    {
        var cut = Context.RenderComponent<AutoComplete>(pb =>
        {
            pb.Add(a => a.SkipEnter, false);
        });
        cut.DoesNotContain("data-bb-skip-enter");

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.SkipEnter, true);
        });
        cut.Contains("data-bb-skip-enter=\"true\"");
    }

    [Fact]
    public void SkipEsc_Ok()
    {
        var cut = Context.RenderComponent<AutoFill<Foo>>(pb =>
        {
            pb.Add(a => a.SkipEsc, false);
        });
        cut.DoesNotContain("data-bb-skip-esc");

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.SkipEsc, true);
        });
        cut.Contains("data-bb-skip-esc=\"true\"");
    }

    [Fact]
    public void ScrollIntoViewBehavior_Ok()
    {
        var cut = Context.RenderComponent<AutoFill<Foo>>(pb =>
        {
            pb.Add(a => a.ScrollIntoViewBehavior, ScrollIntoViewBehavior.Smooth);
        });
        cut.DoesNotContain("data-bb-scroll-behavior");

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.ScrollIntoViewBehavior, ScrollIntoViewBehavior.Auto);
        });
        cut.Contains("data-bb-scroll-behavior=\"auto\"");
    }

    [Fact]
    public async Task OnSelectedItemChanged_Ok()
    {
        Foo? selectedItem = null;
        var cut = Context.RenderComponent<AutoFill<Foo>>(pb =>
        {
            pb.Add(a => a.Value, Model);
            pb.Add(a => a.Items, Items);
            pb.Add(a => a.OnSelectedItemChanged, foo =>
            {
                selectedItem = foo;
                return Task.CompletedTask;
            });
        });
        var item = cut.Find(".dropdown-item");
        await cut.InvokeAsync(() => item.Click());
        Assert.NotNull(selectedItem);
    }

    [Fact]
    public async Task OnGetDisplayText_Ok()
    {
        var cut = Context.RenderComponent<AutoFill<Foo>>(pb =>
        {
            pb.Add(a => a.Value, Model);
            pb.Add(a => a.Items, new List<Foo> { null!, new() { Name = "Test" } });
            pb.Add(a => a.OnGetDisplayText, foo => foo?.Name);
        });
        var input = cut.Find("input");
        Assert.Equal("张三 1000", input.Attributes["value"]?.Value);

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.OnGetDisplayText, null!);
        });
        await cut.InvokeAsync(() => cut.Instance.TriggerChange("t"));

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.IsLikeMatch, true);
        });
        await cut.InvokeAsync(() => cut.Instance.TriggerChange("t"));
    }

    [Fact]
    public async Task IgnoreCase_Ok()
    {
        var items = new List<Foo>() { new() { Name = "task1" }, new() { Name = "task2" }, new() { Name = "Task3" }, new() { Name = "Task4" } };
        var cut = Context.RenderComponent<AutoFill<Foo>>(builder =>
        {
            builder.Add(a => a.Items, items);
            builder.Add(a => a.IgnoreCase, true);
            builder.Add(a => a.OnGetDisplayText, foo => foo?.Name);
        });

        await cut.InvokeAsync(() => cut.Instance.TriggerFilter("t"));
        var menus = cut.FindAll(".dropdown-item");
        Assert.Equal(4, menus.Count);

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.DisplayCount, 2);
        });
        await cut.InvokeAsync(() => cut.Instance.TriggerFilter("t"));
        menus = cut.FindAll(".dropdown-item");
        Assert.Equal(2, menus.Count);

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.IgnoreCase, false);
            pb.Add(a => a.DisplayCount, null);
        });
        await cut.InvokeAsync(() => cut.Instance.TriggerFilter("t"));
        menus = cut.FindAll(".dropdown-item");
        Assert.Equal(2, menus.Count);
    }

    [Fact]
    public async Task IsLikeMatch_Ok()
    {
        var items = new List<Foo>() { new() { Name = "task1" }, new() { Name = "task2" }, new() { Name = "Task3" }, new() { Name = "Task4" } };
        var cut = Context.RenderComponent<AutoFill<Foo>>(builder =>
        {
            builder.Add(a => a.Items, items);
            builder.Add(a => a.IsLikeMatch, false);
            builder.Add(a => a.OnGetDisplayText, foo => foo?.Name);
        });

        await cut.InvokeAsync(() => cut.Instance.TriggerFilter("t"));
        var menus = cut.FindAll(".dropdown-item");
        Assert.Equal(4, menus.Count);

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.DisplayCount, 2);
        });
        await cut.InvokeAsync(() => cut.Instance.TriggerFilter("t"));
        menus = cut.FindAll(".dropdown-item");
        Assert.Equal(2, menus.Count);

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.IsLikeMatch, true);
            pb.Add(a => a.DisplayCount, null);
        });
        await cut.InvokeAsync(() => cut.Instance.TriggerFilter("a"));
        menus = cut.FindAll(".dropdown-item");
        Assert.Equal(4, menus.Count);

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.OnGetDisplayText, null);
            pb.Add(a => a.IsLikeMatch, false);
        });
        await cut.InvokeAsync(() => cut.Instance.TriggerFilter("t"));
        menus = cut.FindAll(".dropdown-item");
        Assert.Single(menus);

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.OnGetDisplayText, null);
            pb.Add(a => a.IsLikeMatch, true);
        });
        await cut.InvokeAsync(() => cut.Instance.TriggerFilter("t"));
        menus = cut.FindAll(".dropdown-item");
        Assert.Single(menus);

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.OnGetDisplayText, foo => null);
            pb.Add(a => a.IsLikeMatch, false);
        });
        await cut.InvokeAsync(() => cut.Instance.TriggerFilter("t"));
        menus = cut.FindAll(".dropdown-item");
        Assert.Single(menus);

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.OnGetDisplayText, foo => null);
            pb.Add(a => a.IsLikeMatch, true);
        });
        await cut.InvokeAsync(() => cut.Instance.TriggerFilter("t"));
        menus = cut.FindAll(".dropdown-item");
        Assert.Single(menus);
    }

    [Fact]
    public void ShowDropdownListOnFocus_Ok()
    {
        var items = new List<Foo>() { new() { Name = "test1" }, new() { Name = "test2" } };
        var cut = Context.RenderComponent<AutoFill<Foo>>(pb =>
        {
            pb.Add(a => a.Items, items);
        });
        cut.Contains("data-bb-auto-dropdown-focus=\"true\"");

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.ShowDropdownListOnFocus, false);
        });
        cut.DoesNotContain("data-bb-auto-dropdown-focus");
    }
}
