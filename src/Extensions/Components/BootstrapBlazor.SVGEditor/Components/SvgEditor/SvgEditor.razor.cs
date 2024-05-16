// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;

namespace BootstrapBlazor.Components;

/// <summary>
/// SvgEditor component
/// </summary>
public partial class SvgEditor
{
    /// <summary>
    /// 获得/设置 首次加载内容
    /// </summary>
    [Parameter]
    [NotNull]
    public string? PreContent { get; set; }

    /// <summary>
    /// 获得/设置 保存编辑器内容回调
    /// </summary>
    [Parameter]
    [NotNull]
    public Func<string, Task>? OnSaveChanged { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <returns></returns>
    protected override Task InvokeInitAsync() => InvokeVoidAsync("init", Id, new { Interop, Preload = PreContent, Callback = nameof(GetContent) });

    /// <summary>
    /// 更新内容方法
    /// </summary>
    /// <param name="content"></param>
    /// <returns></returns>
    public async Task UpdateAsync(string content) => await InvokeVoidAsync("update", Id, content);

    /// <summary>
    /// 获得内容方法 由 JavaScript 调用
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    [JSInvokable]
    public async Task GetContent(string value)
    {
        if (OnSaveChanged != null)
        {
            await OnSaveChanged(value);
        }
    }
}
