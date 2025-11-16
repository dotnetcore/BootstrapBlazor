// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace UnitTest.Components;

public class CascaderTest : BootstrapBlazorTestBase
{
    [Fact]
    public async Task ValidateForm_OK()
    {
        var foo = new Foo() { Name = "test1" };
        var valid = false;
        var invalid = false;
        var items = new List<CascaderItem>()
        {
            new() { Text = "Test1", Value = "test1" },
            new() { Text = "Test2", Value = "test2" }
        };
        var cut = Context.Render<ValidateForm>(pb =>
        {
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
            pb.Add(a => a.Model, foo);
            pb.AddChildContent<Cascader<string>>(pb =>
            {
                pb.Add(a => a.Items, items);
                pb.Add(a => a.DisplayText, "Test_DisplayText");
                pb.Add(a => a.ShowLabel, true);
                pb.Add(a => a.IsClearable, true);
                pb.Add(a => a.Value, foo.Name);
                pb.Add(a => a.ValueExpression, Utility.GenerateValueExpression(foo, "Name", typeof(string)));
                pb.Add(a => a.OnValueChanged, v =>
                {
                    foo.Name = v;
                    return Task.CompletedTask;
                });
            });
        });
        cut.Contains("Test_DisplayText");

        await cut.InvokeAsync(() =>
        {
            var form = cut.Find("form");
            form.Submit();
        });
        Assert.True(valid);

        var span = cut.Find(".clear-icon");
        Assert.True(span.ClassList.Contains("text-success"));

        foo.Name = null;
        var cascader = cut.FindComponent<Cascader<string>>();
        cascader.Render();
        await cut.InvokeAsync(() =>
        {
            var form = cut.Find("form");
            form.Submit();
        });
        Assert.True(invalid);

        span = cut.Find(".clear-icon");
        Assert.True(span.ClassList.Contains("text-danger"));
    }

    [Fact]
    public void Color_Ok()
    {
        var cut = Context.Render<Cascader<string>>(pb =>
        {
            pb.Add(a => a.Color, Color.Success);
            pb.Add(a => a.IsClearable, true);
        });
        cut.Contains("border-success");

        var span = cut.Find(".clear-icon");
        Assert.True(span.ClassList.Contains("text-success"));
    }

    [Fact]
    public async Task Items_Ok()
    {
        var value = "";
        var selectedItems = new List<CascaderItem>();
        var items = new List<CascaderItem>()
        {
            new() { Text = "Test1", Value = "1" },
            new() { Text = "Test2", Value = "2" }
        };
        items[1].AddItem(new("11", "Test11"));
        items[1].AddItem(new() { Text = "Test12", Value = "12" });

        items[1].Items.ElementAt(1).AddItem(new() { Text = "Test111", Value = "111" });

        var cut = Context.Render<Cascader<string>>(pb =>
        {
            pb.Add(a => a.Items, items);
            pb.Add(a => a.Value, value);
            pb.Add(a => a.OnValueChanged, v =>
            {
                value = v;
                return Task.CompletedTask;
            });
            pb.Add(a => a.OnSelectedItemChanged, items =>
            {
                selectedItems.AddRange(items);
                return Task.CompletedTask;
            });
        });

        var dropdownItems = cut.FindAll(".dropdown-item");
        await cut.InvokeAsync(() => dropdownItems[1].Click());
        Assert.Equal("2", value);
        Assert.Single(selectedItems);

        dropdownItems = cut.FindAll(".dropdown-item");
        await cut.InvokeAsync(() => dropdownItems[0].Click());
        Assert.Equal("1", value);

        // nav-link
        var linkItems = cut.FindAll(".nav-link");
        await cut.InvokeAsync(() => linkItems[0].Click());
        Assert.Equal("11", value);

        linkItems = cut.FindAll(".nav-link");
        await cut.InvokeAsync(() => linkItems[1].Click());
        Assert.Equal("12", value);

        // 测试 Items 为 null
        cut.Render(pb =>
        {
            pb.Add(a => a.Items, null);
        });
        cut.Contains("dropdown-menu shadow");
        cut.DoesNotContain("dropdown-item");
    }

    [Fact]
    public void SetDefaultValue_Ok()
    {
        var items = new List<CascaderItem>()
        {
            new() { Text = "Test1", Value = "1" },
            new() { Text = "Test2", Value = "2" }
        };
        items[0].AddItem(new("11", "Test11"));

        var cut = Context.Render<Cascader<string>>(pb =>
        {
            pb.Add(a => a.Items, items);
            pb.Add(a => a.Value, "Test");
        });
        Assert.Equal("", cut.Instance.Value);

        cut.Render(pb =>
        {
            pb.Add(a => a.Value, "11");
        });
        Assert.Equal("11", cut.Instance.Value);

        cut.Render(pb =>
        {
            pb.Add(a => a.Items, null);
            pb.Add(a => a.Value, "1");
            pb.Add(a => a.ShowFullLevels, false);
        });

        // 未提供数据源时 Value 赋值无效
        Assert.Null(cut.Instance.Value);

        cut.Render(pb =>
        {
            pb.Add(a => a.Items, items);
            pb.Add(a => a.Value, "3");
            pb.Add(a => a.ShowFullLevels, false);
        });
    }

    [Fact]
    public void IsDisabled_Ok()
    {
        var cut = Context.Render<Cascader<string>>(pb =>
        {
            pb.Add(a => a.IsDisabled, true);
            pb.Add(a => a.IsClearable, true);
        });

        var input = cut.Find(".dropdown > input");
        Assert.True(input.HasAttribute("disabled"));
    }

    [Fact]
    public async Task IsClearable_Ok()
    {
        var isClear = false;
        var cut = Context.Render<Cascader<string>>(pb =>
        {
            pb.Add(a => a.IsClearable, true);
            pb.Add(a => a.OnClearAsync, () =>
            {
                isClear = true;
                return Task.CompletedTask;
            });
        });

        var clearButton = cut.Find(".clear-icon");
        await cut.InvokeAsync(() => clearButton.Click());
        Assert.True(isClear);
    }

    [Fact]
    public void SubCascader_NullItems()
    {
        var items = new List<CascaderItem>()
        {
            new() { Text = "test1", Value = "1" }
        };
        var cut = Context.Render<CascadingValue<List<CascaderItem>>>(pb =>
        {
            pb.Add(a => a.Value, items);
            pb.AddChildContent<SubCascader>(pb =>
            {
                pb.Add(a => a.Items, null);
            });
        });
        Assert.Equal("", cut.Markup);
    }

    [Fact]
    public async Task ParentSelectable_Ok()
    {
        var value = "";
        var selectedItems = new List<CascaderItem>();
        var items = new List<CascaderItem>()
        {
            new() { Text = "Test1", Value = "1" },
            new() { Text = "Test2", Value = "2" }
        };
        items[1].AddItem(new("11", "Test11"));
        items[1].AddItem(new() { Text = "Test12", Value = "12" });

        items[1].Items.ElementAt(1).AddItem(new() { Text = "Test111", Value = "111" });

        var cut = Context.Render<Cascader<string>>(pb =>
        {
            pb.Add(a => a.Items, items);
            pb.Add(a => a.Value, value);
            pb.Add(a => a.ParentSelectable, false);
            pb.Add(a => a.OnValueChanged, v =>
            {
                value = v;
                return Task.CompletedTask;
            });
        });
        var dropdownItems = cut.FindAll(".dropdown-item");
        await cut.InvokeAsync(() => dropdownItems[1].Click());
        Assert.Equal("", value);

        var linkItems = cut.FindAll(".nav-link");
        await cut.InvokeAsync(() => linkItems[2].Click());
        Assert.Equal("111", value);
    }

    [Fact]
    public async Task ShowFullLevels_Ok()
    {
        var selectedItems = new List<CascaderItem>();
        var items = new List<CascaderItem>()
        {
            new() { Text = "Test1", Value = "1" },
            new() { Text = "Test2", Value = "2" }
        };
        items[0].AddItem(new("11", "Test11"));
        items[1].AddItem(new("21", "Test21"));

        var cut = Context.Render<MockCascader>(pb =>
        {
            pb.Add(a => a.Items, items);
            pb.Add(a => a.ShowFullLevels, false);
        });
        var dropdownItems = cut.FindAll(".nav-link");
        await cut.InvokeAsync(() => dropdownItems[0].Click());
        Assert.Equal("Test11", cut.Instance.MockDisplayText);

        cut.Render(pb =>
        {
            pb.Add(a => a.ShowFullLevels, true);
        });
        dropdownItems = cut.FindAll(".nav-link");
        await cut.InvokeAsync(() => dropdownItems[0].Click());
        Assert.Equal("Test1/Test11", cut.Instance.MockDisplayText);
    }

    [Fact]
    public async Task OnBlurAsync_Ok()
    {
        var value = "";
        var items = new List<CascaderItem>()
        {
            new() { Text = "test1", Value = "1" }
        };
        var cut = Context.Render<Cascader<string>>(pb =>
        {
            pb.Add(a => a.Items, items);
            pb.Add(a => a.Value, "1");
            pb.Add(a => a.OnBlurAsync, v =>
            {
                value = v;
                return Task.CompletedTask;
            });
        });
        var input = cut.Find("input");
        await cut.InvokeAsync(() => input.Blur());
        Assert.Equal("1", value);
    }

    class MockCascader : Cascader<string>
    {
        public string? MockDisplayText => DisplayTextString;
    }
}
