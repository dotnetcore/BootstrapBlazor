// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BootstrapBlazor.Shared.Samples;
public partial class Gantts
{
    private ConsoleLogger? Log { get; set; }

    private List<GanttItem>? Items { get; set; } = new List<GanttItem>()
    {
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
    };

    private Task OnClick(GanttItem item)
    {
        Log?.Log("OnClick");
        return Task.CompletedTask;
    }

    private Task OnDataChanged(GanttItem item, string start, string end)
    {
        Log?.Log($"OnDataChanged,start:{start},end:{end}");
        return Task.CompletedTask;
    }

    private Task OnProgressChanged(GanttItem item, int progress)
    {
        Log?.Log($"OnProgressChanged,progress:{progress}");
        return Task.CompletedTask;
    }
}
