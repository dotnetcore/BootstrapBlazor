// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using System.ComponentModel.DataAnnotations;

namespace UnitTest.Components;

public class CheckboxListGenericTest : BootstrapBlazorTestBase
{
    [Fact]
    public void EditorForm_Ok()
    {
        var dummy = new Dummy() { Data = [new() { Id = 2, Name = "Test2" }] };
        var items = new List<SelectedItem<Foo>>()
        {
            new(new Foo() { Id = 1, Name = "Test1" }, "Test 1"),
            new(new Foo() { Id = 2, Name = "Test2" }, "Test 2")
        };
        var cut = Context.RenderComponent<ValidateForm>(builder =>
        {
            builder.Add(a => a.Model, dummy);
            builder.AddChildContent<CheckboxListGeneric<Foo>>(pb =>
            {
                pb.Add(a => a.Items, items);
                pb.Add(a => a.Value, dummy.Data);
                pb.Add(a => a.ValueExpression, Utility.GenerateValueExpression(dummy, nameof(dummy.Data), typeof(List<Foo>)));
            });
        });
        // 断言生成 CheckboxList
        Assert.Contains("form-check is-label", cut.Markup);

        // 提交表单触发客户端验证
        var form = cut.Find("form");
        form.Submit();
        Assert.Contains("is-valid", cut.Markup);
    }

    [Fact]
    public void ShowBorder_Ok()
    {
        var items = new List<SelectedItem<Foo>>()
        {
            new(new Foo() { Id = 1, Name = "Test1" }, "Test 1"),
            new(new Foo() { Id = 2, Name = "Test2" }, "Test 2")
        };
        var cut = Context.RenderComponent<CheckboxListGeneric<Foo>>(pb =>
        {
            pb.Add(a => a.Items, items);
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
        var cut = Context.RenderComponent<CheckboxListGeneric<Foo>>();
        Assert.DoesNotContain("is-vertical", cut.Markup);

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.IsVertical, true);
            pb.Add(a => a.CustomKeyAttribute, typeof(KeyAttribute));
            pb.Add(a => a.ModelEqualityComparer, new Func<Foo, Foo, bool>((x, y) => x.Id == y.Id));
        });
        Assert.Contains("is-vertical", cut.Markup);
    }

    [Fact]
    public async Task NullItem_Ok()
    {
        var items = new List<SelectedItem<Foo?>>()
        {
            new(null, "Select ..."),
            new(new Foo() { Id = 2, Name = "Test2" }, "Test 2")
        };
        var cut = Context.RenderComponent<CheckboxListGeneric<Foo?>>(pb =>
        {
            pb.Add(a => a.Items, items);
        });
        cut.Contains("Select ...");

        var checkboxes = cut.FindComponents<Checkbox<bool>>();
        await cut.InvokeAsync(async () =>
        {
            await checkboxes[0].Instance.OnToggleClick();
        });
        Assert.Null(cut.Instance.Value[0]);
    }

    [Fact]
    public void IsDisabled_Ok()
    {
        var items = new List<SelectedItem<Foo>>()
        {
            new(new Foo() { Id = 1, Name = "Test1" }, "Test 1"),
            new(new Foo() { Id = 2, Name = "Test2" }, "Test 2") {  IsDisabled = true }
        };
        var cut = Context.RenderComponent<CheckboxListGeneric<Foo>>(pb =>
        {
            pb.Add(a => a.Items, items);
        });
        cut.Contains("form-check is-label disabled");

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.Items, new List<SelectedItem<Foo>>()
            {
                new(new Foo() { Id = 1, Name = "Test1" }, "Test 1"),
                new(new Foo() { Id = 2, Name = "Test2" }, "Test 2")
            });
            pb.Add(a => a.IsDisabled, true);
        });
        cut.Contains("form-check is-label disabled");
    }

    [Fact]
    public void CheckboxItemClass_Ok()
    {
        var cut = Context.RenderComponent<CheckboxListGeneric<Foo>>(builder =>
        {
            builder.Add(a => a.CheckboxItemClass, "test-item");
        });
        Assert.DoesNotContain("test-item", cut.Markup);

        var items = new List<SelectedItem<Foo>>()
        {
            new(new Foo() { Id = 1, Name = "Test1" }, "Test 1"),
            new(new Foo() { Id = 2, Name = "Test2" }, "Test 2")
        };
        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.Items, items);
        });
        Assert.Contains("test-item", cut.Markup);
    }

    [Fact]
    public async Task StringValue_Ok()
    {
        var items = new List<SelectedItem<Foo>>()
        {
            new(new Foo() { Id = 1, Name = "Test1" }, "Test 1"),
            new(new Foo() { Id = 2, Name = "Test2" }, "Test 2")
        };
        var cut = Context.RenderComponent<CheckboxListGeneric<Foo>>(pb =>
        {
            pb.Add(a => a.Items, items);
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
    public async Task IsButton_Ok()
    {
        var items = new List<SelectedItem<Foo>>()
        {
            new(new Foo() { Id = 1, Name = "Test1" }, "Test 1"),
            new(new Foo() { Id = 2, Name = "Test2" }, "Test 2")
        };
        var cut = Context.RenderComponent<CheckboxListGeneric<Foo>>(pb =>
        {
            pb.Add(a => a.IsButton, true);
            pb.Add(a => a.Color, Color.None);
            pb.Add(a => a.Items, items);
        });
        var item = cut.Find(".btn");
        await cut.InvokeAsync(() =>
        {
            item.Click();
        });
        cut.Contains("btn active bg-primary");
    }

    [Fact]
    public async Task OnMaxSelectedCountExceed_Ok()
    {
        bool max = false;
        var items = new List<SelectedItem<Foo>>()
        {
            new(new Foo() { Id = 1, Name = "Test1" }, "Test 1"),
            new(new Foo() { Id = 2, Name = "Test2" }, "Test 2"),
            new(new Foo() { Id = 3, Name = "Test3" }, "Test 3")
        };
        var cut = Context.RenderComponent<CheckboxListGeneric<Foo>>(pb =>
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
        var items = new List<SelectedItem<Foo>>()
        {
            new(new Foo() { Id = 1, Name = "Test1" }, "Test 1"),
            new(new Foo() { Id = 2, Name = "Test2" }, "Test 2"),
            new(new Foo() { Id = 3, Name = "Test3" }, "Test 3")
        };
        var cut = Context.RenderComponent<CheckboxListGeneric<Foo>>(pb =>
        {
            pb.Add(a => a.Items, items);
            pb.Add(a => a.ItemTemplate, foo => builder =>
            {
                builder.AddContent(0, foo.Text);
            });
        });
        var labels = cut.FindAll(".checkbox-item .form-check-label");
        Assert.Equal(3, labels.Count);
    }

    private class Dummy
    {
        [Required]
        public List<Foo>? Data { get; set; }
    }
}
