// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;

namespace UnitTest.Components;

public class TableFilterTest : BootstrapBlazorTestBase
{
    private IStringLocalizer<Foo> Localizer { get; }

    public TableFilterTest()
    {
        Localizer = Context.Services.GetRequiredService<IStringLocalizer<Foo>>();
    }

    [Fact]
    public void TableFilter_Ok()
    {
        var cut = Context.RenderComponent<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<Table<Foo>>(pb =>
            {
                pb.Add(a => a.Items, Foo.GenerateFoo(Localizer));
                pb.Add(a => a.RenderMode, TableRenderMode.Table);
                pb.Add(a => a.ShowFilterHeader, true);
                pb.Add(a => a.TableColumns, CreateTableColumns());
            });
        });

        var table = cut.FindComponent<Table<Foo>>();
        table.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.ShowFilterHeader, false);
        });

        var filterInstance = cut.FindComponent<TableFilter>();

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
    public void FilterTemplate_Ok()
    {
        var cut = Context.RenderComponent<BootstrapBlazorRoot>(pb =>
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
    public async Task MultiFilter_Ok()
    {
        var cut = Context.RenderComponent<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<Table<Cat>>(pb =>
            {
                pb.Add(a => a.Items, new List<Cat>
                {
                    new()
                });
                pb.Add(a => a.RenderMode, TableRenderMode.Table);
                pb.Add(a => a.TableColumns, cat => builder =>
                {
                    var index = 0;
                    builder.OpenComponent<TableColumn<Cat, string?>>(index++);
                    builder.AddAttribute(index++, nameof(TableColumn<Cat, string?>.Field), cat.P8);
                    builder.AddAttribute(index++, nameof(TableColumn<Cat, string?>.FieldExpression), Utility.GenerateValueExpression(cat, nameof(Cat.P8), typeof(string)));
                    builder.AddAttribute(index++, nameof(TableColumn<Cat, string?>.Filterable), true);
                    builder.AddAttribute(index++, nameof(TableColumn<Cat, string?>.FilterTemplate), new RenderFragment(b =>
                    {
                        b.OpenComponent<MultiFilter>(0);
                        b.AddAttribute(1, nameof(MultiFilter.ShowSearch), true);
                        b.AddAttribute(2, nameof(MultiFilter.Items), new SelectedItem[] {
                            new("test1", "test1"),
                            new("test2", "test2")
                        });
                        b.CloseComponent();
                    }));
                    builder.CloseComponent();
                });
            });
        });

        cut.DoesNotContain("multi-filter-placeholder");
        cut.DoesNotContain("multi-filter-All");

        var filter = cut.FindComponent<MultiFilter>();
        filter.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.SearchPlaceHolderText, "multi-filter-placeholder");
            pb.Add(a => a.SelectAllText, "multi-filter-All");
        });
        cut.Contains("multi-filter-placeholder");
        cut.Contains("multi-filter-All");

        await cut.InvokeAsync(() => filter.Instance.Reset());

        // 选中选项
        var checkboxs = cut.FindComponents<Checkbox<bool>>();
        Assert.Equal(3, checkboxs.Count);
        await cut.InvokeAsync(() => checkboxs[2].Instance.SetState(CheckboxState.Checked));

        FilterKeyValueAction? action = null;
        await cut.InvokeAsync(() =>
        {
            action = filter.Instance.GetFilterConditions();
        });
        Assert.NotNull(action);
        Assert.Equal(FilterLogic.Or, action.FilterLogic);
        Assert.NotNull(action.Filters);
        Assert.NotNull(action.Filters[0]);
        Assert.Equal("test2", action.Filters[0].FieldValue);
        Assert.Equal("P8", action.Filters[0].FieldKey);
        Assert.Equal(CheckboxState.Indeterminate, checkboxs[0].Instance.State);

        // 测试全选
        await cut.InvokeAsync(() => checkboxs[0].Instance.SetState(CheckboxState.Checked));
        await cut.InvokeAsync(() => checkboxs[0].Instance.SetState(CheckboxState.UnChecked));

        // 测试搜索
        var input = cut.Find(".bb-multi-filter-search");
        await cut.InvokeAsync(() => input.Input("test02"));
        await cut.InvokeAsync(() => input.Input(""));
    }

    [Fact]
    public void NotInTable_Ok()
    {
        var cut = Context.RenderComponent<TableFilter>(pb =>
        {
            var foo = Foo.Generate(Localizer);
            var column = new TableColumn<Foo, string>();
            column.SetParametersAsync(ParameterView.FromDictionary(new Dictionary<string, object?>()
            {
                [nameof(TableColumn<Foo, string>.Field)] = foo.Name,
                [nameof(TableColumn<Foo, string>.FieldExpression)] = foo.GenerateValueExpression(),
            }));
            pb.Add(a => a.IsHeaderRow, true);
            pb.Add(a => a.Column, column);
        });
    }

    [Fact]
    public void OnlyOneTableFilterPerColumn_Ok()
    {
        var cut = Context.RenderComponent<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<Table<Foo>>(pb =>
            {
                pb.Add(a => a.Items, new List<Foo>() { new() });
                pb.Add(a => a.RenderMode, TableRenderMode.Table);
                pb.Add(a => a.ShowFilterHeader, true);
                pb.Add(a => a.TableColumns, new RenderFragment<Foo>(foo => builder =>
                {
                    var index = 0;
                    builder.OpenComponent<TableColumn<Foo, bool>>(index++);
                    builder.AddAttribute(index++, nameof(TableColumn<Foo, bool>.Field), foo.Complete);
                    builder.AddAttribute(index++, nameof(TableColumn<Foo, bool>.FieldExpression), foo.GenerateValueExpression(nameof(Foo.Complete), typeof(bool)));
                    builder.AddAttribute(index++, nameof(TableColumn<Foo, bool>.Filterable), true);
                    builder.CloseComponent();
                }));
            });
        });

        var filters = cut.FindComponents<BoolFilter>();
        Assert.Single(filters);
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
}
