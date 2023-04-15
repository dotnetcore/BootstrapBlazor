// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;
using System.Reflection.Metadata;

namespace BootstrapBlazor.Components;

/// <summary>
/// Markdown 组件
/// </summary>
public partial class Markdown : IAsyncDisposable
{
    /// <summary>
    /// 获得/设置 控件高度，默认300px
    /// </summary>
    [Parameter]
    public int Height { get; set; } = 300;

    /// <summary>
    /// 获得/设置 控件最小高度，默认200px
    /// </summary>
    [Parameter]
    public int MinHeight { get; set; } = 200;

    /// <summary>
    /// 获得/设置 初始化时显示的界面，markdown 界面，所见即所得界面
    /// </summary>
    [Parameter]
    public InitialEditType InitialEditType { get; set; }

    /// <summary>
    /// 获得/设置 预览模式，Tab 页预览，分栏预览 默认分栏预览 Vertical
    /// </summary>
    [Parameter]
    public PreviewStyle PreviewStyle { get; set; }

    /// <summary>
    /// 获得/设置 语言，默认为简体中文，如果改变，需要自行引入语言包
    /// </summary>
    [Parameter]
    public string? Language { get; set; }

    /// <summary>
    /// 获得/设置 提示信息
    /// </summary>
    [Parameter]
    public string? Placeholder { get; set; }

    /// <summary>
    /// 获得/设置 组件 Html 代码
    /// </summary>
    [Parameter]
    public string? Html { get; set; }

    /// <summary>
    /// 获得/设置 组件 Html 代码回调
    /// </summary>
    [Parameter]
    public EventCallback<string> HtmlChanged { get; set; }

    /// <summary>
    /// 获取/设置 组件是否为浏览器模式
    /// </summary>
    [Parameter]
    public bool? IsViewer { get; set; }

    /// <summary>
    /// 获取/设置 组件是否为为暗黑主题，默认为false
    /// </summary>
    [Parameter]
    public bool IsDark { get; set; } = false;

    /// <summary>
    /// 启用代码高亮插件，需引入对应的css js，默认为false
    /// </summary>
    [Parameter]
    public bool EnableHighlight { get; set; } = false;

    private MarkdownOption Option { get; } = new();

    [NotNull]
    private IJSObjectReference? Module { get; set; }

    [NotNull]
    private DotNetObjectReference<Markdown>? Interop { get; set; }

    /// <summary>
    /// 获得/设置 DOM 元素实例
    /// </summary>
    private ElementReference Element { get; set; }

    /// <summary>
    /// 获得 组件样式
    /// </summary>
    protected string? GetClassString() => CssBuilder.Default()
        .AddClass(CssClass).AddClass(ValidCss)
        .Build();

    /// <summary>
    /// OnInitialized 方法
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        Option.PreviewStyle = PreviewStyle.ToDescriptionString();
        Option.InitialEditType = InitialEditType.ToDescriptionString();
        Option.Language = Language;
        Option.Placeholder = Placeholder;
        Option.Height = $"{Height}px";
        Option.MinHeight = $"{MinHeight}px";
        Option.InitialValue = Value ?? "";
        Option.Viewer = IsViewer;
        Option.Theme = IsDark ? "dark" : "light";
        Option.EnableHighlight = EnableHighlight;
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="firstRender"></param>
    /// <returns></returns>
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            // import JavaScript
            Module = await JSRuntime.InvokeAsync<IJSObjectReference>("import", "./_content/BootstrapBlazor.Markdown/Components/Markdown/Markdown.razor.js");
            Interop = DotNetObjectReference.Create(this);
            await Module.InvokeVoidAsync("init", Element, Interop, Option, nameof(Update));
        }
    }

    /// <summary>
    /// 更新组件值方法
    /// </summary>
    /// <param name="vals"></param>
    /// <returns></returns>
    [JSInvokable]
    public async Task Update(string[] vals)
    {
        if (vals.Length == 2)
        {
            CurrentValueAsString = vals[0];

            var hasChanged = !EqualityComparer<string>.Default.Equals(vals[1], Html);
            if (hasChanged)
            {
                Html = vals[1];
                if (HtmlChanged.HasDelegate)
                {
                    await HtmlChanged.InvokeAsync(Html);
                }
            }

            if (ValidateForm != null)
            {
                StateHasChanged();
            }
        }
    }

    /// <summary>
    /// 设置 Value 方法
    /// </summary>
    /// <returns></returns>
    public new async ValueTask SetValue(string value)
    {
        CurrentValueAsString = value;
        await Module.InvokeVoidAsync("update", Element, Value);
    }

    /// <summary>
    /// 执行方法
    /// </summary>
    /// <param name="method"></param>
    /// <param name="parameters"></param>
    /// <returns></returns>
    public ValueTask DoMethodAsync(string method, params object[] parameters) => Module.InvokeVoidAsync("invoke", Element, method, parameters);

    #region Dispose
    /// <summary>
    /// Dispose 方法
    /// </summary>
    /// <param name="disposing"></param>
    protected virtual async ValueTask DisposeAsync(bool disposing)
    {
        if (disposing)
        {
            Interop?.Dispose();

            if (Module != null)
            {
                await Module.InvokeVoidAsync("dispose", Element);
                await Module.DisposeAsync();
            }
        }
    }

    /// <summary>
    /// Dispose 方法
    /// </summary>
    public async ValueTask DisposeAsync()
    {
        await DisposeAsync(true);
        GC.SuppressFinalize(this);
    }
    #endregion
}
