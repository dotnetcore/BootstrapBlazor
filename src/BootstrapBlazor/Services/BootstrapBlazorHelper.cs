// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using System.Xml.Linq;

namespace BootstrapBlazor.Services;

/// <summary>
/// <inheritdoc/>
/// </summary>
public class BootstrapBlazorHelper : IBootstrapBlazorHelper, IAsyncDisposable
{
    private IJSRuntime JSRuntime { get; set; }

    [NotNull]
    private IJSObjectReference? Module { get; set; }

    [NotNull]
    private DotNetObjectReference<BootstrapBlazorHelper>? Interop { get; set; }

    private List<string> guidList { get; set; } = new List<string>();

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="jSRuntime"></param>
    public BootstrapBlazorHelper(IJSRuntime jSRuntime)
    {
        JSRuntime ??= jSRuntime;
        Interop ??= DotNetObjectReference.Create(this);
    }

    private async Task ImportModule() => Module ??= await JSRuntime.InvokeAsync<IJSObjectReference>("import", "./_content/BootstrapBlazor/modules/helper.js");

    /// <inheritdoc/>
    public async Task RegisterEvent(BootStrapBlazorEventType eventName)
    {
        await ImportModule();
        var guid = Guid.NewGuid();
        guidList.Add($"{guid}");
        await Module.InvokeVoidAsync("addEventListener", guid, Interop, $"JSInvokOn{eventName}", eventName);
    }

    /// <inheritdoc/>
    public async Task RegisterEvent(BootStrapBlazorEventType eventName, string Id)
    {
        await ImportModule();
        var guid = Guid.NewGuid();
        guidList.Add($"{guid}");
        await Module.InvokeVoidAsync("addEventListener", guid, Interop, $"JSInvokOn{eventName}", eventName, Id);
    }

    /// <inheritdoc/>
    public async Task RegisterEvent(BootStrapBlazorEventType eventName, ElementReference element)
    {
        await ImportModule();
        var guid = Guid.NewGuid();
        guidList.Add($"{guid}");
        await Module.InvokeVoidAsync("addEventListener", guid, Interop, $"JSInvokOn{eventName}", eventName, null, element);
    }

    /// <inheritdoc/>
    public async Task<T?> GetElementPropertiesByTagFromIdAsync<T>(string id, string tag)
    {
        await ImportModule();
        return await Module.InvokeAsync<T?>("getElementPropertiesByTagFromId", id, tag);
    }

    /// <inheritdoc/>
    public async Task<T?> GetDocumentPropertiesByTagAsync<T>(string tag)
    {
        await ImportModule();
        return await Module.InvokeAsync<T?>("getDocumentPropertiesByTag", tag);
    }

    /// <inheritdoc/>
    public async Task<T?> GetElementPropertiesByTagAsync<T>(ElementReference element, string tag)
    {
        await ImportModule();
        return await Module.InvokeAsync<T?>("getElementPropertiesByTag", element, tag);
    }

    #region Dispose

    /// <summary>
    /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources asynchronously.
    /// </summary>
    /// <param name="disposing"></param>
    /// <returns></returns>
    protected virtual async ValueTask DisposeAsync(bool disposing)
    {
        if (Module != null && disposing)
        {
            guidList.ForEach(async x => await Module.InvokeVoidAsync("dispose", x));
            guidList.Clear();

            Interop?.Dispose();

            await Module.DisposeAsync();
            Module = null;
        }
    }

    /// <inheritdoc/>
    public async Task RunJSEval(string js)
    {
        await ImportModule();
        await Module.InvokeVoidAsync("runJSEval", js);
    }

    /// <inheritdoc/>
    public async ValueTask DisposeAsync()
    {
        await DisposeAsync(true);
        GC.SuppressFinalize(this);
    }

    #endregion

    #region JSInvok

    /// <summary>
    /// OnClick JS 回调
    /// </summary>
    [JSInvokable] public void JSInvokOnClick() => OnClick?.Invoke();

    /// <summary>
    /// OnDblclick JS 回调
    /// </summary>
    [JSInvokable] public void JSInvokOnDblclick() => OnDblclick?.Invoke();

    /// <summary>
    /// OnMouseup JS 回调
    /// </summary>
    [JSInvokable] public void JSInvokOnMouseup() => OnMouseup?.Invoke();

    /// <summary>
    /// OnMousedown JS 回调
    /// </summary>
    [JSInvokable] public void JSInvokOnMousedown() => OnMousedown?.Invoke();

    /// <summary>
    /// OnContextmenu JS 回调
    /// </summary>
    [JSInvokable] public void JSInvokOnContextmenu() => OnContextmenu?.Invoke();

    /// <summary>
    /// OnMousewheel JS 回调
    /// </summary>
    [JSInvokable] public void JSInvokOnMousewheel() => OnMousewheel?.Invoke();

    /// <summary>
    /// OnDOMMouseScroll JS 回调
    /// </summary>
    [JSInvokable] public void JSInvokOnDOMMouseScroll() => OnDOMMouseScroll?.Invoke();

    /// <summary>
    /// OnMouseover JS 回调
    /// </summary>
    [JSInvokable] public void JSInvokOnMouseover() => OnMouseover?.Invoke();

    /// <summary>
    /// OnMouseout JS 回调
    /// </summary>
    [JSInvokable] public void JSInvokOnMouseout() => OnMouseout?.Invoke();

    /// <summary>
    /// OnMousemove JS 回调
    /// </summary>
    [JSInvokable] public void JSInvokOnMousemove() => OnMousemove?.Invoke();

    /// <summary>
    /// OnSelectstart JS 回调
    /// </summary>
    [JSInvokable] public void JSInvokOnSelectstart() => OnSelectstart?.Invoke();

    /// <summary>
    /// OnSelectend JS 回调
    /// </summary>
    [JSInvokable] public void JSInvokOnSelectend() => OnSelectend?.Invoke();

    /// <summary>
    /// OnKeydown JS 回调
    /// </summary>
    [JSInvokable] public void JSInvokOnKeydown() => OnKeydown?.Invoke();

    /// <summary>
    /// OnKeypress JS 回调
    /// </summary>
    [JSInvokable] public void JSInvokOnKeypress() => OnKeypress?.Invoke();

    /// <summary>
    /// OnKeyup JS 回调
    /// </summary>
    [JSInvokable] public void JSInvokOnKeyup() => OnKeyup?.Invoke();

    /// <summary>
    /// OnOrientationchange JS 回调
    /// </summary>
    [JSInvokable] public void JSInvokOnOrientationchange() => OnOrientationchange?.Invoke();

    /// <summary>
    /// OnTouchstart JS 回调
    /// </summary>
    [JSInvokable] public void JSInvokOnTouchstart() => OnTouchstart?.Invoke();

    /// <summary>
    /// OnTouchmove JS 回调
    /// </summary>
    [JSInvokable] public void JSInvokOnTouchmove() => OnTouchmove?.Invoke();

    /// <summary>
    /// OnTouchend JS 回调
    /// </summary>
    [JSInvokable] public void JSInvokOnTouchend() => OnTouchend?.Invoke();

    /// <summary>
    /// OnTouchcancel JS 回调
    /// </summary>
    [JSInvokable] public void JSInvokOnTouchcancel() => OnTouchcancel?.Invoke();

    /// <summary>
    /// OnPointerdown JS 回调
    /// </summary>
    [JSInvokable] public void JSInvokOnPointerdown() => OnPointerdown?.Invoke();

    /// <summary>
    /// OnPointermove JS 回调
    /// </summary>
    [JSInvokable] public void JSInvokOnPointermove() => OnPointermove?.Invoke();

    /// <summary>
    /// OnPointerup JS 回调
    /// </summary>
    [JSInvokable] public void JSInvokOnPointerup() => OnPointerup?.Invoke();

    /// <summary>
    /// OnPointerleave JS 回调
    /// </summary>
    [JSInvokable] public void JSInvokOnPointerleave() => OnPointerleave?.Invoke();

    /// <summary>
    /// OnPointercancel JS 回调
    /// </summary>
    [JSInvokable] public void JSInvokOnPointercancel() => OnPointercancel?.Invoke();

    /// <summary>
    /// OnGesturestart JS 回调
    /// </summary>
    [JSInvokable] public void JSInvokOnGesturestart() => OnGesturestart?.Invoke();

    /// <summary>
    /// OnGesturechange JS 回调
    /// </summary>
    [JSInvokable] public void JSInvokOnGesturechange() => OnGesturechange?.Invoke();

    /// <summary>
    /// OnGestureend JS 回调
    /// </summary>
    [JSInvokable] public void JSInvokOnGestureend() => OnGestureend?.Invoke();

    /// <summary>
    /// OnFocus JS 回调
    /// </summary>
    [JSInvokable] public void JSInvokOnFocus() => OnFocus?.Invoke();

    /// <summary>
    /// OnBlur JS 回调
    /// </summary>
    [JSInvokable] public void JSInvokOnBlur() => OnBlur?.Invoke();

    /// <summary>
    /// OnChange JS 回调
    /// </summary>
    [JSInvokable] public void JSInvokOnChange() => OnChange?.Invoke();

    /// <summary>
    /// OnReset JS 回调
    /// </summary>
    [JSInvokable] public void JSInvokOnReset() => OnReset?.Invoke();

    /// <summary>
    /// OnSelect JS 回调
    /// </summary>
    [JSInvokable] public void JSInvokOnSelect() => OnSelect?.Invoke();

    /// <summary>
    /// OnSubmit JS 回调
    /// </summary>
    [JSInvokable] public void JSInvokOnSubmit() => OnSubmit?.Invoke();

    /// <summary>
    /// OnFocusin JS 回调
    /// </summary>
    [JSInvokable] public void JSInvokOnFocusin() => OnFocusin?.Invoke();

    /// <summary>
    /// OnFocusout JS 回调
    /// </summary>
    [JSInvokable] public void JSInvokOnFocusout() => OnFocusout?.Invoke();

    /// <summary>
    /// OnLoad JS 回调
    /// </summary>
    [JSInvokable] public void JSInvokOnLoad() => OnLoad?.Invoke();

    /// <summary>
    /// OnUnload JS 回调
    /// </summary>
    [JSInvokable] public void JSInvokOnUnload() => OnUnload?.Invoke();

    /// <summary>
    /// OnBeforeunload JS 回调
    /// </summary>
    [JSInvokable] public void JSInvokOnBeforeunload() => OnBeforeunload?.Invoke();

    /// <summary>
    /// OnResize JS 回调
    /// </summary>
    [JSInvokable] public void JSInvokOnResize() => OnResize?.Invoke();

    /// <summary>
    /// OnMove JS 回调
    /// </summary>
    [JSInvokable] public void JSInvokOnMove() => OnMove?.Invoke();

    /// <summary>
    /// OnDOMContentLoaded JS 回调
    /// </summary>
    [JSInvokable] public void JSInvokOnDOMContentLoaded() => OnDOMContentLoaded?.Invoke();

    /// <summary>
    /// OnReadystatechange JS 回调
    /// </summary>
    [JSInvokable] public void JSInvokOnReadystatechange() => OnReadystatechange?.Invoke();

    /// <summary>
    /// OnError JS 回调
    /// </summary>
    [JSInvokable] public void JSInvokOnError() => OnError?.Invoke();

    /// <summary>
    /// OnAbort JS 回调
    /// </summary>
    [JSInvokable] public void JSInvokOnAbort() => OnAbort?.Invoke();

    /// <summary>
    /// OnScroll JS 回调
    /// </summary>
    [JSInvokable] public void JSInvokOnScroll() => OnScroll?.Invoke();

    #endregion

    #region Event
    /// <inheritdoc/>
    public event BootStrapBlazorEventHandler? OnClick;
    /// <inheritdoc/>
    public event BootStrapBlazorEventHandler? OnDblclick;
    /// <inheritdoc/>
    public event BootStrapBlazorEventHandler? OnMouseup;
    /// <inheritdoc/>
    public event BootStrapBlazorEventHandler? OnMousedown;
    /// <inheritdoc/>
    public event BootStrapBlazorEventHandler? OnContextmenu;
    /// <inheritdoc/>
    public event BootStrapBlazorEventHandler? OnMousewheel;
    /// <inheritdoc/>
    public event BootStrapBlazorEventHandler? OnDOMMouseScroll;
    /// <inheritdoc/>
    public event BootStrapBlazorEventHandler? OnMouseover;
    /// <inheritdoc/>
    public event BootStrapBlazorEventHandler? OnMouseout;
    /// <inheritdoc/>
    public event BootStrapBlazorEventHandler? OnMousemove;
    /// <inheritdoc/>
    public event BootStrapBlazorEventHandler? OnSelectstart;
    /// <inheritdoc/>
    public event BootStrapBlazorEventHandler? OnSelectend;
    /// <inheritdoc/>
    public event BootStrapBlazorEventHandler? OnKeydown;
    /// <inheritdoc/>
    public event BootStrapBlazorEventHandler? OnKeypress;
    /// <inheritdoc/>
    public event BootStrapBlazorEventHandler? OnKeyup;
    /// <inheritdoc/>
    public event BootStrapBlazorEventHandler? OnOrientationchange;
    /// <inheritdoc/>
    public event BootStrapBlazorEventHandler? OnTouchstart;
    /// <inheritdoc/>
    public event BootStrapBlazorEventHandler? OnTouchmove;
    /// <inheritdoc/>
    public event BootStrapBlazorEventHandler? OnTouchend;
    /// <inheritdoc/>
    public event BootStrapBlazorEventHandler? OnTouchcancel;
    /// <inheritdoc/>
    public event BootStrapBlazorEventHandler? OnPointerdown;
    /// <inheritdoc/>
    public event BootStrapBlazorEventHandler? OnPointermove;
    /// <inheritdoc/>
    public event BootStrapBlazorEventHandler? OnPointerup;
    /// <inheritdoc/>
    public event BootStrapBlazorEventHandler? OnPointerleave;
    /// <inheritdoc/>
    public event BootStrapBlazorEventHandler? OnPointercancel;
    /// <inheritdoc/>
    public event BootStrapBlazorEventHandler? OnGesturestart;
    /// <inheritdoc/>
    public event BootStrapBlazorEventHandler? OnGesturechange;
    /// <inheritdoc/>
    public event BootStrapBlazorEventHandler? OnGestureend;
    /// <inheritdoc/>
    public event BootStrapBlazorEventHandler? OnFocus;
    /// <inheritdoc/>
    public event BootStrapBlazorEventHandler? OnBlur;
    /// <inheritdoc/>
    public event BootStrapBlazorEventHandler? OnChange;
    /// <inheritdoc/>
    public event BootStrapBlazorEventHandler? OnReset;
    /// <inheritdoc/>
    public event BootStrapBlazorEventHandler? OnSelect;
    /// <inheritdoc/>
    public event BootStrapBlazorEventHandler? OnSubmit;
    /// <inheritdoc/>
    public event BootStrapBlazorEventHandler? OnFocusin;
    /// <inheritdoc/>
    public event BootStrapBlazorEventHandler? OnFocusout;
    /// <inheritdoc/>
    public event BootStrapBlazorEventHandler? OnLoad;
    /// <inheritdoc/>
    public event BootStrapBlazorEventHandler? OnUnload;
    /// <inheritdoc/>
    public event BootStrapBlazorEventHandler? OnBeforeunload;
    /// <inheritdoc/>
    public event BootStrapBlazorEventHandler? OnResize;
    /// <inheritdoc/>
    public event BootStrapBlazorEventHandler? OnMove;
    /// <inheritdoc/>
    public event BootStrapBlazorEventHandler? OnDOMContentLoaded;
    /// <inheritdoc/>
    public event BootStrapBlazorEventHandler? OnReadystatechange;
    /// <inheritdoc/>
    public event BootStrapBlazorEventHandler? OnError;
    /// <inheritdoc/>
    public event BootStrapBlazorEventHandler? OnAbort;
    /// <inheritdoc/>
    public event BootStrapBlazorEventHandler? OnScroll;
    #endregion
}

/// <summary>
/// <inheritdoc/>
/// </summary>
public delegate void BootStrapBlazorEventHandler();
