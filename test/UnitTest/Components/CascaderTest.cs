﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace UnitTest.Components;

public class CascaderTest : BootstrapBlazorTestBase
{
    [Fact]
    public void ValidateForm_OK()
    {
        var foo = new Foo();
        var cut = Context.RenderComponent<ValidateForm>(pb =>
        {
            pb.Add(a => a.Model, foo);
            pb.AddChildContent<Cascader<string>>(pb =>
            {
                pb.Add(a => a.DisplayText, "Test_DisplayText");
                pb.Add(a => a.ShowLabel, true);
            });
        });
        cut.Contains("Test_DisplayText");
    }

    [Fact]
    public void Color_Ok()
    {
        var cut = Context.RenderComponent<Cascader<string>>(pb =>
        {
            pb.Add(a => a.Color, Color.Success);
        });
        cut.Contains("border-success");
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

        var cut = Context.RenderComponent<Cascader<string>>(pb =>
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
        cut.SetParametersAndRender(pb =>
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

        var cut = Context.RenderComponent<Cascader<string>>(pb =>
        {
            pb.Add(a => a.Items, items);
            pb.Add(a => a.Value, "Test");
        });
        Assert.Equal("1", cut.Instance.Value);

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.Value, "11");
        });
        Assert.Equal("11", cut.Instance.Value);

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.Items, null);
            pb.Add(a => a.Value, "1");
            pb.Add(a => a.ShowFullLevels, false);
        });
        Assert.Empty(cut.Instance.Value);
    }

    [Fact]
    public void IsDisabled_Ok()
    {
        var cut = Context.RenderComponent<Cascader<string>>(pb =>
        {
            pb.Add(a => a.IsDisabled, true);
        });

        var input = cut.Find(".dropdown > input");
        Assert.True(input.HasAttribute("disabled"));
    }

    [Fact]
    public void SubCascader_NullItems()
    {
        var items = new List<CascaderItem>()
        {
            new() { Text = "test1", Value = "1" }
        };
        var cut = Context.RenderComponent<CascadingValue<List<CascaderItem>>>(pb =>
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

        var cut = Context.RenderComponent<Cascader<string>>(pb =>
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

        var cut = Context.RenderComponent<MockCascader>(pb =>
        {
            pb.Add(a => a.Items, items);
            pb.Add(a => a.ShowFullLevels, false);
        });
        var dropdownItems = cut.FindAll(".nav-link");
        await cut.InvokeAsync(() => dropdownItems[0].Click());
        Assert.Equal("Test11", cut.Instance.MockDisplayText);

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.ShowFullLevels, true);
        });
        dropdownItems = cut.FindAll(".nav-link");
        await cut.InvokeAsync(() => dropdownItems[0].Click());
        Assert.Equal("Test1/Test11", cut.Instance.MockDisplayText);
    }

    class MockCascader : Cascader<string>
    {
        public string? MockDisplayText => DisplayTextString;
    }
}
