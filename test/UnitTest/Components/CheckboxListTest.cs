// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

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
    public void ChildContent_Ok()
    {
        var cut = Context.RenderComponent<Checkbox<string>>(builder =>
        {
            builder.Add(a => a.ChildContent, pb => pb.AddContent(0, "test-childcontent"));
        });
        cut.MarkupMatches("<div class=\"form-check\"><input type=\"checkbox\" diff:ignore class=\"form-check-input\" blazor:onclick=\"1\" /><label class=\"form-check-label\" diff:ignore>test-childcontent</label></div>");
    }

    [Fact]
    public void StopPropagation_Ok()
    {
        var cut = Context.RenderComponent<Checkbox<string>>(builder =>
        {
            builder.Add(a => a.StopPropagation, true);
        });
        Assert.Contains("blazor:onclick:stopPropagation", cut.Markup);
    }

    [Fact]
    public void ShowAfterLabel_Ok()
    {
        var cut = Context.RenderComponent<Checkbox<string>>(builder =>
        {
            builder.Add(a => a.ShowAfterLabel, true);
        });
        cut.MarkupMatches("<div class=\"form-check\"><input class=\"form-check-input\" type=\"checkbox\" diff:ignore /></div>");

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.ShowLabelTooltip, true);
            pb.Add(a => a.DisplayText, "Test");
        });

        var span = cut.Find("span");
        span.MarkupMatches("<span tabindex=\"0\" diff:ignore data-bs-original-title=\"Test\" data-bs-toggle=\"tooltip\" data-bs-placement=\"top\" data-bs-trigger=\"focus hover\">Test</span>");
    }

    [Fact]
    public async Task Checkbox_OnBeforeStateChanged()
    {
        var confirm = true;
        var cut = Context.RenderComponent<Checkbox<bool>>(builder =>
        {
            builder.Add(a => a.OnBeforeStateChanged, state =>
            {
                return Task.FromResult(confirm);
            });
        });
        Assert.False(cut.Instance.Value);

        await cut.InvokeAsync(cut.Instance.OnToggleClick);
        Assert.True(cut.Instance.Value);

        confirm = false;
        await cut.InvokeAsync(cut.Instance.OnToggleClick);
        Assert.True(cut.Instance.Value);
    }

    [Fact]
    public async Task Checkbox_OnTriggerClickAsync()
    {
        var cut = Context.RenderComponent<Checkbox<bool>>();
        Assert.False(cut.Instance.Value);

        // JavaScript 调用 OnStateChangedAsync 方法
        await cut.Instance.OnStateChangedAsync(CheckboxState.UnChecked);
        Assert.Equal(CheckboxState.UnChecked, cut.Instance.State);

        await cut.Instance.OnStateChangedAsync(CheckboxState.Checked);
        Assert.Equal(CheckboxState.Checked, cut.Instance.State);
    }

    [Fact]
    public void Checkbox_Dispose()
    {
        var cut = Context.RenderComponent<Checkbox<string>>();

        var checkbox = cut.Instance;
        cut.InvokeAsync(async () => await checkbox.DisposeAsync());

        var propertyInfo = checkbox.GetType().GetProperty("Module", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
        Assert.NotNull(propertyInfo);
        Assert.Null(propertyInfo.GetValue(checkbox));

        var methodInfo = checkbox.GetType().GetMethod("DisposeAsync", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
        Assert.NotNull(methodInfo);
        methodInfo.Invoke(checkbox, [false]);
    }

    [Fact]
    public void Group_Ok()
    {
        var cut = Context.RenderComponent<BootstrapInputGroup>(pb =>
        {
            pb.AddChildContent<BootstrapInputGroupLabel>(pb =>
            {
                pb.Add(a => a.DisplayText, "GroupLabel");
            });
            pb.AddChildContent<Checkbox<string>>(pb =>
            {
                pb.Add(a => a.ShowLabel, true);
                pb.Add(a => a.DisplayText, "TestLabel");
            });
        });
        Assert.Contains("TestLabel", cut.Markup);
        Assert.Contains("GroupLabel", cut.Markup);
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
                pb.Add(a => a.Items, Foo.GenerateHobbies(Localizer));
                pb.Add(a => a.Value, foo.Hobby);
                pb.Add(a => a.ValueExpression, foo.GenerateValueExpression(nameof(foo.Hobby), typeof(IEnumerable<string>)));
            });
            builder.AddChildContent<Checkbox<bool>>(pb =>
            {
                pb.Add(a => a.ShowLabel, false);
                pb.Add(a => a.ShowAfterLabel, true);
                pb.Add(a => a.Value, foo.Complete);
                pb.Add(a => a.ValueExpression, foo.GenerateValueExpression(nameof(foo.Complete), typeof(bool)));
            });
        });
        // 断言生成 CheckboxList
        Assert.Contains("form-check is-label", cut.Markup);
        cut.Contains("是/否");

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
            pb.Add(a => a.Items, Foo.GenerateHobbies(Localizer));
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
                new() { Text = "Item 1", Value = "1" },
                new() { Text = "Item 2", Value = "2" , IsDisabled = true },
                new() { Text = "Item 3", Value = "3" },
            });
        });
        cut.Contains("form-check is-label disabled");

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.Items, new List<SelectedItem>()
            {
                new() { Text = "Item 1", Value = "1" },
                new() { Text = "Item 2", Value = "2" }
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
            pb.Add(a => a.Items, Foo.GenerateHobbies(Localizer));
        });
        Assert.Contains("test-item", cut.Markup);
    }

    [Fact]
    public async Task StringValue_Ok()
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
                new("1", "Test 1"),
                new("2", "Test 2")
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
        var item = cut.FindComponent<Checkbox<bool>>();
        await cut.InvokeAsync(item.Instance.OnToggleClick);
        Assert.True(selected);
    }

    [Fact]
    public async Task OnSelectedChanged_Ok()
    {
        var selected = false;
        var foo = Foo.Generate(Localizer);
        var cut = Context.RenderComponent<CheckboxList<IEnumerable<string>>>(pb =>
        {
            pb.Add(a => a.Items, Foo.GenerateHobbies(Localizer));
            pb.Add(a => a.Value, foo.Hobby);
            pb.Add(a => a.OnSelectedChanged, (v1, v2) =>
            {
                selected = true;
                return Task.CompletedTask;
            });
        });

        var item = cut.FindComponent<Checkbox<bool>>();
        await cut.InvokeAsync(item.Instance.OnToggleClick);
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
    public async Task IntValue_Ok()
    {
        var ret = new List<int>();
        var selectedIntValues = new List<int> { 1, 2 };
        var cut = Context.RenderComponent<CheckboxList<IEnumerable<int>>>(pb =>
        {
            pb.Add(a => a.Value, selectedIntValues);
            pb.Add(a => a.Items, new List<SelectedItem>()
            {
                new("1", "Test 1"),
                new("2", "Test 2")
            });
            pb.Add(a => a.OnSelectedChanged, (v1, v2) =>
            {
                ret.AddRange(v2);
                return Task.CompletedTask;
            });
        });
        var item = cut.FindComponent<Checkbox<bool>>();
        await cut.InvokeAsync(item.Instance.OnToggleClick);

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
                new("1", "Test 1"),
                new("2", "Test 2")
            });
        });
        cut.InvokeAsync(() =>
        {
            var item = cut.Find(".btn");
            item.Click();
            cut.Contains("btn active bg-danger");
        });
    }

    [Fact]
    public async Task OnMaxSelectedCountExceed_Ok()
    {
        bool max = false;
        var items = new List<SelectedItem>()
        {
            new("1", "Test 1"),
            new("2", "Test 2"),
            new("3", "Test 3")
        };
        var cut = Context.RenderComponent<CheckboxList<string>>(pb =>
        {
            pb.Add(a => a.MaxSelectedCount, 2);
            pb.Add(a => a.Items, items);
            pb.Add(a => a.OnMaxSelectedCountExceed, () =>
            {
                max = true;
                return Task.CompletedTask;
            });
        });
        var checkboxes = cut.FindComponents<Checkbox<bool>>();
        Assert.Equal(3, checkboxes.Count);

        await cut.InvokeAsync(async () =>
        {
            await checkboxes[0].Instance.OnToggleClick();
        });
        Assert.Equal(CheckboxState.Checked, checkboxes[0].Instance.State);

        await cut.InvokeAsync(async () =>
        {
            await checkboxes[1].Instance.OnToggleClick();
        });
        Assert.Equal(CheckboxState.Checked, checkboxes[1].Instance.State);

        // 选中第三个由于限制无法选中
        await cut.InvokeAsync(async () =>
        {
            await checkboxes[2].Instance.OnToggleClick();
        });
        Assert.Equal(CheckboxState.Checked, checkboxes[0].Instance.State);
        Assert.Equal(CheckboxState.Checked, checkboxes[1].Instance.State);
        Assert.Equal(CheckboxState.UnChecked, checkboxes[2].Instance.State);
        Assert.True(max);

        // 取消选择第一个
        max = false;
        await cut.InvokeAsync(async () =>
        {
            await checkboxes[0].Instance.OnToggleClick();
        });
        Assert.Equal(CheckboxState.UnChecked, checkboxes[0].Instance.State);
        Assert.Equal(CheckboxState.Checked, checkboxes[1].Instance.State);
        Assert.Equal(CheckboxState.UnChecked, checkboxes[2].Instance.State);
        Assert.False(max);
    }

    [Fact]
    public void ItemTemplate_Ok()
    {
        var items = new List<SelectedItem>()
        {
            new("1", "Test 1"),
            new("2", "Test 2"),
            new("3", "Test 3")
        };
        var cut = Context.RenderComponent<CheckboxList<string>>(pb =>
        {
            pb.Add(a => a.Items, items);
            pb.Add(a => a.ItemTemplate, item => b =>
            {
                b.AddContent(0, item.Text);
            });
        });
        cut.MarkupMatches("<div diff:ignore class=\"checkbox-list form-control\" tabindex=\"0\" hidefocus=\"true\"><div class=\"checkbox-item\"><div class=\"form-check is-label\"><input type=\"checkbox\" diff:ignore class=\"form-check-input\" blazor:onclick=\"1\" /><label class=\"form-check-label\" diff:ignore>Test 1</label></div></div><div class=\"checkbox-item\"><div class=\"form-check is-label\"><input type=\"checkbox\" diff:ignore class=\"form-check-input\" blazor:onclick=\"2\" /><label class=\"form-check-label\" diff:ignore>Test 2</label></div></div><div class=\"checkbox-item\"><div class=\"form-check is-label\"><input type=\"checkbox\" diff:ignore class=\"form-check-input\" blazor:onclick=\"3\" /><label class=\"form-check-label\" diff:ignore>Test 3</label></div></div></div>");
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
