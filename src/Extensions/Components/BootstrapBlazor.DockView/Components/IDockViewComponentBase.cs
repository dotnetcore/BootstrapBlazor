// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using System.Text.Json.Serialization;

namespace BootstrapBlazor.Components;

/// <summary>
/// IDockComponent 接口定义
/// </summary>
public interface IDockViewComponentBase : IDisposable
{
    /// <summary>
    /// 获得/设置 组件类型
    /// </summary>
    [JsonConverter(typeof(DockViewTypeConverter))]
    DockViewContentType Type { get; }
}
