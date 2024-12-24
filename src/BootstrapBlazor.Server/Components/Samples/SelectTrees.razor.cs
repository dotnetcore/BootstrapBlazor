// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Server.Components.Samples;

/// <summary>
/// 
/// </summary>
public partial class SelectTrees
{
    private List<TreeViewItem<TreeFoo>> Items { get; } = TreeFoo.GetTreeItems();

    private List<TreeViewItem<TreeFoo>> DisableItems { get; } = TreeFoo.GetTreeItems();

    private List<TreeViewItem<TreeFoo>> BindingItems { get; } = TreeFoo.GetTreeItems();

    [NotNull]
    private TreeFoo? Model { get; set; }

    private TreeFoo BindModel { get; set; } = new();

    [NotNull]
    private List<TreeViewItem<string>>? BindItems { get; set; }

    private List<TreeViewItem<TreeFoo>> PopoverItems { get; } = TreeFoo.GetTreeItems();

    [NotNull]
    private string? Value { get; set; }

    private List<TreeViewItem<string>> EditItems { get; } =
    [
        new TreeViewItem<string>("101") { Text = "Text 101" },
        new TreeViewItem<string>("102") { Text = "Text 102", Items =
        [
            new TreeViewItem<string>("2021") { Text = "Text 2021" },
            new TreeViewItem<string>("2022") { Text = "Text 2022" }
        ]},
    ];

    /// <summary>
    /// OnInitialized
    /// </summary>
    protected override void OnInitialized()
    {
        Model = new TreeFoo()
        {
            Text = "Sub Menu Three",
            Id = "1090",
            ParentId = "1050",
            Icon = "fa-solid fa-font-awesome",
            IsActive = true
        };

        BindModel = new TreeFoo()
        {
            Id = "1090",
            ParentId = "1050",
            Icon = "fa-solid fa-font-awesome"
        };

        BindItems =
        [
            new TreeViewItem<string>("目录一")
            {
                Text = "目录一",
                Icon = "fa-solid fa-folder",
                ExpandIcon = "fa-solid fa-folder-open",
                Value = "101",
                HasChildren = true
            }
        ];
    }

    private async Task<IEnumerable<TreeViewItem<string>>> OnExpandNodeAsync(TreeViewItem<string> node)
    {
        await Task.Delay(500);

        if (node.Value == "101")
        {
            return [new TreeViewItem<string>("子目录一")
            {
                Text = "子目录一",
                Icon = "fa-solid fa-folder",
                ExpandIcon = "fa-solid fa-folder-open",
                Value = "1001",
                HasChildren = true
            }];
        }
        else if (node.Value == "1001")
        {
            return [
                new TreeViewItem<string>("文件一") { Text = "文件一", Icon = "fa-solid fa-file", Value = "10010" },
                new TreeViewItem<string>("文件二") { Text = "文件二", Icon = "fa-solid fa-file", Value = "10011" }
            ];
        }
        else
        {
            return [];
        }
    }
}
