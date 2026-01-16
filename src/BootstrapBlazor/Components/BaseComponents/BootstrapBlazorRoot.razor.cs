// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.Extensions.Logging;

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">BootstrapBlazorRoot 组件</para>
/// <para lang="en">BootstrapBlazorRoot component</para>
/// </summary>
public partial class BootstrapBlazorRoot
{
    [Inject]
    [NotNull]
    private ICacheManager? Cache { get; set; }

    [Inject]
    [NotNull]
    private IOptionsMonitor<BootstrapBlazorOptions>? Options { get; set; }

    [Inject, NotNull]
    private IEnumerable<IRootComponentGenerator>? Generators { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 子组件</para>
    /// <para lang="en">Gets or sets child content</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// <para lang="zh">获得 Message 组件实例</para>
    /// <para lang="en">Gets the Message component instance</para>
    /// </summary>
    [NotNull]
    public Message? MessageContainer { get; private set; }

    /// <summary>
    /// <para lang="zh">获得 ToastContainer 组件实例</para>
    /// <para lang="en">Gets the ToastContainer component instance</para>
    /// </summary>
    [NotNull]
    public ToastContainer? ToastContainer { get; private set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否开启全局异常捕获 默认 null 使用 <see cref="BootstrapBlazorOptions.EnableErrorLogger"/> 设置值</para>
    /// <para lang="en">Gets or sets whether to enable global exception handling. Default is null (use <see cref="BootstrapBlazorOptions.EnableErrorLogger"/>)</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public bool? EnableErrorLogger { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否记录异常到 <see cref="ILogger"/> 默认 null 使用 <see cref="BootstrapBlazorOptions.EnableErrorLoggerILogger"/> 设置值</para>
    /// <para lang="en">Gets or sets whether to log exceptions to <see cref="ILogger"/>. Default is null (use <see cref="BootstrapBlazorOptions.EnableErrorLoggerILogger"/>)</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public bool? EnableErrorLoggerILogger { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否显示 Error 提示弹窗 默认 null 使用 <see cref="BootstrapBlazorOptions.ShowErrorLoggerToast"/> 设置值</para>
    /// <para lang="en">Gets or sets whether to show Error toast. Default is null (use <see cref="BootstrapBlazorOptions.ShowErrorLoggerToast"/>)</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    [Obsolete("已弃用，请使用 ShowErrorLoggerToast 参数. Deprecated, please use ShowErrorLoggerToast parameter")]
    [ExcludeFromCodeCoverage]
    public bool? ShowToast { get => ShowErrorLoggerToast; set => ShowErrorLoggerToast = value; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否显示 Error 提示弹窗 默认 null 使用 <see cref="BootstrapBlazorOptions.ShowErrorLoggerToast"/> 设置值</para>
    /// <para lang="en">Gets or sets whether to show Error toast. Default is null (use <see cref="BootstrapBlazorOptions.ShowErrorLoggerToast"/>)</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public bool? ShowErrorLoggerToast { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 Error Toast 弹窗标题</para>
    /// <para lang="en">Gets or sets Error Toast title</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public string? ToastTitle { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 自定义错误处理回调方法</para>
    /// <para lang="en">Gets or sets custom error callback method</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public Func<ILogger, Exception, Task>? OnErrorHandleAsync { get; set; }

    private bool EnableErrorLoggerValue => EnableErrorLogger ?? Options.CurrentValue.EnableErrorLogger;

    private bool EnableErrorLoggerILoggerValue => EnableErrorLoggerILogger ?? Options.CurrentValue.EnableErrorLoggerILogger;

    private bool ShowToastValue => ShowErrorLoggerToast ?? Options.CurrentValue.ShowErrorLoggerToast;

    /// <summary>
    /// <para lang="zh"><inheritdoc/></para>
    /// <para lang="en"><inheritdoc/></para>
    /// </summary>
    /// <param name="parameters"></param>
    /// <returns></returns>
    public override async Task SetParametersAsync(ParameterView parameters)
    {
        Cache.SetStartTime();

        await base.SetParametersAsync(parameters);
    }
}
