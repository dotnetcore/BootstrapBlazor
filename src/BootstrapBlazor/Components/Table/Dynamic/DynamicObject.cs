// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// 动态类型实体类 <see cref="IDynamicObject" /> 实例
    /// </summary>
    public abstract class DynamicObject : IDynamicObject
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public abstract object Clone();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public abstract object? GetValue(string propertyName);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="value"></param>
        public abstract void SetValue(string propertyName, object? value);
    }
}
