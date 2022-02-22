// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Shared;

namespace UnitTest.Components;

public class RadioTest : BootstrapBlazorTestBase
{
    [Fact]
    public void EnumValue_Ok()
    {
        var v = EnumEducation.Middel;
        var cut = Context.RenderComponent<RadioList<EnumEducation>>(pb =>
        {
            pb.Add(a => a.Items, typeof(EnumEducation).ToSelectList());
            pb.Add(a => a.Value, v);
        });
        Assert.Contains("form-check-input", cut.Markup);
    }

    [Fact]
    public void EnumNullValue_Ok()
    {
        EnumEducation? v = null;
        var cut = Context.RenderComponent<RadioList<EnumEducation?>>(pb =>
        {
            pb.Add(a => a.Items, typeof(EnumEducation).ToSelectList());
            pb.Add(a => a.Value, v);
            pb.Add(a => a.NullItemText, "");
            pb.Add(a => a.IsAutoAddNullItem, true);
        });
        Assert.Contains("form-check-input", cut.Markup);
    }

    [Fact]
    public void NotInItems_Ok()
    {
        // 组件值为 test
        // 组件给的候选 Items 中无 test 选项
        string v = "test";
        var cut = Context.RenderComponent<RadioList<string>>(pb =>
        {
            pb.Add(a => a.Items, new List<SelectedItem>() { new("1", "test1") });
            pb.Add(a => a.Value, v);
        });
        Assert.Equal("1", cut.Instance.Value);

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.Items, new List<SelectedItem>() { });
        });
        Assert.Equal("", cut.Instance.Value);
    }

    [Fact]
    public void OnSelectedChanged_Ok()
    {
        var selected = false;
        var v = EnumEducation.Middel;
        var cut = Context.RenderComponent<RadioList<EnumEducation>>(pb =>
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
        var cut = Context.RenderComponent<RadioList<SelectedItem>>(pb =>
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
        var cut = Context.RenderComponent<RadioList<RadioListGenericMock<string>>>(pb =>
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
        var cut = Context.RenderComponent<Radio<bool>>();
        cut.InvokeAsync(() => cut.Find("input").Click());

        var clicked = false;
        cut.SetParametersAndRender(pb =>
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
        var cut = Context.RenderComponent<Radio<bool>>(pb =>
        {
            pb.Add(a => a.ShowAfterLabel, true);
            pb.Add(a => a.DisplayText, "AfterLabel");
        });
        cut.Contains("AfterLabel");
    }

    [Fact]
    public void ValidateForm_Ok()
    {
        var foo = new Foo();
        var cut = Context.RenderComponent<ValidateForm>(pb =>
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

    private class RadioListGenericMock<T>
    {

    }
}
