// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Author: Argo Zhang (argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// Split 组件
/// </summary>
public sealed partial class Split
{
    /// <summary>
    /// 获取 是否开启折叠功能 默认 false
    /// </summary>
    [Parameter]
    public bool IsCollapsible { get; set; }

    /// <summary>
    /// 获取 是否显示拖动条 默认 true
    /// </summary>
    [Parameter]
    public bool ShowBarHandle { get; set; } = true;

    /// <summary>
    /// 获得/设置 开启 <see cref="IsCollapsible"/> 后，恢复时是否保持原始大小 默认 true
    /// </summary>
    [Parameter]
    public bool IsKeepOriginalSize { get; set; } = true;

    /// <summary>
    /// 获得/设置 是否垂直分割
    /// </summary>
    [Parameter]
    public bool IsVertical { get; set; }

    /// <summary>
    /// 获得/设置 第一个窗格初始化位置占比 默认为 50%
    /// </summary>
    [Parameter]
    public string Basis { get; set; } = "50%";

    /// <summary>
    /// 获得/设置 第一个窗格模板
    /// </summary>
    [Parameter]
    public RenderFragment? FirstPaneTemplate { get; set; }

    /// <summary>
    /// 获得/设置 第一个窗格模板 支持任意单位如 10px 20% 5em 1rem 未提供单位时默认为 px
    /// </summary>
    [Parameter]
    public string? FirstPaneMinimumSize { get; set; }

    /// <summary>
    /// 获得/设置 第二个窗格模板 支持任意单位如 10px 20% 5em 1rem 未提供单位时默认为 px
    /// </summary>
    [Parameter]
    public RenderFragment? SecondPaneTemplate { get; set; }

    /// <summary>
    /// 获得/设置 第一个窗格模板
    /// </summary>
    [Parameter]
    public string? SecondPaneMinimumSize { get; set; }

    /// <summary>
    /// 获得/设置 窗格折叠时回调方法 参数 bool 值为 true 是表示已折叠 值为 false 表示第二个已折叠
    /// </summary>
    [Parameter]
    [Obsolete("已弃用，请使用 OnResizedAsync 回调方法 Deprecated. Please use OnResizedAsync")]
    [ExcludeFromCodeCoverage]
    public Func<bool, Task>? OnCollapsedAsync { get; set; }

    /// <summary>
    /// 获得/设置 窗格尺寸改变时回调方法 可参阅 <see cref="SplitterResizedEventArgs"/>
    /// </summary>
    [Parameter]
    public Func<SplitterResizedEventArgs, Task>? OnResizedAsync { get; set; }

    /// <summary>
    /// 获得 组件样式
    /// </summary>
    private string? ClassString => CssBuilder.Default("split")
        .AddClass("is-vertical", IsVertical)
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    /// <summary>
    /// 获得 第一个窗格 Style
    /// </summary>
    private string? StyleString => CssBuilder.Default()
        .AddClass($"flex-basis: {Basis.ConvertToPercentString()};")
        .Build();

    private bool _lastCollapsible;

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
            _lastCollapsible = IsCollapsible;
        }
        else if (_lastCollapsible != IsCollapsible)
        {
            _lastCollapsible = IsCollapsible;
            await InvokeVoidAsync("update", Id);
        }
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <returns></returns>
    protected override Task InvokeInitAsync() => InvokeVoidAsync("init", Id, Interop, nameof(TriggerOnResize), new
    {
        IsKeepOriginalSize
    });

    /// <summary>
    /// 窗格折叠时回调方法 由 JavaScript 调用   
    /// </summary>
    /// <returns></returns>
    [JSInvokable]
    public async Task TriggerOnResize(string left)
    {
        if (OnResizedAsync != null)
        {
            await OnResizedAsync(new SplitterResizedEventArgs(left));
        }
    }
}
