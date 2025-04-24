// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using System.Web;

namespace UnitTest.Components;

public class DisplayTest : BootstrapBlazorTestBase
{
    [Fact]
    public void FormatterAsync_Ok()
    {
        var cut = Context.RenderComponent<Display<string>>(pb =>
        {
            pb.Add(a => a.FormatterAsync, v => Task.FromResult<string?>("FormattedValue"));
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
            pb.Add(a => a.Value, [0x01, 0x12, 0x34, 0x56]);
        });
        Assert.Contains("1,18,52,86", cut.Markup);
    }

    [Fact]
    public async Task LookupService_Ok()
    {
        var cut = Context.RenderComponent<Display<List<string>>>(pb =>
        {
            pb.Add(a => a.LookupService, null);
            pb.Add(a => a.LookupServiceKey, "FooLookup");
            pb.Add(a => a.LookupServiceData, true);
            pb.Add(a => a.LookupStringComparison, StringComparison.OrdinalIgnoreCase);
            pb.Add(a => a.Value, ["v1", "v2"]);
        });
        Assert.Contains("LookupService-Test-1,LookupService-Test-2", cut.Markup);

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.LookupService, new MockLookupService());
        });
        await Task.Delay(20);
        Assert.Contains("Test1,Test2", cut.Markup);

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.LookupServiceKey, null);
            pb.Add(a => a.Lookup, new List<SelectedItem> { new("v1", "Test3"), new("v2", "Test4") });
        });
        await Task.Delay(20);
        Assert.Contains("Test3,Test4", cut.Markup);
    }

    [Fact]
    public void TypeResolver_Ok()
    {
        var cut = Context.RenderComponent<Display<Fish[]>>(pb =>
        {
            pb.Add(a => a.Value, [new Fish { Value = "1" }]);
            pb.Add(a => a.TypeResolver, (_, _, _) => typeof(Fish));
        });
        Assert.Equal("<div class=\"form-control is-display\">1</div>", cut.Markup);
    }

    [Fact]
    public void TypeResolver_Null()
    {
        var cut = Context.RenderComponent<Display<Fish[]>>(pb =>
        {
            pb.Add(a => a.Value, [new Fish { Value = "1" }]);
        });
        Assert.Equal("<div class=\"form-control is-display\"></div>", cut.Markup);
    }

    [Fact]
    public void EnumerableNullValue_Ok()
    {
        var cut = Context.RenderComponent<Display<List<int?>>>(pb =>
        {
            pb.Add(a => a.Value, [1, 2, 3, 4, null]);
            pb.Add(a => a.Lookup,
            [
                new SelectedItem("", "Test"),
                new SelectedItem("1", "Test 1")
            ]);
        });

        // 给定值中有空值，Lookup 中对空值转化为 Test
        Assert.Equal("<div class=\"form-control is-display\">Test 1,Test</div>", cut.Markup);
    }

    [Fact]
    public void EnumerableValue_Ok()
    {
        var cut = Context.RenderComponent<Display<List<int>>>(pb =>
        {
            pb.Add(a => a.Value, [1, 2, 3, 4]);
        });
        Assert.Contains("1,2,3,4", cut.Markup);

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.Lookup, new List<SelectedItem>() { new("1", "Test 1") });
        });
        Assert.Contains("Test 1", cut.Markup);
    }

    [Fact]
    public void GenericValue_Ok()
    {
        var cut = Context.RenderComponent<Display<DisplayGenericValueMock<string>>>(pb =>
        {
            pb.Add(a => a.Value, new DisplayGenericValueMock<string>() { Value = "1" });
            pb.Add(a => a.Lookup, new List<SelectedItem>() { new("1", "Test 1") });
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
            pb.Add(a => a.Lookup, new List<SelectedItem>() { new("1", "Test 1") });
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
    public void ShowTooltip_Ok()
    {
        var cut = Context.RenderComponent<Display<string>>(pb =>
        {
            pb.Add(a => a.ShowTooltip, true);
            pb.Add(a => a.Value, "Test Label");
        });
        Assert.Contains("data-bs-original-title=\"Test Label\"", cut.Markup);
    }

    [Fact]
    public void Bind_Ok()
    {
        var foo = new Foo() { Name = "test-name" };
        var cut = Context.RenderComponent<ValidateForm>(pb =>
        {
            pb.Add(a => a.Model, foo);
            pb.AddChildContent<BootstrapInputGroup>(b =>
            {
                b.Add(a => a.ChildContent, builder =>
                {
                    builder.OpenComponent<Display<string>>(0);
                    builder.AddAttribute(1, "Value", foo.Name);
                    builder.AddAttribute(2, "ValueExpression", Utility.GenerateValueExpression(foo, "Name", typeof(string)));
                    builder.CloseComponent();

                    builder.OpenComponent<BootstrapInputGroupLabel>(3);
                    builder.AddAttribute(4, "Value", "Name");
                    builder.AddAttribute(5, "ValueExpression", Utility.GenerateValueExpression(foo, "Name", typeof(string)));
                    builder.CloseComponent();

                    builder.OpenComponent<BootstrapInputGroupLabel>(3);
                    builder.AddAttribute(6, "Value", "Name");
                    builder.CloseComponent();
                });
            });
        });
        Assert.Contains("<div class=\"input-group\"><div class=\"form-control is-display\">test-name</div><div class=\"input-group-text\"><span>姓名</span></div><div class=\"input-group-text\"><span></span></div></div>", cut.Markup);
    }

    [Fact]
    public void Nullable_Enum()
    {
        var model = new Foo() { Education = EnumEducation.Middle };
        var cut = Context.RenderComponent<Display<EnumEducation?>>(pb =>
        {
            pb.Add(a => a.ShowLabel, true);
            pb.Add(a => a.Value, model.Education);
            pb.Add(a => a.ValueExpression, Utility.GenerateValueExpression(model, "Education", typeof(EnumEducation?)));
        });

        // 获得中学 DisplayName
        Assert.Contains("中学", cut.Markup);
    }

    [Fact]
    public void Format_Test()
    {
        var cut = Context.RenderComponent<MockDisplayComponent>();
        var result = cut.Instance.Test(new SelectedItem("1", "Test"));
        Assert.Equal("1", result);
    }

    class DisplayGenericValueMock<T>
    {
        [NotNull]
        public T? Value { get; init; }

        public override string? ToString()
        {
            return Value.ToString();
        }
    }

    class Fish
    {
        public string Value { get; init; } = "";

        public override string ToString()
        {
            return Value;
        }
    }

    internal class MockDisplayComponent : DisplayBase<SelectedItem>
    {
        public string? Test(SelectedItem v)
        {
            return base.FormatValueAsString(v);
        }
    }

    class MockLookupService : LookupServiceBase
    {
        public override IEnumerable<SelectedItem>? GetItemsByKey(string? key, object? data) => null;

        public override async Task<IEnumerable<SelectedItem>?> GetItemsByKeyAsync(string? key, object? data)
        {
            await Task.Delay(10);

            return [new SelectedItem("v1", "Test1"), new SelectedItem("v2", "Test2")];
        }
    }
}
