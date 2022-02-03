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
public sealed partial class Stepss
{
    private IEnumerable<StepItem> Items { get; set; } = new StepItem[3]
    {
            new StepItem() { Title = "步骤一" , Template = builder => { builder.OpenElement(0, "div"); builder.AddContent(1, "步骤一"); builder.CloseElement(); } },
            new StepItem() { Title = "步骤二", Template = builder => { builder.OpenElement(0, "div"); builder.AddContent(1, "步骤二"); builder.CloseElement(); } },
            new StepItem() { Title = "步骤三", Template = builder => { builder.OpenElement(0, "div"); builder.AddContent(1, "步骤三"); builder.CloseElement(); } }
    };

    private void NextStep()
    {
        var item = Items.FirstOrDefault(i => i.Status == StepStatus.Process);
        if (item != null)
        {
            item.Status = StepStatus.Success;
            var index = Items.ToList().IndexOf(item) + 1;
            if (index < Items.Count())
            {
                Items.ElementAt(index).Status = StepStatus.Process;
            }
        }
        else
        {
            ResetStep();
            Items.ElementAt(0).Status = StepStatus.Process;
        }
    }

    private void ResetStep()
    {
        Items.ToList().ForEach(i =>
        {
            i.Status = StepStatus.Wait;
        });
    }

    /// <summary>
    /// 
    /// </summary>
    [NotNull]
    private BlockLogger? Trace { get; set; }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="status"></param>
    private Task OnStatusChanged(StepStatus status)
    {
        Trace.Log($"Steps Status: {status}");
        return Task.CompletedTask;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    private IEnumerable<AttributeItem> GetAttributes() => new AttributeItem[]
    {
            new AttributeItem() {
                Name = "Items",
                Description = Localizer["Desc1"],
                Type = "IEnumerable<StepItem>",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "IsVertical",
                Description = Localizer["Desc2"],
                Type = "bool",
                ValueList = "true|false",
                DefaultValue = "false"
            },
            new AttributeItem() {
                Name = "IsCenter",
                Description = Localizer["Desc3"],
                Type = "bool",
                ValueList = "true|false",
                DefaultValue = "false"
            },
            new AttributeItem() {
                Name = "Status",
                Description = Localizer["Desc4"],
                Type = "StepStatus",
                ValueList = "Wait|Process|Finish|Error|Success",
                DefaultValue = "Wait"
            }
    };

    private IEnumerable<AttributeItem> GetStepItemAttributes() => new AttributeItem[]
    {
            new AttributeItem() {
                Name = "IsCenter",
                Description = Localizer["Att1"],
                Type = "bool",
                ValueList = "true|false",
                DefaultValue = "false"
            },
            new AttributeItem() {
                Name = "IsIcon",
                Description = Localizer["Att2"],
                Type = "bool",
                ValueList = "true|false",
                DefaultValue = "false"
            },
            new AttributeItem() {
                Name = "IsLast",
                Description = Localizer["Att3"],
                Type = "bool",
                ValueList = "true|false",
                DefaultValue = "false"
            },
            new AttributeItem() {
                Name = "StepIndex",
                Description = Localizer["Att4"],
                Type = "int",
                ValueList = " — ",
                DefaultValue = "0"
            },
            new AttributeItem() {
                Name = "Space",
                Description = Localizer["Att5"],
                Type = "string",
                ValueList = " — ",
                DefaultValue = "—"
            },
            new AttributeItem() {
                Name = "Title",
                Description = Localizer["Att6"],
                Type = "string",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "Icon",
                Description = Localizer["Att7"],
                Type = "string",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "Description",
                Description = Localizer["Att8"],
                Type = "string",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "Status",
                Description = Localizer["Att9"],
                Type = "StepStatus",
                ValueList = "Wait|Process|Finish|Error|Success",
                DefaultValue = "Wait"
            },
            new AttributeItem() {
                Name = "Template",
                Description = Localizer["Att10"],
                Type = "RenderFragment",
                ValueList = " — ",
                DefaultValue = " — "
            }
    };

    private IEnumerable<EventItem> GetEvents() => new List<EventItem>()
        {
            new EventItem()
            {
                Name = "OnStatusChanged",
                Description = Localizer["Event1"],
                Type ="Func<StepStatus, Task>"
            }
        };
}
