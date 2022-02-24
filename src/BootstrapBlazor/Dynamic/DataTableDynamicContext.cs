// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using System.Collections.Concurrent;
using System.Data;
using System.Reflection.Emit;

namespace BootstrapBlazor.Components;

/// <summary>
/// DataTable 动态数据上下文实现类 <see cref="DynamicObjectContext" />
/// </summary>
public class DataTableDynamicContext : DynamicObjectContext
{
    /// <summary>
    /// 获得/设置 相关联的 DataTable 实例
    /// </summary>
    [NotNull]
    public DataTable? DataTable { get; set; }

    private Type DynamicObjectType { get; }

    private IEnumerable<ITableColumn>? Columns { get; }

    private List<IDynamicObject>? Items { get; set; }

    private Action<DataTableDynamicContext, ITableColumn>? AddAttributesCallback { get; set; }

    /// <summary>
    /// 负责将 DataRow 与 Items 关联起来方便查找提高效率
    /// </summary>
    private ConcurrentDictionary<Guid, (IDynamicObject DynamicObject, DataRow Row)> Caches { get; } = new();

    /// <summary>
    /// 添加行回调委托
    /// </summary>
    public Func<IEnumerable<IDynamicObject>, Task>? OnAddAsync { get; set; }

    /// <summary>
    /// 删除行回调委托
    /// </summary>
    public Func<IEnumerable<IDynamicObject>, Task<bool>>? OnDeleteAsync { get; set; }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="table"></param>
    /// <param name="addAttributesCallback"></param>
    /// <param name="invisibleColumns">永远不显示的列集合 默认为 null 全部显示</param>
    /// <param name="shownColumns">显示列集合 默认为 null 全部显示</param>
    /// <param name="hiddenColumns">隐藏列集合 默认为 null 无隐藏列</param>
    public DataTableDynamicContext(DataTable table, Action<DataTableDynamicContext, ITableColumn>? addAttributesCallback = null, IEnumerable<string>? invisibleColumns = null, IEnumerable<string>? shownColumns = null, IEnumerable<string>? hiddenColumns = null)
    {
        DataTable = table;
        AddAttributesCallback = addAttributesCallback;

        // 获得 DataTable 列信息转换为 ITableColumn 集合
        var cols = InternalGetColumns();

        // Emit 生成动态类
        var dynamicType = EmitHelper.CreateTypeByName($"BootstrapBlazor_{nameof(DataTableDynamicContext)}_{GetHashCode()}", cols, typeof(DataTableDynamicObject), OnColumnCreating);
        if (dynamicType == null)
        {
            throw new InvalidOperationException();
        }
        DynamicObjectType = dynamicType;

        // 获得显示列
        Columns = InternalTableColumn.GetProperties(DynamicObjectType, cols).Where(col => GetShownColumns(col, invisibleColumns, shownColumns, hiddenColumns)).ToList();

        OnValueChanged = OnCellValueChanged;
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
    /// GetItems 方法
    /// </summary>
    /// <returns></returns>
    public override IEnumerable<IDynamicObject> GetItems()
    {
        Items ??= BuildItems();
        return Items;
    }

    private List<IDynamicObject> BuildItems()
    {
        Caches.Clear();
        var ret = new List<IDynamicObject>();
        foreach (DataRow row in DataTable.Rows)
        {
            if (row.RowState == DataRowState.Deleted || row.RowState == DataRowState.Detached)
            {
                continue;
            }
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
        return ret;
    }

    /// <summary>
    /// GetItems 方法
    /// </summary>
    /// <returns></returns>
    public override IEnumerable<ITableColumn> GetColumns() => Columns ?? Enumerable.Empty<ITableColumn>();

    /// <summary>
    /// 获得列信息方法
    /// </summary>
    /// <returns></returns>
    private IEnumerable<ITableColumn> InternalGetColumns()
    {
        var ret = new List<InternalTableColumn>();
        foreach (DataColumn col in DataTable.Columns)
        {
            ret.Add(new InternalTableColumn(col.ColumnName, col.DataType));
        }
        return ret;
    }

    /// <summary>
    /// 
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
    /// 新建方法
    /// </summary>
    /// <param name="selectedItems">当前选中行</param>
    /// <returns></returns>
    public override async Task AddAsync(IEnumerable<IDynamicObject> selectedItems)
    {
        if (OnAddAsync != null)
        {
            await OnAddAsync(selectedItems);
        }
        else
        {
            if (Activator.CreateInstance(DynamicObjectType) is not DataTableDynamicObject dynamicObject)
            {
                throw new InvalidCastException($"{DynamicObjectType.Name} can not cast to {nameof(IDynamicObject)}");
            }

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
    /// 删除方法
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
    /// 动态类型变更回调方法
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
