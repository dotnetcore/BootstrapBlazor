// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace UnitTest.Components;

public class InputCurrencyTest : BootstrapBlazorTestBase
{
    [Fact]
    public async Task RenderAttributes_Ok()
    {
        var cut = Context.Render<BootstrapInputCurrency>(pb =>
        {
            pb.Add(a => a.Readonly, true);
            pb.Add(a => a.Color, Color.Danger);
            pb.Add(a => a.PlaceHolder, "Amount");
            pb.Add(a => a.AdditionalAttributes, new Dictionary<string, object>
            {
                ["data-test"] = "currency"
            });
        });

        var input = cut.Find("input");
        Assert.Equal("true", input.GetAttribute("readonly"));
        Assert.Equal("decimal", input.GetAttribute("inputmode"));
        Assert.Equal("Amount", input.GetAttribute("placeholder"));
        Assert.Contains("border-danger", input.ClassList);
        Assert.Equal("currency", cut.Find(".bb-input-currency").GetAttribute("data-test"));

        // 调用 focus 触发 GetInputId 方法
        await cut.InvokeAsync(() => cut.Instance.FocusAsync());
    }

    [Fact]
    public void FormatValue_Ok()
    {
        var culture = CultureInfo.GetCultureInfo("en-US");
        var cut = Context.Render<BootstrapInputCurrency>(pb =>
        {
            pb.Add(a => a.Value, 1234.5m);
            pb.Add(a => a.CultureName, culture.Name);
        });

        Assert.Equal("1,234.50", cut.Find("input").GetAttribute("value"));
        cut.Contains("<div class=\"input-group-text\">$</div>");

        cut.Render(pb =>
        {
            pb.Add(a => a.Value, 1234.5m);
            pb.Add(a => a.CultureName, culture.Name);
            pb.Add(a => a.Decimals, 3);
        });
        Assert.Equal("1,234.500", cut.Find("input").GetAttribute("value"));

        cut.Render(pb =>
        {
            pb.Add(a => a.Value, 1234.5m);
            pb.Add(a => a.CultureName, culture.Name);
            pb.Add(a => a.FormatString, "0.0");
        });
        Assert.Equal("1234.5", cut.Find("input").GetAttribute("value"));

        cut.Render(pb =>
        {
            pb.Add(a => a.Value, 1234.5m);
            pb.Add(a => a.CultureName, culture.Name);
            pb.Add(a => a.Formatter, value => $"{value + 1:0.00}");
        });
        Assert.Equal("1235.50", cut.Find("input").GetAttribute("value"));
    }

    [Theory]
    [InlineData("en-US", "USD")]
    [InlineData("zh-CN", "CNY")]
    [InlineData("en", "USD")]
    public void ShowIsoCurrencySymbol_Ok(string cultureName, string expected)
    {
        var cut = Context.Render<BootstrapInputCurrency>(pb =>
        {
            pb.Add(a => a.CultureName, cultureName);
            pb.Add(a => a.ShowIsoCurrencySymbol, true);
        });

        Assert.Equal(expected, cut.Find(".input-group-text").TextContent);
    }

    [Fact]
    public void SymbolTemplate_Ok()
    {
        var cut = Context.Render<BootstrapInputCurrency>(pb =>
        {
            pb.Add(a => a.SymbolTemplate, builder => builder.AddContent(0, "Custom"));
        });

        Assert.Equal("Custom", cut.Find(".input-group-text").TextContent);
    }

    [Fact]
    public async Task CultureParsing_Ok()
    {
        var value = 0m;
        var cut = Context.Render<BootstrapInputCurrency>(pb =>
        {
            pb.Add(a => a.Value, value);
            pb.Add(a => a.ValueChanged, EventCallback.Factory.Create<decimal>(this, v => value = v));
            pb.Add(a => a.CultureName, "de-DE");
        });

        await cut.InvokeAsync(() => cut.Find("input").Change("2.345,75"));

        Assert.Equal(2345.75m, value);
        Assert.Equal("2.345,75", cut.Find("input").GetAttribute("value"));
    }

    [Fact]
    public async Task MinMax_Ok()
    {
        var cut = Context.Render<BootstrapInputCurrency>(pb =>
        {
            pb.Add(a => a.Value, 5m);
            pb.Add(a => a.Min, 10m);
        });

        await cut.InvokeAsync(() => cut.Find("input").Blur());
        Assert.Equal(10m, cut.Instance.Value);

        cut.Render(pb =>
        {
            pb.Add(a => a.Value, 15m);
            pb.Add(a => a.Max, 10m);
        });
        await cut.InvokeAsync(() => cut.Find("input").Blur());
        Assert.Equal(10m, cut.Instance.Value);
    }

    [Fact]
    public async Task Clearable_Ok()
    {
        decimal? clearedValue = null;
        var changedValue = 12.34m;
        var cut = Context.Render<BootstrapInputCurrency>(pb =>
        {
            pb.Add(a => a.Value, changedValue);
            pb.Add(a => a.ValueChanged, EventCallback.Factory.Create<decimal>(this, v => changedValue = v));
            pb.Add(a => a.IsClearable, true);
            pb.Add(a => a.ClearIcon, "test-clear-icon");
            pb.Add(a => a.OnClear, value =>
            {
                clearedValue = value;
                return Task.CompletedTask;
            });
        });

        var icon = cut.Find(".form-control-clear-icon");
        Assert.Contains("test-clear-icon", icon.ClassList);
        await cut.InvokeAsync(() => icon.Click());
        Assert.Equal(12.34m, clearedValue);
        Assert.Equal(0m, changedValue);

        cut.Render(pb =>
        {
            pb.Add(a => a.IsClearable, true);
            pb.Add(a => a.Readonly, true);
        });
        Assert.Empty(cut.FindAll(".form-control-clear-icon"));

        cut.Render(pb =>
        {
            pb.Add(a => a.IsClearable, true);
            pb.Add(a => a.IsDisabled, true);
        });
        Assert.Empty(cut.FindAll(".form-control-clear-icon"));
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(29)]
    public void DecimalsOutOfRange_Throws(int decimals)
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => Context.Render<BootstrapInputCurrency>(pb =>
        {
            pb.Add(a => a.Decimals, decimals);
        }));
    }

    [Fact]
    public async Task OnBlurAsync_Ok()
    {
        decimal? value = null;
        var cut = Context.Render<BootstrapInputCurrency>(pb =>
        {
            pb.Add(a => a.Value, 10m);
            pb.Add(a => a.OnBlurAsync, v =>
            {
                value = v;
                return Task.CompletedTask;
            });
        });

        await cut.InvokeAsync(() => cut.Find("input").Blur());
        Assert.Equal(10m, value);
    }

    [Fact]
    public async Task ValidateForm_Ok()
    {
        var model = new CurrencyModel { Amount = 10m };
        var cut = Context.Render<ValidateForm>(pb =>
        {
            pb.Add(a => a.Model, model);
            pb.AddChildContent<BootstrapInputCurrency>(builder =>
            {
                builder.Add(a => a.Value, model.Amount);
                builder.Add(a => a.ValueChanged, EventCallback.Factory.Create<decimal>(this, v => model.Amount = v));
                builder.Add(a => a.ValueExpression, Utility.GenerateValueExpression(model, nameof(CurrencyModel.Amount), typeof(decimal)));
            });
        });

        await cut.InvokeAsync(() => cut.Find("input").Change("invalid"));
        Assert.Equal(10m, model.Amount);
        Assert.False(await cut.InvokeAsync(() => cut.Instance.ValidateAsync(CancellationToken.None)));

        await cut.InvokeAsync(() => cut.Find("input").Change("5"));
        Assert.Equal(5m, model.Amount);
        Assert.True(await cut.InvokeAsync(() => cut.Instance.ValidateAsync(CancellationToken.None)));
    }

    private class CurrencyModel
    {
        [Range(1, 10)]
        public decimal Amount { get; set; }
    }
}
