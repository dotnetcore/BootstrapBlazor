// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">Split 组件</para>
/// <para lang="en">Split Component</para>
/// </summary>
public sealed partial class Split
{
    /// <summary>
    /// <para lang="zh">获取 是否开启折叠功能 默认 false</para>
    /// <para lang="en">Get/Set Whether to enable collapsible function. Default false</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public bool IsCollapsible { get; set; }

    /// <summary>
    /// <para lang="zh">获取 是否显示拖动条 默认 true</para>
    /// <para lang="en">Get/Set Whether to show drag bar. Default true</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public bool ShowBarHandle { get; set; } = true;

    /// <summary>
    /// <para lang="zh">获得/设置 开启 <see cref="IsCollapsible"/> 后，恢复时是否保持原始大小 默认 true</para>
    /// <para lang="en">Get/Set Whether to keep original size when restoring after enabling <see cref="IsCollapsible"/>. Default true</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public bool IsKeepOriginalSize { get; set; } = true;

    /// <summary>
    /// <para lang="zh">获得/设置 是否垂直分割</para>
    /// <para lang="en">Get/Set Whether vertical split</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public bool IsVertical { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 第一个窗格初始化位置占比 默认为 50%</para>
    /// <para lang="en">Get/Set First panel initial position ratio. Default 50%</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public string Basis { get; set; } = "50%";

    /// <summary>
    /// <para lang="zh">获得/设置 第一个窗格模板</para>
    /// <para lang="en">Get/Set First Panel Template</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public RenderFragment? FirstPaneTemplate { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 第一个窗格最小宽度 支持任意单位如 10px 20% 5em 1rem 未提供单位时默认为 px</para>
    /// <para lang="en">Get/Set First Panel Minimum Size. Supports any unit e.g. 10px 20% 5em 1rem. Default unit is px</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public string? FirstPaneMinimumSize { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 第二个窗格模板</para>
    /// <para lang="en">Get/Set Second Panel Template</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public RenderFragment? SecondPaneTemplate { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 第二个窗格最小宽度</para>
    /// <para lang="en">Get/Set Second Panel Minimum Size</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public string? SecondPaneMinimumSize { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 窗格折叠时回调方法 参数 bool 值为 true 是表示已折叠 值为 false 表示第二个已折叠</para>
    /// <para lang="en">Get/Set Callback method when panel is collapsed. parameter bool value true means collapsed, false means second panel collapsed</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    [Obsolete("已弃用，请使用 OnResizedAsync 回调方法 Deprecated. Please use OnResizedAsync")]
    [ExcludeFromCodeCoverage]
    public Func<bool, Task>? OnCollapsedAsync { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 窗格尺寸改变时回调方法 可参阅 <see cref="SplitterResizedEventArgs"/></para>
    /// <para lang="en">Get/Set Callback method when panel size changes. Refer to <see cref="SplitterResizedEventArgs"/></para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public Func<SplitterResizedEventArgs, Task>? OnResizedAsync { get; set; }

    /// <summary>
    /// <para lang="zh">获得 组件样式</para>
    /// <para lang="en">Get Component Class Name</para>
    /// </summary>
    private string? ClassString => CssBuilder.Default("split")
        .AddClass("is-vertical", IsVertical)
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    /// <summary>
    /// <para lang="zh">获得 第一个窗格 Style</para>
    /// <para lang="en">Get First Panel Style</para>
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
    /// <para lang="zh">设置左侧窗格宽度</para>
    /// <para lang="en">Set Left Panel Width</para>
    /// </summary>
    /// <param name="leftWidth">可以是百分比或者其他单位</param>
    /// <returns></returns>
    public Task SetLeftWidth(string leftWidth) => InvokeVoidAsync("setLeft", Id, leftWidth);

    /// <summary>
    /// <para lang="zh">窗格折叠时回调方法 由 JavaScript 调用</para>
    /// <para lang="en">Callback method when panel collapsed. Called by JavaScript</para>   
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
