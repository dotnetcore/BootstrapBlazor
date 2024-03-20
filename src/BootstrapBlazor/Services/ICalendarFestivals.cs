// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// 节日接口
/// </summary>
public interface ICalendarFestivals
{
    /// <summary>
    /// 获得 节日键值对
    /// </summary>
    /// <returns></returns>
    string? GetFestival(DateTime dt);
}
