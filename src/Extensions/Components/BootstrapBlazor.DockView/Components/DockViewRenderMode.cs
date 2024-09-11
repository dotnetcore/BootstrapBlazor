// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Core.Converter;

namespace BootstrapBlazor.Components;

/// <summary>
/// DockViewRenderMode 渲染模式枚举类型
/// </summary>
[JsonEnumConverter(true)]
public enum DockViewRenderMode
{
    /// <summary>
    /// 可见时渲染
    /// </summary>
    OnlyWhenVisible,

    /// <summary>
    /// 始终渲染
    /// </summary>
    Always
}
