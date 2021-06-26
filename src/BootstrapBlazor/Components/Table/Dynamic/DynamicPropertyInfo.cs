// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// 动态属性信息
    /// </summary>
    public class DynamicPropertyInfo
    {
        /// <summary>
        /// 构造动态属性
        /// </summary>
        /// <param name="name"></param>
        /// <param name="propType"></param>
        /// <param name="attributes"></param>
        public DynamicPropertyInfo(string name, Type propType, Attribute[] attributes)
        {
            this.name = name;
            this.propertyType = propType;
            this.attributes = attributes;
        }
        public override PropertyAttributes Attributes => PropertyAttributes.None;

    }
}
