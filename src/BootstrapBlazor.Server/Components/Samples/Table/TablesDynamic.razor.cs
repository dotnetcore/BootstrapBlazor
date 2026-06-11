// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using System.Data;

namespace BootstrapBlazor.Server.Components.Samples.Table;

/// <summary>
/// 动态表格示例代码
/// </summary>
public partial class TablesDynamic
{
    private DataTableDynamicContext? _dataTableDynamicContext1;
    private DataTableDynamicContext? _dataTableDynamicContext2;
    private DataTableDynamicContext? _dataTableDynamicContext3;
    private DataTableDynamicContext? _dataTableDynamicContext4;
    private List<DynamicObject> _selectedItems = [];
    private string? _buttonAddColumnText;
    private string? _buttonRemoveColumnText;

    /// <summary>
    /// OnInitialized 方法
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        _buttonAddColumnText ??= Localizer["TablesDynamicDynamicColButtonAddColumnText"];
        _buttonRemoveColumnText ??= Localizer["TablesDynamicDynamicColButtonRemoveColumnText"];

        InitDataTableContext();
    }

    private DataTable CreateDataTable()
    {
        var dataTable = new DataTable();
        dataTable.Columns.Add(nameof(Foo.Id), typeof(int));
        dataTable.Columns.Add(nameof(Foo.DateTime), typeof(DateTime));
        dataTable.Columns.Add(nameof(Foo.Name), typeof(string));
        dataTable.Columns.Add(nameof(Foo.Count), typeof(int));
        dataTable.PrimaryKey =
        [
            dataTable.Columns[0]
        ];
        dataTable.Columns[0].AutoIncrement = true;
        Foo.GenerateFoo(FooLocalizer, 10).ForEach(f => { dataTable.Rows.Add(f.Id, f.DateTime, f.Name, f.Count); });
        dataTable.AcceptChanges();

        return dataTable;
    }

    private void InitDataTableContext()
    {
        var table = CreateDataTable();
        _dataTableDynamicContext1 = CreateContext(table);

        table = CreateDataTable();
        _dataTableDynamicContext2 = CreateContext(table);

        table = CreateDataTable();
        _dataTableDynamicContext3 = CreateContext(table);

        CreatePageDataTable();
        _dataTableDynamicContext4 = CreatePageDataContext();
    }

    private static bool ModelEqualityComparer(IDynamicObject x, IDynamicObject y) =>
        x.GetValue("Id")?.ToString() == y.GetValue("Id")?.ToString();

    private DataTableDynamicContext CreateContext(DataTable table) => new DataTableDynamicContext(table,
        (context, col) =>
        {
            var propertyName = col.GetFieldName();
            // 使用 Text 设置显示名称示例
            col.Text = FooLocalizer[propertyName];
            if (propertyName == nameof(Foo.DateTime))
            {
                context.AddRequiredAttribute(nameof(Foo.DateTime));
                // 使用 AutoGenerateColumnAttribute 设置显示名称示例
                context.AddAutoGenerateColumnAttribute(nameof(Foo.DateTime), [
                    new KeyValuePair<string, object?>(nameof(AutoGenerateColumnAttribute.Text),
                        FooLocalizer[nameof(Foo.DateTime)].Value)
                ]);
            }
            else if (propertyName == nameof(Foo.Name))
            {
                context.AddRequiredAttribute(nameof(Foo.Name), FooLocalizer["Name.Required"]);
            }
            else if (propertyName == nameof(Foo.Count))
            {
                context.AddRequiredAttribute(nameof(Foo.Count));
                // 使用 DisplayNameAttribute 设置显示名称示例
                context.AddDisplayNameAttribute(nameof(Foo.Count), FooLocalizer[nameof(Foo.Count)].Value);
            }
            else if (propertyName == nameof(Foo.Complete))
            {
                col.Filterable = true;
                // 使用 DisplayAttribute 设置显示名称示例
                context.AddDisplayAttribute(nameof(Foo.Complete), [
                    new KeyValuePair<string, object?>(nameof(DisplayAttribute.Name),
                        FooLocalizer[nameof(Foo.Complete)].Value)
                ]);
            }
            else if (propertyName == nameof(Foo.Id))
            {
                col.Ignore = true;
            }
        })
    {
        OnDeleteAsync = items =>
        {
            // 数据源中移除
            foreach (var item in items)
            {
                var id = item.GetValue(nameof(Foo.Id));
                if (id != null)
                {
                    var row = table.Rows.Find(id);
                    if (row != null)
                    {
                        table.Rows.Remove(row);
                    }
                }
            }

            table.AcceptChanges();
            return Task.FromResult(true);
        },
        OnChanged = args =>
        {
            if (args.ChangedType == DynamicItemChangedType.Add)
            {
                var item = args.Items.First();
                item.SetValue(nameof(Foo.DateTime), DateTime.Today);
                item.SetValue(nameof(Foo.Name), Localizer["TablesDynamicNewValueText"].Value);
            }

            return Task.CompletedTask;
        }
    };

    private Task OnAddColumn()
    {
        var table = _dataTableDynamicContext3!.DataTable;
        if (!table.Columns.Contains(nameof(Foo.Complete)))
        {
            table.Columns.Add(nameof(Foo.Complete), typeof(bool));

            // 更新数据
            var fs = Foo.GenerateFoo(FooLocalizer, 10);
            for (var i = 0; i < fs.Count; i++)
            {
                table.Rows[i][nameof(Foo.Complete)] = fs[i].Complete;
            }
            table.AcceptChanges();
            _dataTableDynamicContext3 = CreateContext(table);
            StateHasChanged();
        }

        return Task.CompletedTask;
    }

    private Task OnRemoveColumn()
    {
        var table = _dataTableDynamicContext3!.DataTable;
        if (table.Columns.Contains(nameof(Foo.Complete)))
        {
            table.Columns.Remove(nameof(Foo.Complete));
            table.AcceptChanges();
            _dataTableDynamicContext3 = CreateContext(table);
            StateHasChanged();
        }

        return Task.CompletedTask;
    }

    private int _pageItems;
    private int _totalCount;
    private int _pageIndex = 1;
    private int _pageCount;
    private readonly List<Foo> _pageData = [];
    private readonly DataTable _pageDataTable = new();

    private void CreatePageDataTable()
    {
        _pageDataTable.Columns.Add(nameof(Foo.Id), typeof(int));
        _pageDataTable.Columns.Add(nameof(Foo.DateTime), typeof(DateTime));
        _pageDataTable.Columns.Add(nameof(Foo.Name), typeof(string));
        _pageDataTable.Columns.Add(nameof(Foo.Count), typeof(int));
        _pageData.AddRange(Foo.GenerateFoo(FooLocalizer, 80));
        _totalCount = _pageData.Count;
        _pageIndex = 1;
        _pageItems = 2;
        _pageCount = (int)Math.Ceiling(_totalCount / (double)_pageItems);

        // 此处代码可以通过数据库获得分页后的数据转化成 DataTable 再给 DynamicContext 即可实现数据库分页
        foreach (var f in _pageData.Skip((_pageIndex - 1) * _pageItems).Take(_pageItems).ToList())
        {
            _pageDataTable.Rows.Add(f.Id, f.DateTime, f.Name, f.Count);
        }
        _pageDataTable.AcceptChanges();
    }

    private DataTableDynamicContext CreatePageDataContext() => new DataTableDynamicContext(_pageDataTable, (context, col) =>
    {
        var propertyName = col.GetFieldName();
        if (propertyName == nameof(Foo.DateTime))
        {
            context.AddRequiredAttribute(nameof(Foo.DateTime));
            // 使用 AutoGenerateColumnAttribute 设置显示名称示例
            context.AddAutoGenerateColumnAttribute(nameof(Foo.DateTime), [
                new KeyValuePair<string, object?>(nameof(AutoGenerateColumnAttribute.Text),
                        FooLocalizer[nameof(Foo.DateTime)].Value)
            ]);
        }
        else if (propertyName == nameof(Foo.Name))
        {
            context.AddRequiredAttribute(nameof(Foo.Name), FooLocalizer["Name.Required"]);
            // 使用 Text 设置显示名称示例
            col.Text = FooLocalizer[nameof(Foo.Name)];
        }
        else if (propertyName == nameof(Foo.Count))
        {
            context.AddRequiredAttribute(nameof(Foo.Count));
            // 使用 DisplayNameAttribute 设置显示名称示例
            context.AddDisplayNameAttribute(nameof(Foo.Count), FooLocalizer[nameof(Foo.Count)].Value);
        }
        else if (propertyName == nameof(Foo.Id))
        {
            col.Ignore = true;
        }
    })
    {
        UseCache = false
    };

    private void UpdatePageDataContext()
    {
        var table = _dataTableDynamicContext4!.DataTable;
        table.Rows.Clear();

        // 此处代码可以通过数据库获得分页后的数据转化成 DataTable 再给 DynamicContext 即可实现数据库分页
        foreach (var f in _pageData.Skip((_pageIndex - 1) * _pageItems).Take(_pageItems).ToList())
        {
            table.Rows.Add(f.Id, f.DateTime, f.Name, f.Count);
        }

        table.AcceptChanges();
    }

    /// <summary>
    /// 点击页码处理函数
    /// </summary>
    /// <param name = "pageIndex"></param>
    /// <returns></returns>
    private Task OnPageLinkClick(int pageIndex)
    {
        _pageIndex = pageIndex;
        UpdatePageDataContext();

        StateHasChanged();
        return Task.CompletedTask;
    }
}
