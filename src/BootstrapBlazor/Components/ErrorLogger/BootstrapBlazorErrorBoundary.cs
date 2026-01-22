// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using System.Reflection;

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">内部使用 自定义异常组件</para>
/// <para lang="en">Internal Use Custom Exception Component</para>
/// </summary>
class BootstrapBlazorErrorBoundary : ErrorBoundaryBase
{
    [Inject]
    [NotNull]
    private IStringLocalizer<ErrorLogger>? Localizer { get; set; }

    [Inject]
    [NotNull]
    private ILogger<BootstrapBlazorErrorBoundary>? Logger { get; set; }

    [Inject, NotNull]
    private IErrorBoundaryLogger? ErrorBoundaryLogger { get; set; }

    [Inject]
    [NotNull]
    private ToastService? ToastService { get; set; }

    [Inject]
    [NotNull]
    private NavigationManager? NavigationManager { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 自定义错误处理回调方法</para>
    /// <para lang="en">Gets or sets Custom Error Handler</para>
    /// </summary>
    [Parameter]
    public Func<ILogger, Exception, Task>? OnErrorHandleAsync { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否启用日志记录功能 默认 true 启用</para>
    /// <para lang="en">Gets or sets Whether to Enable Logging. Default true</para>
    /// </summary>
    [Parameter]
    public bool EnableILogger { get; set; } = true;

    /// <summary>
    /// <para lang="zh">获得/设置 是否显示弹窗 默认 true 显示</para>
    /// <para lang="en">Gets or sets Whether to Show Toast. Default true</para>
    /// </summary>
    [Parameter]
    public bool ShowToast { get; set; } = true;

    /// <summary>
    /// <para lang="zh">获得/设置 Toast 弹窗标题</para>
    /// <para lang="en">Gets or sets Toast Title</para>
    /// </summary>
    [Parameter]
    [NotNull]
    public string? ToastTitle { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        ToastTitle ??= Localizer[nameof(ToastTitle)];
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="exception"></param>
    protected override async Task OnErrorAsync(Exception exception)
    {
        await ErrorBoundaryLogger.LogErrorAsync(exception);
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="builder"></param>
    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        if (CurrentException is null)
        {
            builder.AddContent(0, ChildContent);
        }
        else if (ErrorContent is not null)
        {
            builder.AddContent(1, ErrorContent(CurrentException));
        }
        else
        {
            var pageException = IsPageException(CurrentException);
            if (pageException)
            {
                RenderException(builder, CurrentException);
            }
            else
            {
                builder.AddContent(0, ChildContent);
            }
            ResetException();
        }
    }

    private void RenderException(RenderTreeBuilder builder, Exception ex)
    {
        if (OnErrorHandleAsync is not null)
        {
            var renderTask = OnErrorHandleAsync(Logger, ex);
        }
        else
        {
            builder.OpenComponent<ErrorRender>(0);
            builder.AddAttribute(1, "Exception", ex);
            builder.CloseElement();
        }
    }

    private static readonly string[] PageMethods = new string[] { "SetParametersAsync", "RunInitAndSetParametersAsync", "OnAfterRenderAsync" };

    private static bool IsPageException(Exception ex)
    {
        var errorMessage = ex.ToString();
        return PageMethods.Any(i => errorMessage.Contains(i, StringComparison.OrdinalIgnoreCase));
    }

    private PropertyInfo? _currentExceptionPropertyInfo;

    private void ResetException()
    {
        _currentExceptionPropertyInfo ??= GetType().BaseType!.GetProperty(nameof(CurrentException), BindingFlags.NonPublic | BindingFlags.Instance)!;
        _currentExceptionPropertyInfo.SetValue(this, null);
    }
}
