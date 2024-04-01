// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;

namespace BootstrapBlazor.Components;

/// <summary>
/// 
/// </summary>
public partial class CodeEditor
{
    private const string MONACO_VS_PATH = "./_content/BootstrapBlazor.CodeEditor/monaco-editor/min/vs";

    /// <summary>
    /// Language used by the editor: csharp, javascript, ...
    /// </summary>
    [Parameter]
    [NotNull]
    public string? Language { get; set; }

    /// <summary>
    /// Theme of the editor.
    /// </summary>
    [Parameter]
    [NotNull]
    public string? Theme { get; set; }

    /// <summary>
    /// Gets or sets the value of the input. This should be used with two-way binding.
    /// </summary>
    [Parameter]
    public string? Value { get; set; }

    /// <summary>
    /// Gets or sets a callback that updates the bound value.
    /// </summary>
    [Parameter]
    public EventCallback<string?> ValueChanged { get; set; }

    /// <summary>
    /// Gets or sets a callback that updates the bound value.
    /// </summary>
    [Parameter]
    public Func<string?, Task>? OnValueChanged { get; set; }

    /// <summary>
    /// 获得/设置 是否显示行号 默认 false
    /// </summary>
    [Parameter]
    public bool ShowLineNo { get; set; }

    /// <summary>
    /// 获得/设置 是否显示只读 默认 false
    /// </summary>
    [Parameter]
    public bool IsReadonly { get; set; }

    private string? ClassString => CssBuilder.Default("code-editor")
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        Language ??= "csharp";
        Theme ??= "vs";
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="firstRender"></param>
    /// <returns></returns>
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (!firstRender)
        {
            await InvokeVoidAsync("monacoSetOptions", Id, new { Value, Theme, Language });
        }
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <returns></returns>
    protected override async Task InvokeInitAsync()
    {
        var options = new
        {
            Value,
            Language,
            Theme,
            Path = MONACO_VS_PATH,
            LineNumbers = ShowLineNo,
            ReadOnly = IsReadonly,
        };
        await InvokeVoidAsync("init", Id, Interop, options);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public async Task Focus() => await InvokeVoidAsync("focus");

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public async Task Resize() => await InvokeVoidAsync("resize");

    /// <summary>
    /// 
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    [JSInvokable]
    public async Task UpdateValueAsync(string value)
    {
        Value = value;
        if (ValueChanged.HasDelegate)
        {
            await ValueChanged.InvokeAsync(Value);
        }
        if (OnValueChanged != null)
        {
            await OnValueChanged(Value);
        }
    }
}
