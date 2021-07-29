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
    /// Title 服务
    /// </summary>
    public class TitleService
    {
        /// <summary>
        /// 获得 回调委托缓存集合
        /// </summary>
        private List<(IComponent Key, Func<string, ValueTask> Callback)> Cache { get; set; } = new();

        /// <summary>
        /// 设置当前页面 Title 方法
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        public async ValueTask SetTitle(string title)
        {
            var cb = Cache.FirstOrDefault().Callback;
            if (cb != null)
            {
                await cb.Invoke(title);
            }
        }

        /// <summary>
        /// 注册服务
        /// </summary>
        /// <param name="key"></param>
        /// <param name="callback"></param>
        internal void Register(IComponent key, Func<string, ValueTask> callback) => Cache.Add((key, callback));

        /// <summary>
        /// 注销事件
        /// </summary>
        internal void UnRegister(IComponent key)
        {
            var item = Cache.FirstOrDefault(i => i.Key == key);
            if (item.Key != null) Cache.Remove(item);
        }
    }
}
