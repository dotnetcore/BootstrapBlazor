// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.AspNetCore.Components.Web.Virtualization;
using System.Reflection;

namespace UnitTest.Components;

public class MultiSelectTest : BootstrapBlazorTestBase
{
    [Fact]
    public async Task OnSearchTextChanged_Null()
    {
        var cut = Context.RenderComponent<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<MultiSelect<string>>(pb =>
            {
                pb.Add(a => a.ShowSearch, true);
                pb.Add(a => a.Items, new List<SelectedItem>()
                {
                    new("1", "Test1"),
                    new("2", "Test2") { IsDisabled = true }
                });
            });
        });

        var ctx = cut.FindComponent<MultiSelect<string>>();
        await ctx.InvokeAsync(async () =>
        {
            await ctx.Instance.ConfirmSelectedItem(0);

            // 搜索 T
            await ctx.Instance.TriggerOnSearch("T");
            await ctx.Instance.ConfirmSelectedItem(0);
        });

        ctx.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.OnSearchTextChanged, text =>
            {
                return new List<SelectedItem>()
                {
                    new("1", "Test1")
                };
            });
        });

        await ctx.InvokeAsync(async () =>
        {
            await ctx.Instance.TriggerOnSearch("T");
        });
        cut.DoesNotContain("Test2");
    }

    [Fact]
    public void MinMax_Ok()
    {
        var cut = Context.RenderComponent<MultiSelect<string>>(pb =>
        {
            pb.Add(a => a.Min, 1);
            pb.Add(a => a.Max, 3);
            pb.Add(a => a.Items, new List<SelectedItem>
            {
                new("1", "Test 1"),
                new("2", "Test 2"),
                new("3", "Test 3"),
                new("4", "Test 4"),
            });
        });
        Assert.Contains("multi-select", cut.Markup);

        cut.InvokeAsync(() =>
        {
            var items = cut.FindAll(".dropdown-item");
            items[0].Click();
            items[1].Click();
            items[2].Click();
        });
    }

    [Fact]
    public async Task IsEditable_Ok()
    {
        var cut = Context.RenderComponent<MultiSelect<string>>(pb =>
        {
            pb.Add(a => a.Max, 2);
            pb.Add(a => a.Items, new List<SelectedItem>
            {
                new("1", "Test 1"),
                new("2", "Test 2"),
                new("3", "Test 3"),
                new("4", "Test 4"),
            });
        });
        Assert.DoesNotContain("class=\"multi-select-input\"", cut.Markup);

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.IsEditable, true);
        });
        Assert.Contains("class=\"multi-select-input\"", cut.Markup);
        Assert.DoesNotContain("data-bb-trigger-key", cut.Markup);

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.EditSubmitKey, EditSubmitKey.Space);
        });
        Assert.Contains("data-bb-trigger-key=\"space\"", cut.Markup);

        await cut.InvokeAsync(() => cut.Instance.TriggerEditTag("123"));
        Assert.Equal("123", cut.Instance.Value);

        await cut.InvokeAsync(() => cut.Instance.TriggerEditTag("123"));
        Assert.Equal("123", cut.Instance.Value);

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.OnEditCallback, async v =>
            {
                await Task.Delay(10);
                return new SelectedItem("test", "456");
            });
        });
        await cut.InvokeAsync(() => cut.Instance.TriggerEditTag("456"));
        Assert.Equal("123,test", cut.Instance.Value);
        Assert.DoesNotContain("class=\"multi-select-input\"", cut.Markup);
    }

    [Fact]
    public void IsFixedHeight_Ok()
    {
        var cut = Context.RenderComponent<MultiSelect<EnumEducation>>(pb =>
        {
            pb.Add(a => a.IsFixedHeight, true);
        });
        var toggle = cut.Find(".dropdown-toggle");
        Assert.True(toggle.ClassList.Contains("is-fixed"));
    }

    [Fact]
    public void IsSingleLine_Ok()
    {
        var cut = Context.RenderComponent<MultiSelect<EnumEducation>>(pb =>
        {
            pb.Add(a => a.IsSingleLine, true);
        });
        cut.Contains("dropdown-toggle scroll is-single-line");
    }

    [Fact]
    public void EnumValue_Ok()
    {
        var cut = Context.RenderComponent<MultiSelect<EnumEducation>>();
        Assert.Contains("multi-select", cut.Markup);
    }

    [Fact]
    public async Task FlagEnum_Ok()
    {
        var value = MockFlagEnum.One | MockFlagEnum.Two;
        var cut = Context.RenderComponent<MultiSelect<MockFlagEnum>>(pb =>
        {
            pb.Add(a => a.Value, value);
        });
        var values = cut.FindAll(".multi-select-items .multi-select-item");
        Assert.Equal(2, values.Count);

        // 选中第四个
        var items = cut.FindAll(".dropdown-menu .dropdown-item");
        var item = items[items.Count - 1];
        await cut.InvokeAsync(() => item.Click());
        values = cut.FindAll(".multi-select-items .multi-select-item");
        Assert.Equal(3, values.Count);
    }

    [Flags]
    private enum MockFlagEnum
    {
        One = 1,
        Two = 2,
        Three = 4,
        Four = 8
    }

    [Fact]
    public void Group_Ok()
    {
        var cut = Context.RenderComponent<MultiSelect<string>>(pb =>
        {
            pb.Add(a => a.Items, new List<SelectedItem>
            {
                new("1", "Test 1") { GroupName = "Group 1" },
                new("2", "Test 2") { GroupName = "Group 1" },
                new("3", "Test 3") { GroupName = "Group 2" },
                new("4", "Test 4") { GroupName = "Group 2" }
            });
        });
        Assert.Contains("Group 1", cut.Markup);
        Assert.Contains("Group 2", cut.Markup);
    }

    [Fact]
    public void NullableEnumValue_Ok()
    {
        var cut = Context.RenderComponent<MultiSelect<EnumEducation?>>();
        Assert.Contains("multi-select", cut.Markup);
    }

    [Fact]
    public void EnumerableValue_Ok()
    {
        var cut = Context.RenderComponent<MultiSelect<IEnumerable<string>>>();
        Assert.Contains("multi-select", cut.Markup);

        // 代码覆盖率 SetValue
        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.ShowSearch, true);
            pb.Add(a => a.ShowToolbar, true);
            pb.Add(a => a.Items, new List<SelectedItem>
            {
                new("1", "Test1"),
                new("2", "Test2")
            });
        });

        var buttons = cut.FindAll(".toolbar button");
        // SelectAll
        buttons[0].Click();
    }

    [Fact]
    public void ButtonTemplate_Ok()
    {
        var cut = Context.RenderComponent<MultiSelect<string>>(pb =>
        {
            pb.Add(a => a.ShowToolbar, true);
            pb.Add(a => a.ButtonTemplate, builder => builder.AddContent(0, new MarkupString("<button class=\"btn-test\">ButtonTemplate</button>")));
        });

        // 没有数据也显示 Toolbar 
        Assert.Contains("btn-test", cut.Markup);

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.Items, new List<SelectedItem>
            {
                new("1", "Test1"),
                new("2", "Test2")
            });
        });
        Assert.Contains("btn-test", cut.Markup);
    }

    [Fact]
    public void ArrayValue_Ok()
    {
        var cut = Context.RenderComponent<MultiSelect<string[]>>();

        // 代码覆盖率 SetValue
        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.ShowSearch, true);
            pb.Add(a => a.ShowToolbar, true);
            pb.Add(a => a.Items, new List<SelectedItem>
            {
                new("1", "Test1"),
                new("2", "Test2")
            });
        });

        var buttons = cut.FindAll(".toolbar button");
        // SelectAll
        buttons[0].Click();
    }

    [Fact]
    public async Task OnSearchTextChanged_Ok()
    {
        var cut = Context.RenderComponent<MultiSelect<string>>(pb =>
        {
            pb.Add(a => a.ShowSearch, true);
            pb.Add(a => a.ShowToolbar, true);
            pb.Add(a => a.Items, new List<SelectedItem>
            {
                new("1", "Test1"),
                new("2", "Test2")
            });
        });
        await cut.InvokeAsync(() => cut.Instance.TriggerOnSearch("a"));
        cut.Contains("<div class=\"dropdown-item\">无数据</div>");
    }

    [Fact]
    public void ScrollIntoViewBehavior_Ok()
    {
        var cut = Context.RenderComponent<MultiSelect<string>>(pb =>
        {
            pb.Add(a => a.Items, new SelectedItem[]
            {
                new("1", "Test1"),
                new("2", "Test2")
            });
            pb.Add(a => a.Value, "2");
            pb.Add(a => a.ScrollIntoViewBehavior, ScrollIntoViewBehavior.Auto);
        });
        Assert.Contains("data-bb-scroll-behavior=\"auto\"", cut.Markup);

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.ScrollIntoViewBehavior, ScrollIntoViewBehavior.Smooth);
        });
        Assert.DoesNotContain("data-bb-scroll-behavior", cut.Markup);
    }

    [Fact]
    public void ToggleRow_Ok()
    {
        var toggle = false;
        var cut = Context.RenderComponent<MultiSelect<string>>(pb =>
        {
            pb.Add(a => a.Max, 3);
            pb.Add(a => a.Items, new List<SelectedItem>
            {
                new("1", "Test1"),
                new("2", "Test2")
            });
            pb.Add(a => a.OnSelectedItemsChanged, items =>
            {
                toggle = true;
                return Task.CompletedTask;
            });
        });
        cut.InvokeAsync(() => cut.Find(".dropdown-item").Click());
        Assert.True(toggle);

        // 增加代码覆盖率
        cut.InvokeAsync(() => cut.Find(".multi-select-close").Click());

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.Max, 1);
        });
        cut.InvokeAsync(() => cut.Find(".dropdown-item").Click());

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.Max, 0);
            pb.Add(a => a.Min, 1);
        });
        cut.InvokeAsync(() => cut.Find(".dropdown-item").Click());

        var foo = new Foo();
        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.Value, "");
            pb.Add(a => a.ValueExpression, foo.GenerateValueExpression());
        });
        cut.InvokeAsync(() => cut.Find(".dropdown-item").Click());
    }

    [Fact]
    public void DefaultButtons_Ok()
    {
        List<SelectedItem> selectedItems = [];
        var cut = Context.RenderComponent<MultiSelect<string>>(pb =>
        {
            pb.Add(a => a.ShowToolbar, true);
            pb.Add(a => a.ShowDefaultButtons, true);
            pb.Add(a => a.Items, new List<SelectedItem>
            {
                new("1", "Test1"),
                new("2", "Test2")
            });
            pb.Add(a => a.OnSelectedItemsChanged, items =>
            {
                selectedItems.Clear();
                selectedItems.AddRange(items);
                return Task.CompletedTask;
            });
        });
        var buttons = cut.FindAll(".toolbar button");
        Assert.Equal(3, buttons.Count);

        // SelectAll
        cut.InvokeAsync(() => buttons[0].Click());
        //Assert.Equal(2, selectedItems.Count);

        // InvertSelect
        cut.InvokeAsync(() => buttons[1].Click());
        //Assert.Empty(selectedItems);

        // InvertSelect
        cut.InvokeAsync(() => buttons[1].Click());
        //Assert.Equal(2, selectedItems.Count);

        // Clear
        cut.InvokeAsync(() => buttons[2].Click());
        //Assert.Empty(selectedItems);
    }

    [Fact]
    public void IsPopover_True()
    {
        var cut = Context.RenderComponent<MultiSelect<string>>(pb =>
        {
            pb.Add(a => a.IsPopover, true);
            pb.Add(a => a.Items, new List<SelectedItem>
            {
                new("1", "Test1"),
                new("2", "Test2")
            });
        });
        cut.DoesNotContain("data-bs-toggle=\"dropdown\"");
    }

    [Fact]
    public void IsPopover_False()
    {
        var cut = Context.RenderComponent<MultiSelect<string>>(pb =>
        {
            pb.Add(a => a.IsPopover, false);
            pb.Add(a => a.Items, new List<SelectedItem>
            {
                new("1", "Test1"),
                new("2", "Test2")
            });
        });
        cut.Contains("data-bs-toggle=\"dropdown\"");
    }

    [Fact]
    public void ShowCloseButton_Ok()
    {
        var cut = Context.RenderComponent<MultiSelect<string>>(pb =>
        {
            pb.Add(a => a.Value, "1");
            pb.Add(a => a.ShowCloseButton, false);
            pb.Add(a => a.Items, new List<SelectedItem>
            {
                new("1", "Test1"),
                new("2", "Test2")
            });
        });
        Assert.DoesNotContain("multi-select-item-group", cut.Markup);

        // 设置 SelectedItems
        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.ShowCloseButton, true);
            pb.Add(a => a.Items, new List<SelectedItem>
            {
                new("1", "Test1") { Active = true },
                new("2", "Test2")
            });
        });
        cut.Contains("multi-select-item-group");
        cut.DoesNotContain("data-bb-val");

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.IsPopover, true);
        });
        cut.Contains("data-bb-val");
    }

    [Fact]
    public void ShowLabel_Ok()
    {
        var cut = Context.RenderComponent<MultiSelect<string>>(pb =>
        {
            pb.Add(a => a.ShowLabel, true);
            pb.Add(a => a.DisplayText, "Test-MultiSelect-Label");
            pb.Add(a => a.Items, new List<SelectedItem>
            {
                new("1", "Test1"),
                new("2", "Test2")
            });
        });
        Assert.Contains("Test-MultiSelect-Label", cut.Markup);
    }

    [Fact]
    public void DisplayTemplate_Ok()
    {
        var cut = Context.RenderComponent<MultiSelect<string>>(pb =>
        {
            pb.Add(a => a.Items, new List<SelectedItem>
            {
                new("1", "Test1"),
                new("2", "Test2")
            });
            pb.Add(a => a.Value, "1");
            pb.Add(a => a.DisplayTemplate, v => builder =>
            {
                builder.AddContent(0, new MarkupString($"<h1>{string.Join("", v.Select(i => i.Value))}</h1>"));
            });
        });
        cut.Contains("<h1>1</h1>");
    }

    [Fact]
    public void Validate_Ok()
    {
        var model = new Foo();
        var valid = false;
        var invalid = false;
        var cut = Context.RenderComponent<ValidateForm>(pb =>
        {
            pb.Add(a => a.Model, model);
            pb.Add(a => a.OnValidSubmit, context =>
            {
                valid = true;
                return Task.CompletedTask;
            });
            pb.Add(a => a.OnInvalidSubmit, context =>
            {
                invalid = true;
                return Task.CompletedTask;
            });

            pb.AddChildContent<MultiSelect<string>>(pb =>
            {
                pb.Add(a => a.Value, model.Name);
                pb.Add(a => a.OnValueChanged, v =>
                {
                    model.Name = v;
                    return Task.CompletedTask;
                });
                pb.Add(a => a.ValueExpression, model.GenerateValueExpression());
                pb.Add(a => a.Items, new List<SelectedItem>
                {
                    new("1", "Test1"),
                    new("2", "Test2")
                });
            });
        });

        cut.InvokeAsync(() =>
        {
            var form = cut.Find("form");
            form.Submit();
        });
        Assert.True(invalid);

        cut.InvokeAsync(() =>
        {
            cut.Find(".dropdown-item").Click();
        });
        cut.InvokeAsync(() =>
        {
            var form = cut.Find("form");
            form.Submit();
        });
        Assert.True(valid);
    }

    [Fact]
    public void ItemTemplate_Ok()
    {
        var cut = Context.RenderComponent<MultiSelect<string>>(pb =>
        {
            pb.Add(a => a.Value, "1");
            pb.Add(a => a.ShowCloseButton, false);
            pb.Add(a => a.Items, new List<SelectedItem>
            {
                new("1", "Test1"),
                new("2", "Test2")
            });
            pb.Add(a => a.ItemTemplate, item => builder =>
            {
                builder.OpenElement(0, "div");
                builder.AddContent(1, $"test-{item.Text}-test");
                builder.CloseElement();
            });
        });
        Assert.Contains("test-Test1-test", cut.Markup);
        Assert.Contains("test-Test2-test", cut.Markup);
    }

    [Fact]
    public void GroupItemTemplate_Ok()
    {
        var cut = Context.RenderComponent<MultiSelect<string>>(pb =>
        {
            pb.Add(a => a.Value, "1");
            pb.Add(a => a.ShowCloseButton, false);
            pb.Add(a => a.Items, new List<SelectedItem>
            {
                new("1", "Test1") { GroupName = "Test1" },
                new("2", "Test2") { GroupName = "Test2" }
            });
            pb.Add(a => a.GroupItemTemplate, title => builder =>
            {
                builder.OpenElement(0, "div");
                builder.AddAttribute(1, "class", "group-key");
                builder.AddContent(2, title);
                builder.CloseElement();
            });
        });
        cut.Contains("<div class=\"group-key\">Test1</div>");
        cut.Contains("<div class=\"group-key\">Test2</div>");
    }

    [Fact]
    public void SearchIcon_Ok()
    {
        var cut = Context.RenderComponent<MultiSelect<string>>(pb =>
        {
            pb.Add(a => a.Value, "1");
            pb.Add(a => a.ShowSearch, true);
            pb.Add(a => a.Items, new List<SelectedItem>
            {
                new("1", "Test1"),
                new("2", "Test2")
            });
            pb.Add(a => a.SearchIcon, "icon-search");
        });
        Assert.Contains("icon-search", cut.Markup);

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.SearchIcon, null);
        });
        Assert.Contains("fa-solid fa-magnifying-glass", cut.Markup);
    }

    [Fact]
    public void ClearIcon_Ok()
    {
        var cut = Context.RenderComponent<MultiSelect<string>>(pb =>
        {
            pb.Add(a => a.Value, "1");
            pb.Add(a => a.ShowCloseButton, true);
            pb.Add(a => a.Items, new List<SelectedItem>
            {
                new("1", "Test1"),
                new("2", "Test2")
            });
            pb.Add(a => a.IsClearable, true);
        });
        Assert.Contains("fa-regular fa-circle-xmark", cut.Markup);

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.ClearIcon, "icon-clear-test");
        });
        Assert.DoesNotContain("fa-regular fa-circle-xmark", cut.Markup);
        Assert.Contains("icon-clear-test", cut.Markup);
    }

    [Fact]
    public void IsMarkupString_Ok()
    {
        var cut = Context.RenderComponent<MultiSelect<string>>(pb =>
        {
            pb.Add(a => a.Items, new SelectedItem[]
            {
                new("1", "<div>Test1</div>"),
                new("2", "<div>Test2</div>")
            });
            pb.Add(a => a.Value, "2");
            pb.Add(a => a.IsMarkupString, true);
        });
        Assert.Contains("<div>Test1</div>", cut.Markup);
    }

    [Fact]
    public void DefaultVirtualizeItemText_Ok()
    {
        var cut = Context.RenderComponent<MultiSelect<string>>(pb =>
        {
            pb.Add(a => a.Items, new SelectedItem[]
            {
                new("1", "Test1"),
                new("2", "Test2")
            });
            pb.Add(a => a.Value, "1,2");
            pb.Add(a => a.DefaultVirtualizeItemText, "Test1");
            pb.Add(a => a.IsVirtualize, true);
        });
        var items = cut.FindAll(".multi-select-items .multi-select-item");
        Assert.Equal(2, items.Count);
        Assert.Equal("Test1", items[0].InnerHtml);
        Assert.Equal("2", items[1].InnerHtml);
    }

    [Fact]
    public async Task IsVirtualize_Items_Clearable_Ok()
    {
        var cut = Context.RenderComponent<MultiSelect<string>>(pb =>
        {
            pb.Add(a => a.Items, new SelectedItem[]
            {
                new("1", "Test1"),
                new("2", "Test2")
            });
            pb.Add(a => a.Value, "2");
            pb.Add(a => a.IsVirtualize, true);
            pb.Add(a => a.RowHeight, 33f);
            pb.Add(a => a.OverscanCount, 4);
            pb.Add(a => a.IsClearable, true);
            pb.Add(a => a.ShowSearch, true);
        });

        // 覆盖有搜索条件时，点击清空按钮
        // 期望 UI 显示值为默认值
        // 期望 下拉框为全数据
        var input = cut.Find(".search-text");
        await cut.InvokeAsync(() => cut.Instance.TriggerOnSearch("2"));

        // 下拉框仅显示一个选项 Test2
        var items = cut.FindAll(".dropdown-item");
        Assert.Single(items);

        // 点击 Clear 按钮
        var button = cut.Find(".clear-icon");
        await cut.InvokeAsync(() => button.Click());

        // 下拉框显示所有选项
        items = cut.FindAll(".dropdown-item");
        Assert.Equal(2, items.Count);
    }

    [Fact]
    public async Task IsVirtualize_OnQueryAsync_Clearable_Ok()
    {
        var query = false;
        var startIndex = 0;
        var requestCount = 0;
        var searchText = string.Empty;
        var cut = Context.RenderComponent<MultiSelect<string>>(pb =>
        {
            pb.Add(a => a.OnQueryAsync, option =>
            {
                query = true;
                startIndex = option.StartIndex;
                requestCount = option.Count;
                searchText = option.SearchText;
                return Task.FromResult(new QueryData<SelectedItem>()
                {
                    Items = string.IsNullOrEmpty(searchText)
                        ? [new("", "All"), new("1", "Test1"), new("2", "Test2")]
                        : [new("2", "Test2")],
                    TotalCount = string.IsNullOrEmpty(searchText) ? 2 : 1
                });
            });
            pb.Add(a => a.Value, "");
            pb.Add(a => a.IsVirtualize, true);
            pb.Add(a => a.IsClearable, true);
            pb.Add(a => a.ShowSearch, true);
        });

        // 覆盖有搜索条件时，点击清空按钮
        // 期望 UI 显示值为默认值
        // 期望 下拉框为全数据
        var input = cut.Find(".search-text");
        await cut.InvokeAsync(() => cut.Instance.TriggerOnSearch("2"));

        // 下拉框仅显示一个选项 Test2
        var items = cut.FindAll(".dropdown-item");
        Assert.Single(items);

        query = false;
        // 点击 Clear 按钮
        var button = cut.Find(".clear-icon");
        await cut.InvokeAsync(() => button.Click());

        // 下拉框显示所有选项
        Assert.True(query);
    }

    [Fact]
    public async Task IsVirtualize_BindValue()
    {
        var value = "3";
        var cut = Context.RenderComponent<MultiSelect<string>>(pb =>
        {
            pb.Add(a => a.Value, value);
            pb.Add(a => a.IsVirtualize, true);
            pb.Add(a => a.ValueChanged, EventCallback.Factory.Create(this, new Action<string?>(item =>
            {
                value = item;
            })));
            pb.Add(a => a.OnQueryAsync, option =>
            {
                return Task.FromResult(new QueryData<SelectedItem>()
                {
                    Items = new SelectedItem[]
                    {
                        new("1", "Test1"),
                        new("2", "Test2")
                    },
                    TotalCount = 2
                });
            });
        });

        // 3 不在集合内，但是由于是虚拟集合，只能显示
        var select = cut.Instance;
        Assert.Equal("3", select.Value);

        var item = cut.Find(".dropdown-item");
        await cut.InvokeAsync(() =>
        {
            item.Click();
        });
        Assert.Equal("3,1", value);
    }

    [Fact]
    public void LoadItems_Ok()
    {
        var cut = Context.RenderComponent<MultiSelect<string>>(pb =>
        {
            pb.Add(a => a.OnQueryAsync, option =>
            {
                return Task.FromResult(new QueryData<SelectedItem>());
            });
            pb.Add(a => a.Value, "2");
            pb.Add(a => a.IsVirtualize, true);
        });
        var select = cut.Instance;
        var mi = select.GetType().GetMethod("LoadItems", BindingFlags.NonPublic | BindingFlags.Instance);
        mi?.Invoke(select, [new ItemsProviderRequest(0, 1, CancellationToken.None)]);

        var totalCountProperty = select.GetType().GetProperty("TotalCount", BindingFlags.NonPublic | BindingFlags.Instance);
        totalCountProperty?.SetValue(select, 2);
        mi?.Invoke(select, [new ItemsProviderRequest(0, 1, CancellationToken.None)]);
    }

    [Fact]
    public async Task OnClearAsync_Ok()
    {
        var clear = false;
        var cut = Context.RenderComponent<MultiSelect<string>>(pb =>
        {
            pb.Add(a => a.Items, new SelectedItem[]
            {
                new("1", "<div>Test1</div>"),
                new("2", "<div>Test2</div>")
            });
            pb.Add(a => a.Value, "2");
            pb.Add(a => a.ShowSearch, true);
            pb.Add(a => a.IsClearable, true);
            pb.Add(a => a.OnClearAsync, () =>
            {
                clear = true;
                return Task.CompletedTask;
            });
        });
        cut.Contains("select dropdown multi-select is-clearable");
        cut.Contains("fa-regular fa-circle-xmark");

        var span = cut.Find(".clear-icon");
        Assert.NotNull(span);

        await cut.InvokeAsync(() =>
        {
            span.Click();
        });
        Assert.True(clear);
    }
}
