// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Components;
using BootstrapBlazor.Shared.Common;
using BootstrapBlazor.Shared.Components;
using System.ComponentModel;

namespace BootstrapBlazor.Shared.Samples;

/// <summary>
/// 
/// </summary>
public sealed partial class Checkboxs
{
    private class Foo
    {
        [DisplayName("标签文字1")]
        public bool BindValue { get; set; }

        [DisplayName("标签文字2")]
        public bool BindValue1 { get; set; }
    }

    private Foo Model { get; set; } = new Foo();

    /// <summary>
    /// 
    /// </summary>
    private BlockLogger? Trace { get; set; }

    /// <summary>
    /// 
    /// </summary>
    private BlockLogger? BinderLog { get; set; }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="state"></param>
    /// <param name="value"></param>
    private Task OnStateChanged(CheckboxState state, string value)
    {
        Trace?.Log($"Checkbox state changed State: {state}");
        return Task.CompletedTask;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="state"></param>
    /// <param name="value"></param>
    private Task OnItemChanged(CheckboxState state, bool value)
    {
        BinderLog?.Log($"CheckboxState: {state} - Bind Value: {value}");
        return Task.CompletedTask;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="state"></param>
    /// <param name="value"></param>
    private Task OnItemChangedString(CheckboxState state, string value)
    {
        BinderLog?.Log($"CheckboxState: {state} - Bind Value: {value}");
        return Task.CompletedTask;
    }

    /// <summary>
    /// 
    /// </summary>
    private string BindString { get; set; } = "我爱 Blazor";

    /// <summary>
    /// 
    /// </summary>
    private bool BindValue { get; set; }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    private IEnumerable<AttributeItem> GetAttributes()
    {
        return new AttributeItem[]
        {
                new AttributeItem() {
                    Name = "ShowLabel",
                    Description = Localizer["Att1"],
                    Type = "bool",
                    ValueList = "true|false",
                    DefaultValue = "false"
                },
                new AttributeItem() {
                    Name = "ShowAfterLabel",
                    Description = Localizer["Att2"],
                    Type = "bool",
                    ValueList = "true|false",
                    DefaultValue = "false"
                },
                new AttributeItem() {
                    Name = "DisplayText",
                    Description = Localizer["Att3"],
                    Type = "string",
                    ValueList = " — ",
                    DefaultValue = " — "
                },
                new AttributeItem(){
                    Name = "IsDisabled",
                    Description = Localizer["Att4"],
                    Type = "boolean",
                    ValueList = "true / false",
                    DefaultValue = "false"
                },
                new AttributeItem()
                {
                    Name = "State",
                    Description = Localizer["Att5"],
                    Type = "CheckboxState",
                    ValueList = "Mixed / Checked / UnChecked",
                    DefaultValue = "UnChecked"
                },
        };
    }

    /// <summary>
    /// 获得事件方法
    /// </summary>
    /// <returns></returns>
    private IEnumerable<EventItem> GetEvents() => new EventItem[]
    {
            new EventItem()
            {
                Name = "OnStateChanged",
                Description = Localizer["Event1"],
                Type ="Action<CheckboxState, TItem>"
            },
            new EventItem()
            {
                Name = "StateChanged",
                Description = Localizer["Event2"],
                Type ="EventCallback<CheckboxState>"
            }
    };
}
