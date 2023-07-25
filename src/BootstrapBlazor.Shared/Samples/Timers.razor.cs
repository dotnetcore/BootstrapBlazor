// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Shared.Samples;

/// <summary>
/// Timers
/// </summary>
public sealed partial class Timers
{
    [NotNull]
    private ConsoleLogger? Logger { get; set; }

    private Task OnTimeout()
    {
        Logger.Log("timer time up");
        return Task.CompletedTask;
    }

    private Task OnCancel()
    {
        Logger.Log("timer canceled");
        return Task.CompletedTask;
    }

    private static IEnumerable<AttributeItem> GetAttributes() => new AttributeItem[]
    {
        new()
        {
            Name = "Width",
            Description = "Component width",
            Type = "int",
            ValueList = " — ",
            DefaultValue = "300"
        },
        new()
        {
            Name = "StrokeWidth",
            Description = "Progress bar width",
            Type = "int",
            ValueList = " — ",
            DefaultValue = "6"
        },
        new()
        {
            Name = "IsVibrate",
            Description = "Device vibrates when countdown ends",
            Type = "bool",
            ValueList = "true/false",
            DefaultValue = "true"
        },
        new()
        {
            Name = "Value",
            Description = "Countdown time",
            Type = "Timespan",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "Color",
            Description = "Progress bar color",
            Type = "Color",
            ValueList = "Primary / Secondary / Success / Danger / Warning / Info / Dark",
            DefaultValue = "Primary"
        }
    };
}
