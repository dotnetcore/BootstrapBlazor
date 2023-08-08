// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Shared;

namespace UnitTest.Components;

public class MultiSelectTest : BootstrapBlazorTestBase
{
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
    public void EnumValue_Ok()
    {
        var cut = Context.RenderComponent<MultiSelect<EnumEducation>>();
        Assert.Contains("multi-select", cut.Markup);
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
    public void OnSearchTextChanged_Ok()
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
        cut.Find(".form-control").Input("T");
        Assert.Contains("multi-select", cut.Markup);
    }

    [Fact]
    public void OnParameterSet_Ok()
    {
        var cut = Context.RenderComponent<MultiSelect<string>>(pb =>
        {
            pb.Add(a => a.Value, "1,2");
            pb.Add(a => a.Items, new List<SelectedItem>
            {
                new("1", "Test1"),
                new("2", "Test2")
            });
        });
        Assert.Contains("multi-select", cut.Markup);
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
        List<SelectedItem> selectedItems = new();
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

        var form = cut.Find("form");
        form.Submit();
        Assert.True(invalid);

        cut.Find(".dropdown-item").Click();
        form.Submit();
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
            pb.Add(a => a.ClearIcon, "icon-clear");
        });
        Assert.Contains("icon-clear", cut.Markup);

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.ClearIcon, null);
        });
        Assert.Contains("fa-solid fa-xmark", cut.Markup);
    }
}
