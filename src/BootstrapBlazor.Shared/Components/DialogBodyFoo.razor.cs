// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Components;

namespace BootstrapBlazor.Shared.Components;

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
