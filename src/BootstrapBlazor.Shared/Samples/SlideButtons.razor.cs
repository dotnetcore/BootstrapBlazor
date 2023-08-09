// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Shared.Samples;

/// <summary>
/// FloatButton 组件示例
/// </summary>
public partial class SlideButtons
{
    private Placement Placement { get; set; }

    private readonly IEnumerable<SelectedItem> _items = new List<SelectedItem>()
    {
        new("Auto","auto"),
        new("Top", "top"),
        new("TopStart", "top-start"),
        new("TopCenter", "top-center"),
        new("TopEnd", "top-end"),
        new("Bottom", "bottom"),
        new("BottomStart", "bottom-start"),
        new("BottomCenter", "bottom-center"),
        new("BottomEnd", "bottom-end")
    };

    /// <summary>
    /// GetAttributes
    /// </summary>
    /// <returns></returns>
    private static IEnumerable<AttributeItem> GetAttributes() => new AttributeItem[]
    {
        new()
        {
            Name = nameof(FileViewer.Filename),
            Description = "Excel/Word 文件路径或者URL",
            Type = "string",
            ValueList = " — ",
            DefaultValue = " — "
        }
    };
}
