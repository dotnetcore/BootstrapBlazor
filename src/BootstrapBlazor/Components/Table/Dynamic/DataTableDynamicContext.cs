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

        private Action<DataTableDynamicContext, ITableColumn>? AddAttributesCallback { get; set; }

        private ConcurrentDictionary<Guid, (IDynamicObject DynamicObject, DataRow Row)> Caches { get; } = new();

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
            var dynamicType = EmitHelper.CreateTypeByName($"BootstrapBlazor_{nameof(DataTableDynamicContext)}_{GetHashCode()}", cols, typeof(DynamicObject), OnColumnCreating);
            if (dynamicType == null)
            {
                throw new InvalidOperationException();
            }
            DynamicObjectType = dynamicType;
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
                    var dynamicObject = Activator.CreateInstance(DynamicObjectType);
                    if (dynamicObject != null)
                    {
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
                            d.DynamicObjectPrimaryKey = Guid.NewGuid();
                            Caches.TryAdd(d.DynamicObjectPrimaryKey, (d, row));
                            ret.Add(d);
                        }
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
        public override async Task<IDynamicObject> AddAsync()
        {
            var dynamicObject = Activator.CreateInstance(DynamicObjectType) as IDynamicObject;
            if (dynamicObject == null)
            {
                throw new InvalidCastException($"{DynamicObjectType.Name} can not cast to {nameof(IDynamicObject)}");
            }
            if (OnChanged != null)
            {
                await OnChanged(new(new[] { dynamicObject }));
            }
            return dynamicObject;
        }

        /// <summary>
        /// 保存方法
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public override async Task<bool> SaveAsync(IDynamicObject item)
        {
            DataRow? row;
            if (Caches.TryGetValue(item.DynamicObjectPrimaryKey, out var cacheItem))
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
            if (OnChanged != null)
            {
                await OnChanged(new(new[] { item }, DynamicItemChangedType.Update));
            }
            Items = null;
            return true;
        }

        /// <summary>
        /// 删除方法
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        public override async Task<bool> DeleteAsync(IEnumerable<IDynamicObject> items)
        {
            var changed = false;
            foreach (var item in items)
            {
                if (Caches.TryGetValue(item.DynamicObjectPrimaryKey, out var row))
                {
                    changed = true;
                    DataTable.Rows.Remove(row.Row);
                }
            }
            if (changed && OnChanged != null)
            {
                await OnChanged(new(items, DynamicItemChangedType.Delete));
            }
            if (changed)
            {
                Items = null;
            }
            return true;
        }
        #endregion
    }
}
