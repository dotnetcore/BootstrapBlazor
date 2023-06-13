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
    /// 获得/设置 标签关闭时回调方法
    /// </summary>
    [Parameter]
    public Func<string, bool, Task>? OnVisibleStateChangedAsync { get; set; }

    /// <summary>
    /// 获得/设置 组件初始化完成回调方法
    /// </summary>
    [Parameter]
    public Func<Task>? OnInitializedCallbackAsync { get; set; }

    /// <summary>
    /// 获得/设置 组件初始化完成回调方法
    /// </summary>
    [Parameter]
    public Func<Task>? OnSaveLayoutCallbackAsync { get; set; }

    /// <summary>
    /// 获得/设置 标签页拖动完成回调方法
    /// </summary>
    [Parameter]
    public Func<Task>? OnTabDropCallbackAsync { get; set; }

    /// <summary>
    /// 获得/设置 标签页调整大小完成回调方法
    /// </summary>
    [Parameter]
    public Func<Task>? OnSplitterStopCallbackAsync { get; set; }

    /// <summary>
    /// 获得/设置 是否启用本地存储布局 默认 true 启用
    /// </summary>
    [Parameter]
    public bool EnableLocalStorage { get; set; } = true;

    private DockViewConfig Config { get; } = new();

    private DockContent Content { get; } = new();

    private bool IsRendered { get; set; }

    private string? ClassString => CssBuilder.Default("bb-dock")
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    private bool IsInit { get; set; }

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
            Config.Contents.Add(Content);
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

        if (IsRendered && Module != null)
        {
            if (IsInit)
            {
                await InvokeVoidAsync("update", Id, GetOption());
            }
            else
            {
                IsInit = true;
                await InvokeVoidAsync("init", Id, GetOption(), Interop);
            }
        }

        DockViewConfig GetOption() => new()
        {
            Version = "v1",
            Name = Name,
            EnableLocalStorage = EnableLocalStorage,
            Contents = Config.Contents,
            VisibleChangedCallback = nameof(VisibleChangedCallbackAsync),
            InitializedCallback = nameof(InitializedCallbackAsync),
            SaveLayoutCallback = nameof(SaveLayoutCallbackAsync),
            TabDropCallback = nameof(TabDropCallbackAsync),
            SplitterStopCallback = nameof(SplitterStopCallbackAsync)
        };
    }

    private RenderFragment RenderDockContent(List<DockContent> contents) => new(builder =>
    {
        foreach (var content in contents)
        {
            builder.AddContent(0, RenderDockComponent(content.Items));
        }
    });

    private RenderFragment RenderDockComponent(List<IDockComponent> items) => new(builder =>
    {
        foreach (var item in items)
        {
            if (item is DockComponent com)
            {
                builder.OpenElement(0, "div");
                builder.AddAttribute(1, "id", com.Id);
                builder.AddAttribute(2, "class", "bb-dock-item d-none");
                builder.AddAttribute(3, "data-bb-title", com.Title);
                builder.AddContent(4, com.ChildContent);
                builder.CloseComponent();
            }
            else if (item is DockContent content)
            {
                builder.AddContent(5, RenderDockComponent(content.Items));
            }
        }
    });

    /// <summary>
    /// 标签页关闭回调方法 由 JavaScript 调用
    /// </summary>
    [JSInvokable]
    public async Task VisibleChangedCallbackAsync(string title, bool visible)
    {
        if (OnVisibleStateChangedAsync != null)
        {
            await OnVisibleStateChangedAsync(title, visible);
        }
    }

    /// <summary>
    /// 标签页关闭回调方法 由 JavaScript 调用
    /// </summary>
    [JSInvokable]
    public async Task InitializedCallbackAsync()
    {
        if (OnInitializedCallbackAsync != null)
        {
            await OnInitializedCallbackAsync();
        }
    }

    /// <summary>
    /// 标签页关闭回调方法 由 JavaScript 调用
    /// </summary>
    [JSInvokable]
    public async Task SaveLayoutCallbackAsync()
    {
        if (OnSaveLayoutCallbackAsync != null)
        {
            await OnSaveLayoutCallbackAsync();
        }
    }

    /// <summary>
    /// 标签页关闭回调方法 由 JavaScript 调用
    /// </summary>
    [JSInvokable]
    public async Task TabDropCallbackAsync()
    {
        if (OnTabDropCallbackAsync != null)
        {
            await OnTabDropCallbackAsync();
        }
    }

    /// <summary>
    /// 标签页关闭回调方法 由 JavaScript 调用
    /// </summary>
    [JSInvokable]
    public async Task SplitterStopCallbackAsync()
    {
        if (OnSplitterStopCallbackAsync != null)
        {
            await OnSplitterStopCallbackAsync();
        }
    }
}
