// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;

namespace BootstrapBlazor.Components;

/// <summary>
/// 
/// </summary>
public partial class Radio
{
    /// <summary>
    /// 
    /// </summary>
    [Parameter]
    public Func<SelectedItem, Task>? OnClick { get; set; }

    [CascadingParameter(Name = "GroupName")]
    [NotNull]
    private string? GroupName { get; set; }

    private void OnClickHandler()
    {
        OnClick?.Invoke(Value);
    }
}
