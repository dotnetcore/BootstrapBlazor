// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">SelectOption 组件</para>
/// <para lang="en">SelectOption component</para>
/// </summary>
public class SelectOption : ComponentBase
{
    /// <summary>
    /// <para lang="zh">获得/设置 显示名称</para>
    /// <para lang="en">Gets or sets the display name</para>
    /// </summary>
    [Parameter]
    public string? Text { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 选项值</para>
    /// <para lang="en">Gets or sets the option value</para>
    /// </summary>
    [Parameter]
    public string? Value { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 选项是否被选中，默认值为 <c>false</c></para>
    /// <para lang="en">Gets or sets a value indicating whether the option is selected. Default is false</para>
    /// </summary>
    [Parameter]
    public bool Active { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 选项是否被禁用，默认值为 <c>false</c></para>
    /// <para lang="en">Gets or sets a value indicating whether the option is disabled. Default is false</para>
    /// </summary>
    [Parameter]
    public bool IsDisabled { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 分组名称</para>
    /// <para lang="en">Gets or sets the group name</para>
    /// </summary>
    [Parameter]
    public string? GroupName { get; set; }

    [CascadingParameter]
    private ISelect? Container { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        Container?.Add(ToSelectedItem());
    }

    /// <summary>
    /// <para lang="zh">Converts the current 实例 to a <see cref="SelectedItem"/>.</para>
    /// <para lang="en">Converts the current instance to a <see cref="SelectedItem"/>.</para>
    /// </summary>
    private SelectedItem ToSelectedItem() => new()
    {
        Active = Active,
        GroupName = GroupName ?? "",
        Text = Text ?? "",
        Value = Value ?? "",
        IsDisabled = IsDisabled
    };
}
