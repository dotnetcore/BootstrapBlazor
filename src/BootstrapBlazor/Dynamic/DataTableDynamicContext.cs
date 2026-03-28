// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using System.Collections.Concurrent;
using System.Data;
using System.Reflection.Emit;

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">DataTable 动态数据上下文实现类 <see cref="DynamicObjectContext"/></para>
/// <para lang="en">DataTable dynamic data context implementation class <see cref="DynamicObjectContext"/></para>
/// </summary>
public class DataTableDynamicContext : DynamicObjectContext
{
    /// <summary>
    /// <para lang="zh">获得/设置 相关联的 DataTable 实例</para>
    /// <para lang="en">Gets or sets the associated DataTable instance</para>
    /// </summary>
    [NotNull]
    public DataTable? DataTable { get; set; }

    private Type DynamicObjectType { get; }

    private IEnumerable<ITableColumn> Columns { get; }

    private List<IDynamicObject>? Items { get; set; }

    private Action<DataTableDynamicContext, ITableColumn>? AddAttributesCallback { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否启用内部缓存 默认 true 启用</para>
    /// <para lang="en">Gets or sets whether to enable internal caching. Default is true.</para>
    /// </summary>
    public bool UseCache { get; set; } = true;

    /// <summary>
    /// <para lang="zh">负责将 DataRow 与 Items 关联起来方便查找提高效率</para>
    /// <para lang="en">Responsible for associating DataRow with Items to facilitate lookup and improve efficiency</para>
    /// </summary>
    private readonly ConcurrentDictionary<Guid, (IDynamicObject DynamicObject, DataRow Row)> _dataCache = new();

    /// <summary>
    /// <para lang="zh">添加行回调委托</para>
    /// <para lang="en">Add row callback delegate</para>
    /// </summary>
    public Func<IEnumerable<IDynamicObject>, Task>? OnAddAsync { get; set; }

    /// <summary>
    /// <para lang="zh">删除行回调委托</para>
    /// <para lang="en">Delete row callback delegate</para>
    /// </summary>
    public Func<IEnumerable<IDynamicObject>, Task<bool>>? OnDeleteAsync { get; set; }

    /// <summary>
    /// <para lang="zh">构造函数</para>
    /// <para lang="en">Constructor</para>
    /// </summary>
    /// <param name="table"></param>
    /// <param name="addAttributesCallback"></param>
    /// <param name="invisibleColumns">
    ///   <para lang="zh">永远不显示的列集合 默认为 null 全部显示</para>
    ///   <para lang="en">Collection of columns that are never displayed. Default is null, meaning all columns are displayed.</para>
    /// </param>
    /// <param name="shownColumns">
    ///   <para lang="zh">显示列集合 默认为 null 全部显示</para>
    ///   <para lang="en">Collection of columns that are always displayed. Default is null, meaning all columns are displayed.</para></param>
    /// <param name="hiddenColumns">
    ///   <para lang="zh">隐藏列集合 默认为 null 无隐藏列</para>
    ///   <para lang="en">Collection of columns that are hidden. Default is null, meaning no columns are hidden.</para>
    /// </param>
    public DataTableDynamicContext(DataTable table, Action<DataTableDynamicContext, ITableColumn>? addAttributesCallback = null, IEnumerable<string>? invisibleColumns = null, IEnumerable<string>? shownColumns = null, IEnumerable<string>? hiddenColumns = null)
    {
        DataTable = table;
        AddAttributesCallback = addAttributesCallback;
        OnValueChanged = OnCellValueChanged;

        // 获得 DataTable 列信息转换为 ITableColumn 集合
        var cols = InternalGetColumns();

        // Emit 生成动态类 (使用缓存)
        var columnNames = string.Join('|', cols.Select(static c => $"{c.GetFieldName}:{c.PropertyType.FullName}"));
        var cacheKey = $"BootstrapBlazor-{nameof(DataTableDynamicContext)}-{columnNames}";
        var dynamicType = CacheManager.GetOrCreateDynamicObjectTypeByName(cacheKey, cols, OnColumnCreating, out var cached);

        // 缓存命中时仍需调用回调以处理列属性
        if (!cached && AddAttributesCallback != null)
        {
            foreach (var col in cols)
            {
                AddAttributesCallback(this, col);
            }
        }
        DynamicObjectType = GetOrCreateType();

        // 获得显示列
        Columns = Utility.GetTableColumns(DynamicObjectType, cols).Where(col => GetShownColumns(col, invisibleColumns, shownColumns, hiddenColumns));

        [ExcludeFromCodeCoverage]
        Type GetOrCreateType() => dynamicType ?? throw new InvalidOperationException();
    }

    private static bool GetShownColumns(ITableColumn col, IEnumerable<string>? invisibleColumns, IEnumerable<string>? shownColumns, IEnumerable<string>? hiddenColumns)
    {
        var ret = true;
        var columnName = col.GetFieldName();
        if (invisibleColumns != null && invisibleColumns.Any(c => c.Equals(columnName, StringComparison.OrdinalIgnoreCase)))
        {
            ret = false;
        }

        // 隐藏列存在时隐藏列
        if (ret && hiddenColumns != null && hiddenColumns.Any(c => c.Equals(columnName, StringComparison.OrdinalIgnoreCase)))
        {
            col.Visible = false;
        }

        // 显示列存在时显示列
        if (ret && shownColumns != null && shownColumns.Any(c => c.Equals(columnName, StringComparison.OrdinalIgnoreCase)))
        {
            col.Visible = true;
        }
        return ret;
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
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
        _dataCache.Clear();
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
                    _dataCache.TryAdd(d.DynamicObjectPrimaryKey, (d, row));
                    ret.Add(d);
                }
            }
        }
        return ret;
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public override IEnumerable<ITableColumn> GetColumns() => Columns;

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
    /// <inheritdoc/>
    /// </summary>
    protected internal override IEnumerable<CustomAttributeBuilder> OnColumnCreating(ITableColumn col)
    {
        AddAttributesCallback?.Invoke(this, col);
        return base.OnColumnCreating(col);
    }

    #region Add Save Delete
    /// <summary>
    /// <inheritdoc/>
    /// </summary>
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

            if (item != null && _dataCache.TryGetValue(item.DynamicObjectPrimaryKey, out var c))
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
            _dataCache.TryAdd(dynamicObject.DynamicObjectPrimaryKey, (dynamicObject, row));
        }
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
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
                if (_dataCache.TryGetValue(item.DynamicObjectPrimaryKey, out var row))
                {
                    changed = true;

                    // 删除数据源
                    DataTable.Rows.Remove(row.Row);

                    // 清理缓存
                    _dataCache.TryRemove(item.DynamicObjectPrimaryKey, out _);

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
    /// <para lang="zh">单元格变更回调方法</para>
    /// <para lang="en">Cell value changed callback method</para>
    /// </summary>
    /// <param name="item"></param>
    /// <param name="column"></param>
    /// <param name="val"></param>
    private Task OnCellValueChanged(IDynamicObject item, ITableColumn column, object? val)
    {
        // 更新内部 DataRow
        if (_dataCache.TryGetValue(item.DynamicObjectPrimaryKey, out var cacheItem))
        {
            cacheItem.Row[column.GetFieldName()] = val;
            Items = null;
        }
        return Task.CompletedTask;
    }
    #endregion
}
