// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Shared;
using UnitTest.Extensions;

namespace UnitTest.Components;

public class MultiSelectTest : BootstrapBlazorTestBase
{
    [Fact]
    public void MinMax_Ok()
    {
        var cut = Context.RenderComponent<MultiSelect<string>>(pb =>
        {
            pb.Add(a => a.Min, 2);
            pb.Add(a => a.Max, 3);
        });
        Assert.Contains("multi-select", cut.Markup);
    }

    [Fact]
    public void EnumValue_Ok()
    {
        var cut = Context.RenderComponent<MultiSelect<EnumEducation>>();
        Assert.Contains("multi-select", cut.Markup);
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
        cut.Find(".search-text").Input("T");
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
        cut.Find(".dropdown-item").Click();
        Assert.True(toggle);

        // 增加代码覆盖率
        cut.Find(".multi-select-close").Click();

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.Max, 1);
        });
        cut.Find(".dropdown-item").Click();

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.Max, 0);
            pb.Add(a => a.Min, 1);
        });
        cut.Find(".dropdown-item").Click();

        var foo = new Foo();
        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.Value, "");
            pb.Add(a => a.ValueExpression, foo.GenerateValueExpression());
        });
        cut.Find(".dropdown-item").Click();
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

        // SelectAll
        buttons[0].Click();
        Assert.Equal(2, selectedItems.Count);

        // InvertSelect
        buttons[1].Click();
        Assert.Empty(selectedItems);

        // InvertSelect
        buttons[0].Click();
        Assert.Equal(2, selectedItems.Count);
        buttons[2].Click();
        Assert.Empty(selectedItems);
    }

    [Fact]
    public void ToggleMenu_Ok()
    {
        var cut = Context.RenderComponent<MultiSelect<string>>(pb =>
        {
            pb.Add(a => a.Items, new List<SelectedItem>
            {
                new("1", "Test1"),
                new("2", "Test2")
            });
        });
        cut.Find(".dropdown-menu-toggle").Click();
        Assert.Contains("show", cut.Markup);

        // 代码覆盖率 Close
        cut.InvokeAsync(() => cut.Instance.Close());
        Assert.DoesNotContain("show", cut.Markup);
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
}
