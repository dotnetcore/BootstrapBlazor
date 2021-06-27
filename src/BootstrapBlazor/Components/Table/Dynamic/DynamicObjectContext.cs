// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BootstrapBlazor.Components.Table.Dynamic
{
    /// <summary>
    /// 
    /// </summary>
    public class DynamicObjectContext : IDynamicObject
    {
        /// <summary>
        /// 复制对象方法
        /// </summary>
        /// <returns></returns>
        public DynamicObjectContext Clone()
        {
            return new DynamicObjectContext();
        }

        object ICloneable.Clone()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 获得属性值方法
        /// </summary>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public object? GetValue(string propertyName)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 保存属性值方法
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="value"></param>
        public void SetValue(string propertyName, object? value)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 对象配置方法
        /// </summary>
        protected internal void OnConfigurating()
        {

        }

        /// <summary>
        /// 对象创建方法
        /// </summary>
        protected internal void OnCreating()
        {

        }
    }
}
