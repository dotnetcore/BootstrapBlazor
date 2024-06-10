// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Configuration;

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
    /// 获得/设置 是否锁定 默认 false
    /// </summary>
    /// <remarks>锁定后无法拖动</remarks>
    [Parameter]
    public bool IsLock { get; set; }

    /// <summary>
    /// 获得/设置 是否显示锁定按钮 默认 true
    /// </summary>
    [Parameter]
    public bool ShowLock { get; set; } = true;

    /// <summary>
    /// 获得/设置 锁定状态回调此方法
    /// </summary>
    [Parameter]
    public Func<string[], bool, Task>? OnLockChangedCallbackAsync { get; set; }

    /// <summary>
    /// 获得/设置 标签关闭时回调此方法
    /// </summary>
    /// <remarks>可用于第三方组件显示标签页状态更新</remarks>
    [Parameter]
    public Func<string, Task>? OnPanelClosedCallbackAsync { get; set; }

    /// <summary>
    /// 获得/设置 客户端组件脚本初始化完成后回调此方法
    /// </summary>
    [Parameter]
    public Func<Task>? OnInitializedCallbackAsync { get; set; }

    /// <summary>
    /// 获得/设置 子组件
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

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

    /// <summary>
    /// 获得/设置 DockView 组件主题 默认 Light
    /// </summary>
    [Parameter]
    public DockViewTheme Theme { get; set; } = DockViewTheme.Light;

    [Inject]
    [NotNull]
    private IConfiguration? Configuration { get; set; }

    private string? ClassString => CssBuilder.Default("bb-dockview")
        .AddClass(Theme.ToDescriptionString())
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    private readonly List<DockViewComponentBase> _root = [];

    private readonly List<DockViewComponent> _components = [];

    private string? _templateId;

    private bool _rendered;

    private bool _init;

    [NotNull]
    private DockViewOptions? _options = default!;

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        var section = Configuration.GetSection(nameof(DockViewOptions));
        _options = section.Exists() ? section.Get<DockViewOptions>() : new();
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
            _rendered = true;
            StateHasChanged();
            return;
        }

        if (Module != null)
        {
            if (_init)
            {
                await InvokeVoidAsync("update", Id, GetOptions());
            }
            else
            {
                _init = true;
                await InvokeVoidAsync("init", Id, Interop, GetOptions());
            }
        }
    }

    private DockViewConfig GetOptions() => new()
    {
        EnableLocalStorage = EnableLocalStorage ?? _options.EnableLocalStorage ?? false,
        LocalStorageKey = $"{GetPrefixKey()}-{Name}-{GetVersion()}",
        IsLock = IsLock,
        ShowLock = ShowLock,
        ShowClose = ShowClose,
        LayoutConfig = LayoutConfig,
        InitializedCallback = nameof(InitializedCallbackAsync),
        PanelClosedCallback = nameof(PanelClosedCallbackAsync),
        LockChangedCallback = nameof(LockChangedCallbackAsync),
        TemplateId = _templateId,
        Contents = _root
    };

    private string GetVersion() => Version ?? _options.Version ?? "v1";

    private string GetPrefixKey() => LocalStoragePrefix ?? _options.LocalStoragePrefix ?? "bb-dockview";

    private static async Task OnClickBar(Func<Task>? callback)
    {
        if (callback != null)
        {
            await callback();
        }
    }

    /// <summary>
    /// 重置为默认布局
    /// </summary>
    /// <returns></returns>
    public async Task Reset(string? layoutConfig = null)
    {
        var options = GetOptions();
        if (layoutConfig != null)
        {
            options.LayoutConfig = layoutConfig;
        }
        await InvokeVoidAsync("reset", Id, options);
    }

    /// <summary>
    /// 获得当前布局 Json 字符串
    /// </summary>
    /// <returns></returns>
    public Task<string?> SaveLayout() => InvokeAsync<string?>("save", Id);

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
    public async Task PanelClosedCallbackAsync(string title)
    {
        if (OnPanelClosedCallbackAsync != null)
        {
            await OnPanelClosedCallbackAsync(title);
        }
    }

    /// <summary>
    /// 锁定回调方法 由 JavaScript 调用
    /// </summary>
    [JSInvokable]
    public async Task LockChangedCallbackAsync(string[] panels, bool state)
    {
        if (OnLockChangedCallbackAsync != null)
        {
            await OnLockChangedCallbackAsync(panels, state);
        }
    }
}
