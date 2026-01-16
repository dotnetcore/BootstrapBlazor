// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">DropdownWidgetItem 组件</para>
/// <para lang="en">DropdownWidgetItem Component</para>
/// </summary>
public class DropdownWidgetItem : BootstrapComponentBase
{
    /// <summary>
    /// <para lang="zh">获得/设置 挂件图标</para>
    /// <para lang="en">Get/Set Widget Icon</para>
    /// </summary>
    [Parameter]
    public string? Icon { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 DropdownWidgetItem 组件项目的悬浮提示信息</para>
    /// <para lang="en">Get/Set Tooltip Title</para>
    /// </summary>
    [Parameter]
    public string? Title { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 徽章颜色 默认为 Color.Success</para>
    /// <para lang="en">Get/Set Badge Color. Default is Color.Success</para>
    /// </summary>
    [Parameter]
    public Color BadgeColor { get; set; } = Color.Success;

    /// <summary>
    /// <para lang="zh">获得/设置 Header 颜色 默认为 Color.Primary</para>
    /// <para lang="en">Get/Set Header Color. Default is Color.Primary</para>
    /// </summary>
    [Parameter]
    public Color HeaderColor { get; set; } = Color.Primary;

    /// <summary>
    /// <para lang="zh">获得/设置 徽章显示数量</para>
    /// <para lang="en">Get/Set Badge Number</para>
    /// </summary>
    [Parameter]
    public string? BadgeNumber { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否显示小箭头 默认为 true 显示</para>
    /// <para lang="en">Get/Set Whether to Show Arrow. Default is true</para>
    /// </summary>
    [Parameter]
    public bool ShowArrow { get; set; } = true;

    /// <summary>
    /// <para lang="zh">获得/设置 Header 模板</para>
    /// <para lang="en">Get/Set Header Template</para>
    /// </summary>
    [Parameter]
    public RenderFragment? HeaderTemplate { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 Body 模板</para>
    /// <para lang="en">Get/Set Body Template</para>
    /// </summary>
    [Parameter]
    public RenderFragment? BodyTemplate { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 Footer 模板</para>
    /// <para lang="en">Get/Set Footer Template</para>
    /// </summary>
    [Parameter]
    public RenderFragment? FooterTemplate { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 父组件通过级联参数获得</para>
    /// <para lang="en">Get/Set Parent Container</para>
    /// </summary>
    [CascadingParameter]
    private DropdownWidget? Container { get; set; }

    /// <summary>
    /// <para lang="zh">OnInitialized 方法</para>
    /// <para lang="en">OnInitialized Method</para>
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        Container?.Add(this);
    }
}
