// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Shared;

namespace UnitTest.Components;

public class InputTest : BootstrapBlazorTestBase
{
    [Fact]
    public void Color_Ok()
    {
        var cut = Context.RenderComponent<BootstrapInput<string>>(builder =>
        {
            builder.Add(a => a.Color, Color.None);
            builder.Add(a => a.IsDisabled, false);
        });
        Assert.DoesNotContain("border-", cut.Markup);

        cut.SetParametersAndRender(builder => builder.Add(a => a.Color, Color.Primary));
        Assert.Contains("border-primary", cut.Markup);
    }

    [Fact]
    public void PlaceHolder_Ok()
    {
        var ph = "placeholder_test";
        var cut = Context.RenderComponent<BootstrapInput<string>>(builder => builder.Add(a => a.PlaceHolder, ph));
        Assert.Contains($"placeholder=\"{ph}\"", cut.Markup);
    }

    [Fact]
    public void Value_Ok()
    {
        var model = new Foo() { Name = "Test" };
        var valueChanged = false;
        var cut = Context.RenderComponent<BootstrapInput<string>>(builder =>
        {
            builder.Add(a => a.Value, model.Name);
            builder.Add(a => a.ValueChanged, v => model.Name = v);
            builder.Add(a => a.ValueExpression, model.GenerateValueExpression());
            builder.Add(a => a.OnValueChanged, v => { valueChanged = true; return Task.CompletedTask; });
        });
        cut.Find("input").Change("Test1");
        Assert.Contains(model.Name, "Test1");
        Assert.True(valueChanged);
    }

    [Fact]
    public void IsAutoFocus_Ok()
    {
        var cut = Context.RenderComponent<BootstrapInput<string>>(builder => builder.Add(a => a.IsAutoFocus, true));
    }

    [Fact]
    public void Type_Ok()
    {
        var cut = Context.RenderComponent<BootstrapInput<string>>(builder => builder.AddUnmatched("type", "number"));
        Assert.Contains($"type=\"number\"", cut.Markup);

        cut = Context.RenderComponent<BootstrapInput<string>>();
        Assert.Contains($"type=\"text\"", cut.Markup);
    }

    [Fact]
    public void Readonly_Ok()
    {
        var cut = Context.RenderComponent<BootstrapInput<string>>(builder => builder.Add(a => a.Readonly, true));
        cut.Contains("readonly=\"true\"");
    }

    [Fact]
    public void Password_Ok()
    {
        var cut = Context.RenderComponent<BootstrapPassword>();
        cut.Contains("type=\"password\"");
    }

    [Fact]
    public async Task IsTrim_Ok()
    {
        var val = "    test    ";
        var cut = Context.RenderComponent<BootstrapInput<string>>(builder =>
        {
            builder.Add(a => a.IsTrim, true);
            builder.Add(a => a.Value, "");
        });
        Assert.Equal("", cut.Instance.Value);
        var input = cut.Find("input");
        await cut.InvokeAsync(() => input.Change(val));
        Assert.Equal(val.Trim(), cut.Instance.Value);

        cut.SetParametersAndRender(builder =>
        {
            builder.Add(a => a.IsTrim, false);
            builder.Add(a => a.Value, "");
        });
        Assert.Equal("", cut.Instance.Value);
        input = cut.Find("input");
        await cut.InvokeAsync(() => input.Change(val));
        Assert.Equal(val, cut.Instance.Value);
    }

    [Fact]
    public void Formatter_Ok()
    {
        var cut = Context.RenderComponent<BootstrapInput<DateTime>>(builder =>
        {
            builder.Add(a => a.FormatString, "yyyy-MM-dd");
            builder.Add(a => a.Value, DateTime.Now);
        });
        Assert.Contains($"value=\"{DateTime.Now:yyyy-MM-dd}\"", cut.Markup);

        cut.SetParametersAndRender(builder =>
        {
            builder.Add(a => a.FormatString, null);
            builder.Add(a => a.Formatter, dt => dt.ToString("HH:mm"));
            builder.Add(a => a.Value, DateTime.Now);
        });
        Assert.Contains($"value=\"{DateTime.Now:HH:mm}\"", cut.Markup);
    }

    [Fact]
    public async Task EnterCallback_Ok()
    {
        var val = "";
        var cut = Context.RenderComponent<BootstrapInput<string>>(builder =>
        {
            builder.Add(a => a.OnEnterAsync, v => { val = v; return Task.CompletedTask; });
            builder.Add(a => a.Value, "test");
        });
        await cut.Instance.EnterCallback("Test1");
        Assert.Equal("Test1", val);
    }

    [Fact]
    public async Task EscCallback_Ok()
    {
        var val = "";
        var cut = Context.RenderComponent<BootstrapInput<string>>(builder =>
        {
            builder.Add(a => a.OnEscAsync, v => { val = v; return Task.CompletedTask; });
            builder.Add(a => a.Value, "test");
        });
        await cut.Instance.EscCallback();
        Assert.Equal("test", val);
    }

    [Fact]
    public void IsSelectAllTextOnFocus_Ok()
    {
        var cut = Context.RenderComponent<BootstrapInput<string>>(builder =>
        {
            builder.Add(a => a.IsSelectAllTextOnFocus, true);
        });
    }

    [Fact]
    public async Task IsSelectAllTextOnEnter_Ok()
    {
        var cut = Context.RenderComponent<BootstrapInput<string>>(builder =>
        {
            builder.Add(a => a.IsSelectAllTextOnEnter, true);
        });
        await cut.Instance.SelectAllTextAsync();
    }

    [Fact]
    public void FloatingLabel_Ok()
    {
        var cut = Context.RenderComponent<FloatingLabel<string>>();

        Assert.NotEmpty(cut.Markup);
    }

    [Fact]
    public void GroupLabel_Ok()
    {
        var cut = Context.RenderComponent<BootstrapInputGroupLabel>(builder =>
        {
            builder.Add(s => s.DisplayText, "DisplayText");
        });

        Assert.Contains("DisplayText", cut.Markup);
    }

    [Fact]
    public void GroupIcon_Ok()
    {
        var cut = Context.RenderComponent<BootstrapInputGroupIcon>(builder =>
        {
            builder.Add(s => s.Icon, "fa-solid fa-user");
        });

        var ele = cut.Find(".fa-user");
        Assert.NotNull(ele);
    }

    [Fact]
    public void InputGroup_Ok()
    {
        var cut = Context.RenderComponent<BootstrapInputGroup>(builder =>
        {
            builder.Add(s => s.ChildContent, new RenderFragment(builder =>
            {
                builder.OpenComponent<BootstrapInputGroupLabel>(0);
                builder.AddAttribute(1, nameof(BootstrapInputGroupLabel.DisplayText), "BootstrapInputGroup");
                builder.CloseComponent();
            }));
        });

        Assert.NotEmpty(cut.Markup);
        Assert.Contains("BootstrapInputGroup", cut.Markup);
    }

    [Fact]
    public void Focus_Ok()
    {
        var cut = Context.RenderComponent<Modal>(pb =>
        {
            pb.AddChildContent<BootstrapInput<string>>(pb =>
            {
                pb.Add(a => a.IsAutoFocus, true);
            });
        });
    }

    [Fact]
    public void OnValueChanged_Ok()
    {
        var val = "";
        var foo = new Foo() { Name = "Test" };
        var cut = Context.RenderComponent<BootstrapInput<string>>(builder =>
        {
            builder.Add(a => a.Value, foo.Name);
            builder.Add(a => a.ValueChanged, EventCallback.Factory.Create<string>(this, v =>
            {
                foo.Name = v;
            }));
            builder.Add(a => a.OnValueChanged, v =>
            {
                val = $"{foo.Name}-{v}";
                return Task.CompletedTask;
            });
        });
        var input = cut.Find("input");
        cut.InvokeAsync(() => input.Change("Test_Test"));

        // 保证 ValueChanged 先触发，再触发 OnValueChanged
        Assert.Equal("Test_Test", foo.Name);
        Assert.Equal("Test_Test-Test_Test", val);
    }
}
