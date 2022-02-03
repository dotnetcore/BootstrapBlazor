// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Components;
using BootstrapBlazor.Shared.Common;
using BootstrapBlazor.Shared.Components;

namespace BootstrapBlazor.Shared.Samples;

/// <summary>
/// 
/// </summary>
public sealed partial class Collapses
{
    [NotNull]
    private BlockLogger? Trace { get; set; }

    private bool State { get; set; }

    private Task OnChanged(CollapseItem item)
    {
        Trace.Log($"{item.Text}: {item.IsCollapsed}");
        return Task.CompletedTask;
    }

    private void OnToggle()
    {
        State = !State;
    }

    /// <summary>
    /// 获得属性方法
    /// </summary>
    /// <returns></returns>
    private IEnumerable<AttributeItem> GetAttributes() => new AttributeItem[]
    {
            // TODO: 移动到数据库中
            new AttributeItem() {
                Name = "CollapseItems",
                Description = Localizer["CollapseItems"],
                Type = "RenderFragment",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "IsAccordion",
                Description = Localizer["IsAccordion"],
                Type = "bool",
                ValueList = "true|false",
                DefaultValue = "false"
            }
    };
}
