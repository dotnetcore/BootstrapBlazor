// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Server.Components.Samples;

/// <summary>
/// Splits
/// </summary>
public sealed partial class Splits
{
    private bool _showBarHandle = true;

    private bool _isCollapsible = false;

    private string? _barHandleText;

    private string? _collapsibleText;

    private ConsoleLogger _logger = default!;

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();
        _barHandleText = _showBarHandle ? Localizer["SplitsBarHandleShow"] : Localizer["SplitsBarHandleHide"];
        _collapsibleText = _showBarHandle ? Localizer["SplitsCollapsibleTrue"] : Localizer["SplitsCollapsibleFalse"];
    }

    private Task OnShowBarHandle(bool v)
    {
        _showBarHandle = v;
        _barHandleText = _showBarHandle ? Localizer["SplitsBarHandleShow"] : Localizer["SplitsBarHandleHide"];
        StateHasChanged();
        return Task.CompletedTask;
    }

    private Task OnCollapsible(bool v)
    {
        _isCollapsible = v;
        _collapsibleText = _showBarHandle ? Localizer["SplitsCollapsibleTrue"] : Localizer["SplitsCollapsibleFalse"];
        StateHasChanged();
        return Task.CompletedTask;
    }

    private Task OnResizedAsync(SplitterResizedEventArgs args)
    {
        _logger.Log($"FirstPanelSize: {args.FirstPanelSize} IsCollapsed: {args.IsCollapsed} IsExpanded: {args.IsExpanded}");
        return Task.CompletedTask;
    }

    private Split Split1 = default!;

    private Task OnSetLeft(string leftWidth) => Split1.SetLeftWidth(leftWidth);

    /// <summary>
    /// 获得属性方法
    /// </summary>
    /// <returns></returns>
    private AttributeItem[] GetAttributes() =>
    [
        new()
        {
            Name = "IsVertical",
            Description = Localizer["SplitsIsVertical"],
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "false"
        },
        new()
        {
            Name = "ShowBarHandle",
            Description = Localizer["SplitsShowBarHandle"],
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "true"
        },
        new()
        {
            Name = "Basis",
            Description = Localizer["SplitsBasis"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = "50%"
        },
        new()
        {
            Name = "FirstPaneTemplate",
            Description = Localizer["SplitsFirstPaneTemplate"],
            Type = "RenderFragment",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "FirstPaneMinimumSize",
            Description = Localizer["SplitsFirstPaneMinimumSize"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "SecondPaneTemplate",
            Description = Localizer["SplitsSecondPaneTemplate"],
            Type = "RenderFragment",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "SecondPaneMinimumSize",
            Description = Localizer["SplitsSecondPaneMinimumSize"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "IsCollapsible",
            Description = Localizer["SplitsIsCollapsible"],
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "false"
        },
        new()
        {
            Name = "IsKeepOriginalSize",
            Description = Localizer["SplitsIsKeepOriginalSize"],
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "true"
        },
        new()
        {
            Name = "OnResizedAsync",
            Description = Localizer["SplitsOnResizedAsync"],
            Type = "Func<SplitterResizedEventArgs, Task>",
            ValueList = " — ",
            DefaultValue = " — "
        }
    ];
}
