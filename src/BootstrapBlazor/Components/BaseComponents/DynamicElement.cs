// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components.Web;

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">动态元素组件</para>
/// <para lang="en">动态元素component</para>
/// </summary>
public class DynamicElement : BootstrapComponentBase
{
    /// <summary>
    /// <para lang="zh">获得/设置 TagName 属性 默认为 div</para>
    /// <para lang="en">Gets or sets TagName property Default is为 div</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    [NotNull]
    public string? TagName { get; set; } = "div";

    /// <summary>
    /// <para lang="zh">获得/设置 是否触发 Click 事件 默认 true</para>
    /// <para lang="en">Gets or sets whether触发 Click 事件 Default is true</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public bool TriggerClick { get; set; } = true;

    /// <summary>
    /// <para lang="zh">获得/设置 是否阻止默认行为 默认 false</para>
    /// <para lang="en">Gets or sets whether阻止Default is行为 Default is false</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public bool PreventDefault { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否事件冒泡 默认为 false</para>
    /// <para lang="en">Gets or sets whether事件冒泡 Default is为 false</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public bool StopPropagation { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 Click 回调委托</para>
    /// <para lang="en">Gets or sets Click 回调delegate</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public Func<Task>? OnClick { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否触发 DoubleClick 事件 默认 true</para>
    /// <para lang="en">Gets or sets whether触发 DoubleClick 事件 Default is true</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public bool TriggerDoubleClick { get; set; } = true;

    /// <summary>
    /// <para lang="zh">获得/设置 DoubleClick 回调委托</para>
    /// <para lang="en">Gets or sets DoubleClick 回调delegate</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public Func<Task>? OnDoubleClick { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 OnContextMenu 回调委托</para>
    /// <para lang="en">Gets or sets OnContextMenu 回调delegate</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public Func<MouseEventArgs, Task>? OnContextMenu { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否触发 OnContextMenu 事件 默认 false</para>
    /// <para lang="en">Gets or sets whether触发 OnContextMenu 事件 Default is false</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public bool TriggerContextMenu { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 内容组件</para>
    /// <para lang="en">Gets or sets contentcomponent</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否生成指定 Tag 元素 默认 true 生成</para>
    /// <para lang="en">Gets or sets whether生成指定 Tag 元素 Default is true 生成</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public bool GenerateElement { get; set; } = true;

    /// <summary>
    /// <para lang="zh">BuildRenderTree 方法</para>
    /// <para lang="en">BuildRenderTree 方法</para>
    /// </summary>
    /// <param name="builder"></param>
    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        if (GenerateElement || IsTriggerClick() || IsTriggerDoubleClick())
        {
            builder.OpenElement(0, TagName);
            if (AdditionalAttributes != null)
            {
                builder.AddMultipleAttributes(1, AdditionalAttributes);
            }
        }

        if (IsTriggerClick())
        {
            builder.AddAttribute(2, "onclick", EventCallback.Factory.Create<MouseEventArgs>(this, OnTriggerClick));
        }

        if (IsTriggerDoubleClick())
        {
            builder.AddAttribute(3, "ondblclick", EventCallback.Factory.Create<MouseEventArgs>(this, OnTriggerDoubleClick));
        }

        if (IsTriggerClick() || IsTriggerDoubleClick())
        {
            builder.AddEventPreventDefaultAttribute(4, "onclick", PreventDefault);
            builder.AddEventStopPropagationAttribute(5, "onclick", StopPropagation);
        }

        if (IsTriggerContextMenu())
        {
            builder.AddAttribute(6, "oncontextmenu", EventCallback.Factory.Create<MouseEventArgs>(this, OnTriggerContextMenu));
            builder.AddEventPreventDefaultAttribute(7, "oncontextmenu", true);
        }

        builder.AddContent(8, ChildContent);

        if (GenerateElement || IsTriggerClick() || IsTriggerDoubleClick())
        {
            builder.CloseElement();
        }
    }

    private bool IsTriggerClick() => TriggerClick && OnClick != null;

    private bool IsTriggerDoubleClick() => TriggerDoubleClick && OnDoubleClick != null;

    private bool IsTriggerContextMenu() => TriggerContextMenu && OnContextMenu != null;

    private async Task OnTriggerClick()
    {
        if (OnClick != null)
        {
            await OnClick();
        }
    }

    private async Task OnTriggerDoubleClick()
    {
        if (OnDoubleClick != null)
        {
            await OnDoubleClick();
        }
    }

    private async Task OnTriggerContextMenu(MouseEventArgs e)
    {
        if (OnContextMenu != null)
        {
            await OnContextMenu(e);
        }
    }
}
