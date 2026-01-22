// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using System.Reflection;

#if NET8_0_OR_GREATER
using System.Runtime.CompilerServices;
#endif

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
    private DialogService? DialogService { get; set; }

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

    [CascadingParameter]
    private Tab? TabSet { get; set; }

    [CascadingParameter]
    private Modal? Modal { get; set; }

    private Exception? _exception;

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
        if (EnableILogger)
        {
            await ErrorBoundaryLogger.LogErrorAsync(exception);
        }
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="builder"></param>
    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        if (_exception is not null)
        {
            builder.AddContent(0, RenderErrorContent(_exception));
            _exception = null;
            return;
        }

        if (CurrentException is null)
        {
            builder.AddContent(0, ChildContent);
            return;
        }

        var pageException = IsPageException(CurrentException);
        if (pageException)
        {
            RenderPageException(builder, CurrentException);
        }
        else
        {
            // 保持 UI 使用 Toast 通知
            builder.AddContent(0, ChildContent);
        }
        ResetException();
    }

    private void RenderPageException(RenderTreeBuilder builder, Exception ex)
    {
        if (OnErrorHandleAsync is not null)
        {
            _ = OnErrorHandleAsync(Logger, ex);
        }
        else
        {
            builder.AddContent(0, RenderErrorContent(ex));
        }
    }

    private RenderFragment RenderErrorContent(Exception ex) => builder =>
    {
        if (ErrorContent is null)
        {
            builder.OpenComponent<ErrorRender>(0);
            builder.AddAttribute(1, "Exception", ex);
            builder.CloseElement();
        }
        else
        {
            builder.AddContent(10, ErrorContent(ex));
        }
    };

    private static readonly string[] PageMethods = new string[] { "SetParametersAsync", "RunInitAndSetParametersAsync", "OnAfterRenderAsync" };

    private static bool IsPageException(Exception ex)
    {
        var errorMessage = ex.ToString();
        return PageMethods.Any(i => errorMessage.Contains(i, StringComparison.OrdinalIgnoreCase));
    }

#if NET8_0_OR_GREATER
    private void ResetException(Exception? exception = null)
    {
        ResetExceptionCore(this, exception);
    }
#else
    private PropertyInfo? _currentExceptionPropertyInfo;

    private void ResetException(Exception? exception = null)
    {
        _currentExceptionPropertyInfo ??= GetType().BaseType!.GetProperty(nameof(CurrentException), BindingFlags.NonPublic | BindingFlags.Instance)!;
        _currentExceptionPropertyInfo.SetValue(this, exception);
    }
#endif

    /// <summary>
    /// <para lang="zh">处理指定异常 由 <see cref="BootstrapComponentBase"/> 处理 <see cref="IHandleEvent"/> 接口调用</para>
    /// <para lang="en">Handle exception/></para>
    /// </summary>
    /// <param name="exception"></param>
    /// <returns></returns>
    public async Task HandlerExceptionAsync(Exception exception)
    {
        // 记录日志
        await OnErrorAsync(exception);

        // 自定义处理后调用其处理逻辑
        if (OnErrorHandleAsync is not null)
        {
            await OnErrorHandleAsync(Logger, exception);
            return;
        }

        if (TabSet != null)
        {
            await DialogService.ShowExceptionDialog(ToastTitle, RenderErrorContent(exception));
            return;
        }

        if (Modal != null)
        {
            _exception = exception;
            StateHasChanged();
            return;
        }

        await HandlerErrorContent(exception);
    }

    private async Task HandlerErrorContent(Exception exception)
    {
        if (ShowToast)
        {
            var option = new ToastOption()
            {
                Category = ToastCategory.Error,
                Title = ToastTitle,
                ChildContent = RenderErrorContent(exception)
            };
            await ToastService.Show(option);
        }
    }

#if NET8_0_OR_GREATER
    [UnsafeAccessor(UnsafeAccessorKind.Method, Name = "set_CurrentException")]
    static extern void ResetExceptionCore(ErrorBoundaryBase @this, Exception? exception);
#endif
}
