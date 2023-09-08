// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Shared;

namespace UnitTest.Components;

public class TableFooterCellTest : BootstrapBlazorTestBase
{
    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void IsMobileMode_Ok(bool mobile)
    {
        var ds = new List<Foo>()
        {
            new Foo() { Count = 1 },
            new Foo() { Count = 2 },
        };

        var cut = Context.RenderComponent<TableFooterCell>(pb =>
        {
            pb.AddCascadingValue<bool>("IsMobileMode", mobile);
            pb.AddCascadingValue<object>("TableFooterContext", ds);
            pb.Add(a => a.Field, nameof(Foo.Count));
        });

        cut.Contains("table-cell");
        cut.Contains("3");

        if (!mobile)
        {
            cut.Contains("td");
        }
        else
        {
            cut.DoesNotContain("td");
        }
    }

    [Fact]
    public void Text_Ok()
    {
        var ds = new List<Foo>()
        {
            new Foo() { Count = 1 },
            new Foo() { Count = 2 },
        };

        var cut = Context.RenderComponent<TableFooterCell>(pb =>
        {
            pb.AddCascadingValue<object>("TableFooterContext", ds);
            pb.Add(a => a.Field, nameof(Foo.Count));
            pb.Add(a => a.Text, "test-Text");
        });

        cut.Contains("test-Text");
    }

    [Fact]
    public void Count_Empty()
    {
        var ds = new List<Foo>();

        var cut = Context.RenderComponent<TableFooterCell>(pb =>
        {
            pb.AddCascadingValue<object>("TableFooterContext", ds);
            pb.Add(a => a.Field, nameof(Foo.Count));
            pb.Add(a => a.Aggregate, AggregateType.Count);
        });
        cut.Contains("0");
    }

    [Fact]
    public void Count_Ok()
    {
        var ds = new List<Foo>()
        {
            new Foo() { Count = 1 },
            new Foo() { Count = 2 },
            new Foo() { Count = 3 },
        };
        var cut = Context.RenderComponent<TableFooterCell>(pb =>
        {
            pb.AddCascadingValue<object>("TableFooterContext", ds);
            pb.Add(a => a.Field, nameof(Foo.Count));
            pb.Add(a => a.Aggregate, AggregateType.Count);
        });
        cut.Contains("3");
    }

    [Fact]
    public void Align_Ok()
    {
        var ds = new List<Foo>()
        {
            new Foo() { Count = 1 },
            new Foo() { Count = 2 },
            new Foo() { Count = 3 },
        };

        var cut = Context.RenderComponent<TableFooterCell>(pb =>
        {
            pb.AddCascadingValue<object>("TableFooterContext", ds);
            pb.Add(a => a.Field, nameof(Foo.Count));
            pb.Add(a => a.Align, Alignment.Right);
        });
        cut.Contains("justify-content-end");
    }

    [Theory]
    [InlineData(AggregateType.Sum, "6")]
    [InlineData(AggregateType.Average, "2")]
    [InlineData(AggregateType.Count, "3")]
    [InlineData(AggregateType.Max, "3")]
    [InlineData(AggregateType.Min, "1")]
    public void Aggregate_Ok(AggregateType aggregate, string expected)
    {
        var ds = new List<Foo>()
        {
            new Foo() { Count = 1 },
            new Foo() { Count = 2 },
            new Foo() { Count = 3 },
        };

        var cut = Context.RenderComponent<TableFooterCell>(pb =>
        {
            pb.AddCascadingValue<object>("TableFooterContext", ds);
            pb.Add(a => a.Field, nameof(Foo.Count));
            pb.Add(a => a.Aggregate, aggregate);
        });
        cut.Contains(expected);
    }

    [Fact]
    public void Aggregate_Customer()
    {
        var ds = new List<MockFoo>()
        {
            new() { Name = "1" },
            new() { Name = "2" },
            new() { Name = "3" },
        };

        var cut = Context.RenderComponent<TableFooterCell>(pb =>
        {
            pb.AddCascadingValue<object>("TableFooterContext", ds);
            pb.Add(a => a.Field, nameof(MockFoo.Name));
            pb.Add(a => a.Aggregate, AggregateType.Customer);
            pb.Add(a => a.CustomerAggregateCallback, (obj, field) =>
            {
                return "test-customer-aggregate";
            });
        });
        cut.Contains("test-customer-aggregate");
    }

    [Fact]
    public void Aggregate_Empty()
    {
        var ds = new List<Foo>()
        {
            new Foo() { Count = 1 },
            new Foo() { Count = 2 },
            new Foo() { Count = 3 },
        };

        var cut = Context.RenderComponent<TableFooterCell>(pb =>
        {
            pb.AddCascadingValue<object>("TableFooterContext", ds);
            pb.Add(a => a.Aggregate, AggregateType.Average);
        });
    }

    [Fact]
    public void Double_Ok()
    {
        var ds = new List<MockFoo>()
        {
            new() { Count = 1.0 },
            new() { Count = 2.0 },
            new() { Count = 3.0 },
        };

        var cut = Context.RenderComponent<TableFooterCell>(pb =>
        {
            pb.AddCascadingValue<object>("TableFooterContext", ds);
            pb.Add(a => a.Field, nameof(MockFoo.Count));
            pb.Add(a => a.Aggregate, AggregateType.Average);
        });

        cut.Contains("2");
    }

    [Fact]
    public void Single_Ok()
    {
        var ds = new List<MockFoo>()
        {
            new() { FloatCount = 1.0f },
            new() { FloatCount = 2.0f },
            new() { FloatCount = 3.0f },
        };

        var cut = Context.RenderComponent<TableFooterCell>(pb =>
        {
            pb.AddCascadingValue<object>("TableFooterContext", ds);
            pb.Add(a => a.Field, nameof(MockFoo.FloatCount));
            pb.Add(a => a.Aggregate, AggregateType.Average);
        });

        cut.Contains("2");
    }

    [Fact]
    public void Long_Ok()
    {
        var ds = new List<MockFoo>()
        {
            new() { LongCount = 1 },
            new() { LongCount = 2 },
            new() { LongCount = 3 },
        };

        var cut = Context.RenderComponent<TableFooterCell>(pb =>
        {
            pb.AddCascadingValue<object>("TableFooterContext", ds);
            pb.Add(a => a.Field, nameof(MockFoo.LongCount));
            pb.Add(a => a.Aggregate, AggregateType.Average);
        });

        cut.Contains("2");
    }

    [Fact]
    public void Decimal_Ok()
    {
        var ds = new List<MockFoo>()
        {
            new() { DecimalCount = 1.0m },
            new() { DecimalCount = 2.0m },
            new() { DecimalCount = 3.0m },
        };

        var cut = Context.RenderComponent<TableFooterCell>(pb =>
        {
            pb.AddCascadingValue<object>("TableFooterContext", ds);
            pb.Add(a => a.Field, nameof(MockFoo.DecimalCount));
            pb.Add(a => a.Aggregate, AggregateType.Average);
        });

        cut.Contains("2");
    }

    [Fact]
    public void Name_Exception()
    {
        var ds = new List<MockFoo>()
        {
            new() { Name = "1" },
            new() { Name = "2" },
            new() { Name = "3" },
        };

        var cut = Context.RenderComponent<TableFooterCell>(pb =>
        {
            pb.AddCascadingValue<object>("TableFooterContext", ds);
            pb.Add(a => a.Field, nameof(MockFoo.Name));
            pb.Add(a => a.Aggregate, AggregateType.Average);
        });
    }

    [Fact]
    public void FormatString_Ok()
    {
        var ds = new List<MockFoo>()
        {
            new() { DecimalCount = 1.1m },
            new() { DecimalCount = 2.2m },
            new() { DecimalCount = 3.3m },
        };
        var cut = Context.RenderComponent<TableFooterCell>(pb =>
        {
            pb.AddCascadingValue<object>("TableFooterContext", ds);
            pb.Add(a => a.Field, nameof(MockFoo.DecimalCount));
            pb.Add(a => a.Aggregate, AggregateType.Average);
            pb.Add(a => a.FormatString, "#.00");
        });
        cut.Contains("2.20");
    }

    [Fact]
    public void Formatter_Ok()
    {
        var ds = new List<MockFoo>()
        {
            new() { DecimalCount = 1.1m },
            new() { DecimalCount = 2.2m },
            new() { DecimalCount = 3.3m },
        };
        var cut = Context.RenderComponent<TableFooterCell>(pb =>
        {
            pb.AddCascadingValue<object>("TableFooterContext", ds);
            pb.Add(a => a.Field, nameof(MockFoo.DecimalCount));
            pb.Add(a => a.Aggregate, AggregateType.Average);
            pb.Add(a => a.Formatter, v => Task.FromResult(v?.ToString()));
        });
        cut.Contains("2.2");
    }

    private class MockFoo
    {
        public double Count { get; set; }

        public long LongCount { get; set; }

        public float FloatCount { get; set; }

        public decimal DecimalCount { get; set; }

        public string? Name { get; set; }
    }
}
