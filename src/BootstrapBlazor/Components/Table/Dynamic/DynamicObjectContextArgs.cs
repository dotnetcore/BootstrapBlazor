// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using System.Collections.Generic;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// 
    /// </summary>
    public class DynamicObjectContextArgs
    {
        /// <summary>
        /// 
        /// </summary>
        public DynamicObjectContextArgs(IEnumerable<IDynamicObject> items, DynamicObjectChangedType changedType = DynamicObjectChangedType.Add)
        {
            Items = items;
            ChangedType = changedType;
        }

        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<IDynamicObject> Items { get; }

        /// <summary>
        /// 
        /// </summary>
        public DynamicObjectChangedType ChangedType { get; }
    }

    /// <summary>
    /// 动态类型数据变化类型
    /// </summary>
    public enum DynamicObjectChangedType
    {
        /// <summary>
        /// 新建
        /// </summary>
        Add,

        /// <summary>
        /// 更新
        /// </summary>
        Update,

        /// <summary>
        /// 删除
        /// </summary>
        Delete
    }
}
