// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Shared;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;

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
        await cut.InvokeAsync(() => pager.Instance.OnPageItemsChanged!.Invoke(2));
        var activePage = cut.Find(".page-item.active");
        Assert.Equal("1", activePage.TextContent);

        await cut.InvokeAsync(() => pager.Instance.OnPageClick!.Invoke(2, 2));
        activePage = cut.Find(".page-item.active");
        Assert.Equal("2", activePage.TextContent);
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

        cut.Contains("left: 300px;");

        if (showExtendButton)
        {
            cut.Contains("right: 230px;");
            cut.Contains("right: 130px;");
        }
        if (!showExtendButton)
        {
            cut.Contains("right: 300px;");
            cut.Contains("right: 100px;");
        }
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
        table.SetParametersAndRender(pb => pb.Add(a => a.Items, new Foo[] { }));
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

    private static Func<QueryPageOptions, Task<QueryData<Foo>>> OnQueryAsync(IStringLocalizer<Foo> localizer) => new(op =>
    {
        var items = Foo.GenerateFoo(localizer, 5);
        return Task.FromResult(new QueryData<Foo>()
        {
            Items = items,
            TotalCount = items.Count,
            IsAdvanceSearch = true,
            IsFiltered = true,
            IsSearch = true,
            IsSorted = true
        });
    });

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
}
