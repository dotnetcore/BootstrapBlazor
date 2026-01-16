// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components.Web;

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">ContextMenuTrigger 组件</para>
/// <para lang="en">ContextMenuTrigger component</para>
/// </summary>
public class ContextMenuTrigger : BootstrapComponentBase
{
    /// <summary>
    /// <para lang="zh">获得/设置 子组件</para>
    /// <para lang="en">Gets or sets the child content.</para>
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 包裹组件 TagName 默认为 div</para>
    /// <para lang="en">Gets or sets the wrapper component tag name. Default is div.</para>
    /// </summary>
    [Parameter]
    public string WrapperTag { get; set; } = "div";

    /// <summary>
    /// <para lang="zh">获得/设置 上下文数据</para>
    /// <para lang="en">Gets or sets the context data.</para>
    /// </summary>
    [Parameter]
    public object? ContextItem { get; set; }

    [CascadingParameter]
    [NotNull]
    private ContextMenuZone? ContextMenuZone { get; set; }

    /// <summary>
    /// <para lang="zh">触摸事件触发菜单的超时时间（毫秒）。默认值为 <see cref="ContextMenuOptions.OnTouchDelay"/> 毫秒。必须大于 0。</para>
    /// <para lang="en">The timeout duration for touch events to trigger the context menu (in milliseconds). Default is <see cref="ContextMenuOptions.OnTouchDelay"/> milliseconds. Must be greater than 0.</para>
    /// </summary>
    [Parameter]
    public int? OnTouchDelay { get; set; }

    [Inject, NotNull]
    private IOptionsMonitor<BootstrapBlazorOptions>? Options { get; set; }

    private string? ClassString => CssBuilder.Default()
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        OnTouchDelay ??= Options.CurrentValue.ContextMenuOptions.OnTouchDelay;
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="builder"><para lang="zh">渲染树生成器</para><para lang="en">The render tree builder</para></param>
    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        builder.OpenElement(0, WrapperTag);
        builder.AddMultipleAttributes(10, AdditionalAttributes);
        builder.AddAttribute(20, "class", ClassString);
        builder.AddAttribute(30, "oncontextmenu", EventCallback.Factory.Create<MouseEventArgs>(this, OnContextMenu));
        builder.AddAttribute(35, "ontouchstart", EventCallback.Factory.Create<TouchEventArgs>(this, OnTouchStart));
        builder.AddAttribute(36, "ontouchend", EventCallback.Factory.Create<TouchEventArgs>(this, OnTouchEnd));
        builder.AddEventPreventDefaultAttribute(40, "oncontextmenu", true);
        builder.AddContent(50, ChildContent);
        builder.CloseElement();
    }

    /// <summary>
    /// <para lang="zh">点击 ContextMenu 菜单项时触发</para>
    /// <para lang="en">Triggered when a ContextMenu menu item is clicked.</para>
    /// </summary>
    [DynamicDependency(DynamicallyAccessedMemberTypes.PublicMethods, typeof(MouseEventArgs))]
    public Task OnContextMenu(MouseEventArgs args) => ContextMenuZone.OnContextMenu(args, ContextItem);

    /// <summary>
    /// <para lang="zh">是否触摸</para>
    /// <para lang="en">Indicates whether touch has started.</para>
    /// </summary>
    public bool IsTouchStarted { get; private set; }

    /// <summary>
    /// <para lang="zh">触摸定时器工作指示</para>
    /// <para lang="en">Indicates whether the touch timer is working.</para>
    /// </summary>
    private bool IsBusy { get; set; }

    private async Task OnTouchStart(TouchEventArgs e)
    {
        if (!IsBusy)
        {
            IsBusy = true;
            IsTouchStarted = true;

            // 延时保持 TouchStart 状态
            // Delay to maintain TouchStart state
            if (OnTouchDelay.HasValue)
            {
                await Task.Delay(OnTouchDelay.Value);
            }
            if (IsTouchStarted)
            {
                var args = new MouseEventArgs()
                {
                    ClientX = e.Touches[0].ClientX,
                    ClientY = e.Touches[0].ClientY,
                    ScreenX = e.Touches[0].ScreenX,
                    ScreenY = e.Touches[0].ScreenY,
                };

                await OnContextMenu(args);

                // 延时防止重复激活菜单功能
                // Delay to prevent repeated activation of menu functions
                if (OnTouchDelay.HasValue)
                {
                    await Task.Delay(OnTouchDelay.Value);
                }
            }
            IsBusy = false;
        }
    }

    private void OnTouchEnd()
    {
        IsTouchStarted = false;
    }
}
