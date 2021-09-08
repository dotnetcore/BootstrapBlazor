// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using System.Collections.Generic;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// DynamicObjectContextArgs 类
    /// </summary>
    public class DynamicObjectContextArgs
    {
        /// <summary>
        /// 
        /// </summary>
        public DynamicObjectContextArgs(IEnumerable<IDynamicObject> items, DynamicItemChangedType changedType = DynamicItemChangedType.Add)
        {
            Items = items;
            ChangedType = changedType;
        }

        /// <summary>
        /// 获得 编辑数据集合
        /// </summary>
        public IEnumerable<IDynamicObject> Items { get; }

        /// <summary>
        /// 获得 数据改变类型 默认 Add
        /// </summary>
        public DynamicItemChangedType ChangedType { get; }
    }
}
