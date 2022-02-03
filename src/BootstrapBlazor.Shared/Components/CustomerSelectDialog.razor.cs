// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Components;

namespace BootstrapBlazor.Shared.Components;

/// <summary>
/// 
/// </summary>
public partial class CustomerSelectDialog
{
    private IEnumerable<SelectedItem>? Items2;
    private readonly IEnumerable<SelectedItem> Items3 = new SelectedItem[]
    {
            new SelectedItem ("", "请选择 ..."),
            new SelectedItem ("Beijing", "北京"),
            new SelectedItem ("Shanghai", "上海")
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
                    new SelectedItem("1","朝阳区"),
                    new SelectedItem("2","海淀区"),
            };
        }
        else if (item.Value == "Shanghai")
        {
            Items2 = new SelectedItem[]
            {
                    new SelectedItem("1","静安区"),
                    new SelectedItem("2","黄浦区"),
            };
        }
        else
        {
            Items2 = Enumerable.Empty<SelectedItem>();
        }
        StateHasChanged();
    }
}
