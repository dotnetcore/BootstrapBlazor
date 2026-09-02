// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Reflection;

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
    [InlineData(typeof(sbyte), null)]
    [InlineData(typeof(byte), null)]
    [InlineData(typeof(short), null)]
    [InlineData(typeof(ushort), null)]
    [InlineData(typeof(int), null)]
    [InlineData(typeof(uint), null)]
    [InlineData(typeof(long), null)]
    [InlineData(typeof(ulong), null)]
    [InlineData(typeof(float), null)]
    [InlineData(typeof(double), null)]
    [InlineData(typeof(decimal), null)]
    [InlineData(typeof(int), "any")]
    [InlineData(typeof(float), "any")]
    public async Task Type_Ok(Type t, string? step)
    {
        var cut = Context.Render(builder =>
        {
            builder.OpenComponent(0, typeof(BootstrapInputNumber<>).MakeGenericType(t));
            builder.AddAttribute(1, "ShowButton", true);
            builder.AddAttribute(2, "Max", "10");
            builder.AddAttribute(3, "Step", step);
            builder.CloseComponent();
        });
        var buttons = cut.FindAll("button");
        await cut.InvokeAsync(() => buttons[0].Click());
        await cut.InvokeAsync(() => buttons[1].Click());
    }

    [Theory]
    [InlineData(typeof(sbyte), (object)(sbyte)1, "2", (object)(sbyte)-1, (object)(sbyte)1)]
    [InlineData(typeof(byte), (object)(byte)1, "2", (object)(byte)0, (object)(byte)2)]
    [InlineData(typeof(short), (object)(short)1, "2", (object)(short)-1, (object)(short)1)]
    [InlineData(typeof(ushort), (object)(ushort)1, "2", (object)(ushort)0, (object)(ushort)2)]
    [InlineData(typeof(int), 1, "2", -1, 1)]
    [InlineData(typeof(uint), (object)(uint)1, "2", (object)(uint)0, (object)(uint)2)]
    [InlineData(typeof(long), (object)1L, "2", (object)(-1L), (object)1L)]
    [InlineData(typeof(ulong), (object)1UL, "2", (object)0UL, (object)2UL)]
    public async Task ShowButton_IntegralTypeStep_Ok(Type t, object value, string step, object decrementExpected, object incrementExpected)
    {
        var cut = Context.Render(builder =>
        {
            builder.OpenComponent(0, typeof(BootstrapInputNumber<>).MakeGenericType(t));
            builder.AddAttribute(1, "ShowButton", true);
            builder.AddAttribute(2, "Value", value);
            builder.AddAttribute(3, "Step", step);
            builder.CloseComponent();
        });

        var buttons = cut.FindAll("button");
        await cut.InvokeAsync(() => buttons[0].Click());
        Assert.Equal(Convert.ToString(decrementExpected, CultureInfo.InvariantCulture), cut.Find("input").GetAttribute("value"));

        await cut.InvokeAsync(() => buttons[1].Click());
        Assert.Equal(Convert.ToString(incrementExpected, CultureInfo.InvariantCulture), cut.Find("input").GetAttribute("value"));
    }

    [Theory]
    [InlineData(typeof(sbyte), (object)sbyte.MaxValue, "2", (object)sbyte.MaxValue)]
    [InlineData(typeof(byte), (object)byte.MaxValue, "2", (object)byte.MaxValue)]
    [InlineData(typeof(short), (object)short.MaxValue, "2", (object)short.MaxValue)]
    [InlineData(typeof(ushort), (object)ushort.MaxValue, "2", (object)ushort.MaxValue)]
    [InlineData(typeof(int), int.MaxValue, "2", int.MaxValue)]
    [InlineData(typeof(uint), (object)uint.MaxValue, "2", (object)uint.MaxValue)]
    [InlineData(typeof(long), (object)long.MaxValue, "2", (object)long.MaxValue)]
    [InlineData(typeof(ulong), (object)ulong.MaxValue, "2", (object)ulong.MaxValue)]
    public async Task ShowButton_IntegralTypeMaxClamp_Ok(Type t, object value, string step, object expected)
    {
        var cut = Context.Render(builder =>
        {
            builder.OpenComponent(0, typeof(BootstrapInputNumber<>).MakeGenericType(t));
            builder.AddAttribute(1, "ShowButton", true);
            builder.AddAttribute(2, "Value", value);
            builder.AddAttribute(3, "Step", step);
            builder.CloseComponent();
        });

        var buttons = cut.FindAll("button");
        await cut.InvokeAsync(() => buttons[1].Click());
        Assert.Equal(Convert.ToString(expected, CultureInfo.InvariantCulture), cut.Find("input").GetAttribute("value"));
    }

    [Fact]
    public async Task ShowButton_NullableValue_Ok()
    {
        var increment = false;
        var decrement = false;
        var cut = Context.Render<BootstrapInputNumber<int?>>(pb =>
        {
            pb.Add(a => a.ShowButton, true);
            pb.Add(a => a.Value, null);
            pb.Add(a => a.OnIncrement, v =>
            {
                increment = true;
                return Task.CompletedTask;
            });
            pb.Add(a => a.OnDecrement, v =>
            {
                decrement = true;
                return Task.CompletedTask;
            });
        });

        var buttons = cut.FindAll("button");
        await cut.InvokeAsync(() => buttons[0].Click());
        Assert.True(decrement);
        Assert.Null(cut.Instance.Value);

        await cut.InvokeAsync(() => buttons[1].Click());
        Assert.True(increment);
        Assert.Null(cut.Instance.Value);
    }

    [Fact]
    public async Task ShowButton_EmptyStep_Ok()
    {
        var cut = Context.Render<BootstrapInputNumber<int>>(pb =>
        {
            pb.Add(a => a.ShowButton, true);
            pb.Add(a => a.Value, 1);
            pb.Add(a => a.Step, string.Empty);
        });

        var buttons = cut.FindAll("button");
        await cut.InvokeAsync(() => buttons[1].Click());
        Assert.Equal(2, cut.Instance.Value);
    }

    [Fact]
    public async Task MinMax_Ok()
    {
        var cut = Context.Render<BootstrapInputNumber<int>>(pb =>
        {
            pb.Add(a => a.Value, 5);
            pb.Add(a => a.Min, "10");
        });

        var input = cut.Find("input");
        await cut.InvokeAsync(() => input.Blur());
        Assert.Equal(10, cut.Instance.Value);

        cut.Render(pb =>
        {
            pb.Add(a => a.Value, 15);
            pb.Add(a => a.Min, null);
            pb.Add(a => a.Max, "10");
        });
        input = cut.Find("input");
        await cut.InvokeAsync(() => input.Blur());
        Assert.Equal(10, cut.Instance.Value);

        cut.Render(pb =>
        {
            pb.Add(a => a.Value, 5);
            pb.Add(a => a.Min, "0");
            pb.Add(a => a.Max, "10");
        });
        input = cut.Find("input");
        await cut.InvokeAsync(() => input.Blur());
        Assert.Equal(5, cut.Instance.Value);
    }

    [Fact]
    public void PrivateFallback_Ok()
    {
        var calculateMethod = typeof(BootstrapInputNumber<string>).GetMethod("Calculate", BindingFlags.NonPublic | BindingFlags.Static);
        Assert.NotNull(calculateMethod);

        var value = calculateMethod.Invoke(null, ["test", "1", true]);
        Assert.Equal("test", value);

        value = calculateMethod.Invoke(null, [null, "1", true]);
        Assert.Null(value);

        var cut = Context.Render<BootstrapInputNumber<int>>(pb => { pb.Add(a => a.Min, "test"); });
        var setMinMethod = typeof(BootstrapInputNumber<int>).GetMethod("SetMin", BindingFlags.NonPublic | BindingFlags.Instance);
        Assert.NotNull(setMinMethod);

        var ex = Assert.Throws<TargetInvocationException>(() => setMinMethod.Invoke(cut.Instance, [1]));
        Assert.IsType<InvalidOperationException>(ex.InnerException);
    }

    [Fact]
    public void UnsignedType_Bind_Ok()
    {
        AssertInputNumberValueChanged<byte>(1, "2", 2);
        AssertInputNumberValueChanged<ushort>(1, "2", 2);
        AssertInputNumberValueChanged<uint>(1, "2", 2);
        AssertInputNumberValueChanged<ulong>(1, "2", 2);
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

    private void AssertInputNumberValueChanged<TValue>(TValue value, string inputValue, TValue expected)
    {
        var currentValue = value;
        var cut = Context.Render<BootstrapInputNumber<TValue>>(pb =>
        {
            pb.Add(a => a.Value, value);
            pb.Add(a => a.ValueChanged, EventCallback.Factory.Create<TValue?>(this, v => currentValue = v));
        });

        var input = cut.Find(".form-control");
        cut.InvokeAsync(() => input.Change(inputValue));
        Assert.Equal(expected, currentValue);
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
