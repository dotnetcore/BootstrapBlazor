// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using System.Collections.Concurrent;

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">Modal 组件</para>
/// <para lang="en">Modal component</para>
/// </summary>
public partial class Modal
{
    [Inject]
    [NotNull]
    private IOptionsMonitor<BootstrapBlazorOptions>? Options { get; set; }

    /// <summary>
    /// <para lang="zh">获得样式字符串</para>
    /// <para lang="en">Gets the style string</para>
    /// </summary>
    private string? ClassString => CssBuilder.Default("modal")
        .AddClass("fade", Options.CurrentValue.GetIsFadeValue(IsFade))
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    /// <summary>
    /// <para lang="zh">获得ModalDialog的集合</para>
    /// <para lang="en">Gets the collection of ModalDialog</para>
    /// </summary>
    protected List<ModalDialog> Dialogs { get; } = new(8);

    private readonly ConcurrentDictionary<IComponent, Func<Task>> _shownCallbackCache = [];

    /// <summary>
    /// <para lang="zh">获得/设置 是否在后台关闭弹出窗口，默认为 false</para>
    /// <para lang="en">Gets or sets whether to close the popup in the background, default is false</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public bool IsBackdrop { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否启用键盘支持，默认为 true 响应 ESC 键</para>
    /// <para lang="en">Gets or sets whether to enable keyboard support, default is true to respond to the ESC key</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public bool IsKeyboard { get; set; } = true;

    /// <summary>
    /// <para lang="zh">获得/设置 是否启用淡入淡出动画，默认为 null</para>
    /// <para lang="en">Gets or sets whether to enable fade in and out animation, default is null</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public bool? IsFade { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 子组件</para>
    /// <para lang="en">Gets or sets the child component</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 组件完成渲染时的回调方法</para>
    /// <para lang="en">Gets or sets the callback method when the component has finished rendering</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public Func<Modal, Task>? FirstAfterRenderCallbackAsync { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 弹出窗口显示时的回调方法</para>
    /// <para lang="en">Gets or sets the callback method when the popup is shown</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public Func<Task>? OnShownAsync { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 弹出窗口关闭时的回调委托</para>
    /// <para lang="en">Gets or sets the callback delegate when the popup is closed</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public Func<Task>? OnCloseAsync { get; set; }

    /// <summary>
    /// <para lang="zh">获得后台关闭弹出窗口的设置</para>
    /// <para lang="en">Gets the background close popup setting</para>
    /// </summary>
    private string? Backdrop => IsBackdrop ? null : "static";

    private string KeyboardString => IsKeyboard ? "true" : "false";

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="firstRender"></param>
    /// <returns></returns>
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (firstRender && FirstAfterRenderCallbackAsync != null)
        {
            await FirstAfterRenderCallbackAsync(this);
        }
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <returns></returns>
    protected override Task InvokeInitAsync() => InvokeVoidAsync("init", Id, Interop, nameof(ShownCallback), nameof(CloseCallback));

    /// <summary>
    /// <para lang="zh">添加对话框的方法</para>
    /// <para lang="en">Method to add a dialog</para>
    /// </summary>
    /// <param name="dialog"></param>
    internal void AddDialog(ModalDialog dialog)
    {
        Dialogs.Add(dialog);
        ResetShownDialog(dialog);
    }

    /// <summary>
    /// <para lang="zh">删除对话框的方法</para>
    /// <para lang="en">Method to remove a dialog</para>
    /// </summary>
    /// <param name="dialog"></param>
    internal void RemoveDialog(ModalDialog dialog)
    {
        // Remove the current popup
        Dialogs.Remove(dialog);

        if (Dialogs.Count > 0)
        {
            ResetShownDialog(Dialogs.Last());
        }
    }

    private void ResetShownDialog(ModalDialog dialog)
    {
        // Ensure the newly added Dialog is the current popup
        Dialogs.ForEach(d =>
        {
            d.IsShown = d == dialog;
        });
    }

    /// <summary>
    /// <para lang="zh">弹出窗口显示时的回调方法，由 JSInvoke 调用</para>
    /// <para lang="en">Callback method when the popup has been shown, called by JSInvoke</para>
    /// </summary>
    /// <returns></returns>
    [JSInvokable]
    public async Task ShownCallback()
    {
        if (OnShownAsync != null)
        {
            await OnShownAsync();
        }

        foreach (var callback in _shownCallbackCache.Values)
        {
            await callback();
        }
    }

    /// <summary>
    /// <para lang="zh">弹出窗口关闭时的回调方法，由 JSInvoke 调用</para>
    /// <para lang="en">Callback method when the popup has been closed, called by JSInvoke</para>
    /// </summary>
    /// <returns></returns>
    [JSInvokable]
    public async Task CloseCallback()
    {
        // Remove the current popup
        var dialog = Dialogs.FirstOrDefault(d => d.IsShown);
        if (dialog != null)
        {
            Dialogs.Remove(dialog);
        }

        // Support for multi-level popups
        if (Dialogs.Count > 0)
        {
            ResetShownDialog(Dialogs.Last());
        }

        if (OnCloseAsync != null)
        {
            await OnCloseAsync();
        }
    }

    /// <summary>
    /// <para lang="zh">切换弹出窗口状态的方法</para>
    /// <para lang="en">Method to toggle the popup state</para>
    /// </summary>
    public async Task Toggle()
    {
        await ModuleInitTask.Task;
        await InvokeVoidAsync("execute", Id, "toggle");
    }

    /// <summary>
    /// <para lang="zh">显示弹出窗口的方法</para>
    /// <para lang="en">Method to show the popup</para>
    /// </summary>
    /// <returns></returns>
    public async Task Show()
    {
        await ModuleInitTask.Task;
        await InvokeVoidAsync("execute", Id, "show");
    }

    /// <summary>
    /// <para lang="zh">关闭当前弹出窗口的方法</para>
    /// <para lang="en">Method to close the current popup</para>
    /// </summary>
    /// <returns></returns>
    public Task Close() => InvokeVoidAsync("execute", Id, "hide");

    /// <summary>
    /// <para lang="zh">设置标题文本的方法</para>
    /// <para lang="en">Method to set the header text</para>
    /// </summary>
    /// <param name="text"></param>
    public void SetHeaderText(string text)
    {
        var dialog = Dialogs.FirstOrDefault(d => d.IsShown);
        dialog?.SetHeaderText(text);
    }

    /// <summary>
    /// <para lang="zh">注册弹出窗口显示后调用的回调方法，等同于设置 OnShownAsync 参数</para>
    /// <para lang="en">Registers a callback method to be called after the popup is shown, equivalent to setting the OnShownAsync parameter</para>
    /// </summary>
    /// <param name="component">Component</param>
    /// <param name="value">Callback method</param>
    public void RegisterShownCallback(IComponent component, Func<Task> value)
    {
        _shownCallbackCache.AddOrUpdate(component, _ => value, (_, _) => value);
    }

    /// <summary>
    /// <para lang="zh">注销弹出窗口显示后调用的回调方法</para>
    /// <para lang="en">Unregisters the callback method to be called after the popup is shown</para>
    /// </summary>
    /// <param name="component">Component</param>
    public void UnRegisterShownCallback(IComponent component)
    {
        _shownCallbackCache.TryRemove(component, out _);
    }
}
