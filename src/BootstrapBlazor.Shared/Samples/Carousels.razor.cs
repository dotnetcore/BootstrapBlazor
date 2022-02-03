// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Shared.Common;
using BootstrapBlazor.Shared.Components;

namespace BootstrapBlazor.Shared.Samples;

/// <summary>
/// 
/// </summary>
public sealed partial class Carousels
{
    /// <summary>
    /// 
    /// </summary>
    private BlockLogger? Trace { get; set; }

    private static List<string> Images => new()
    {
        "_content/BootstrapBlazor.Shared/images/Pic0.jpg",
        "_content/BootstrapBlazor.Shared/images/Pic1.jpg",
        "_content/BootstrapBlazor.Shared/images/Pic2.jpg"
    };

    /// <summary>
    /// 
    /// </summary>
    /// <param name="imageUrl"></param>
    /// <returns></returns>
    private Task OnClick(string imageUrl)
    {
        Trace?.Log($"Image Clicked: {imageUrl}");
        return Task.CompletedTask;
    }

    /// <summary>
    /// 获得属性方法
    /// </summary>
    /// <returns></returns>
    private IEnumerable<AttributeItem> GetAttributes() => new AttributeItem[]
    {
            // TODO: 移动到数据库中
            new AttributeItem() {
                Name = "Images",
                Description = Localizer["Images"],
                Type = "IEnumerable<string>",
                ValueList = "—",
                DefaultValue = "—"
            },
            new AttributeItem() {
                Name = "IsFade",
                Description = Localizer["IsFade"],
                Type = "boolean",
                ValueList = " — ",
                DefaultValue = "false"
            },
            new AttributeItem() {
                Name = "Width",
                Description = Localizer["Width"],
                Type = "int",
                ValueList = " — ",
                DefaultValue = "—"
            },
            new AttributeItem() {
                Name = "OnClick",
                Description = Localizer["OnClick"],
                Type = "Func<string, Task>",
                ValueList = " — ",
                DefaultValue = " — "
            }
    };
}
