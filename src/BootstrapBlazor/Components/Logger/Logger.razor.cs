// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using System.Collections.Concurrent;

namespace BootstrapBlazor.Components;

/// <summary>
/// Logger 组件
/// </summary>
public partial class Logger
{
    /// <summary>
    /// 获得/设置 最大行数 默认 3 行
    /// </summary>
    [Parameter]
    public int Max { get; set; } = 3;

    /// <summary>
    /// 获得/设置 是否为 Html 代码 默认 false
    /// </summary>
    [Parameter]
    public bool IsHtml { get; set; }

    private ConcurrentQueue<string> Message { get; set; } = new();

    /// <summary>
    /// 获得 按钮样式集合
    /// </summary>
    /// <returns></returns>
    private string? ClassName => CssBuilder.Default("logger")
        .AddClass(Class)
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    private string Class { get; set; } = "collapse";

    /// <summary>
    /// 输入日志方法
    /// </summary>
    /// <param name="message"></param>
    public void Log(string message)
    {
        Message.Enqueue($"{DateTimeOffset.Now}: {message}");
        Class = "";
        if (Message.Count > Max)
        {
            Message.TryDequeue(out _);
        }
        StateHasChanged();
    }
}
