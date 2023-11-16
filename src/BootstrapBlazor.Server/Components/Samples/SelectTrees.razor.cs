// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Shared.Samples;

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

    private TreeFoo BindModel { get; set; } = new TreeFoo();

    [NotNull]
    private List<TreeViewItem<string>>? BindItems { get; set; }

    private List<TreeViewItem<TreeFoo>> PopoverItems { get; } = TreeFoo.GetTreeItems();

    [NotNull]
    private string? Value { get; set; }

    private List<TreeViewItem<string>> EditItems { get; } = new List<TreeViewItem<string>>()
    {
        new TreeViewItem<string>("101") { Text = "Text 101" },
        new TreeViewItem<string>("102") { Text = "Text 102", Items = new List<TreeViewItem<string>>()
        {
            new TreeViewItem<string>("2021") { Text = "Text 2021" },
            new TreeViewItem<string>("2022") { Text = "Text 2022" }
        }},
    };

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
