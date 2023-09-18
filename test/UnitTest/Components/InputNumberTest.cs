// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using System.ComponentModel.DataAnnotations;

namespace UnitTest.Components;

public class InputNumberTest : BootstrapBlazorTestBase
{
    [Fact]
    public void OnBlur_Ok()
    {
        var cut = Context.RenderComponent<BootstrapInputNumber<int>>(pb =>
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
        var cut = Context.RenderComponent<ValidateForm>(pb =>
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
        Assert.ThrowsAny<InvalidOperationException>(() => Context.RenderComponent<BootstrapInputNumber<string>>());
    }

    [Fact]
    public void Formatter_Ok()
    {
        var cut = Context.RenderComponent<BootstrapInputNumber<decimal>>(pb =>
        {
            pb.Add(a => a.Value, 10.01m);
            pb.Add(a => a.Formatter, v => $"{v + 1}");
        });
        var input = cut.Find("input");
        Assert.Equal("11.01", input.GetAttribute("value"));

        cut.SetParametersAndRender(pb =>
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
        var cut = Context.RenderComponent<BootstrapInputNumber<int?>>(pb =>
        {
            pb.Add(a => a.FormatString, "d2");
        });
        cut.Contains("value=\"\"");
    }

    [Fact]
    public void Formatter_Error()
    {
        Assert.ThrowsAny<InvalidOperationException>(() => Context.RenderComponent<MockInputNumber>());
    }

    [Fact]
    public void ShowButton_Ok()
    {
        var inc = false;
        var dec = false;
        var cut = Context.RenderComponent<BootstrapInputNumber<int>>(pb =>
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
        cut.InvokeAsync(() => buttons[0].Click());
        Assert.True(inc);
        Assert.Equal(-1, cut.Instance.Value);

        cut.InvokeAsync(() => buttons[1].Click());
        Assert.True(dec);
        Assert.Equal(0, cut.Instance.Value);

        cut.SetParametersAndRender(pb => pb.Add(a => a.Step, "10"));
        buttons = cut.FindAll("button");
        cut.InvokeAsync(() => buttons[0].Click());
        Assert.Equal(-10, cut.Instance.Value);

        cut.InvokeAsync(() => buttons[1].Click());
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

        protected override string? InternalFormat(string value)
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
