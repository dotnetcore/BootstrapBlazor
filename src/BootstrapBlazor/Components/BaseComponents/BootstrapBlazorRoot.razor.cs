// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace BootstrapBlazor.Components;

/// <summary>
/// BootstrapBlazorRoot 组件
/// </summary>
public partial class BootstrapBlazorRoot
{
    [Inject]
    [NotNull]
    private ICacheManager? Cache { get; set; }

    [Inject]
    [NotNull]
    private IOptionsMonitor<BootstrapBlazorOptions>? Options { get; set; }

    /// <summary>
    /// 获得/设置 子组件
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// 获得 Message 组件实例
    /// </summary>
    [NotNull]
    public Message? MessageContainer { get; private set; }

    /// <summary>
    /// 获得 Toast 组件实例
    /// </summary>
    [NotNull]
    public ToastContainer? ToastContainer { get; private set; }

    /// <summary>
    /// 获得/设置 自定义错误处理回调方法
    /// </summary>
    [Parameter]
    public Func<ILogger, Exception, Task>? OnErrorHandleAsync { get; set; }

    /// <summary>
    /// 获得/设置 是否显示 Error 提示弹窗 默认 true 显示
    /// </summary>
    [Parameter]
    public bool ShowToast { get; set; } = true;

    /// <summary>
    /// 获得/设置 Error Toast 弹窗标题
    /// </summary>
    [Parameter]
    public string? ToastTitle { get; set; }

    /// <summary>
    /// 获得/设置 是否开启全局异常捕获 默认 null 读取配置文件 EnableErrorLogger 值
    /// </summary>
    [Parameter]
    public bool? EnableErrorLogger { get; set; }

    private bool EnableErrorLoggerValue => EnableErrorLogger ?? Options.CurrentValue.EnableErrorLogger;

    /// <summary>
    /// SetParametersAsync 方法
    /// </summary>
    /// <param name="parameters"></param>
    /// <returns></returns>
    public override async Task SetParametersAsync(ParameterView parameters)
    {
        Cache.SetStartTime();

        await base.SetParametersAsync(parameters);
    }

    private RenderFragment RenderBody() => builder =>
    {
        if (EnableErrorLoggerValue)
        {
            builder.OpenComponent<ErrorLogger>(0);
            builder.AddAttribute(1, nameof(ErrorLogger.ShowToast), ShowToast);
            builder.AddAttribute(2, nameof(ErrorLogger.ToastTitle), ToastTitle);
            if (OnErrorHandleAsync != null)
            {
                builder.AddAttribute(3, nameof(ErrorLogger.OnErrorHandleAsync), OnErrorHandleAsync);
            }
            builder.AddAttribute(4, nameof(ErrorLogger.ChildContent), RenderContent);
            builder.CloseComponent();
        }
        else
        {
            builder.AddContent(0, RenderContent);
        }
    };

    private static RenderFragment RenderComponents() => builder =>
    {
        builder.OpenComponent<Dialog>(0);
        builder.CloseComponent();

        builder.OpenComponent<Ajax>(1);
        builder.CloseComponent();

        builder.OpenComponent<SweetAlert>(2);
        builder.CloseComponent();

        builder.OpenComponent<Print>(3);
        builder.CloseComponent();

        builder.OpenComponent<Download>(4);
        builder.CloseComponent();
    };

    private RenderFragment RenderContent => builder =>
    {
        Render();

        [ExcludeFromCodeCoverage]
        void Render()
        {
            if (OperatingSystem.IsBrowser())
            {
                builder.AddContent(0, RenderChildContent);
                builder.AddContent(1, RenderComponents());
            }
            else
            {
                builder.OpenElement(0, "app");
                builder.AddContent(1, RenderChildContent);
                builder.CloseElement();
                builder.AddContent(2, RenderComponents());
            }
        }
    };
}
