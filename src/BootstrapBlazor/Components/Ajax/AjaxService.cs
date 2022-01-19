// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BootstrapBlazor.Components;

/// <summary>
/// Ajax服务类
/// </summary>
public class AjaxService
{
    /// <summary>
    /// 获得 回调委托缓存集合
    /// </summary>
    private List<(IComponent Key, Func<AjaxOption, Task<string?>> Callback)> Cache { get; set; } = new();

    /// <summary>
    /// 注册服务
    /// </summary>
    /// <param name="key"></param>
    /// <param name="callback"></param>
    internal void Register(IComponent key, Func<AjaxOption, Task<string?>> callback) => Cache.Add((key, callback));

    /// <summary>
    /// 注销事件
    /// </summary>
    internal void UnRegister(IComponent key)
    {
        var item = Cache.FirstOrDefault(i => i.Key == key);
        if (item.Key != null)
        {
            Cache.Remove(item);
        }
    }

    /// <summary>
    /// 调用Ajax方法发送请求
    /// </summary>
    /// <param name="option"></param>
    /// <returns></returns>
    public async Task<string?> GetMessage(AjaxOption option)
    {
        var cb = Cache.FirstOrDefault().Callback;
        return cb == null ? null : await cb.Invoke(option);
    }
}
