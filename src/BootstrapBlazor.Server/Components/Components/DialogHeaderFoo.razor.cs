// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Author: Argo Zhang (argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Server.Components.Components;

/// <summary>
/// 
/// </summary>
public partial class DialogHeaderFoo
{
    [NotNull]
    private IEnumerable<SelectedItem>? Items { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [Parameter]
    public string? Value { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [Parameter]
    public Func<string, Task>? OnValueChanged { get; set; }

    /// <summary>
    /// 
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        Items = new[]
        {
                new SelectedItem("beijing", "北京"),
                new SelectedItem("shanghai", "上海")
            };
    }

    private async Task OnSelectedItemChanged(SelectedItem item)
    {
        Value = item.Value;
        if (OnValueChanged != null)
        {
            await OnValueChanged(Value);
        }
    }
}
