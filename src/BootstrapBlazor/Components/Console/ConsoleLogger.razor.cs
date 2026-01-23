// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using System.Collections.Concurrent;

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">Logger 组件</para>
/// <para lang="en">Logger component</para>
/// </summary>
public partial class ConsoleLogger
{
    /// <summary>
    /// <para lang="zh">获得/设置 最大行数 默认 3 行</para>
    /// <para lang="en">Gets or sets max rows, default is 3</para>
    /// </summary>
    [Parameter]
    public int Max { get; set; } = 3;

    /// <summary>
    /// <para lang="zh">获得/设置 是否为 Html 代码 默认 false</para>
    /// <para lang="en">Gets or sets whether it is Html code, default is false</para>
    /// </summary>
    [Parameter]
    public bool IsHtml { get; set; }

    private ConcurrentQueue<string> Message { get; } = new();

    /// <summary>
    /// <para lang="zh">获得 按钮样式集合</para>
    /// <para lang="en">Get button style collection</para>
    /// </summary>
    private string? ClassName => CssBuilder.Default("console-logger")
        .AddClass(Class)
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    private string Class { get; set; } = "collapse";

    /// <summary>
    /// <para lang="zh">输入日志方法</para>
    /// <para lang="en">Log method</para>
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
