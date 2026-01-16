// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">SelectOption component</para>
/// <para lang="en">SelectOption component</para>
/// </summary>
public class SelectOption : ComponentBase
{
    /// <summary>
    /// <para lang="zh">获得/设置 the 显示 name.</para>
    /// <para lang="en">Gets or sets the display name.</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public string? Text { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 the option value.</para>
    /// <para lang="en">Gets or sets the option value.</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public string? Value { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 a value indicating 是否 the option is selected. 默认为 <c>false</c>.</para>
    /// <para lang="en">Gets or sets a value indicating whether the option is selected. Default is <c>false</c>.</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public bool Active { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 a value indicating 是否 the option is disabled. 默认为 <c>false</c>.</para>
    /// <para lang="en">Gets or sets a value indicating whether the option is disabled. Default is <c>false</c>.</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public bool IsDisabled { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 the group name.</para>
    /// <para lang="en">Gets or sets the group name.</para>
    /// <para><version>10.2.2</version></para>
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
    /// <returns>A <see cref="SelectedItem"/> instance.</returns>
    private SelectedItem ToSelectedItem() => new()
    {
        Active = Active,
        GroupName = GroupName ?? "",
        Text = Text ?? "",
        Value = Value ?? "",
        IsDisabled = IsDisabled
    };
}
