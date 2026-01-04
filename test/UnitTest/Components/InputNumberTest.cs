// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using System.ComponentModel.DataAnnotations;

namespace UnitTest.Components;

public class InputNumberTest : BootstrapBlazorTestBase
{
    [Theory]
    [InlineData(null)]
    [InlineData(0.0)]
    public async Task OnInput_Ok(double? v)
    {
        double? value = 0.0;
        var cut = Context.Render<BootstrapInputNumber<double?>>(builder =>
        {
            builder.Add(a => a.Value, v);
            builder.Add(a => a.UseInputEvent, true);
            builder.Add(a => a.ValueChanged, EventCallback.Factory.Create<double?>(this, v =>
            {
                value = v;
            }));
        });
        var input = cut.Find("input");
        await cut.InvokeAsync(() =>
        {
            input.Input("0.0");
        });
        cut.Contains("value=\"0.0\"");

        await cut.InvokeAsync(() =>
        {
            input.Input("0.01");
        });
        cut.Contains("value=\"0.01\"");
    }

    [Fact]
    public void OnBlur_Ok()
    {
        var cut = Context.Render<BootstrapInputNumber<int>>(pb =>
        {
            pb.Add(a => a.Min, "0");
            pb.Add(a => a.Max, "10");
            pb.Add(a => a.Step, "2");
        });
        cut.Contains("min=\"0\"");
        cut.Contains("max=\"10\"");
        cut.Contains("step=\"2\"");

        var input = cut.Find("input");
        cut.InvokeAsync(() => input.Blur());
    }

    [Fact]
    public void ValidateForm()
    {
        var foo = new Cat() { Count = 20 };
        var cut = Context.Render<ValidateForm>(pb =>
        {
            pb.Add(a => a.Model, foo);
            pb.AddChildContent<BootstrapInputNumber<int>>(pb =>
            {
                pb.Add(a => a.Value, foo.Count);
                pb.Add(a => a.ValueExpression, Utility.GenerateValueExpression(foo, nameof(Cat.Count), typeof(int)));
            });
        });
        cut.Contains("class=\"form-label\"");

        var input = cut.Find("input");
        cut.InvokeAsync(() => input.Change(""));

        var form = cut.Find("form");
        cut.InvokeAsync(() => form.Submit());
        cut.Contains("is-invalid");
    }

    [Fact]
    public void InvalidOperationException_Error()
    {
        Assert.ThrowsAny<InvalidOperationException>(() => Context.Render<BootstrapInputNumber<string>>());
    }

    [Fact]
    public void Formatter_Ok()
    {
        var cut = Context.Render<BootstrapInputNumber<decimal>>(pb =>
        {
            pb.Add(a => a.Value, 10.01m);
            pb.Add(a => a.Formatter, v => $"{v + 1}");
        });
        var input = cut.Find("input");
        Assert.Equal("11.01", input.GetAttribute("value"));

        cut.Render(pb =>
        {
            pb.Add(a => a.Formatter, null);
            pb.Add(a => a.FormatString, "#0.0");
        });
        Assert.Equal("10.0", input.GetAttribute("value"));

        input = cut.Find("input");
        cut.InvokeAsync(() => input.Change(""));
    }

    [Fact]
    public void Formatter_Null()
    {
        var cut = Context.Render<BootstrapInputNumber<int?>>(pb =>
        {
            pb.Add(a => a.FormatString, "d2");
        });
        cut.Contains("value=\"\"");
    }

    [Fact]
    public void Formatter_Error()
    {
        Assert.ThrowsAny<InvalidOperationException>(() => Context.Render<MockInputNumber>());
    }

    [Fact]
    public async Task Nullable_Ok()
    {
        var cut = Context.Render<BootstrapInputNumber<int?>>(pb =>
        {
            pb.Add(a => a.Value, 5);
        });
        var input = cut.Find("input");
        await cut.InvokeAsync(() =>
        {
            input.Change("1+2");
            input.Blur();
        });
        Assert.Null(cut.Instance.Value);
    }

    [Fact]
    public async Task OnBlurAsync_Ok()
    {
        var blur = false;
        var cut = Context.Render<BootstrapInputNumber<int>>(builder =>
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

    [Fact]
    public async Task ShowButton_Ok()
    {
        var inc = false;
        var dec = false;
        var cut = Context.Render<BootstrapInputNumber<int>>(pb =>
        {
            pb.Add(a => a.ShowButton, true);
            pb.Add(a => a.OnIncrement, v =>
            {
                dec = true;
                return Task.CompletedTask;
            });
            pb.Add(a => a.OnDecrement, v =>
            {
                inc = true;
                return Task.CompletedTask;
            });
        });
        cut.Contains("class=\"input-group\"");

        var buttons = cut.FindAll("button");
        await cut.InvokeAsync(() => buttons[0].Click());
        Assert.True(inc);
        Assert.Equal(-1, cut.Instance.Value);

        await cut.InvokeAsync(() => buttons[1].Click());
        Assert.True(dec);
        Assert.Equal(0, cut.Instance.Value);

        cut.Render(pb => pb.Add(a => a.Step, "10"));
        buttons = cut.FindAll("button");
        await cut.InvokeAsync(() => buttons[0].Click());
        Assert.Equal(-10, cut.Instance.Value);

        await cut.InvokeAsync(() => buttons[1].Click());
        Assert.Equal(0, cut.Instance.Value);
    }

    [Theory]
    [InlineData(typeof(short))]
    [InlineData(typeof(int))]
    [InlineData(typeof(long))]
    [InlineData(typeof(float))]
    [InlineData(typeof(double))]
    [InlineData(typeof(decimal))]
    public void Type_Ok(Type t)
    {
        var cut = Context.Render(builder =>
        {
            builder.OpenComponent(0, typeof(BootstrapInputNumber<>).MakeGenericType(t));
            builder.AddAttribute(1, "ShowButton", true);
            builder.AddAttribute(1, "Min", "-10");
            builder.AddAttribute(1, "Max", "10");
            builder.CloseComponent();
        });
        var buttons = cut.FindAll("button");
        cut.InvokeAsync(() => buttons[0].Click());
        cut.InvokeAsync(() => buttons[1].Click());
    }

    [Fact]
    public async Task Validate_Ok()
    {
        var model = new Foo() { Count = 1 };
        var cut = Context.Render<ValidateForm>(pb =>
        {
            pb.Add(a => a.Model, model);
            pb.AddChildContent<BootstrapInputNumber<int>>(builder =>
            {
                builder.Add(a => a.Value, model.Count);
                builder.Add(a => a.ValueChanged, EventCallback.Factory.Create<int>(this, v =>
                {
                    model.Count = v;
                }));
                builder.Add(a => a.ValueExpression, Utility.GenerateValueExpression(model, nameof(model.Count), typeof(int)));
            });
        });
        var input = cut.Find(".form-control");

        // 更改成非法数值 测试 CurrentValueAsString 赋值逻辑
        await cut.InvokeAsync(() =>
        {
            input.Change("t");
        });
        Assert.Equal(1, model.Count);

        var valid = await cut.InvokeAsync(cut.Instance.ValidateAsync);
        Assert.False(valid);

        await cut.InvokeAsync(() =>
        {
            input.Change("t2");
        });
        Assert.Equal(1, model.Count);
        valid = await cut.InvokeAsync(cut.Instance.ValidateAsync);
        Assert.False(valid);

        await cut.InvokeAsync(() =>
        {
            input.Change("2");
        });
        Assert.Equal(2, model.Count);

        valid = await cut.InvokeAsync(cut.Instance.ValidateAsync);
        Assert.True(valid);
    }

    [Fact]
    public async Task TryParseValueFromString_Ok()
    {
        var model = new Foo() { Count = 1 };
        var cut = Context.Render<BootstrapInputNumber<int>>(pb =>
        {
            pb.Add(a => a.Value, 1);
            pb.Add(a => a.ValueChanged, EventCallback.Factory.Create<int>(this, v =>
            {
                model.Count = v;
            }));
            pb.Add(a => a.ValueExpression, Utility.GenerateValueExpression(model, nameof(model.Count), typeof(int)));
        });
        var input = cut.Find(".form-control");

        // 更改成非法数值 测试 CurrentValueAsString 赋值逻辑
        await cut.InvokeAsync(() =>
        {
            input.Change("t");
        });
        Assert.Equal(1, model.Count);

        await cut.InvokeAsync(() =>
        {
            input.Change("t2");
        });
        Assert.Equal(1, model.Count);

        await cut.InvokeAsync(() =>
        {
            input.Change("2");
        });
        Assert.Equal(2, model.Count);
    }

    private class Cat
    {
        [Range(1, 10)]
        public int Count { get; set; }
    }

    private class MockInputNumber : BootstrapInputNumber<string>
    {
        public override Task SetParametersAsync(ParameterView parameters)
        {
            parameters.SetParameterProperties(this);

            OnInitialized();

            return Task.CompletedTask;
        }

        protected override string? InternalFormat(string? value)
        {
            return base.InternalFormat(value);
        }

        protected override void OnInitialized()
        {
            base.OnInitialized();

            InternalFormat("");
        }
    }
}
