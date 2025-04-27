// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

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
    public void Clearable_Ok()
    {
        var cut = Context.RenderComponent<BootstrapInput<string>>(builder => builder.Add(a => a.IsClearable, false));
        cut.DoesNotContain("bb-clearable-input");

        cut.SetParametersAndRender(pb => pb.Add(a => a.IsClearable, true));
        cut.Contains("bb-clearable-input");
        cut.Contains("form-control-clear-icon");

        cut.SetParametersAndRender(pb => pb.Add(a => a.Readonly, true));
        cut.DoesNotContain("form-control-clear-icon");

        cut.SetParametersAndRender(pb => pb.Add(a => a.Readonly, false));
        cut.SetParametersAndRender(pb => pb.Add(a => a.IsDisabled, true));
        cut.DoesNotContain("form-control-clear-icon");
    }

    [Fact]
    public async Task OnClear_Ok()
    {
        var clicked = false;
        var cut = Context.RenderComponent<BootstrapInput<string>>(builder =>
        {
            builder.Add(a => a.IsClearable, true);
            builder.Add(a => a.OnClear, v =>
            {
                clicked = true;
                return Task.CompletedTask;
            });
        });
        var icon = cut.Find(".form-control-clear-icon");
        await cut.InvokeAsync(() => icon.Click());
        Assert.True(clicked);
    }

    [Fact]
    public async Task OnInput_Ok()
    {
        var foo = new Foo() { Name = "Test" };
        var cut = Context.RenderComponent<BootstrapInput<string>>(builder =>
        {
            builder.Add(a => a.Value, foo.Name);
            builder.Add(a => a.UseInputEvent, true);
            builder.Add(a => a.ValueChanged, EventCallback.Factory.Create<string?>(this, v =>
            {
                foo.Name = v;
            }));
        });
        cut.Contains("blazor:oninput");

        // 输入字符
        var input = cut.Find("input");
        await cut.InvokeAsync(() =>
        {
            input.Input("1");
        });
        Assert.Equal("1", foo.Name);
    }

    [Fact]
    public void Password_Ok()
    {
        var cut = Context.RenderComponent<BootstrapPassword>();
        cut.Contains("type=\"password\"");
    }

    [Fact]
    public void IsTrim_Ok()
    {
        var val = "    test    ";
        var cut = Context.RenderComponent<BootstrapInput<string>>(builder =>
        {
            builder.Add(a => a.IsTrim, true);
            builder.Add(a => a.Value, "");
        });
        Assert.Equal("", cut.Instance.Value);

        var input = cut.Find("input");
        cut.InvokeAsync(() => input.Change(val));
        cut.WaitForAssertion(() => Assert.Equal(val.Trim(), cut.Instance.Value));

        cut.SetParametersAndRender(builder =>
        {
            builder.Add(a => a.IsTrim, false);
            builder.Add(a => a.Value, "");
        });
        cut.WaitForAssertion(() => Assert.Equal("", cut.Instance.Value));

        input = cut.Find("input");
        cut.InvokeAsync(() => input.Change(val));
        cut.WaitForAssertion(() => Assert.Equal(val, cut.Instance.Value));
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
        cut.WaitForAssertion(() => Assert.Contains($"value=\"{DateTime.Now:HH:mm}\"", cut.Markup));
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
        await cut.Instance.EnterCallback();
        Assert.Equal("test", val);
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
        cut.Contains("<div class=\"form-floating\">");

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.IsGroupBox, true);
        });
        cut.Contains("<div class=\"form-floating is-group\">");

        // PlaceHolder
        var foo = new Foo() { Name = "Foo" };
        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.Value, "test");
        });
        var input = cut.Find("input");
        Assert.Null(input.GetAttribute("placeholder"));

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.ValueExpression, Utility.GenerateValueExpression(foo, "Name", typeof(string)));
        });
        input = cut.Find("input");
        Assert.Equal("姓名", input.GetAttribute("placeholder"));

        // PlaceHolder
        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.PlaceHolder, "fl-pl");
        });
        input = cut.Find("input");
        Assert.Equal("fl-pl", input.GetAttribute("placeholder"));
    }

    [Fact]
    public void GroupLabel_Ok()
    {
        var cut = Context.RenderComponent<BootstrapInputGroupLabel>(builder =>
        {
            builder.Add(s => s.DisplayText, "DisplayText");
        });

        Assert.Contains("DisplayText", cut.Markup);

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.ChildContent, builder => builder.AddContent(0, "test-child-content"));
        });
        cut.Contains("test-child-content");
        cut.DoesNotContain("DisplayText");
    }

    [Fact]
    public void ShowRequiredMark_Ok()
    {
        var cut = Context.RenderComponent<BootstrapInputGroupLabel>(builder =>
        {
            builder.Add(s => s.DisplayText, "DisplayText");
            builder.Add(s => s.ShowRequiredMark, true);
        });

        cut.MarkupMatches("<label class=\"form-label\" required=\"true\">DisplayText</label>");
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
    public void InputGroup_Width()
    {
        var cut = Context.RenderComponent<BootstrapInputGroup>(builder =>
        {
            builder.Add(s => s.ChildContent, new RenderFragment(builder =>
            {
                builder.OpenComponent<BootstrapInputGroupLabel>(0);
                builder.AddAttribute(1, nameof(BootstrapInputGroupLabel.DisplayText), "BootstrapInputGroup");
                builder.AddAttribute(2, nameof(BootstrapInputGroupLabel.ShowRequiredMark), true);
                builder.AddAttribute(2, nameof(BootstrapInputGroupLabel.Width), 120);
                builder.CloseComponent();
            }));
        });

        cut.MarkupMatches("<div class=\"input-group\"><div class=\"input-group-text\" required=\"true\" style=\"--bb-input-group-label-width: 120px;\"><span>BootstrapInputGroup</span></div></div>");
    }

    [Fact]
    public void InputGroup_ChildContent()
    {
        var cut = Context.RenderComponent<BootstrapInputGroup>(builder =>
        {
            builder.Add(s => s.ChildContent, new RenderFragment(builder =>
            {
                builder.OpenComponent<BootstrapInputGroupLabel>(0);
                builder.AddAttribute(1, nameof(BootstrapInputGroupLabel.ChildContent), new RenderFragment(builder => builder.AddContent(0, "child-content")));
                builder.CloseComponent();
            }));
        });

        cut.Contains("child-content");
    }

    [Theory]
    [InlineData(Alignment.Center, "center")]
    [InlineData(Alignment.Right, "end")]
    public void InputGroup_Alignment(Alignment alignment, string expected)
    {
        var cut = Context.RenderComponent<BootstrapInputGroup>(builder =>
        {
            builder.Add(s => s.ChildContent, new RenderFragment(builder =>
            {
                builder.OpenComponent<BootstrapInputGroupLabel>(0);
                builder.AddAttribute(1, nameof(BootstrapInputGroupLabel.DisplayText), "BootstrapInputGroup");
                builder.AddAttribute(2, nameof(BootstrapInputGroupLabel.Alignment), alignment);
                builder.CloseComponent();
            }));
        });

        cut.Contains($"justify-content-{expected}");
    }

    [Fact]
    public void Focus_Ok()
    {
        var cut = Context.RenderComponent<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<Modal>(pb =>
            {
                pb.AddChildContent<BootstrapInput<string>>(pb =>
                {
                    pb.Add(a => a.IsAutoFocus, true);
                });
            });
        });
    }

    [Fact]
    public async Task AutoSetDefaultWhenNull_Ok()
    {
        var cut = Context.RenderComponent<BootstrapInput<int>>(builder =>
        {
            builder.Add(a => a.Value, 123);
            builder.Add(a => a.AutoSetDefaultWhenNull, true);
        });
        await cut.InvokeAsync(() =>
        {
            var input = cut.Find("input");
            input.Change("");
        });
        Assert.Equal(0, cut.Instance.Value);
    }

    [Fact]
    public void OnValueChanged_Ok()
    {
        var val = "";
        var foo = new Foo() { Name = "Test" };
        var cut = Context.RenderComponent<BootstrapInput<string>>(builder =>
        {
            builder.Add(a => a.Value, foo.Name);
            builder.Add(a => a.ValueChanged, EventCallback.Factory.Create<string?>(this, v =>
            {
                foo.Name = v;
            }));
            builder.Add(a => a.OnValueChanged, v =>
            {
                val = $"{foo.Name}-{v}";
                return Task.CompletedTask;
            });
        });
        cut.InvokeAsync(() =>
        {
            var input = cut.Find("input");
            input.Change("Test_Test");

            // 保证 ValueChanged 先触发，再触发 OnValueChanged
            Assert.Equal("Test_Test", foo.Name);
            Assert.Equal("Test_Test-Test_Test", val);
        });
    }

    [Fact]
    public async Task OnBlurAsync_Ok()
    {
        var blur = false;
        var cut = Context.RenderComponent<BootstrapInput<string>>(builder =>
        {
            builder.Add(a => a.OnBlurAsync, v =>
            {
                blur = true;
                return Task.CompletedTask;
            });
        });
        var input = cut.Find("input");
        await cut.InvokeAsync(() => { input.Blur(); });
        Assert.True(blur);
    }
}
