// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using System.Collections.Generic;
using System.Data;
using System.Linq;

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
        public DataTable? DataTable { get; set; }

        /// <summary>
        /// GetItems 方法
        /// </summary>
        /// <returns></returns>
        public override IEnumerable<IDynamicObject> GetItems()
        {
            var ret = new List<DataTableDynamicObject>();
            if (DataTable != null)
            {
                foreach (DataRow row in DataTable.Rows)
                {
                    ret.Add(new DataTableDynamicObject() { Row = row });
                }
            }
            return ret.Cast<IDynamicObject>();
        }

        /// <summary>
        /// 获得列信息方法
        /// </summary>
        /// <returns></returns>
        public override IEnumerable<ITableColumn> GetColumns()
        {
            var ret = new List<InternalTableColumn>();
            if (DataTable != null)
            {
                foreach (DataColumn col in DataTable.Columns)
                {
                    ret.Add(new InternalTableColumn(col.ColumnName, col.DataType, col.ColumnName));
                }
            }
            return ret;
        }
    }
}
