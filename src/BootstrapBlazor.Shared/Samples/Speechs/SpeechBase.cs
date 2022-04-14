// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Components;

namespace BootstrapBlazor.Shared.Samples;

/// <summary>
/// 
/// </summary>
public class SpeechBase : BootstrapComponentBase
{
    /// <summary>
    /// 
    /// </summary>
    protected List<SelectedItem> SpeechItems { get; } = new List<SelectedItem>
    {
        new ("Baidu", "Baidu 语音"),
        new ("Azure", "Azure 语音")
    };

    /// <summary>
    /// 
    /// </summary>
    [NotNull]
    protected string? SpeechItem { get; set; } = "Baidu";
}
