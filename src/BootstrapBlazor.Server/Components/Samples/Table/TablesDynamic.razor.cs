// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.Extensions.Configuration;
using System.Data;

namespace BootstrapBlazor.Server.Components.Samples.Table;

/// <summary>
/// 动态表格示例代码
/// </summary>
public partial class TablesDynamic
{
    [NotNull]
    private DataTableDynamicContext? DataTableDynamicContext { get; set; }

    private DataTable UserData { get; } = new DataTable();

    private List<DynamicObject> SelectedItems { get; set; } = [];

    private string? ButtonAddColumnText { get; set; }

    private string? ButtonRemoveColumnText { get; set; }

    /// <summary>
    /// OnInitialized 方法
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        ButtonAddColumnText ??= Localizer["TablesDynamicDynamicColButtonAddColumnText"];
        ButtonRemoveColumnText ??= Localizer["TablesDynamicDynamicColButtonRemoveColumnText"];

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
                context.AddAutoGenerateColumnAttribute(nameof(Foo.DateTime), new KeyValuePair<string, object?>[] { new(nameof(AutoGenerateColumnAttribute.Text), Localizer[nameof(Foo.DateTime)].Value) });
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
                context.AddDisplayAttribute(nameof(Foo.Complete), new KeyValuePair<string, object?>[] { new(nameof(DisplayAttribute.Name), Localizer[nameof(Foo.Complete)].Value) });
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
        UserData.PrimaryKey =
        [
            UserData.Columns[0]
        ];
        UserData.Columns[0].AutoIncrement = true;
        Foo.GenerateFoo(LocalizerFoo, 10).ForEach(f =>
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
            var fs = Foo.GenerateFoo(LocalizerFoo, 10);
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

    private DataTable PageDataTable { get; set; } = new();

    private int PageItems { get; set; }

    private int TotalCount { get; set; }

    private int PageIndex { get; set; } = 1;

    private int PageCount { get; set; }

    [NotNull]
    private List<Foo>? PageFoos { get; set; }

    private void InitPageDataTable()
    {
        PageDataTable.Columns.Add(nameof(Foo.Id), typeof(int));
        PageDataTable.Columns.Add(nameof(Foo.DateTime), typeof(DateTime));
        PageDataTable.Columns.Add(nameof(Foo.Name), typeof(string));
        PageDataTable.Columns.Add(nameof(Foo.Count), typeof(int));
        PageFoos = Foo.GenerateFoo(LocalizerFoo, 80);
        TotalCount = PageFoos.Count;
        PageIndex = 1;
        PageItems = 2;
        PageCount = (int)Math.Ceiling(TotalCount / 2.0);
        RebuildPageDataTable();
        RebuildPaginationDataTable();
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
    }


    private void RebuildPaginationDataTable()
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
                context.AddAutoGenerateColumnAttribute(nameof(Foo.DateTime), new KeyValuePair<string, object?>[] { new(nameof(AutoGenerateColumnAttribute.Text), Localizer[nameof(Foo.DateTime)].Value) });
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
                col.Ignore = true;
            }
        });
    }

    [NotNull]
    private DataTableDynamicContext? DataTablePageDynamicContext { get; set; }

    /// <summary>
    /// 点击页码处理函数
    /// </summary>
    /// <param name = "pageIndex"></param>
    /// <returns></returns>
    private Task OnPageLinkClick(int pageIndex)
    {
        PageIndex = pageIndex;
        RebuildPaginationDataTable();
        StateHasChanged();
        return Task.CompletedTask;
    }
}
