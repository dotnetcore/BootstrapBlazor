// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Components;

namespace BootstrapBlazor.Shared.Components;

class TreeDataFoo
{
    public string? Code { get; set; }

    public string? ParentCode { get; set; }

    public string? Text { get; set; }

    public string Icon { get; set; } = "fa fa-fa";

    public bool IsActive { get; set; }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public static List<TreeItem> GetTreeItems()
    {
        var items = new List<TreeDataFoo>
            {
                new TreeDataFoo() { Text = "导航一", Code = "1010" },
                new TreeDataFoo() { Text = "导航二", Code = "1020" },
                new TreeDataFoo() { Text = "导航三", Code = "1030" },

                new TreeDataFoo() { Text = "子菜单一", Code = "1040", ParentCode = "1020" },
                new TreeDataFoo() { Text = "子菜单二", Code = "1050", ParentCode = "1020" },
                new TreeDataFoo() { Text = "子菜单三", Code = "1060", ParentCode = "1020" },

                new TreeDataFoo() { Text = "孙菜单一", Code = "1070", ParentCode = "1050" },
                new TreeDataFoo() { Text = "孙菜单二", Code = "1080", ParentCode = "1050" },
                new TreeDataFoo() { Text = "孙菜单三", Code = "1090", ParentCode = "1050" },

                new TreeDataFoo() { Text = "曾孙菜单一", Code = "1100", ParentCode = "1080" },
                new TreeDataFoo() { Text = "曾孙菜单二", Code = "1110", ParentCode = "1080" },
                new TreeDataFoo() { Text = "曾孙菜单三", Code = "1120", ParentCode = "1080" },

                new TreeDataFoo() { Text = "曾曾孙菜单一", Code = "1130", ParentCode = "1100" },
                new TreeDataFoo() { Text = "曾曾孙菜单二", Code = "1140", ParentCode = "1100" },
                new TreeDataFoo() { Text = "曾曾孙菜单三", Code = "1150", ParentCode = "1100" }
            };

        // 算法获取属性结构数据
        return GetSubItems(null, items).ToList();
    }

    private static List<TreeItem> GetSubItems(string? parentCode, IEnumerable<TreeDataFoo> foos)
    {
        var subData = foos.Where(i => i.ParentCode == parentCode);

        return subData.Select(i => new TreeItem() { Text = i.Text, IsActive = i.IsActive, Items = GetSubItems(i.Code, foos) }).ToList();
    }
}
