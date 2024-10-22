// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Author: Argo Zhang (argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// 工具栏按钮接口
/// </summary>
public interface IToolbarComponent
{
    /// <summary>
    /// 获得/设置 是否显示 默认 true 显示
    /// </summary>
    bool IsShow { get; set; }
}
