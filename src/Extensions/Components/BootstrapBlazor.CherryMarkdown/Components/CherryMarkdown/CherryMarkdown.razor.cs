// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;

namespace BootstrapBlazor.Components;

/// <summary>
///
/// </summary>
public partial class CherryMarkdown : IAsyncDisposable
{
    private readonly CherryMarkdownOption _option = new();

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

    private string? _value;
    /// <summary>
    /// 获得/设置 组件值
    /// </summary>
    [Parameter]
    public string? Value
    {
        get => _value;
        set
        {
            if (_value != value)
            {
                _value = value;
                IsRender = true;
            }
        }
    }

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

    /// <summary>
    /// 获得/设置 DOM 元素实例
    /// </summary>
    private ElementReference MarkdownElement { get; set; }

    private bool IsRender { get; set; }

    [NotNull]
    private JSModule<CherryMarkdown>? Module { get; set; }

    /// <summary>
    /// OnInitialized 方法
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();
        _option.Value = _value;
        _option.Editor = EditorSettings ?? new EditorSettings();
        _option.Toolbars = ToolbarSettings ?? new ToolbarSettings();
        if (IsViewer == true)
        {
            _option.Editor.DefaultModel = "previewOnly";
            _option.Toolbars.Toolbar = false;
        }
    }

    /// <summary>
    /// OnAfterRenderAsync 方法
    /// </summary>
    /// <param name="firstRender"></param>
    /// <returns></returns>
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);
        if (firstRender)
        {
            IsRender = false;
            Module = await JSRuntime.LoadModule<CherryMarkdown>("./_content/BootstrapBlazor.CherryMarkdown/js/bootstrap.blazor.cherrymarkdown.min.js", this, false);
            await Module.InvokeVoidAsync("bb_cherry_markdown", MarkdownElement, _option, "init");
        }

        if (IsRender)
        {
            await Module.InvokeVoidAsync("bb_cherry_markdown", MarkdownElement, Value ?? "", "setMarkdown");
        }
    }

    /// <summary>
    /// 文件上传回调
    /// </summary>
    /// <param name="id"></param>
    /// <param name="uploadFile"></param>
    [JSInvokable]
    public async Task<string> Upload(string id, CherryMarkdownUploadFile uploadFile)
    {
#if NET6_0_OR_GREATER
        var stream = await Module.InvokeAsync<IJSStreamReference>("bb_cherry_markdown_file", id);
        using var data = await stream.OpenReadStreamAsync();
        uploadFile.UploadStream = data;
        var ret = "";
        if (OnFileUpload != null)
        {
            ret = await OnFileUpload.Invoke(uploadFile);
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
                _value = vals[0];
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
    public ValueTask DoMethodAsync(string method, params object[] parameters) => Module.InvokeVoidAsync("bb_cherry_markdown_method", MarkdownElement, method, parameters);

    /// <summary>
    /// Dispose 方法
    /// </summary>
    /// <param name="disposing"></param>
    protected virtual async ValueTask DisposeAsync(bool disposing)
    {
        if (disposing)
        {
            if (Module != null)
            {
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
}
