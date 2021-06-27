// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using System.Data;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// DataTable 动态类实例
    /// </summary>
    public class DataTableDynamicObject : DynamicObject
    {
        /// <summary>
        /// 
        /// </summary>
        public DataRow? Row { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public override object? GetValue(string propertyName) => Row?[propertyName];
    }
}
