// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// 
/// </summary>
public class DockViewConfig
{
    /// <summary>
    /// 
    /// </summary>
    public string? Type { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public string? ComponentName { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public object? ComponentState { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public IEnumerable<DockViewConfig>? Content { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public int Width { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public int Height { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public string? Id { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public bool IsCloseable { get; set; } = true;

    /// <summary>
    /// 
    /// </summary>
    public string? Title { get; set; }

}
