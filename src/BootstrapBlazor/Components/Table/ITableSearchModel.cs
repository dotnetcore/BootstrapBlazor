// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// Table 组件自定义搜索模型接口定义
/// </summary>
public interface ITableSearchModel
{
    /// <summary>
    /// 获得 搜索集合
    /// </summary>
    IEnumerable<IFilterAction> GetSearchs();

    /// <summary>
    /// 重置操作
    /// </summary>
    void Reset();
}
