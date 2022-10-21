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

    private string? _value;
    private bool IsRender { get; set; }

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
    /// OnInitialized 方法
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();
        Option.Value = _value;
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
    /// <returns></returns>
    protected override async Task ModuleInitAsync()
    {
        if (Module != null)
        {
            IsRender = false;
            await Module.InvokeVoidAsync($"{ModuleName}.init", Id, Option);
        }
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <returns></returns>
    protected override async Task ModuleExecuteAsync()
    {
        if (Module != null && IsRender)
        {
            IsRender = false;
            await Module.InvokeVoidAsync($"{ModuleName}.execute", Id, Value);
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
            var stream = await Module.InvokeAsync<IJSStreamReference>($"{ModuleName}.fetch", Id);
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
            var hasChanged = !EqualityComparer<string>.Default.Equals(vals[0], _value);
            if (hasChanged)
            {
                _value = vals[0];
                if (ValueChanged.HasDelegate)
                {
                    await ValueChanged.InvokeAsync(_value);
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
    public async ValueTask DoMethodAsync(string method, params object[] parameters)
    {
        if (Module != null)
        {
            await Module.InvokeVoidAsync($"{ModuleName}.invoke", Id, method, parameters);
        }
    }
}
