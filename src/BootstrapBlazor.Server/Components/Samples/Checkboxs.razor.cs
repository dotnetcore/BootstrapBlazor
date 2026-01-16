// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using System.ComponentModel;

namespace BootstrapBlazor.Server.Components.Samples;

/// <summary>
/// Checkboxs
/// </summary>
public sealed partial class Checkboxs
{
    [Inject, NotNull]
    private SwalService? SwalService { get; set; }

    private Foo Model { get; set; } = new Foo();

    private class Foo
    {
        [DisplayName("标签文字1")]
        public bool BindValue { get; set; }

        [DisplayName("标签文字2")]
        public bool BindValue1 { get; set; }
    }

    [NotNull]
    private ConsoleLogger? BindStringLogger { get; set; }

    private string BindString { get; set; } = "我爱 Blazor";

    private Task OnItemChangedString(CheckboxState state, string value)
    {
        BindStringLogger.Log($"CheckboxState: {state} - Bind Value: {value}");
        return Task.CompletedTask;
    }

    private bool BindValue { get; set; }

    [NotNull]
    private ConsoleLogger? OnStateChangedLogger { get; set; }

    private Task OnItemChanged(CheckboxState state, bool value)
    {
        OnStateChangedLogger.Log($"CheckboxState: {state} - Bind Value: {value}");
        return Task.CompletedTask;
    }

    [NotNull]
    private ConsoleLogger? NormalLogger { get; set; }

    private Task OnStateChanged(CheckboxState state, string value)
    {
        NormalLogger.Log($"Checkbox state changed State: {state}");
        return Task.CompletedTask;
    }

    private bool SelectedValue { get; set; }

    private Task<bool> OnBeforeStateChanged(CheckboxState state) => SwalService.ShowModal(new SwalOption()
    {
        Title = Localizer["OnBeforeStateChangedSwalTitle"],
        Content = Localizer["OnBeforeStateChangedSwalContent"]
    });
    /// <summary>
    /// 获得事件方法
    /// </summary>
    /// <returns></returns>
    private EventItem[] GetEvents() =>
    [
        new()
        {
            Name = "OnBeforeStateChanged",
            Description = Localizer["OnBeforeStateChanged"],
            Type ="Action<CheckboxState, TItem>"
        },
        new()
        {
            Name = "OnStateChanged",
            Description = Localizer["OnStateChanged"],
            Type ="Action<CheckboxState, TItem>"
        },
        new()
        {
            Name = "StateChanged",
            Description = Localizer["StateChanged"],
            Type ="EventCallback<CheckboxState>"
        }
    ];
}
