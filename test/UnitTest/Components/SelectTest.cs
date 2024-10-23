// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using Microsoft.AspNetCore.Components.Web.Virtualization;
using System.Reflection;

namespace UnitTest.Components;

public class SelectTest : BootstrapBlazorTestBase
{
    [Fact]
    public void OnSearchTextChanged_Null()
    {
        var cut = Context.RenderComponent<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<Select<string>>(pb =>
            {
                pb.Add(a => a.ShowSearch, true);
                pb.Add(a => a.Items, new List<SelectedItem>()
                {
                    new("1", "Test1"),
                    new("2", "Test2") { IsDisabled = true }
                });
            });
        });

        var ctx = cut.FindComponent<Select<string>>();
        ctx.InvokeAsync(async () =>
        {
            await ctx.Instance.ConfirmSelectedItem(0);

            // 搜索 T
            ctx.Find(".search-text").Input("T");
            await ctx.Instance.ConfirmSelectedItem(0);
        });

        ctx.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.OnBeforeSelectedItemChange, item => Task.FromResult(false));
            pb.Add(a => a.OnSelectedItemChanged, item => Task.CompletedTask);
        });
        ctx.InvokeAsync(() => ctx.Instance.ConfirmSelectedItem(0));

        ctx.Instance.ClearSearchText();
    }

    [Fact]
    public void Options_Ok()
    {
        var cut = Context.RenderComponent<Select<string>>(pb =>
        {
            pb.Add(a => a.Options, builder =>
            {
                builder.OpenComponent<SelectOption>(0);
                builder.AddAttribute(1, "Text", "Test-Select");
                builder.CloseComponent();

                builder.OpenComponent<SelectOption>(2);
                builder.CloseComponent();
            });
        });
        Assert.Contains("Test-Select", cut.Markup);
    }

    [Fact]
    public void Disabled_Ok()
    {
        var cut = Context.RenderComponent<Select<string>>(pb =>
        {
            pb.Add(a => a.IsDisabled, true);
            pb.Add(a => a.Options, builder =>
            {
                builder.OpenComponent<SelectOption>(0);
                builder.AddAttribute(1, nameof(SelectOption.IsDisabled), true);
                builder.CloseComponent();

                builder.OpenComponent<SelectOption>(2);
                builder.CloseComponent();
            });
        });
        Assert.Contains("_input\" disabled=\"disabled\"", cut.Markup);
        Assert.Contains("dropdown-item active disabled", cut.Markup);
    }

    [Fact]
    public void IsClearable_Ok()
    {
        var val = "Test2";
        var cut = Context.RenderComponent<Select<string>>(pb =>
        {
            pb.Add(a => a.IsClearable, true);
            pb.Add(a => a.Items, new List<SelectedItem>()
            {
                new("", "请选择"),
                new("2", "Test2"),
                new("3", "Test3")
            });
            pb.Add(a => a.Value, "2");
            pb.Add(a => a.OnValueChanged, v =>
            {
                val = v;
                return Task.CompletedTask;
            });
        });
        var clearButton = cut.Find(".clear-icon");
        cut.InvokeAsync(() => clearButton.Click());
        Assert.Empty(val);

        // 提高代码覆盖率
        var select = cut;
        select.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.Color, Color.Danger);
        });

        var validPi = typeof(Select<string>).GetProperty("IsValid", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic)!;
        validPi.SetValue(select.Instance, true);

        var pi = typeof(Select<string>).GetProperty("ClearClassString", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic)!;
        val = pi.GetValue(select.Instance, null)!.ToString();
        Assert.Contains("text-success", val);

        validPi.SetValue(select.Instance, false);
        val = pi.GetValue(select.Instance, null)!.ToString();
        Assert.Contains("text-danger", val);
    }

    [Fact]
    public void SelectOption_Ok()
    {
        var cut = Context.RenderComponent<SelectOption>(pb =>
        {
            pb.Add(a => a.Text, "Test-SelectOption");
            pb.Add(a => a.GroupName, "Test-GroupName");
            pb.Add(a => a.IsDisabled, false);
            pb.Add(a => a.Active, true);
            pb.Add(a => a.Value, "");
        });
    }

    [Fact]
    public void Enum_Ok()
    {
        var cut = Context.RenderComponent<Select<EnumEducation>>();
        Assert.Equal(2, cut.FindAll(".dropdown-item").Count);
    }

    [Fact]
    public void NullableEnum_Ok()
    {
        var cut = Context.RenderComponent<Select<EnumEducation?>>(pb =>
        {
            pb.Add(a => a.AdditionalAttributes, new Dictionary<string, object>()
            {
                ["placeholder"] = ""
            });
        });
        Assert.Equal(3, cut.FindAll(".dropdown-item").Count);
    }

    [Fact]
    public async Task OnSelectedItemChanged_OK()
    {
        var triggered = false;

        // 空值时，不触发 OnSelectedItemChanged 回调
        var cut = Context.RenderComponent<Select<string>>(pb =>
        {
            pb.Add(a => a.Items, new SelectedItem[]
            {
                new("", "Test"),
                new("1", "Test2")
            });
            pb.Add(a => a.Value, "");
            pb.Add(a => a.OnSelectedItemChanged, item =>
            {
                triggered = true;
                return Task.CompletedTask;
            });
        });
        Assert.False(triggered);

        // 切换候选项时触发 OnSelectedItemChanged 回调测试
        await cut.InvokeAsync(() =>
        {
            var items = cut.FindAll(".dropdown-item");
            var count = items.Count;
            Assert.Equal(2, count);

            var item = items[1];
            item.Click();
        });
        Assert.True(triggered);

        // 切换回 空值 触发 OnSelectedItemChanged 回调测试
        triggered = false;
        await cut.InvokeAsync(() =>
        {
            var items = cut.FindAll(".dropdown-item");
            var item = items[0];
            item.Click();
        });
        Assert.True(triggered);

        // 首次加载值不为空时触发 OnSelectedItemChanged 回调测试
        triggered = false;
        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.Items, new SelectedItem[]
            {
                new("", "Test"),
                new("1", "Test1"),
                new("2", "Test2")
            });
            pb.Add(a => a.Value, "2");
        });
        Assert.True(triggered);

        // 切换回 空值 触发 OnSelectedItemChanged 回调测试
        triggered = false;
        await cut.InvokeAsync(() =>
        {
            var items = cut.FindAll(".dropdown-item");
            var count = items.Count;
            Assert.Equal(3, count);
            var item = items[0];
            item.Click();
        });
        Assert.True(triggered);
    }

    [Fact]
    public async Task OnSelectedItemChanged_Generic()
    {
        Foo? selectedValue = null;
        var cut = Context.RenderComponent<Select<Foo>>(pb =>
        {
            pb.Add(a => a.Items, new SelectedItem<Foo>[]
            {
                new() { Value = new Foo() { Id = 1, Address = "Foo1" }, Text = "test1" },
                new() { Value = new Foo() { Id = 2, Address = "Foo2" }, Text = "test2" }
            });
            pb.Add(a => a.Value, new Foo() { Id = 1, Address = "Foo1" });
            pb.Add(a => a.OnSelectedItemChanged, v =>
            {
                if (v is SelectedItem<Foo> d)
                {
                    selectedValue = d.Value;
                }
                return Task.CompletedTask;
            });
        });

        var items = cut.FindAll(".dropdown-item");
        await cut.InvokeAsync(() => items[1].Click());
        Assert.NotNull(selectedValue);
    }

    [Fact]
    public void DisableItemChangedWhenFirstRender_Ok()
    {
        var triggered = false;

        // 空值时，不触发 OnSelectedItemChanged 回调
        var cut = Context.RenderComponent<Select<string>>(pb =>
        {
            pb.Add(a => a.Items, new SelectedItem[]
            {
                new("1", "Test"),
                new("2", "Test2")
            });
            pb.Add(a => a.Value, "");
            pb.Add(a => a.OnSelectedItemChanged, item =>
            {
                triggered = true;
                return Task.CompletedTask;
            });
            pb.Add(a => a.DisableItemChangedWhenFirstRender, true);
        });
        Assert.False(triggered);
    }

    [Fact]
    public void Color_Ok()
    {
        var cut = Context.RenderComponent<Select<string>>(pb =>
        {
            pb.Add(a => a.Color, Color.Danger);
        });
        Assert.Contains("border-danger", cut.Markup);
    }

    [Fact]
    public void Validate_Ok()
    {
        var valid = false;
        var invalid = false;
        var model = new Foo() { Name = "Test-Select1" };
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
            builder.AddChildContent<Select<string>>(pb =>
            {
                pb.Add(a => a.Value, model.Name);
                pb.Add(a => a.OnValueChanged, v =>
                {
                    model.Name = v;
                    return Task.CompletedTask;
                });
                pb.Add(a => a.ValueExpression, model.GenerateValueExpression());
                pb.Add(a => a.Items, new SelectedItem[]
                {
                    new("", "Test"),
                    new("1", "Test1") { GroupName = "Test1" },
                    new("2", "Test2") { GroupName = "Test2" }
                });
            });
        });

        cut.InvokeAsync(() =>
        {
            var form = cut.Find("form");
            form.Submit();
            Assert.True(valid);
        });

        var ctx = cut.FindComponent<Select<string>>();
        ctx.InvokeAsync(async () =>
        {
            await ctx.Instance.ConfirmSelectedItem(0);
            var form = cut.Find("form");
            form.Submit();
            Assert.True(invalid);
        });
    }

    [Fact]
    public void ItemTemplate_Ok()
    {
        var cut = Context.RenderComponent<Select<string>>(pb =>
        {
            pb.Add(a => a.Items, new SelectedItem[]
            {
                new("1", "Test1") { GroupName = "Test1" },
                new("2", "Test2") { GroupName = "Test2" }
            });
            pb.Add(a => a.Value, "2");
            pb.Add(a => a.ItemTemplate, item => builder =>
            {
                builder.OpenElement(0, "div");
                builder.AddContent(1, item.Text);
                builder.CloseComponent();
            });
        });

        cut.Find(".dropdown-item").Click();
    }

    [Fact]
    public void GroupItemTemplate_Ok()
    {
        var cut = Context.RenderComponent<Select<string>>(pb =>
        {
            pb.Add(a => a.Items, new SelectedItem[]
            {
                new("1", "Test1") { GroupName = "Test1" },
                new("2", "Test2") { GroupName = "Test2" }
            });
            pb.Add(a => a.Value, "2");
            pb.Add(a => a.GroupItemTemplate, title => builder =>
            {
                builder.OpenElement(0, "div");
                builder.AddAttribute(1, "class", "group-key");
                builder.AddContent(2, title);
                builder.CloseComponent();
            });
        });
        cut.Contains("<div class=\"group-key\">Test1</div>");
        cut.Contains("<div class=\"group-key\">Test2</div>");
    }

    [Fact]
    public void NullItems_Ok()
    {
        var cut = Context.RenderComponent<Select<string>>();
        Assert.Contains("select", cut.Markup);
    }

    [Fact]
    public void NullBool_Ok()
    {
        var cut = Context.RenderComponent<Select<bool?>>(pb =>
        {
            pb.Add(a => a.Items, new List<SelectedItem>
            {
                new("true", "True"),
                new("false", "False"),
            });
            pb.Add(a => a.Value, null);
        });

        // 值为 null
        // 候选项中无，导致默认选择第一个 Value 被更改为 true
        Assert.True(cut.Instance.Value);
    }

    [Fact]
    public void SelectItem_Ok()
    {
        var v = new SelectedItem("2", "Text2");
        var cut = Context.RenderComponent<Select<SelectedItem>>(pb =>
        {
            pb.Add(a => a.Items, new List<SelectedItem>
            {
                new("1", "Text1"),
                new("2", "Text2"),
            });
            pb.Add(a => a.Value, v);
            pb.Add(a => a.ValueChanged, EventCallback.Factory.Create<SelectedItem>(this, i => v = i));
        });
        Assert.Equal("2", cut.Instance.Value.Value);

        cut.InvokeAsync(() => cut.Find(".dropdown-item").Click());
        Assert.Equal("1", cut.Instance.Value.Value);
    }

    [Fact]
    public void SearchIcon_Ok()
    {
        var cut = Context.RenderComponent<Select<string>>(pb =>
        {
            pb.Add(a => a.Items, new SelectedItem[]
            {
                new("1", "Test1"),
                new("2", "Test2")
            });
            pb.Add(a => a.Value, "2");
            pb.Add(a => a.ShowSearch, true);
            pb.Add(a => a.SearchIcon, "search-icon");
        });
        Assert.Contains("search-icon", cut.Markup);
    }

    [Fact]
    public void IsFixedSearch_Ok()
    {
        var cut = Context.RenderComponent<Select<string>>(pb =>
        {
            pb.Add(a => a.Items, new SelectedItem[]
            {
                new("1", "Test1"),
                new("2", "Test2")
            });
            pb.Add(a => a.Value, "2");
            pb.Add(a => a.ShowSearch, true);
            pb.Add(a => a.IsFixedSearch, true);
        });
        Assert.Contains("search is-fixed", cut.Markup);
        Assert.Contains("class=\"icon", cut.Markup);
    }

    [Fact]
    public void CustomClass_Ok()
    {
        var cut = Context.RenderComponent<Select<string>>(pb =>
        {
            pb.Add(a => a.Items, new SelectedItem[]
            {
                new("1", "Test1"),
                new("2", "Test2")
            });
            pb.Add(a => a.Value, "2");
            pb.Add(a => a.CustomClass, "test-custom-class");
        });
        Assert.Contains("test-custom-class", cut.Markup);
    }

    [Fact]
    public void ShowShadow_Ok()
    {
        var cut = Context.RenderComponent<Select<string>>(pb =>
        {
            pb.Add(a => a.Items, new SelectedItem[]
            {
                new("1", "Test1"),
                new("2", "Test2")
            });
            pb.Add(a => a.Value, "2");
        });
        Assert.Contains("shadow", cut.Markup);

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.ShowShadow, false);
        });
        Assert.DoesNotContain("shadow", cut.Markup);
    }

    [Fact]
    public void DropdownIcon_Ok()
    {
        var cut = Context.RenderComponent<Select<string>>(pb =>
        {
            pb.Add(a => a.Items, new SelectedItem[]
            {
                new("1", "Test1"),
                new("2", "Test2")
            });
            pb.Add(a => a.Value, "2");
            pb.Add(a => a.DropdownIcon, "search-icon");
        });
        Assert.Contains("search-icon", cut.Markup);
    }

    [Fact]
    public void DisplayTemplate_Ok()
    {
        var cut = Context.RenderComponent<Select<string>>(pb =>
        {
            pb.Add(a => a.Items, new SelectedItem[]
            {
                new("1", "Test1"),
                new("2", "Test2")
            });
            pb.Add(a => a.Value, "2");
            pb.Add(a => a.DisplayTemplate, item => builder =>
            {
                builder.AddContent(0, $"test-display-template-{item?.Text}");
            });
        });
        Assert.Contains("test-display-template-Test2", cut.Markup);
    }

    [Fact]
    public void IsPopover_Ok()
    {
        var cut = Context.RenderComponent<Select<string>>(pb =>
        {
            pb.Add(a => a.Items, new SelectedItem[]
            {
                new("1", "Test1"),
                new("2", "Test2")
            });
            pb.Add(a => a.Value, "2");
            pb.Add(a => a.IsPopover, true);
        });
        Assert.DoesNotContain("dropdown-menu-arrow", cut.Markup);
        Assert.DoesNotContain("data-bs-toggle=\"dropdown\"", cut.Markup);
    }

    [Fact]
    public void Offset_Ok()
    {
        var cut = Context.RenderComponent<Select<string>>(pb =>
        {
            pb.Add(a => a.Items, new SelectedItem[]
            {
                new("1", "Test1"),
                new("2", "Test2")
            });
            pb.Add(a => a.Value, "2");
            pb.Add(a => a.IsPopover, false);
            pb.Add(a => a.Offset, "[0, 11]");
        });
        Assert.Contains("data-bs-offset=\"[0, 11]\"", cut.Markup);
    }

    [Fact]
    public void Placement_Ok()
    {
        var cut = Context.RenderComponent<Select<string>>(pb =>
        {
            pb.Add(a => a.Items, new SelectedItem[]
            {
                new("1", "Test1"),
                new("2", "Test2")
            });
            pb.Add(a => a.Value, "2");
            pb.Add(a => a.Placement, Placement.Top);
        });
        cut.Contains($"data-bs-placement=\"{Placement.Top.ToDescriptionString()}\"");

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.Placement, Placement.Auto);
        });
        cut.DoesNotContain("data-bs-placement");
    }

    [Fact]
    public void ItemClick_Ok()
    {
        var cut = Context.RenderComponent<Select<string>>(pb =>
        {
            pb.Add(a => a.Items, new SelectedItem[]
            {
                new("1", "Test1"),
                new("2", "Test2")
            });
            pb.Add(a => a.Value, "2");
            pb.Add(a => a.IsPopover, true);
        });

        cut.InvokeAsync(() =>
        {
            var item = cut.Find(".dropdown-item");
            item.Click();
            Assert.True(item.ClassList.Contains("active"));
        });
    }

    [Fact]
    public void IsVirtualize_Items()
    {
        var cut = Context.RenderComponent<Select<string>>(pb =>
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
        });

        cut.SetParametersAndRender(pb => pb.Add(a => a.ShowSearch, true));
        cut.InvokeAsync(async () =>
        {
            // 搜索 T
            cut.Find(".search-text").Input("T");
            await cut.Instance.ConfirmSelectedItem(0);
        });
    }

    [Fact]
    public async Task IsVirtualize_Items_Clearable_Ok()
    {
        var cut = Context.RenderComponent<Select<string>>(pb =>
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
        await cut.InvokeAsync(() => input.Input("2"));

        // 下拉框仅显示一个选项 Test2
        var items = cut.FindAll(".dropdown-item");
        Assert.Single(items);

        // UI 值为 Test2
        await cut.InvokeAsync(() => items[0].Click());
        var el = cut.Find(".form-select") as IHtmlInputElement;
        Assert.NotNull(el);
        Assert.Equal("Test2", el.Value);
        Assert.Equal("2", cut.Instance.Value);

        // 点击 Clear 按钮
        var button = cut.Find(".clear-icon");
        await cut.InvokeAsync(() => button.Click());

        // UI 恢复 Test1
        Assert.Equal("Test1", el.Value);

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
        var cut = Context.RenderComponent<Select<string>>(pb =>
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
                        ? new SelectedItem[]
                        {
                            new("", "All"),
                            new("1", "Test1"),
                            new("2", "Test2")
                        } : [new("2", "Test2")],
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
        await cut.InvokeAsync(() => input.Input("2"));

        // 下拉框仅显示一个选项 Test2
        var items = cut.FindAll(".dropdown-item");
        Assert.Single(items);

        // UI 值为 Test2
        await cut.InvokeAsync(() => items[0].Click());
        var el = cut.Find(".form-select") as IHtmlInputElement;
        Assert.NotNull(el);
        Assert.Equal("Test2", el.Value);
        Assert.Equal("2", cut.Instance.Value);

        query = false;
        // 点击 Clear 按钮
        var button = cut.Find(".clear-icon");
        await cut.InvokeAsync(() => button.Click());

        // UI 恢复 Test1
        Assert.Equal("All", el.Value);

        // 下拉框显示所有选项
        Assert.True(query);
    }

    [Fact]
    public void IsVirtualize_BindValue()
    {
        var value = new SelectedItem("3", "Test 3");
        var cut = Context.RenderComponent<Select<SelectedItem>>(pb =>
        {
            pb.Add(a => a.Value, value);
            pb.Add(a => a.IsVirtualize, true);
            pb.Add(a => a.ValueChanged, EventCallback.Factory.Create<SelectedItem>(this, new Action<SelectedItem>(item =>
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

        cut.InvokeAsync(() =>
        {
            var input = cut.Find(".form-select");
            Assert.Equal("Test 3", input.GetAttribute("value"));
        });
        cut.Contains("Test 3");
        var select = cut.Instance;
        Assert.Equal("3", select.Value?.Value);

        cut.InvokeAsync(() =>
        {
            var item = cut.Find(".dropdown-item");
            item.Click();
            Assert.Equal("1", value.Value);

            var input = cut.Find(".form-select");
            Assert.Equal("Test1", input.GetAttribute("value"));
        });
    }

    [Fact]
    public void IsVirtualize_DefaultVirtualizeItemText()
    {
        string value = "3";
        var cut = Context.RenderComponent<Select<string>>(pb =>
        {
            pb.Add(a => a.IsVirtualize, true);
            pb.Add(a => a.DefaultVirtualizeItemText, "Test 3");
            pb.Add(a => a.Value, value);
            pb.Add(a => a.ValueChanged, EventCallback.Factory.Create<string>(this, new Action<string>(item =>
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

        cut.InvokeAsync(() =>
        {
            var input = cut.Find(".form-select");
            Assert.Equal("Test 3", input.GetAttribute("value"));
        });
    }

    [Fact]
    public void LoadItems_Ok()
    {
        var cut = Context.RenderComponent<Select<string>>(pb =>
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
    public void TryParseValueFromString_Ok()
    {
        var items = new SelectedItem[]
        {
            new("1", "Test1"),
            new("2", "Test2")
        };
        var cut = Context.RenderComponent<Select<SelectedItem>>(pb =>
        {
            pb.Add(a => a.Items, items);
            pb.Add(a => a.Value, new SelectedItem("1", "Test1"));
            pb.Add(a => a.IsVirtualize, true);
        });
        var select = cut.Instance;
        var mi = select.GetType().GetMethod("TryParseSelectItem", BindingFlags.NonPublic | BindingFlags.Instance);

        string value = "";
        SelectedItem result = new();
        string? msg = null;
        mi?.Invoke(select, [value, result, msg]);

        var p = select.GetType().GetProperty("VirtualItems", BindingFlags.NonPublic | BindingFlags.Instance);
        p?.SetValue(select, items);
        value = "1";
        mi?.Invoke(select, [value, result, msg]);
    }

    [Fact]
    public void IsMarkupString_Ok()
    {
        var cut = Context.RenderComponent<Select<string>>(pb =>
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
    public async Task IsEditable_Ok()
    {
        var cut = Context.RenderComponent<Select<string>>(pb =>
        {
            pb.Add(a => a.Items, new SelectedItem[]
            {
                new("1", "<div>Test1</div>"),
                new("2", "<div>Test2</div>")
            });
            pb.Add(a => a.Value, "2");
        });
        var input = cut.Find(".form-select");
        Assert.True(input.IsReadOnly());

        var updated = false;
        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.IsEditable, true);
            pb.Add(a => a.OnInputChangedCallback, v =>
            {
                updated = true;
                return Task.CompletedTask;
            });
        });
        Assert.False(input.IsReadOnly());

        await cut.InvokeAsync(() =>
        {
            input.Change("Test3");
        });
        Assert.Equal("Test3", cut.Instance.Value);
        Assert.True(updated);
    }

    [Fact]
    public async Task IsEditable_Generic()
    {
        var cut = Context.RenderComponent<Select<Foo>>(pb =>
        {
            pb.Add(a => a.Items, new SelectedItem<Foo>[]
            {
                new() { Value = new Foo() { Id = 1, Address = "Foo1" }, Text = "test1" },
                new() { Value = new Foo() { Id = 2, Address = "Foo2" }, Text = "test2" }
            });
            pb.Add(a => a.Value, new Foo() { Id = 1, Address = "Foo1" });
            pb.Add(a => a.IsEditable, true);
            pb.Add(a => a.EditTextConvertToValueCallback, v =>
            {
                return Task.FromResult(new Foo() { Id = 3, Address = "Foo3" });
            });
        });

        var input = cut.Find(".form-select");
        await cut.InvokeAsync(() =>
        {
            input.Change("test2");
        });
        Assert.Equal("Foo2", cut.Instance.Value.Address);

        await cut.InvokeAsync(() =>
        {
            input.Change("test3");
        });
        Assert.Equal("Foo3", cut.Instance.Value.Address);
    }

    [Fact]
    public async Task OnClearAsync_Ok()
    {
        var clear = false;
        var cut = Context.RenderComponent<Select<string>>(pb =>
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

        var span = cut.Find(".clear-icon");
        Assert.NotNull(span);

        await cut.InvokeAsync(() =>
        {
            span.Click();
        });
        Assert.True(clear);
    }

    [Fact]
    public async Task Toggle_Ok()
    {
        var cut = Context.RenderComponent<Select<string>>(pb =>
        {
            pb.Add(a => a.Items, new SelectedItem[]
            {
                new("1", "Test1"),
                new("2", "Test2")
            });
        });
        await cut.Instance.Show();
        await cut.Instance.Hide();
    }

    [Fact]
    public void GenericValue_Ok()
    {
        var items = new List<SelectedItem<Foo>>()
        {
            new()
            {
                Value = new Foo() { Id = 1, Name = "Foo1" },
                Text = "Foo1"
            },
            new()
            {
                Value = new Foo() { Id = 2, Name = "Foo2" },
                Text = "Foo2"
            }
        };
        var cut = Context.RenderComponent<Select<Foo>>(pb =>
        {
            pb.Add(a => a.Items, items);
        });
    }
}
