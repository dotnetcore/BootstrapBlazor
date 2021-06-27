// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using System.Collections.Generic;
using System.Data;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// 
    /// </summary>
    public class DataTableDynamicContext : DynamicObjectContext
    {
        /// <summary>
        /// 获得/设置 相关联的 DataTable 实例
        /// </summary>
        public DataTable? DataTable { get; set; }

        /// <summary>
        /// ToDynamicObjects 方法
        /// </summary>
        /// <returns></returns>
        public IEnumerable<DataTableDynamicObject> ToDynamicObjects()
        {
            var ret = new List<DataTableDynamicObject>();
            if (DataTable != null)
            {
                foreach (DataColumn col in DataTable.Columns)
                {
                    DynamicObjectRegister.AddColumn<DataTableDynamicObject>(col.ColumnName, col.DataType);
                }

                foreach (DataRow row in DataTable.Rows)
                {
                    ret.Add(new DataTableDynamicObject() { Row = row });
                }
            }
            return ret;
        }
    }
}
