// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection.Emit;
using System.Threading.Tasks;

namespace BootstrapBlazor.Components
{
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

        private Lazy<IEnumerable<IDynamicObject>>? Items { get; set; }

        private readonly ConcurrentDictionary<int, (IDynamicObject DynamicObject, DataRow Row)> Caches = new();

        private Action<DataTableDynamicContext, ITableColumn>? AddAttributesCallback { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="table"></param>
        /// <param name="addAttributesCallback"></param>
        public DataTableDynamicContext(DataTable table, Action<DataTableDynamicContext, ITableColumn>? addAttributesCallback = null)
        {
            DataTable = table;
            AddAttributesCallback = addAttributesCallback;

            var cols = InternalGetColumns();
            DynamicObjectType = EmitHelper.CreateTypeByName($"BootstrapBlazor_{nameof(DataTableDynamicContext)}_{GetHashCode()}", cols, typeof(DynamicObject), OnColumnCreating);
            Columns = InternalTableColumn.GetProperties(DynamicObjectType, cols);
        }

        /// <summary>
        /// GetItems 方法
        /// </summary>
        /// <returns></returns>
        public override IEnumerable<IDynamicObject> GetItems()
        {
            Items ??= new(() =>
            {
                Caches.Clear();
                var ret = new List<IDynamicObject>();
                foreach (DataRow row in DataTable.Rows)
                {
                    var dynamicObject = Activator.CreateInstance(DynamicObjectType)!;
                    foreach (DataColumn col in DataTable.Columns)
                    {
                        var invoker = SetPropertyCache.GetOrAdd((dynamicObject.GetType(), col.ColumnName), key => LambdaExtensions.SetPropertyValueLambda<object, object?>(dynamicObject, key.PropertyName).Compile());
                        var v = row[col];
                        if (row.IsNull(col))
                        {
                            v = null;
                        }
                        invoker.Invoke(dynamicObject, v);
                    }
                    if (dynamicObject is IDynamicObject d)
                    {
                        ret.Add(d);
                        Caches.TryAdd(d.GetHashCode(), (d, row));
                    }
                }
                return ret;
            });
            return Items.Value;
        }

        private ConcurrentDictionary<(Type ModelType, string PropertyName), Action<object, object?>> SetPropertyCache { get; } = new();

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
        /// <returns></returns>
        public Task<DynamicObject> AddAsync()
        {
            var dynamicObject = Activator.CreateInstance(DynamicObjectType) as DynamicObject;
            return Task.FromResult(dynamicObject!);
        }

        /// <summary>
        /// 保存方法
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public Task<bool> SaveAsync(DynamicObject item)
        {
            DataRow? row;
            if (Caches.TryGetValue(item.GetHashCode(), out var cacheItem))
            {
                row = cacheItem.Row;
            }
            else
            {
                row = DataTable.NewRow();
                DataTable.Rows.InsertAt(row, 0);
            }
            foreach (DataColumn col in DataTable.Columns)
            {
                row[col] = item.GetValue(col.ColumnName);
            }
            DataTable.AcceptChanges();
            Items = null;
            return Task.FromResult(true);
        }

        /// <summary>
        /// 删除方法
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        public Task<bool> DeleteAsync(IEnumerable<DynamicObject> items)
        {
            foreach (var item in items)
            {
                if (Caches.TryGetValue(item.GetHashCode(), out var row))
                {
                    DataTable.Rows.Remove(row.Row);
                }
            }
            Items = null;
            return Task.FromResult(true);
        }
        #endregion
    }
}
