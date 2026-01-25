// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh"></para>
/// <para lang="en"></para>
/// </summary>
/// <typeparam name="TValue"></typeparam>
public abstract class PopoverDropdownBase<TValue> : ValidateBase<TValue>
{
    /// <summary>
    /// <para lang="zh">获得/设置 弹窗位置 默认为 Bottom</para>
    /// <para lang="en">Gets or sets Popover Placement. Default is Bottom</para>
    /// </summary>
    [Parameter]
    public Placement Placement { get; set; } = Placement.Bottom;

    /// <summary>
    /// <para lang="zh">获得/设置 自定义样式 参数 默认 null</para>
    /// <para lang="en">Gets or sets Custom Class. Default is null</para>
    /// </summary>
    /// <remarks>由 data-bs-custom-class 实现</remarks>
    [Parameter]
    public string? CustomClass { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否显示阴影 默认 true</para>
    /// <para lang="en">Gets or sets Whether to Show Shadow. Default is true</para>
    /// </summary>
    [Parameter]
    public bool ShowShadow { get; set; } = true;

    /// <summary>
    /// <para lang="zh">弹窗位置字符串</para>
    /// <para lang="en">Popover Placement String</para>
    /// </summary>
    protected string? PlacementString => Placement == Placement.Auto ? null : Placement.ToDescriptionString();

    /// <summary>
    /// <para lang="zh">获得 CustomClass 字符串</para>
    /// <para lang="en">Get CustomClass String</para>
    /// </summary>
    protected virtual string? CustomClassString => CssBuilder.Default(CustomClass)
        .AddClass("popover-region")
        .AddClass("shadow", ShowShadow)
        .Build();
}
