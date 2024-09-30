﻿// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// 根组件接口
/// </summary>
public interface IRootComponentGenerator
{
    /// <summary>
    /// 生成组件方法
    /// </summary>
    /// <returns></returns>
    RenderFragment Generator();
}
