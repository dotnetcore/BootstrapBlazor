// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Components;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace BootstrapBlazor.Shared.Samples.Table;

/// <summary>
/// 动态表格示例
/// </summary>
public partial class TablesDynamic
{
    [NotNull]
    private DataTableDynamicContext? DataTableDynamicContext { get; set; }

    [NotNull]
    private DataTableDynamicContext? DataTablePageDynamicContext { get; set; }

    private DataTable UserData { get; } = new DataTable();

    [Inject]
    [NotNull]
    private IStringLocalizer<Foo>? Localizer { get; set; }

    [Inject]
    [NotNull]
    private IStringLocalizer<Tables>? TablesLocalizer { get; set; }

    [Inject]
    [NotNull]
    private IStringLocalizer<TablesDynamic>? DynamicLocalizer { get; set; }

    private string? ButtonAddColumnText { get; set; }

    private string? ButtonRemoveColumnText { get; set; }

    private List<DynamicObject> SelectedItems { get; set; } = new List<DynamicObject>();

    /// <summary>
    /// OnInitialized 方法
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        ButtonAddColumnText ??= TablesLocalizer[nameof(ButtonAddColumnText)];
        ButtonRemoveColumnText ??= TablesLocalizer[nameof(ButtonRemoveColumnText)];

        // 初始化 DataTable
        InitDataTable();

        // 初始化分页表格
        InitPageDataTable();
    }

    private static bool ModelEqualityComparer(IDynamicObject x, IDynamicObject y) => x.GetValue("Id")?.ToString() == y.GetValue("Id")?.ToString();

    private void CreateContext()
    {
        // 初始化动态类型上下文实例
        DataTableDynamicContext = new DataTableDynamicContext(UserData, (context, col) =>
        {
            var propertyName = col.GetFieldName();
            if (propertyName == nameof(Foo.DateTime))
            {
                context.AddRequiredAttribute(nameof(Foo.DateTime));
                // 使用 AutoGenerateColumnAttribute 设置显示名称示例
                context.AddAutoGenerateColumnAttribute(nameof(Foo.DateTime), new KeyValuePair<string, object?>[]
                {
                    new(nameof(AutoGenerateColumnAttribute.Text), Localizer[nameof(Foo.DateTime)].Value)
                });
            }
            else if (propertyName == nameof(Foo.Name))
            {
                context.AddRequiredAttribute(nameof(Foo.Name), Localizer["Name.Required"]);
                // 使用 Text 设置显示名称示例
                col.Text = Localizer[nameof(Foo.Name)];
            }
            else if (propertyName == nameof(Foo.Count))
            {
                context.AddRequiredAttribute(nameof(Foo.Count));
                // 使用 DisplayNameAttribute 设置显示名称示例
                context.AddDisplayNameAttribute(nameof(Foo.Count), Localizer[nameof(Foo.Count)].Value);
            }
            else if (propertyName == nameof(Foo.Complete))
            {
                col.Filterable = true;
                // 使用 DisplayAttribute 设置显示名称示例
                context.AddDisplayAttribute(nameof(Foo.Complete), new KeyValuePair<string, object?>[]
                {
                    new(nameof(DisplayAttribute.Name), Localizer[nameof(Foo.Complete)].Value)
                });
            }
            else if (propertyName == nameof(Foo.Id))
            {
                col.Editable = false;
                col.Visible = false;
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
                        var row = UserData.Rows.Find(id);
                        if (row != null)
                        {
                            UserData.Rows.Remove(row);
                        }
                    }
                }
                UserData.AcceptChanges();
                return Task.FromResult(true);
            },
            OnChanged = args =>
            {
                if (args.ChangedType == DynamicItemChangedType.Add)
                {
                    var item = args.Items.First();
                    item.SetValue(nameof(Foo.DateTime), DateTime.Today);
                    item.SetValue(nameof(Foo.Name), "新建值");
                }
                return Task.CompletedTask;
            }
        };
    }

    private void InitDataTable()
    {
        UserData.Columns.Add(nameof(Foo.Id), typeof(int));
        UserData.Columns.Add(nameof(Foo.DateTime), typeof(DateTime));
        UserData.Columns.Add(nameof(Foo.Name), typeof(string));
        UserData.Columns.Add(nameof(Foo.Count), typeof(int));
        UserData.PrimaryKey = new DataColumn[] { UserData.Columns[0] };
        UserData.Columns[0].AutoIncrement = true;

        Foo.GenerateFoo(Localizer, 10).ForEach(f =>
        {
            UserData.Rows.Add(f.Id, f.DateTime, f.Name, f.Count);
        });

        CreateContext();
    }

    private Task OnAddColumn()
    {
        if (!UserData.Columns.Contains(nameof(Foo.Complete)))
        {
            UserData.Columns.Add(nameof(Foo.Complete), typeof(bool));

            // 更新数据
            var fs = Foo.GenerateFoo(Localizer, 10);
            for (var i = 0; i < fs.Count; i++)
            {
                UserData.Rows[i][nameof(Foo.Complete)] = fs[i].Complete;
            }
            CreateContext();
            StateHasChanged();
        }
        return Task.CompletedTask;
    }

    private Task OnRemoveColumn()
    {
        if (UserData.Columns.Contains(nameof(Foo.Complete)))
        {
            UserData.Columns.Remove(nameof(Foo.Complete));
            CreateContext();
            StateHasChanged();
        }
        return Task.CompletedTask;
    }

    private DataTable PageDataTable { get; set; } = new DataTable();

    private IEnumerable<int> PageItemsSource { get; set; } = new int[] { 2, 4, 8, 10 };

    private int PageItems { get; set; }

    private int TotalCount { get; set; }

    private int PageIndex { get; set; } = 1;

    [NotNull]
    private List<Foo>? PageFoos { get; set; }

    private void InitPageDataTable()
    {
        PageDataTable.Columns.Add(nameof(Foo.Id), typeof(int));
        PageDataTable.Columns.Add(nameof(Foo.DateTime), typeof(DateTime));
        PageDataTable.Columns.Add(nameof(Foo.Name), typeof(string));
        PageDataTable.Columns.Add(nameof(Foo.Count), typeof(int));

        PageFoos = Foo.GenerateFoo(Localizer, 80);
        PageIndex = 1;
        PageItems = PageItemsSource.First();
        TotalCount = PageFoos.Count;

        RebuildPageDataTable();
    }

    private void RebuildPageDataTable()
    {
        PageDataTable.Rows.Clear();
        // 此处代码可以通过数据库获得分页后的数据转化成 DataTable 再给 DynamicContext 即可实现数据库分页
        foreach (var f in PageFoos.Skip((PageIndex - 1) * PageItems).Take(PageItems).ToList())
        {
            PageDataTable.Rows.Add(f.Id, f.DateTime, f.Name, f.Count);
        }
        PageDataTable.AcceptChanges();

        DataTablePageDynamicContext = new DataTableDynamicContext(PageDataTable, (context, col) =>
        {
            var propertyName = col.GetFieldName();
            if (propertyName == nameof(Foo.DateTime))
            {
                context.AddRequiredAttribute(nameof(Foo.DateTime));
                // 使用 AutoGenerateColumnAttribute 设置显示名称示例
                context.AddAutoGenerateColumnAttribute(nameof(Foo.DateTime), new KeyValuePair<string, object?>[]
                {
                    new(nameof(AutoGenerateColumnAttribute.Text), Localizer[nameof(Foo.DateTime)].Value)
                });
            }
            else if (propertyName == nameof(Foo.Name))
            {
                context.AddRequiredAttribute(nameof(Foo.Name), Localizer["Name.Required"]);
                // 使用 Text 设置显示名称示例
                col.Text = Localizer[nameof(Foo.Name)];
            }
            else if (propertyName == nameof(Foo.Count))
            {
                context.AddRequiredAttribute(nameof(Foo.Count));
                // 使用 DisplayNameAttribute 设置显示名称示例
                context.AddDisplayNameAttribute(nameof(Foo.Count), Localizer[nameof(Foo.Count)].Value);
            }
            else if (propertyName == nameof(Foo.Id))
            {
                col.Editable = false;
                col.Visible = false;
            }
        });
    }

    /// <summary>
    /// 点击页码处理函数
    /// </summary>
    /// <param name="pageIndex"></param>
    /// <param name="pageItems"></param>
    /// <returns></returns>
    private Task OnPageClick(int pageIndex, int pageItems)
    {
        PageIndex = pageIndex;
        PageItems = pageItems;
        RebuildPageDataTable();
        StateHasChanged();
        return Task.CompletedTask;
    }

    /// <summary>
    /// 点击每页显示数量下拉框处理函数
    /// </summary>
    /// <param name="pageItems"></param>
    /// <returns></returns>
    private Task OnPageItemsChanged(int pageItems)
    {
        PageIndex = 1;
        PageItems = pageItems;
        RebuildPageDataTable();
        StateHasChanged();
        return Task.CompletedTask;
    }
}
