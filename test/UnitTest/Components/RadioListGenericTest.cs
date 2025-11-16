// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using System.ComponentModel.DataAnnotations;

namespace UnitTest.Components;

public class RadioListGenericTest : BootstrapBlazorTestBase
{
    [Fact]
    public void Enum_NoItems()
    {
        var v = EnumEducation.Middle;
        var cut = Context.Render<RadioListGeneric<EnumEducation>>(pb =>
        {
            pb.Add(a => a.Value, v);
        });
        Assert.Contains("radio-list form-control", cut.Markup);
        Assert.Contains("form-check-input", cut.Markup);
    }

    [Fact]
    public void EnumValue_Ok()
    {
        var v = EnumEducation.Middle;
        var cut = Context.Render<RadioListGeneric<EnumEducation>>(pb =>
        {
            pb.Add(a => a.Items, typeof(EnumEducation).ToSelectList<EnumEducation>());
            pb.Add(a => a.Value, v);
        });
        Assert.Contains("form-check-input", cut.Markup);
    }

    [Fact]
    public void EnumNullValue_Ok()
    {
        EnumEducation? v = null;
        var cut = Context.Render<RadioListGeneric<EnumEducation?>>(pb =>
        {
            pb.Add(a => a.Value, v);
            pb.Add(a => a.NullItemText, "Test-Null");
            pb.Add(a => a.IsAutoAddNullItem, true);
        });
        var items = cut.FindAll("[type='radio']");
        Assert.Equal(3, items.Count);
        Assert.Contains("Test-Null", cut.Markup);
        Assert.Contains("form-check-input", cut.Markup);
    }

    [Fact]
    public async Task SelectedItem_Ok()
    {
        var cut = Context.Render<RadioListGeneric<Foo>>(pb =>
        {
            pb.Add(a => a.Items, new List<SelectedItem<Foo>>
            {
                new(new Foo() { Id = 1, Name = "张三 001" }, "Test1"),
                new(new Foo() { Id = 2, Name = "张三 002" }, "Test2")
            });
            pb.Add(a => a.IsVertical, true);
        });
        cut.Contains("is-vertical");
        var item = cut.Find(".form-check-input");
        await cut.InvokeAsync(() => item.Click());
        Assert.Equal("张三 001", cut.Instance.Value.Name);
    }

    [Fact]
    public void IsDisabled_Ok()
    {
        var cut = Context.Render<RadioListGeneric<Foo>>(pb =>
        {
            pb.Add(a => a.Items, new List<SelectedItem<Foo>>
            {
                new(new Foo() { Id = 1, Name = "张三 001" }, "Test1"),
                new(new Foo() { Id = 2, Name = "张三 002" }, "Test2") { IsDisabled = true }
            });
        });

        // 候选项被禁用
        cut.Contains("disabled=\"disabled\"");

        // 组件被禁用
        cut.Render(pb =>
        {
            pb.Add(a => a.IsDisabled, true);
        });
        cut.Contains("form-check disabled");
    }

    [Fact]
    public void ShowLabel_Ok()
    {
        var cut = Context.Render<RadioListGeneric<Foo>>(pb =>
        {
            pb.Add(a => a.Items, new List<SelectedItem<Foo>>
            {
                new(new Foo() { Id = 1, Name = "张三 001" }, "Test1"),
                new(new Foo() { Id = 2, Name = "张三 002" }, "Test2")
            });
            pb.Add(a => a.ShowLabel, true);
            pb.Add(a => a.DisplayText, "test-label");
        });
        cut.Contains("test-label");
        cut.Contains("form-label");
    }

    [Fact]
    public void NotInItems_Ok()
    {
        var cut = Context.Render<RadioListGeneric<Foo>>();
        Assert.Null(cut.Instance.Value);
        Assert.Equal("<div class=\"radio-list form-control\" role=\"group\"></div>", cut.Markup);

        // 组件值为 test
        // 组件给的候选 Items 中无 test 选项
        var v = new Foo() { Id = 3, Name = "test" };
        cut.Render(pb =>
        {
            pb.Add(a => a.Items, new List<SelectedItem<Foo>>
            {
                new(new Foo() { Id = 1, Name = "张三 001" }, "Test1"),
                new(new Foo() { Id = 2, Name = "张三 002" }, "Test2")
            });
            pb.Add(a => a.Value, v);
            pb.Add(a => a.CustomKeyAttribute, typeof(KeyAttribute));
            pb.Add(a => a.ModelEqualityComparer, new Func<Foo, Foo, bool>((x, y) => x.Id == y.Id));
        });
        Assert.Null(cut.Instance.Value);

        v = new Foo() { Id = 3, Name = "test" };
        cut.Render(pb =>
        {
            pb.Add(a => a.Items, new List<SelectedItem<Foo>>());
            pb.Add(a => a.Value, v);
        });
        Assert.Null(cut.Instance.Value);
    }

    [Fact]
    public async Task OnSelectedChanged_Ok()
    {
        var selected = false;
        var v = EnumEducation.Middle;
        var cut = Context.Render<RadioListGeneric<EnumEducation>>(pb =>
        {
            pb.Add(a => a.Items, typeof(EnumEducation).ToSelectList<EnumEducation>());
            pb.Add(a => a.Value, v);
            pb.Add(a => a.OnSelectedChanged, v =>
            {
                selected = true;
                return Task.CompletedTask;
            });
        });
        var item = cut.Find(".form-check-input");
        await cut.InvokeAsync(() => item.Click());
        Assert.True(selected);
    }

    [Fact]
    public async Task OnSelectedChanged_SelectedItem_Ok()
    {
        var selected = false;
        var v = EnumEducation.Middle;
        var cut = Context.Render<RadioListGeneric<EnumEducation>>(pb =>
        {
            pb.Add(a => a.Items, typeof(EnumEducation).ToSelectList<EnumEducation>());
            pb.Add(a => a.Value, v);
            pb.Add(a => a.OnSelectedChanged, v =>
            {
                selected = true;
                return Task.CompletedTask;
            });
        });
        var item = cut.Find(".form-check-input");
        await cut.InvokeAsync(() => item.Click());
        Assert.True(selected);
        Assert.Equal(EnumEducation.Primary, cut.Instance.Value);
    }

    [Fact]
    public void ItemTemplate_Ok()
    {
        var v = new Foo() { Id = 3, Name = "test" };
        var cut = Context.Render<RadioListGeneric<Foo>>(pb =>
        {
            pb.Add(a => a.Items, new List<SelectedItem<Foo>>
            {
                new(new Foo() { Id = 1, Name = "张三 001" }, "Test1"),
                new(new Foo() { Id = 2, Name = "张三 002" }, "Test2")
            });
            pb.Add(a => a.Value, v);
            pb.Add(a => a.ItemTemplate, v => builder =>
            {
                builder.AddContent(0, $"<div>item-template-{v.Value?.Name}</div>");
            });
        });
        cut.Contains("item-template-张三 001");
        cut.Contains("item-template-张三 002");
    }

    [Fact]
    public async Task IsButton_Ok()
    {
        var cut = Context.Render<RadioListGeneric<EnumEducation>>(pb =>
        {
            pb.Add(a => a.IsButton, true);
            pb.Add(a => a.Items, typeof(EnumEducation).ToSelectList<EnumEducation>());
            pb.Add(a => a.Value, EnumEducation.Middle);
        });
        cut.Contains("radio-list btn-group");

        cut.Render(pb =>
        {
            pb.Add(a => a.Color, Color.Danger);
        });
        cut.Contains("btn active bg-danger");

        var btn = cut.Find(".btn");
        await cut.InvokeAsync(() =>
        {
            btn.Click();
        });
        cut.Contains("btn active bg-danger");
    }

    [Fact]
    public void ShowBorder_Ok()
    {
        var cut = Context.Render<RadioListGeneric<EnumEducation>>(pb =>
        {
            pb.Add(a => a.ShowBorder, false);
            pb.Add(a => a.Items, typeof(EnumEducation).ToSelectList<EnumEducation>());
            pb.Add(a => a.Value, EnumEducation.Middle);
        });
        cut.Contains("no-border");
    }
}
