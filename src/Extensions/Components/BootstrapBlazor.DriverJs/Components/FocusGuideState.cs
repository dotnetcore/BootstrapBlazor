// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// the current state of the driver
/// </summary>
public class FocusGuideState
{
    /// <summary>
    /// Whether the driver is currently active or not
    /// </summary>
    public bool IsInitialized { get; set; }

    /// <summary>
    /// Index of the currently active step if using as a product tour and have configured the steps array.
    /// </summary>
    public int ActiveIndex { get; set; }

    /// <summary>
    /// Step object of the currently active step
    /// </summary>
    public FocusGuideStep? ActiveStep { get; set; }

    /// <summary>
    /// Step object of the previously active step
    /// </summary>
    public FocusGuideStep? PreviousStep { get; set; }
}
