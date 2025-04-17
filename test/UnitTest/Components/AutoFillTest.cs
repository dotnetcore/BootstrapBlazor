// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.Extensions.Localization;
using System.ComponentModel.DataAnnotations;

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
    public void OnGetDisplayText_Ok()
    {
        var cut = Context.RenderComponent<AutoFill<Foo>>(pb =>
        {
            pb.Add(a => a.Value, Model);
            pb.Add(a => a.Items, new List<Foo> { null!, new() { Name = "Test" } });
            pb.Add(a => a.OnGetDisplayText, foo => foo?.Name);
        });
        var input = cut.Find("input");
        Assert.Equal("张三 1000", input.Attributes["value"]?.Value);
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

    [Fact]
    public async Task IsVirtualize_Items()
    {
        var items = new List<Foo>() { new() { Name = "test1" }, new() { Name = "test2" } };
        var cut = Context.RenderComponent<AutoFill<Foo>>(pb =>
        {
            pb.Add(a => a.IsLikeMatch, true);
            pb.Add(a => a.Items, items);
            pb.Add(a => a.Value, items[0]);
            pb.Add(a => a.IsVirtualize, true);
            pb.Add(a => a.RowHeight, 58f);
            pb.Add(a => a.OverscanCount, 4);
            pb.Add(a => a.OnGetDisplayText, f => f?.Name);
        });

        await cut.InvokeAsync(() => cut.Instance.TriggerFilter("2"));
        cut.Contains("<div>test2</div>");
    }

    [Fact]
    public async Task IsVirtualize_Items_Clearable_Ok()
    {
        var items = new List<Foo>() { new() { Name = "test1" }, new() { Name = "test2" } };
        var cut = Context.RenderComponent<AutoFill<Foo>>(pb =>
        {
            pb.Add(a => a.Items, items);
            pb.Add(a => a.Value, items[0]);
            pb.Add(a => a.IsVirtualize, true);
            pb.Add(a => a.IsClearable, true);
            pb.Add(a => a.OnGetDisplayText, f => f?.Name);
        });
        await cut.InvokeAsync(() => cut.Instance.TriggerFilter("2"));

        // 点击 Clear 按钮
        var button = cut.Find(".clear-icon");
        await cut.InvokeAsync(() => button.Click());
        cut.SetParametersAndRender();

        var input = cut.Find(".form-control");
        Assert.Null(input.NodeValue);
    }

    [Fact]
    public void Placeholder_Ok()
    {
        var localizer = Context.Services.GetRequiredService<IStringLocalizer<Foo>>();
        var cut = Context.RenderComponent<AutoFill<Foo>>(pb =>
        {
            pb.Add(a => a.OnQueryAsync, option =>
            {
                var items = Foo.GenerateFoo(localizer, 80).Skip(option.StartIndex).Take(5);
                var ret = new QueryData<Foo>()
                {
                    Items = items,
                    TotalCount = 80
                };
                return Task.FromResult(ret);
            });
            pb.Add(a => a.IsVirtualize, true);
            pb.Add(a => a.RowHeight, 50f);
            pb.Add(a => a.OnGetDisplayText, f => f?.Name);
        });
        cut.Contains("<div class=\"dropdown-item\"><div class=\"is-ph\"></div></div>");

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.RowHeight, 35f);
        });
        cut.Contains("<div class=\"dropdown-item\" style=\"height: 35px;\"><div class=\"is-ph\"></div></div>");
    }

    [Fact]
    public async Task IsVirtualize_OnQueryAsync_Clearable_Ok()
    {
        var query = false;
        var startIndex = 0;
        var requestCount = 0;
        var searchText = string.Empty;
        var cleared = false;
        var items = new List<Foo>() { new() { Name = "test1" }, new() { Name = "test2" } };
        var cut = Context.RenderComponent<AutoFill<Foo>>(pb =>
        {
            pb.Add(a => a.OnQueryAsync, option =>
            {
                query = true;
                startIndex = option.StartIndex;
                requestCount = option.Count;
                searchText = option.SearchText;
                return Task.FromResult(new QueryData<Foo>()
                {
                    Items = string.IsNullOrEmpty(searchText) ? items : items.Where(i => i.Name!.Contains(searchText, StringComparison.OrdinalIgnoreCase)),
                    TotalCount = string.IsNullOrEmpty(searchText) ? 2 : 1
                });
            });
            pb.Add(a => a.Value, items[0]);
            pb.Add(a => a.IsVirtualize, true);
            pb.Add(a => a.IsClearable, true);
            pb.Add(a => a.RowHeight, 35f);
            pb.Add(a => a.OnGetDisplayText, f => f?.Name);
            pb.Add(a => a.OnClearAsync, () =>
            {
                cleared = true;
                return Task.CompletedTask;
            });
        });
        await cut.InvokeAsync(() => cut.Instance.TriggerFilter("2"));
        Assert.Equal("2", searchText);
        Assert.Contains("<div>test2</div>", cut.Markup);

        query = false;
        // 点击 Clear 按钮
        var button = cut.Find(".clear-icon");
        await cut.InvokeAsync(() => button.Click());

        Assert.True(query);
        Assert.True(cleared);

        // OnQueryAsync 返回空集合
        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.OnQueryAsync, option =>
            {
                return Task.FromResult(new QueryData<Foo>()
                {
                    Items = null,
                    TotalCount = 0
                });
            });
        });
        await cut.InvokeAsync(() => button.Click());
    }

    [Fact]
    public void Clearable_Ok()
    {
        var items = new List<int?>() { 1, 2 };
        var cut = Context.RenderComponent<AutoFill<int?>>(pb =>
        {
            pb.Add(a => a.Items, items);
            pb.Add(a => a.Value, items[0]);
            pb.Add(a => a.IsVirtualize, true);
            pb.Add(a => a.IsClearable, true);
            pb.Add(a => a.OnGetDisplayText, f => f?.ToString());
        });
        cut.Contains("clear-icon");
    }


    [Fact]
    public async Task Validate_Ok()
    {
        var valid = false;
        var invalid = false;
        var model = new MockModel() { Value = new Foo() { Name = "Test-Select1" } };
        var items = new List<Foo>()
        {
            new() { Name = "test1" },
            new() { Name = "test2" }
        };
        var cut = Context.RenderComponent<ValidateForm>(builder =>
        {
            builder.Add(a => a.OnValidSubmit, context =>
            {
                valid = true;
                return Task.CompletedTask;
            });
            builder.Add(a => a.OnInvalidSubmit, context =>
            {
                invalid = true;
                return Task.CompletedTask;
            });
            builder.Add(a => a.Model, model);
            builder.AddChildContent<AutoFill<Foo>>(pb =>
            {
                pb.Add(a => a.Items, items);
                pb.Add(a => a.Value, model.Value);
                pb.Add(a => a.IsClearable, true);
                pb.Add(a => a.OnGetDisplayText, f => f?.Name);
                pb.Add(a => a.OnValueChanged, v =>
                {
                    model.Value = v;
                    return Task.CompletedTask;
                });
                pb.Add(a => a.ValueExpression, Utility.GenerateValueExpression(model, "Value", typeof(Foo)));
            });
        });

        await cut.InvokeAsync(() =>
        {
            var form = cut.Find("form");
            form.Submit();
        });
        Assert.True(valid);
        Assert.Equal("Test-Select1", model.Value.Name);

        // 点击 Clear 按钮
        var button = cut.Find(".clear-icon");
        await cut.InvokeAsync(() => button.Click());

        var form = cut.Find("form");
        form.Submit();
        Assert.True(invalid);
    }

    class MockModel
    {
        [Required]
        public Foo? Value { get; set; }
    }
}
