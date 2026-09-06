// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Reflection;

namespace UnitTest.Components;

public class InputCurrencyTest : BootstrapBlazorTestBase
{
    [Theory]
    [InlineData(null)]
    [InlineData(0.0)]
    public async Task OnInput_Ok(double? v)
    {
        double? value = 0.0;
        var cut = Context.Render<BootstrapInputCurrency<double?>>(builder =>
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
    public void RenderAttributes_Ok()
    {
        var integer = Context.Render<BootstrapInputCurrency<int>>(pb =>
        {
            pb.Add(a => a.Readonly, true);
            pb.Add(a => a.Color, Color.Danger);
            pb.Add(a => a.AdditionalAttributes, new Dictionary<string, object>
            {
                ["data-test"] = "currency"
            });
        });
        var input = integer.Find("input");
        Assert.Equal("true", input.GetAttribute("readonly"));
        Assert.Equal("numeric", input.GetAttribute("inputmode"));
        Assert.Equal("currency", input.GetAttribute("data-test"));
        Assert.Contains("border-danger", input.ClassList);

        var decimalInput = Context.Render<BootstrapInputCurrency<decimal>>();
        Assert.Equal("decimal", decimalInput.Find("input").GetAttribute("inputmode"));
        Assert.Null(decimalInput.Find("input").GetAttribute("readonly"));
    }

    [Fact]
    public void OnBlur_Ok()
    {
        var cut = Context.Render<BootstrapInputCurrency<int>>(pb =>
        {
            pb.Add(a => a.Min, "0");
            pb.Add(a => a.Max, "10");
        });
        cut.Contains("min=\"0\"");
        cut.Contains("max=\"10\"");

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
            pb.AddChildContent<BootstrapInputCurrency<int>>(pb =>
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
        Assert.ThrowsAny<InvalidOperationException>(() => Context.Render<BootstrapInputCurrency<string>>());
    }

    [Fact]
    public void Formatter_Ok()
    {
        var cut = Context.Render<BootstrapInputCurrency<decimal>>(pb =>
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
    public async Task CultureFormattingAndParsing_Ok()
    {
        var culture = CultureInfo.GetCultureInfo("de-DE");
        var value = 1234.5m;
        var cut = Context.Render<BootstrapInputCurrency<decimal>>(pb =>
        {
            pb.Add(a => a.Value, value);
            pb.Add(a => a.ValueChanged, EventCallback.Factory.Create<decimal>(this, v => value = v));
            pb.Add(a => a.CultureInfo, culture);
            pb.Add(a => a.FormatString, "C2");
        });
        var input = cut.Find("input");
        Assert.Equal(value.ToString("C2", culture), input.GetAttribute("value"));

        await cut.InvokeAsync(() => input.Change("2.345,75 €"));

        Assert.Equal(2345.75m, value);
        Assert.Equal(value.ToString("C2", culture), cut.Find("input").GetAttribute("value"));
    }

    [Theory]
    [InlineData("12345abc", 12345)]
    [InlineData("-1,234.50 USD", -1234.50)]
    [InlineData("1.2.3", 1.23)]
    [InlineData("(1,234.50)", -1234.50)]
    [InlineData("1e+3", 1000)]
    [InlineData("1e-3", 0.001)]
    [InlineData("1e", 1)]
    [InlineData("e1", 1)]
    public async Task FormattedInput_Normalizes_Ok(string source, double expected)
    {
        var value = 0d;
        var culture = CultureInfo.GetCultureInfo("en-US");
        var cut = Context.Render<BootstrapInputCurrency<double>>(pb =>
        {
            pb.Add(a => a.Value, value);
            pb.Add(a => a.ValueChanged, EventCallback.Factory.Create<double>(this, v => value = v));
            pb.Add(a => a.CultureInfo, culture);
            pb.Add(a => a.FormatString, "0.##");
        });

        await cut.InvokeAsync(() => cut.Find("input").Change(source));

        Assert.Equal(expected, value);
        Assert.Equal(expected.ToString("0.##", culture), cut.Find("input").GetAttribute("value"));
    }

    [Fact]
    public async Task FormattedInteger_Normalizes_Ok()
    {
        var value = 0;
        var cut = Context.Render<BootstrapInputCurrency<int>>(pb =>
        {
            pb.Add(a => a.Value, value);
            pb.Add(a => a.ValueChanged, EventCallback.Factory.Create<int>(this, v => value = v));
            pb.Add(a => a.FormatString, "N0");
            pb.Add(a => a.CultureInfo, CultureInfo.InvariantCulture);
        });

        await cut.InvokeAsync(() => cut.Find("input").Change("12abc"));

        Assert.Equal(12, value);
    }

    [Fact]
    public async Task Parser_SuccessFailureAndCulture_Ok()
    {
        var value = 1;
        var culture = CultureInfo.GetCultureInfo("fr-FR");
        CultureInfo? receivedCulture = null;
        var shouldSucceed = true;
        var cut = Context.Render<BootstrapInputCurrency<int>>(pb =>
        {
            pb.Add(a => a.Value, value);
            pb.Add(a => a.ValueChanged, EventCallback.Factory.Create<int>(this, v => value = v));
            pb.Add(a => a.CultureInfo, culture);
            pb.Add(a => a.Parser, (text, currentCulture) =>
            {
                receivedCulture = currentCulture;
                return (shouldSucceed, shouldSucceed ? text.Length : default);
            });
        });

        await cut.InvokeAsync(() => cut.Find("input").Change("custom"));
        Assert.Equal(6, value);
        Assert.Same(culture, receivedCulture);

        shouldSucceed = false;
        await cut.InvokeAsync(() => cut.Find("input").Change("123"));
        Assert.Equal(6, value);
    }

    [Fact]
    public async Task UseInputEvent_FormatterPreservesInput_Ok()
    {
        var value = 0m;
        var cut = Context.Render<BootstrapInputCurrency<decimal>>(pb =>
        {
            pb.Add(a => a.Value, value);
            pb.Add(a => a.ValueChanged, EventCallback.Factory.Create<decimal>(this, v => value = v));
            pb.Add(a => a.UseInputEvent, true);
            pb.Add(a => a.FormatString, "N2");
            pb.Add(a => a.CultureInfo, CultureInfo.GetCultureInfo("en-US"));
        });

        await cut.InvokeAsync(() => cut.Find("input").Input("$1,234.50abc"));

        Assert.Equal(1234.50m, value);
        Assert.Equal("$1,234.50abc", cut.Find("input").GetAttribute("value"));
    }

    [Fact]
    public void Formatter_Null()
    {
        var cut = Context.Render<BootstrapInputCurrency<int?>>(pb =>
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
        var cut = Context.Render<BootstrapInputCurrency<int?>>(pb =>
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
        var cut = Context.Render<BootstrapInputCurrency<int>>(builder =>
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
    public async Task OnValueChanged_Ok()
    {
        int? changedValue = null;
        var cut = Context.Render<BootstrapInputCurrency<int>>(pb =>
        {
            pb.Add(a => a.OnValueChanged, value =>
            {
                changedValue = value;
                return Task.CompletedTask;
            });
        });

        await cut.InvokeAsync(() => cut.Find("input").Change("12"));

        Assert.Equal(12, changedValue);
    }

    [Fact]
    public async Task MinMax_Ok()
    {
        var cut = Context.Render<BootstrapInputCurrency<int>>(pb =>
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
    public async Task InvalidMin_Throws_Ok()
    {
        var cut = Context.Render<BootstrapInputCurrency<int>>(pb =>
        {
            pb.Add(a => a.Value, 1);
            pb.Add(a => a.Min, "invalid");
        });

        await Assert.ThrowsAsync<InvalidOperationException>(() => cut.InvokeAsync(() => cut.Find("input").Blur()));
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
            pb.AddChildContent<BootstrapInputCurrency<int>>(builder =>
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

        var valid = await cut.InvokeAsync(() => cut.Instance.ValidateAsync(CancellationToken.None));
        Assert.False(valid);

        await cut.InvokeAsync(() =>
        {
            input.Change("t2");
        });
        Assert.Equal(1, model.Count);
        valid = await cut.InvokeAsync(() => cut.Instance.ValidateAsync(CancellationToken.None));
        Assert.False(valid);

        await cut.InvokeAsync(() =>
        {
            input.Change("2");
        });
        Assert.Equal(2, model.Count);

        valid = await cut.InvokeAsync(() => cut.Instance.ValidateAsync(CancellationToken.None));
        Assert.True(valid);
    }

    [Fact]
    public async Task TryParseValueFromString_Ok()
    {
        var model = new Foo() { Count = 1 };
        var cut = Context.Render<BootstrapInputCurrency<int>>(pb =>
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
        var cut = Context.Render<BootstrapInputCurrency<TValue>>(pb =>
        {
            pb.Add(a => a.Value, value);
            pb.Add(a => a.ValueChanged, EventCallback.Factory.Create<TValue?>(this, v => currentValue = v));
        });

        var input = cut.Find(".form-control");
        cut.InvokeAsync(() => input.Change(inputValue));
        Assert.Equal(expected, currentValue);
    }

    private class MockInputNumber : BootstrapInputCurrency<string>
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
