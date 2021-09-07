// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class BootstrapServiceBase<TOption>
    {
        /// <summary>
        /// 获得 回调委托缓存集合
        /// </summary>
        protected List<(ComponentBase Key, Func<TOption, Task> Callback)> Cache { get; private set; } = new List<(ComponentBase, Func<TOption, Task>)>();

        /// <summary>
        /// 注册弹窗事件
        /// </summary>
        /// <param name="key"></param>
        /// <param name="callback"></param>
        internal void Register(ComponentBase key, Func<TOption, Task> callback)
        {
            Cache.Add((key, callback));
        }

        /// <summary>
        /// 注销弹窗事件
        /// </summary>
        internal void UnRegister(ComponentBase key)
        {
            var item = Cache.FirstOrDefault(i => i.Key == key);
            if (item.Key != null) Cache.Remove(item);
        }
    }
}
