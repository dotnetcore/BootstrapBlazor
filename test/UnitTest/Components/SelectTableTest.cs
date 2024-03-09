// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components.Forms;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;

namespace UnitTest.Components;

public class SelectTableTest : BootstrapBlazorTestBase
{
    [Fact]
    public void Items_Ok()
    {
        var localizer = Context.Services.GetRequiredService<IStringLocalizer<Foo>>();
        var items = Foo.GenerateFoo(localizer, 4);
        var cut = Context.RenderComponent<BootstrapBlazorRoot>(pb =>
        {
            pb.Add(a => a.EnableErrorLogger, false);
            pb.AddChildContent<SelectTable<Foo>>(pb =>
            {
                pb.Add(a => a.OnQueryAsync, options => OnFilterQueryAsync(options, items));
                pb.Add(a => a.GetTextCallback, foo => foo.Name);
            });
        });
        var table = cut.FindComponent<SelectTable<Foo>>();
        Assert.Throws<InvalidOperationException>(() =>
        {
            table.SetParametersAndRender(pb =>
            {
                pb.Add(a => a.OnQueryAsync, null);
            });
        });
    }

    [Fact]
    public void TableMinWidth_Ok()
    {
        var localizer = Context.Services.GetRequiredService<IStringLocalizer<Foo>>();
        var items = Foo.GenerateFoo(localizer, 4);
        var cut = Context.RenderComponent<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<SelectTable<Foo>>(pb =>
            {
                pb.Add(a => a.TableMinWidth, 300);
                pb.Add(a => a.OnQueryAsync, options => OnFilterQueryAsync(options, items));
                pb.Add(a => a.GetTextCallback, foo => foo.Name);
            });
        });
        Assert.Contains("data-bb-min-width=\"300\"", cut.Markup);
    }

    [Fact]
    public void Color_Ok()
    {
        var localizer = Context.Services.GetRequiredService<IStringLocalizer<Foo>>();
        var items = Foo.GenerateFoo(localizer, 4);
        var cut = Context.RenderComponent<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<SelectTable<Foo>>(pb =>
            {
                pb.Add(a => a.Color, Color.Danger);
                pb.Add(a => a.GetTextCallback, foo => foo.Name);
                pb.Add(a => a.OnQueryAsync, options => OnFilterQueryAsync(options, items));
            });
        });
        cut.Contains("border-danger");
    }

    [Fact]
    public void ShowAppendArrow_Ok()
    {
        var localizer = Context.Services.GetRequiredService<IStringLocalizer<Foo>>();
        var items = Foo.GenerateFoo(localizer, 4);
        var cut = Context.RenderComponent<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<SelectTable<Foo>>(pb =>
            {
                pb.Add(a => a.ShowAppendArrow, false);
                pb.Add(a => a.OnQueryAsync, options => OnFilterQueryAsync(options, items));
                pb.Add(a => a.GetTextCallback, foo => foo.Name);
            });
        });
        cut.DoesNotContain("form-select-append");
    }

    [Fact]
    public void Template_Ok()
    {
        var localizer = Context.Services.GetRequiredService<IStringLocalizer<Foo>>();
        var items = Foo.GenerateFoo(localizer, 4);
        var cut = Context.RenderComponent<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<SelectTable<Foo>>(pb =>
            {
                pb.Add(a => a.OnQueryAsync, options => OnFilterQueryAsync(options, items));
                pb.Add(a => a.GetTextCallback, foo => foo.Name);
                pb.Add(a => a.TableColumns, foo => builder =>
                {
                    builder.OpenComponent<TableColumn<Foo, string>>(0);
                    builder.AddAttribute(1, "Field", "Name");
                    builder.AddAttribute(2, "FieldExpression", Utility.GenerateValueExpression(foo, "Name", typeof(string)));
                    builder.CloseComponent();

                    builder.OpenComponent<TableColumn<Foo, string>>(0);
                    builder.AddAttribute(1, "Field", "Address");
                    builder.AddAttribute(2, "FieldExpression", Utility.GenerateValueExpression(foo, "Address", typeof(string)));
                    builder.CloseComponent();
                });
                pb.Add(a => a.Template, foo => builder =>
                {
                    builder.AddContent(0, $"Template-{foo.Name}");
                });
            });
        });
        var rows = cut.FindAll("tbody > tr");
        Assert.Equal(4, rows.Count);

        var table = cut.FindComponent<SelectTable<Foo>>();
        table.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.Value, items[0]);
        });
        Assert.Contains($"Template-{items[0].Name}", cut.Markup);
    }

    [Fact]
    public void GetTextCallback_Ok()
    {
        var localizer = Context.Services.GetRequiredService<IStringLocalizer<Foo>>();
        var items = Foo.GenerateFoo(localizer, 4);
        var cut = Context.RenderComponent<BootstrapBlazorRoot>(pb =>
        {
            pb.Add(a => a.EnableErrorLogger, false);
            pb.AddChildContent<SelectTable<Foo>>(pb =>
            {
                pb.Add(a => a.OnQueryAsync, options => OnFilterQueryAsync(options, items));
                pb.Add(a => a.Value, items[0]);
                pb.Add(a => a.TableColumns, foo => builder =>
                {
                    builder.OpenComponent<TableColumn<Foo, string>>(0);
                    builder.AddAttribute(1, "Field", "Name");
                    builder.AddAttribute(2, "FieldExpression", Utility.GenerateValueExpression(foo, "Name", typeof(string)));
                    builder.CloseComponent();

                    builder.OpenComponent<TableColumn<Foo, string>>(0);
                    builder.AddAttribute(1, "Field", "Address");
                    builder.AddAttribute(2, "FieldExpression", Utility.GenerateValueExpression(foo, "Address", typeof(string)));
                    builder.CloseComponent();
                });
                pb.Add(a => a.GetTextCallback, foo => foo.Name);
            });
        });
        Assert.Contains($"value=\"{items[0].Name}\"", cut.Markup);

        var table = cut.FindComponent<SelectTable<Foo>>();
        table.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.GetTextCallback, foo => null);
        });

        table.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.Value, null);
        });
        Assert.DoesNotContain("value=\"\"", cut.Markup);

        Assert.Throws<InvalidOperationException>(() =>
        {
            table.SetParametersAndRender(pb =>
            {
                pb.Add(a => a.GetTextCallback, null);
            });
        });
    }

    [Fact]
    public void Height_Ok()
    {
        var localizer = Context.Services.GetRequiredService<IStringLocalizer<Foo>>();
        var items = Foo.GenerateFoo(localizer, 4);
        var cut = Context.RenderComponent<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<SelectTable<Foo>>(pb =>
            {
                pb.Add(a => a.OnQueryAsync, options => OnFilterQueryAsync(options, items));
                pb.Add(a => a.Value, items[0]);
                pb.Add(a => a.Height, 100);
                pb.Add(a => a.TableColumns, foo => builder =>
                {
                    builder.OpenComponent<TableColumn<Foo, string>>(0);
                    builder.AddAttribute(1, "Field", "Name");
                    builder.AddAttribute(2, "FieldExpression", Utility.GenerateValueExpression(foo, "Name", typeof(string)));
                    builder.CloseComponent();

                    builder.OpenComponent<TableColumn<Foo, string>>(0);
                    builder.AddAttribute(1, "Field", "Address");
                    builder.AddAttribute(2, "FieldExpression", Utility.GenerateValueExpression(foo, "Address", typeof(string)));
                    builder.CloseComponent();
                });
                pb.Add(a => a.GetTextCallback, foo => foo.Name);
            });
        });
        Assert.Contains($"height: 100px;", cut.Markup);
    }

    [Fact]
    public async Task Value_Ok()
    {
        var localizer = Context.Services.GetRequiredService<IStringLocalizer<Foo>>();
        var items = Foo.GenerateFoo(localizer, 4);
        Foo? v = null;
        var cut = Context.RenderComponent<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<SelectTable<Foo>>(pb =>
            {
                pb.Add(a => a.OnQueryAsync, options => OnFilterQueryAsync(options, items));
                pb.Add(a => a.Value, items[0]);
                pb.Add(a => a.OnValueChanged, foo =>
                {
                    v = foo;
                    return Task.CompletedTask;
                });
                pb.Add(a => a.GetTextCallback, foo => foo.Name);
                pb.Add(a => a.TableColumns, foo => builder =>
                {
                    builder.OpenComponent<TableColumn<Foo, string>>(0);
                    builder.AddAttribute(1, "Field", "Name");
                    builder.AddAttribute(2, "FieldExpression", Utility.GenerateValueExpression(foo, "Name", typeof(string)));
                    builder.CloseComponent();

                    builder.OpenComponent<TableColumn<Foo, string>>(0);
                    builder.AddAttribute(1, "Field", "Address");
                    builder.AddAttribute(2, "FieldExpression", Utility.GenerateValueExpression(foo, "Address", typeof(string)));
                    builder.CloseComponent();
                });
            });
        });

        var rows = cut.FindAll("tbody > tr");
        await cut.InvokeAsync(() =>
        {
            rows[1].Click();
        });
        Assert.Equal(items[1].Name, v?.Name);
    }

    [Fact]
    public async Task Validate_Ok()
    {
        var localizer = Context.Services.GetRequiredService<IStringLocalizer<Foo>>();
        var items = Foo.GenerateFoo(localizer, 4);
        var valid = false;
        var invalid = false;
        var model = new SelectTableModel() { Foo = items[0] };
        var cut = Context.RenderComponent<ValidateForm>(builder =>
        {
            builder.Add(a => a.OnValidSubmit, context =>
            {
                valid = true;
                return Task.CompletedTask;
            });
            builder.Add(a => a.OnInvalidSubmit, context =>
            {
                invalid = true;
                return Task.CompletedTask;
            });
            builder.Add(a => a.Model, model);
            builder.AddChildContent<SelectTable<Foo>>(pb =>
            {
                pb.Add(a => a.Value, model.Foo);
                pb.Add(a => a.ValueExpression, Utility.GenerateValueExpression(model, "Foo", typeof(Foo)));
                pb.Add(a => a.OnValueChanged, v =>
                {
                    model.Foo = v;
                    return Task.CompletedTask;
                });
                pb.Add(a => a.GetTextCallback, foo => foo.Name);
                pb.Add(a => a.OnQueryAsync, options => OnFilterQueryAsync(options, items));
                pb.Add(a => a.TableColumns, foo => builder =>
                {
                    builder.OpenComponent<TableColumn<Foo, string>>(0);
                    builder.AddAttribute(1, "Field", "Name");
                    builder.AddAttribute(2, "FieldExpression", Utility.GenerateValueExpression(foo, "Name", typeof(string)));
                    builder.CloseComponent();

                    builder.OpenComponent<TableColumn<Foo, string>>(0);
                    builder.AddAttribute(1, "Field", "Address");
                    builder.AddAttribute(2, "FieldExpression", Utility.GenerateValueExpression(foo, "Address", typeof(string)));
                    builder.CloseComponent();
                });
            });
        });

        await cut.InvokeAsync(() =>
        {
            var form = cut.Find("form");
            form.Submit();
        });
        Assert.True(valid);

        model.Foo = null;
        var table = cut.FindComponent<SelectTable<Foo>>();
        table.SetParametersAndRender();
        await cut.InvokeAsync(() =>
        {
            var form = cut.Find("form");
            form.Submit();
        });
        Assert.True(invalid);
    }

    [Fact]
    public void Search_Ok()
    {
        var localizer = Context.Services.GetRequiredService<IStringLocalizer<Foo>>();
        var items = Foo.GenerateFoo(localizer, 4);
        var cut = Context.RenderComponent<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<SelectTable<Foo>>(pb =>
            {
                pb.Add(a => a.OnQueryAsync, options => OnFilterQueryAsync(options, items));
                pb.Add(a => a.Value, items[0]);
                pb.Add(a => a.GetTextCallback, foo => foo.Name);
                pb.Add(a => a.TableColumns, foo => builder =>
                {
                    builder.OpenComponent<TableColumn<Foo, string>>(0);
                    builder.AddAttribute(1, "Field", "Name");
                    builder.AddAttribute(2, "FieldExpression", Utility.GenerateValueExpression(foo, "Name", typeof(string)));
                    builder.CloseComponent();

                    builder.OpenComponent<TableColumn<Foo, string>>(0);
                    builder.AddAttribute(1, "Field", "Address");
                    builder.AddAttribute(2, "FieldExpression", Utility.GenerateValueExpression(foo, "Address", typeof(string)));
                    builder.CloseComponent();
                });
                pb.Add(a => a.ShowSearch, true);
                pb.Add(a => a.SearchModel, new Foo());
                pb.Add(a => a.CollapsedTopSearch, false);
                pb.Add(a => a.SearchTemplate, foo => builder => builder.AddContent(0, "SearchTemplate"));
            });
        });

        cut.Contains("SearchTemplate");
    }

    [Fact]
    public void EditTemplate_Ok()
    {
        // Table 组件 EditTemplate 内使用 SelectTable 组件单元测试
        var localizer = Context.Services.GetRequiredService<IStringLocalizer<Foo>>();
        var items = Foo.GenerateFoo(localizer);
        var cut = Context.RenderComponent<BootstrapBlazorRoot>(pb =>
        {
            pb.AddCascadingValue(true);
            pb.AddChildContent<EditForm>(pb =>
            {
                pb.Add(a => a.Model, new Dog() { Name = "Dog1" });
                pb.Add(a => a.ChildContent, new RenderFragment<EditContext>(context => pb =>
                {
                    pb.OpenComponent<SelectTable<Foo>>(0);
                    pb.AddAttribute(1, "OnQueryAsync", new Func<QueryPageOptions, Task<QueryData<Foo>>>(options => OnFilterQueryAsync(options, items)));
                    pb.AddAttribute(2, "Value", items[0]);
                    pb.AddAttribute(3, "GetTextCallback", new Func<Foo, string?>(foo => foo.Name));
                    pb.AddAttribute(4, "ShowSearch", true);
                    pb.AddAttribute(5, "TableColumns", new RenderFragment<Foo>(foo => builder =>
                    {
                        builder.OpenComponent<TableColumn<Foo, string>>(0);
                        builder.AddAttribute(1, "Field", "Name");
                        builder.AddAttribute(2, "FieldExpression", Utility.GenerateValueExpression(foo, "Name", typeof(string)));
                        builder.CloseComponent();

                        builder.OpenComponent<TableColumn<Foo, string>>(0);
                        builder.AddAttribute(1, "Field", "Address");
                        builder.AddAttribute(2, "FieldExpression", Utility.GenerateValueExpression(foo, "Address", typeof(string)));
                        builder.CloseComponent();
                    }));
                    pb.CloseComponent();
                }));
            });
        });

        // EditForm 模型 与 SelectTable 模型不一致 这里不报错就对了
        cut.Contains("form");
    }

    [Fact]
    public void CustomSearch_Ok()
    {
        var localizer = Context.Services.GetRequiredService<IStringLocalizer<Foo>>();
        var items = Foo.GenerateFoo(localizer, 4);
        var cut = Context.RenderComponent<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<SelectTable<Foo>>(pb =>
            {
                pb.Add(a => a.OnQueryAsync, options => OnFilterQueryAsync(options, items));
                pb.Add(a => a.Value, items[0]);
                pb.Add(a => a.GetTextCallback, foo => foo.Name);
                pb.Add(a => a.TableColumns, foo => builder =>
                {
                    builder.OpenComponent<TableColumn<Foo, string>>(0);
                    builder.AddAttribute(1, "Field", "Name");
                    builder.AddAttribute(2, "FieldExpression", Utility.GenerateValueExpression(foo, "Name", typeof(string)));
                    builder.CloseComponent();

                    builder.OpenComponent<TableColumn<Foo, string>>(0);
                    builder.AddAttribute(1, "Field", "Address");
                    builder.AddAttribute(2, "FieldExpression", Utility.GenerateValueExpression(foo, "Address", typeof(string)));
                    builder.CloseComponent();
                });
                pb.Add(a => a.ShowSearch, true);
                pb.Add(a => a.CustomerSearchModel, new CustomSearchModel());
                pb.Add(a => a.CustomerSearchTemplate, model => builder => builder.AddContent(0, "CustomSearchTemplate"));
            });
        });

        cut.Contains("CustomSearchTemplate");
    }

    [Fact]
    public void Page_Ok()
    {
        var localizer = Context.Services.GetRequiredService<IStringLocalizer<Foo>>();
        var items = Foo.GenerateFoo(localizer);
        var pageItemsSource = new int[] { 4, 10, 20 };
        var cut = Context.RenderComponent<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<SelectTable<Foo>>(pb =>
            {
                pb.Add(a => a.OnQueryAsync, options => OnFilterQueryAsync(options, items));
                pb.Add(a => a.Value, items[0]);
                pb.Add(a => a.GetTextCallback, foo => foo.Name);
                pb.Add(a => a.TableColumns, foo => builder =>
                {
                    builder.OpenComponent<TableColumn<Foo, string>>(0);
                    builder.AddAttribute(1, "Field", "Name");
                    builder.AddAttribute(2, "FieldExpression", Utility.GenerateValueExpression(foo, "Name", typeof(string)));
                    builder.CloseComponent();

                    builder.OpenComponent<TableColumn<Foo, string>>(0);
                    builder.AddAttribute(1, "Field", "Address");
                    builder.AddAttribute(2, "FieldExpression", Utility.GenerateValueExpression(foo, "Address", typeof(string)));
                    builder.CloseComponent();
                });
                pb.Add(a => a.IsPagination, true);
                pb.Add(a => a.PageItemsSource, pageItemsSource);
            });
        });

        cut.Contains("nav-pages");
    }

    private static Task<QueryData<Foo>> OnFilterQueryAsync(QueryPageOptions options, IEnumerable<Foo> _filterItems)
    {
        _filterItems = _filterItems.Where(options.ToFilterFunc<Foo>());

        if (!string.IsNullOrEmpty(options.SortName))
        {
            _filterItems = _filterItems.Sort(options.SortName, options.SortOrder);
        }
        return Task.FromResult(new QueryData<Foo>()
        {
            Items = _filterItems.ToList(),
            IsAdvanceSearch = true,
            IsFiltered = true,
            IsSearch = true,
            IsSorted = true
        });
    }

    class SelectTableModel()
    {
        public Foo? Foo { get; set; }
    }

    class Dog()
    {
        public string? Name { get; set; }
    }

    class CustomSearchModel : ITableSearchModel
    {
        public IEnumerable<IFilterAction> GetSearches() => [];

        public void Reset() { }
    }
}
