// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using System.ComponentModel;

namespace BootstrapBlazor.Server.Components.Samples;

/// <summary>
/// Switches
/// </summary>
public sealed partial class Switches
{
    [NotNull]
    private ConsoleLogger? Logger { get; set; }

    private bool BindValue { get; set; } = true;

    private bool? NullValue { get; set; }

    private void OnValueChanged(bool val)
    {
        BindValue = val;
        Logger.Log($"Switch CurrentValue: {val}");
    }

    private class Foo
    {
        [DisplayName("绑定标签")]
        public bool BindValue { get; set; }
    }

    private Foo Model { get; set; } = new Foo();

    /// <summary>
    /// 获得事件方法
    /// </summary>
    /// <returns></returns>
    private EventItem[] GetEvents() =>
    [
        new()
        {
            Name = "ValueChanged",
            Description = Localizer["SwitchesEventValueChanged"],
            Type = "EventCallback<bool>"
        }
    ];
}
