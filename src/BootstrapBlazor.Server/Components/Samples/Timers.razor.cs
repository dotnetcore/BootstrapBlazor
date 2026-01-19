// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Server.Components.Samples;

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

    private static AttributeItem[] GetAttributes() =>
    [
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
    ];
}
