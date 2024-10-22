// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Server.Components.Components;

/// <summary>
/// 
/// </summary>
public partial class DialogBodyFoo
{
    private string? Value { get; set; }

    private List<SelectedItem> Items { get; } = new(new[]
    {
            new SelectedItem("beijing", "北京"),
            new SelectedItem("shanghai", "上海")
        });

    /// <summary>
    /// 
    /// </summary>
    public Task UpdateAsync(string val)
    {
        Value = Items.First(i => i.Value == val).Text;
        StateHasChanged();
        return Task.CompletedTask;
    }
}
