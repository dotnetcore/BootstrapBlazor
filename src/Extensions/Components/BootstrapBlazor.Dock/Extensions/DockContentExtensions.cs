// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// DockContent 扩展方法
/// </summary>
public static class DockContentExtensions
{
    /// <summary>
    /// 获得 所有组件
    /// </summary>
    /// <param name="content"></param>
    /// <returns></returns>
    public static List<DockContentItem> GetAllItems(this DockContent content)
    {
        var ret = new List<DockContentItem>();
        foreach (var item in content.Items)
        {
            if (item is DockContentItem dockContentItem)
            {
                ret.Add(dockContentItem);
            }
            else if (item is DockContent dockContent)
            {
                ret.AddRange(dockContent.GetAllItems());
            }
        }
        return ret;
    }
}
