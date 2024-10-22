// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Author: Argo Zhang (argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Server.Components.Components;

/// <summary>
/// 
/// </summary>
public partial class CustomerSelectDialog
{
    private IEnumerable<SelectedItem>? Items2;
    private readonly IEnumerable<SelectedItem> Items3 = new SelectedItem[]
    {
        new("", "请选择 ..."),
        new("Beijing", "北京"),
        new("Shanghai", "上海")
    };

    /// <summary>
    /// 级联绑定菜单
    /// </summary>
    /// <param name="item"></param>
    private async Task OnCascadeBindSelectClick(SelectedItem item)
    {
        // 模拟异步通讯获取数据
        await Task.Delay(100);
        if (item.Value == "Beijing")
        {
            Items2 = new SelectedItem[]
            {
                new("1","朝阳区"),
                new("2","海淀区"),
            };
        }
        else if (item.Value == "Shanghai")
        {
            Items2 = new SelectedItem[]
            {
                new("1","静安区"),
                new("2","黄浦区"),
            };
        }
        else
        {
            Items2 = [];
        }
        StateHasChanged();
    }
}
