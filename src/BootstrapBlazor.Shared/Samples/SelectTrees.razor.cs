// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Shared.Samples;

/// <summary>
/// 
/// </summary>
public partial class SelectTrees
{
    [Inject]
    [NotNull]
    private IStringLocalizer<SelectTrees>? Localizer { get; set; }

    private List<TreeViewItem<TreeFoo>> Items { get; } = TreeFoo.GetTreeItems();

    [NotNull]
    private List<TreeViewItem<string>>? BindItems { get; set; }

    [NotNull]
    private TreeFoo? Model { get; set; }

    private TreeFoo BindModel { get; set; } = new TreeFoo();

    /// <summary>
    /// <inheritdoc/>
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

        BindItems = new List<TreeViewItem<string>>()
        {
            new TreeViewItem<string>("目录一")
            {
                Text ="目录一",
                Icon = "fa-solid fa-folder",
                ExpandIcon = "fa-solid fa-folder-open",
                Items = new List<TreeViewItem<string>>()
                {
                    new TreeViewItem<string>("子目录一")
                    {
                        Text ="子目录一",
                        Icon = "fa-solid fa-folder",
                        ExpandIcon = "fa-solid fa-folder-open",
                        Items = new List<TreeViewItem<string>>()
                        {
                            new TreeViewItem<string>("文件一") { Text = "文件一", Icon = "fa-solid fa-file", IsActive = true },
                            new TreeViewItem<string>("文件二") { Text = "文件二", Icon = "fa-solid fa-file" }
                        }
                    }
                }
            }
        };
    }
}
