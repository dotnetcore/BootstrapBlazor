// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;

namespace BootstrapBlazor.Components;

/// <summary>
///
/// </summary>
public partial class CherryMarkdown : IAsyncDisposable
{
    private CherryMarkdownOption Option { get; } = new();

    /// <summary>
    /// 获得/设置 编辑器设置
    /// </summary>
    [Parameter]
    public EditorSettings? EditorSettings { get; set; }

    /// <summary>
    /// 获得/设置 工具栏设置
    /// </summary>
    [Parameter]
    public ToolbarSettings? ToolbarSettings { get; set; }

    private string? _lastValue;
    /// <summary>
    /// 获得/设置 组件值
    /// </summary>
    [Parameter]
    public string? Value { get; set; }

    /// <summary>
    /// 获得/设置 组件值回调
    /// </summary>
    [Parameter]
    public EventCallback<string?> ValueChanged { get; set; }

    /// <summary>
    /// 获得/设置 组件 Html 代码
    /// </summary>
    [Parameter]
    public string? Html { get; set; }

    /// <summary>
    /// 获得/设置 组件 Html 代码回调
    /// </summary>
    [Parameter]
    public EventCallback<string?> HtmlChanged { get; set; }

    /// <summary>
    /// 获得/设置 Markdown组件内上传文件时回调此方法
    /// </summary>
    [Parameter]
    public Func<CherryMarkdownUploadFile, Task<string>>? OnFileUpload { get; set; }

    /// <summary>
    /// 获取/设置 组件是否为浏览器模式
    /// </summary>
    [Parameter]
    public bool? IsViewer { get; set; }

    [NotNull]
    private IJSObjectReference? Module { get; set; }

    [NotNull]
    private DotNetObjectReference<CherryMarkdown>? Interop { get; set; }

    /// <summary>
    /// 获得/设置 DOM 元素实例
    /// </summary>
    private ElementReference Element { get; set; }

    /// <summary>
    /// OnInitialized 方法
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        _lastValue = Value;
        Option.Value = Value;
        Option.Editor = EditorSettings ?? new EditorSettings();
        Option.Toolbars = ToolbarSettings ?? new ToolbarSettings();
        if (IsViewer == true)
        {
            Option.Editor.DefaultModel = "previewOnly";
            Option.Toolbars.Toolbar = false;
        }
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
            Module = await JSRuntime.InvokeAsync<IJSObjectReference>("import", "./_content/BootstrapBlazor.Chart/Components/Chart/Chart.razor.js");
            Interop = DotNetObjectReference.Create(this);
            await Module.InvokeVoidAsync("init", Element, Interop, Option, nameof(Upload));
        }
        else
        {
            if (Value != _lastValue)
            {
                _lastValue = Value;
                await Module.InvokeVoidAsync("update", Element, Value);
            }
        }
    }

    /// <summary>
    /// 文件上传回调
    /// </summary>
    /// <param name="uploadFile"></param>
    [JSInvokable]
    public async Task<string> Upload(CherryMarkdownUploadFile uploadFile)
    {
#if NET6_0_OR_GREATER
        var ret = "";
        if (Module != null)
        {
            var stream = await Module.InvokeAsync<IJSStreamReference>("fetch", Element);
            using var data = await stream.OpenReadStreamAsync();
            uploadFile.UploadStream = data;
            if (OnFileUpload != null)
            {
                ret = await OnFileUpload(uploadFile);
            }
        }
        return ret;
#else
        await Task.Yield();
        throw new NotSupportedException();
#endif
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
            var hasChanged = !EqualityComparer<string>.Default.Equals(vals[0], Value);
            if (hasChanged)
            {
                Value = vals[0];
                _lastValue = Value;

                if (ValueChanged.HasDelegate)
                {
                    await ValueChanged.InvokeAsync(Value);
                }
            }

            hasChanged = !EqualityComparer<string>.Default.Equals(vals[1], Html);
            if (hasChanged)
            {
                Html = vals[1];
                if (HtmlChanged.HasDelegate)
                {
                    await HtmlChanged.InvokeAsync(Html);
                }
            }
        }
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
