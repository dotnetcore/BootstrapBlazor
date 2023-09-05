// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Shared.Samples;

/// <summary>
/// Steps
/// </summary>
public sealed partial class Steps
{
    [NotNull]
    private ConsoleLogger? Logger { get; set; }

    [NotNull]
    private List<StepItem>? Items { get; set; }

    /// <summary>
    /// 
    /// </summary>
    protected override void OnInitialized()
    {
        Items = new()
        {
            new StepItem() { Title = Localizer["StepItemI1Text"], Template = builder => { builder.OpenElement(0, "div"); builder.AddContent(1, Localizer["StepItemI1TextC"]); builder.CloseElement(); } },
            new StepItem() { Title = Localizer["StepItemI2Text"], Template = builder => { builder.OpenElement(0, "div"); builder.AddContent(1, Localizer["StepItemI2TextC"]); builder.CloseElement(); } },
            new StepItem() { Title = Localizer["StepItemI3Text"], Template = builder => { builder.OpenElement(0, "div"); builder.AddContent(1, Localizer["StepItemI3TextC"]); builder.CloseElement(); } }
        };
    }

    private void NextStep()
    {
        var item = Items.FirstOrDefault(i => i.Status == StepStatus.Process);
        if (item != null)
        {
            item.Status = StepStatus.Success;
            var index = Items.ToList().IndexOf(item) + 1;
            if (index < Items.Count)
            {
                Items.ElementAt(index).Status = StepStatus.Process;
            }
        }
        else
        {
            ResetStep();
            Items[0].Status = StepStatus.Process;
        }
    }

    private void ResetStep()
    {
        Items.ToList().ForEach(i =>
        {
            i.Status = StepStatus.Wait;
        });
    }

    private Task OnStatusChanged(StepStatus status)
    {
        Logger.Log($"Steps Status: {status}");
        return Task.CompletedTask;
    }

    private IEnumerable<AttributeItem> GetAttributes() => new AttributeItem[]
    {
        new()
        {
            Name = "Items",
            Description = Localizer["StepsItems"],
            Type = "IEnumerable<StepItem>",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "IsVertical",
            Description = Localizer["StepsIsVertical"],
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "false"
        },
        new()
        {
            Name = "IsCenter",
            Description = Localizer["StepsIsCenter"],
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "false"
        },
        new()
        {
            Name = "Status",
            Description = Localizer["StepsStatus"],
            Type = "StepStatus",
            ValueList = "Wait|Process|Finish|Error|Success",
            DefaultValue = "Wait"
        }
    };

    private IEnumerable<AttributeItem> GetStepItemAttributes() => new AttributeItem[]
    {
        new()
        {
            Name = "IsCenter",
            Description = Localizer["StepsAttrIsCenter"],
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "false"
        },
        new()
        {
            Name = "IsIcon",
            Description = Localizer["StepsAttrIsIcon"],
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "false"
        },
        new()
        {
            Name = "IsLast",
            Description = Localizer["StepsAttrIsLast"],
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "false"
        },
        new()
        {
            Name = "StepIndex",
            Description = Localizer["StepsAttrStepIndex"],
            Type = "int",
            ValueList = " — ",
            DefaultValue = "0"
        },
        new()
        {
            Name = "Space",
            Description = Localizer["StepsAttrSpace"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = "—"
        },
        new()
        {
            Name = "Title",
            Description = Localizer["StepsAttrTitle"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "Icon",
            Description = Localizer["StepsAttrIcon"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "Description",
            Description = Localizer["StepsAttrDescription"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "Status",
            Description = Localizer["StepsAttrStatus"],
            Type = "StepStatus",
            ValueList = "Wait|Process|Finish|Error|Success",
            DefaultValue = "Wait"
        },
        new()
        {
            Name = "Template",
            Description = Localizer["StepsAttrTemplate"],
            Type = "RenderFragment",
            ValueList = " — ",
            DefaultValue = " — "
        }
    };

    private IEnumerable<EventItem> GetEvents() => new List<EventItem>()
    {
        new()
        {
            Name = "OnStatusChanged",
            Description = Localizer["StepsEventOnStatusChanged"],
            Type ="Func<StepStatus, Task>"
        }
    };
}
