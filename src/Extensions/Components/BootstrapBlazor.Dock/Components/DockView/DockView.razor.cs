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
    /// 获得/设置 标签切换 Visible 状态时回调此方法
    /// </summary>
    /// <remarks>可用于第三方组件显示标签页状态更新</remarks>
    [Parameter]
    public Func<string, bool, Task>? OnVisibleStateChangedAsync { get; set; }

    /// <summary>
    /// 获得/设置 客户端组件脚本初始化完成后回调此方法
    /// </summary>
    [Parameter]
    public Func<Task>? OnInitializedCallbackAsync { get; set; }

    /// <summary>
    /// 获得/设置 标签页拖动完成时回调此方法
    /// </summary>
    [Parameter]
    public Func<Task>? OnTabDropCallbackAsync { get; set; }

    /// <summary>
    /// 获得/设置 标签页调整大小完成时回调此方法
    /// </summary>
    [Parameter]
    public Func<Task>? OnSplitterCallbackAsync { get; set; }

    /// <summary>
    /// 获得/设置 标签页位置变化时回调此方法
    /// </summary>
    /// <remarks>拖动标签 <see cref="OnTabDropCallbackAsync"/> 或者调整标签  <see cref="OnSplitterCallbackAsync"/> 时均触发此方法</remarks>
    [Parameter]
    public Func<Task>? OnResizeCallbackAsync { get; set; }

    /// <summary>
    /// 获得/设置 是否锁定当前布局
    /// </summary>
    [Parameter]
    public bool Lock { get; set; }

    /// <summary>
    /// 获得/设置 是否启用本地存储布局 默认 true 启用
    /// </summary>
    [Parameter]
    public bool EnableLocalStorage { get; set; } = true;

    private DockViewConfig Config { get; } = new();

    private DockContent Content { get; } = new();

    private bool IsRendered { get; set; }

    private bool _isLock = false;

    private string? ClassString => CssBuilder.Default("bb-dock")
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    private bool IsInit { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="firstRender"></param>
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            IsRendered = true;
            Config.Contents.Add(Content);
            await base.OnAfterRenderAsync(firstRender);
            StateHasChanged();
            return;
        }

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
            TabDropCallback = nameof(TabDropCallbackAsync),
            SplitterCallback = nameof(SplitterCallbackAsync)
        };

        if (_isLock != Lock)
        {
            _isLock = Lock;
            await InvokeVoidAsync("lock", Id);
        }
    }

    private static RenderFragment RenderDockContent(List<DockContent> contents) => builder =>
    {
        foreach (var content in contents)
        {
            builder.AddContent(0, RenderDockComponent(content.Items));
        }
    };

    private static RenderFragment RenderDockComponent(List<IDockComponent> items) => builder =>
    {
        foreach (var item in items)
        {
            switch (item)
            {
                case DockComponent com:
                    builder.OpenElement(0, "div");
                    builder.AddAttribute(1, "id", com.Id);
                    builder.AddAttribute(2, "class", "bb-dock-item d-none");
                    builder.AddAttribute(3, "data-bb-key", com.Key);
                    builder.AddAttribute(4, "data-bb-title", com.Title);
                    builder.AddContent(5, com.ChildContent);
                    builder.CloseComponent();
                    break;
                case DockContent content:
                    builder.AddContent(6, RenderDockComponent(content.Items));
                    break;
            }
        }
    };

    /// <summary>
    /// 锁定当前布局
    /// </summary>
    /// <returns></returns>
    //public Task Lock() => InvokeVoidAsync("lock", Id);

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
    public async Task SplitterCallbackAsync()
    {
        if (OnSplitterCallbackAsync != null)
        {
            await OnSplitterCallbackAsync();
        }
    }
}
