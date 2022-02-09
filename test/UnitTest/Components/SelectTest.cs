// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Shared;
using UnitTest.Extensions;

namespace UnitTest.Components;

public class SelectTest : BootstrapBlazorTestBase
{
    [Fact]
    public void OnSearchTextChanged_Null()
    {
        var cut = Context.RenderComponent<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<Select<string>>(pb =>
            {
                pb.Add(a => a.ShowSearch, true);
                pb.Add(a => a.Items, new List<SelectedItem>()
                {
                    new SelectedItem("1", "Test1"),
                    new SelectedItem("2", "Test2") { IsDisabled = true }
                });
            });
        });

        var ctx = cut.FindComponent<Select<string>>();
        ctx.InvokeAsync(() => ctx.Instance.ConfirmSelectedItem(0));

        // 搜索 T
        ctx.Find(".search-text").Input("T");
        ctx.InvokeAsync(() => ctx.Instance.ConfirmSelectedItem(0));

        ctx.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.OnBeforeSelectedItemChange, item => Task.FromResult(false));
            pb.Add(a => a.OnSelectedItemChanged, item => Task.CompletedTask);
        });
        ctx.InvokeAsync(() => ctx.Instance.ConfirmSelectedItem(0));
    }

    [Fact]
    public void Options_Ok()
    {
        var cut = Context.RenderComponent<Select<string>>(pb =>
        {
            pb.Add(a => a.Options, builder =>
            {
                builder.OpenComponent<SelectOption>(0);
                builder.AddAttribute(1, "Text", "Test-Select");
                builder.CloseComponent();

                builder.OpenComponent<SelectOption>(2);
                builder.CloseComponent();
            });
        });
        Assert.Contains("Test-Select", cut.Markup);
    }

    [Fact]
    public void SelectOption_Ok()
    {
        var cut = Context.RenderComponent<SelectOption>(pb =>
        {
            pb.Add(a => a.Text, "Test-SelectOption");
            pb.Add(a => a.GroupName, "Test-GroupName");
            pb.Add(a => a.IsDisabled, false);
            pb.Add(a => a.Active, true);
            pb.Add(a => a.Value, "");
        });
    }

    [Fact]
    public void Enum_Ok()
    {
        var cut = Context.RenderComponent<Select<EnumEducation>>();
        Assert.Equal(2, cut.FindAll(".dropdown-item").Count);
    }

    [Fact]
    public void NullableEnum_Ok()
    {
        var cut = Context.RenderComponent<Select<EnumEducation?>>(pb =>
        {
            pb.Add(a => a.AdditionalAttributes, new Dictionary<string, object>()
            {
                ["placeholder"] = ""
            });
        });
        Assert.Equal(3, cut.FindAll(".dropdown-item").Count);
    }

    [Fact]
    public void OnSelectedItemChanged_OK()
    {
        var triggered = false;
        // 首次加载触发 OnSelectedItemChanged 回调测试
        var cut = Context.RenderComponent<Select<string>>(pb =>
        {
            pb.Add(a => a.Items, new SelectedItem[]
            {
                new SelectedItem("1", "Test1"),
                new SelectedItem("2", "Test2")
            });
            pb.Add(a => a.Value, "2");
            pb.Add(a => a.OnSelectedItemChanged, item =>
            {
                triggered = true;
                return Task.CompletedTask;
            });
        });
        Assert.True(triggered);
    }

    [Fact]
    public void Color_Ok()
    {
        var cut = Context.RenderComponent<Select<string>>(pb =>
        {
            pb.Add(a => a.Color, Color.Danger);
        });
        Assert.Contains("border-danger", cut.Markup);
    }

    [Fact]
    public void Validate_Ok()
    {
        var valid = false;
        var invalid = false;
        var model = new Foo() { Name = "Test-Select1" };
        var cut = Context.RenderComponent<ValidateForm>(builder =>
        {
            builder.Add(a => a.OnValidSubmit, context =>
            {
                valid = true;
                return Task.CompletedTask;
            });
            builder.Add(a => a.OnInvalidSubmit, context =>
            {
                invalid = true;
                return Task.CompletedTask;
            });
            builder.Add(a => a.Model, model);
            builder.AddChildContent<Select<string>>(pb =>
            {
                pb.Add(a => a.Value, model.Name);
                pb.Add(a => a.OnValueChanged, v =>
                {
                    model.Name = v;
                    return Task.CompletedTask;
                });
                pb.Add(a => a.ValueExpression, model.GenerateValueExpression());
                pb.Add(a => a.Items, new SelectedItem[]
                {
                    new SelectedItem("", "Test"),
                    new SelectedItem("1", "Test1") { GroupName = "Test1" },
                    new SelectedItem("2", "Test2") { GroupName = "Test2" }
                });
            });
        });
        var form = cut.Find("form");
        form.Submit();
        Assert.True(valid);

        var ctx = cut.FindComponent<Select<string>>();
        ctx.InvokeAsync(() => ctx.Instance.ConfirmSelectedItem(0));
        form.Submit();
        Assert.True(invalid);
    }

    [Fact]
    public void ItemTemplate_Ok()
    {
        var cut = Context.RenderComponent<Select<string>>(pb =>
        {
            pb.Add(a => a.Items, new SelectedItem[]
            {
                new SelectedItem("1", "Test1") { GroupName = "Test1" },
                new SelectedItem("2", "Test2") { GroupName = "Test2" }
            });
            pb.Add(a => a.Value, "2");
            pb.Add(a => a.ItemTemplate, item => builder =>
            {
                builder.OpenElement(0, "div");
                builder.AddContent(1, item.Text);
                builder.CloseComponent();
            });
        });

        cut.Find(".dropdown-item").Click();
    }

    [Fact]
    public void OnBeforeSelectedItemChange_Ok()
    {
        var cut = Context.RenderComponent<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<Select<string>>(pb =>
            {
                pb.Add(a => a.OnBeforeSelectedItemChange, item => Task.FromResult(true));
                pb.Add(a => a.SwalFooter, "Test-Swal-Footer");
                pb.Add(a => a.SwalCategory, SwalCategory.Question);
                pb.Add(a => a.SwalTitle, "Test-Swal-Title");
                pb.Add(a => a.SwalContent, "Test-Swal-Content");
                pb.Add(a => a.Items, new SelectedItem[]
                {
                    new SelectedItem("1", "Test1"),
                    new SelectedItem("2", "Test2")
                });
            });
        });
        cut.Find(".dropdown-item").Click();
        //Assert.Contains("Test-Swal-Title", cut.Markup);
        //Assert.Contains("Test-Swal-Content", cut.Markup);
        //Assert.Contains("Test-Swal-Footer", cut.Markup);
    }

    [Fact]
    public void NullItems_Ok()
    {
        var cut = Context.RenderComponent<Select<string>>();
        Assert.Contains("select", cut.Markup);
    }
}
