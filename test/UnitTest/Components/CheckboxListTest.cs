// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Shared;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;

namespace UnitTest.Components;

public class CheckboxListTest : BootstrapBlazorTestBase
{
    private IStringLocalizer<Foo> Localizer { get; }

    public CheckboxListTest()
    {
        Localizer = Context.Services.GetRequiredService<IStringLocalizer<Foo>>();
    }

    [Fact]
    public void Checkbox_Ok()
    {
        var cut = Context.RenderComponent<Checkbox<string>>(builder =>
        {
            builder.Add(a => a.ShowLabel, true);
            builder.Add(a => a.DisplayText, "Test");
        });
        Assert.DoesNotContain("is-label", cut.Markup);
    }

    [Fact]
    public void EditorForm_Ok()
    {
        var foo = Foo.Generate(Localizer);
        var cut = Context.RenderComponent<ValidateForm>(builder =>
        {
            builder.Add(a => a.Model, foo);
            builder.AddChildContent<CheckboxList<IEnumerable<string>>>(pb =>
            {
                pb.Add(a => a.Items, Foo.GenerateHobbys(Localizer));
                pb.Add(a => a.Value, foo.Hobby);
                pb.Add(a => a.ValueExpression, foo.GenerateValueExpression(nameof(foo.Hobby), typeof(IEnumerable<string>)));
            });
        });
        // 断言生成 CheckboxList
        Assert.Contains("form-check is-label", cut.Markup);

        // 提交表单触发客户端验证
        var form = cut.Find("form");
        form.Submit();
        Assert.Contains("is-invalid", cut.Markup);
    }

    [Fact]
    public void ShowBorder_Ok()
    {
        var foo = Foo.Generate(Localizer);
        var cut = Context.RenderComponent<CheckboxList<IEnumerable<string>>>(pb =>
        {
            pb.Add(a => a.Items, Foo.GenerateHobbys(Localizer));
            pb.Add(a => a.Value, foo.Hobby);
        });
        Assert.DoesNotContain("no-border", cut.Markup);

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.ShowBorder, false);
        });
        Assert.Contains("no-border", cut.Markup);
    }

    [Fact]
    public void IsVertical_Ok()
    {
        var cut = Context.RenderComponent<CheckboxList<IEnumerable<int>>>();
        Assert.DoesNotContain("is-vertical", cut.Markup);

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.IsVertical, true);
        });
        Assert.Contains("is-vertical", cut.Markup);
    }

    [Fact]
    public void IsDisabled_Ok()
    {
        var cut = Context.RenderComponent<CheckboxList<IEnumerable<SelectedItem>>>(pb =>
        {
            pb.Add(a => a.Items, new List<SelectedItem>()
            {
                new SelectedItem { Text = "Item 1", Value = "1" },
                new SelectedItem { Text = "Item 2", Value = "2" , IsDisabled = true },
                new SelectedItem { Text = "Item 3", Value = "3" },
            });
        });
        cut.Contains("form-check is-label disabled");

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.Items, new List<SelectedItem>()
            {
                new SelectedItem { Text = "Item 1", Value = "1" },
                new SelectedItem { Text = "Item 2", Value = "2" }
            });
            pb.Add(a => a.IsDisabled, true);
        });
        cut.Contains("form-check is-label disabled");
    }

    [Fact]
    public void CheckboxItemClass_Ok()
    {
        var cut = Context.RenderComponent<CheckboxList<string>>(builder =>
        {
            builder.Add(a => a.CheckboxItemClass, "test-item");
        });
        Assert.DoesNotContain("test-item", cut.Markup);

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.Items, Foo.GenerateHobbys(Localizer));
        });
        Assert.Contains("test-item", cut.Markup);
    }

    [Fact]
    public void StringValue_Ok()
    {
        var cut = Context.RenderComponent<CheckboxList<string>>(builder =>
        {
            builder.Add(a => a.Value, "1,2");
        });
        Assert.Contains("checkbox-list", cut.Markup);

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.Items, new List<SelectedItem>()
            {
                new SelectedItem("1", "Test 1"),
                new SelectedItem("2", "Test 2")
            });
        });
        Assert.Contains("checkbox-list", cut.Markup);

        var selected = false;
        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.OnSelectedChanged, (v1, v2) =>
            {
                selected = true;
                return Task.CompletedTask;
            });
        });
        // 字符串值选中事件
        var item = cut.Find(".form-check-input");
        item.Click();
        Assert.True(selected);
    }

    [Fact]
    public void OnSelectedChanged_Ok()
    {
        var selected = false;
        var foo = Foo.Generate(Localizer);
        var cut = Context.RenderComponent<CheckboxList<IEnumerable<string>>>(pb =>
        {
            pb.Add(a => a.Items, Foo.GenerateHobbys(Localizer));
            pb.Add(a => a.Value, foo.Hobby);
            pb.Add(a => a.OnSelectedChanged, (v1, v2) =>
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
    public void EnumValue_Ok()
    {
        var selectedEnumValues = new List<EnumEducation> { EnumEducation.Middle, EnumEducation.Primary };
        var cut = Context.RenderComponent<CheckboxList<IEnumerable<EnumEducation>>>(pb =>
        {
            pb.Add(a => a.Value, selectedEnumValues);
        });
        Assert.Contains("form-check-input", cut.Markup);
    }

    [Fact]
    public void IntValue_Ok()
    {
        var ret = new List<int>();
        var selectedIntValues = new List<int> { 1, 2 };
        var cut = Context.RenderComponent<CheckboxList<IEnumerable<int>>>(pb =>
        {
            pb.Add(a => a.Value, selectedIntValues);
            pb.Add(a => a.Items, new List<SelectedItem>()
            {
                new SelectedItem("1", "Test 1"),
                new SelectedItem("2", "Test 2")
            });
            pb.Add(a => a.OnSelectedChanged, (v1, v2) =>
            {
                ret.AddRange(v2);
                return Task.CompletedTask;
            });
        });
        var item = cut.Find(".form-check-input");
        item.Click();

        // 选中 2 
        Assert.Equal(2, ret.First());
    }

    [Fact]
    public void NotSupportedException_Error()
    {
        Assert.Throws<NotSupportedException>(() => Context.RenderComponent<CheckboxList<CheckboxListGenericMock<int>>>());
        Assert.Throws<NotSupportedException>(() => Context.RenderComponent<CheckboxList<int>>());
    }

    [Fact]
    public async Task Size_Ok()
    {
        var @checked = false;
        var cut = Context.RenderComponent<Checkbox<string>>(pb =>
        {
            pb.Add(a => a.Size, Size.ExtraExtraLarge);
            pb.Add(a => a.StateChanged, v => @checked = v == CheckboxState.Checked);
        });
        await cut.InvokeAsync(() => cut.Instance.SetState(CheckboxState.Checked));
        Assert.True(@checked);
    }

    [Fact]
    public void FormatValue_Ok()
    {
        var cut = Context.RenderComponent<FormatValueTestCheckboxList>();
        cut.InvokeAsync(() =>
        {
            Assert.Null(cut.Instance.NullValueTest());
            Assert.NotNull(cut.Instance.NotNullValueTest());
        });
    }

    [Fact]
    public void FormatGenericValue_Ok()
    {
        var cut = Context.RenderComponent<FormatValueTestGenericCheckboxList>();
        cut.InvokeAsync(() =>
        {
            Assert.Equal(string.Empty, cut.Instance.NullValueTest());
            Assert.Equal("test", cut.Instance.NotNullValueTest());
        });
    }

    [Fact]
    public void IsButton_Ok()
    {
        var cut = Context.RenderComponent<CheckboxList<IEnumerable<int>>>(pb =>
        {
            pb.Add(a => a.IsButton, true);
            pb.Add(a => a.Color, Color.Danger);
            pb.Add(a => a.Items, new List<SelectedItem>()
            {
                new SelectedItem("1", "Test 1"),
                new SelectedItem("2", "Test 2")
            });
        });
        cut.InvokeAsync(() =>
        {
            var item = cut.Find(".btn");
            item.Click();
            cut.Contains("btn active bg-danger");
        });
    }

    private class CheckboxListGenericMock<T>
    {

    }

    private class FormatValueTestCheckboxList : CheckboxList<string?>
    {
        public string? NullValueTest() => base.FormatValueAsString(null);

        public string? NotNullValueTest() => base.FormatValueAsString("test");
    }

    private class FormatValueTestGenericCheckboxList : CheckboxList<IEnumerable<string>?>
    {
        public string? NullValueTest() => base.FormatValueAsString(null);

        public string? NotNullValueTest()
        {
            Items = new List<SelectedItem>() { new("test", "test") { Active = true } };
            return base.FormatValueAsString(new List<string>() { "test" });
        }
    }
}
