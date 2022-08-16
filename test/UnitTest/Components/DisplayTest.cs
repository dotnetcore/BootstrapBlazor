// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Shared;
using System.Reflection;
using System.Web;

namespace UnitTest.Components;

public class DisplayTest : BootstrapBlazorTestBase
{
    [Fact]
    public void FormatterAsync_Ok()
    {
        var cut = Context.RenderComponent<Display<string>>(pb =>
        {
            pb.Add(a => a.FormatterAsync, new Func<string, Task<string>>(v =>
            {
                return Task.FromResult("FormattedValue");
            }));
        });
        Assert.Contains("FormattedValue", cut.Markup);
    }

    [Fact]
    public void EnumValue_Ok()
    {
        var cut = Context.RenderComponent<Display<EnumEducation>>(pb =>
        {
            pb.Add(a => a.Value, EnumEducation.Primary);
        });
        Assert.Contains("小学", HttpUtility.HtmlDecode(cut.Markup));
    }

    [Fact]
    public void ArrayValue_Ok()
    {
        var cut = Context.RenderComponent<Display<byte[]>>(pb =>
        {
            pb.Add(a => a.Value, new byte[] { 0x01, 0x12, 0x34, 0x56 });
        });
        Assert.Contains("1,18,52,86", cut.Markup);
    }

    [Fact]
    public void LookupService_Ok()
    {
        var cut = Context.RenderComponent<Display<List<string>>>(pb =>
        {
            pb.Add(a => a.LookupServiceKey, "FooLookup");
            pb.Add(a => a.Value, new List<string> { "v1", "v2" });
        });
        Assert.Contains("LookupService-Test-1,LookupService-Test-2", cut.Markup);
    }

    [Fact]
    public void TypeResolver_Ok()
    {
        var cut = Context.RenderComponent<Display<DisplayTest.Foo[]>>(pb =>
        {
            pb.Add(a => a.Value, new DisplayTest.Foo[] { new DisplayTest.Foo() { Value = "1" } });
            pb.Add(a => a.TypeResolver, new Func<Assembly, string, bool, Type>((assembly, typeName, ignoreCase) => typeof(DisplayTest.Foo)));
        });
        Assert.Equal("<div class=\"form-control is-display\">1</div>", cut.Markup);
    }

    [Fact]
    public void TypeResolver_Null()
    {
        var cut = Context.RenderComponent<Display<DisplayTest.Foo[]>>(pb =>
        {
            pb.Add(a => a.Value, new DisplayTest.Foo[] { new DisplayTest.Foo() { Value = "1" } });
        });
        Assert.Equal("<div class=\"form-control is-display\"></div>", cut.Markup);
    }

    [Fact]
    public void EnumerableNullValue_Ok()
    {
        var cut = Context.RenderComponent<Display<List<int?>>>(pb =>
        {
            pb.Add(a => a.Value, new List<int?> { 1, 2, 3, 4, null });
            pb.Add(a => a.Lookup, new List<SelectedItem>()
            {
                new("", "Test"),
                new("1", "Test 1")
            });
        });

        // 给定值中有空值，Lookup 中对空值转化为 Test
        Assert.Equal("<div class=\"form-control is-display\">Test 1,Test</div>", cut.Markup);
    }

    [Fact]
    public void EnumerableValue_Ok()
    {
        var cut = Context.RenderComponent<Display<List<int>>>(pb =>
        {
            pb.Add(a => a.Value, new List<int> { 1, 2, 3, 4 });
        });
        Assert.Contains("1,2,3,4", cut.Markup);

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.Lookup, new List<SelectedItem>()
            {
                new("1", "Test 1")
            });
        });
        Assert.Contains("Test 1", cut.Markup);
    }

    [Fact]
    public void GenericValue_Ok()
    {
        var cut = Context.RenderComponent<Display<DisplayGenericValueMock<string>>>(pb =>
        {
            pb.Add(a => a.Value, new DisplayGenericValueMock<string>() { Value = "1" });
            pb.Add(a => a.Lookup, new List<SelectedItem>()
            {
                new("1", "Test 1")
            });
        });
        Assert.Contains("Test 1", cut.Markup);
    }

    [Fact]
    public void StringValue_Ok()
    {
        var cut = Context.RenderComponent<Display<string>>(pb =>
        {
            pb.Add(a => a.Value, "Test 1");
        });
        Assert.Contains("Test 1", cut.Markup);
    }

    [Fact]
    public void DateTimeValue_Ok()
    {
        var cut = Context.RenderComponent<Display<DateTime>>(pb =>
        {
            pb.Add(a => a.Value, DateTime.Now);
            pb.Add(a => a.FormatString, "yyyy-MM-dd");
        });
        Assert.Contains($"{DateTime.Now:yyyy-MM-dd}", cut.Markup);
    }

    [Fact]
    public void NullValue_Ok()
    {
        var cut = Context.RenderComponent<Display<string?>>(pb =>
        {
            pb.Add(a => a.Value, null);
            pb.Add(a => a.Lookup, new List<SelectedItem>()
            {
                new("1", "Test 1")
            });
        });
        Assert.Equal("<div class=\"form-control is-display\"></div>", cut.Markup);
    }

    [Fact]
    public void ShowLabel_Ok()
    {
        var cut = Context.RenderComponent<Display<string>>(pb =>
        {
            pb.Add(a => a.ShowLabel, true);
            pb.Add(a => a.DisplayText, "Test Label");
        });
        Assert.Contains("Test Label", cut.Markup);
    }

    [Fact]
    public void Bind_Ok()
    {
        var foo = new BootstrapBlazor.Shared.Foo();
        var cut = Context.RenderComponent<ValidateForm>(pb =>
        {
            pb.Add(a => a.Model, foo);
            pb.AddChildContent<BootstrapInputGroup>(pb =>
            {
                pb.Add(a => a.ChildContent, builder =>
                {
                    builder.OpenComponent<Display<string>>(0);
                    builder.AddAttribute(1, "Value", foo.Name);
                    builder.AddAttribute(2, "ValueExpression", Utility.GenerateValueExpression(foo, "Name", typeof(string)));
                    builder.CloseComponent();

                    builder.OpenComponent<BootstrapInputGroupLabel>(3);
                    builder.AddAttribute(4, "Value", "Name");
                    builder.AddAttribute(5, "ValueExpression", Utility.GenerateValueExpression(foo, "Name", typeof(string)));
                    builder.CloseComponent();
                });
            });
        });
        Assert.Contains("is-display", cut.Markup);
        Assert.Contains("input-group-text", cut.Markup);
        Assert.Contains("<span>&#x59D3;&#x540D;</span>", cut.Markup);
    }

    [Fact]
    public void Nullable_Enum()
    {
        var model = new BootstrapBlazor.Shared.Foo() { Education = EnumEducation.Middle };
        var cut = Context.RenderComponent<Display<EnumEducation?>>(pb =>
        {
            pb.Add(a => a.ShowLabel, true);
            pb.Add(a => a.Value, model.Education);
            pb.Add(a => a.ValueExpression, Utility.GenerateValueExpression(model, "Education", typeof(Nullable<EnumEducation>)));
        });

        // 获得中学 DisplayName
        Assert.Contains("&#x5B66;&#x5386;", cut.Markup);
    }

    class DisplayGenericValueMock<T>
    {
        [NotNull]
        public T? Value { get; set; }

        public override string? ToString()
        {
            return Value.ToString();
        }
    }

    class Foo
    {
        public string Value { get; set; } = "";

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}
