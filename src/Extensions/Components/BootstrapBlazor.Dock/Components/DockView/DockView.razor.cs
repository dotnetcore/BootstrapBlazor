// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Configuration;

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
    /// 获得/设置 锁定状态回调此方法
    /// </summary>
    [Parameter]
    public Func<bool, Task>? OnLockChangedCallbackAsync { get; set; }

    /// <summary>
    /// 获得/设置 标签页位置变化时回调此方法
    /// </summary>
    /// <remarks>拖动标签 <see cref="OnTabDropCallbackAsync"/> 或者调整标签  <see cref="OnSplitterCallbackAsync"/> 时均触发此方法</remarks>
    [Parameter]
    public Func<Task>? OnResizeCallbackAsync { get; set; }

    /// <summary>
    /// 获得/设置 是否锁定 默认 false
    /// </summary>
    /// <remarks>锁定后无法拖动</remarks>
    [Parameter]
    public bool IsLock { get; set; }

    /// <summary>
    /// 获得/设置 布局配置
    /// </summary>
    [Parameter]
    public string? LayoutConfig { get; set; }

    /// <summary>
    /// 获得/设置 版本设置 默认 null 未设置 用于本地配置 可通过全局统一配置
    /// </summary>
    [Parameter]
    public string? Version { get; set; }

    /// <summary>
    /// 获得/设置 是否启用本地存储布局 默认 null 未设置
    /// </summary>
    [Parameter]
    public bool? EnableLocalStorage { get; set; }

    /// <summary>
    /// 获得/设置 本地存储前缀 默认 bb-dock
    /// </summary>
    [Parameter]
    public string? LocalStoragePrefix { get; set; }

    [Inject]
    [NotNull]
    private IConfiguration? Configuration { get; set; }

    private DockViewConfig Config { get; } = new();

    private DockContent Content { get; } = new();

    private bool _rendered;

    private bool _isLock;

    private bool _init;

    [NotNull]
    private DockViewOptions? _options = default!;

    private string? ClassString => CssBuilder.Default("bb-dock")
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        var section = Configuration.GetSection(nameof(DockViewOptions));
        _options = section.Exists() ? section.Get<DockViewOptions>() : new();
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="firstRender"></param>
    protected override void OnAfterRender(bool firstRender)
    {
        if (firstRender)
        {
            Config.Contents.Add(Content);
        }
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="firstRender"></param>
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (firstRender)
        {
            _rendered = true;
            StateHasChanged();
            return;
        }

        if (_rendered)
        {
            if (_init)
            {
                await InvokeVoidAsync("update", Id, GetOptions());
            }
            else
            {
                _init = true;
                await InvokeVoidAsync("init", Id, GetOptions(), Interop);
            }
        }
    }

    private DockViewConfig GetOptions() => new()
    {
        Version = Version ?? _options.Version ?? "v1",
        Name = Name,
        EnableLocalStorage = EnableLocalStorage ?? _options.EnableLocalStorage ?? false,
        IsLock = IsLock,
        Contents = Config.Contents,
        LayoutConfig = LayoutConfig,
        LocalStorageKeyPrefix = $"{LocalStoragePrefix ?? _options.LocalStoragePrefix ?? "bb-dock"}-{Name}",
        VisibleChangedCallback = nameof(VisibleChangedCallbackAsync),
        InitializedCallback = nameof(InitializedCallbackAsync),
        TabDropCallback = nameof(TabDropCallbackAsync),
        SplitterCallback = nameof(SplitterCallbackAsync),
        LockChangedCallback = nameof(LockChangedCallbackAsync)
    };

    private RenderFragment RenderDockComponent(List<IDockComponent> items) => builder =>
    {
        foreach (var item in items)
        {
            switch (item)
            {
                case DockComponent com:
                    builder.AddContent(0, RenderComponent(com));
                    break;
                case DockContent content:
                    builder.AddContent(1, RenderDockComponent(content.Items));
                    break;
            }
        }
    };

    /// <summary>
    /// 锁定/解锁当前布局
    /// </summary>
    /// <param name="lock">true 时锁定 false 时解锁</param>
    /// <returns></returns>
    public async Task Lock(bool @lock)
    {
        IsLock = @lock;
        if (_isLock != IsLock)
        {
            _isLock = IsLock;
            await InvokeVoidAsync("lock", Id, _isLock);
        }
    }

    /// <summary>
    /// 获取布局配置
    /// </summary>
    /// <returns></returns>
    public Task<string?> GetLayoutConfig() => InvokeAsync<string>("getLayoutConfig", Id);

    /// <summary>
    /// 重置为默认布局
    /// </summary>
    /// <returns></returns>
    public Task Reset(string? layoutConfig = null)
    {
        var config = GetOptions();
        if (layoutConfig != null)
        {
            config.LayoutConfig = layoutConfig;
        }
        return InvokeVoidAsync("reset", Id, config);
    }

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

    /// <summary>
    /// 锁定回调方法 由 JavaScript 调用
    /// </summary>
    [JSInvokable]
    public async Task LockChangedCallbackAsync(bool state)
    {
        if (OnLockChangedCallbackAsync != null)
        {
            await OnLockChangedCallbackAsync(state);
        }
    }
}
