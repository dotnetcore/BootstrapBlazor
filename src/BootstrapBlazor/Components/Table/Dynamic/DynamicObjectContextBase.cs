// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components
{
    public abstract class DynamicObjectContextBase
    {

        /// <summary>
        /// 获得动态类数据方法
        /// </summary>
        /// <returns></returns>
        public abstract IEnumerable<IDynamicObject> GetItems();
    }
}