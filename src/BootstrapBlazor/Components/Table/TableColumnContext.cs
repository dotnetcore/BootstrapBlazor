// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// TableColumn 上下文类
    /// </summary>
    public class TableColumnContext<TModel, TValue>
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="model"></param>
        /// <param name="value"></param>
        public TableColumnContext(TModel model, TValue value)
        {
            Row = model;
            Value = value;
        }

        /// <summary>
        /// 获得/设置 行数据实例
        /// </summary>
        public TModel Row { get; set; }

        /// <summary>
        /// 获得/设置 当前绑定字段数据实例
        /// </summary>
        public TValue Value { get; set; }
    }
}
