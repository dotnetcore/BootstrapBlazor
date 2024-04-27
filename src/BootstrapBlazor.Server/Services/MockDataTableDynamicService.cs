// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using System.Data;

namespace BootstrapBlazor.Server.Services;

internal class MockDataTableDynamicService
{
    private DataTable MockDataTable { get; }

    private IStringLocalizer<Foo> Localizer { get; }

    public MockDataTableDynamicService(IStringLocalizer<Foo> localizer)
    {
        Localizer = localizer;
        MockDataTable = InitDataTable();
    }
    private DataTable InitDataTable()
    {
        var dt = new DataTable();
        dt.Columns.Add(nameof(Foo.Id), typeof(int));
        dt.Columns.Add(nameof(Foo.DateTime), typeof(DateTime));
        dt.Columns.Add(nameof(Foo.Name), typeof(string));
        dt.Columns.Add(nameof(Foo.Count), typeof(int));
        dt.PrimaryKey = [dt.Columns[0]];
        dt.Columns[0].AutoIncrement = true;

        Foo.GenerateFoo(Localizer, 10).ForEach(f =>
        {
            dt.Rows.Add(f.Id, f.DateTime, f.Name, f.Count);
        });
        dt.AcceptChanges();
        return dt;
    }

    public DataTableDynamicContext CreateContext()
    {
        // 初始化动态类型上下文实例
        return new DataTableDynamicContext(MockDataTable, (context, col) =>
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
                        var row = MockDataTable.Rows.Find(id);
                        if (row != null)
                        {
                            MockDataTable.Rows.Remove(row);
                        }
                    }
                }
                MockDataTable.AcceptChanges();
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

    public DataTableDynamicContext CreateDetailContext(DynamicObject context)
    {
        var id = context.GetValue(nameof(Foo.Id));
        var detailTable = InitDataTable();

        return new DataTableDynamicContext(detailTable, (context, col) =>
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
                        var row = MockDataTable.Rows.Find(id);
                        if (row != null)
                        {
                            MockDataTable.Rows.Remove(row);
                        }
                    }
                }
                MockDataTable.AcceptChanges();
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
}
