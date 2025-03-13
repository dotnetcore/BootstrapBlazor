// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using System.Collections.Concurrent;

namespace BootstrapBlazor.Components;

/// <summary>
/// Modal component
/// </summary>
public partial class Modal
{
    [Inject]
    [NotNull]
    private IOptionsMonitor<BootstrapBlazorOptions>? Options { get; set; }

    /// <summary>
    /// Gets the style string
    /// </summary>
    private string? ClassString => CssBuilder.Default("modal")
        .AddClass("fade", Options.CurrentValue.GetIsFadeValue(IsFade))
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    /// <summary>
    /// Gets the collection of ModalDialog
    /// </summary>
    protected List<ModalDialog> Dialogs { get; } = new(8);

    private readonly ConcurrentDictionary<IComponent, Func<Task>> _shownCallbackCache = [];

    /// <summary>
    /// Gets or sets whether to close the popup in the background, default is false
    /// </summary>
    [Parameter]
    public bool IsBackdrop { get; set; }

    /// <summary>
    /// Gets or sets whether to enable keyboard support, default is true to respond to the ESC key
    /// </summary>
    [Parameter]
    public bool IsKeyboard { get; set; } = true;

    /// <summary>
    /// Gets or sets whether to enable fade in and out animation, default is null
    /// </summary>
    [Parameter]
    public bool? IsFade { get; set; }

    /// <summary>
    /// Gets or sets the child component
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// Gets or sets the callback method when the component has finished rendering
    /// </summary>
    [Parameter]
    public Func<Modal, Task>? FirstAfterRenderCallbackAsync { get; set; }

    /// <summary>
    /// Gets or sets the callback method when the popup is shown
    /// </summary>
    [Parameter]
    public Func<Task>? OnShownAsync { get; set; }

    /// <summary>
    /// Gets or sets the callback delegate when the popup is closed
    /// </summary>
    [Parameter]
    public Func<Task>? OnCloseAsync { get; set; }

    /// <summary>
    /// Gets the background close popup setting
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
    /// Method to add a dialog
    /// </summary>
    /// <param name="dialog"></param>
    internal void AddDialog(ModalDialog dialog)
    {
        Dialogs.Add(dialog);
        ResetShownDialog(dialog);
    }

    /// <summary>
    /// Method to remove a dialog
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
    /// Callback method when the popup has been shown, called by JSInvoke
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
    /// Callback method when the popup has been closed, called by JSInvoke
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
    /// Method to toggle the popup state
    /// </summary>
    public async Task Toggle()
    {
        await ModuleInitTask.Task;
        await InvokeVoidAsync("execute", Id, "toggle");
    }

    /// <summary>
    /// Method to show the popup
    /// </summary>
    /// <returns></returns>
    public async Task Show()
    {
        await ModuleInitTask.Task;
        await InvokeVoidAsync("execute", Id, "show");
    }

    /// <summary>
    /// Method to close the current popup
    /// </summary>
    /// <returns></returns>
    public Task Close() => InvokeVoidAsync("execute", Id, "hide");

    /// <summary>
    /// Method to set the header text
    /// </summary>
    /// <param name="text"></param>
    public void SetHeaderText(string text)
    {
        var dialog = Dialogs.FirstOrDefault(d => d.IsShown);
        dialog?.SetHeaderText(text);
    }

    /// <summary>
    /// Registers a callback method to be called after the popup is shown, equivalent to setting the OnShownAsync parameter
    /// </summary>
    /// <param name="component">Component</param>
    /// <param name="value">Callback method</param>
    public void RegisterShownCallback(IComponent component, Func<Task> value)
    {
        _shownCallbackCache.AddOrUpdate(component, _ => value, (_, _) => value);
    }

    /// <summary>
    /// Unregisters the callback method to be called after the popup is shown
    /// </summary>
    /// <param name="component">Component</param>
    public void UnRegisterShownCallback(IComponent component)
    {
        _shownCallbackCache.TryRemove(component, out _);
    }
}
