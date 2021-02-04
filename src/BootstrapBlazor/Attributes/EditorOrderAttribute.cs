// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using System;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// 
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class EditorOrderAttribute : Attribute
    {
        /// <summary>
        /// 获得 Order 属性
        /// </summary>
        public int Order { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="order"></param>
        public EditorOrderAttribute(int order)
        {
            Order = order;
        }
    }
}
