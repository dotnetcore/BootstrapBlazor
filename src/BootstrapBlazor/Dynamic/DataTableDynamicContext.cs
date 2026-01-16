// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using System.Collections.Concurrent;
using System.Data;
using System.Reflection.Emit;

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">DataTable 动态数据上下文实现类 <see cref="DynamicObjectContext" /></para>
/// <para lang="en">DataTable 动态data上下文实现类 <see cref="DynamicObjectContext" /></para>
/// </summary>
public class DataTableDynamicContext : DynamicObjectContext
{
    /// <summary>
    /// <para lang="zh">获得/设置 相关联的 DataTable 实例</para>
    /// <para lang="en">Gets or sets 相关联的 DataTable instance</para>
    /// </summary>
    [NotNull]
    public DataTable? DataTable { get; set; }

    private Type DynamicObjectType { get; }

    private IEnumerable<ITableColumn> Columns { get; }

    private List<IDynamicObject>? Items { get; set; }

    private Action<DataTableDynamicContext, ITableColumn>? AddAttributesCallback { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否启用内部缓存 默认 true 启用</para>
    /// <para lang="en">Gets or sets whether启用内部缓存 Default is true 启用</para>
    /// </summary>
    public bool UseCache { get; set; } = true;

    /// <summary>
    /// <para lang="zh">负责将 DataRow 与 Items 关联起来方便查找提高效率</para>
    /// <para lang="en">负责将 DataRow 与 Items 关联起来方便查找提高效率</para>
    /// </summary>
    private ConcurrentDictionary<Guid, (IDynamicObject DynamicObject, DataRow Row)> Caches { get; } = new();

    /// <summary>
    /// <para lang="zh">添加行回调委托</para>
    /// <para lang="en">添加行回调delegate</para>
    /// </summary>
    public Func<IEnumerable<IDynamicObject>, Task>? OnAddAsync { get; set; }

    /// <summary>
    /// <para lang="zh">删除行回调委托</para>
    /// <para lang="en">删除行回调delegate</para>
    /// </summary>
    public Func<IEnumerable<IDynamicObject>, Task<bool>>? OnDeleteAsync { get; set; }

    /// <summary>
    /// <para lang="zh">构造函数</para>
    /// <para lang="en">构造函数</para>
    /// </summary>
    /// <param name="table"></param>
    /// <param name="addAttributesCallback"></param>
    /// <param name="invisibleColumns"><para lang="zh">永远不显示的列集合 默认为 null 全部显示</para><para lang="en">永远不display的列collection default is为 null 全部display</para></param>
    /// <param name="shownColumns"><para lang="zh">显示列集合 默认为 null 全部显示</para><para lang="en">display列collection default is为 null 全部display</para></param>
    /// <param name="hiddenColumns"><para lang="zh">隐藏列集合 默认为 null 无隐藏列</para><para lang="en">隐藏列collection default is为 null 无隐藏列</para></param>
    public DataTableDynamicContext(DataTable table, Action<DataTableDynamicContext, ITableColumn>? addAttributesCallback = null, IEnumerable<string>? invisibleColumns = null, IEnumerable<string>? shownColumns = null, IEnumerable<string>? hiddenColumns = null)
    {
        DataTable = table;
        AddAttributesCallback = addAttributesCallback;

        // 获得 DataTable 列信息转换为 ITableColumn 集合
        var cols = InternalGetColumns();

        // Emit 生成动态类
        DynamicObjectType = CreateType();

        // 获得显示列
        Columns = Utility.GetTableColumns(DynamicObjectType, cols).Where(col => GetShownColumns(col, invisibleColumns, shownColumns, hiddenColumns));

        OnValueChanged = OnCellValueChanged;

        [ExcludeFromCodeCoverage]
        Type CreateType()
        {
            var dynamicType = EmitHelper.CreateTypeByName($"BootstrapBlazor_{nameof(DataTableDynamicContext)}_{GetHashCode()}", cols, typeof(DataTableDynamicObject), OnColumnCreating);
            return dynamicType ?? throw new InvalidOperationException();
        }
    }

    private static bool GetShownColumns(ITableColumn col, IEnumerable<string>? invisibleColumns, IEnumerable<string>? shownColumns, IEnumerable<string>? hiddenColumns)
    {
        var ret = true;
        var columnName = col.GetFieldName();
        if (invisibleColumns != null && invisibleColumns.Any(c => c.Equals(columnName, StringComparison.OrdinalIgnoreCase)))
        {
            ret = false;
        }

        // 隐藏列优先 移除隐藏列
        if (ret && hiddenColumns != null && hiddenColumns.Any(c => c.Equals(columnName, StringComparison.OrdinalIgnoreCase)))
        {
            col.Visible = false;
        }

        // 显示列不存在时 不显示
        if (ret && shownColumns != null && !shownColumns.Any(c => c.Equals(columnName, StringComparison.OrdinalIgnoreCase)))
        {
            col.Visible = true;
        }
        return ret;
    }

    /// <summary>
    /// <para lang="zh">GetItems 方法</para>
    /// <para lang="en">GetItems 方法</para>
    /// </summary>
    /// <returns></returns>
    public override IEnumerable<IDynamicObject> GetItems()
    {
        if (UseCache)
        {
            Items ??= BuildItems();
        }
        else
        {
            Items = BuildItems();
        }
        return Items;
    }

    private List<IDynamicObject> BuildItems()
    {
        Caches.Clear();
        var ret = new List<IDynamicObject>();
        foreach (DataRow row in DataTable.Rows)
        {
            if (row.RowState != DataRowState.Deleted)
            {
                var dynamicObject = Activator.CreateInstance(DynamicObjectType);
                if (dynamicObject is DataTableDynamicObject d)
                {
                    foreach (DataColumn col in DataTable.Columns)
                    {
                        if (!row.IsNull(col))
                        {
                            Utility.SetPropertyValue<object, object?>(d, col.ColumnName, row[col]);
                        }
                    }

                    d.Row = row;
                    d.DynamicObjectPrimaryKey = Guid.NewGuid();
                    Caches.TryAdd(d.DynamicObjectPrimaryKey, (d, row));
                    ret.Add(d);
                }
            }
        }
        return ret;
    }

    /// <summary>
    /// <para lang="zh">GetItems 方法</para>
    /// <para lang="en">GetItems 方法</para>
    /// </summary>
    /// <returns></returns>
    public override IEnumerable<ITableColumn> GetColumns() => Columns;

    /// <summary>
    /// <para lang="zh">获得列信息方法</para>
    /// <para lang="en">Gets列信息方法</para>
    /// </summary>
    /// <returns></returns>
    private List<ITableColumn> InternalGetColumns()
    {
        var ret = new List<ITableColumn>();
        foreach (DataColumn col in DataTable.Columns)
        {
            ret.Add(new InternalTableColumn(col.ColumnName, col.DataType));
        }
        return ret;
    }

    /// <summary>
    /// <para lang="zh"></para>
    /// <para lang="en"></para>
    /// </summary>
    /// <param name="col"></param>
    /// <returns></returns>
    protected internal override IEnumerable<CustomAttributeBuilder> OnColumnCreating(ITableColumn col)
    {
        AddAttributesCallback?.Invoke(this, col);
        return base.OnColumnCreating(col);
    }

    #region Add Save Delete
    /// <summary>
    /// <para lang="zh">新建方法</para>
    /// <para lang="en">新建方法</para>
    /// </summary>
    /// <param name="selectedItems"><para lang="zh">当前选中行</para><para lang="en">当前选中行</para></param>
    /// <returns></returns>
    public override async Task AddAsync(IEnumerable<IDynamicObject> selectedItems)
    {
        if (OnAddAsync != null)
        {
            await OnAddAsync(selectedItems);
        }
        else if (Activator.CreateInstance(DynamicObjectType) is DataTableDynamicObject dynamicObject)
        {
            var row = DataTable.NewRow();
            var indexOfRow = 0;
            var item = selectedItems.FirstOrDefault();

            if (item != null && Caches.TryGetValue(item.DynamicObjectPrimaryKey, out var c))
            {
                indexOfRow = DataTable.Rows.IndexOf(c.Row);
            }

            // DataTable 数据源增加数据
            DataTable.Rows.InsertAt(row, indexOfRow);

            // 新建动态类型属性赋值
            dynamicObject.DynamicObjectPrimaryKey = Guid.NewGuid();
            foreach (DataColumn col in DataTable.Columns)
            {
                if (col.DefaultValue != DBNull.Value)
                {
                    Utility.SetPropertyValue<object, object?>(dynamicObject, col.ColumnName, col.DefaultValue);
                }
            }
            dynamicObject.Row = row;

            // 触发 Changed 回调
            if (OnChanged != null)
            {
                await OnChanged(new(new[] { dynamicObject }, DynamicItemChangedType.Add));
            }

            // Table 组件数据源更新数据
            Items?.Insert(indexOfRow, dynamicObject);

            // 缓存更新数据
            Caches.TryAdd(dynamicObject.DynamicObjectPrimaryKey, (dynamicObject, row));
        }
    }

    /// <summary>
    /// <para lang="zh">删除方法</para>
    /// <para lang="en">删除方法</para>
    /// </summary>
    /// <param name="items"></param>
    /// <returns></returns>
    public override async Task<bool> DeleteAsync(IEnumerable<IDynamicObject> items)
    {
        var ret = false;
        if (OnDeleteAsync != null)
        {
            ret = await OnDeleteAsync(items);
            Items?.RemoveAll(i => items.Any(item => item == i));
        }
        else
        {
            var changed = false;
            foreach (var item in items)
            {
                if (Caches.TryGetValue(item.DynamicObjectPrimaryKey, out var row))
                {
                    changed = true;

                    // 删除数据源
                    DataTable.Rows.Remove(row.Row);

                    // 清理缓存
                    Caches.TryRemove(item.DynamicObjectPrimaryKey, out _);

                    // 清理 Table 组件数据源
                    Items?.Remove(item);
                }
            }
            if (changed)
            {
                DataTable.AcceptChanges();
                if (OnChanged != null)
                {
                    await OnChanged(new(items, DynamicItemChangedType.Delete));
                }
            }
            ret = true;
        }
        return ret;
    }

    /// <summary>
    /// <para lang="zh">动态类型变更回调方法</para>
    /// <para lang="en">动态type变更callback method</para>
    /// </summary>
    /// <param name="item"></param>
    /// <param name="column"></param>
    /// <param name="val"></param>
    /// <returns></returns>
    private Task OnCellValueChanged(IDynamicObject item, ITableColumn column, object? val)
    {
        // 更新内部 DataRow
        if (Caches.TryGetValue(item.DynamicObjectPrimaryKey, out var cacheItem))
        {
            cacheItem.Row[column.GetFieldName()] = val;
            Items = null;
        }
        return Task.CompletedTask;
    }
    #endregion
}
