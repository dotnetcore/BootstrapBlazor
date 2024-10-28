﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// SelectOption 组件
/// </summary>
public class SelectOption : ComponentBase
{
    /// <summary>
    /// 获得/设置 显示名称
    /// </summary>
    [Parameter]
    public string? Text { get; set; }

    /// <summary>
    /// 获得/设置 选项值
    /// </summary>
    [Parameter]
    public string? Value { get; set; }

    /// <summary>
    /// 获得/设置 是否选中 默认 false
    /// </summary>
    [Parameter]
    public bool Active { get; set; }

    /// <summary>
    /// 获得/设置 是否禁用 默认 false
    /// </summary>
    [Parameter]
    public bool IsDisabled { get; set; }

    /// <summary>
    /// 获得/设置 分组名称
    /// </summary>
    [Parameter]
    public string? GroupName { get; set; }

    /// <summary>
    /// 父组件通过级联参数获得
    /// </summary>
    [CascadingParameter]
    private ISelect? Container { get; set; }

    /// <summary>
    /// OnInitialized 方法
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        Container?.Add(ToSelectedItem());
    }

    private SelectedItem ToSelectedItem() => new()
    {
        Active = Active,
        GroupName = GroupName ?? "",
        Text = Text ?? "",
        Value = Value ?? "",
        IsDisabled = IsDisabled
    };
}
