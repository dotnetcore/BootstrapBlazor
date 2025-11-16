// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace UnitTest.Components;

public class RadioTest : BootstrapBlazorTestBase
{
    [Fact]
    public void Enum_NoItems()
    {
        var v = EnumEducation.Middle;
        var cut = Context.Render<RadioList<EnumEducation>>(pb =>
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
        var cut = Context.Render<RadioList<EnumEducation>>(pb =>
        {
            pb.Add(a => a.Items, typeof(EnumEducation).ToSelectList());
            pb.Add(a => a.Value, v);
        });
        Assert.Contains("form-check-input", cut.Markup);
    }

    [Fact]
    public void SelectedItem_Ok()
    {
        var cut = Context.Render<RadioList<SelectedItem>>(pb =>
        {
            pb.Add(a => a.Items, new List<SelectedItem> { new("1", "Test1"), new("2", "Test2") });
        });
        var item = cut.Find(".form-check-input");
        item.Click();
    }

    [Fact]
    public void EnumNullValue_Ok()
    {
        EnumEducation? v = null;
        var cut = Context.Render<RadioList<EnumEducation?>>(pb =>
        {
            pb.Add(a => a.Value, v);
            pb.Add(a => a.NullItemText, "Test-Null");
            pb.Add(a => a.IsAutoAddNullItem, true);
        });
        var items = cut.FindComponents<Radio<SelectedItem>>();
        Assert.Equal(3, items.Count);
        Assert.Contains("Test-Null", cut.Markup);
        Assert.Contains("form-check-input", cut.Markup);
    }

    [Fact]
    public void NotInItems_Ok()
    {
        // 组件值为 test
        // 组件给的候选 Items 中无 test 选项
        string v = "test";
        var cut = Context.Render<RadioList<string>>(pb =>
        {
            pb.Add(a => a.Items, new List<SelectedItem>() { new("1", "test1") });
            pb.Add(a => a.Value, v);
        });
        Assert.Equal("1", cut.Instance.Value);

        cut.Render(pb =>
        {
            pb.Add(a => a.Items, new List<SelectedItem>() { });
        });
        Assert.Equal("", cut.Instance.Value);
    }

    [Fact]
    public void OnSelectedChanged_Ok()
    {
        var selected = false;
        var v = EnumEducation.Middle;
        var cut = Context.Render<RadioList<EnumEducation>>(pb =>
        {
            pb.Add(a => a.Items, typeof(EnumEducation).ToSelectList());
            pb.Add(a => a.Value, v);
            pb.Add(a => a.OnSelectedChanged, (items, v) =>
            {
                selected = true;
                return Task.CompletedTask;
            });
        });
        var item = cut.Find(".form-check-input");
        item.Click();
        Assert.True(selected);
    }

    [Fact]
    public void OnSelectedChanged_SelectedItem_Ok()
    {
        var selected = false;
        var v = new SelectedItem(nameof(EnumEducation.Primary), "Test 1");
        var cut = Context.Render<RadioList<SelectedItem>>(pb =>
        {
            pb.Add(a => a.Items, typeof(EnumEducation).ToSelectList());
            pb.Add(a => a.Value, v);
            pb.Add(a => a.OnSelectedChanged, (items, v) =>
            {
                selected = true;
                return Task.CompletedTask;
            });
        });
        var item = cut.Find(".form-check-input");
        item.Click();
        Assert.True(selected);
    }

    [Fact]
    public void GenericValue_Ok()
    {
        var cut = Context.Render<RadioList<RadioListGenericMock<string>>>(pb =>
        {
            pb.Add(a => a.Items, typeof(EnumEducation).ToSelectList());
            pb.Add(a => a.Color, Color.Primary);
            pb.Add(a => a.Value, new RadioListGenericMock<string>());
        });
        Assert.Contains("primary", cut.Markup);
    }

    [Fact]
    public void OnClick_Ok()
    {
        var cut = Context.Render<Radio<bool>>();
        cut.InvokeAsync(() => cut.Find("input").Click());

        var clicked = false;
        cut.Render(pb =>
        {
            pb.Add(a => a.OnClick, v =>
            {
                clicked = true;
                return Task.CompletedTask;
            });
        });
        cut.InvokeAsync(() => cut.Find("input").Click());
        Assert.True(clicked);
    }

    [Fact]
    public void ShowLabel_Ok()
    {
        var cut = Context.Render<Radio<bool>>(pb =>
        {
            pb.Add(a => a.ShowAfterLabel, true);
            pb.Add(a => a.DisplayText, "AfterLabel");
        });
        cut.Contains("AfterLabel");
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void ItemTemplate_Ok(bool isButton)
    {
        var cut = Context.Render<RadioList<IEnumerable<SelectedItem>>>(pb =>
        {
            pb.Add(a => a.IsButton, isButton);
            pb.Add(a => a.Items, new List<SelectedItem>
            {
                new("1", "Test1"),
                new("2", "Test2")
            });
            pb.Add(a => a.ItemTemplate, v => builder =>
            {
                builder.AddContent(0, $"<div>item-template-{v.Value}</div>");
            });
        });
        cut.Contains("item-template-1");
        cut.Contains("item-template-2");
    }

    [Fact]
    public void AutoSelectFirstWhenValueIsNull_Ok()
    {
        var cut = Context.Render<RadioList<SelectedItem>>(pb =>
        {
            pb.Add(a => a.AutoSelectFirstWhenValueIsNull, false);
            pb.Add(a => a.Items, new List<SelectedItem>
            {
                new("1", "Test1"),
                new("2", "Test2")
            });
            pb.Add(a => a.Value, new SelectedItem());
        });
        cut.Contains("class=\"form-check-label\"");
        cut.DoesNotContain("is-checked");
    }

    [Fact]
    public void ValidateForm_Ok()
    {
        var foo = new Foo();
        var cut = Context.Render<ValidateForm>(pb =>
        {
            pb.Add(a => a.Model, foo);
            pb.AddChildContent<RadioList<IEnumerable<string>>>(pb =>
            {
                pb.Add(a => a.Items, new List<SelectedItem>
                {
                    new("1", "Test1"),
                    new("2", "Test2")
                });
                pb.Add(a => a.Value, foo.Hobby);
                pb.Add(a => a.ValueExpression, foo.GenerateValueExpression(nameof(Foo.Hobby), typeof(IEnumerable<string>)));
            });
        });
        cut.Contains("class=\"form-label\"");
    }

    [Fact]
    public async Task IsButton_Ok()
    {
        var cut = Context.Render<RadioList<EnumEducation>>(pb =>
        {
            pb.Add(a => a.IsButton, true);
            pb.Add(a => a.Items, typeof(EnumEducation).ToSelectList());
            pb.Add(a => a.Value, EnumEducation.Middle);
        });
        cut.Contains("radio-list btn-group");

        cut.Render(pb =>
        {
            pb.Add(a => a.Color, Color.Danger);
        });
        cut.Contains("btn border-secondary");

        var btn = cut.Find(".btn");
        await cut.InvokeAsync(() =>
        {
            btn.Click();
        });
        cut.Contains("btn border-secondary active bg-danger");
    }

    [Fact]
    public void ShowBorder_Ok()
    {
        var cut = Context.Render<RadioList<EnumEducation>>(pb =>
        {
            pb.Add(a => a.ShowBorder, false);
            pb.Add(a => a.Items, typeof(EnumEducation).ToSelectList());
            pb.Add(a => a.Value, EnumEducation.Middle);
        });
        cut.Contains("no-border");
    }

    private class RadioListGenericMock<T>
    {

    }
}
