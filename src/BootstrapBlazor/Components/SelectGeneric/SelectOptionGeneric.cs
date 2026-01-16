// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">SelectOptionPro 组件</para>
/// <para lang="en">SelectOptionPro Component</para>
/// </summary>
public class SelectOptionGeneric<TValue> : ComponentBase
{
    /// <summary>
    /// <para lang="zh">获得/设置 显示名称</para>
    /// <para lang="en">Get/Set Display Name</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public string? Text { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 选项值</para>
    /// <para lang="en">Get/Set Option Value</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public TValue? Value { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否选中 默认 false</para>
    /// <para lang="en">Get/Set Whether selected. Default false</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public bool Active { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否禁用 默认 false</para>
    /// <para lang="en">Get/Set Whether disabled. Default false</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public bool IsDisabled { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 分组名称</para>
    /// <para lang="en">Get/Set Group Name</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public string? GroupName { get; set; }

    /// <summary>
    /// <para lang="zh">父组件通过级联参数获得</para>
    /// <para lang="en">Parent component obtained through cascading parameter</para>
    /// </summary>
    [CascadingParameter]
    private ISelectGeneric<TValue>? Container { get; set; }

    /// <summary>
    /// <para lang="zh">OnInitialized 方法</para>
    /// <para lang="en">OnInitialized Method</para>
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        Container?.Add(ToSelectedItem());
    }

    private SelectedItem<TValue> ToSelectedItem() => new(Value!, Text ?? "")
    {
        Active = Active,
        GroupName = GroupName ?? "",
        IsDisabled = IsDisabled
    };
}
