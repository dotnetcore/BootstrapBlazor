// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.Extensions.Localization;

namespace UnitTest.Components;

public partial class TableColumnFilterTest : BootstrapBlazorTestBase
{
    [Fact]
    public void TableColumnFilter_Ok()
    {
        var localizer = Context.Services.GetRequiredService<IStringLocalizer<Foo>>();
        var cut = Context.Render<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<Table<Foo>>(pb =>
            {
                pb.Add(a => a.Items, Foo.GenerateFoo(localizer));
                pb.Add(a => a.RenderMode, TableRenderMode.Table);
                pb.Add(a => a.ShowFilterHeader, true);
                pb.Add(a => a.TableColumns, CreateTableColumns());
            });
        });

        var table = cut.FindComponent<Table<Foo>>();
        table.Render(pb =>
        {
            pb.Add(a => a.ShowFilterHeader, false);
        });

        var filterInstance = cut.FindComponent<TableColumnFilter>();

        // Reset/Confirm buttons
        // ClickReset
        var buttons = filterInstance.FindAll(".filter-dismiss");
        cut.InvokeAsync(() => buttons[0].Click());

        // ClickConfirm
        cut.InvokeAsync(() => buttons[1].Click());

        // OnFilterAsync
        var input = filterInstance.FindComponent<BootstrapInput<string>>();
        cut.InvokeAsync(() => input.Instance.SetValue("0001"));
        cut.InvokeAsync(() => buttons[1].Click());

        // Show more button
        buttons = filterInstance.FindAll("button");

        // add +
        cut.InvokeAsync(() => buttons[0].Click());

        // sub -
        cut.InvokeAsync(() => buttons[1].Click());
    }

    [Fact]
    public void FilterProvider_Ok()
    {
        var cut = Context.Render<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<Table<Cat>>(pb =>
            {
                pb.Add(a => a.Items, new List<Cat>
                {
                    new()
                });
                pb.Add(a => a.RenderMode, TableRenderMode.Table);
                pb.Add(a => a.TableColumns, CreateCatTableColumns());
            });
        });
    }

    [Fact]
    public async Task OnFilterAsync_Ok()
    {
        var cut = Context.Render<TableColumnFilter>(pb =>
        {
            pb.Add(a => a.Table, new MockTable());
            pb.Add(a => a.Column, new MockColumn<string>());
        });

        var filter = cut.Instance;
        await cut.InvokeAsync(() => filter.OnFilterAsync());

        cut.Render(pb =>
        {
            pb.Add(a => a.Table, new MockTable() { OnFilterAsync = () => Task.CompletedTask });
        });
        await cut.InvokeAsync(() => filter.OnFilterAsync());

        var f = cut.FindComponent<StringFilter>();
        await cut.InvokeAsync(() => f.Instance.SetFilterConditionsAsync(new FilterKeyValueAction()
        {
            Filters =
            [
                new() { FieldValue = "test" }
            ]
        }));
        await cut.InvokeAsync(() => filter.OnFilterAsync());
    }

    private static RenderFragment<Foo> CreateTableColumns() => foo => builder =>
    {
        var index = 0;
        builder.OpenComponent<TableColumn<Foo, string>>(index++);
        builder.AddAttribute(index++, nameof(TableColumn<Foo, string>.Field), foo.Name);
        builder.AddAttribute(index++, nameof(TableColumn<Foo, string>.FieldExpression), foo.GenerateValueExpression());
        builder.AddAttribute(index++, nameof(TableColumn<Foo, string>.Filterable), true);
        builder.CloseComponent();

        builder.OpenComponent<TableColumn<Foo, bool>>(index++);
        builder.AddAttribute(index++, nameof(TableColumn<Foo, bool>.Field), foo.Complete);
        builder.AddAttribute(index++, nameof(TableColumn<Foo, bool>.FieldExpression), foo.GenerateValueExpression(nameof(Foo.Complete), typeof(bool)));
        builder.AddAttribute(index++, nameof(TableColumn<Foo, bool>.Filterable), true);
        builder.CloseComponent();

        builder.OpenComponent<TableColumn<Foo, int>>(index++);
        builder.AddAttribute(index++, nameof(TableColumn<Foo, int>.Field), foo.Count);
        builder.AddAttribute(index++, nameof(TableColumn<Foo, int>.FieldExpression), foo.GenerateValueExpression(nameof(Foo.Count), typeof(int)));
        builder.AddAttribute(index++, nameof(TableColumn<Foo, int>.Filterable), true);
        builder.CloseComponent();

        builder.OpenComponent<TableColumn<Foo, EnumEducation?>>(index++);
        builder.AddAttribute(index++, nameof(TableColumn<Foo, EnumEducation?>.Field), foo.Education);
        builder.AddAttribute(index++, nameof(TableColumn<Foo, EnumEducation?>.FieldExpression), foo.GenerateValueExpression(nameof(Foo.Education), typeof(EnumEducation?)));
        builder.AddAttribute(index++, nameof(TableColumn<Foo, EnumEducation?>.Filterable), true);
        builder.CloseComponent();

        builder.OpenComponent<TableColumn<Foo, DateTime?>>(index++);
        builder.AddAttribute(index++, nameof(TableColumn<Foo, DateTime?>.Field), foo.DateTime);
        builder.AddAttribute(index++, nameof(TableColumn<Foo, DateTime?>.FieldExpression), foo.GenerateValueExpression(nameof(Foo.DateTime), typeof(DateTime?)));
        builder.AddAttribute(index++, nameof(TableColumn<Foo, DateTime?>.Filterable), true);
        builder.CloseComponent();

        builder.OpenComponent<TableColumn<Foo, IEnumerable<string>>>(index++);
        builder.AddAttribute(index++, nameof(TableColumn<Foo, IEnumerable<string>>.Field), foo.Hobby);
        builder.AddAttribute(index++, nameof(TableColumn<Foo, IEnumerable<string>>.Lookup), new List<SelectedItem>()
        {
            new("1", "Test1"),
            new("2", "Test2"),
        });
        builder.AddAttribute(index++, nameof(TableColumn<Foo, IEnumerable<string>>.FieldExpression), foo.GenerateValueExpression(nameof(Foo.Hobby), typeof(IEnumerable<string>)));
        builder.AddAttribute(index++, nameof(TableColumn<Foo, IEnumerable<string>>.Filterable), true);
        builder.CloseComponent();

        builder.OpenComponent<TableColumn<Foo, int>>(index++);
        builder.AddAttribute(index++, nameof(TableColumn<Foo, int>.Field), foo.Id);
        builder.AddAttribute(index++, nameof(TableColumn<Foo, int>.LookupServiceKey), "FooLookup");
        builder.AddAttribute(index++, nameof(TableColumn<Foo, int>.LookupServiceData), new Foo());
        builder.AddAttribute(index++, nameof(TableColumn<Foo, int>.FieldExpression), foo.GenerateValueExpression(nameof(Foo.Id), typeof(int)));
        builder.AddAttribute(index++, nameof(TableColumn<Foo, int>.Filterable), true);
        builder.CloseComponent();
    };

    private static RenderFragment<Cat> CreateCatTableColumns() => cat => builder =>
    {
        var index = 0;
        builder.OpenComponent<TableColumn<Cat, short>>(index++);
        builder.AddAttribute(index++, nameof(TableColumn<Cat, short>.Field), cat.P1);
        builder.AddAttribute(index++, nameof(TableColumn<Cat, short>.FieldExpression), Utility.GenerateValueExpression(cat, nameof(Cat.P1), typeof(short)));
        builder.AddAttribute(index++, nameof(TableColumn<Cat, short>.Filterable), true);
        builder.CloseComponent();

        builder.OpenComponent<TableColumn<Cat, long>>(index++);
        builder.AddAttribute(index++, nameof(TableColumn<Cat, long>.Field), cat.P2);
        builder.AddAttribute(index++, nameof(TableColumn<Cat, long>.FieldExpression), Utility.GenerateValueExpression(cat, nameof(Cat.P2), typeof(long)));
        builder.AddAttribute(index++, nameof(TableColumn<Cat, long>.Filterable), true);
        builder.CloseComponent();

        builder.OpenComponent<TableColumn<Cat, float>>(index++);
        builder.AddAttribute(index++, nameof(TableColumn<Cat, float>.Field), cat.P3);
        builder.AddAttribute(index++, nameof(TableColumn<Cat, float>.FieldExpression), Utility.GenerateValueExpression(cat, nameof(Cat.P3), typeof(float)));
        builder.AddAttribute(index++, nameof(TableColumn<Cat, float>.Filterable), true);
        builder.CloseComponent();

        builder.OpenComponent<TableColumn<Cat, double>>(index++);
        builder.AddAttribute(index++, nameof(TableColumn<Cat, double>.Field), cat.P4);
        builder.AddAttribute(index++, nameof(TableColumn<Cat, double>.FieldExpression), Utility.GenerateValueExpression(cat, nameof(Cat.P4), typeof(double)));
        builder.AddAttribute(index++, nameof(TableColumn<Cat, double>.Filterable), true);
        builder.AddAttribute(index++, nameof(TableColumn<Cat, double>.Step), "0.02");
        builder.CloseComponent();

        builder.OpenComponent<TableColumn<Cat, decimal>>(index++);
        builder.AddAttribute(index++, nameof(TableColumn<Cat, decimal>.Field), cat.P5);
        builder.AddAttribute(index++, nameof(TableColumn<Cat, decimal>.FieldExpression), Utility.GenerateValueExpression(cat, nameof(Cat.P5), typeof(decimal)));
        builder.AddAttribute(index++, nameof(TableColumn<Cat, decimal>.Filterable), true);
        builder.CloseComponent();

        builder.OpenComponent<TableColumn<Cat, decimal>>(index++);
        builder.AddAttribute(index++, nameof(TableColumn<Cat, decimal>.Field), cat.P6);
        builder.AddAttribute(index++, nameof(TableColumn<Cat, decimal>.FieldExpression), Utility.GenerateValueExpression(cat, nameof(Cat.P6), typeof(decimal)));
        builder.AddAttribute(index++, nameof(TableColumn<Cat, decimal>.Filterable), true);
        builder.AddAttribute(index++, nameof(TableColumn<Cat, decimal>.FilterTemplate), new RenderFragment(builder =>
        {
            builder.AddContent(0, "Test-FilterTemplate");
        }));
        builder.CloseComponent();

        builder.OpenComponent<TableColumn<Cat, Foo>>(index++);
        builder.AddAttribute(index++, nameof(TableColumn<Cat, Foo>.Field), cat.P7);
        builder.AddAttribute(index++, nameof(TableColumn<Cat, Foo>.FieldExpression), Utility.GenerateValueExpression(cat, nameof(Cat.P7), typeof(Foo)));
        builder.AddAttribute(index++, nameof(TableColumn<Cat, Foo>.Filterable), true);
        builder.CloseComponent();
    };

    private class Cat
    {
        public short P1 { get; set; }

        public long P2 { get; set; }

        public float P3 { get; set; }

        public double P4 { get; set; }

        public decimal P5 { get; set; }

        public decimal P6 { get; set; }

        public Foo? P7 { get; set; }

        public string? P8 { get; set; }
    }

    private class MockTable : ITable
    {
        public Dictionary<string, IFilterAction> Filters { get; set; } = [];

        public Func<Task>? OnFilterAsync { get; set; }

        public List<ITableColumn> Columns => [];

        public IEnumerable<ITableColumn> GetVisibleColumns() => Columns;
    }

    private class MockColumn<TType> : TableColumn<Foo, TType>
    {
        public MockColumn()
        {
            PropertyType = typeof(TType);
            FieldName = "Name";
        }
    }
}
