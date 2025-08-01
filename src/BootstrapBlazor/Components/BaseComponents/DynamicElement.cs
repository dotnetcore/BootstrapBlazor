// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components.Web;

namespace BootstrapBlazor.Components;

/// <summary>
/// 动态元素组件
/// </summary>
public class DynamicElement : BootstrapComponentBase
{
    /// <summary>
    /// 获得/设置 TagName 属性 默认为 div
    /// </summary>
    [Parameter]
    [NotNull]
    public string? TagName { get; set; } = "div";

    /// <summary>
    /// 获得/设置 是否触发 Click 事件 默认 true
    /// </summary>
    [Parameter]
    public bool TriggerClick { get; set; } = true;

    /// <summary>
    /// 获得/设置 是否阻止默认行为 默认 false
    /// </summary>
    [Parameter]
    public bool PreventDefault { get; set; }

    /// <summary>
    /// 获得/设置 是否事件冒泡 默认为 false
    /// </summary>
    [Parameter]
    public bool StopPropagation { get; set; }

    /// <summary>
    /// 获得/设置 Click 回调委托
    /// </summary>
    [Parameter]
    public Func<Task>? OnClick { get; set; }

    /// <summary>
    /// 获得/设置 是否触发 DoubleClick 事件 默认 true
    /// </summary>
    [Parameter]
    public bool TriggerDoubleClick { get; set; } = true;

    /// <summary>
    /// 获得/设置 DoubleClick 回调委托
    /// </summary>
    [Parameter]
    public Func<Task>? OnDoubleClick { get; set; }

    /// <summary>
    /// 获得/设置 OnContextMenu 回调委托
    /// </summary>
    [Parameter]
    public Func<MouseEventArgs, Task>? OnContextMenu { get; set; }

    /// <summary>
    /// 获得/设置 是否触发 OnContextMenu 事件 默认 false
    /// </summary>
    [Parameter]
    public bool TriggerContextMenu { get; set; }

    /// <summary>
    /// 获得/设置 内容组件
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// 获得/设置 是否生成指定 Tag 元素 默认 true 生成
    /// </summary>
    [Parameter]
    public bool GenerateElement { get; set; } = true;

    /// <summary>
    /// 获得/设置 组件 Key 值
    /// </summary>
    [Parameter]
    public object? Key { get; set; }

    /// <summary>
    /// BuildRenderTree 方法
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
            builder.AddAttribute(2, "onclick", EventCallback.Factory.Create<MouseEventArgs>(this, e => OnClick!()));
        }

        if (IsTriggerDoubleClick())
        {
            builder.AddAttribute(3, "ondblclick", EventCallback.Factory.Create<MouseEventArgs>(this, e => OnDoubleClick!()));
        }

        if (IsTriggerClick() || IsTriggerDoubleClick())
        {
            builder.AddEventPreventDefaultAttribute(4, "onclick", PreventDefault);
            builder.AddEventStopPropagationAttribute(5, "onclick", StopPropagation);
        }

        if (TriggerContextMenu && OnContextMenu != null)
        {
            builder.AddAttribute(6, "oncontextmenu", EventCallback.Factory.Create<MouseEventArgs>(this, e => OnContextMenu(e)));
            builder.AddEventPreventDefaultAttribute(7, "oncontextmenu", true);
        }

        builder.AddContent(8, ChildContent);

        if (Key != null)
        {
            builder.SetKey(Key);
        }

        if (GenerateElement || IsTriggerClick() || IsTriggerDoubleClick())
        {
            builder.CloseElement();
        }

        bool IsTriggerClick() => TriggerClick && OnClick != null;
        bool IsTriggerDoubleClick() => TriggerDoubleClick && OnDoubleClick != null;
    }
}
