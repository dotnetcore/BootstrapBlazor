// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using System;
using System.Collections.Generic;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class DynamicObjectContext
    {
        /// <summary>
        /// 
        /// </summary>
        public List<object?>? Values { get; set; }

        /// <summary>
        /// 复制对象方法
        /// </summary>
        /// <returns></returns>
        public DynamicObjectContext Clone()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 获得属性值方法
        /// </summary>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public virtual object? GetValue(string propertyName)
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
        protected internal virtual void OnConfigurating()
        {

        }

        /// <summary>
        /// 对象创建方法
        /// </summary>
        protected internal virtual void OnCreating()
        {

        }
    }
}
