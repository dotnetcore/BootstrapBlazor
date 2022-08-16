// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Shared;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace UnitTest.Components;

public class TableTest : TableTestBase
{
    [Fact]
    public void Items_Ok()
    {
        var localizer = Context.Services.GetRequiredService<IStringLocalizer<Foo>>();
        var cut = Context.RenderComponent<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<Table<Foo>>(pb =>
            {
                pb.Add(a => a.Items, Foo.GenerateFoo(localizer));
            });
        });

        cut.Contains("table");
    }

    [Fact]
    public async void Items_Bind()
    {
        var localizer = Context.Services.GetRequiredService<IStringLocalizer<Foo>>();
        var items = Foo.GenerateFoo(localizer, 2);
        var binded = false;
        var cut = Context.RenderComponent<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<Table<Foo>>(pb =>
            {
                pb.Add(a => a.Items, items);
                pb.Add(a => a.ItemsChanged, EventCallback.Factory.Create<IEnumerable<Foo>>(this, rows =>
                {
                    binded = true;
                }));
                pb.Add(a => a.EditMode, EditMode.InCell);
                pb.Add(a => a.RenderMode, TableRenderMode.Table);
                pb.Add(a => a.ShowExtendButtons, true);
            });
        });
        var button = cut.Find("button");
        await cut.InvokeAsync(() => button.Click());

        button = cut.Find("button");
        await cut.InvokeAsync(() => button.Click());
        Assert.True(binded);
    }

    [Fact]
    public async void SelectedRowsChanged_Bind()
    {
        var localizer = Context.Services.GetRequiredService<IStringLocalizer<Foo>>();
        var items = Foo.GenerateFoo(localizer, 2);
        var selectedRows = new List<Foo>();
        var count = 0;
        var cut = Context.RenderComponent<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<Table<Foo>>(pb =>
            {
                pb.Add(a => a.Items, items);
                pb.Add(a => a.SelectedRows, selectedRows);
                pb.Add(a => a.SelectedRowsChanged, EventCallback.Factory.Create<List<Foo>>(this, rows => count = rows.Count));
                pb.Add(a => a.EditMode, EditMode.InCell);
                pb.Add(a => a.RenderMode, TableRenderMode.Table);
                pb.Add(a => a.ShowExtendButtons, true);
            });
        });

        // 编辑时触发 SelectedRow
        var button = cut.Find("button");
        await cut.InvokeAsync(() => button.Click());

        button = cut.Find("button");
        await cut.InvokeAsync(() => button.Click());
        Assert.Equal(1, count);
    }

    [Theory]
    [InlineData(InsertRowMode.First)]
    [InlineData(InsertRowMode.Last)]
    public async Task Items_Add(InsertRowMode insertMode)
    {
        var localizer = Context.Services.GetRequiredService<IStringLocalizer<Foo>>();
        var items = Foo.GenerateFoo(localizer, 2);
        var cut = Context.RenderComponent<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<Table<Foo>>(pb =>
            {
                pb.Add(a => a.Items, items);
                pb.Add(a => a.ItemsChanged, EventCallback.Factory.Create<IEnumerable<Foo>>(this, rows =>
                {
                    items = rows.ToList();
                }));
                pb.Add(a => a.EditMode, EditMode.InCell);
                pb.Add(a => a.InsertRowMode, insertMode);
                pb.Add(a => a.RenderMode, TableRenderMode.Table);
                pb.Add(a => a.ShowExtendButtons, true);
            });
        });
        var table = cut.FindComponent<Table<Foo>>();
        await cut.InvokeAsync(() => table.Instance.AddAsync());

        if (insertMode == InsertRowMode.First)
        {
            var button = cut.Find("tbody tr button");
            await cut.InvokeAsync(() => button.Click());
            Assert.Null(items.First().Name);
        }
        else if (insertMode == InsertRowMode.Last)
        {
            var button = cut.FindAll("tbody tr button").Last(i => i.ClassList.Contains("btn-success"));
            await cut.InvokeAsync(() => button.Click());
            Assert.Null(items.Last().Name);
        }
    }


    [Theory]
    [InlineData(InsertRowMode.First)]
    [InlineData(InsertRowMode.Last)]
    public async void Items_EditForm_Add(InsertRowMode insertMode)
    {
        var localizer = Context.Services.GetRequiredService<IStringLocalizer<Foo>>();
        var items = Foo.GenerateFoo(localizer, 2);
        var cut = Context.RenderComponent<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<Table<Foo>>(pb =>
            {
                pb.Add(a => a.Items, items);
                pb.Add(a => a.ItemsChanged, EventCallback.Factory.Create<IEnumerable<Foo>>(this, rows =>
                {
                    items = rows.ToList();
                }));
                pb.Add(a => a.EditMode, EditMode.EditForm);
                pb.Add(a => a.InsertRowMode, insertMode);
                pb.Add(a => a.RenderMode, TableRenderMode.Table);
                pb.Add(a => a.ShowExtendButtons, true);
            });
        });
        var table = cut.FindComponent<Table<Foo>>();
        await cut.InvokeAsync(() => table.Instance.AddAsync());
        Assert.Contains("<form ", table.Markup);
    }

    [Fact]
    public async void Items_Delete()
    {
        var localizer = Context.Services.GetRequiredService<IStringLocalizer<Foo>>();
        var items = Foo.GenerateFoo(localizer, 2);
        var cut = Context.RenderComponent<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<MockTable>(pb =>
            {
                pb.Add(a => a.Items, items);
                pb.Add(a => a.ItemsChanged, EventCallback.Factory.Create<IEnumerable<Foo>>(this, rows =>
                {
                    items = rows.ToList();
                }));
                pb.Add(a => a.EditMode, EditMode.InCell);
                pb.Add(a => a.RenderMode, TableRenderMode.Table);
                pb.Add(a => a.ShowExtendButtons, true);
            });
        });
        var table = cut.FindComponent<MockTable>();
        await cut.InvokeAsync(() => table.Instance.TestDeleteAsync());
        Assert.Equal(localizer["Foo.Name", "0002"], items.First().Name);
    }

    [Fact]
    public void TableColumns_Ok()
    {
        var localizer = Context.Services.GetRequiredService<IStringLocalizer<Foo>>();
        var cut = Context.RenderComponent<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<Table<Foo>>(pb =>
            {
                pb.Add(a => a.Items, Foo.GenerateFoo(localizer));
                pb.Add(a => a.TableColumns, foo => builder =>
                {
                    builder.OpenComponent<TableColumn<Foo, string>>(0);
                    builder.AddAttribute(1, "Field", "Name");
                    builder.AddAttribute(2, "FieldExpression", Utility.GenerateValueExpression(foo, "Name", typeof(string)));
                    builder.CloseComponent();
                });
            });
        });

        cut.Contains("table");
    }

    [Fact]
    public void ShowSkeleton_Ok()
    {
        var localizer = Context.Services.GetRequiredService<IStringLocalizer<Foo>>();
        var cut = Context.RenderComponent<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<Table<Foo>>(pb =>
            {
                pb.Add(a => a.ShowSkeleton, true);
                pb.Add(a => a.Items, Foo.GenerateFoo(localizer));
                pb.Add(a => a.TableColumns, foo => builder =>
                {
                    builder.OpenComponent<TableColumn<Foo, string>>(0);
                    builder.AddAttribute(1, "Field", "Name");
                    builder.AddAttribute(2, "FieldExpression", Utility.GenerateValueExpression(foo, "Name", typeof(string)));
                    builder.CloseComponent();
                });
            });
        });
    }

    [Fact]
    public void ShowLoading_Ok()
    {
        var localizer = Context.Services.GetRequiredService<IStringLocalizer<Foo>>();
        var cut = Context.RenderComponent<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<Table<Foo>>(pb =>
            {
                pb.Add(a => a.ShowLoadingInFirstRender, false);
                pb.Add(a => a.Items, Foo.GenerateFoo(localizer));
                pb.Add(a => a.TableColumns, foo => builder =>
                {
                    builder.OpenComponent<TableColumn<Foo, string>>(0);
                    builder.AddAttribute(1, "Field", "Name");
                    builder.AddAttribute(2, "FieldExpression", Utility.GenerateValueExpression(foo, "Name", typeof(string)));
                    builder.CloseComponent();
                });
            });
        });
    }

    [Fact]
    public async Task ShowSearch_Ok()
    {
        var localizer = Context.Services.GetRequiredService<IStringLocalizer<Foo>>();
        var cut = Context.RenderComponent<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<Table<Foo>>(pb =>
            {
                pb.Add(a => a.ShowSearch, true);
                pb.Add(a => a.Items, Foo.GenerateFoo(localizer));
                pb.Add(a => a.TableColumns, foo => builder =>
                {
                    builder.OpenComponent<TableColumn<Foo, string>>(0);
                    builder.AddAttribute(1, "Field", "Name");
                    builder.AddAttribute(2, "FieldExpression", Utility.GenerateValueExpression(foo, "Name", typeof(string)));
                    builder.CloseComponent();
                });
            });
        });
        cut.Contains("float-end table-toolbar-button btn-group");

        var searchButton = cut.Find(".fa-search");
        await cut.InvokeAsync(() => searchButton.Click());
    }

    [Fact]
    public async Task ShowAdvancedSearch_Ok()
    {
        var localizer = Context.Services.GetRequiredService<IStringLocalizer<Foo>>();
        var cut = Context.RenderComponent<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<Table<Foo>>(pb =>
            {
                pb.Add(a => a.ShowSearch, true);
                pb.Add(a => a.ShowSearchText, false);
                pb.Add(a => a.SearchDialogSize, Size.ExtraExtraLarge);
                pb.Add(a => a.SearchDialogIsDraggable, true);
                pb.Add(a => a.ScrollingDialogContent, true);
                pb.Add(a => a.SearchDialogShowMaximizeButton, true);
                pb.Add(a => a.SearchDialogItemsPerRow, 2);
                pb.Add(a => a.SearchDialogRowType, RowType.Inline);
                pb.Add(a => a.SearchDialogLabelAlign, Alignment.Right);
                pb.Add(a => a.ShowAdvancedSearch, true);
                pb.Add(a => a.ShowUnsetGroupItemsOnTop, true);
                pb.Add(a => a.Items, Foo.GenerateFoo(localizer, 1));
                pb.Add(a => a.TableColumns, foo => builder =>
                {
                    builder.OpenComponent<TableColumn<Foo, string>>(0);
                    builder.AddAttribute(1, "Field", "Name");
                    builder.AddAttribute(2, "FieldExpression", Utility.GenerateValueExpression(foo, "Name", typeof(string)));
                    builder.AddAttribute(3, "Searchable", true);
                    builder.CloseComponent();
                });
            });
        });

        var searchButton = cut.Find(".fa-search-plus");
        await cut.InvokeAsync(() => searchButton.Click());
    }

    [Fact]
    public async Task ShowAdvancedSearch_CustomerModel_Ok()
    {
        var localizer = Context.Services.GetRequiredService<IStringLocalizer<Foo>>();
        var searchModel = new FooSearchModel();
        var cut = Context.RenderComponent<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<Table<Foo>>(pb =>
            {
                pb.Add(a => a.ShowSearch, true);
                pb.Add(a => a.CustomerSearchModel, searchModel);
                pb.Add(a => a.CustomerSearchTemplate, foo => builder => builder.AddContent(0, "test_CustomerSearchTemplate"));
                pb.Add(a => a.ShowAdvancedSearch, true);
                pb.Add(a => a.Items, Foo.GenerateFoo(localizer, 1));
                pb.Add(a => a.TableColumns, foo => builder =>
                {
                    builder.OpenComponent<TableColumn<Foo, string>>(0);
                    builder.AddAttribute(1, "Field", foo.Name);
                    builder.AddAttribute(2, "FieldExpression", Utility.GenerateValueExpression(foo, "Name", typeof(string)));
                    builder.AddAttribute(3, "Searchable", true);
                    builder.CloseComponent();
                });
            });
        });

        var searchButton = cut.Find(".fa-search-plus");
        await cut.InvokeAsync(() => searchButton.Click());
    }

    [Fact]
    public async Task ShowTopSearch_Ok()
    {
        var localizer = Context.Services.GetRequiredService<IStringLocalizer<Foo>>();
        var cut = Context.RenderComponent<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<Table<Foo>>(pb =>
            {
                pb.Add(a => a.ShowSearch, true);
                pb.Add(a => a.SearchMode, SearchMode.Top);
                pb.Add(a => a.OnQueryAsync, OnQueryAsync(localizer));
                pb.Add(a => a.TableColumns, foo => builder =>
                {
                    builder.OpenComponent<TableColumn<Foo, string>>(0);
                    builder.AddAttribute(1, "Field", "Name");
                    builder.AddAttribute(2, "FieldExpression", Utility.GenerateValueExpression(foo, "Name", typeof(string)));
                    builder.AddAttribute(3, "Searchable", true);
                    builder.CloseComponent();
                });
                pb.Add(a => a.TableColumns, foo => builder =>
                {
                    builder.OpenComponent<TableColumn<Foo, int>>(0);
                    builder.AddAttribute(1, "Field", foo.Count);
                    builder.AddAttribute(2, "FieldExpression", Utility.GenerateValueExpression(foo, "Count", typeof(int)));
                    builder.AddAttribute(3, "Searchable", true);
                    builder.CloseComponent();
                });
            });
        });
        cut.Contains("table-search");

        var searchButton = cut.Find(".fa-search");
        await cut.InvokeAsync(() => searchButton.Click());
    }

    [Fact]
    public async Task Search_JSInvoke()
    {
        var localizer = Context.Services.GetRequiredService<IStringLocalizer<Foo>>();
        var cut = Context.RenderComponent<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<Table<Foo>>(pb =>
            {
                pb.Add(a => a.ShowSearch, true);
                pb.Add(a => a.SearchMode, SearchMode.Top);
                pb.Add(a => a.OnQueryAsync, OnQueryAsync(localizer));
                pb.Add(a => a.TableColumns, foo => builder =>
                {
                    builder.OpenComponent<TableColumn<Foo, string>>(0);
                    builder.AddAttribute(1, "Field", "Name");
                    builder.AddAttribute(2, "FieldExpression", Utility.GenerateValueExpression(foo, "Name", typeof(string)));
                    builder.AddAttribute(3, "Searchable", true);
                    builder.CloseComponent();
                });
            });
        });

        var table = cut.FindComponent<Table<Foo>>();
        await cut.InvokeAsync(() => table.Instance.OnSearch());
        await cut.InvokeAsync(() => table.Instance.OnClearSearch());
    }

    [Fact]
    public void ShowToolbar_Ok()
    {
        var localizer = Context.Services.GetRequiredService<IStringLocalizer<Foo>>();
        var cut = Context.RenderComponent<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<Table<Foo>>(pb =>
            {
                pb.Add(a => a.ShowToolbar, true);
                pb.Add(a => a.SearchMode, SearchMode.Top);
                pb.Add(a => a.Items, Foo.GenerateFoo(localizer));
                pb.Add(a => a.TableColumns, foo => builder =>
                {
                    builder.OpenComponent<TableColumn<Foo, string>>(0);
                    builder.AddAttribute(1, "Field", "Name");
                    builder.AddAttribute(2, "FieldExpression", Utility.GenerateValueExpression(foo, "Name", typeof(string)));
                    builder.CloseComponent();
                });
            });
        });
        cut.Contains("float-end table-toolbar-button");
    }

    [Fact]
    public async Task ShowToolbar_IsExcel_Ok()
    {
        var localizer = Context.Services.GetRequiredService<IStringLocalizer<Foo>>();
        var items = Foo.GenerateFoo(localizer, 2);
        var edit = false;
        var cut = Context.RenderComponent<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<Table<Foo>>(pb =>
            {
                pb.Add(a => a.RenderMode, TableRenderMode.Table);
                pb.Add(a => a.IsExcel, true);
                pb.Add(a => a.Items, items);
                pb.Add(a => a.OnSaveAsync, (foo, changedItem) =>
                {
                    edit = true;
                    return Task.FromResult(true);
                });
                pb.Add(a => a.TableColumns, foo => builder =>
                {
                    builder.OpenComponent<TableColumn<Foo, string>>(0);
                    builder.AddAttribute(1, "Field", "Name");
                    builder.AddAttribute(2, "FieldExpression", Utility.GenerateValueExpression(foo, "Name", typeof(string)));
                    builder.CloseComponent();
                });
            });
        });
        cut.Contains("float-end table-toolbar-button");

        // edit
        var input = cut.Find("tbody tr input[type=\"text\"]");
        await cut.InvokeAsync(() => input.Change("test"));
        Assert.True(edit);
    }

    [Fact]
    public async Task ResetFilters_Ok()
    {
        var cut = Context.RenderComponent<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<Table<Foo>>(pb =>
            {
                pb.Add(a => a.Items, new List<Foo>() { new Foo() });
                pb.Add(a => a.RenderMode, TableRenderMode.Table);
                pb.Add(a => a.ShowFilterHeader, true);
                pb.Add(a => a.TableColumns, new RenderFragment<Foo>(foo => builder =>
                {
                    var index = 0;
                    builder.OpenComponent<TableColumn<Foo, string>>(index++);
                    builder.AddAttribute(index++, nameof(TableColumn<Foo, string>.Field), foo.Name);
                    builder.AddAttribute(index++, nameof(TableColumn<Foo, string>.FieldExpression), foo.GenerateValueExpression());
                    builder.AddAttribute(index++, nameof(TableColumn<Foo, string>.Filterable), true);
                    builder.CloseComponent();
                }));
            });
        });
        var filter = cut.FindComponent<BootstrapInput<string>>().Instance;
        await cut.InvokeAsync(() => filter.SetValue("test"));

        var items = cut.FindAll(".dropdown-item");
        IEnumerable<FilterKeyValueAction>? condtions = null;
        await cut.InvokeAsync(() => items[1].Click());
        await cut.InvokeAsync(() => condtions = cut.FindComponent<StringFilter>().Instance.GetFilterConditions());
        Assert.Single(condtions);

        var table = cut.FindComponent<Table<Foo>>().Instance;
        await cut.InvokeAsync(() => table.ResetFilters());

        condtions = null;
        await cut.InvokeAsync(() => condtions = cut.FindComponent<StringFilter>().Instance.GetFilterConditions());
        Assert.Empty(condtions);
    }

    [Fact]
    public async Task ShowColumnList_Ok()
    {
        var show = false;
        var localizer = Context.Services.GetRequiredService<IStringLocalizer<Foo>>();
        var cut = Context.RenderComponent<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<Table<Foo>>(pb =>
            {
                pb.Add(a => a.RenderMode, TableRenderMode.Table);
                pb.Add(a => a.ShowToolbar, true);
                pb.Add(a => a.ShowColumnList, true);
                pb.Add(a => a.ColumnButtonText, "Test_Column_List");
                pb.Add(a => a.Items, Foo.GenerateFoo(localizer, 2));
                pb.Add(a => a.OnColumnVisibleChanged, (colName, visible) =>
                {
                    show = visible;
                    return Task.CompletedTask;
                });
                pb.Add(a => a.TableColumns, foo => builder =>
                {
                    builder.OpenComponent<TableColumn<Foo, string>>(0);
                    builder.AddAttribute(1, "Field", "Name");
                    builder.AddAttribute(2, "FieldExpression", Utility.GenerateValueExpression(foo, "Name", typeof(string)));
                    builder.AddAttribute(3, "Visible", false);
                    builder.CloseComponent();

                    builder.OpenComponent<TableColumn<Foo, string>>(0);
                    builder.AddAttribute(1, "Field", "Address");
                    builder.AddAttribute(2, "FieldExpression", Utility.GenerateValueExpression(foo, "Address", typeof(string)));
                    builder.CloseComponent();
                });
            });
        });
        cut.Contains("Test_Column_List");

        var item = cut.Find(".btn-col .dropdown-item .form-check-input");
        await cut.InvokeAsync(() => item.Click());

        Assert.True(show);
    }

    [Fact]
    public void ShowCardView_Ok()
    {
        var localizer = Context.Services.GetRequiredService<IStringLocalizer<Foo>>();
        var cut = Context.RenderComponent<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<Table<Foo>>(pb =>
            {
                pb.Add(a => a.ShowToolbar, true);
                pb.Add(a => a.ShowCardView, true);
                pb.Add(a => a.CardViewButtonText, "Test_CardView");
                pb.Add(a => a.Items, Foo.GenerateFoo(localizer));
                pb.Add(a => a.TableColumns, foo => builder =>
                {
                    builder.OpenComponent<TableColumn<Foo, string>>(0);
                    builder.AddAttribute(1, "Field", "Name");
                    builder.AddAttribute(2, "FieldExpression", Utility.GenerateValueExpression(foo, "Name", typeof(string)));
                    builder.CloseComponent();
                });
            });
        });
        cut.Contains("Test_CardView");
    }

    [Fact]
    public void ShowExportButton_Ok()
    {
        var localizer = Context.Services.GetRequiredService<IStringLocalizer<Foo>>();
        var cut = Context.RenderComponent<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<Table<Foo>>(pb =>
            {
                pb.Add(a => a.ShowToolbar, true);
                pb.Add(a => a.ShowExportButton, true);
                pb.Add(a => a.ExportButtonText, "Test_Export");
                pb.Add(a => a.Items, Foo.GenerateFoo(localizer));
                pb.Add(a => a.TableColumns, foo => builder =>
                {
                    builder.OpenComponent<TableColumn<Foo, string>>(0);
                    builder.AddAttribute(1, "Field", "Name");
                    builder.AddAttribute(2, "FieldExpression", Utility.GenerateValueExpression(foo, "Name", typeof(string)));
                    builder.CloseComponent();
                });
            });
        });
        cut.Contains("Test_Export");
    }

    [Fact]
    public void ExportButtonDropdownTemplate_Ok()
    {
        var localizer = Context.Services.GetRequiredService<IStringLocalizer<Foo>>();
        var cut = Context.RenderComponent<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<Table<Foo>>(pb =>
            {
                pb.Add(a => a.ShowToolbar, true);
                pb.Add(a => a.ShowExportButton, true);
                pb.Add(a => a.ExportButtonText, "Test_Export");
                pb.Add(a => a.ExportButtonDropdownTemplate, builder =>
                {
                    builder.OpenElement(0, "div");
                    builder.AddContent(1, "test-export-dropdown-item");
                    builder.CloseElement();
                });
                pb.Add(a => a.Items, Foo.GenerateFoo(localizer));
                pb.Add(a => a.TableColumns, foo => builder =>
                {
                    builder.OpenComponent<TableColumn<Foo, string>>(0);
                    builder.AddAttribute(1, "Field", "Name");
                    builder.AddAttribute(2, "FieldExpression", Utility.GenerateValueExpression(foo, "Name", typeof(string)));
                    builder.CloseComponent();
                });
            });
        });
        cut.Contains("test-export-dropdown-item");
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void ShowTopPagination_Ok(bool showTopPagination)
    {
        var localizer = Context.Services.GetRequiredService<IStringLocalizer<Foo>>();
        var cut = Context.RenderComponent<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<Table<Foo>>(pb =>
            {
                pb.Add(a => a.ShowTopPagination, showTopPagination);
                pb.Add(a => a.IsPagination, true);
                pb.Add(a => a.OnQueryAsync, OnQueryAsync(localizer));
                pb.Add(a => a.TableColumns, foo => builder =>
                {
                    builder.OpenComponent<TableColumn<Foo, string>>(0);
                    builder.AddAttribute(1, "Field", "Name");
                    builder.AddAttribute(2, "FieldExpression", Utility.GenerateValueExpression(foo, "Name", typeof(string)));
                    builder.CloseComponent();
                });
            });
        });
        cut.Contains("table-pagination");

        if (showTopPagination)
        {
            cut.Contains("is-top");
        }
    }

    [Fact]
    public async Task PageItemsSource_Ok()
    {
        var localizer = Context.Services.GetRequiredService<IStringLocalizer<Foo>>();
        var cut = Context.RenderComponent<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<Table<Foo>>(pb =>
            {
                pb.Add(a => a.RenderMode, TableRenderMode.Table);
                pb.Add(a => a.HeaderTextWrap, true);
                pb.Add(a => a.PageItemsSource, new int[] { 2, 4, 8 });
                pb.Add(a => a.IsPagination, true);
                pb.Add(a => a.OnQueryAsync, OnQueryAsync(localizer));
                pb.Add(a => a.TableColumns, foo => builder =>
                {
                    builder.OpenComponent<TableColumn<Foo, string>>(0);
                    builder.AddAttribute(1, "Field", "Name");
                    builder.AddAttribute(2, "FieldExpression", Utility.GenerateValueExpression(foo, "Name", typeof(string)));
                    builder.CloseComponent();
                });
            });
        });

        var pager = cut.FindComponent<Pagination>();
        await cut.InvokeAsync(() => pager.Instance.OnPageItemsChanged!.Invoke(4));
        var activePage = cut.Find(".page-item.active");
        Assert.Equal("1", activePage.TextContent);

        await cut.InvokeAsync(() => pager.Instance.OnPageClick!.Invoke(2, 4));
        activePage = cut.Find(".page-item.active");
        Assert.Equal("2", activePage.TextContent);
    }

    [Fact]
    public void PageItemsSource_null()
    {
        var localizer = Context.Services.GetRequiredService<IStringLocalizer<Foo>>();
        var cut = Context.RenderComponent<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<Table<Foo>>(pb =>
            {
                pb.Add(a => a.RenderMode, TableRenderMode.Table);
                pb.Add(a => a.IsPagination, true);
                pb.Add(a => a.OnQueryAsync, OnQueryAsync(localizer));
                pb.Add(a => a.TableColumns, foo => builder =>
                {
                    builder.OpenComponent<TableColumn<Foo, string>>(0);
                    builder.AddAttribute(1, "Field", "Name");
                    builder.AddAttribute(2, "FieldExpression", Utility.GenerateValueExpression(foo, "Name", typeof(string)));
                    builder.CloseComponent();
                });
            });
        });

        var table = cut.FindComponent<Table<Foo>>();
        Assert.Equal(20, table.Instance.PageItemsSource.First());
    }

    [Fact]
    public void PageItems_Ok()
    {
        var localizer = Context.Services.GetRequiredService<IStringLocalizer<Foo>>();
        var cut = Context.RenderComponent<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<Table<Foo>>(pb =>
            {
                pb.Add(a => a.RenderMode, TableRenderMode.Table);
                pb.Add(a => a.IsPagination, true);
                pb.Add(a => a.OnQueryAsync, OnQueryAsync(localizer));
                pb.Add(a => a.PageItems, 20);
                pb.Add(a => a.TableColumns, foo => builder =>
                {
                    builder.OpenComponent<TableColumn<Foo, string>>(0);
                    builder.AddAttribute(1, "Field", "Name");
                    builder.AddAttribute(2, "FieldExpression", Utility.GenerateValueExpression(foo, "Name", typeof(string)));
                    builder.CloseComponent();
                });
            });
        });

        var table = cut.FindComponent<Table<Foo>>();
        Assert.Equal(20, table.Instance.PageItems);
    }

    [Fact]
    public void IsFixedHeader_Ok()
    {
        var localizer = Context.Services.GetRequiredService<IStringLocalizer<Foo>>();
        var cut = Context.RenderComponent<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<Table<Foo>>(pb =>
            {
                pb.Add(a => a.RenderMode, TableRenderMode.Table);
                pb.Add(a => a.IsFixedHeader, true);
                pb.Add(a => a.Height, 200);
                pb.Add(a => a.Items, Foo.GenerateFoo(localizer));
                pb.Add(a => a.TableColumns, foo => builder =>
                {
                    builder.OpenComponent<TableColumn<Foo, string>>(0);
                    builder.AddAttribute(1, "Field", "Name");
                    builder.AddAttribute(2, "FieldExpression", Utility.GenerateValueExpression(foo, "Name", typeof(string)));
                    builder.CloseComponent();
                });
            });
        });
        cut.Contains("table-fixed-header");
        cut.Contains("height: 200px;");
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void FixedExtendButtonsColumn_Ok(bool inHeaderRow)
    {
        var localizer = Context.Services.GetRequiredService<IStringLocalizer<Foo>>();
        var cut = Context.RenderComponent<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<Table<Foo>>(pb =>
            {
                pb.Add(a => a.RenderMode, TableRenderMode.Table);
                pb.Add(a => a.FixedExtendButtonsColumn, true);
                pb.Add(a => a.IsExtendButtonsInRowHeader, inHeaderRow);
                pb.Add(a => a.ShowExtendButtons, true);
                pb.Add(a => a.Items, Foo.GenerateFoo(localizer));
                pb.Add(a => a.TableColumns, foo => builder =>
                {
                    builder.OpenComponent<TableColumn<Foo, string>>(0);
                    builder.AddAttribute(1, "Field", "Name");
                    builder.AddAttribute(2, "FieldExpression", Utility.GenerateValueExpression(foo, "Name", typeof(string)));
                    builder.CloseComponent();
                });
            });
        });
        cut.Contains("overflow-auto");
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void TextEllipsis_Ok(bool ellipsis)
    {
        var localizer = Context.Services.GetRequiredService<IStringLocalizer<Foo>>();
        var cut = Context.RenderComponent<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<Table<Foo>>(pb =>
            {
                pb.Add(a => a.RenderMode, TableRenderMode.Table);
                pb.Add(a => a.Items, Foo.GenerateFoo(localizer, 1));
                pb.Add(a => a.AllowResizing, false);
                pb.Add(a => a.TableColumns, foo => builder =>
                {
                    builder.OpenComponent<TableColumn<Foo, string>>(0);
                    builder.AddAttribute(1, "Field", "Name");
                    builder.AddAttribute(2, "FieldExpression", Utility.GenerateValueExpression(foo, "Name", typeof(string)));
                    builder.AddAttribute(3, "TextEllipsis", ellipsis);
                    builder.CloseComponent();
                });
            });
        });

        if (ellipsis)
        {
            cut.Contains("is-ellips");
        }
        else
        {
            cut.DoesNotContain("is-ellips");
        }
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void AllowResizing_Ok(bool resizing)
    {
        var localizer = Context.Services.GetRequiredService<IStringLocalizer<Foo>>();
        var cut = Context.RenderComponent<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<Table<Foo>>(pb =>
            {
                pb.Add(a => a.RenderMode, TableRenderMode.Table);
                pb.Add(a => a.Items, Foo.GenerateFoo(localizer, 1));
                pb.Add(a => a.AllowResizing, resizing);
                pb.Add(a => a.TableColumns, foo => builder =>
                {
                    builder.OpenComponent<TableColumn<Foo, string>>(0);
                    builder.AddAttribute(1, "Field", "Name");
                    builder.AddAttribute(2, "FieldExpression", Utility.GenerateValueExpression(foo, "Name", typeof(string)));
                    builder.AddAttribute(4, "TextEllipsis", true);
                    builder.CloseComponent();
                });
            });
        });

        if (resizing)
        {
            cut.Contains("is-resizable");
        }
        else
        {
            cut.DoesNotContain("is-resizable");
        }
    }

    [Theory]
    [InlineData(null)]
    [InlineData(100)]
    public void ColWidth_Ok(int? width)
    {
        var localizer = Context.Services.GetRequiredService<IStringLocalizer<Foo>>();
        var cut = Context.RenderComponent<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<Table<Foo>>(pb =>
            {
                pb.Add(a => a.RenderMode, TableRenderMode.Table);
                pb.Add(a => a.Items, Foo.GenerateFoo(localizer, 1));
                pb.Add(a => a.AllowResizing, false);
                pb.Add(a => a.TableColumns, foo => builder =>
                {
                    builder.OpenComponent<TableColumn<Foo, string>>(0);
                    builder.AddAttribute(1, "Field", "Name");
                    builder.AddAttribute(2, "FieldExpression", Utility.GenerateValueExpression(foo, "Name", typeof(string)));
                    builder.AddAttribute(4, "TextEllipsis", true);
                    builder.AddAttribute(4, "Width", width);
                    builder.CloseComponent();
                });
            });
        });

        if (width.HasValue)
        {
            cut.Contains("width: 100px");
        }
        else
        {
            cut.DoesNotContain("width: 100px");
        }
    }

    [Theory]
    [InlineData(true, true)]
    [InlineData(false, true)]
    [InlineData(true, false)]
    [InlineData(false, false)]
    public void ColumnFixed_Ok(bool showExtendButton, bool isFixedHeader)
    {
        var localizer = Context.Services.GetRequiredService<IStringLocalizer<Foo>>();
        var cut = Context.RenderComponent<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<Table<Foo>>(pb =>
            {
                pb.Add(a => a.RenderMode, TableRenderMode.Table);
                pb.Add(a => a.Items, Foo.GenerateFoo(localizer, 2));
                pb.Add(a => a.ShowExtendButtons, showExtendButton);
                pb.Add(a => a.FixedExtendButtonsColumn, true);
                pb.Add(a => a.IsFixedHeader, isFixedHeader);
                pb.Add(a => a.TableColumns, foo => builder =>
                {
                    builder.OpenComponent<TableColumn<Foo, string>>(0);
                    builder.AddAttribute(1, "Field", foo.Name);
                    builder.AddAttribute(2, "FieldExpression", Utility.GenerateValueExpression(foo, "Name", typeof(string)));
                    builder.AddAttribute(3, nameof(TableColumn<Foo, string>.Fixed), true);
                    builder.CloseComponent();

                    builder.OpenComponent<TableColumn<Foo, int>>(4);
                    builder.AddAttribute(5, "Field", foo.Count);
                    builder.AddAttribute(6, "FieldExpression", Utility.GenerateValueExpression(foo, "Count", typeof(int)));
                    builder.AddAttribute(3, nameof(TableColumn<Foo, string>.Fixed), true);
                    builder.AddAttribute(3, "Width", 100);
                    builder.CloseComponent();

                    builder.OpenComponent<TableColumn<Foo, string>>(10);
                    builder.AddAttribute(11, "Field", foo.Address);
                    builder.AddAttribute(12, "FieldExpression", Utility.GenerateValueExpression(foo, "Address", typeof(string)));
                    builder.CloseComponent();

                    builder.OpenComponent<TableColumn<Foo, string>>(10);
                    builder.AddAttribute(11, "Field", foo.Address);
                    builder.AddAttribute(12, "FieldExpression", Utility.GenerateValueExpression(foo, "Address", typeof(string)));
                    builder.AddAttribute(13, nameof(TableColumn<Foo, string>.Fixed), true);
                    builder.CloseComponent();

                    builder.OpenComponent<TableColumn<Foo, string>>(10);
                    builder.AddAttribute(11, "Field", foo.Address);
                    builder.AddAttribute(12, "FieldExpression", Utility.GenerateValueExpression(foo, "Address", typeof(string)));
                    builder.AddAttribute(13, nameof(TableColumn<Foo, string>.Fixed), true);
                    builder.AddAttribute(3, "Width", 100);
                    builder.CloseComponent();

                    builder.OpenComponent<TableColumn<Foo, string>>(10);
                    builder.AddAttribute(11, "Field", foo.Address);
                    builder.AddAttribute(12, "FieldExpression", Utility.GenerateValueExpression(foo, "Address", typeof(string)));
                    builder.AddAttribute(13, nameof(TableColumn<Foo, string>.Fixed), true);
                    builder.CloseComponent();
                });
            });
        });

        cut.Contains("left: 0px;");
        cut.Contains("left: 200px;");
        cut.Contains("left: 500px;");
        if (showExtendButton)
        {
            if (isFixedHeader)
            {
                cut.Contains("right: 236px;");
                cut.Contains("right: 136px;");
                cut.Contains("right: 6px;");
            }
            else
            {
                cut.Contains("right: 230px;");
                cut.Contains("right: 130px;");
                cut.Contains("right: 0px;");
            }
        }
        if (!showExtendButton)
        {
            cut.Contains("right: 100px;");
            cut.Contains("right: 0px;");

            if (isFixedHeader)
            {
                cut.Contains("right: 106px;");
                cut.Contains("right: 6px;");
            }
        }
    }

    [Fact]
    public void ColumnFixed_Null()
    {
        var localizer = Context.Services.GetRequiredService<IStringLocalizer<Foo>>();
        var cut = Context.RenderComponent<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<Table<Foo>>(pb =>
            {
                pb.Add(a => a.RenderMode, TableRenderMode.Table);
                pb.Add(a => a.Items, Foo.GenerateFoo(localizer, 2));
                pb.Add(a => a.TableColumns, foo => builder =>
                {
                    builder.OpenComponent<TableColumn<Foo, string>>(0);
                    builder.AddAttribute(1, "Field", foo.Name);
                    builder.AddAttribute(2, "FieldExpression", Utility.GenerateValueExpression(foo, "Name", typeof(string)));
                    builder.CloseComponent();

                    builder.OpenComponent<TableColumn<Foo, string>>(0);
                    builder.AddAttribute(1, "Field", foo.Name);
                    builder.AddAttribute(2, "FieldExpression", Utility.GenerateValueExpression(foo, "Name", typeof(string)));
                    builder.CloseComponent();

                    builder.OpenComponent<TableColumn<Foo, string>>(0);
                    builder.AddAttribute(1, "Field", foo.Name);
                    builder.AddAttribute(2, "FieldExpression", Utility.GenerateValueExpression(foo, "Name", typeof(string)));
                    builder.CloseComponent();

                    builder.OpenComponent<TableColumn<Foo, string>>(0);
                    builder.AddAttribute(1, "Field", foo.Address);
                    builder.AddAttribute(2, "FieldExpression", Utility.GenerateValueExpression(foo, "Address", typeof(string)));
                    builder.AddAttribute(3, nameof(TableColumn<Foo, string>.Fixed), true);
                    builder.CloseComponent();

                    builder.OpenComponent<TableColumn<Foo, string>>(0);
                    builder.AddAttribute(1, "Field", foo.Address);
                    builder.AddAttribute(2, "FieldExpression", Utility.GenerateValueExpression(foo, "Address", typeof(string)));
                    builder.AddAttribute(3, nameof(TableColumn<Foo, string>.Fixed), true);
                    builder.CloseComponent();
                });
            });
        });
    }

    [Theory]
    [InlineData(TableRenderMode.CardView)]
    [InlineData(TableRenderMode.Table)]
    public void IsMultipleSelect_Ok(TableRenderMode mode)
    {
        var localizer = Context.Services.GetRequiredService<IStringLocalizer<Foo>>();
        var cut = Context.RenderComponent<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<Table<Foo>>(pb =>
            {
                pb.Add(a => a.RenderMode, mode);
                pb.Add(a => a.IsMultipleSelect, true);
                pb.Add(a => a.ShowCheckboxText, true);
                pb.Add(a => a.Items, Foo.GenerateFoo(localizer));
                pb.Add(a => a.TableColumns, foo => builder =>
                {
                    builder.OpenComponent<TableColumn<Foo, string>>(0);
                    builder.AddAttribute(1, "Field", "Name");
                    builder.AddAttribute(2, "FieldExpression", Utility.GenerateValueExpression(foo, "Name", typeof(string)));
                    builder.CloseComponent();
                });
            });
        });
        cut.Contains("checkbox");
    }

    [Theory]
    [InlineData(TableRenderMode.CardView)]
    [InlineData(TableRenderMode.Table)]
    public void ShowLineNo_Ok(TableRenderMode mode)
    {
        var localizer = Context.Services.GetRequiredService<IStringLocalizer<Foo>>();
        var cut = Context.RenderComponent<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<Table<Foo>>(pb =>
            {
                pb.Add(a => a.RenderMode, mode);
                pb.Add(a => a.ShowLineNo, true);
                pb.Add(a => a.LineNoText, "Test_LineNo");
                pb.Add(a => a.Items, Foo.GenerateFoo(localizer));
                pb.Add(a => a.TableColumns, foo => builder =>
                {
                    builder.OpenComponent<TableColumn<Foo, string>>(0);
                    builder.AddAttribute(1, "Field", "Name");
                    builder.AddAttribute(2, "FieldExpression", Utility.GenerateValueExpression(foo, "Name", typeof(string)));
                    builder.CloseComponent();
                });
            });
        });
        cut.Contains("Test_LineNo");
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void IsExtendButtonsInRowHeader_Ok(bool inRowHeader)
    {
        var localizer = Context.Services.GetRequiredService<IStringLocalizer<Foo>>();
        var cut = Context.RenderComponent<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<Table<Foo>>(pb =>
            {
                pb.Add(a => a.ShowExtendButtons, true);
                pb.Add(a => a.IsExtendButtonsInRowHeader, inRowHeader);
                pb.Add(a => a.Items, Foo.GenerateFoo(localizer));
                pb.Add(a => a.TableColumns, foo => builder =>
                {
                    builder.OpenComponent<TableColumn<Foo, string>>(0);
                    builder.AddAttribute(1, "Field", "Name");
                    builder.AddAttribute(2, "FieldExpression", Utility.GenerateValueExpression(foo, "Name", typeof(string)));
                    builder.CloseComponent();
                });
                pb.Add(a => a.RowButtonTemplate, new RenderFragment<Foo>(foo => builder =>
                {
                    builder.AddContent(0, "test-button");
                }));
            });
        });
        cut.Contains("test-button");
    }

    [Fact]
    public void RowButtonTemplate_Ok()
    {
        var localizer = Context.Services.GetRequiredService<IStringLocalizer<Foo>>();
        var cut = Context.RenderComponent<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<Table<Foo>>(pb =>
            {
                pb.Add(a => a.ShowExtendButtons, true);
                pb.Add(a => a.Items, Foo.GenerateFoo(localizer));
                pb.Add(a => a.TableColumns, foo => builder =>
                {
                    builder.OpenComponent<TableColumn<Foo, string>>(0);
                    builder.AddAttribute(1, "Field", "Name");
                    builder.AddAttribute(2, "FieldExpression", Utility.GenerateValueExpression(foo, "Name", typeof(string)));
                    builder.CloseComponent();
                });
            });
        });
    }

    [Fact]
    public void ShowExtendButtons_Table_Ok()
    {
        var localizer = Context.Services.GetRequiredService<IStringLocalizer<Foo>>();
        var cut = Context.RenderComponent<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<Table<Foo>>(pb =>
            {
                pb.Add(a => a.RenderMode, TableRenderMode.Table);
                pb.Add(a => a.ShowExtendButtons, true);
                pb.Add(a => a.IsExtendButtonsInRowHeader, false);
                pb.Add(a => a.Items, Foo.GenerateFoo(localizer));
                pb.Add(a => a.TableColumns, foo => builder =>
                {
                    builder.OpenComponent<TableColumn<Foo, string>>(0);
                    builder.AddAttribute(1, "Field", "Name");
                    builder.AddAttribute(2, "FieldExpression", Utility.GenerateValueExpression(foo, "Name", typeof(string)));
                    builder.CloseComponent();
                });
            });
        });
        cut.Contains("<col width=\"130\"");
    }

    [Fact]
    public void OnCellRender_Ok()
    {
        var localizer = Context.Services.GetRequiredService<IStringLocalizer<Foo>>();
        var cut = Context.RenderComponent<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<Table<Foo>>(pb =>
            {
                pb.Add(a => a.Items, Foo.GenerateFoo(localizer));
                pb.Add(a => a.TableColumns, foo => builder =>
                {
                    builder.OpenComponent<TableColumn<Foo, string>>(0);
                    builder.AddAttribute(1, "Field", "Name");
                    builder.AddAttribute(2, "FieldExpression", Utility.GenerateValueExpression(foo, "Name", typeof(string)));
                    builder.AddAttribute(3, "OnCellRender", new Action<TableCellArgs>(args =>
                    {
                        args.Class = "table-cell-class";
                        args.Value = "Test-Cell";
                        args.Colspan = 1;
                        Assert.Equal("Name", args.ColumnName);
                        Assert.NotNull(args.Row);
                        Assert.Equal(1, args.Colspan);
                    }));
                    builder.CloseComponent();
                });
            });
        });
        cut.Contains("Test-Cell");
        cut.Contains("table-cell-class");
    }

    [Theory]
    [InlineData(TableRenderMode.CardView)]
    [InlineData(TableRenderMode.Table)]
    public void ShowFooter_Ok(TableRenderMode mode)
    {
        var localizer = Context.Services.GetRequiredService<IStringLocalizer<Foo>>();
        var cut = Context.RenderComponent<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<Table<Foo>>(pb =>
            {
                pb.Add(a => a.RenderMode, mode);
                pb.Add(a => a.ShowFooter, true);
                pb.Add(a => a.IsHideFooterWhenNoData, false);
                pb.Add(a => a.Items, Foo.GenerateFoo(localizer));
                pb.Add(a => a.TableColumns, foo => builder =>
                {
                    builder.OpenComponent<TableColumn<Foo, string>>(0);
                    builder.AddAttribute(1, "Field", "Name");
                    builder.AddAttribute(2, "FieldExpression", Utility.GenerateValueExpression(foo, "Name", typeof(string)));
                    builder.CloseComponent();
                });
            });
        });
        if (mode == TableRenderMode.CardView)
        {
            cut.Contains("table-footer");
        }
        else
        {
            cut.Contains("tfoot");
        }
    }

    [Theory]
    [InlineData(TableRenderMode.CardView)]
    [InlineData(TableRenderMode.Table)]
    public void TableFooter_Ok(TableRenderMode mode)
    {
        var localizer = Context.Services.GetRequiredService<IStringLocalizer<Foo>>();
        var cut = Context.RenderComponent<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<Table<Foo>>(pb =>
            {
                pb.Add(a => a.RenderMode, mode);
                pb.Add(a => a.ShowFooter, true);
                pb.Add(a => a.IsHideFooterWhenNoData, false);
                pb.Add(a => a.Items, Foo.GenerateFoo(localizer));
                pb.Add(a => a.TableColumns, foo => builder =>
                {
                    builder.OpenComponent<TableColumn<Foo, string>>(0);
                    builder.AddAttribute(1, "Field", "Name");
                    builder.AddAttribute(2, "FieldExpression", Utility.GenerateValueExpression(foo, "Name", typeof(string)));
                    builder.CloseComponent();
                });
                pb.Add(a => a.TableFooter, foos => builder =>
                {
                    builder.AddContent(0, "table-footer-test");
                });
            });
        });
        cut.Contains("table-footer-test");
    }

    [Fact]
    public void FooterTemplate_Ok()
    {
        var localizer = Context.Services.GetRequiredService<IStringLocalizer<Foo>>();
        var cut = Context.RenderComponent<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<Table<Foo>>(pb =>
            {
                pb.Add(a => a.RenderMode, TableRenderMode.Table);
                pb.Add(a => a.ShowFooter, true);
                pb.Add(a => a.IsHideFooterWhenNoData, false);
                pb.Add(a => a.Items, Foo.GenerateFoo(localizer));
                pb.Add(a => a.TableColumns, foo => builder =>
                {
                    builder.OpenComponent<TableColumn<Foo, string>>(0);
                    builder.AddAttribute(1, "Field", "Name");
                    builder.AddAttribute(2, "FieldExpression", Utility.GenerateValueExpression(foo, "Name", typeof(string)));
                    builder.CloseComponent();
                });
                pb.Add(a => a.FooterTemplate, foos => builder =>
                {
                    builder.AddContent(0, "table-footer-test");
                });
            });
        });
        cut.Contains("table-footer-test");
    }

    [Fact]
    public void Filterable_Ok()
    {
        var localizer = Context.Services.GetRequiredService<IStringLocalizer<Foo>>();
        var cut = Context.RenderComponent<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<Table<Foo>>(pb =>
            {
                pb.Add(a => a.RenderMode, TableRenderMode.Table);
                pb.Add(a => a.ShowFilterHeader, false);
                pb.Add(a => a.Items, Foo.GenerateFoo(localizer));
                pb.Add(a => a.TableColumns, foo => builder =>
                {
                    builder.OpenComponent<TableColumn<Foo, string>>(0);
                    builder.AddAttribute(1, "Field", "Name");
                    builder.AddAttribute(2, "FieldExpression", Utility.GenerateValueExpression(foo, "Name", typeof(string)));
                    builder.AddAttribute(3, "Filterable", true);
                    builder.CloseComponent();
                });
            });
        });
        cut.Contains("table-filter");
    }

    [Fact]
    public async Task CustomerToolbarButton_Ok()
    {
        var clicked = false;
        var clickCallback = false;
        var selected = 0;
        var localizer = Context.Services.GetRequiredService<IStringLocalizer<Foo>>();
        var cut = Context.RenderComponent<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<Table<Foo>>(pb =>
            {
                pb.Add(a => a.ShowToolbar, true);
                pb.Add(a => a.IsMultipleSelect, true);
                pb.Add(a => a.RenderMode, TableRenderMode.Table);
                pb.Add(a => a.Items, Foo.GenerateFoo(localizer));
                pb.Add(a => a.TableColumns, foo => builder =>
                {
                    builder.OpenComponent<TableColumn<Foo, string>>(0);
                    builder.AddAttribute(1, "Field", "Name");
                    builder.AddAttribute(2, "FieldExpression", Utility.GenerateValueExpression(foo, "Name", typeof(string)));
                    builder.CloseComponent();
                });
                pb.Add(a => a.TableToolbarTemplate, builder =>
                {
                    builder.OpenComponent<TableToolbarButton<Foo>>(0);
                    builder.AddAttribute(1, nameof(TableToolbarButton<Foo>.Text), "test");
                    builder.AddAttribute(2, nameof(TableToolbarButton<Foo>.OnClickCallback), new Func<IEnumerable<Foo>, Task>(foos =>
                    {
                        clickCallback = true;
                        return Task.CompletedTask;
                    }));
                    builder.AddAttribute(3, nameof(TableToolbarButton<Foo>.OnClick), EventCallback.Factory.Create<MouseEventArgs>(this, e =>
                    {
                        clicked = true;
                    }));
                    builder.AddAttribute(4, nameof(TableToolbarButton<Foo>.IsEnableWhenSelectedOneRow), true);
                    builder.CloseComponent();

                    builder.OpenComponent<TableToolbarButton<Foo>>(0);
                    builder.AddAttribute(1, nameof(TableToolbarButton<Foo>.Text), "test-async");
                    builder.AddAttribute(2, nameof(TableToolbarButton<Foo>.IsAsync), true);
                    builder.AddAttribute(2, nameof(TableToolbarButton<Foo>.IsShow), true);
                    builder.AddAttribute(3, nameof(TableToolbarButton<Foo>.OnClickCallback), new Func<IEnumerable<Foo>, Task>(foos =>
                    {
                        selected = foos.Count();
                        return Task.CompletedTask;
                    }));
                    builder.CloseComponent();

                    builder.OpenComponent<TableToolbarPopconfirmButton<Foo>>(0);
                    builder.AddAttribute(1, nameof(TableToolbarPopconfirmButton<Foo>.Text), "test-confirm");
                    builder.AddAttribute(2, nameof(TableToolbarPopconfirmButton<Foo>.IsShow), true);
                    builder.CloseComponent();

                    builder.OpenComponent<MockToolbarButton<Foo>>(0);
                    builder.AddAttribute(1, nameof(MockToolbarButton<Foo>.Text), "test-confirm-mock");
                    builder.CloseComponent();
                });
            });
        });

        var button = cut.FindComponents<Button>().First(b => b.Instance.Text == "test");
        await cut.InvokeAsync(() => button.Instance.OnClickWithoutRender!.Invoke());
        Assert.True(clicked);
        Assert.True(clickCallback);

        // 选中一行
        var input = cut.Find("tbody tr input");
        await cut.InvokeAsync(() => input.Click());

        button = cut.FindComponents<Button>().First(b => b.Instance.Text == "test-async");
        await cut.InvokeAsync(async () =>
        {
            await button.Instance.OnClickWithoutRender!.Invoke();
        });
        Assert.Equal(1, selected);
    }

    [Fact]
    public async Task CardViewToolbarButton_Ok()
    {
        var clickCallback = false;
        var localizer = Context.Services.GetRequiredService<IStringLocalizer<Foo>>();
        var cut = Context.RenderComponent<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<Table<Foo>>(pb =>
            {
                pb.Add(a => a.ShowToolbar, true);
                pb.Add(a => a.RenderMode, TableRenderMode.CardView);
                pb.Add(a => a.Items, Foo.GenerateFoo(localizer));
                pb.Add(a => a.TableColumns, foo => builder =>
                {
                    builder.OpenComponent<TableColumn<Foo, string>>(0);
                    builder.AddAttribute(1, "Field", "Name");
                    builder.AddAttribute(2, "FieldExpression", Utility.GenerateValueExpression(foo, "Name", typeof(string)));
                    builder.CloseComponent();
                });
                pb.Add(a => a.TableToolbarTemplate, builder =>
                {
                    builder.OpenComponent<TableToolbarButton<Foo>>(0);
                    builder.AddAttribute(1, nameof(TableToolbarButton<Foo>.Text), "test");
                    builder.AddAttribute(2, nameof(TableToolbarButton<Foo>.OnClickCallback), new Func<IEnumerable<Foo>, Task>(foos =>
                    {
                        clickCallback = true;
                        return Task.CompletedTask;
                    }));
                    builder.CloseComponent();
                });
            });
        });

        var item = cut.FindAll(".dropdown-item").First(i => i.TextContent == "test");
        await cut.InvokeAsync(() => item.Click());
        Assert.True(clickCallback);
    }

    [Fact]
    public void CustomerToolbarButton_Disable()
    {
        var localizer = Context.Services.GetRequiredService<IStringLocalizer<Foo>>();
        var cut = Context.RenderComponent<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<Table<Foo>>(pb =>
            {
                pb.Add(a => a.ShowToolbar, true);
                pb.Add(a => a.RenderMode, TableRenderMode.Table);
                pb.Add(a => a.Items, Foo.GenerateFoo(localizer));
                pb.Add(a => a.TableColumns, foo => builder =>
                {
                    builder.OpenComponent<TableColumn<Foo, string>>(0);
                    builder.AddAttribute(1, "Field", "Name");
                    builder.AddAttribute(2, "FieldExpression", Utility.GenerateValueExpression(foo, "Name", typeof(string)));
                    builder.CloseComponent();
                });
                pb.Add(a => a.TableToolbarTemplate, builder =>
                {
                    builder.OpenComponent<TableToolbarButton<Foo>>(0);
                    builder.AddAttribute(1, nameof(TableToolbarButton<Foo>.Text), "test-async");
                    builder.AddAttribute(2, nameof(TableToolbarButton<Foo>.IsAsync), true);
                    builder.AddAttribute(3, nameof(TableToolbarButton<Foo>.OnClickCallback), new Func<IEnumerable<Foo>, Task>(foos => Task.Delay(2000)));
                    builder.CloseComponent();
                });
            });
        });

        var button = cut.FindComponents<Button>().First(b => b.Instance.Text == "test-async");
        cut.InvokeAsync(() => button.Instance.OnClickWithoutRender!.Invoke());
        var toolbar = cut.FindComponent<TableToolbar<Foo>>();
        toolbar.SetParametersAndRender();
    }

    [Fact]
    public void TableToolbar_Null()
    {
        var cut = Context.RenderComponent<TableToolbarButton<Foo>>();
        Assert.Equal("", cut.Markup);

        var cut1 = Context.RenderComponent<TableToolbarPopconfirmButton<Foo>>();
        Assert.Equal("", cut1.Markup);
    }

    [Fact]
    public void ScrollMode_Ok()
    {
        var localizer = Context.Services.GetRequiredService<IStringLocalizer<Foo>>();
        var cut = Context.RenderComponent<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<Table<Foo>>(pb =>
            {
                pb.Add(a => a.RenderMode, TableRenderMode.Table);
                pb.Add(a => a.ScrollMode, ScrollMode.Virtual);
                pb.Add(a => a.RowHeight, 39.5f);
                pb.Add(a => a.Items, Foo.GenerateFoo(localizer));
                pb.Add(a => a.TableColumns, foo => builder =>
                {
                    builder.OpenComponent<TableColumn<Foo, string>>(0);
                    builder.AddAttribute(1, "Field", "Name");
                    builder.AddAttribute(2, "FieldExpression", Utility.GenerateValueExpression(foo, "Name", typeof(string)));
                    builder.CloseComponent();
                });
            });
        });
    }

    [Fact]
    public void ShowEmpty_Ok()
    {
        var localizer = Context.Services.GetRequiredService<IStringLocalizer<Foo>>();
        var cut = Context.RenderComponent<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<Table<Foo>>(pb =>
            {
                pb.Add(a => a.RenderMode, TableRenderMode.Table);
                pb.Add(a => a.ShowEmpty, true);
                pb.Add(a => a.EmptyText, "test-empty");
                pb.Add(a => a.TableColumns, foo => builder =>
                {
                    builder.OpenComponent<TableColumn<Foo, string>>(0);
                    builder.AddAttribute(1, "Field", "Name");
                    builder.AddAttribute(2, "FieldExpression", Utility.GenerateValueExpression(foo, "Name", typeof(string)));
                    builder.CloseComponent();
                });
            });
        });
        cut.Contains("test-empty");
    }

    [Fact]
    public async Task ScrollMode_Query_Ok()
    {
        var localizer = Context.Services.GetRequiredService<IStringLocalizer<Foo>>();
        var cut = Context.RenderComponent<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<Table<Foo>>(pb =>
            {
                pb.Add(a => a.RenderMode, TableRenderMode.Table);
                pb.Add(a => a.ScrollMode, ScrollMode.Virtual);
                pb.Add(a => a.OnQueryAsync, OnQueryAsync(localizer));
                pb.Add(a => a.TableColumns, foo => builder =>
                {
                    builder.OpenComponent<TableColumn<Foo, string>>(0);
                    builder.AddAttribute(1, "Field", "Name");
                    builder.AddAttribute(2, "FieldExpression", Utility.GenerateValueExpression(foo, "Name", typeof(string)));
                    builder.AddAttribute(3, "Sortable", true);
                    builder.CloseComponent();
                });
            });
        });

        var th = cut.Find("th");
        await cut.InvokeAsync(() => th.Click());
        // desc
        await cut.InvokeAsync(() => th.Click());
        // desc
        await cut.InvokeAsync(() => th.Click());
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public async Task ShowDetail_OK(bool isExcel)
    {
        var showDetail = false;
        var localizer = Context.Services.GetRequiredService<IStringLocalizer<Foo>>();
        var cut = Context.RenderComponent<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<Table<Foo>>(pb =>
            {
                pb.Add(a => a.RenderMode, TableRenderMode.Table);
                pb.Add(a => a.IsDetails, true);
                pb.Add(a => a.IsExcel, isExcel);
                pb.Add(a => a.Items, Foo.GenerateFoo(localizer));
                pb.Add(a => a.TableColumns, foo => builder =>
                {
                    builder.OpenComponent<TableColumn<Foo, string>>(0);
                    builder.AddAttribute(1, "Field", "Name");
                    builder.AddAttribute(2, "FieldExpression", Utility.GenerateValueExpression(foo, "Name", typeof(string)));
                    builder.CloseComponent();
                });
                pb.Add(a => a.DetailRowTemplate, foo => builder =>
                {
                    showDetail = true;
                    builder.AddContent(1, foo.Name);
                });
            });
        });

        cut.Contains("table-cell is-bar");
        var btn = cut.Find(".is-master .is-bar > i");
        await cut.InvokeAsync(() => btn.Click());
        Assert.True(showDetail);
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public async Task RenderEditForm_Ok(bool isExcel)
    {
        var edited = false;
        var localizer = Context.Services.GetRequiredService<IStringLocalizer<Foo>>();
        var cut = Context.RenderComponent<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<Table<Foo>>(pb =>
            {
                pb.Add(a => a.RenderMode, TableRenderMode.Table);
                pb.Add(a => a.EditMode, EditMode.EditForm);
                pb.Add(a => a.ShowToolbar, true);
                pb.Add(a => a.ShowLineNo, true);
                pb.Add(a => a.IsDetails, true);
                pb.Add(a => a.ShowExtendButtons, true);
                pb.Add(a => a.IsExtendButtonsInRowHeader, true);
                pb.Add(a => a.IsExcel, isExcel);
                pb.Add(a => a.Items, Foo.GenerateFoo(localizer));
                pb.Add(a => a.TableColumns, foo => builder =>
                {
                    builder.OpenComponent<TableColumn<Foo, string>>(0);
                    builder.AddAttribute(1, "Field", "Name");
                    builder.AddAttribute(2, "FieldExpression", Utility.GenerateValueExpression(foo, "Name", typeof(string)));
                    builder.CloseComponent();
                });
                pb.Add(a => a.EditTemplate, foo => builder =>
                {
                    edited = true;
                    builder.AddContent(0, "test-edit");
                });
                pb.Add(a => a.DetailRowTemplate, foo => builder =>
                {
                    builder.AddContent(1, foo.Name);
                });
            });
        });
        if (!isExcel)
        {
            var btn = cut.Find(".table-cell .btn-primary");
            await cut.InvokeAsync(() => btn.Click());
            Assert.True(edited);
        }
    }

    [Fact]
    public void MultiHeaderTemplate_Ok()
    {
        var localizer = Context.Services.GetRequiredService<IStringLocalizer<Foo>>();
        var cut = Context.RenderComponent<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<Table<Foo>>(pb =>
            {
                pb.Add(a => a.RenderMode, TableRenderMode.Table);
                pb.Add(a => a.ShowMultiFilterHeader, false);
                pb.Add(a => a.OnQueryAsync, OnQueryAsync(localizer));
                pb.Add(a => a.TableColumns, foo => builder =>
                {
                    builder.OpenComponent<TableColumn<Foo, string>>(0);
                    builder.AddAttribute(1, "Field", "Name");
                    builder.AddAttribute(2, "FieldExpression", Utility.GenerateValueExpression(foo, "Name", typeof(string)));
                    builder.CloseComponent();
                });
                pb.Add(a => a.MultiHeaderTemplate, builder =>
                {
                    builder.AddContent(0, "Test-MultiHeaderTemplate");
                });
            });
        });
        cut.Contains("Test-MultiHeaderTemplate");
    }

    [Fact]
    public void HeaderTemplate_Ok()
    {
        var localizer = Context.Services.GetRequiredService<IStringLocalizer<Foo>>();
        var cut = Context.RenderComponent<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<Table<Foo>>(pb =>
            {
                pb.Add(a => a.RenderMode, TableRenderMode.Table);
                pb.Add(a => a.OnQueryAsync, OnQueryAsync(localizer));
                pb.Add(a => a.TableColumns, foo => builder =>
                {
                    builder.OpenComponent<TableColumn<Foo, string>>(0);
                    builder.AddAttribute(1, "Field", "Name");
                    builder.AddAttribute(2, "FieldExpression", Utility.GenerateValueExpression(foo, "Name", typeof(string)));
                    builder.AddAttribute(3, "HeaderTemplate", new RenderFragment<ITableColumn>(col => builder =>
                    {
                        builder.AddContent(0, $"{col.GetFieldName()}-HeaderTemplate");
                    }));
                    builder.CloseComponent();
                });
            });
        });
        cut.Contains("Name-HeaderTemplate");
    }

    [Fact]
    public async void Sortable_Ok()
    {
        var sorted = false;
        var localizer = Context.Services.GetRequiredService<IStringLocalizer<Foo>>();
        var cut = Context.RenderComponent<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<Table<Foo>>(pb =>
            {
                pb.Add(a => a.RenderMode, TableRenderMode.Table);
                pb.Add(a => a.OnQueryAsync, OnQueryAsync(localizer));
                pb.Add(a => a.SortIconAsc, "fa fa-sort-asc");
                pb.Add(a => a.SortIconDesc, "fa fa-sort-desc");
                pb.Add(a => a.OnSort, (_, _) =>
                {
                    sorted = true;
                    return "";
                });
                pb.Add(a => a.TableColumns, foo => builder =>
                {
                    builder.OpenComponent<TableColumn<Foo, string>>(0);
                    builder.AddAttribute(1, "Field", "Name");
                    builder.AddAttribute(2, "FieldExpression", Utility.GenerateValueExpression(foo, "Name", typeof(string)));
                    builder.AddAttribute(3, "Sortable", true);
                    builder.AddAttribute(4, "DefaultSort", true);
                    builder.CloseComponent();
                });
                pb.Add(a => a.SortIcon, "fa fa-sort");
            });
        });
        cut.Contains("fa fa-sort");

        var th = cut.Find("th");
        await cut.InvokeAsync(() => th.Click());
        Assert.True(sorted);

        var column = cut.FindComponent<TableColumn<Foo, string>>();
        column.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.DefaultSort, true);
        });

        var table = cut.FindComponent<Table<Foo>>();
        table.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.OnSort, null);
        });
        await cut.InvokeAsync(() => table.Instance.QueryAsync());
    }

    [Fact]
    public async Task OnSort_Ok()
    {
        // 外部未排序，组件内部自动排序
        var localizer = Context.Services.GetRequiredService<IStringLocalizer<Foo>>();
        var cut = Context.RenderComponent<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<Table<Foo>>(pb =>
            {
                pb.Add(a => a.RenderMode, TableRenderMode.Table);
                pb.Add(a => a.OnQueryAsync, OnQueryAsync(localizer, isSorted: false));
                pb.Add(a => a.TableColumns, foo => builder =>
                {
                    builder.OpenComponent<TableColumn<Foo, string>>(0);
                    builder.AddAttribute(1, "Field", "Name");
                    builder.AddAttribute(2, "FieldExpression", Utility.GenerateValueExpression(foo, "Name", typeof(string)));
                    builder.AddAttribute(3, "Sortable", true);
                    builder.AddAttribute(4, "DefaultSort", true);
                    builder.AddAttribute(4, "DefaultSortOrder", SortOrder.Desc);
                    builder.CloseComponent();
                });
                pb.Add(a => a.SortIcon, "fa fa-sort");
            });
        });

        var name = cut.Find("td").TextContent;
        Assert.Contains("0005", name);

        // click sort
        var sort = cut.Find("th");
        await cut.InvokeAsync(() => sort.Click());

        name = name = cut.Find("td").TextContent;
        Assert.Contains("0001", name);
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void ShowFilterHeader_Ok(bool showCheckboxText)
    {
        var localizer = Context.Services.GetRequiredService<IStringLocalizer<Foo>>();
        var cut = Context.RenderComponent<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<Table<Foo>>(pb =>
            {
                pb.Add(a => a.RenderMode, TableRenderMode.Table);
                pb.Add(a => a.ShowFilterHeader, true);
                pb.Add(a => a.IsDetails, true);
                pb.Add(a => a.DetailRowTemplate, foo => builder => builder.AddContent(0, foo.Name));
                pb.Add(a => a.IsMultipleSelect, true);
                pb.Add(a => a.ShowLineNo, true);
                pb.Add(a => a.ShowExtendButtons, true);
                pb.Add(a => a.IsExtendButtonsInRowHeader, true);
                pb.Add(a => a.ShowCheckboxText, showCheckboxText);
                pb.Add(a => a.OnQueryAsync, OnQueryAsync(localizer));
                pb.Add(a => a.TableColumns, foo => builder =>
                {
                    builder.OpenComponent<TableColumn<Foo, string>>(0);
                    builder.AddAttribute(1, "Field", "Name");
                    builder.AddAttribute(2, "FieldExpression", Utility.GenerateValueExpression(foo, "Name", typeof(string)));
                    builder.CloseComponent();
                });
            });
        });
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void ShowFilterHeader_ExtendButton_Ok(bool fixedHeader)
    {
        var localizer = Context.Services.GetRequiredService<IStringLocalizer<Foo>>();
        var cut = Context.RenderComponent<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<Table<Foo>>(pb =>
            {
                pb.Add(a => a.RenderMode, TableRenderMode.Table);
                pb.Add(a => a.IsFixedHeader, fixedHeader);
                pb.Add(a => a.ShowFilterHeader, true);
                pb.Add(a => a.ShowExtendButtons, true);
                pb.Add(a => a.OnQueryAsync, OnQueryAsync(localizer));
                pb.Add(a => a.TableColumns, foo => builder =>
                {
                    builder.OpenComponent<TableColumn<Foo, string>>(0);
                    builder.AddAttribute(1, "Field", "Name");
                    builder.AddAttribute(2, "FieldExpression", Utility.GenerateValueExpression(foo, "Name", typeof(string)));
                    builder.CloseComponent();
                });
            });
        });
    }

    [Fact]
    public async Task ShowDetailRow_Ok()
    {
        var localizer = Context.Services.GetRequiredService<IStringLocalizer<Foo>>();
        var cut = Context.RenderComponent<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<Table<Foo>>(pb =>
            {
                pb.Add(a => a.RenderMode, TableRenderMode.Table);
                pb.Add(a => a.IsDetails, true);
                pb.Add(a => a.TreeIcon, "fa-caret-right");
                pb.Add(a => a.ShowDetailRow, foo => true);
                pb.Add(a => a.DetailRowTemplate, foo => builder => builder.AddContent(0, foo.Name));
                pb.Add(a => a.OnQueryAsync, OnQueryAsync(localizer));
                pb.Add(a => a.TableColumns, foo => builder =>
                {
                    builder.OpenComponent<TableColumn<Foo, string>>(0);
                    builder.AddAttribute(1, "Field", "Name");
                    builder.AddAttribute(2, "FieldExpression", Utility.GenerateValueExpression(foo, "Name", typeof(string)));
                    builder.CloseComponent();
                });
            });
        });
        cut.Contains("fa fa-fw fa-caret-right");

        // 点击展开明细行
        var bar = cut.Find("tbody .is-bar i");
        await cut.InvokeAsync(() => bar.Click());
        cut.Contains("fa fa-fw fa-caret-right fa-rotate-90");
        await cut.InvokeAsync(() => bar.Click());
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    [InlineData(null)]
    public void IsDetails_Ok(bool? isDetails)
    {
        var localizer = Context.Services.GetRequiredService<IStringLocalizer<Foo>>();
        var cut = Context.RenderComponent<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<Table<Foo>>(pb =>
            {
                pb.Add(a => a.RenderMode, TableRenderMode.Table);
                pb.Add(a => a.IsDetails, isDetails);
                pb.Add(a => a.DetailRowTemplate, foo => builder => builder.AddContent(0, foo.Name));
                pb.Add(a => a.OnQueryAsync, OnQueryAsync(localizer));
                pb.Add(a => a.TableColumns, foo => builder =>
                {
                    builder.OpenComponent<TableColumn<Foo, string>>(0);
                    builder.AddAttribute(1, "Field", "Name");
                    builder.AddAttribute(2, "FieldExpression", Utility.GenerateValueExpression(foo, "Name", typeof(string)));
                    builder.CloseComponent();
                });
            });
        });

        if (isDetails.HasValue && isDetails.Value == false)
        {
            // 无明细行
            cut.DoesNotContain("is-master");
        }
        else
        {
            cut.Contains("is-master");
        }
    }

    [Fact]
    public void ColSpan_OK()
    {
        var localizer = Context.Services.GetRequiredService<IStringLocalizer<Foo>>();
        var cut = Context.RenderComponent<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<Table<Foo>>(pb =>
            {
                pb.Add(a => a.RenderMode, TableRenderMode.Table);
                pb.Add(a => a.OnQueryAsync, OnQueryAsync(localizer));
                pb.Add(a => a.TableColumns, foo => builder =>
                {
                    builder.OpenComponent<TableColumn<Foo, string>>(0);
                    builder.AddAttribute(1, "Field", "Name");
                    builder.AddAttribute(2, "FieldExpression", Utility.GenerateValueExpression(foo, "Name", typeof(string)));
                    builder.AddAttribute(3, nameof(TableColumn<Foo, string>.OnCellRender), new Action<TableCellArgs>(args =>
                    {
                        args.Colspan = 2;
                        args.Class = "test_cellClass";
                        args.Value = "Test_Value";
                    }));
                    builder.CloseComponent();
                });
                pb.Add(a => a.TableColumns, foo => builder =>
                {
                    builder.OpenComponent<TableColumn<Foo, string>>(0);
                    builder.AddAttribute(1, "Field", "Address");
                    builder.AddAttribute(2, "FieldExpression", Utility.GenerateValueExpression(foo, "Address", typeof(string)));
                    builder.CloseComponent();
                });
                pb.Add(a => a.TableColumns, foo => builder =>
                {
                    builder.OpenComponent<TableColumn<Foo, string>>(0);
                    builder.AddAttribute(1, "Field", "Address");
                    builder.AddAttribute(2, "FieldExpression", Utility.GenerateValueExpression(foo, "Address", typeof(string)));
                    builder.AddAttribute(3, nameof(TableColumn<Foo, string>.OnCellRender), new Action<TableCellArgs>(args =>
                    {
                        args.Class = "test_cellClass";
                        args.Value = "Test_Value";
                    }));
                    builder.CloseComponent();
                });
            });
        });
        cut.Contains("test_cellClass");
    }

    [Fact]
    public void IsTree_Items()
    {
        var localizer = Context.Services.GetRequiredService<IStringLocalizer<Foo>>();
        var cut = Context.RenderComponent<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<Table<FooTree>>(pb =>
            {
                pb.Add(a => a.RenderMode, TableRenderMode.Table);
                pb.Add(a => a.IsTree, true);
                pb.Add(a => a.Items, FooTree.Generate(localizer));
                pb.Add(a => a.TreeNodeConverter, items => BuildTreeAsync(items));
                pb.Add(a => a.TableColumns, foo => builder =>
                {
                    builder.OpenComponent<TableColumn<Foo, string>>(0);
                    builder.AddAttribute(1, "Field", "Name");
                    builder.AddAttribute(2, "FieldExpression", Utility.GenerateValueExpression(foo, "Name", typeof(string)));
                    builder.CloseComponent();
                });
            });
        });
        cut.Contains("is-node");
    }

    [Fact]
    public async void IsTree_OnQuery()
    {
        var localizer = Context.Services.GetRequiredService<IStringLocalizer<Foo>>();
        var cut = Context.RenderComponent<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<Table<FooTree>>(pb =>
            {
                pb.Add(a => a.RenderMode, TableRenderMode.Table);
                pb.Add(a => a.IsTree, true);
                pb.Add(a => a.IsMultipleSelect, true);
                pb.Add(a => a.OnQueryAsync, op => OnQueryAsync(op, localizer));
                pb.Add(a => a.TreeNodeConverter, items => BuildTreeAsync(items));
                pb.Add(a => a.OnTreeExpand, foo => Task.FromResult(FooTree.Generate(localizer, foo.Id, 100).Select(foo => new TableTreeNode<FooTree>(foo))));
                pb.Add(a => a.TableColumns, foo => builder =>
                {
                    builder.OpenComponent<TableColumn<Foo, string>>(0);
                    builder.AddAttribute(1, "Field", "Name");
                    builder.AddAttribute(2, "FieldExpression", Utility.GenerateValueExpression(foo, "Name", typeof(string)));
                    builder.CloseComponent();
                });
            });
        });
        var input = cut.Find("tbody tr input");
        await cut.InvokeAsync(() => input.Click());

        // 点击展开
        var node = cut.Find("tbody .is-tree");
        await cut.InvokeAsync(() => node.Click());
        await cut.InvokeAsync(() => node.Click());

        var table = cut.FindComponent<Table<FooTree>>();
        await cut.InvokeAsync(() => table.Instance.QueryAsync());

        var nodes = cut.FindAll("tbody tr");
        Assert.Equal(4, nodes.Count);
    }

    [Fact]
    public async void IsTree_OnTreeExpand()
    {
        var localizer = Context.Services.GetRequiredService<IStringLocalizer<Foo>>();
        var cut = Context.RenderComponent<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<Table<FooTree>>(pb =>
            {
                pb.Add(a => a.RenderMode, TableRenderMode.Table);
                pb.Add(a => a.IsTree, true);
                pb.Add(a => a.Items, FooTree.Generate(localizer));
                pb.Add(a => a.TreeNodeConverter, items =>
                {
                    var ret = items.Select(i => new TableTreeNode<FooTree>(i) { HasChildren = true });
                    return Task.FromResult(ret);
                });
                pb.Add(a => a.OnTreeExpand, foo => Task.FromResult(FooTree.Generate(localizer, foo.Id, 100).Select(foo => new TableTreeNode<FooTree>(foo))));
                pb.Add(a => a.TableColumns, foo => builder =>
                {
                    builder.OpenComponent<TableColumn<Foo, string>>(0);
                    builder.AddAttribute(1, "Field", "Name");
                    builder.AddAttribute(2, "FieldExpression", Utility.GenerateValueExpression(foo, "Name", typeof(string)));
                    builder.CloseComponent();
                });
            });
        });
        // 点击展开
        var node = cut.Find("tbody .is-tree");
        await cut.InvokeAsync(() => node.Click());
        var nodes = cut.FindAll("tbody tr");
        Assert.Equal(4, nodes.Count);
    }

    [Fact]
    public void IsTree_Exception()
    {
        var localizer = Context.Services.GetRequiredService<IStringLocalizer<Foo>>();
        var cut = Context.RenderComponent<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<Table<FooTree>>(pb =>
            {
                pb.Add(a => a.RenderMode, TableRenderMode.Table);
                pb.Add(a => a.IsTree, true);
                pb.Add(a => a.OnQueryAsync, op => OnQueryAsync(op, localizer));
                pb.Add(a => a.TreeNodeConverter, items => BuildTreeAsync(items));
                pb.Add(a => a.TableColumns, foo => builder =>
                {
                    builder.OpenComponent<TableColumn<Foo, string>>(0);
                    builder.AddAttribute(1, "Field", "Name");
                    builder.AddAttribute(2, "FieldExpression", Utility.GenerateValueExpression(foo, "Name", typeof(string)));
                    builder.CloseComponent();
                });
            });
        });

        // 点击展开
        var node = cut.Find("tbody .table-cell.is-tree");
        Assert.ThrowsAsync<InvalidOperationException>(() => cut.InvokeAsync(() => node.Click()));
    }

    [Fact]
    public void IsTree_TableRowEqualityComparer()
    {
        var cut = Context.RenderComponent<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<MockTreeTable<Cat>>(pb => pb.Add(a => a.ModelEqualityComparer, (x, y) => x.Id == y.Id));
        });

        var table = cut.FindComponent<MockTreeTable<Cat>>();
        var ret = table.Instance.TestComparerItem(new Cat() { Id = 1 }, new Cat() { Id = 1 });
        Assert.True(ret);
    }

    [Fact]
    public void IsTree_KeyAttribute()
    {
        var cut = Context.RenderComponent<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<MockTreeTable<Cat>>(pb => pb.Add(a => a.CustomKeyAttribute, typeof(CatKeyAttribute)));
        });

        var table = cut.FindComponent<MockTreeTable<Cat>>();
        var ret = table.Instance.TestComparerItem(new Cat() { Id = 1 }, new Cat() { Id = 1 });
        Assert.True(ret);
    }

    [Fact]
    public void IsTree_EqualityComparer()
    {
        var cut = Context.RenderComponent<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<MockTreeTable<Dummy>>();
        });

        var table = cut.FindComponent<MockTreeTable<Dummy>>();
        var ret = table.Instance.TestComparerItem(new Dummy() { Id = 1 }, new Dummy() { Id = 1 });
        Assert.True(ret);
    }

    [Fact]
    public void IsTree_Equality()
    {
        var cut = Context.RenderComponent<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<MockTreeTable<Dog>>();
        });

        var table = cut.FindComponent<MockTreeTable<Dog>>();
        var ret = table.Instance.TestComparerItem(new Dog() { Id = 1 }, new Dog() { Id = 1 });
        Assert.True(ret);
    }

    [Fact]
    public async Task IsTree_KeepExpand()
    {
        // 展开树状节点
        // 重新查询后节点依然展开
        var localizer = Context.Services.GetRequiredService<IStringLocalizer<Foo>>();
        var cut = Context.RenderComponent<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<Table<FooTree>>(pb =>
            {
                pb.Add(a => a.RenderMode, TableRenderMode.Table);
                pb.Add(a => a.IsTree, true);
                pb.Add(a => a.OnQueryAsync, op =>
                {
                    var items = FooTree.Generate(localizer);
                    return Task.FromResult(new QueryData<FooTree>()
                    {
                        Items = items
                    });
                });
                pb.Add(a => a.TreeNodeConverter, items =>
                {
                    var ret = items.Select(i => new TableTreeNode<FooTree>(i) { HasChildren = true });
                    return Task.FromResult(ret);
                });
                pb.Add(a => a.OnTreeExpand, foo => Task.FromResult(FooTree.Generate(localizer, foo.Id, 100).Select(foo => new TableTreeNode<FooTree>(foo))));
                pb.Add(a => a.TableColumns, foo => builder =>
                {
                    builder.OpenComponent<TableColumn<Foo, string>>(0);
                    builder.AddAttribute(1, "Field", "Name");
                    builder.AddAttribute(2, "FieldExpression", Utility.GenerateValueExpression(foo, "Name", typeof(string)));
                    builder.CloseComponent();
                });
            });
        });

        var nodes = cut.FindAll("tbody tr");
        Assert.Equal(2, nodes.Count);

        // 点击展开
        var node = cut.Find("tbody .is-tree");
        await cut.InvokeAsync(() => node.Click());
        nodes = cut.FindAll("tbody tr");
        Assert.Equal(4, nodes.Count);

        // 查询
        var table = cut.FindComponent<Table<FooTree>>();
        await cut.InvokeAsync(() => table.Instance.QueryAsync());
        Assert.Contains("is-tree fa fa-fw fa-caret-right fa-rotate-90", cut.Markup);

        nodes = cut.FindAll("tbody tr");
        Assert.Equal(4, nodes.Count);

        table.SetParametersAndRender(pb => pb.Add(a => a.OnTreeExpand, null));
        await Assert.ThrowsAsync<InvalidOperationException>(() => table.Instance.QueryAsync());
    }


    [Fact]
    public async Task IsTree_KeepCollapsed()
    {
        // 收起树状节点
        // 重新查询后节点依然收起
        var localizer = Context.Services.GetRequiredService<IStringLocalizer<Foo>>();
        var cut = Context.RenderComponent<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<Table<FooTree>>(pb =>
            {
                pb.Add(a => a.RenderMode, TableRenderMode.Table);
                pb.Add(a => a.IsTree, true);
                pb.Add(a => a.OnQueryAsync, op =>
                {
                    var items = FooTree.Generate(localizer);
                    items.AddRange(FooTree.Generate(localizer, 1, 100));
                    return Task.FromResult(new QueryData<FooTree>()
                    {
                        Items = items
                    });
                });
                pb.Add(a => a.TreeNodeConverter, items =>
                {
                    var ret = items.Where(i => i.ParentId == 0).Select(i =>
                    {
                        var node = new TableTreeNode<FooTree>(i)
                        {
                            HasChildren = i.Id == 1,
                            IsExpand = i.Id == 1
                        };
                        if (i.Id == 1)
                        {
                            node.Items = items.Where(i => i.ParentId == 1).Select(i => new TableTreeNode<FooTree>(i));
                        }
                        return node;
                    });
                    return Task.FromResult(ret);
                });
                pb.Add(a => a.OnTreeExpand, foo => Task.FromResult(FooTree.Generate(localizer, foo.Id, 100).Select(foo => new TableTreeNode<FooTree>(foo))));
                pb.Add(a => a.TableColumns, foo => builder =>
                {
                    builder.OpenComponent<TableColumn<Foo, string>>(0);
                    builder.AddAttribute(1, "Field", "Name");
                    builder.AddAttribute(2, "FieldExpression", Utility.GenerateValueExpression(foo, "Name", typeof(string)));
                    builder.CloseComponent();
                });
            });
        });

        var nodes = cut.FindAll("tbody tr");
        Assert.Equal(4, nodes.Count);

        // 点击收缩
        var node = cut.Find("tbody .is-tree");
        await cut.InvokeAsync(() => node.Click());
        nodes = cut.FindAll("tbody tr");
        Assert.Equal(2, nodes.Count);

        // 查询
        var table = cut.FindComponent<Table<FooTree>>();
        await cut.InvokeAsync(() => table.Instance.QueryAsync());
        Assert.Contains("is-tree fa fa-fw fa-caret-right", cut.Markup);

        nodes = cut.FindAll("tbody tr");
        Assert.Equal(2, nodes.Count);
    }

    private static Task<QueryData<FooTree>> OnQueryAsync(QueryPageOptions _, IStringLocalizer<Foo> localizer)
    {
        var items = FooTree.Generate(localizer);
        // 生成第一行子项数据集合
        items.AddRange(FooTree.Generate(localizer, 1, 10));

        var data = new QueryData<FooTree>()
        {
            Items = items,
            TotalCount = items.Count,
        };
        return Task.FromResult(data);
    }

    private static Task<IEnumerable<TableTreeNode<FooTree>>> BuildTreeAsync(IEnumerable<FooTree> items)
    {
        // 构造树状数据结构
        var ret = BuildTreeNodes(items, 0);
        return Task.FromResult(ret);

        IEnumerable<TableTreeNode<FooTree>> BuildTreeNodes(IEnumerable<FooTree> items, int parentId)
        {
            var ret = new List<TableTreeNode<FooTree>>();
            ret.AddRange(items.Where(i => i.ParentId == parentId).Select((foo, index) => new TableTreeNode<FooTree>(foo)
            {
                // 此处为示例，假设偶行数据都有子数据
                HasChildren = index % 2 == 0,
                // 如果子项集合有值 则默认展开此节点
                IsExpand = items.Any(i => i.ParentId == foo.Id),
                // 获得子项集合
                Items = BuildTreeNodes(items.Where(i => i.ParentId == foo.Id), foo.Id)
            }));
            return ret;
        }
    }

    [Fact]
    public async void IsTree_OnQuery_NoKey()
    {
        var localizer = Context.Services.GetRequiredService<IStringLocalizer<Foo>>();
        var cut = Context.RenderComponent<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<Table<FooNoKeyTree>>(pb =>
            {
                pb.Add(a => a.RenderMode, TableRenderMode.Table);
                pb.Add(a => a.IsTree, true);
                pb.Add(a => a.OnQueryAsync, OnQueryAsync);
                pb.Add(a => a.TreeNodeConverter, items =>
                {
                    var ret = items.Select(i => new TableTreeNode<FooNoKeyTree>(i));
                    return Task.FromResult(ret);
                });
                pb.Add(a => a.IsMultipleSelect, true);
                pb.Add(a => a.TableColumns, foo => builder =>
                {
                    builder.OpenComponent<TableColumn<Foo, string>>(0);
                    builder.AddAttribute(1, "Field", "Name");
                    builder.AddAttribute(2, "FieldExpression", Utility.GenerateValueExpression(foo, "Name", typeof(string)));
                    builder.CloseComponent();
                });
            });
        });
        cut.Contains("is-node");
        var input = cut.Find("tbody tr input");
        await cut.InvokeAsync(() => input.Click());

        var table = cut.FindComponent<Table<FooNoKeyTree>>();
        await cut.InvokeAsync(() => table.Instance.QueryAsync());

        Task<QueryData<FooNoKeyTree>> OnQueryAsync(QueryPageOptions options)
        {
            var items = FooNoKeyTree.Generate(localizer);
            var data = new QueryData<FooNoKeyTree>()
            {
                Items = items,
                TotalCount = items.Count(),
            };
            return Task.FromResult(data);
        }
    }


    [Fact]
    public async void IsTree_ToggleTreeRow()
    {
        var localizer = Context.Services.GetRequiredService<IStringLocalizer<Foo>>();
        var cut = Context.RenderComponent<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<Table<FooTree>>(pb =>
            {
                pb.Add(a => a.RenderMode, TableRenderMode.Table);
                pb.Add(a => a.IsTree, true);
                pb.Add(a => a.TreeNodeConverter, _ =>
                {
                    var items = FooTree.Generate(localizer);
                    items.AddRange(FooTree.Generate(localizer, 1, 100));
                    items.AddRange(FooTree.Generate(localizer, 2, 100));

                    var ret = BuildTreeNodes(items, 0);
                    return Task.FromResult(ret);
                });
                pb.Add(a => a.TableColumns, foo => builder =>
                {
                    builder.OpenComponent<TableColumn<Foo, string>>(0);
                    builder.AddAttribute(1, "Field", "Name");
                    builder.AddAttribute(2, "FieldExpression", Utility.GenerateValueExpression(foo, "Name", typeof(string)));
                    builder.CloseComponent();
                });
            });
        });

        var nodes = cut.FindAll("tbody tr");
        Assert.Equal(6, nodes.Count);

        // 点击收缩第一个节点
        var node = cut.Find("tbody .table-cell.is-tree");
        await cut.InvokeAsync(() => node.Click());
        nodes = cut.FindAll("tbody tr");
        Assert.Equal(4, nodes.Count);

        // 点击收缩第二个节点
        node = cut.FindAll("tbody .table-cell.is-tree").Skip(1).First();
        await cut.InvokeAsync(() => node.Click());
        nodes = cut.FindAll("tbody tr");
        Assert.Equal(2, nodes.Count);

        // 点击展开第一个节点
        node = cut.Find("tbody .table-cell.is-tree");
        await cut.InvokeAsync(() => node.Click());
        nodes = cut.FindAll("tbody tr");
        Assert.Equal(4, nodes.Count);

        // 点击展开第二个节点
        node = cut.FindAll("tbody .table-cell.is-tree").Skip(1).First();
        await cut.InvokeAsync(() => node.Click());
        nodes = cut.FindAll("tbody tr");
        Assert.Equal(6, nodes.Count);

        IEnumerable<TableTreeNode<FooTree>> BuildTreeNodes(IEnumerable<FooTree> items, int parentId)
        {
            var ret = new List<TableTreeNode<FooTree>>();
            ret.AddRange(items.Where(i => i.ParentId == parentId).Select((foo, index) => new TableTreeNode<FooTree>(foo)
            {
                // 此处为示例，假设偶行数据都有子数据
                HasChildren = parentId == 0,
                // 如果子项集合有值 则默认展开此节点
                IsExpand = items.Any(i => i.ParentId == foo.Id),
                // 获得子项集合
                Items = BuildTreeNodes(items.Where(i => i.ParentId == foo.Id), foo.Id)
            }));
            return ret;
        }
    }

    [Fact]
    public async Task InCell_Ok()
    {
        var localizer = Context.Services.GetRequiredService<IStringLocalizer<Foo>>();
        var cut = Context.RenderComponent<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<Table<Foo>>(pb =>
            {
                pb.Add(a => a.RenderMode, TableRenderMode.Table);
                pb.Add(a => a.EditMode, EditMode.InCell);
                pb.Add(a => a.ShowExtendButtons, true);
                pb.Add(a => a.OnQueryAsync, OnQueryAsync(localizer));
                pb.Add(a => a.TableColumns, foo => builder =>
                {
                    builder.OpenComponent<TableColumn<Foo, string>>(0);
                    builder.AddAttribute(1, "Field", "Name");
                    builder.AddAttribute(2, "FieldExpression", Utility.GenerateValueExpression(foo, "Name", typeof(string)));
                    builder.CloseComponent();
                });
            });
        });
        var input = cut.Find("tbody tr td button");
        await cut.InvokeAsync(() => input.Click());
    }

    [Fact]
    public async Task CustomerSearchTemplate_Ok()
    {
        var searchModel = new FooSearchModel()
        {
            Name = "test_name"
        };
        var localizer = Context.Services.GetRequiredService<IStringLocalizer<Foo>>();
        var cut = Context.RenderComponent<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<Table<Foo>>(pb =>
            {
                pb.Add(a => a.ShowSearch, true);
                pb.Add(a => a.SearchMode, SearchMode.Top);
                pb.Add(a => a.RenderMode, TableRenderMode.Table);
                pb.Add(a => a.CustomerSearchModel, searchModel);
                pb.Add(a => a.CustomerSearchTemplate, foo => builder => builder.AddContent(0, "test_CustomerSearchTemplate"));
                pb.Add(a => a.OnQueryAsync, OnQueryAsync(localizer));
                pb.Add(a => a.TableColumns, foo => builder =>
                {
                    builder.OpenComponent<TableColumn<Foo, string>>(0);
                    builder.AddAttribute(1, "Field", "Name");
                    builder.AddAttribute(2, "FieldExpression", Utility.GenerateValueExpression(foo, "Name", typeof(string)));
                    builder.CloseComponent();
                });
            });
        });

        var resetButton = cut.Find(".fa-trash");
        await cut.InvokeAsync(() => resetButton.Click());
        Assert.Null(searchModel.Name);
    }

    [Fact]
    public void SearchTemplate_Ok()
    {
        var localizer = Context.Services.GetRequiredService<IStringLocalizer<Foo>>();
        var cut = Context.RenderComponent<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<Table<Foo>>(pb =>
            {
                pb.Add(a => a.ShowSearch, true);
                pb.Add(a => a.SearchModel, new Foo());
                pb.Add(a => a.SearchMode, SearchMode.Top);
                pb.Add(a => a.RenderMode, TableRenderMode.Table);
                pb.Add(a => a.SearchTemplate, foo => builder => builder.AddContent(0, "test_SearchTemplate"));
                pb.Add(a => a.OnQueryAsync, OnQueryAsync(localizer));
                pb.Add(a => a.TableColumns, foo => builder =>
                {
                    builder.OpenComponent<TableColumn<Foo, string>>(0);
                    builder.AddAttribute(1, "Field", "Name");
                    builder.AddAttribute(2, "FieldExpression", Utility.GenerateValueExpression(foo, "Name", typeof(string)));
                    builder.CloseComponent();
                });
            });
        });

        cut.Contains("test_SearchTemplate");
    }

    [Fact]
    public async Task SearchTemplate_Null()
    {
        var localizer = Context.Services.GetRequiredService<IStringLocalizer<Foo>>();
        var cut = Context.RenderComponent<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<Table<Foo>>(pb =>
            {
                pb.Add(a => a.ShowSearch, true);
                pb.Add(a => a.SearchModel, new Foo());
                pb.Add(a => a.SearchMode, SearchMode.Top);
                pb.Add(a => a.RenderMode, TableRenderMode.Table);
                pb.Add(a => a.OnQueryAsync, OnQueryAsync(localizer));
                pb.Add(a => a.TableColumns, foo => builder =>
                {
                    builder.OpenComponent<TableColumn<Foo, string>>(0);
                    builder.AddAttribute(1, "Field", "Name");
                    builder.AddAttribute(2, "FieldExpression", Utility.GenerateValueExpression(foo, "Name", typeof(string)));
                    builder.CloseComponent();
                });
            });
        });

        var table = cut.FindComponent<Table<Foo>>();
        table.Instance.SearchModel.Name = "Test";

        var resetButton = cut.Find(".fa-trash");
        await cut.InvokeAsync(() => resetButton.Click());
        Assert.Null(table.Instance.SearchModel.Name);
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void CollapsedTopSearch_Ok(bool collapsed)
    {
        var localizer = Context.Services.GetRequiredService<IStringLocalizer<Foo>>();
        var cut = Context.RenderComponent<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<Table<Foo>>(pb =>
            {
                pb.Add(a => a.ShowSearch, true);
                pb.Add(a => a.SearchMode, SearchMode.Top);
                pb.Add(a => a.RenderMode, TableRenderMode.Table);
                pb.Add(a => a.CollapsedTopSearch, collapsed);
                pb.Add(a => a.OnQueryAsync, OnQueryAsync(localizer));
                pb.Add(a => a.TableColumns, foo => builder =>
                {
                    builder.OpenComponent<TableColumn<Foo, string>>(0);
                    builder.AddAttribute(1, "Field", "Name");
                    builder.AddAttribute(2, "FieldExpression", Utility.GenerateValueExpression(foo, "Name", typeof(string)));
                    builder.CloseComponent();
                });
            });
        });

        if (collapsed)
        {
            cut.DoesNotContain("is-open");
            cut.Contains("display: none;");
        }
        else
        {
            cut.Contains("is-open");
            cut.DoesNotContain("display: none;");
        }
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void ShowSearchTextTooltip_Ok(bool showTooltip)
    {
        var localizer = Context.Services.GetRequiredService<IStringLocalizer<Foo>>();
        var cut = Context.RenderComponent<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<Table<Foo>>(pb =>
            {
                pb.Add(a => a.ShowSearch, true);
                pb.Add(a => a.RenderMode, TableRenderMode.Table);
                pb.Add(a => a.ShowSearchTextTooltip, showTooltip);
                pb.Add(a => a.SearchTooltip, "test_tooltip");
                pb.Add(a => a.OnQueryAsync, OnQueryAsync(localizer));
                pb.Add(a => a.TableColumns, foo => builder =>
                {
                    builder.OpenComponent<TableColumn<Foo, string>>(0);
                    builder.AddAttribute(1, "Field", "Name");
                    builder.AddAttribute(2, "FieldExpression", Utility.GenerateValueExpression(foo, "Name", typeof(string)));
                    builder.CloseComponent();
                });
            });
        });

        if (showTooltip)
        {
            var tooltip = cut.FindComponent<Tooltip>();
            Assert.Equal("test_tooltip", tooltip.Instance.Title);
        }
        else
        {
            cut.DoesNotContain("test_tooltip");
        }
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void ShowResetButton_Ok(bool showResetbutton)
    {
        var localizer = Context.Services.GetRequiredService<IStringLocalizer<Foo>>();
        var cut = Context.RenderComponent<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<Table<Foo>>(pb =>
            {
                pb.Add(a => a.ShowSearch, true);
                pb.Add(a => a.RenderMode, TableRenderMode.Table);
                pb.Add(a => a.ShowResetButton, showResetbutton);
                pb.Add(a => a.ResetSearchButtonText, "test_reset");
                pb.Add(a => a.OnQueryAsync, OnQueryAsync(localizer));
                pb.Add(a => a.TableColumns, foo => builder =>
                {
                    builder.OpenComponent<TableColumn<Foo, string>>(0);
                    builder.AddAttribute(1, "Field", "Name");
                    builder.AddAttribute(2, "FieldExpression", Utility.GenerateValueExpression(foo, "Name", typeof(string)));
                    builder.CloseComponent();
                });
            });
        });

        if (showResetbutton)
        {
            cut.Contains("test_reset");
        }
        else
        {
            cut.DoesNotContain("test_reset");
        }
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void ShowSearchButton_Ok(bool showSearchbutton)
    {
        var localizer = Context.Services.GetRequiredService<IStringLocalizer<Foo>>();
        var cut = Context.RenderComponent<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<Table<Foo>>(pb =>
            {
                pb.Add(a => a.ShowSearch, true);
                pb.Add(a => a.RenderMode, TableRenderMode.Table);
                pb.Add(a => a.ShowSearchButton, showSearchbutton);
                pb.Add(a => a.SearchMode, SearchMode.Top);
                pb.Add(a => a.ShowSearchText, false);
                pb.Add(a => a.SearchButtonText, "test_search");
                pb.Add(a => a.OnQueryAsync, OnQueryAsync(localizer));
                pb.Add(a => a.TableColumns, foo => builder =>
                {
                    builder.OpenComponent<TableColumn<Foo, string>>(0);
                    builder.AddAttribute(1, "Field", "Name");
                    builder.AddAttribute(2, "FieldExpression", Utility.GenerateValueExpression(foo, "Name", typeof(string)));
                    builder.CloseComponent();
                });
            });
        });

        // 仅在 Top 模式下不显示 ShowSearchText 时可切换是否显示搜索按钮
        if (showSearchbutton)
        {
            cut.Contains("test_search");
        }
        else
        {
            cut.DoesNotContain("test_search");
        }
    }

    [Fact]
    public async Task SearchText_Ok()
    {
        var localizer = Context.Services.GetRequiredService<IStringLocalizer<Foo>>();
        var cut = Context.RenderComponent<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<Table<Foo>>(pb =>
            {
                pb.Add(a => a.ShowSearch, true);
                pb.Add(a => a.RenderMode, TableRenderMode.Table);
                pb.Add(a => a.SearchMode, SearchMode.Top);
                pb.Add(a => a.SearchText, "test_search_text");
                pb.Add(a => a.OnQueryAsync, op =>
                {
                    Assert.Equal("test_search_text", op.SearchText);
                    Assert.Equal(1, op.PageIndex);
                    Assert.Equal(0, op.StartIndex);
                    Assert.Equal(20, op.PageItems);
                    Assert.False(op.IsPage);
                    Assert.NotNull(op.SearchModel);
                    return OnQueryAsync(localizer, isSearch: false)(op);
                });
                pb.Add(a => a.TableColumns, foo => builder =>
                {
                    builder.OpenComponent<TableColumn<Foo, string>>(0);
                    builder.AddAttribute(1, "Field", "Name");
                    builder.AddAttribute(2, "FieldExpression", Utility.GenerateValueExpression(foo, "Name", typeof(string)));
                    builder.AddAttribute(3, "Searchable", true);
                    builder.CloseComponent();
                });
                pb.Add(a => a.TableColumns, foo => builder =>
                {
                    builder.OpenComponent<TableColumn<Foo, int>>(0);
                    builder.AddAttribute(1, "Field", foo.Count);
                    builder.AddAttribute(2, "FieldExpression", Utility.GenerateValueExpression(foo, "Count", typeof(int)));
                    builder.AddAttribute(3, "Searchable", true);
                    builder.CloseComponent();
                });
            });
        });

        cut.Contains("test_search_text");

        var searchButton = cut.Find(".fa-search");
        await cut.InvokeAsync(() => searchButton.Click());
    }

    [Fact]
    public async Task ResetSearch_Ok()
    {
        var reset = false;
        var localizer = Context.Services.GetRequiredService<IStringLocalizer<Foo>>();
        var cut = Context.RenderComponent<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<Table<Foo>>(pb =>
            {
                pb.Add(a => a.ShowSearch, true);
                pb.Add(a => a.SearchMode, SearchMode.Top);
                pb.Add(a => a.RenderMode, TableRenderMode.Table);
                pb.Add(a => a.OnQueryAsync, OnQueryAsync(localizer));
                pb.Add(a => a.ShowLoading, true);
                pb.Add(a => a.OnResetSearchAsync, foo =>
                {
                    reset = true;
                    return Task.CompletedTask;
                });
                pb.Add(a => a.TableColumns, foo => builder =>
                {
                    builder.OpenComponent<TableColumn<Foo, string>>(0);
                    builder.AddAttribute(1, "Field", "Name");
                    builder.AddAttribute(2, "FieldExpression", Utility.GenerateValueExpression(foo, "Name", typeof(string)));
                    builder.CloseComponent();
                });
            });
        });

        var resetButton = cut.Find(".fa-trash");
        await cut.InvokeAsync(() => resetButton.Click());

        Assert.True(reset);
    }

    [Fact]
    public async Task ClickToSelect_Ok()
    {
        var clicked = false;
        var localizer = Context.Services.GetRequiredService<IStringLocalizer<Foo>>();
        var items = Foo.GenerateFoo(localizer);
        var cut = Context.RenderComponent<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<Table<Foo>>(pb =>
            {
                pb.Add(a => a.ClickToSelect, true);
                pb.Add(a => a.RenderMode, TableRenderMode.CardView);
                pb.Add(a => a.Items, items);
                pb.Add(a => a.TableColumns, foo => builder =>
                {
                    builder.OpenComponent<TableColumn<Foo, string>>(0);
                    builder.AddAttribute(1, "Field", "Name");
                    builder.AddAttribute(2, "FieldExpression", Utility.GenerateValueExpression(foo, "Name", typeof(string)));
                    builder.CloseComponent();
                });
                pb.Add(a => a.OnClickRowCallback, foo =>
                {
                    clicked = true;
                    return Task.CompletedTask;
                });
            });
        });
        var row = cut.Find(".table-row");
        await cut.InvokeAsync(() => row.Click());
        Assert.True(clicked);

        // 设置 非多选模式
        var table = cut.FindComponent<Table<Foo>>();
        table.SetParametersAndRender(pb => pb.Add(a => a.IsMultipleSelect, true));

        clicked = false;
        await cut.InvokeAsync(() => row.Click());
        Assert.True(clicked);

        clicked = false;
        await cut.InvokeAsync(() => row.Click());
        Assert.True(clicked);

        // 设置 Table 模式
        table.SetParametersAndRender(pb => pb.Add(a => a.RenderMode, TableRenderMode.Table));

        clicked = false;
        row = cut.Find("tbody tr");
        await cut.InvokeAsync(() => row.Click());
        Assert.True(clicked);
    }


    [Fact]
    public async Task DoubleClickRow_Ok()
    {
        var clicked = false;
        var localizer = Context.Services.GetRequiredService<IStringLocalizer<Foo>>();
        var items = Foo.GenerateFoo(localizer);
        var cut = Context.RenderComponent<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<Table<Foo>>(pb =>
            {
                pb.Add(a => a.DoubleClickToEdit, true);
                pb.Add(a => a.RenderMode, TableRenderMode.Table);
                pb.Add(a => a.Items, items);
                pb.Add(a => a.TableColumns, foo => builder =>
                {
                    builder.OpenComponent<TableColumn<Foo, string>>(0);
                    builder.AddAttribute(1, "Field", "Name");
                    builder.AddAttribute(2, "FieldExpression", Utility.GenerateValueExpression(foo, "Name", typeof(string)));
                    builder.CloseComponent();
                });
                pb.Add(a => a.OnDoubleClickRowCallback, foo =>
                {
                    clicked = true;
                    return Task.CompletedTask;
                });
            });
        });
        var row = cut.Find("tbody tr");
        await cut.InvokeAsync(() => row.DoubleClick());
        Assert.True(clicked);
    }

    [Fact]
    public async Task OnFilterClick_Ok()
    {
        var localizer = Context.Services.GetRequiredService<IStringLocalizer<Foo>>();
        var items = Foo.GenerateFoo(localizer, 2);
        var cut = Context.RenderComponent<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<Table<Foo>>(pb =>
            {
                pb.Add(a => a.RenderMode, TableRenderMode.Table);
                pb.Add(a => a.Items, items);
                pb.Add(a => a.TableColumns, foo => builder =>
                {
                    builder.OpenComponent<TableColumn<Foo, string>>(0);
                    builder.AddAttribute(1, "Field", "Name");
                    builder.AddAttribute(2, "FieldExpression", Utility.GenerateValueExpression(foo, "Name", typeof(string)));
                    builder.AddAttribute(3, "Filterable", true);
                    builder.CloseComponent();
                });
            });
        });
        var row = cut.Find(".fa-filter");
        await cut.InvokeAsync(() => row.Click());
        cut.Contains("card table-filter-item shadow show");
    }

    [Fact]
    public void OnFilterClick_Null()
    {
        var localizer = Context.Services.GetRequiredService<IStringLocalizer<Foo>>();
        var items = Foo.GenerateFoo(localizer, 2);
        var cut = Context.RenderComponent<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<MockTable>(pb =>
            {
                pb.Add(a => a.RenderMode, TableRenderMode.Table);
                pb.Add(a => a.Items, items);
                pb.Add(a => a.TableColumns, foo => builder =>
                {
                    builder.OpenComponent<TableColumn<Foo, string>>(0);
                    builder.AddAttribute(1, "Field", "Name");
                    builder.AddAttribute(2, "FieldExpression", Utility.GenerateValueExpression(foo, "Name", typeof(string)));
                    builder.CloseComponent();
                });
            });
        });
        var table = cut.FindComponent<MockTable>();
        table.Instance.OnFilterClick();
    }

    [Fact]
    public async Task SaveAsync_Ok()
    {
        var localizer = Context.Services.GetRequiredService<IStringLocalizer<Foo>>();
        var items = Foo.GenerateFoo(localizer, 2);
        var cut = Context.RenderComponent<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<Table<Foo>>(pb =>
            {
                pb.Add(a => a.RenderMode, TableRenderMode.Table);
                pb.Add(a => a.EditMode, EditMode.EditForm);
                pb.Add(a => a.ShowToolbar, true);
                pb.Add(a => a.ShowExtendButtons, true);
                pb.Add(a => a.Items, items);
                pb.Add(a => a.TableColumns, foo => builder =>
                {
                    builder.OpenComponent<TableColumn<Foo, string>>(0);
                    builder.AddAttribute(1, "Field", "Name");
                    builder.AddAttribute(2, "FieldExpression", Utility.GenerateValueExpression(foo, "Name", typeof(string)));
                    builder.CloseComponent();
                });
            });
        });

        // 点击编辑按钮
        var btn = cut.Find("tr .btn-primary");
        await cut.InvokeAsync(() => btn.Click());

        var btnSave = cut.Find(".form-footer .btn-primary");
        await cut.InvokeAsync(() => btnSave.Click());

        // 卡片模式下点击编辑按钮
        var table = cut.FindComponent<Table<Foo>>();
        table.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.RenderMode, TableRenderMode.CardView);
        });
        btn = cut.Find(".table-row .btn-primary");
        await cut.InvokeAsync(() => btn.Click());
    }

    [Theory]
    [InlineData(".btn-test0")]
    [InlineData(".btn-test1")]
    public async Task OnClickExtensionButton_Ok(string selector)
    {
        var localizer = Context.Services.GetRequiredService<IStringLocalizer<Foo>>();
        var items = Foo.GenerateFoo(localizer, 2);
        var index = 0;
        var cut = Context.RenderComponent<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<Table<Foo>>(pb =>
            {
                pb.Add(a => a.RenderMode, TableRenderMode.Table);
                pb.Add(a => a.EditMode, EditMode.EditForm);
                pb.Add(a => a.ShowToolbar, true);
                pb.Add(a => a.IsMultipleSelect, true);
                pb.Add(a => a.ShowExtendButtons, true);
                pb.Add(a => a.Items, items);
                pb.Add(a => a.TableColumns, foo => builder =>
                {
                    builder.OpenComponent<TableColumn<Foo, string>>(0);
                    builder.AddAttribute(1, "Field", "Name");
                    builder.AddAttribute(2, "FieldExpression", Utility.GenerateValueExpression(foo, "Name", typeof(string)));
                    builder.CloseComponent();
                });

                // <TableCellButton Color="Color.Primary" Icon="fa fa-edit" Text="明细" OnClick="@(() => OnRowButtonClick(context, "明细"))" />
                pb.Add(a => a.RowButtonTemplate, foo => builder =>
                {
                    builder.OpenComponent<TableCellButton>(0);
                    builder.AddAttribute(2, "Text", "test-extend-button");
                    builder.AddAttribute(3, "class", $"btn-test{index++}");
                    builder.CloseComponent();
                });
                pb.Add(a => a.BeforeRowButtonTemplate, foo => builder =>
                {
                    builder.OpenComponent<TableCellButton>(0);
                    builder.AddAttribute(2, "Text", "test-extend-button");
                    builder.AddAttribute(3, "class", $"btn-test{index++}");
                    builder.CloseComponent();
                });
            });
        });

        var btn = cut.Find(selector);
        await cut.InvokeAsync(() => btn.Click());
    }

    [Fact]
    public async Task OnClickExtensionButton_InRowHeader_Ok()
    {
        var localizer = Context.Services.GetRequiredService<IStringLocalizer<Foo>>();
        var items = Foo.GenerateFoo(localizer, 2);
        var cut = Context.RenderComponent<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<Table<Foo>>(pb =>
            {
                pb.Add(a => a.RenderMode, TableRenderMode.CardView);
                pb.Add(a => a.EditMode, EditMode.EditForm);
                pb.Add(a => a.IsExtendButtonsInRowHeader, true);
                pb.Add(a => a.ShowToolbar, true);
                pb.Add(a => a.ShowExtendButtons, true);
                pb.Add(a => a.Items, items);
                pb.Add(a => a.TableColumns, foo => builder =>
                {
                    builder.OpenComponent<TableColumn<Foo, string>>(0);
                    builder.AddAttribute(1, "Field", "Name");
                    builder.AddAttribute(2, "FieldExpression", Utility.GenerateValueExpression(foo, "Name", typeof(string)));
                    builder.CloseComponent();
                });

                // <TableCellButton Color="Color.Primary" Icon="fa fa-edit" Text="明细" OnClick="@(() => OnRowButtonClick(context, "明细"))" />
                pb.Add(a => a.RowButtonTemplate, foo => builder =>
                {
                    builder.OpenComponent<TableCellButton>(0);
                    builder.AddAttribute(2, "Text", "test-extend-button");
                    builder.CloseComponent();
                });
            });
        });

        var btn = cut.FindComponent<TableExtensionButton>();
        await cut.InvokeAsync(() => btn.Instance.OnClickButton!(new TableCellButtonArgs() { AutoRenderTableWhenClick = true, AutoSelectedRowWhenClick = true }));
    }

    [Fact]
    public async Task AutoSelectedRowWhenClick_Ok()
    {
        var localizer = Context.Services.GetRequiredService<IStringLocalizer<Foo>>();
        var items = Foo.GenerateFoo(localizer, 2);
        var cut = Context.RenderComponent<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<Table<Foo>>(pb =>
            {
                pb.Add(a => a.RenderMode, TableRenderMode.Table);
                pb.Add(a => a.ShowToolbar, true);
                pb.Add(a => a.ShowExtendButtons, true);
                pb.Add(a => a.Items, items);
                pb.Add(a => a.TableColumns, foo => builder =>
                {
                    builder.OpenComponent<TableColumn<Foo, string>>(0);
                    builder.AddAttribute(1, "Field", "Name");
                    builder.AddAttribute(2, "FieldExpression", Utility.GenerateValueExpression(foo, "Name", typeof(string)));
                    builder.CloseComponent();
                });

                // <TableCellButton Color="Color.Primary" Icon="fa fa-edit" Text="明细" OnClick="@(() => OnRowButtonClick(context, "明细"))" />
                pb.Add(a => a.RowButtonTemplate, foo => builder =>
                {
                    builder.OpenComponent<TableCellButton>(0);
                    builder.AddAttribute(1, "Text", "test-extend-button");
                    builder.AddAttribute(2, "AutoSelectedRowWhenClick", true);
                    builder.AddAttribute(3, "AutoRenderTableWhenClick", false);
                    builder.AddAttribute(4, "IsShow", true);
                    builder.CloseComponent();
                });
            });
        });

        var btn = cut.FindComponent<TableExtensionButton>();
        await cut.InvokeAsync(() => btn.Instance.OnClickButton!(new TableCellButtonArgs() { AutoRenderTableWhenClick = true, AutoSelectedRowWhenClick = true }));
    }

    [Fact]
    public async Task TableCellButton_Ok()
    {
        var clicked = false;
        var clicked2 = false;
        var cut1 = Context.RenderComponent<TableExtensionButton>(pb =>
        {
            pb.Add(a => a.ChildContent, builder =>
            {
                builder.OpenComponent<TableCellButton>(0);
                builder.AddAttribute(1, "OnClick", EventCallback.Factory.Create<MouseEventArgs>(this, () =>
                {
                    clicked = true;
                }));
                builder.AddAttribute(1, "OnClickWithoutRender", () =>
                {
                    clicked2 = true;
                    return Task.CompletedTask;
                });
                builder.CloseComponent();
            });
        });
        var btn = cut1.Find("button");
        await cut1.InvokeAsync(() => btn.Click());
        Assert.True(clicked);
        Assert.True(clicked2);
    }

    [Fact]
    public void TableCellButton_Null()
    {
        var cut = Context.RenderComponent<TableCellButton>();
        Assert.Equal("", cut.Markup);
    }

    [Fact]
    public void TableCellNormalButton_Ok()
    {
        var cut = Context.RenderComponent<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<TableExtensionButton>(pb =>
            {
                pb.Add(a => a.ChildContent, builder =>
                {
                    builder.OpenComponent<MockButton>(0);
                    builder.CloseComponent();
                });
            });
        });
    }

    [Fact]
    public async Task HeaderCheckbox_Ok()
    {
        var localizer = Context.Services.GetRequiredService<IStringLocalizer<Foo>>();
        var items = Foo.GenerateFoo(localizer, 2);
        var cut = Context.RenderComponent<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<Table<Foo>>(pb =>
            {
                pb.Add(a => a.RenderMode, TableRenderMode.Table);
                pb.Add(a => a.IsMultipleSelect, true);
                pb.Add(a => a.Items, items);
                pb.Add(a => a.TableColumns, foo => builder =>
                {
                    builder.OpenComponent<TableColumn<Foo, string>>(0);
                    builder.AddAttribute(1, "Field", "Name");
                    builder.AddAttribute(2, "FieldExpression", Utility.GenerateValueExpression(foo, "Name", typeof(string)));
                    builder.CloseComponent();
                });
            });
        });

        var btn = cut.Find("thead tr th input");
        await cut.InvokeAsync(() => btn.Click());

        var checkboxs = cut.FindAll(".is-checked");
        Assert.Equal(3, checkboxs.Count);

        await cut.InvokeAsync(() => btn.Click());
        checkboxs = cut.FindAll(".is-checked");
        Assert.Equal(0, checkboxs.Count);

        var table = cut.FindComponent<Table<Foo>>();
        table.SetParametersAndRender(pb => pb.Add(a => a.Items, Array.Empty<Foo>()));
        btn = cut.Find("thead tr th input");
        await cut.InvokeAsync(() => btn.Click());
    }

    [Fact]
    public async Task RowCheckbox_Ok()
    {
        var localizer = Context.Services.GetRequiredService<IStringLocalizer<Foo>>();
        var items = Foo.GenerateFoo(localizer, 2);
        var cut = Context.RenderComponent<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<Table<Foo>>(pb =>
            {
                pb.Add(a => a.RenderMode, TableRenderMode.Table);
                pb.Add(a => a.IsMultipleSelect, true);
                pb.Add(a => a.Items, items);
                pb.Add(a => a.TableColumns, foo => builder =>
                {
                    builder.OpenComponent<TableColumn<Foo, string>>(0);
                    builder.AddAttribute(1, "Field", "Name");
                    builder.AddAttribute(2, "FieldExpression", Utility.GenerateValueExpression(foo, "Name", typeof(string)));
                    builder.CloseComponent();
                });
            });
        });

        var btn = cut.Find("tbody tr td input");
        await cut.InvokeAsync(() => btn.Click());

        var checkboxs = cut.FindAll(".is-checked");
        Assert.Equal(1, checkboxs.Count);

        await cut.InvokeAsync(() => btn.Click());
        checkboxs = cut.FindAll(".is-checked");
        Assert.Equal(0, checkboxs.Count);
    }

    [Fact]
    public void TableColumn_Ok()
    {
        var localizer = Context.Services.GetRequiredService<IStringLocalizer<Foo>>();
        var items = Foo.GenerateFoo(localizer, 2);
        var cut = Context.RenderComponent<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<Table<Foo>>(pb =>
            {
                pb.Add(a => a.RenderMode, TableRenderMode.Table);
                pb.Add(a => a.Items, items);
                pb.Add(a => a.TableColumns, foo => builder =>
                {
                    builder.OpenComponent<TableColumn<Foo, string>>(0);
                    builder.AddAttribute(1, "Field", "Name");
                    builder.AddAttribute(2, "FieldExpression", Utility.GenerateValueExpression(foo, "Name", typeof(string)));
                    builder.AddAttribute(3, "ComponentType", typeof(BootstrapInput<string>));
                    builder.CloseComponent();
                });
            });
        });
        var column = cut.FindComponent<TableColumn<Foo, string>>();
        Assert.Equal("Name", column.Instance.Field);
    }

    [Fact]
    public void TableColumn_DefaultSort()
    {
        var localizer = Context.Services.GetRequiredService<IStringLocalizer<Foo>>();
        var items = Foo.GenerateFoo(localizer, 2);
        var cut = Context.RenderComponent<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<Table<Foo>>(pb =>
            {
                pb.Add(a => a.RenderMode, TableRenderMode.Table);
                pb.Add(a => a.Items, items);
                pb.Add(a => a.TableColumns, foo => builder =>
                {
                    builder.OpenComponent<TableColumn<Foo, string>>(0);
                    builder.AddAttribute(1, "Field", "Name");
                    builder.AddAttribute(2, "FieldExpression", Utility.GenerateValueExpression(foo, "Name", typeof(string)));
                    builder.AddAttribute(3, "DefaultSort", true);
                    builder.CloseComponent();
                });
            });
        });
        var column = cut.FindComponent<TableColumn<Foo, string>>();
        Assert.True(column.Instance.DefaultSort);
    }

    [Fact]
    public void TableColumn_TextWrap()
    {
        var localizer = Context.Services.GetRequiredService<IStringLocalizer<Foo>>();
        var items = Foo.GenerateFoo(localizer, 2);
        var cut = Context.RenderComponent<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<Table<Foo>>(pb =>
            {
                pb.Add(a => a.RenderMode, TableRenderMode.Table);
                pb.Add(a => a.Items, items);
                pb.Add(a => a.TableColumns, foo => builder =>
                {
                    builder.OpenComponent<TableColumn<Foo, string>>(0);
                    builder.AddAttribute(1, "Field", "Name");
                    builder.AddAttribute(2, "FieldExpression", Utility.GenerateValueExpression(foo, "Name", typeof(string)));
                    builder.AddAttribute(3, "TextWrap", true);
                    builder.CloseComponent();
                });
            });
        });
        var column = cut.FindComponent<TableColumn<Foo, string>>();
        cut.Contains("table-cell is-wrap");
    }

    [Fact]
    public void TableColumn_ShowLabelTooltip()
    {
        var localizer = Context.Services.GetRequiredService<IStringLocalizer<Foo>>();
        var items = Foo.GenerateFoo(localizer, 2);
        var cut = Context.RenderComponent<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<Table<Foo>>(pb =>
            {
                pb.Add(a => a.RenderMode, TableRenderMode.Table);
                pb.Add(a => a.Items, items);
                pb.Add(a => a.TableColumns, foo => builder =>
                {
                    builder.OpenComponent<TableColumn<Foo, string>>(0);
                    builder.AddAttribute(1, "Field", "Name");
                    builder.AddAttribute(2, "FieldExpression", Utility.GenerateValueExpression(foo, "Name", typeof(string)));
                    builder.AddAttribute(3, "ShowLabelTooltip", true);
                    builder.CloseComponent();
                });
            });
        });
        var column = cut.FindComponent<TableColumn<Foo, string>>();
        Assert.True(column.Instance.ShowLabelTooltip);
    }

    [Fact]
    public async Task TableColumn_DefaultSortOrder()
    {
        var localizer = Context.Services.GetRequiredService<IStringLocalizer<Foo>>();
        var cut = Context.RenderComponent<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<Table<Foo>>(pb =>
            {
                pb.Add(a => a.RenderMode, TableRenderMode.Table);
                pb.Add(a => a.OnQueryAsync, op =>
                {
                    return OnQueryAsync(localizer, isSorted: false)(op);
                });
                pb.Add(a => a.TableColumns, foo => builder =>
                {
                    builder.OpenComponent<TableColumn<Foo, string>>(0);
                    builder.AddAttribute(1, "Field", "Name");
                    builder.AddAttribute(2, "FieldExpression", Utility.GenerateValueExpression(foo, "Name", typeof(string)));
                    builder.AddAttribute(3, "Sortable", true);
                    builder.AddAttribute(4, "DefaultSortOrder", SortOrder.Asc);
                    builder.CloseComponent();
                });
            });
        });
        var column = cut.FindComponent<TableColumn<Foo, string>>();
        Assert.Equal(SortOrder.Asc, column.Instance.DefaultSortOrder);

        // query
        var th = cut.Find("table thead th");
        await cut.InvokeAsync(() => th.Click());
    }

    [Fact]
    public void TableColumn_Editable()
    {
        var localizer = Context.Services.GetRequiredService<IStringLocalizer<Foo>>();
        var items = Foo.GenerateFoo(localizer, 2);
        var cut = Context.RenderComponent<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<Table<Foo>>(pb =>
            {
                pb.Add(a => a.RenderMode, TableRenderMode.Table);
                pb.Add(a => a.Items, items);
                pb.Add(a => a.TableColumns, foo => builder =>
                {
                    builder.OpenComponent<TableColumn<Foo, string>>(0);
                    builder.AddAttribute(1, "Field", "Name");
                    builder.AddAttribute(2, "FieldExpression", Utility.GenerateValueExpression(foo, "Name", typeof(string)));
                    builder.AddAttribute(3, "Editable", true);
                    builder.CloseComponent();
                });
            });
        });
        var column = cut.FindComponent<TableColumn<Foo, string>>();
        Assert.True(column.Instance.Editable);
    }

    [Fact]
    public void TableColumn_Property()
    {
        var localizer = Context.Services.GetRequiredService<IStringLocalizer<Foo>>();
        var items = Foo.GenerateFoo(localizer, 2);
        var cut = Context.RenderComponent<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<Table<Foo>>(pb =>
            {
                pb.Add(a => a.RenderMode, TableRenderMode.Table);
                pb.Add(a => a.Items, items);
                pb.Add(a => a.TableColumns, foo => builder =>
                {
                    builder.OpenComponent<TableColumn<Foo, string>>(0);
                    builder.AddAttribute(1, "Field", "Name");
                    builder.AddAttribute(2, "FieldExpression", Utility.GenerateValueExpression(foo, "Name", typeof(string)));
                    builder.AddAttribute(3, "Readonly", true);
                    builder.AddAttribute(4, "IsReadonlyWhenAdd", true);
                    builder.AddAttribute(5, "IsReadonlyWhenEdit", true);
                    builder.AddAttribute(6, "SkipValidate", true);
                    builder.AddAttribute(7, "Text", "test");
                    builder.AddAttribute(8, "Visible", true);
                    builder.AddAttribute(9, "ShowTips", true);
                    builder.AddAttribute(10, "CssClass", "test");
                    builder.AddAttribute(11, "Align", Alignment.Right);
                    builder.AddAttribute(12, "FormatString", "test");
                    builder.AddAttribute(13, "Formatter", new Func<object?, Task<string>>(obj => Task.FromResult("test-Formatter")));
                    builder.AddAttribute(14, "Step", "1");
                    builder.AddAttribute(15, "Template", new RenderFragment<TableColumnContext<Foo, string>>(context => builder =>
                    {

                    }));
                    builder.AddAttribute(16, "Rows", 1);
                    builder.AddAttribute(17, "EditTemplate", new RenderFragment<Foo>(context => builder =>
                    {

                    }));
                    builder.AddAttribute(18, "SearchTemplate", new RenderFragment<Foo>(context => builder =>
                    {

                    }));
                    builder.AddAttribute(19, "FilterTemplate", new RenderFragment(builder =>
                    {

                    }));
                    builder.AddAttribute(20, "ShownWithBreakPoint", BreakPoint.ExtraSmall);
                    builder.AddAttribute(21, "Items", Array.Empty<SelectedItem>());
                    builder.AddAttribute(22, "Order", 2);
                    builder.AddAttribute(23, "Lookup", Array.Empty<SelectedItem>());
                    builder.AddAttribute(24, "LookupStringComparison", StringComparison.OrdinalIgnoreCase);
                    builder.AddAttribute(25, "LookupServiceKey", "test");
                    builder.AddAttribute(26, "ValidateRules", new List<IValidator>());
                    builder.AddAttribute(27, "GroupName", "test");
                    builder.AddAttribute(28, "GroupOrder", 1);
                    builder.CloseComponent();
                });
            });
        });
        var column = cut.FindComponent<TableColumn<Foo, string>>();
        Assert.True(column.Instance.Readonly);
        Assert.True(column.Instance.IsReadonlyWhenAdd);
        Assert.True(column.Instance.IsReadonlyWhenEdit);
        Assert.True(column.Instance.SkipValidate);
        Assert.Equal("test", column.Instance.Text);
        Assert.True(column.Instance.Visible);
        Assert.True(column.Instance.ShowTips);
        Assert.Equal("test", column.Instance.CssClass);
        Assert.Equal(Alignment.Right, column.Instance.Align);
        Assert.Equal("test", column.Instance.FormatString);
        Assert.NotNull(column.Instance.Formatter);
        Assert.NotNull(column.Instance.Step);
        Assert.NotNull(column.Instance.Template);
        Assert.Equal(1, column.Instance.Rows);
        Assert.NotNull(column.Instance.EditTemplate);
        Assert.NotNull(column.Instance.SearchTemplate);
        Assert.NotNull(column.Instance.FilterTemplate);
        Assert.Equal(BreakPoint.ExtraSmall, column.Instance.ShownWithBreakPoint);
        Assert.NotNull(column.Instance.Items);
        Assert.Equal(2, column.Instance.Order);
        Assert.NotNull(column.Instance.Lookup);
        Assert.Equal(StringComparison.OrdinalIgnoreCase, column.Instance.LookupStringComparison);
        Assert.Equal("test", column.Instance.LookupServiceKey);
        Assert.NotNull(column.Instance.ValidateRules);
        Assert.Equal("test", column.Instance.GroupName);
        Assert.Equal(1, column.Instance.GroupOrder);

        var col = column.Instance as ITableColumn;
        Assert.NotNull(col.Template);
        Assert.NotNull(col.EditTemplate);
        Assert.NotNull(col.SearchTemplate);

        col.Template = null;
        col.EditTemplate = null;
        col.SearchTemplate = null;
    }

    [Fact]
    public void TableColumn_GetDisplayName()
    {
        var cut = Context.RenderComponent<TableColumn<Foo, string>>(pb =>
        {
            pb.Add(a => a.Text, null);
            pb.Add(a => a.FieldName, null);
        });
        Assert.Equal("", cut.Instance.GetDisplayName());

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.FieldName, "FieldName");
        });
        Assert.Equal("FieldName", cut.Instance.GetDisplayName());
    }

    [Fact]
    public void TableColumn_EditTemplate()
    {
        var cut = Context.RenderComponent<TableColumn<Foo, string>>(pb =>
        {
            pb.Add(a => a.EditTemplate, new RenderFragment<Foo>(foo => builder =>
            {
                builder.OpenElement(0, "div");
                builder.AddContent(1, foo.Name);
                builder.CloseElement();
            }));
        });

        var col = cut.Instance as ITableColumn;
        Assert.NotNull(col.EditTemplate);

        var cut1 = Context.Render(new RenderFragment(builder =>
        {
            builder.OpenElement(0, "div");
            builder.AddContent(1, col.EditTemplate!.Invoke(new Foo() { Name = "test-table-column" }));
            builder.CloseElement();
        }));
        Assert.Contains("test-table-column", cut1.Markup);
    }

    [Fact]
    public void TableColumn_SearchTemplate()
    {
        var cut = Context.RenderComponent<TableColumn<Foo, string>>(pb =>
        {
            pb.Add(a => a.SearchTemplate, new RenderFragment<Foo>(foo => builder =>
            {
                builder.OpenElement(0, "div");
                builder.AddContent(1, foo.Name);
                builder.CloseElement();
            }));
        });

        var col = cut.Instance as ITableColumn;
        Assert.NotNull(col.SearchTemplate);

        var cut1 = Context.Render(new RenderFragment(builder =>
        {
            builder.OpenElement(0, "div");
            builder.AddContent(1, col.SearchTemplate!.Invoke(new Foo() { Name = "test-table-column" }));
            builder.CloseElement();
        }));
        Assert.Contains("test-table-column", cut1.Markup);
    }

    [Fact]
    public void TableColumn_GetFieldName()
    {
        var cut = Context.RenderComponent<TableColumn<Foo, string>>(pb =>
        {
            pb.Add(a => a.FieldName, "Name");
        });
        var col = cut.Instance;
        var v = col.GetFieldName();
        Assert.Equal("Name", v);

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.FieldName, "");
            pb.Add(a => a.Field, "Name");
            pb.Add(a => a.FieldExpression, Utility.GenerateValueExpression(new Foo(), "Name", typeof(string)));
        });
        v = col.GetFieldName();
        Assert.Equal("", v);
    }

    [Fact]
    public void TableColumn_ComplexObject()
    {
        var cut = Context.RenderComponent<TableColumn<MockComplexFoo, string>>(pb =>
        {
            pb.Add(a => a.Field, "Foo.Name");
            pb.Add(a => a.FieldExpression, Utility.GenerateValueExpression(new MockComplexFoo(), "Foo.Name", typeof(string)));
        });
        var col = cut.Instance;
        var v = col.GetFieldName();
        Assert.Equal("Name", v);
    }

    [Fact]
    public async Task SelectedRowsChanged_Ok()
    {
        var localizer = Context.Services.GetRequiredService<IStringLocalizer<Foo>>();
        var items = Foo.GenerateFoo(localizer, 2);
        var selectedRows = new List<Foo>();
        var cut = Context.RenderComponent<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<Table<Foo>>(pb =>
            {
                pb.Add(a => a.RenderMode, TableRenderMode.Table);
                pb.Add(a => a.Items, items);
                pb.Add(a => a.SelectedRows, selectedRows);
                pb.Add(a => a.IsMultipleSelect, true);
                pb.Add(a => a.SelectedRowsChanged, EventCallback.Factory.Create<List<Foo>>(this, foos =>
                {
                    selectedRows = foos;
                }));
                pb.Add(a => a.TableColumns, foo => builder =>
                {
                    builder.OpenComponent<TableColumn<Foo, string>>(0);
                    builder.AddAttribute(1, "Field", "Name");
                    builder.AddAttribute(2, "FieldExpression", Utility.GenerateValueExpression(foo, "Name", typeof(string)));
                    builder.CloseComponent();
                });
            });
        });
        Assert.Empty(selectedRows);

        var check = cut.Find("thead input");
        await cut.InvokeAsync(() => check.Click());
        Assert.Equal(2, selectedRows.Count);

        await cut.InvokeAsync(() => check.Click());
        Assert.Empty(selectedRows);
    }

    [Fact]
    public void SelectedRows_Bind()
    {
        var localizer = Context.Services.GetRequiredService<IStringLocalizer<Foo>>();
        var items = FooNoKeyTree.Generate(localizer);
        var selectedRows = new List<FooNoKeyTree>();
        selectedRows.AddRange(items.Take(2));
        var cut = Context.RenderComponent<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<Table<FooNoKeyTree>>(pb =>
            {
                pb.Add(a => a.RenderMode, TableRenderMode.Table);
                pb.Add(a => a.OnQueryAsync, options =>
                {
                    var data = new QueryData<FooNoKeyTree>()
                    {
                        Items = items,
                        TotalCount = 80
                    };
                    return Task.FromResult(data);
                });
                pb.Add(a => a.SelectedRows, selectedRows);
                pb.Add(a => a.IsMultipleSelect, true);
                pb.Add(a => a.TableColumns, foo => builder =>
                {
                    builder.OpenComponent<TableColumn<Foo, string>>(0);
                    builder.AddAttribute(1, "Field", "Name");
                    builder.AddAttribute(2, "FieldExpression", Utility.GenerateValueExpression(foo, "Name", typeof(string)));
                    builder.CloseComponent();
                });
            });
        });
    }

    [Fact]
    public void SetRowClassFormatter_Ok()
    {
        var localizer = Context.Services.GetRequiredService<IStringLocalizer<Foo>>();
        var items = Foo.GenerateFoo(localizer, 2);
        var selectedRows = new List<Foo>();
        var cut = Context.RenderComponent<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<Table<Foo>>(pb =>
            {
                pb.Add(a => a.RenderMode, TableRenderMode.Table);
                pb.Add(a => a.Items, items);
                pb.Add(a => a.IsKeyboard, true);
                pb.Add(a => a.ShowLoading, false);
                pb.Add(a => a.UseComponentWidth, true);
                pb.Add(a => a.RenderModeResponsiveWidth, 768);
                pb.Add(a => a.SetRowClassFormatter, foo => "test_row_class");
                pb.Add(a => a.TableColumns, foo => builder =>
                {
                    builder.OpenComponent<TableColumn<Foo, string>>(0);
                    builder.AddAttribute(1, "Field", "Name");
                    builder.AddAttribute(2, "FieldExpression", Utility.GenerateValueExpression(foo, "Name", typeof(string)));
                    builder.CloseComponent();
                });
            });
        });
        cut.Contains("test_row_class");
    }

    [Fact]
    public void OnQueryAsync_DataService()
    {
        var localizer = Context.Services.GetRequiredService<IStringLocalizer<Foo>>();
        var cut = Context.RenderComponent<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<Table<Foo>>(pb =>
            {
                pb.Add(a => a.RenderMode, TableRenderMode.Table);
                pb.Add(a => a.DataService, new MockNullDataService(localizer));
                pb.Add(a => a.TableColumns, foo => builder =>
                {
                    builder.OpenComponent<TableColumn<Foo, string>>(0);
                    builder.AddAttribute(1, "Field", "Name");
                    builder.AddAttribute(2, "FieldExpression", Utility.GenerateValueExpression(foo, "Name", typeof(string)));
                    builder.CloseComponent();
                });
            });
        });
        Assert.Equal(2, cut.FindAll("tbody tr").Count);
    }

    [Fact]
    public async Task Delete_Ok()
    {
        var localizer = Context.Services.GetRequiredService<IStringLocalizer<Foo>>();
        var items = Foo.GenerateFoo(localizer, 2);
        var cut = Context.RenderComponent<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<Table<Foo>>(pb =>
            {
                pb.Add(a => a.RenderMode, TableRenderMode.Table);
                pb.Add(a => a.Items, items);
                pb.Add(a => a.IsMultipleSelect, true);
                pb.Add(a => a.ShowToolbar, true);
                pb.Add(a => a.TableColumns, foo => builder =>
                {
                    builder.OpenComponent<TableColumn<Foo, string>>(0);
                    builder.AddAttribute(1, "Field", "Name");
                    builder.AddAttribute(2, "FieldExpression", Utility.GenerateValueExpression(foo, "Name", typeof(string)));
                    builder.CloseComponent();
                });
            });
        });
        var input = cut.Find("tbody tr input");
        await cut.InvokeAsync(() => input.Click());

        var button = cut.FindComponent<TableToolbarPopconfirmButton<Foo>>();
        await cut.InvokeAsync(() => button.Instance.OnConfirm.Invoke());

        var row = cut.FindAll("tbody tr");
        Assert.Equal(2, row.Count);
    }

    [Fact]
    public async Task OnDeleteAsync_Ok()
    {
        var localizer = Context.Services.GetRequiredService<IStringLocalizer<Foo>>();
        var items = Foo.GenerateFoo(localizer, 2);
        var cut = Context.RenderComponent<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<Table<Foo>>(pb =>
            {
                pb.Add(a => a.RenderMode, TableRenderMode.Table);
                pb.Add(a => a.Items, items);
                pb.Add(a => a.IsMultipleSelect, true);
                pb.Add(a => a.ShowToolbar, true);
                pb.Add(a => a.OnDeleteAsync, foos =>
                {
                    foreach (var foo in foos)
                    {
                        items.Remove(foo);
                    }
                    return Task.FromResult(true);
                });
                pb.Add(a => a.TableColumns, foo => builder =>
                {
                    builder.OpenComponent<TableColumn<Foo, string>>(0);
                    builder.AddAttribute(1, "Field", "Name");
                    builder.AddAttribute(2, "FieldExpression", Utility.GenerateValueExpression(foo, "Name", typeof(string)));
                    builder.CloseComponent();
                });
            });
        });
        var input = cut.Find("tbody tr input");
        await cut.InvokeAsync(() => input.Click());

        var button = cut.FindComponent<TableToolbarPopconfirmButton<Foo>>();
        await cut.InvokeAsync(() => button.Instance.OnConfirm.Invoke());

        var row = cut.FindAll("tbody tr");
        Assert.Equal(1, row.Count);
    }

    [Fact]
    public async Task OnBeforeDelete_Ok()
    {
        var localizer = Context.Services.GetRequiredService<IStringLocalizer<Foo>>();
        var items = Foo.GenerateFoo(localizer, 2);
        var cut = Context.RenderComponent<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<Table<Foo>>(pb =>
            {
                pb.Add(a => a.RenderMode, TableRenderMode.Table);
                pb.Add(a => a.Items, items);
                pb.Add(a => a.ShowToolbar, true);
                pb.Add(a => a.ShowExtendButtons, true);
                pb.Add(a => a.TableColumns, foo => builder =>
                {
                    builder.OpenComponent<TableColumn<Foo, string>>(0);
                    builder.AddAttribute(1, "Field", "Name");
                    builder.AddAttribute(2, "FieldExpression", Utility.GenerateValueExpression(foo, "Name", typeof(string)));
                    builder.CloseComponent();
                });
            });
        });

        var buttons = cut.FindComponents<PopConfirmButton>();
        await cut.InvokeAsync(() => buttons[1].Instance.OnBeforeClick());

        var table = cut.FindComponent<Table<Foo>>();
        Assert.Single(table.Instance.SelectedRows);
    }

    [Fact]
    public async Task OnSaveAsync_Ok()
    {
        var localizer = Context.Services.GetRequiredService<IStringLocalizer<Foo>>();
        var items = Foo.GenerateFoo(localizer, 2);
        var itemChagned = ItemChangedType.Add;
        var afterSave = false;
        var cut = Context.RenderComponent<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<Table<Foo>>(pb =>
            {
                pb.Add(a => a.RenderMode, TableRenderMode.Table);
                pb.Add(a => a.Items, items);
                pb.Add(a => a.IsMultipleSelect, true);
                pb.Add(a => a.ShowToolbar, true);
                pb.Add(a => a.ShowExtendButtons, true);
                pb.Add(a => a.EditMode, EditMode.InCell);
                pb.Add(a => a.ShowToastAfterSaveOrDeleteModel, false);
                pb.Add(a => a.OnSaveAsync, (foo, changedType) =>
                {
                    itemChagned = changedType;
                    return Task.FromResult(true);
                });
                pb.Add(a => a.OnAfterSaveAsync, foo =>
                {
                    afterSave = true;
                    return Task.CompletedTask;
                });
                pb.Add(a => a.TableColumns, foo => builder =>
                {
                    builder.OpenComponent<TableColumn<Foo, string>>(0);
                    builder.AddAttribute(1, "Field", "Name");
                    builder.AddAttribute(2, "FieldExpression", Utility.GenerateValueExpression(foo, "Name", typeof(string)));
                    builder.CloseComponent();
                });
            });
        });

        // test edit button
        var button = cut.FindAll("tbody tr button");
        await cut.InvokeAsync(() => button[0].Click());

        var update = cut.Find("tbody tr button");
        await cut.InvokeAsync(() => update.Click());
        Assert.Equal(ItemChangedType.Update, itemChagned);

        Assert.True(afterSave);
    }

    [Theory]
    [InlineData(EditMode.EditForm)]
    [InlineData(EditMode.InCell)]
    public async Task OnAddAsync_Ok(EditMode mode)
    {
        var localizer = Context.Services.GetRequiredService<IStringLocalizer<Foo>>();
        var items = Foo.GenerateFoo(localizer, 2);
        var added = false;
        var itemChagned = ItemChangedType.Update;
        var cut = Context.RenderComponent<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<Table<Foo>>(pb =>
            {
                pb.Add(a => a.RenderMode, TableRenderMode.Table);
                pb.Add(a => a.Items, items);
                pb.Add(a => a.IsMultipleSelect, true);
                pb.Add(a => a.ShowToolbar, true);
                pb.Add(a => a.ShowExtendButtons, true);
                pb.Add(a => a.EditMode, mode);
                pb.Add(a => a.InsertRowMode, InsertRowMode.First);
                pb.Add(a => a.OnAddAsync, () =>
                {
                    added = true;
                    return Task.FromResult(new Foo() { Name = "test" });
                });
                pb.Add(a => a.OnSaveAsync, (foo, changedType) =>
                {
                    itemChagned = changedType;
                    return Task.FromResult(true);
                });
                pb.Add(a => a.TableColumns, foo => builder =>
                {
                    builder.OpenComponent<TableColumn<Foo, string>>(0);
                    builder.AddAttribute(1, "Field", "Name");
                    builder.AddAttribute(2, "FieldExpression", Utility.GenerateValueExpression(foo, "Name", typeof(string)));
                    builder.CloseComponent();
                });
            });
        });

        // test add button
        var button = cut.FindComponent<TableToolbarButton<Foo>>();
        await cut.InvokeAsync(() => button.Instance.OnClick.InvokeAsync());
        Assert.True(added);

        if (mode == EditMode.InCell)
        {
            // test update button
            var update = cut.Find("tbody tr button");
            await cut.InvokeAsync(() => update.Click());
            Assert.Equal(ItemChangedType.Add, itemChagned);
        }
        else if (mode == EditMode.EditForm)
        {
            var input = cut.Find("tbody form input");
            await cut.InvokeAsync(() => input.Change("test_name"));

            var form = cut.Find("tbody form");
            await cut.InvokeAsync(() => form.Submit());
            Assert.Equal(ItemChangedType.Add, itemChagned);
        }
    }

    [Fact]
    public async Task OnEditAsync_Ok()
    {
        var localizer = Context.Services.GetRequiredService<IStringLocalizer<Foo>>();
        var items = Foo.GenerateFoo(localizer, 2);
        var edited = false;
        var cut = Context.RenderComponent<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<Table<Foo>>(pb =>
            {
                pb.Add(a => a.RenderMode, TableRenderMode.Table);
                pb.Add(a => a.Items, items);
                pb.Add(a => a.IsMultipleSelect, true);
                pb.Add(a => a.ShowToolbar, true);
                pb.Add(a => a.ShowExtendButtons, true);
                pb.Add(a => a.EditMode, EditMode.InCell);
                pb.Add(a => a.OnEditAsync, foo =>
                {
                    edited = true;
                    return Task.CompletedTask;
                });
                pb.Add(a => a.TableColumns, foo => builder =>
                {
                    builder.OpenComponent<TableColumn<Foo, string>>(0);
                    builder.AddAttribute(1, "Field", "Name");
                    builder.AddAttribute(2, "FieldExpression", Utility.GenerateValueExpression(foo, "Name", typeof(string)));
                    builder.CloseComponent();
                });
            });
        });

        // test edit button
        var button = cut.FindAll("tbody tr button");
        await cut.InvokeAsync(() => button[0].Click());
        Assert.True(edited);
    }

    [Fact]
    public async Task ToggleLoading_Ok()
    {
        var localizer = Context.Services.GetRequiredService<IStringLocalizer<Foo>>();
        var items = Foo.GenerateFoo(localizer, 2);
        var cut = Context.RenderComponent<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<Table<Foo>>(pb =>
            {
                pb.Add(a => a.RenderMode, TableRenderMode.Table);
                pb.Add(a => a.Items, items);
                pb.Add(a => a.ShowLoading, true);
                pb.Add(a => a.TableColumns, foo => builder =>
                {
                    builder.OpenComponent<TableColumn<Foo, string>>(0);
                    builder.AddAttribute(1, "Field", "Name");
                    builder.AddAttribute(2, "FieldExpression", Utility.GenerateValueExpression(foo, "Name", typeof(string)));
                    builder.CloseComponent();
                });
            });
        });

        var table = cut.FindComponent<Table<Foo>>();
        await cut.InvokeAsync(() => table.Instance.QueryAsync());
    }

    [Fact]
    public async Task Refresh_Ok()
    {
        var localizer = Context.Services.GetRequiredService<IStringLocalizer<Foo>>();
        var items = Foo.GenerateFoo(localizer, 2);
        var cut = Context.RenderComponent<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<Table<Foo>>(pb =>
            {
                pb.Add(a => a.RenderMode, TableRenderMode.Table);
                pb.Add(a => a.ShowToolbar, true);
                pb.Add(a => a.Items, items);
                pb.Add(a => a.TableColumns, foo => builder =>
                {
                    builder.OpenComponent<TableColumn<Foo, string>>(0);
                    builder.AddAttribute(1, "Field", "Name");
                    builder.AddAttribute(2, "FieldExpression", Utility.GenerateValueExpression(foo, "Name", typeof(string)));
                    builder.CloseComponent();
                });
            });
        });

        var button = cut.FindComponents<Button>().First(i => i.Instance.Icon == "fa fa-refresh");
        await cut.InvokeAsync(() => button.Instance.OnClickWithoutRender!.Invoke());
    }

    [Fact]
    public async Task CardView_Ok()
    {
        var localizer = Context.Services.GetRequiredService<IStringLocalizer<Foo>>();
        var items = Foo.GenerateFoo(localizer, 2);
        var cut = Context.RenderComponent<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<Table<Foo>>(pb =>
            {
                pb.Add(a => a.RenderMode, TableRenderMode.Auto);
                pb.Add(a => a.ShowToolbar, true);
                pb.Add(a => a.ShowCardView, true);
                pb.Add(a => a.Items, items);
                pb.Add(a => a.TableColumns, foo => builder =>
                {
                    builder.OpenComponent<TableColumn<Foo, string>>(0);
                    builder.AddAttribute(1, "Field", "Name");
                    builder.AddAttribute(2, "FieldExpression", Utility.GenerateValueExpression(foo, "Name", typeof(string)));
                    builder.CloseComponent();
                });
            });
        });

        var button = cut.FindComponents<Button>().First(i => i.Instance.Icon == "fa fa-bars");
        await cut.InvokeAsync(() => button.Instance.OnClick.InvokeAsync());

        var table = cut.FindComponent<Table<Foo>>();
        Assert.Equal(TableRenderMode.Table, table.Instance.RenderMode);

        await cut.InvokeAsync(() => button.Instance.OnClick.InvokeAsync());
        Assert.Equal(TableRenderMode.CardView, table.Instance.RenderMode);
    }

    [Fact]
    public void SortString_Ok()
    {
        var sortList = new List<string>();
        var localizer = Context.Services.GetRequiredService<IStringLocalizer<Foo>>();
        var cut = Context.RenderComponent<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<Table<Foo>>(pb =>
            {
                pb.Add(a => a.RenderMode, TableRenderMode.Table);
                pb.Add(a => a.OnQueryAsync, op =>
                {
                    sortList.AddRange(op.SortList!);
                    return OnQueryAsync(localizer, isSorted: false)(op);
                });
                pb.Add(a => a.SortString, "Name Desc, Count");
                pb.Add(a => a.TableColumns, foo => builder =>
                {
                    builder.OpenComponent<TableColumn<Foo, string>>(0);
                    builder.AddAttribute(1, "Field", "Name");
                    builder.AddAttribute(2, "FieldExpression", Utility.GenerateValueExpression(foo, "Name", typeof(string)));
                    builder.CloseComponent();

                    builder.OpenComponent<TableColumn<Foo, int>>(0);
                    builder.AddAttribute(1, "Field", 1);
                    builder.AddAttribute(2, "FieldExpression", Utility.GenerateValueExpression(foo, "Count", typeof(int)));
                    builder.CloseComponent();
                });
            });
        });

        Assert.Equal("Name Desc", sortList[0]);
        Assert.Equal(" Count", sortList[1]);
    }

    [Fact]
    public async Task DynamicContext_EqualityComparer()
    {
        var comparered = false;
        var localizer = Context.Services.GetRequiredService<IStringLocalizer<Foo>>();
        var items = Foo.GenerateFoo(localizer, 2);
        var context = CreateDynamicContext(localizer);
        context.EqualityComparer = (x, y) =>
        {
            comparered = true;
            return x.GetValue("Id") == y.GetValue("Id");
        };
        var cut = Context.RenderComponent<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<Table<DynamicObject>>(pb =>
            {
                pb.Add(a => a.RenderMode, TableRenderMode.Table);
                pb.Add(a => a.IsMultipleSelect, true);
                pb.Add(a => a.DynamicContext, context);
            });
        });

        // 选中行
        var input = cut.Find("tbody input");
        await cut.InvokeAsync(() => input.Click());
        Assert.True(comparered);
    }

    [Fact]
    public async Task DynamicContext_Add()
    {
        var localizer = Context.Services.GetRequiredService<IStringLocalizer<Foo>>();
        var items = Foo.GenerateFoo(localizer, 2);
        var cut = Context.RenderComponent<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<Table<DynamicObject>>(pb =>
            {
                pb.Add(a => a.ShowToolbar, true);
                pb.Add(a => a.SortString, "Name desc");
                pb.Add(a => a.DynamicContext, CreateDynamicContext(localizer));
            });
        });

        var table = cut.FindComponent<Table<DynamicObject>>();
        await cut.InvokeAsync(() => table.Instance.AddAsync());

        var delete = cut.FindComponent<TableToolbarPopconfirmButton<DynamicObject>>();
        await cut.InvokeAsync(() => delete.Instance.OnConfirm());
    }

    [Fact]
    public async Task DynamicContext_Edit()
    {
        var localizer = Context.Services.GetRequiredService<IStringLocalizer<Foo>>();
        var items = Foo.GenerateFoo(localizer, 2);
        var cut = Context.RenderComponent<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<MockDynamicTable>(pb =>
            {
                pb.Add(a => a.RenderMode, TableRenderMode.Table);
                pb.Add(a => a.IsMultipleSelect, true);
                pb.Add(a => a.ShowToolbar, true);
                pb.Add(a => a.DynamicContext, CreateDynamicContext(localizer));
            });
        });

        var input = cut.Find("tbody tr input");
        await cut.InvokeAsync(() => input.Click());

        var table = cut.FindComponent<MockDynamicTable>();
        var saved = await table.Instance.SaveModelTest();
        Assert.True(saved);
    }

    [Fact]
    public void CustomerSearches_Ok()
    {
        var localizer = Context.Services.GetRequiredService<IStringLocalizer<Foo>>();
        var cut = Context.RenderComponent<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<Table<Foo>>(pb =>
            {
                pb.Add(a => a.RenderMode, TableRenderMode.Table);
                pb.Add(a => a.OnQueryAsync, op =>
                {
                    op.CustomerSearchs.AddRange(new List<IFilterAction>() { new MockFilterAction() });
                    op.Filters.AddRange(new List<IFilterAction>() { new MockFilterAction() });
                    return OnQueryAsync(localizer, isAdvanceSearch: false, isFilter: false)(op);
                });
                pb.Add(a => a.TableColumns, foo => builder =>
                {
                    builder.OpenComponent<TableColumn<Foo, string>>(0);
                    builder.AddAttribute(1, "Field", "Name");
                    builder.AddAttribute(2, "FieldExpression", Utility.GenerateValueExpression(foo, "Name", typeof(string)));
                    builder.CloseComponent();
                });
            });
        });
    }

    [Fact]
    public void ShowButtons_Ok()
    {
        var localizer = Context.Services.GetRequiredService<IStringLocalizer<Foo>>();
        var items = Foo.GenerateFoo(localizer, 2);
        var cut = Context.RenderComponent<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<Table<Foo>>(pb =>
            {
                pb.Add(a => a.RenderMode, TableRenderMode.Table);
                pb.Add(a => a.Items, items);
                pb.Add(a => a.ShowToolbar, false);
                pb.Add(a => a.TableColumns, foo => builder =>
                {
                    builder.OpenComponent<TableColumn<Foo, string>>(0);
                    builder.AddAttribute(1, "Field", "Name");
                    builder.AddAttribute(2, "FieldExpression", Utility.GenerateValueExpression(foo, "Name", typeof(string)));
                    builder.CloseComponent();
                });
            });
        });

        var table = cut.FindComponent<Table<Foo>>();
        Assert.Contains("<div class=\"table-toolbar\"></div>", table.Markup);

        table.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.ShowToolbar, true);
        });
        Assert.Contains("float-start table-toolbar-button", table.Markup);
    }

    [Fact]
    public void ShowDefaultButtons_Ok()
    {
        var localizer = Context.Services.GetRequiredService<IStringLocalizer<Foo>>();
        var items = Foo.GenerateFoo(localizer, 2);
        var cut = Context.RenderComponent<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<Table<Foo>>(pb =>
            {
                pb.Add(a => a.RenderMode, TableRenderMode.Table);
                pb.Add(a => a.Items, items);
                pb.Add(a => a.ShowToolbar, true);
                pb.Add(a => a.TableColumns, foo => builder =>
                {
                    builder.OpenComponent<TableColumn<Foo, string>>(0);
                    builder.AddAttribute(1, "Field", "Name");
                    builder.AddAttribute(2, "FieldExpression", Utility.GenerateValueExpression(foo, "Name", typeof(string)));
                    builder.CloseComponent();
                });
            });
        });

        var table = cut.FindComponent<Table<Foo>>();
        Assert.Contains("fa fa-plus", table.Markup);
        Assert.Contains("fa fa-pencil", table.Markup);
        Assert.Contains("fa fa-remove", table.Markup);

        table.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.ShowDefaultButtons, false);
        });
        Assert.DoesNotContain("fa fa-plus", table.Markup);
        Assert.DoesNotContain("fa fa-pencil", table.Markup);
        Assert.DoesNotContain("fa fa-remove", table.Markup);
    }

    [Fact]
    public void ShowAddButton_Ok()
    {
        var localizer = Context.Services.GetRequiredService<IStringLocalizer<Foo>>();
        var items = Foo.GenerateFoo(localizer, 2);
        var cut = Context.RenderComponent<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<Table<Foo>>(pb =>
            {
                pb.Add(a => a.RenderMode, TableRenderMode.Table);
                pb.Add(a => a.Items, items);
                pb.Add(a => a.ShowToolbar, true);
                pb.Add(a => a.TableColumns, foo => builder =>
                {
                    builder.OpenComponent<TableColumn<Foo, string>>(0);
                    builder.AddAttribute(1, "Field", "Name");
                    builder.AddAttribute(2, "FieldExpression", Utility.GenerateValueExpression(foo, "Name", typeof(string)));
                    builder.CloseComponent();
                });
            });
        });

        var table = cut.FindComponent<Table<Foo>>();
        Assert.Contains("fa fa-plus", table.Markup);

        table.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.ShowAddButton, false);
        });
        Assert.DoesNotContain("fa fa-plus", table.Markup);
    }

    [Fact]
    public void ShowRefreshButton_Ok()
    {
        var localizer = Context.Services.GetRequiredService<IStringLocalizer<Foo>>();
        var items = Foo.GenerateFoo(localizer, 2);
        var cut = Context.RenderComponent<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<Table<Foo>>(pb =>
            {
                pb.Add(a => a.RenderMode, TableRenderMode.Table);
                pb.Add(a => a.Items, items);
                pb.Add(a => a.ShowToolbar, true);
                pb.Add(a => a.ShowRefresh, true);
                pb.Add(a => a.TableColumns, foo => builder =>
                {
                    builder.OpenComponent<TableColumn<Foo, string>>(0);
                    builder.AddAttribute(1, "Field", "Name");
                    builder.AddAttribute(2, "FieldExpression", Utility.GenerateValueExpression(foo, "Name", typeof(string)));
                    builder.CloseComponent();
                });
            });
        });

        var table = cut.FindComponent<Table<Foo>>();
        Assert.Contains("fa fa-refresh", table.Markup);

        table.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.ShowRefresh, false);
        });
        Assert.DoesNotContain("fa fa-refresh", table.Markup);
    }

    [Fact]
    public void ShowEditButton_Ok()
    {
        var localizer = Context.Services.GetRequiredService<IStringLocalizer<Foo>>();
        var items = Foo.GenerateFoo(localizer, 2);
        var cut = Context.RenderComponent<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<Table<Foo>>(pb =>
            {
                pb.Add(a => a.RenderMode, TableRenderMode.Table);
                pb.Add(a => a.Items, items);
                pb.Add(a => a.ShowToolbar, true);
                pb.Add(a => a.TableColumns, foo => builder =>
                {
                    builder.OpenComponent<TableColumn<Foo, string>>(0);
                    builder.AddAttribute(1, "Field", "Name");
                    builder.AddAttribute(2, "FieldExpression", Utility.GenerateValueExpression(foo, "Name", typeof(string)));
                    builder.CloseComponent();
                });
            });
        });

        var table = cut.FindComponent<Table<Foo>>();
        Assert.Contains("fa fa-pencil", table.Markup);

        table.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.ShowEditButton, false);
        });
        Assert.DoesNotContain("fa fa-pencil", table.Markup);
    }

    [Fact]
    public void ShowDeleteButton_Ok()
    {
        var localizer = Context.Services.GetRequiredService<IStringLocalizer<Foo>>();
        var items = Foo.GenerateFoo(localizer, 2);
        var cut = Context.RenderComponent<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<Table<Foo>>(pb =>
            {
                pb.Add(a => a.RenderMode, TableRenderMode.Table);
                pb.Add(a => a.Items, items);
                pb.Add(a => a.ShowToolbar, true);
                pb.Add(a => a.TableColumns, foo => builder =>
                {
                    builder.OpenComponent<TableColumn<Foo, string>>(0);
                    builder.AddAttribute(1, "Field", "Name");
                    builder.AddAttribute(2, "FieldExpression", Utility.GenerateValueExpression(foo, "Name", typeof(string)));
                    builder.CloseComponent();
                });
            });
        });

        var table = cut.FindComponent<Table<Foo>>();
        Assert.Contains("fa fa-remove", table.Markup);

        table.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.ShowDeleteButton, false);
        });
        Assert.DoesNotContain("fa fa-remove", table.Markup);
    }

    [Fact]
    public void ShowEditButtonCallback_Ok()
    {
        var localizer = Context.Services.GetRequiredService<IStringLocalizer<Foo>>();
        var items = Foo.GenerateFoo(localizer, 2);
        var cut = Context.RenderComponent<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<Table<Foo>>(pb =>
            {
                pb.Add(a => a.RenderMode, TableRenderMode.Table);
                pb.Add(a => a.Items, items);
                pb.Add(a => a.ShowToolbar, true);
                pb.Add(a => a.ShowExtendButtons, true);
                pb.Add(a => a.ShowEditButtonCallback, foo => true);
                pb.Add(a => a.TableColumns, foo => builder =>
                {
                    builder.OpenComponent<TableColumn<Foo, string>>(0);
                    builder.AddAttribute(1, "Field", "Name");
                    builder.AddAttribute(2, "FieldExpression", Utility.GenerateValueExpression(foo, "Name", typeof(string)));
                    builder.CloseComponent();
                });
            });
        });

        var table = cut.FindComponent<Table<Foo>>();
        Assert.Contains("fa fa-edit", table.Find("tbody").ToMarkup());

        table.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.ShowEditButtonCallback, foo => false);
        });
        Assert.DoesNotContain("fa fa-edit", table.Find("tbody").ToMarkup());
    }

    [Fact]
    public void ShowDeleteButtonCallback_Ok()
    {
        var localizer = Context.Services.GetRequiredService<IStringLocalizer<Foo>>();
        var items = Foo.GenerateFoo(localizer, 2);
        var cut = Context.RenderComponent<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<Table<Foo>>(pb =>
            {
                pb.Add(a => a.RenderMode, TableRenderMode.Table);
                pb.Add(a => a.Items, items);
                pb.Add(a => a.ShowToolbar, true);
                pb.Add(a => a.ShowExtendButtons, true);
                pb.Add(a => a.ShowDeleteButtonCallback, foo => true);
                pb.Add(a => a.TableColumns, foo => builder =>
                {
                    builder.OpenComponent<TableColumn<Foo, string>>(0);
                    builder.AddAttribute(1, "Field", "Name");
                    builder.AddAttribute(2, "FieldExpression", Utility.GenerateValueExpression(foo, "Name", typeof(string)));
                    builder.CloseComponent();
                });
            });
        });

        var table = cut.FindComponent<Table<Foo>>();
        Assert.Contains("fa fa-remove", table.Find("tbody").ToMarkup());

        table.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.ShowDeleteButtonCallback, foo => false);
        });
        Assert.DoesNotContain("fa fa-remove", table.Find("tbody").ToMarkup());
    }

    [Fact]
    public void ShowExtendEditButton_Ok()
    {
        var localizer = Context.Services.GetRequiredService<IStringLocalizer<Foo>>();
        var items = Foo.GenerateFoo(localizer, 2);
        var cut = Context.RenderComponent<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<Table<Foo>>(pb =>
            {
                pb.Add(a => a.RenderMode, TableRenderMode.Table);
                pb.Add(a => a.Items, items);
                pb.Add(a => a.ShowToolbar, true);
                pb.Add(a => a.ExtendButtonColumnWidth, 130);
                pb.Add(a => a.ShowExtendButtons, true);
                pb.Add(a => a.ShowExtendEditButton, true);
                pb.Add(a => a.TableColumns, foo => builder =>
                {
                    builder.OpenComponent<TableColumn<Foo, string>>(0);
                    builder.AddAttribute(1, "Field", "Name");
                    builder.AddAttribute(2, "FieldExpression", Utility.GenerateValueExpression(foo, "Name", typeof(string)));
                    builder.CloseComponent();
                });
            });
        });

        var table = cut.FindComponent<Table<Foo>>();
        Assert.Contains("fa fa-edit", table.Find("tbody").ToMarkup());

        table.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.ShowExtendEditButton, false);
        });
        Assert.DoesNotContain("fa fa-edit", table.Find("tbody").ToMarkup());

        table.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.ShowExtendEditButton, true);
            pb.Add(a => a.ShowDefaultButtons, false);
            pb.Add(a => a.ShowEditButtonCallback, foo => true);
        });
        Assert.Contains("fa fa-edit", table.Find("tbody").ToMarkup());
    }

    [Fact]
    public void ShowExtendDeleteButton_Ok()
    {
        var localizer = Context.Services.GetRequiredService<IStringLocalizer<Foo>>();
        var items = Foo.GenerateFoo(localizer, 2);
        var cut = Context.RenderComponent<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<Table<Foo>>(pb =>
            {
                pb.Add(a => a.RenderMode, TableRenderMode.Table);
                pb.Add(a => a.Items, items);
                pb.Add(a => a.ShowToolbar, true);
                pb.Add(a => a.ShowExtendButtons, true);
                pb.Add(a => a.ShowExtendDeleteButton, true);
                pb.Add(a => a.TableColumns, foo => builder =>
                {
                    builder.OpenComponent<TableColumn<Foo, string>>(0);
                    builder.AddAttribute(1, "Field", "Name");
                    builder.AddAttribute(2, "FieldExpression", Utility.GenerateValueExpression(foo, "Name", typeof(string)));
                    builder.CloseComponent();
                });
            });
        });

        var table = cut.FindComponent<Table<Foo>>();
        Assert.Contains("fa fa-remove", table.Find("tbody").ToMarkup());

        table.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.ShowExtendDeleteButton, false);
        });
        Assert.DoesNotContain("fa fa-remove", table.Find("tbody").ToMarkup());

        table.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.ShowExtendDeleteButton, true);
            pb.Add(a => a.ShowDefaultButtons, false);
            pb.Add(a => a.ShowDeleteButtonCallback, foo => true);
        });
        Assert.Contains("fa fa-remove", table.Find("tbody").ToMarkup());
    }

    [Fact]
    public async Task EditAsync_Ok()
    {
        var localizer = Context.Services.GetRequiredService<IStringLocalizer<Foo>>();
        var items = Foo.GenerateFoo(localizer, 2);
        var cut = Context.RenderComponent<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<Table<Foo>>(pb =>
            {
                pb.Add(a => a.RenderMode, TableRenderMode.Table);
                pb.Add(a => a.Items, items);
                pb.Add(a => a.IsMultipleSelect, true);
                pb.Add(a => a.ShowToolbar, true);
                pb.Add(a => a.TableColumns, foo => builder =>
                {
                    builder.OpenComponent<TableColumn<Foo, string>>(0);
                    builder.AddAttribute(1, "Field", "Name");
                    builder.AddAttribute(2, "FieldExpression", Utility.GenerateValueExpression(foo, "Name", typeof(string)));
                    builder.CloseComponent();
                });
            });
        });

        var table = cut.FindComponent<Table<Foo>>();
        await cut.InvokeAsync(() => table.Instance.EditAsync());

        // 选一个
        var input = cut.Find("tbody tr input");
        await cut.InvokeAsync(() => input.Click());
        await cut.InvokeAsync(() => table.Instance.EditAsync());

        table.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.ShowEditButtonCallback, foo => false);
        });
        await cut.InvokeAsync(() => table.Instance.EditAsync());

        // 选两个
        input = cut.Find("thead input");
        await cut.InvokeAsync(() => input.Click());
        await cut.InvokeAsync(() => table.Instance.EditAsync());
    }

    [Theory]
    [InlineData(EditMode.EditForm)]
    [InlineData(EditMode.InCell)]
    public async Task CancelSave_Ok(EditMode mode)
    {
        var localizer = Context.Services.GetRequiredService<IStringLocalizer<Foo>>();
        var items = Foo.GenerateFoo(localizer, 2);
        var cut = Context.RenderComponent<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<Table<Foo>>(pb =>
            {
                pb.Add(a => a.RenderMode, TableRenderMode.Table);
                pb.Add(a => a.Items, items);
                pb.Add(a => a.IsMultipleSelect, true);
                pb.Add(a => a.ShowToolbar, true);
                pb.Add(a => a.ShowExtendButtons, true);
                pb.Add(a => a.EditMode, mode);
                pb.Add(a => a.TableColumns, foo => builder =>
                {
                    builder.OpenComponent<TableColumn<Foo, string>>(0);
                    builder.AddAttribute(1, "Field", "Name");
                    builder.AddAttribute(2, "FieldExpression", Utility.GenerateValueExpression(foo, "Name", typeof(string)));
                    builder.CloseComponent();
                });
            });
        });

        // test update button
        var update = cut.Find("tbody tr button");
        await cut.InvokeAsync(() => update.Click());
        if (mode == EditMode.InCell)
        {
            var cancel = cut.Find("tbody tr .btn-warning");
            await cut.InvokeAsync(() => cancel.Click());
        }
        else if (mode == EditMode.EditForm)
        {
            var button = cut.Find("tbody form .form-footer .btn-secondary");
            await cut.InvokeAsync(() => button.Click());
        }
    }

    [Fact]
    public async Task ConfirmDelete_Ok()
    {
        var localizer = Context.Services.GetRequiredService<IStringLocalizer<Foo>>();
        var items = Foo.GenerateFoo(localizer, 2);
        var cut = Context.RenderComponent<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<Table<Foo>>(pb =>
            {
                pb.Add(a => a.RenderMode, TableRenderMode.Table);
                pb.Add(a => a.Items, items);
                pb.Add(a => a.IsMultipleSelect, true);
                pb.Add(a => a.ShowToolbar, true);
                pb.Add(a => a.TableColumns, foo => builder =>
                {
                    builder.OpenComponent<TableColumn<Foo, string>>(0);
                    builder.AddAttribute(1, "Field", "Name");
                    builder.AddAttribute(2, "FieldExpression", Utility.GenerateValueExpression(foo, "Name", typeof(string)));
                    builder.CloseComponent();
                });
            });
        });

        var table = cut.FindComponent<Table<Foo>>();
        var deleteButton = table.FindComponent<TableToolbarPopconfirmButton<Foo>>();
        await cut.InvokeAsync(() => deleteButton.Instance.OnBeforeClick());

        // 选一个
        var input = cut.Find("tbody tr input");
        await cut.InvokeAsync(() => input.Click());
        await cut.InvokeAsync(() => deleteButton.Instance.OnBeforeClick());

        table.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.ShowDeleteButtonCallback, foo => false);
        });
        await cut.InvokeAsync(() => deleteButton.Instance.OnBeforeClick());
    }

    [Fact]
    public async Task OnConfirm_Ok()
    {
        var localizer = Context.Services.GetRequiredService<IStringLocalizer<Foo>>();
        var cut = Context.RenderComponent<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<Table<Foo>>(pb =>
            {
                pb.Add(a => a.RenderMode, TableRenderMode.Table);
                pb.Add(a => a.OnQueryAsync, OnQueryAsync(localizer));
                pb.Add(a => a.IsMultipleSelect, true);
                pb.Add(a => a.ShowToolbar, true);
                pb.Add(a => a.IsPagination, true);
                pb.Add(a => a.PageItemsSource, new int[] { 1 });
                pb.Add(a => a.TableColumns, foo => builder =>
                {
                    builder.OpenComponent<TableColumn<Foo, string>>(0);
                    builder.AddAttribute(1, "Field", "Name");
                    builder.AddAttribute(2, "FieldExpression", Utility.GenerateValueExpression(foo, "Name", typeof(string)));
                    builder.CloseComponent();
                });
                pb.Add(a => a.OnDeleteAsync, foos => Task.FromResult(true));
            });
        });

        var table = cut.FindComponent<Table<Foo>>();
        var deleteButton = table.FindComponent<TableToolbarPopconfirmButton<Foo>>();
        // 选一个
        var input = cut.Find("tbody tr input");
        await cut.InvokeAsync(() => input.Click());
        await cut.InvokeAsync(() => deleteButton.Instance.OnConfirm());

        table.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.PageItemsSource, new int[] { 1, 2, 4, 8 });
        });
        await cut.InvokeAsync(() => deleteButton.Instance.OnConfirm());
    }

    [Fact]
    public async Task ExportAsync_Ok()
    {
        var localizer = Context.Services.GetRequiredService<IStringLocalizer<Foo>>();
        var cut = Context.RenderComponent<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<Table<Foo>>(pb =>
            {
                pb.Add(a => a.RenderMode, TableRenderMode.Table);
                pb.Add(a => a.OnQueryAsync, OnQueryAsync(localizer));
                pb.Add(a => a.ShowToolbar, true);
                pb.Add(a => a.ShowExportButton, true);
                pb.Add(a => a.TableColumns, foo => builder =>
                {
                    builder.OpenComponent<TableColumn<Foo, string>>(0);
                    builder.AddAttribute(1, "Field", "Name");
                    builder.AddAttribute(2, "FieldExpression", Utility.GenerateValueExpression(foo, "Name", typeof(string)));
                    builder.CloseComponent();
                });
            });
        });

        var button = cut.Find(".dropdown-menu-end .dropdown-item");
        await cut.InvokeAsync(() => button.Click());

        // 
        var table = cut.FindComponent<Table<Foo>>();
        table.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.OnExportAsync, foos => Task.FromResult(true));
        });
        await cut.InvokeAsync(() => button.Click());
    }

    [Fact]
    public void TableRender_Ok()
    {
        var localizer = Context.Services.GetRequiredService<IStringLocalizer<Foo>>();
        var cut = Context.RenderComponent<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<MockTable>(pb =>
            {
                pb.Add(a => a.RenderMode, TableRenderMode.Table);
            });
        });

        var table = cut.FindComponent<MockTable>();
        Assert.Equal(TableRenderMode.Table, table.Instance.ShouldBeTable());
        Assert.Equal(TableRenderMode.CardView, table.Instance.ShouldBeCardView());
    }

    [Fact]
    public async Task EFCoreDataService_Ok()
    {
        var localizer = Context.Services.GetRequiredService<IStringLocalizer<Foo>>();
        var cut = Context.RenderComponent<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<Table<Foo>>(pb =>
            {
                pb.Add(a => a.RenderMode, TableRenderMode.Table);
                pb.Add(a => a.DataService, new MockEFCoreDataService(localizer));
                pb.Add(a => a.IsMultipleSelect, true);
                pb.Add(a => a.ShowToolbar, true);
                pb.Add(a => a.TableColumns, foo => builder =>
                {
                    builder.OpenComponent<TableColumn<Foo, string>>(0);
                    builder.AddAttribute(1, "Field", "Name");
                    builder.AddAttribute(2, "FieldExpression", Utility.GenerateValueExpression(foo, "Name", typeof(string)));
                    builder.CloseComponent();
                });
            });
        });
        var table = cut.FindComponent<Table<Foo>>();
        // 选一个
        var input = cut.Find("tbody tr input");
        await cut.InvokeAsync(() => input.Click());
        await cut.InvokeAsync(() => table.Instance.EditAsync());
    }

    [Fact]
    public async Task IsExcel_Ok()
    {
        var localizer = Context.Services.GetRequiredService<IStringLocalizer<Foo>>();
        var items = Foo.GenerateFoo(localizer, 2);
        var cut = Context.RenderComponent<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<Table<Foo>>(pb =>
            {
                pb.Add(a => a.RenderMode, TableRenderMode.Table);
                pb.Add(a => a.Items, items);
                pb.Add(a => a.IsExcel, true);
                pb.Add(a => a.IsMultipleSelect, true);
                pb.Add(a => a.ShowToolbar, true);
                pb.Add(a => a.TableColumns, foo => builder =>
                {
                    builder.OpenComponent<TableColumn<Foo, string>>(0);
                    builder.AddAttribute(1, "Field", "Name");
                    builder.AddAttribute(2, "FieldExpression", Utility.GenerateValueExpression(foo, "Name", typeof(string)));
                    builder.CloseComponent();
                });
            });
        });

        var table = cut.FindComponent<Table<Foo>>();
        await cut.InvokeAsync(() => table.Instance.AddAsync());

        var delete = cut.FindComponent<TableToolbarPopconfirmButton<Foo>>();
        await cut.InvokeAsync(() => delete.Instance.OnConfirm());
    }

    [Fact]
    public void IsExcel_Readonly()
    {
        var localizer = Context.Services.GetRequiredService<IStringLocalizer<Foo>>();
        var items = Foo.GenerateFoo(localizer, 2);
        var cut = Context.RenderComponent<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<Table<Foo>>(pb =>
            {
                pb.Add(a => a.RenderMode, TableRenderMode.Table);
                pb.Add(a => a.Items, items);
                pb.Add(a => a.IsExcel, true);
                pb.Add(a => a.TableColumns, foo => builder =>
                {
                    builder.OpenComponent<TableColumn<Foo, string>>(0);
                    builder.AddAttribute(1, "Field", "Name");
                    builder.AddAttribute(2, "FieldExpression", Utility.GenerateValueExpression(foo, "Name", typeof(string)));
                    builder.AddAttribute(1, "Readonly", true);
                    builder.CloseComponent();
                });
            });
        });
        cut.Contains("<div class=\"form-control is-display\"");
    }

    [Fact]
    public void IsExcel_EditTemplate()
    {
        var localizer = Context.Services.GetRequiredService<IStringLocalizer<Foo>>();
        var items = Foo.GenerateFoo(localizer, 2);
        var cut = Context.RenderComponent<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<Table<Foo>>(pb =>
            {
                pb.Add(a => a.RenderMode, TableRenderMode.Table);
                pb.Add(a => a.Items, items);
                pb.Add(a => a.IsExcel, true);
                pb.Add(a => a.TableColumns, foo => builder =>
                {
                    builder.OpenComponent<TableColumn<Foo, string>>(0);
                    builder.AddAttribute(1, "Field", "Name");
                    builder.AddAttribute(2, "FieldExpression", Utility.GenerateValueExpression(foo, "Name", typeof(string)));
                    builder.CloseComponent();
                });
            });
        });
    }

    [Fact]
    public async Task IsExcel_Dynamic()
    {
        var localizer = Context.Services.GetRequiredService<IStringLocalizer<Foo>>();
        var data = CreateDynamicContext(localizer);
        var cut = Context.RenderComponent<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<Table<DynamicObject>>(pb =>
            {
                pb.Add(a => a.RenderMode, TableRenderMode.Table);
                pb.Add(a => a.DynamicContext, data);
                pb.Add(a => a.IsExcel, true);
            });
        });
        cut.Contains("table table-excel");

        // trigger value changed
        var input = cut.Find("tbody tr input[type=\"text\"]");
        await cut.InvokeAsync(() => input.Change("test"));
        Assert.Equal("test", data.DataTable.Rows[0][0]);
    }

    [Fact]
    public void TreeNodeConverter_Ok()
    {
        var localizer = Context.Services.GetRequiredService<IStringLocalizer<Foo>>();
        var cut = Context.RenderComponent<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<Table<FooTree>>(pb =>
            {
                pb.Add(a => a.RenderMode, TableRenderMode.Table);
                pb.Add(a => a.IsTree, true);
                pb.Add(a => a.IndentSize, 32);
                pb.Add(a => a.OnQueryAsync, op => OnQueryAsync(op, localizer));
                pb.Add(a => a.TreeNodeConverter, items => BuildTreeAsync(items));
                pb.Add(a => a.TableColumns, foo => builder =>
                {
                    builder.OpenComponent<TableColumn<Foo, string>>(0);
                    builder.AddAttribute(1, "Field", "Name");
                    builder.AddAttribute(2, "FieldExpression", Utility.GenerateValueExpression(foo, "Name", typeof(string)));
                    builder.CloseComponent();
                });
            });
        });

        // 有子节点所以有两个 .is-node
        var nodes = cut.FindAll("tbody .is-tree .is-node");
        Assert.Equal(2, nodes.Count);
    }

    [Fact]
    public void OnAfterRenderCallback_Ok()
    {
        var callback = false;
        var localizer = Context.Services.GetRequiredService<IStringLocalizer<Foo>>();
        var cut = Context.RenderComponent<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<Table<Foo>>(pb =>
            {
                pb.Add(a => a.RenderMode, TableRenderMode.Table);
                pb.Add(a => a.OnQueryAsync, OnQueryAsync(localizer));
                pb.Add(a => a.OnAfterRenderCallback, table =>
                {
                    callback = true;
                    return Task.CompletedTask;
                });
                pb.Add(a => a.TableColumns, foo => builder =>
                {
                    builder.OpenComponent<TableColumn<Foo, string>>(0);
                    builder.AddAttribute(1, "Field", "Name");
                    builder.AddAttribute(2, "FieldExpression", Utility.GenerateValueExpression(foo, "Name", typeof(string)));
                    builder.CloseComponent();
                });
            });
        });

        Assert.True(callback);
    }

    [Fact]
    public async Task OnDoubleClickCellCallback_Ok()
    {
        var callback = false;
        var localizer = Context.Services.GetRequiredService<IStringLocalizer<Foo>>();
        var cut = Context.RenderComponent<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<Table<Foo>>(pb =>
            {
                pb.Add(a => a.RenderMode, TableRenderMode.Table);
                pb.Add(a => a.OnQueryAsync, OnQueryAsync(localizer));
                pb.Add(a => a.OnDoubleClickCellCallback, (colName, foo, val) =>
                {
                    callback = true;
                    return Task.CompletedTask;
                });
                pb.Add(a => a.TableColumns, foo => builder =>
                {
                    builder.OpenComponent<TableColumn<Foo, string>>(0);
                    builder.AddAttribute(1, "Field", "Name");
                    builder.AddAttribute(2, "FieldExpression", Utility.GenerateValueExpression(foo, "Name", typeof(string)));
                    builder.CloseComponent();
                });
            });
        });

        var cell = cut.Find("tbody tr td .is-dbcell");
        await cut.InvokeAsync(() => cell.DoubleClick());
        Assert.True(callback);
    }

    [Fact]
    public void TableSize_Ok()
    {
        var localizer = Context.Services.GetRequiredService<IStringLocalizer<Foo>>();
        var cut = Context.RenderComponent<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<Table<Foo>>(pb =>
            {
                pb.Add(a => a.RenderMode, TableRenderMode.Table);
                pb.Add(a => a.TableSize, TableSize.Normal);
                pb.Add(a => a.OnQueryAsync, OnQueryAsync(localizer));
                pb.Add(a => a.TableColumns, foo => builder =>
                {
                    builder.OpenComponent<TableColumn<Foo, string>>(0);
                    builder.AddAttribute(1, "Field", "Name");
                    builder.AddAttribute(2, "FieldExpression", Utility.GenerateValueExpression(foo, "Name", typeof(string)));
                    builder.CloseComponent();
                });
            });
        });
        cut.DoesNotContain("table-sm");

        var table = cut.FindComponent<Table<Foo>>();
        table.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.TableSize, TableSize.Compact);
        });
        cut.Contains("table-sm");
    }

    [Fact]
    public void EmptyImage_Ok()
    {
        var localizer = Context.Services.GetRequiredService<IStringLocalizer<Foo>>();
        var cut = Context.RenderComponent<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<Table<Foo>>(pb =>
            {
                pb.Add(a => a.RenderMode, TableRenderMode.Table);
                pb.Add(a => a.Items, new List<Foo>());
                pb.Add(a => a.ShowEmpty, true);
                pb.Add(a => a.EmptyImage, "/images/empty.jpg");
                pb.Add(a => a.TableColumns, foo => builder =>
                {
                    builder.OpenComponent<TableColumn<Foo, string>>(0);
                    builder.AddAttribute(1, "Field", "Name");
                    builder.AddAttribute(2, "FieldExpression", Utility.GenerateValueExpression(foo, "Name", typeof(string)));
                    builder.CloseComponent();
                });
            });
        });
        cut.Contains("/images/empty.jpg");
    }

    [Fact]
    public void EmptyTemplate_Ok()
    {
        var localizer = Context.Services.GetRequiredService<IStringLocalizer<Foo>>();
        var cut = Context.RenderComponent<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<Table<Foo>>(pb =>
            {
                pb.Add(a => a.RenderMode, TableRenderMode.Table);
                pb.Add(a => a.Items, new List<Foo>());
                pb.Add(a => a.ShowEmpty, true);
                pb.Add(a => a.EmptyTemplate, builder => builder.AddContent(0, "empty-test"));
                pb.Add(a => a.TableColumns, foo => builder =>
                {
                    builder.OpenComponent<TableColumn<Foo, string>>(0);
                    builder.AddAttribute(1, "Field", "Name");
                    builder.AddAttribute(2, "FieldExpression", Utility.GenerateValueExpression(foo, "Name", typeof(string)));
                    builder.CloseComponent();
                });
            });
        });
        cut.Contains("empty-test");
    }

    [Fact]
    public void IsBordered_Ok()
    {
        var localizer = Context.Services.GetRequiredService<IStringLocalizer<Foo>>();
        var cut = Context.RenderComponent<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<Table<Foo>>(pb =>
            {
                pb.Add(a => a.RenderMode, TableRenderMode.Table);
                pb.Add(a => a.OnQueryAsync, OnQueryAsync(localizer));
                pb.Add(a => a.IsBordered, true);
                pb.Add(a => a.TableColumns, foo => builder =>
                {
                    builder.OpenComponent<TableColumn<Foo, string>>(0);
                    builder.AddAttribute(1, "Field", "Name");
                    builder.AddAttribute(2, "FieldExpression", Utility.GenerateValueExpression(foo, "Name", typeof(string)));
                    builder.CloseComponent();
                });
            });
        });
        cut.Contains("table-bordered");
    }

    [Fact]
    public async Task AutoRefresh_Ok()
    {
        var index = 0;
        var localizer = Context.Services.GetRequiredService<IStringLocalizer<Foo>>();
        var cut = Context.RenderComponent<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<Table<Foo>>(pb =>
            {
                pb.Add(a => a.RenderMode, TableRenderMode.Table);
                pb.Add(a => a.OnQueryAsync, async op =>
                {
                    index++;
                    var data = await OnQueryAsync(localizer)(op);
                    return data;
                });
                pb.Add(a => a.IsAutoRefresh, true);
                pb.Add(a => a.AutoRefreshInterval, 600);
                pb.Add(a => a.TableColumns, foo => builder =>
                {
                    builder.OpenComponent<TableColumn<Foo, string>>(0);
                    builder.AddAttribute(1, "Field", "Name");
                    builder.AddAttribute(2, "FieldExpression", Utility.GenerateValueExpression(foo, "Name", typeof(string)));
                    builder.CloseComponent();
                });
            });
        });
        await Task.Delay(1200);
        Assert.True(index > 1);
    }

    [Fact]
    public async Task AutoRefresh_Cancel()
    {
        var localizer = Context.Services.GetRequiredService<IStringLocalizer<Foo>>();
        var cut = Context.RenderComponent<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<MockTable>(pb =>
            {
                pb.Add(a => a.RenderMode, TableRenderMode.Table);
                pb.Add(a => a.OnQueryAsync, OnQueryAsync(localizer));
                pb.Add(a => a.IsAutoRefresh, true);
                pb.Add(a => a.AutoRefreshInterval, 600);
                pb.Add(a => a.TableColumns, foo => builder =>
                {
                    builder.OpenComponent<TableColumn<Foo, string>>(0);
                    builder.AddAttribute(1, "Field", "Name");
                    builder.AddAttribute(2, "FieldExpression", Utility.GenerateValueExpression(foo, "Name", typeof(string)));
                    builder.CloseComponent();
                });
            });
        });

        var table = cut.FindComponent<MockTable>();
        await cut.InvokeAsync(() => table.Instance.TestLoopQueryAsync());
    }

    [Fact]
    public void HeaderStyle_Ok()
    {
        var localizer = Context.Services.GetRequiredService<IStringLocalizer<Foo>>();
        var cut = Context.RenderComponent<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<Table<Foo>>(pb =>
            {
                pb.Add(a => a.RenderMode, TableRenderMode.Table);
                pb.Add(a => a.Items, new List<Foo>());
                pb.Add(a => a.HeaderStyle, TableHeaderStyle.Light);
                pb.Add(a => a.TableColumns, foo => builder =>
                {
                    builder.OpenComponent<TableColumn<Foo, string>>(0);
                    builder.AddAttribute(1, "Field", "Name");
                    builder.AddAttribute(2, "FieldExpression", Utility.GenerateValueExpression(foo, "Name", typeof(string)));
                    builder.CloseComponent();
                });
            });
        });
        cut.Contains("table-light");

        var table = cut.FindComponent<Table<Foo>>();
        table.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.HeaderStyle, TableHeaderStyle.Dark);
        });
        cut.Contains("table-dark");
    }

    [Fact]
    public void OnColumnCreating_Ok()
    {
        var creating = false;
        var localizer = Context.Services.GetRequiredService<IStringLocalizer<Foo>>();
        var items = Foo.GenerateFoo(localizer, 2);
        var cut = Context.RenderComponent<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<Table<Foo>>(pb =>
            {
                pb.Add(a => a.RenderMode, TableRenderMode.Table);
                pb.Add(a => a.Items, items);
                pb.Add(a => a.OnColumnCreating, cols =>
                {
                    creating = true;
                    return Task.CompletedTask;
                });
                pb.Add(a => a.TableColumns, foo => builder =>
                {
                    builder.OpenComponent<TableColumn<Foo, string>>(0);
                    builder.AddAttribute(1, "Field", "Name");
                    builder.AddAttribute(2, "FieldExpression", Utility.GenerateValueExpression(foo, "Name", typeof(string)));
                    builder.CloseComponent();
                });
            });
        });
        Assert.True(creating);
    }

    [Fact]
    public void TableRenderMode_Ok()
    {
        var localizer = Context.Services.GetRequiredService<IStringLocalizer<Foo>>();
        var op = Context.Services.GetRequiredService<IOptionsMonitor<BootstrapBlazorOptions>>();
        op.CurrentValue.TableSettings.TableRenderMode = TableRenderMode.CardView;
        var cut = Context.RenderComponent<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<Table<Foo>>(pb =>
            {
                pb.Add(a => a.Items, new List<Foo>());
                pb.Add(a => a.TableColumns, foo => builder =>
                {
                    builder.OpenComponent<TableColumn<Foo, string>>(0);
                    builder.AddAttribute(1, "Field", "Name");
                    builder.AddAttribute(2, "FieldExpression", Utility.GenerateValueExpression(foo, "Name", typeof(string)));
                    builder.CloseComponent();
                });
            });
        });

        var table = cut.FindComponent<Table<Foo>>();
        Assert.Equal(TableRenderMode.CardView, table.Instance.RenderMode);
    }

    [Fact]
    public void AutoGenerateColumns_Ok()
    {
        var localizer = Context.Services.GetRequiredService<IStringLocalizer<Foo>>();
        var cut = Context.RenderComponent<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<Table<Foo>>(pb =>
            {
                pb.Add(a => a.Items, new List<Foo>());
                pb.Add(a => a.AutoGenerateColumns, true);
            });
        });

        var table = cut.FindComponent<Table<Foo>>();
        Assert.Equal(7, table.Instance.Columns.Count);
    }

    [Fact]
    public void CheckShownWithBreakpoint_Ok()
    {
        var localizer = Context.Services.GetRequiredService<IStringLocalizer<Foo>>();
        var cut = Context.RenderComponent<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<MockTable>(pb =>
            {
                pb.Add(a => a.AutoGenerateColumns, true);
            });
        });

        var table = cut.FindComponent<MockTable>();
        Assert.True(table.Instance.TestCheckShownWithBreakpoint(BreakPoint.Small, 1500));
        Assert.True(table.Instance.TestCheckShownWithBreakpoint(BreakPoint.Medium, 1500));
        Assert.True(table.Instance.TestCheckShownWithBreakpoint(BreakPoint.Large, 1500));
        Assert.True(table.Instance.TestCheckShownWithBreakpoint(BreakPoint.ExtraLarge, 1500));
        Assert.True(table.Instance.TestCheckShownWithBreakpoint(BreakPoint.ExtraExtraLarge, 1500));
        Assert.True(table.Instance.TestCheckShownWithBreakpoint(BreakPoint.ExtraSmall, 1500));
        Assert.True(table.Instance.TestCheckShownWithBreakpoint(BreakPoint.None, 1500));
    }

    [Fact]
    public async Task QueryItems_Null()
    {
        var localizer = Context.Services.GetRequiredService<IStringLocalizer<Foo>>();
        var cut = Context.RenderComponent<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<MockTable>(pb =>
            {
                pb.Add(a => a.DataService, new MockNullItemsDataService(localizer));
                pb.Add(a => a.AutoGenerateColumns, true);
            });
        });

        var table = cut.FindComponent<MockTable>();
        var items = await table.Instance.DataService!.QueryAsync(new QueryPageOptions());
        Assert.Null(items.Items);

        table.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.ScrollMode, ScrollMode.Virtual);
        });
        await cut.InvokeAsync(() => table.Instance.QueryAsync());
    }

    [Fact]
    public void GetValue_ColorPicker()
    {
        var localizer = Context.Services.GetRequiredService<IStringLocalizer<Foo>>();
        var cut = Context.RenderComponent<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<MockTable>(pb =>
            {
                pb.Add(a => a.Items, new List<Foo> { new Foo() { Name = null }, new Foo() { Name = "#fff" } });
                pb.Add(a => a.TableColumns, foo => builder =>
                {
                    builder.OpenComponent<TableColumn<Foo, string>>(0);
                    builder.AddAttribute(1, "Field", "Name");
                    builder.AddAttribute(2, "FieldExpression", Utility.GenerateValueExpression(foo, "Name", typeof(string)));
                    builder.AddAttribute(3, "ComponentType", typeof(ColorPicker));
                    builder.CloseComponent();
                });
            });
        });
        cut.Contains("is-color");
    }

    [Fact]
    public async Task GetValue_LookupServiceKey()
    {
        var localizer = Context.Services.GetRequiredService<IStringLocalizer<Foo>>();
        var cut = Context.RenderComponent<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<MockTable>(pb =>
            {
                pb.Add(a => a.OnQueryAsync, OnQueryAsync(localizer));
                pb.Add(a => a.TableColumns, foo => builder =>
                {
                    builder.OpenComponent<TableColumn<Foo, bool>>(0);
                    builder.AddAttribute(1, "Field", true);
                    builder.AddAttribute(2, "FieldExpression", Utility.GenerateValueExpression(foo, "Complete", typeof(bool)));
                    builder.AddAttribute(3, "LookupServiceKey", "test");
                    builder.CloseComponent();
                });
            });
        });

        var col = cut.FindComponent<TableColumn<Foo, bool>>();
        col.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.Lookup, new List<SelectedItem>()
            {
                new("True", "True"),
                new("False", "False")
            });
        });

        var table = cut.FindComponent<MockTable>();
        await cut.InvokeAsync(() => table.Instance.QueryAsync());
    }

    [Fact]
    public async Task Value_Formatter()
    {
        var localizer = Context.Services.GetRequiredService<IStringLocalizer<Foo>>();
        var cut = Context.RenderComponent<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<MockTable>(pb =>
            {
                pb.Add(a => a.Items, new List<Foo> { new Foo() { Count = 10 } });
                pb.Add(a => a.TableColumns, foo => builder =>
                {
                    builder.OpenComponent<TableColumn<Foo, int>>(0);
                    builder.AddAttribute(1, "Field", 10);
                    builder.AddAttribute(2, "FieldExpression", Utility.GenerateValueExpression(foo, "Count", typeof(int)));
                    builder.AddAttribute(3, "FormatString", "D3");
                    builder.CloseComponent();
                });
            });
        });
        cut.Contains("010");

        var col = cut.FindComponent<TableColumn<Foo, int>>();
        col.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.Formatter, new Func<object?, Task<string>>(obj =>
            {
                return Task.FromResult("test-formatter");
            }));
        });
        var table = cut.FindComponent<MockTable>();
        await cut.InvokeAsync(() => table.Instance.QueryAsync());
        cut.Contains("test-formatter");
    }

    [Fact]
    public void Value_Enum()
    {
        var localizer = Context.Services.GetRequiredService<IStringLocalizer<Foo>>();
        var cut = Context.RenderComponent<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<MockTable>(pb =>
            {
                pb.Add(a => a.Items, new List<Foo> { new Foo() { } });
                pb.Add(a => a.TableColumns, foo => builder =>
                {
                    builder.OpenComponent<TableColumn<Foo, EnumEducation?>>(0);
                    builder.AddAttribute(1, "Field", EnumEducation.Middle);
                    builder.AddAttribute(2, "FieldExpression", Utility.GenerateValueExpression(foo, "Education", typeof(EnumEducation?)));
                    builder.CloseComponent();
                });
            });
        });
        var table = cut.FindComponent<MockTable>();
        table.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.Items, new List<Foo> { new Foo() { Education = EnumEducation.Primary } });
        });
    }

    [Fact]
    public void Value_Datetime()
    {
        var localizer = Context.Services.GetRequiredService<IStringLocalizer<Foo>>();
        var cut = Context.RenderComponent<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<MockTable>(pb =>
            {
                pb.Add(a => a.Items, new List<Foo> { new Foo() { } });
                pb.Add(a => a.TableColumns, foo => builder =>
                {
                    builder.OpenComponent<TableColumn<Foo, DateTime?>>(0);
                    builder.AddAttribute(1, "Field", DateTime.Now);
                    builder.AddAttribute(2, "FieldExpression", Utility.GenerateValueExpression(foo, "DateTime", typeof(DateTime?)));
                    builder.CloseComponent();
                });
            });
        });
    }

    [Fact]
    public void Value_Enumerable()
    {
        var localizer = Context.Services.GetRequiredService<IStringLocalizer<Foo>>();
        var cut = Context.RenderComponent<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<MockTable>(pb =>
            {
                pb.Add(a => a.Items, new List<Foo> { new Foo() { Hobby = new string[] { "test-1", "test-2" } } });
                pb.Add(a => a.TableColumns, foo => builder =>
                {
                    builder.OpenComponent<TableColumn<Foo, IEnumerable<string>>>(0);
                    builder.AddAttribute(1, "Field", new string[] { "test-0" });
                    builder.AddAttribute(2, "FieldExpression", Utility.GenerateValueExpression(foo, "Hobby", typeof(IEnumerable<string>)));
                    builder.CloseComponent();
                });
            });
        });
        cut.Contains("test-1,test-2");
    }

    [Fact]
    public void RenderCell_CanWrite_False()
    {
        var cut = Context.RenderComponent<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<MockRenderCellTable>(pb =>
            {
                pb.Add(a => a.Items, new List<ReadonlyFoo> { new ReadonlyFoo() });
                pb.Add(a => a.TableColumns, foo => builder =>
                {
                    builder.OpenComponent<TableColumn<ReadonlyFoo, string>>(0);
                    builder.AddAttribute(1, "Field", "ReadonlyValue");
                    builder.AddAttribute(2, "FieldExpression", Utility.GenerateValueExpression(foo, "ReadonlyValue", typeof(string)));
                    builder.CloseComponent();
                });
            });
        });

        var table = cut.FindComponent<MockRenderCellTable>();
        var foo = new ReadonlyFoo();
        var cut1 = Context.Render(builder => builder.AddContent(0, table.Instance.TestRenderCell(foo, ItemChangedType.Add, col =>
        {

        })));
        Assert.Equal("<div class=\"form-control is-display\"></div>", cut1.Markup);
    }

    [Fact]
    public void RenderCell_Editable_False()
    {
        var cut = Context.RenderComponent<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<MockTable>(pb =>
            {
                pb.Add(a => a.Items, new List<Foo> { new ReadonlyFoo() });
                pb.Add(a => a.TableColumns, foo => builder =>
                {
                    builder.OpenComponent<TableColumn<Foo, string>>(0);
                    builder.AddAttribute(1, "Field", "ReadonlyValue");
                    builder.AddAttribute(2, "FieldExpression", Utility.GenerateValueExpression(foo, "Name", typeof(string)));
                    builder.AddAttribute(3, "Editable", false);
                    builder.AddAttribute(4, "Template", new RenderFragment<TableColumnContext<Foo, string>>(context => builder => builder.AddContent(0, "test-edittemplate")));
                    builder.CloseComponent();
                });
            });
        });

        var table = cut.FindComponent<MockTable>();
        var foo = new Foo();
        var cut1 = Context.Render(builder => builder.AddContent(0, table.Instance.TestRenderCell(foo, ItemChangedType.Add, col =>
        {
        })));
        Assert.Equal("test-edittemplate", cut1.Markup);
    }

    [Fact]
    public void TableColumnContext_Ok()
    {
        var context = new TableColumnContext<Foo, string>(new Foo() { Name = "Test" }, "Test-Value");
        Assert.Equal("Test", context.Row.Name);
        Assert.Equal("Test-Value", context.Value);
    }

    [Fact]
    public void TableColumnContext_Exception()
    {
        Assert.Throws<ArgumentNullException>(() => new TableColumnContext<Foo?, string>(null, "Test-Value"));
    }

    [Fact]
    public void PlaceHolder_Ok()
    {
        var cut = Context.RenderComponent<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<MockTable>(pb =>
            {
                pb.Add(a => a.ScrollMode, ScrollMode.Virtual);
                pb.Add(a => a.IsMultipleSelect, true);
                pb.Add(a => a.ShowExtendButtons, true);
                pb.Add(a => a.IsExtendButtonsInRowHeader, true);
                pb.Add(a => a.TableColumns, foo => builder =>
                {
                    builder.OpenComponent<TableColumn<Foo, string>>(0);
                    builder.AddAttribute(1, "Field", "ReadonlyValue");
                    builder.AddAttribute(2, "FieldExpression", Utility.GenerateValueExpression(foo, "Name", typeof(string)));
                    builder.CloseComponent();
                });
            });
        });

        var table = cut.FindComponent<MockTable>();
        var cut1 = Context.Render(table.Instance.RenderVirtualPlaceHolder());
        var tds = cut1.FindAll("td");
        Assert.Equal(3, tds.Count);

        table.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.IsExtendButtonsInRowHeader, false);
        });
        cut1 = Context.Render(table.Instance.RenderVirtualPlaceHolder());
        tds = cut1.FindAll("td");
        Assert.Equal(3, tds.Count);
    }

    [Fact]
    public void RenderCell_Editable_True()
    {
        var cut = Context.RenderComponent<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<MockTable>(pb =>
            {
                pb.Add(a => a.Items, new List<Foo> { new ReadonlyFoo() });
                pb.Add(a => a.TableColumns, foo => builder =>
                {
                    builder.OpenComponent<TableColumn<Foo, string>>(0);
                    builder.AddAttribute(1, "Field", "ReadonlyValue");
                    builder.AddAttribute(2, "FieldExpression", Utility.GenerateValueExpression(foo, "Name", typeof(string)));
                    builder.AddAttribute(3, "Editable", true);
                    builder.CloseComponent();
                });
            });
        });

        var table = cut.FindComponent<MockTable>();
        var foo = new Foo();
        var cut1 = Context.Render(builder => builder.AddContent(0, table.Instance.TestRenderCell(foo, ItemChangedType.Add, col =>
        {
        })));
        Assert.Contains("<input type=\"text\"", cut1.Markup);

        var col = cut.FindComponent<TableColumn<Foo, string>>();
        col.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.EditTemplate, foo => builder => builder.AddContent(0, "test-edittemplate"));
        });
        cut1 = Context.Render(builder => builder.AddContent(0, table.Instance.TestRenderCell(foo, ItemChangedType.Add, col =>
        {
        })));
        Assert.Contains("test-edittemplate", cut1.Markup);
    }

    [Fact]
    public async Task OnQuery_Save()
    {
        var localizer = Context.Services.GetRequiredService<IStringLocalizer<Foo>>();
        var cut = Context.RenderComponent<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<Table<Foo>>(pb =>
            {
                pb.Add(a => a.RenderMode, TableRenderMode.Table);
                pb.Add(a => a.EditMode, EditMode.InCell);
                pb.Add(a => a.ShowExtendButtons, true);
                pb.Add(a => a.OnQueryAsync, OnQueryAsync(localizer));
                pb.Add(a => a.TableColumns, foo => builder =>
                {
                    builder.OpenComponent<TableColumn<Foo, string>>(0);
                    builder.AddAttribute(1, "Field", "Name");
                    builder.AddAttribute(2, "FieldExpression", Utility.GenerateValueExpression(foo, "Name", typeof(string)));
                    builder.CloseComponent();
                });
            });
        });

        // trigger edit button
        var button = cut.Find("button");
        await cut.InvokeAsync(() => button.Click());

        // trigger update button
        button = cut.Find("button");
        await cut.InvokeAsync(() => button.Click());
    }

    [Fact]
    public async Task OnQuery_Delete()
    {
        var localizer = Context.Services.GetRequiredService<IStringLocalizer<Foo>>();
        var cut = Context.RenderComponent<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<MockTable>(pb =>
            {
                pb.Add(a => a.RenderMode, TableRenderMode.Table);
                pb.Add(a => a.EditMode, EditMode.InCell);
                pb.Add(a => a.ShowExtendButtons, true);
                pb.Add(a => a.OnQueryAsync, OnQueryAsync(localizer));
                pb.Add(a => a.TableColumns, foo => builder =>
                {
                    builder.OpenComponent<TableColumn<Foo, string>>(0);
                    builder.AddAttribute(1, "Field", "Name");
                    builder.AddAttribute(2, "FieldExpression", Utility.GenerateValueExpression(foo, "Name", typeof(string)));
                    builder.CloseComponent();
                });
            });
        });

        // trigger delete button
        var table = cut.FindComponent<MockTable>();
        await cut.InvokeAsync(() => table.Instance.TestDeleteAsync());
    }

    [Fact]
    public async Task OnQuery_Add()
    {
        var localizer = Context.Services.GetRequiredService<IStringLocalizer<Foo>>();
        var cut = Context.RenderComponent<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<Table<Foo>>(pb =>
            {
                pb.Add(a => a.RenderMode, TableRenderMode.Table);
                pb.Add(a => a.EditMode, EditMode.InCell);
                pb.Add(a => a.ShowExtendButtons, true);
                pb.Add(a => a.OnQueryAsync, OnQueryAsync(localizer));
                pb.Add(a => a.TableColumns, foo => builder =>
                {
                    builder.OpenComponent<TableColumn<Foo, string>>(0);
                    builder.AddAttribute(1, "Field", "Name");
                    builder.AddAttribute(2, "FieldExpression", Utility.GenerateValueExpression(foo, "Name", typeof(string)));
                    builder.CloseComponent();
                });
            });
        });

        // trigger delete button
        var table = cut.FindComponent<Table<Foo>>();
        await cut.InvokeAsync(() => table.Instance.AddAsync());
    }

    [Fact]
    public void IsAutoCollapsedToolbarButton_Ok()
    {
        var localizer = Context.Services.GetRequiredService<IStringLocalizer<Foo>>();
        var cut = Context.RenderComponent<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<Table<Foo>>(pb =>
            {
                pb.Add(a => a.RenderMode, TableRenderMode.CardView);
                pb.Add(a => a.IsAutoCollapsedToolbarButton, false);
                pb.Add(a => a.ShowToolbar, true);
                pb.Add(a => a.ShowExtendButtons, true);
                pb.Add(a => a.OnQueryAsync, OnQueryAsync(localizer));
                pb.Add(a => a.TableColumns, foo => builder =>
                {
                    builder.OpenComponent<TableColumn<Foo, string>>(0);
                    builder.AddAttribute(1, "Field", "Name");
                    builder.AddAttribute(2, "FieldExpression", Utility.GenerateValueExpression(foo, "Name", typeof(string)));
                    builder.CloseComponent();
                });
            });
        });

        Assert.DoesNotContain("btn-gear", cut.Markup);
        Assert.Contains("btn-toolbar btn-group", cut.Markup);
    }

    [Fact]
    public void OnSelectedRows_Ok()
    {
        var localizer = Context.Services.GetRequiredService<IStringLocalizer<Foo>>();
        var items = Foo.GenerateFoo(localizer, 2);
        var context = CreateDynamicContext(localizer);
        var rows = context.GetItems().Take(1);
        var cut = Context.RenderComponent<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<Table<DynamicObject>>(pb =>
            {
                pb.Add(a => a.ShowToolbar, true);
                pb.Add(a => a.RenderMode, TableRenderMode.Table);
                pb.Add(a => a.IsMultipleSelect, true);
                pb.Add(a => a.DynamicContext, context);
                pb.Add(a => a.ModelEqualityComparer, (x, y) => x.GetValue("Id")?.ToString() == y.GetValue("Id")?.ToString());
                pb.Add(a => a.SelectedRows, rows.Cast<DynamicObject>().ToList());
            });
        });

        var check = cut.FindComponents<Checkbox<DynamicObject>>().FirstOrDefault(i => i.Instance.State == CheckboxState.Checked);
        Assert.NotNull(check);
    }

    private static DataTable CreateDataTable(IStringLocalizer<Foo> localizer)
    {
        var userData = new DataTable();
        userData.Columns.Add(nameof(Foo.Name), typeof(string));
        userData.Columns.Add("Id", typeof(int));

        var index = 0;
        Foo.GenerateFoo(localizer, 2).ForEach(f =>
        {
            userData.Rows.Add(f.Name, index++);
        });

        return userData;
    }

    public static DataTableDynamicContext CreateDynamicContext(IStringLocalizer<Foo> localizer)
    {
        var UserData = CreateDataTable(localizer);
        return new DataTableDynamicContext(UserData, (context, col) =>
        {
            var propertyName = col.GetFieldName();
            // 使用 Text 设置显示名称示例
            col.Text = localizer[nameof(Foo.Name)];
        });
    }

    private static Func<QueryPageOptions, Task<QueryData<Foo>>> OnQueryAsync(IStringLocalizer<Foo> localizer, bool isSearch = true, bool isAdvanceSearch = true, bool isFilter = true, bool isSorted = true) => new(op =>
    {
        var items = Foo.GenerateFoo(localizer, 5);
        return Task.FromResult(new QueryData<Foo>()
        {
            Items = items,
            TotalCount = items.Count,
            IsAdvanceSearch = isAdvanceSearch,
            IsFiltered = isFilter,
            IsSearch = isSearch,
            IsSorted = isSorted
        });
    });

    private class MockNullDataService : IDataService<Foo>
    {
        IStringLocalizer<Foo> Localizer { get; set; }

        public MockNullDataService(IStringLocalizer<Foo> localizer) => Localizer = localizer;

        public Task<bool> AddAsync(Foo model) => Task.FromResult(true);

        public Task<bool> DeleteAsync(IEnumerable<Foo> models) => Task.FromResult(true);

        public virtual Task<QueryData<Foo>> QueryAsync(QueryPageOptions option)
        {
            var foos = Foo.GenerateFoo(Localizer, 2);
            return Task.FromResult(new QueryData<Foo>()
            {
                Items = foos,
                TotalCount = 2,
                IsAdvanceSearch = true,
                IsFiltered = true,
                IsSearch = true,
                IsSorted = true
            });
        }

        public Task<bool> SaveAsync(Foo model, ItemChangedType changedType) => Task.FromResult(true);
    }

    private class MockEFCoreDataService : MockNullDataService, IEntityFrameworkCoreDataService
    {
        public MockEFCoreDataService(IStringLocalizer<Foo> localizer) : base(localizer)
        {

        }

        public Task CancelAsync() => Task.CompletedTask;

        public Task EditAsync(object model) => Task.CompletedTask;
    }

    private class MockNullItemsDataService : MockNullDataService
    {
        public MockNullItemsDataService(IStringLocalizer<Foo> localizer) : base(localizer)
        {

        }

        public override Task<QueryData<Foo>> QueryAsync(QueryPageOptions option)
        {
            return Task.FromResult(new QueryData<Foo>()
            {
                Items = null!,
                TotalCount = 2,
                IsAdvanceSearch = true,
                IsFiltered = true,
                IsSearch = true,
                IsSorted = true
            });
        }
    }

    private class MockButton : ButtonBase
    {
        [CascadingParameter]
        [NotNull]
        protected TableExtensionButton? Buttons { get; set; }

        protected override void OnInitialized()
        {
            Buttons.AddButton(this);
        }

        /// <summary>
        /// DisposeAsyncCore
        /// </summary>
        /// <param name="disposing"></param>
        /// <returns></returns>
        protected override ValueTask DisposeAsyncCore(bool disposing)
        {
            Buttons?.RemoveButton(this);
            return base.DisposeAsyncCore(disposing);
        }
    }

    private class MockToolbarButton<TItem> : ButtonBase
    {
        [CascadingParameter]
        [NotNull]
        protected TableToolbar<TItem>? Buttons { get; set; }

        protected override void OnInitialized()
        {
            Buttons.AddButton(this);
        }
    }

    private class MockComplexFoo
    {
        public string? Name { get; set; }

        public Foo Foo { get; set; } = new Foo();
    }

    private class FooTree : Foo
    {
        public int ParentId { get; set; }

        public static List<FooTree> Generate(IStringLocalizer<Foo> localizer, int parentId = 0, int seed = 0) => Enumerable.Range(1, 2).Select(i => new FooTree()
        {
            Id = i + seed,
            ParentId = parentId,
            Name = localizer["Foo.Name", $"{seed:d2}{(i + seed):d2}"],
            DateTime = System.DateTime.Now.AddDays(i - 1),
            Address = localizer["Foo.Address", $"{Random.Next(1000, 2000)}"],
            Count = Random.Next(1, 100),
            Complete = Random.Next(1, 100) > 50,
            Education = Random.Next(1, 100) > 50 ? EnumEducation.Primary : EnumEducation.Middle
        }).ToList();
    }

    private class FooNoKeyTree : FooTree
    {
        [Display(Name = "主键")]
        [AutoGenerateColumn(Ignore = true)]
        public new int Id { get; set; }

        public static IEnumerable<FooNoKeyTree> Generate(IStringLocalizer<Foo> localizer, int seed = 0) => Enumerable.Range(1, 2).Select(i => new FooNoKeyTree()
        {
            Id = i + seed,
            Name = localizer["Foo.Name", $"{seed:d2}{(i + seed):d2}"],
            DateTime = System.DateTime.Now.AddDays(i - 1),
            Address = localizer["Foo.Address", $"{Random.Next(1000, 2000)}"],
            Count = Random.Next(1, 100),
            Complete = Random.Next(1, 100) > 50,
            Education = Random.Next(1, 100) > 50 ? EnumEducation.Primary : EnumEducation.Middle
        }).ToList();
    }

    private class ReadonlyFoo : Foo
    {
        public string? ReadonlyValue { get; }
    }

    private class Cat
    {
        [CatKey]
        public int Id { get; set; }

        public string? Name { get; set; }
    }

    [AttributeUsage(AttributeTargets.Property)]
    private class CatKeyAttribute : Attribute
    {

    }

    private class FooSearchModel : ITableSearchModel
    {
        public string? Name { get; set; }

        public string? Count { get; set; }

        public IEnumerable<IFilterAction> GetSearchs()
        {
            var ret = new List<IFilterAction>();
            if (!string.IsNullOrEmpty(Name))
            {
                ret.Add(new SearchFilterAction(nameof(Foo.Name), Name));
            }

            if (!string.IsNullOrEmpty(Count))
            {
                if (Count == "1")
                {
                    ret.Add(new SearchFilterAction(nameof(Foo.Count), 30, FilterAction.LessThan));
                }
                else if (Count == "2")
                {
                    ret.Add(new SearchFilterAction(nameof(Foo.Count), 30, FilterAction.GreaterThanOrEqual));
                    ret.Add(new SearchFilterAction(nameof(Foo.Count), 70, FilterAction.LessThan));
                }
                else if (Count == "3")
                {
                    ret.Add(new SearchFilterAction(nameof(Foo.Count), 70, FilterAction.GreaterThanOrEqual));
                    ret.Add(new SearchFilterAction(nameof(Foo.Count), 100, FilterAction.LessThan));
                }
            }
            return ret;
        }

        public void Reset()
        {
            Name = null;
            Count = null;
        }
    }

    private class MockFilterAction : IFilterAction
    {
        public IEnumerable<FilterKeyValueAction> GetFilterConditions() => new FilterKeyValueAction[]
        {
            new FilterKeyValueAction()
            {
                 FieldKey ="Name",
                 FieldValue = "Zhang"
            }
        };

        public void Reset()
        {

        }

        public Task SetFilterConditionsAsync(IEnumerable<FilterKeyValueAction> conditions) => Task.CompletedTask;
    }

    private class MockTable : Table<Foo>
    {
        public TableRenderMode ShouldBeTable()
        {
            ScreenSize = 10;
            RenderModeResponsiveWidth = 5;
            RenderMode = TableRenderMode.Auto;
            return base.ActiveRenderMode;
        }

        public TableRenderMode ShouldBeCardView()
        {
            // ScreenSize < RenderModeResponsiveWidth ? TableRenderMode.CardView : TableRenderMode.Table
            ScreenSize = 1;
            RenderModeResponsiveWidth = 5;
            RenderMode = TableRenderMode.Auto;
            return base.ActiveRenderMode;
        }

        public bool TestCheckShownWithBreakpoint(BreakPoint point, decimal screenSize)
        {
            var col = new AutoGenerateColumnAttribute() { ShownWithBreakPoint = point };
            ScreenSize = screenSize;
            return CheckShownWithBreakpoint(col);
        }

        public RenderFragment TestRenderCell(Foo item, ItemChangedType changedType, Action<ITableColumn> callback)
        {
            var col = Columns[0];
            callback(col);
            return RenderCell(col, item, changedType);
        }

        public void OnFilterClick()
        {
            var col = Columns[0];
            OnFilterClick(col);
        }

        public async Task TestLoopQueryAsync()
        {
            AutoRefreshCancelTokenSource = new CancellationTokenSource();
            AutoRefreshInterval = 2000;
            _ = LoopQueryAsync();
            await Task.Delay(200);
            AutoRefreshCancelTokenSource.Cancel();
        }

        public RenderFragment RenderVirtualPlaceHolder() => new(builder =>
        {
            if (ScrollMode == ScrollMode.Virtual && VirtualizeElement != null)
            {
                builder.AddContent(0, VirtualizeElement.Placeholder?.Invoke(new Microsoft.AspNetCore.Components.Web.Virtualization.PlaceholderContext()));
            }
        });

        public async Task TestDeleteAsync()
        {
            SelectedRows.Add(Rows[0]);
            await DeleteAsync();
        }
    }

    private class MockRenderCellTable : Table<ReadonlyFoo>
    {
        public RenderFragment TestRenderCell(ReadonlyFoo item, ItemChangedType changedType, Action<ITableColumn> callback)
        {
            var col = Columns[0];
            callback(col);
            return RenderCell(col, item, changedType);
        }
    }

    private class MockDynamicTable : Table<DynamicObject>
    {
        public async Task<bool> SaveModelTest()
        {
            var context = new EditContext(SelectedRows[0]);
            return await base.SaveModelAsync(context, ItemChangedType.Update);
        }
    }

    private class MockTreeTable<TItem> : Table<TItem> where TItem : class, new()
    {
        public bool TestComparerItem(TItem a, TItem b)
        {
            return ComparerItem(a, b);
        }
    }

    private class Dummy : IEqualityComparer<Dummy>
    {
        public int Id { get; set; }

        public bool Equals(Dummy? x, Dummy? y)
        {
            var ret = false;
            if (x != null && y != null)
            {
                ret = x.Id == y.Id;
            }
            return ret;
        }

        public int GetHashCode([DisallowNull] Dummy obj) => obj.GetHashCode();
    }

    private class Dog
    {
        public int Id { get; set; }

        public override bool Equals(object? obj)
        {
            var ret = false;
            if (obj is Dog d)
            {
                ret = d.Id == Id;
            }
            return ret;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
