// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Shared;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using System.Data;

namespace UnitTest.Components;

public class TableTest : TableTestBase
{
    [Fact]
    public void Table_Ok()
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
                pb.Add(a => a.SearchDialogShowMaximizeButton, true);
                pb.Add(a => a.SearchDialogItemsPerRow, 2);
                pb.Add(a => a.SearchDialogRowType, RowType.Inline);
                pb.Add(a => a.SearchDialogLabelAlign, Alignment.Right);
                pb.Add(a => a.ShowAdvancedSearch, true);
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
    public void ShowToolbar_IsExcel_Ok()
    {
        var localizer = Context.Services.GetRequiredService<IStringLocalizer<Foo>>();
        var cut = Context.RenderComponent<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<Table<Foo>>(pb =>
            {
                pb.Add(a => a.IsExcel, true);
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
    public void ShowColumnList_Ok()
    {
        var localizer = Context.Services.GetRequiredService<IStringLocalizer<Foo>>();
        var cut = Context.RenderComponent<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<Table<Foo>>(pb =>
            {
                pb.Add(a => a.ShowToolbar, true);
                pb.Add(a => a.ShowColumnList, true);
                pb.Add(a => a.ColumnButtonText, "Test_Column_List");
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
        cut.Contains("Test_Column_List");
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
        Assert.Equal(20, table.Instance.PageItems);

        table.SetParametersAndRender(pb => pb.Add(a => a.PageItemsSource, new int[] { 4, 6, 8 }));
        Assert.Equal(4, table.Instance.PageItems);

        table.SetParametersAndRender(pb => pb.Add(a => a.PageItemsSource, null));
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

    [Theory]
    [InlineData(EditMode.EditForm)]
    [InlineData(EditMode.InCell)]
    public async Task EditForm_Ok(EditMode mode)
    {
        var localizer = Context.Services.GetRequiredService<IStringLocalizer<Foo>>();
        var cut = Context.RenderComponent<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<Table<Foo>>(pb =>
            {
                pb.Add(a => a.ShowToolbar, true);
                pb.Add(a => a.IsMultipleSelect, true);
                pb.Add(a => a.ShowLineNo, true);
                pb.Add(a => a.DetailRowTemplate, foo => builder => builder.AddContent(0, foo.Name));
                pb.Add(a => a.RenderMode, TableRenderMode.Table);
                pb.Add(a => a.EditMode, mode);
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

        // 获得 Add 按钮
        var btnAdd = cut.FindComponent<TableToolbarButton<Foo>>();
        await cut.InvokeAsync(async () =>
        {
            if (btnAdd.Instance.OnClick.HasDelegate)
            {
                await btnAdd.Instance.OnClick.InvokeAsync();
            }
        });
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
    public async Task IsTracking_Ok()
    {
        var localizer = Context.Services.GetRequiredService<IStringLocalizer<Foo>>();
        var cut = Context.RenderComponent<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<Table<Foo>>(pb =>
            {
                pb.Add(a => a.ShowToolbar, true);
                pb.Add(a => a.IsTracking, true);
                pb.Add(a => a.RenderMode, TableRenderMode.Table);
                pb.Add(a => a.EditMode, EditMode.EditForm);
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

        // 获得 Add 按钮
        var btnAdd = cut.FindComponent<TableToolbarButton<Foo>>();
        await cut.InvokeAsync(async () =>
        {
            if (btnAdd.Instance.OnClick.HasDelegate)
            {
                await btnAdd.Instance.OnClick.InvokeAsync();
            }
        });
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

        if (isExcel)
        {
            cut.DoesNotContain("is-master");
        }
        else
        {
            cut.Contains("table-cell is-bar");
            var btn = cut.Find(".is-master .is-bar > i");
            await cut.InvokeAsync(() => btn.Click());
            Assert.True(showDetail);
        }
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
                    builder.CloseComponent();
                });
                pb.Add(a => a.SortIcon, "fa fa-sort");
            });
        });
        cut.Contains("fa fa-sort");

        var th = cut.Find("th");
        await cut.InvokeAsync(() => th.Click());
        Assert.True(sorted);
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
    public void ShowDetailRow_Ok()
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
        cut.Contains("fa-caret-right");
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
    public void IsTree_Ok()
    {
        var localizer = Context.Services.GetRequiredService<IStringLocalizer<Foo>>();
        var cut = Context.RenderComponent<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<Table<FooTree>>(pb =>
            {
                pb.Add(a => a.RenderMode, TableRenderMode.Table);
                pb.Add(a => a.IsTree, true);
                pb.Add(a => a.Items, FooTree.Generate(localizer));
                pb.Add(a => a.OnTreeExpand, foo => Task.FromResult(FooTree.Generate(localizer).AsEnumerable()));
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
    public async Task InCell_Tracking()
    {
        var localizer = Context.Services.GetRequiredService<IStringLocalizer<Foo>>();
        var cut = Context.RenderComponent<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<Table<Foo>>(pb =>
            {
                pb.Add(a => a.RenderMode, TableRenderMode.Table);
                pb.Add(a => a.EditMode, EditMode.InCell);
                pb.Add(a => a.ShowExtendButtons, true);
                pb.Add(a => a.IsTracking, true);
                pb.Add(a => a.BeforeRowButtonTemplate, foo => builder => builder.AddContent(0, "test-BeforeRowButtonTemplate"));
                pb.Add(a => a.RowButtonTemplate, foo => builder => builder.AddContent(0, "test-RowButtonTemplate"));
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
        cut.Contains("test-BeforeRowButtonTemplate");
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
                    builder.AddAttribute(25, "LookUpServiceKey", "test");
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
        Assert.Equal("test", column.Instance.LookUpServiceKey);
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
                pb.Add(a => a.AutoGenerateColumns, false);
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

        // test edit button
        var button = cut.FindAll("tbody tr button");
        await cut.InvokeAsync(() => button[0].Click());

        var update = cut.Find("tbody tr button");
        await cut.InvokeAsync(() => update.Click());
        Assert.Equal(ItemChangedType.Update, itemChagned);
    }

    [Fact]
    public async Task OnAddAsync_Ok()
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
                pb.Add(a => a.EditMode, EditMode.InCell);
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

        // test update button
        var update = cut.Find("tbody tr button");
        await cut.InvokeAsync(() => update.Click());
        Assert.Equal(ItemChangedType.Add, itemChagned);
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public async Task OnEditAsync_Ok(bool tracking)
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
                pb.Add(a => a.IsTracking, tracking);
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
    public void DynamicContext_Ok()
    {
        var localizer = Context.Services.GetRequiredService<IStringLocalizer<Foo>>();
        var items = Foo.GenerateFoo(localizer, 2);
        var cut = Context.RenderComponent<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<Table<DynamicObject>>(pb =>
            {
                pb.Add(a => a.SortString, "Name desc");
                pb.Add(a => a.DynamicContext, CreateDynamicContext(localizer));
            });
        });
    }

    [Fact]
    public void CustomerSearchs_Ok()
    {
        var localizer = Context.Services.GetRequiredService<IStringLocalizer<Foo>>();
        var cut = Context.RenderComponent<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<Table<Foo>>(pb =>
            {
                pb.Add(a => a.RenderMode, TableRenderMode.Table);
                pb.Add(a => a.OnQueryAsync, op =>
                {
                    op.CustomerSearchs = new List<IFilterAction>() { new MockFilterAction() };
                    op.Filters = new List<IFilterAction>() { new MockFilterAction() };
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

    private static DataTable CreateDataTable(IStringLocalizer<Foo> localizer)
    {
        var userData = new DataTable();
        userData.Columns.Add(nameof(Foo.Name), typeof(string));

        Foo.GenerateFoo(localizer, 2).ForEach(f =>
        {
            userData.Rows.Add(f.Name);
        });

        return userData;
    }

    private static DataTableDynamicContext CreateDynamicContext(IStringLocalizer<Foo> localizer)
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

        public Task<QueryData<Foo>> QueryAsync(QueryPageOptions option)
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

    private class MockButton : ButtonBase
    {
        [CascadingParameter]
        [NotNull]
        protected TableExtensionButton? Buttons { get; set; }

        protected override void OnInitialized()
        {
            Buttons.AddButton(this);
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
        private static readonly Random random = new();

        public IEnumerable<FooTree>? Children { get; set; }

        public bool HasChildren { get; set; }

        public static IEnumerable<FooTree> Generate(IStringLocalizer<Foo> localizer, bool hasChildren = true, int seed = 0) => Enumerable.Range(1, 2).Select(i => new FooTree()
        {
            Id = i + seed,
            Name = localizer["Foo.Name", $"{seed:d2}{(i + seed):d2}"],
            DateTime = System.DateTime.Now.AddDays(i - 1),
            Address = localizer["Foo.Address", $"{random.Next(1000, 2000)}"],
            Count = random.Next(1, 100),
            Complete = random.Next(1, 100) > 50,
            Education = random.Next(1, 100) > 50 ? EnumEducation.Primary : EnumEducation.Middel,
            HasChildren = hasChildren
        }).ToList();
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
}
