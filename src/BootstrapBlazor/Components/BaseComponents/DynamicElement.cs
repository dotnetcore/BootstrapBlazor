// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components.Web;

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">动态元素组件</para>
/// <para lang="en">Dynamic element component</para>
/// </summary>
public class DynamicElement : BootstrapComponentBase
{
    /// <summary>
    /// <para lang="zh">获得/设置 TagName 属性 默认为 div</para>
    /// <para lang="en">Gets or sets the TagName property. Default is div</para>
    /// </summary>
    [Parameter]
    [NotNull]
    public string? TagName { get; set; } = "div";

    /// <summary>
    /// <para lang="zh">获得/设置 是否触发 Click 事件 默认 true</para>
    /// <para lang="en">Gets or sets whether to trigger Click event. Default is true</para>
    /// </summary>
    [Parameter]
    public bool TriggerClick { get; set; } = true;

    /// <summary>
    /// <para lang="zh">获得/设置 是否阻止默认行为 默认 false</para>
    /// <para lang="en">Gets or sets whether to prevent default behavior. Default is false</para>
    /// </summary>
    [Parameter]
    public bool PreventDefault { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否事件冒泡 默认为 false</para>
    /// <para lang="en">Gets or sets whether to stop event propagation. Default is false</para>
    /// </summary>
    [Parameter]
    public bool StopPropagation { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 Click 回调委托</para>
    /// <para lang="en">Gets or sets the Click callback delegate</para>
    /// </summary>
    [Parameter]
    public Func<Task>? OnClick { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否触发 DoubleClick 事件 默认 true</para>
    /// <para lang="en">Gets or sets whether to trigger DoubleClick event. Default is true</para>
    /// </summary>
    [Parameter]
    public bool TriggerDoubleClick { get; set; } = true;

    /// <summary>
    /// <para lang="zh">获得/设置 DoubleClick 回调委托</para>
    /// <para lang="en">Gets or sets the DoubleClick callback delegate</para>
    /// </summary>
    [Parameter]
    public Func<Task>? OnDoubleClick { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 OnContextMenu 回调委托</para>
    /// <para lang="en">Gets or sets the OnContextMenu callback delegate</para>
    /// </summary>
    [Parameter]
    public Func<MouseEventArgs, Task>? OnContextMenu { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否触发 OnContextMenu 事件 默认 false</para>
    /// <para lang="en">Gets or sets whether to trigger OnContextMenu event. Default is false</para>
    /// </summary>
    [Parameter]
    public bool TriggerContextMenu { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 内容组件</para>
    /// <para lang="en">Gets or sets the child content</para>
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否生成指定 Tag 元素 默认 true 生成</para>
    /// <para lang="en">Gets or sets whether to generate the specified Tag element. Default is true</para>
    /// </summary>
    [Parameter]
    public bool GenerateElement { get; set; } = true;

    /// <summary>
    /// <para lang="zh">获得/设置 元素唯一标识 Key 默认 null</para>
    /// <para lang="en">Gets or sets the unique key of the element. Default null</para>
    /// </summary>
    [Parameter]
    public object? Key { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="builder"></param>
    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        if (GenerateElement || IsTriggerClick() || IsTriggerDoubleClick())
        {
            builder.OpenElement(0, TagName);

            if (Key != null)
            {
                builder.SetKey(Key);
            }
            if (AdditionalAttributes != null)
            {
                builder.AddMultipleAttributes(1, AdditionalAttributes);
            }
        }

        if (IsTriggerClick())
        {
            builder.AddAttribute(2, "onclick", EventCallback.Factory.Create<MouseEventArgs>(this, OnTriggerClick));
            builder.AddEventPreventDefaultAttribute(3, "onclick", PreventDefault);
            builder.AddEventStopPropagationAttribute(4, "onclick", StopPropagation);
        }

        if (IsTriggerDoubleClick())
        {
            builder.AddAttribute(5, "ondblclick", EventCallback.Factory.Create<MouseEventArgs>(this, OnTriggerDoubleClick));
            builder.AddEventPreventDefaultAttribute(6, "ondblclick", PreventDefault);
            builder.AddEventStopPropagationAttribute(7, "ondblclick", StopPropagation);
        }

        if (IsTriggerContextMenu())
        {
            builder.AddAttribute(8, "oncontextmenu", EventCallback.Factory.Create<MouseEventArgs>(this, OnTriggerContextMenu));
            builder.AddEventPreventDefaultAttribute(9, "oncontextmenu", true);
        }

        builder.AddContent(10, ChildContent);

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
