// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// 动态对象接口
/// </summary>
public interface IDynamicColumnsObject : IDynamicObject
{
    /// <summary>
    /// 获得设置 列与列数值集合
    /// </summary>
    public Dictionary<string, object?> Columns { get; set; }
}
