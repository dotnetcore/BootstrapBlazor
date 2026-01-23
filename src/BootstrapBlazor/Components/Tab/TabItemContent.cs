// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.AspNetCore.Components.Rendering;

namespace BootstrapBlazor.Components;

class TabItemContent : IComponent
{
    /// <summary>
    /// <para lang="zh">获得/设置 标签项，默认为 null</para>
    /// <para lang="en">Gets or sets the component content. Default is null</para>
    /// </summary>
    [Parameter, NotNull]
    public TabItem? Item { get; set; }

    [CascadingParameter, NotNull]
    private Tab? TabSet { get; set; }

    [Inject]
    [NotNull]
    private IOptionsMonitor<BootstrapBlazorOptions>? Options { get; set; }

    private RenderHandle _renderHandle;

    void IComponent.Attach(RenderHandle renderHandle)
    {
        _renderHandle = renderHandle;
    }

    Task IComponent.SetParametersAsync(ParameterView parameters)
    {
        parameters.SetParameterProperties(this);

        RenderContent();
        return Task.CompletedTask;
    }

    private void RenderContent()
    {
        _renderHandle.Render(BuildRenderTree);
    }

    private Guid _key = Guid.NewGuid();

    private bool EnableErrorLogger => TabSet.EnableErrorLogger ?? Options.CurrentValue.EnableErrorLogger;
    private bool EnableErrorLoggerILogger => TabSet.EnableErrorLoggerILogger ?? Options.CurrentValue.EnableErrorLoggerILogger;
    private bool ShowErrorLoggerToast => TabSet.ShowErrorLoggerToast ?? Options.CurrentValue.ShowErrorLoggerToast;
    private string ToastTitle => TabSet.ErrorLoggerToastTitle ?? "Error";

    private void BuildRenderTree(RenderTreeBuilder builder)
    {
        builder.OpenComponent<ErrorLogger>(0);
        builder.SetKey(_key);
        builder.AddAttribute(1, nameof(ErrorLogger.ChildContent), Item.ChildContent);
        builder.AddAttribute(2, nameof(ErrorLogger.EnableErrorLogger), EnableErrorLogger);
        builder.AddAttribute(3, nameof(ErrorLogger.EnableILogger), EnableErrorLoggerILogger);
        builder.AddAttribute(4, nameof(ErrorLogger.ShowToast), ShowErrorLoggerToast);
        builder.AddAttribute(5, nameof(ErrorLogger.ToastTitle), ToastTitle);
        builder.AddAttribute(6, nameof(ErrorLogger.OnErrorHandleAsync), TabSet.OnErrorHandleAsync);
        builder.CloseComponent();
    }

    /// <summary>
    /// <para lang="zh">重新呈现内容方法</para>
    /// <para lang="en">Render method</para>
    /// </summary>
    public void Render()
    {
        _key = Guid.NewGuid();
        RenderContent();
    }
}
