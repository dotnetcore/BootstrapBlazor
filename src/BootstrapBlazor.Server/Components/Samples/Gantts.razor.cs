// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BootstrapBlazor.Server.Components.Samples;

/// <summary>
/// 
/// </summary>
public partial class Gantts
{
    private ConsoleLogger? Log { get; set; }

    private Gantt? Gantt { get; set; }

    private IEnumerable<SegmentedOption<string>>? ViewModes { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        ViewModes = new List<SegmentedOption<string>>()
        {
            new SegmentedOption<string>()
            {
                 Text = "Quarter Day",
                 Value= "Quarter Day"
            },
            new SegmentedOption<string>()
            {
                 Text = "Half Day",
                 Value= "Half Day"
            },
            new SegmentedOption<string>()
            {
                 Text = "Day",
                 Value= "Day"
            },
            new SegmentedOption<string>()
            {
                 Text = "Week",
                 Value= "Week"
            },
            new SegmentedOption<string>()
            {
                 Text = "Month",
                 Value= "Month"
            }
        };
    }

    private List<GanttItem>? Items { get; set; } =
    [
        new GanttItem()
        {
            Id = "Task 0",
            Start = "2018-10-01",
            End = "2018-10-08",
            Progress = 20,
            Name = "Redesign website"
        },
        new GanttItem()
        {
            Id = "Task 1",
            Start = "2018-10-03",
            End = "2018-10-06",
            Progress = 5,
            Name = "Write new content",
            Dependencies = "Task 0"
        },
        new GanttItem()
        {
            Id = "Task 2",
            Start = "2018-10-04",
            End = "2018-10-08",
            Progress = 10,
            Name = "Apply new styles",
            Dependencies = "Task 1"
        },
        new GanttItem()
        {
            Id = "Task 3",
            Start = "2018-10-08",
            End = "2018-10-09",
            Progress = 5,
            Name = "Review",
            Dependencies = "Task 2"
        },
        new GanttItem()
        {
            Id = "Task 4",
            Start = "2018-10-08",
            End = "2018-10-10",
            Progress = 0,
            Name = "Deploy",
            Dependencies = "Task 2"
        },
        new GanttItem()
        {
            Id = "Task 5",
            Start = "2018-10-01",
            End = "2018-10-08",
            Progress = 0,
            Name = "Go Live!",
            Dependencies = "Task 4"
        }
    ];

    private Task OnClick(GanttItem item)
    {
        Log?.Log($"OnClick:{item.Name}");
        return Task.CompletedTask;
    }

    private Task OnDataChanged(GanttItem item, string start, string end)
    {
        Log?.Log($"OnDataChanged: start:{start},end:{end}");
        return Task.CompletedTask;
    }

    private Task OnProgressChanged(GanttItem item, int progress)
    {
        Log?.Log($"OnProgressChanged: progress:{progress}");
        return Task.CompletedTask;
    }

    private async Task OnValueChanged(string value)
    {
        await Gantt!.ChangeVieMode(value);
        StateHasChanged();
    }

    private IEnumerable<AttributeItem> GetAttributeItems()
    {
        return new List<AttributeItem>()
        {
            new AttributeItem()
            {
                Name = nameof(Gantt.Items),
                Type = "IEnumerable<GanttItem>",
                Description = Localizer["AttrItems"],
                DefaultValue = "-"
            },
            new AttributeItem()
            {
                Name = nameof(Gantt.OnClick),
                Type = "Func<GanttItem,Task>",
                Description = Localizer["AttrOnClick"],
                DefaultValue = "-"
            },
            new AttributeItem()
            {
                Name = nameof(Gantt.OnDataChanged),
                Type = "Func<GanttItem,string,string,Task>",
                Description = Localizer["AttrOnDataChanged"],
                DefaultValue = "-"
            },
            new AttributeItem()
            {
                Name = nameof(Gantt.OnProgressChanged),
                Type = "Func<GanttItem,int,Task>",
                Description = Localizer["AttrOnProgressChanged"],
                DefaultValue = "-"
            },
            new AttributeItem()
            {
                Name = nameof(Gantt.Option),
                Type = "Class",
                Description = Localizer["AttrOption"],
                ValueList = "",
                DefaultValue = "-"
            },
        };
    }

    private IEnumerable<AttributeItem> GetMethodItems()
    {
        return new List<AttributeItem>()
        {
            new AttributeItem()
            {
                Name = nameof(Gantt.ChangeVieMode),
                Type = "Method",
                Description = Localizer["AttrMethod"],
                DefaultValue = "-"
            }
        };
    }
}
