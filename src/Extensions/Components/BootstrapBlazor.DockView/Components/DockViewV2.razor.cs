// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;

namespace BootstrapBlazor.Components;

/// <summary>
/// DockViewV2 组件
/// </summary>
public partial class DockViewV2
{
    /// <summary>
    /// 获得/设置 DockView 名称 默认 null 用于本地存储识别
    /// </summary>
    [Parameter]
#if NET6_0_OR_GREATER
    [EditorRequired]
#endif
    [NotNull]
    public string? Name { get; set; }

    /// <summary>
    /// 获得/设置 布局配置
    /// </summary>
    [Parameter]
    public string? LayoutConfig { get; set; }

    /// <summary>
    /// 获得/设置 是否显示关闭按钮 默认为 true
    /// </summary>
    [Parameter]
    public bool ShowClose { get; set; } = true;

    /// <summary>
    /// 获得/设置 子组件
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    private string? ClassString => CssBuilder.Default("bb-dock-view")
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    private readonly List<DockViewV2Panel> _panels = [];

    private string? _templateId;

    private bool _isInit;

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        _templateId ??= $"{Id}_template";
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="firstRender"></param>
    /// <returns></returns>
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (firstRender)
        {
            _isInit = true;
            StateHasChanged();
        }
        else if (_isInit)
        {
            await InvokeVoidAsync("init", Id, new { Invoke = Interop, ShowClose, LayoutConfig, TemplateId = _templateId });
        }
    }

    /// <summary>
    /// 添加 Group 方法
    /// </summary>
    /// <returns></returns>
    public ValueTask AddGroup()
    {
        return ValueTask.CompletedTask;
    }

    /// <summary>
    /// 添加 Panel 方法
    /// </summary>
    /// <returns></returns>
    public ValueTask AddPanel()
    {
        return ValueTask.CompletedTask;
    }
}
