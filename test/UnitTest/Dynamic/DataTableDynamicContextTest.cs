// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace UnitTest.Dynamic;

public class DataTableDynamicContextTest : BootstrapBlazorTestBase
{
    [Fact]
    public async Task DataTableDynamicContext_Ok()
    {
        var added = false;
        var deleted = false;
        var localizer = Context.Services.GetRequiredService<IStringLocalizer<Foo>>();
        var fooData = GenerateDataTable(localizer);
        var context = new DataTableDynamicContext(fooData, AddAttributesCallback(localizer))
        {
            OnAddAsync = foos =>
            {
                added = true;
                return Task.CompletedTask;
            },
            OnDeleteAsync = foos =>
            {
                deleted = true;
                return Task.FromResult(true);
            }
        };

        var cols = context.GetColumns();
        Assert.NotEmpty(cols);

        var items = context.GetItems();
        Assert.NotEmpty(items);

        await context.OnAddAsync(new IDynamicObject[] { Utility.Clone(items.First()) });
        Assert.True(added);

        await context.OnDeleteAsync(items);
        Assert.True(deleted);
    }

    [Fact]
    public void GetShownColumns_Ok()
    {
        var localizer = Context.Services.GetRequiredService<IStringLocalizer<Foo>>();
        var fooData = GenerateDataTable(localizer);
        var context = new DataTableDynamicContext(fooData, AddAttributesCallback(localizer),
            invisibleColumns: new string[] { nameof(Foo.DateTime) },
            shownColumns: new string[] { nameof(Foo.Name) },
            hiddenColumns: new string[] { nameof(Foo.Count) });
        Assert.Equal(4, context.GetColumns().Count());
    }

    [Fact]
    public void Ignore_Ok()
    {
        var localizer = Context.Services.GetRequiredService<IStringLocalizer<Foo>>();
        var fooData = GenerateDataTable(localizer);
        var context = new DataTableDynamicContext(fooData, IgnoreCallback(localizer));
        Assert.Equal(4, context.GetColumns().Count());
    }

    [Fact]
    public void RowStatus_Ok()
    {
        var localizer = Context.Services.GetRequiredService<IStringLocalizer<Foo>>();
        var fooData = GenerateDataTable(localizer);
        var context = new DataTableDynamicContext(fooData, AddAttributesCallback(localizer));
        fooData.Rows.RemoveAt(0);
        Assert.NotEmpty(context.GetItems());
    }

    [Fact]
    public void AddAttributesCallback_Ok()
    {
        var localizer = Context.Services.GetRequiredService<IStringLocalizer<Foo>>();
        var fooData = GenerateDataTable(localizer);
        var context = new DataTableDynamicContext(fooData);
        Assert.NotEmpty(context.GetItems());
    }

    [Fact]
    public void AddAttribute_Null()
    {
        var localizer = Context.Services.GetRequiredService<IStringLocalizer<Foo>>();
        var fooData = GenerateDataTable(localizer);
        var context = new DataTableDynamicContext(fooData);
        context.AddAttribute(nameof(Foo.Name), typeof(DisplayAttribute), Type.EmptyTypes, Array.Empty<object>(), null, null);
    }

    [Fact]
    public async Task AddAsync_Ok()
    {
        var added = false;
        var changed = false;
        var localizer = Context.Services.GetRequiredService<IStringLocalizer<Foo>>();
        var fooData = GenerateDataTable(localizer);
        var context = new DataTableDynamicContext(fooData)
        {
            OnAddAsync = foos =>
            {
                added = true;
                return Task.CompletedTask;
            },
            OnChanged = context =>
            {
                changed = true;
                return Task.CompletedTask;
            }
        };
        var items = context.GetItems();
        await context.AddAsync(items);
        Assert.True(added);
        Assert.False(changed);
    }

    [Fact]
    public async Task OnChanged_Ok()
    {
        var changed = false;
        var localizer = Context.Services.GetRequiredService<IStringLocalizer<Foo>>();
        var fooData = GenerateDataTable(localizer);
        var context = new DataTableDynamicContext(fooData)
        {
            OnChanged = context =>
            {
                changed = true;
                return Task.CompletedTask;
            }
        };
        await context.AddAsync(Enumerable.Empty<IDynamicObject>());
        Assert.True(changed);
    }

    [Fact]
    public async Task DeleteAsync_Ok()
    {
        var deleted = false;
        var localizer = Context.Services.GetRequiredService<IStringLocalizer<Foo>>();
        var fooData = GenerateDataTable(localizer);
        var context = new DataTableDynamicContext(fooData)
        {
            OnDeleteAsync = foos =>
            {
                deleted = true;
                return Task.FromResult(true);
            }
        };
        var items = context.GetItems();
        await context.DeleteAsync(items);
        Assert.True(deleted);
    }

    [Fact]
    public async Task DeleteAsync_Null()
    {
        var actual = "";
        var deleted = false;
        var added = false;
        var localizer = Context.Services.GetRequiredService<IStringLocalizer<Foo>>();
        var fooData = GenerateDataTable(localizer);
        var context = new DataTableDynamicContext(fooData)
        {
            OnChanged = args =>
            {
                deleted = args.ChangedType == DynamicItemChangedType.Delete;
                added = args.ChangedType == DynamicItemChangedType.Add;
                actual = args.Items.First().DynamicObjectPrimaryKey.ToString();
                return Task.CompletedTask;
            }
        };
        var items = context.GetItems().ToList().Take(1);
        var expected = items.First().DynamicObjectPrimaryKey.ToString();
        await context.DeleteAsync(items);
        Assert.Equal(expected, actual);
        Assert.True(deleted);
        Assert.Equal(3, context.GetItems().Count());

        // add
        await context.AddAsync(items);
        Assert.True(added);
        Assert.Equal(4, context.GetItems().Count());

        // add empty
        await context.AddAsync(Enumerable.Empty<IDynamicObject>());

        // 在选中行位置插入
        await context.AddAsync(context.GetItems().Take(2));

        // 反射设置 内部 Items 为 null
        items = context.GetItems().Take(1).ToList();
        context.GetType().GetProperty("Items", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic)!.SetValue(context, null);
        await context.DeleteAsync(items);

        context.OnDeleteAsync = context => Task.FromResult(true);
        await context.DeleteAsync(items);
    }

    [Fact]
    public async Task OnValueChanged_Ok()
    {
        var localizer = Context.Services.GetRequiredService<IStringLocalizer<Foo>>();
        var fooData = GenerateDataTable(localizer);
        var context = new DataTableDynamicContext(fooData);
        var item = context.GetItems().First();
        item.SetValue(nameof(Foo.Name), "test-name");
        await context.SetValue(item);
        Assert.Equal("test-name", fooData.Rows[0].ItemArray[1]?.ToString());
        Assert.Equal("test-name", item.GetValue(nameof(Foo.Name)));

        // not exist
        item.DynamicObjectPrimaryKey = Guid.NewGuid();
        await context.SetValue(item);
    }

    [Fact]
    public void GetValue_Exception()
    {
        var context = new DataTableDynamicObject();
        Assert.Throws<InvalidOperationException>(() => context.GetValue(nameof(Foo.Name)));
    }

    [Fact]
    public void GetValue_Ok()
    {
        var context = new DynamicObject();
        context.SetValue(nameof(DynamicObject.DynamicObjectPrimaryKey), Guid.Empty);
        var val = context.GetValue(nameof(DynamicObject.DynamicObjectPrimaryKey));
        Assert.Equal(Guid.Empty, val);
    }

    [Fact]
    public void SetValue_Null()
    {
        var context = new DataTableDynamicObject();
        context.SetValue(nameof(DynamicObject.DynamicObjectPrimaryKey), Guid.Empty);
        var val = context.GetValue(nameof(DynamicObject.DynamicObjectPrimaryKey));
        Assert.Equal(Guid.Empty, val);
    }

    private static DataTable GenerateDataTable(IStringLocalizer<Foo> localizer)
    {
        var fooData = new DataTable();
        fooData.Columns.Add(new DataColumn(nameof(Foo.DateTime), typeof(DateTime)) { DefaultValue = DateTime.Now });
        fooData.Columns.Add(nameof(Foo.Name), typeof(string));
        fooData.Columns.Add(nameof(Foo.Complete), typeof(bool));
        fooData.Columns.Add(nameof(Foo.Education), typeof(string));
        fooData.Columns.Add(nameof(Foo.Count), typeof(int));
        Foo.GenerateFoo(localizer, 4).ForEach(f =>
        {
            fooData.Rows.Add(f.DateTime, f.Name, f.Complete, f.Education, f.Count);
        });
        return fooData;
    }

    private static Action<DataTableDynamicContext, ITableColumn> AddAttributesCallback(IStringLocalizer<Foo> localizer) => new((context, col) =>
    {
        var propertyName = col.GetFieldName();
        if (propertyName == nameof(Foo.DateTime))
        {
            context.AddRequiredAttribute(nameof(Foo.DateTime));
            // 使用 AutoGenerateColumnAttribute 设置显示名称示例
            context.AddAutoGenerateColumnAttribute(nameof(Foo.DateTime), new KeyValuePair<string, object?>[]
            {
                new(nameof(AutoGenerateColumnAttribute.Text), localizer[nameof(Foo.DateTime)].Value)
            });
            context.AddDisplayAttribute(nameof(Foo.DateTime), new KeyValuePair<string, object?>[]
            {
                new(nameof(DisplayAttribute.Name), localizer[nameof(Foo.DateTime)].Value)
            });
        }
        else if (propertyName == nameof(Foo.Name))
        {
            context.AddRequiredAttribute(nameof(Foo.Name), localizer["Name.Required"].Value);
            context.AddDisplayNameAttribute(nameof(Foo.Name), "Test-Name");
            context.AddDescriptionAttribute(nameof(Foo.Name), "Test-Name-Desc");
            // 使用 Text 设置显示名称示例
            col.Text = localizer[nameof(Foo.Name)];
        }
    });

    private static Action<DataTableDynamicContext, ITableColumn> IgnoreCallback(IStringLocalizer<Foo> localizer) => new((context, col) =>
    {
        var propertyName = col.GetFieldName();
        if (propertyName == nameof(Foo.DateTime))
        {
            context.AddRequiredAttribute(nameof(Foo.DateTime));
            // 使用 AutoGenerateColumnAttribute 设置显示名称示例
            context.AddAutoGenerateColumnAttribute(nameof(Foo.DateTime), new KeyValuePair<string, object?>[]
            {
                new(nameof(AutoGenerateColumnAttribute.Text), localizer[nameof(Foo.DateTime)].Value)
            });
            context.AddDisplayAttribute(nameof(Foo.DateTime), new KeyValuePair<string, object?>[]
            {
                new(nameof(DisplayAttribute.Name), localizer[nameof(Foo.DateTime)].Value)
            });
            col.Ignore = true;
        }
        else if (propertyName == nameof(Foo.Name))
        {
            context.AddRequiredAttribute(nameof(Foo.Name), localizer["Name.Required"].Value);
            context.AddDisplayNameAttribute(nameof(Foo.Name), "Test-Name");
            context.AddDescriptionAttribute(nameof(Foo.Name), "Test-Name-Desc");
            // 使用 Text 设置显示名称示例
            col.Text = localizer[nameof(Foo.Name)];
        }
    });
}
