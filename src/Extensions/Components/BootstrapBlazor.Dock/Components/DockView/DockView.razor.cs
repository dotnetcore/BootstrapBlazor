// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;

namespace BootstrapBlazor.Components;

/// <summary>
/// DockView 组件
/// </summary>
public partial class DockView
{
    /// <summary>
    /// 获得/设置 RenderFragment 实例
    /// </summary>
    [Parameter]
#if NET6_0_OR_GREATER
    [EditorRequired]
#endif
    [NotNull]
    public RenderFragment? ChildContent { get; set; }

    private DockContent Option { get; } = new();

    private bool IsRendered { get; set; }

    private string? ClassString => CssBuilder.Default("bb-dock")
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="firstRender"></param>
    protected override void OnAfterRender(bool firstRender)
    {
        base.OnAfterRender(firstRender);

        if (firstRender)
        {
            IsRendered = true;
            StateHasChanged();
        }
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="firstRender"></param>
    /// <returns></returns>
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (IsRendered)
        {
            await InvokeVoidAsync("init", Id, Option, Interop, nameof(Demo));
        }
    }

    private RenderFragment RenderDockComponent(DockContent content) => new(builder =>
    {
        foreach (var item in content.Items)
        {
            if (item is DockContentItem com)
            {
                builder.OpenElement(0, "div");
                builder.AddAttribute(1, "id", com.Id);
                builder.AddAttribute(2, "class", CssBuilder.Default("bb-dock-item").AddClass(com.Class).Build());
                builder.AddContent(3, com.ChildContent);
                builder.CloseComponent();
            }
            else if (item is DockContent content)
            {
                builder.AddContent(4, RenderDockComponent(content));
            }
        }
    });

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    [JSInvokable]
    public Task Demo()
    {
        return Task.CompletedTask;
    }
}
