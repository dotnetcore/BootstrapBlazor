// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.JSInterop;

using System.ComponentModel;
using System.Reflection;
using System.Reflection.Metadata;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace BootstrapBlazor.Services;

/// <summary>
/// <inheritdoc/>
/// </summary>
public class BootstrapBlazorHelper : IBootstrapBlazorHelper
{
    private IJSRuntime JSRuntime { get; set; }

    [NotNull]
    private IJSObjectReference? Module { get; set; }

    [NotNull]
    private DotNetObjectReference<BootstrapBlazorHelper>? Interop { get; set; }

    public BootstrapBlazorHelper(IJSRuntime js) => JSRuntime = js;

    private async Task ImportModule()
    {
        Module = await JSRuntime.InvokeAsync<IJSObjectReference>("import", "./_content/BootstrapBlazor/modules/event-handler.js");
        Interop = DotNetObjectReference.Create(this);
    }

    public async Task RegisterEvent(BootStrapBlazorEventType handles, string? Id)
    {
        await ImportModule();
        await Module.InvokeVoidAsync("registerEvent", Interop, handles, $"JSInvokOn{handles}", Id);
    }

    public async Task<T> GetIdPropertieByNameAsync<T>(string id, string tag)
    {
        await ImportModule();
        return await Module.InvokeAsync<T>("getIdPropertieByName", id, tag);
    }

    public async Task<T> GetDocumentPropertieByNameAsync<T>(string tag)
    {
        await ImportModule();
        return await Module.InvokeAsync<T>("getDocumentPropertieByName", tag);
    }

    public async Task<T> GetElementPropertieByNameAsync<T>(ElementReference element, string tag)
    {
        await ImportModule();
        return await Module.InvokeAsync<T>("getElementPropertieByName", element, tag);
    }

    #region JSInvok
    [JSInvokable] public void JSInvokOnClick() => OnClick?.Invoke();
    [JSInvokable] public void JSInvokOnDblclick() => OnDblclick?.Invoke();
    [JSInvokable] public void JSInvokOnMouseup() => OnMouseup?.Invoke();
    [JSInvokable] public void JSInvokOnMousedown() => OnMousedown?.Invoke();
    [JSInvokable] public void JSInvokOnContextmenu() => OnContextmenu?.Invoke();
    [JSInvokable] public void JSInvokOnMousewheel() => OnMousewheel?.Invoke();
    [JSInvokable] public void JSInvokOnDOMMouseScroll() => OnDOMMouseScroll?.Invoke();
    [JSInvokable] public void JSInvokOnMouseover() => OnMouseover?.Invoke();
    [JSInvokable] public void JSInvokOnMouseout() => OnMouseout?.Invoke();
    [JSInvokable] public void JSInvokOnMousemove() => OnMousemove?.Invoke();
    [JSInvokable] public void JSInvokOnSelectstart() => OnSelectstart?.Invoke();
    [JSInvokable] public void JSInvokOnSelectend() => OnSelectend?.Invoke();
    [JSInvokable] public void JSInvokOnKeydown() => OnKeydown?.Invoke();
    [JSInvokable] public void JSInvokOnKeypress() => OnKeypress?.Invoke();
    [JSInvokable] public void JSInvokOnKeyup() => OnKeyup?.Invoke();
    [JSInvokable] public void JSInvokOnOrientationchange() => OnOrientationchange?.Invoke();
    [JSInvokable] public void JSInvokOnTouchstart() => OnTouchstart?.Invoke();
    [JSInvokable] public void JSInvokOnTouchmove() => OnTouchmove?.Invoke();
    [JSInvokable] public void JSInvokOnTouchend() => OnTouchend?.Invoke();
    [JSInvokable] public void JSInvokOnTouchcancel() => OnTouchcancel?.Invoke();
    [JSInvokable] public void JSInvokOnPointerdown() => OnPointerdown?.Invoke();
    [JSInvokable] public void JSInvokOnPointermove() => OnPointermove?.Invoke();
    [JSInvokable] public void JSInvokOnPointerup() => OnPointerup?.Invoke();
    [JSInvokable] public void JSInvokOnPointerleave() => OnPointerleave?.Invoke();
    [JSInvokable] public void JSInvokOnPointercancel() => OnPointercancel?.Invoke();
    [JSInvokable] public void JSInvokOnGesturestart() => OnGesturestart?.Invoke();
    [JSInvokable] public void JSInvokOnGesturechange() => OnGesturechange?.Invoke();
    [JSInvokable] public void JSInvokOnGestureend() => OnGestureend?.Invoke();
    [JSInvokable] public void JSInvokOnFocus() => OnFocus?.Invoke();
    [JSInvokable] public void JSInvokOnBlur() => OnBlur?.Invoke();
    [JSInvokable] public void JSInvokOnChange() => OnChange?.Invoke();
    [JSInvokable] public void JSInvokOnReset() => OnReset?.Invoke();
    [JSInvokable] public void JSInvokOnSelect() => OnSelect?.Invoke();
    [JSInvokable] public void JSInvokOnSubmit() => OnSubmit?.Invoke();
    [JSInvokable] public void JSInvokOnFocusin() => OnFocusin?.Invoke();
    [JSInvokable] public void JSInvokOnFocusout() => OnFocusout?.Invoke();
    [JSInvokable] public void JSInvokOnLoad() => OnLoad?.Invoke();
    [JSInvokable] public void JSInvokOnUnload() => OnUnload?.Invoke();
    [JSInvokable] public void JSInvokOnBeforeunload() => OnBeforeunload?.Invoke();
    [JSInvokable] public void JSInvokOnResize() => OnResize?.Invoke();
    [JSInvokable] public void JSInvokOnMove() => OnMove?.Invoke();
    [JSInvokable] public void JSInvokOnDOMContentLoaded() => OnDOMContentLoaded?.Invoke();
    [JSInvokable] public void JSInvokOnReadystatechange() => OnReadystatechange?.Invoke();
    [JSInvokable] public void JSInvokOnError() => OnError?.Invoke();
    [JSInvokable] public void JSInvokOnAbort() => OnAbort?.Invoke();
    [JSInvokable] public void JSInvokOnScroll() => OnScroll?.Invoke();

    #endregion

    #region Event
    public event BootStrapBlazorEventHandler OnClick;
    public event BootStrapBlazorEventHandler OnDblclick;
    public event BootStrapBlazorEventHandler OnMouseup;
    public event BootStrapBlazorEventHandler OnMousedown;
    public event BootStrapBlazorEventHandler OnContextmenu;
    public event BootStrapBlazorEventHandler OnMousewheel;
    public event BootStrapBlazorEventHandler OnDOMMouseScroll;
    public event BootStrapBlazorEventHandler OnMouseover;
    public event BootStrapBlazorEventHandler OnMouseout;
    public event BootStrapBlazorEventHandler OnMousemove;
    public event BootStrapBlazorEventHandler OnSelectstart;
    public event BootStrapBlazorEventHandler OnSelectend;
    public event BootStrapBlazorEventHandler OnKeydown;
    public event BootStrapBlazorEventHandler OnKeypress;
    public event BootStrapBlazorEventHandler OnKeyup;
    public event BootStrapBlazorEventHandler OnOrientationchange;
    public event BootStrapBlazorEventHandler OnTouchstart;
    public event BootStrapBlazorEventHandler OnTouchmove;
    public event BootStrapBlazorEventHandler OnTouchend;
    public event BootStrapBlazorEventHandler OnTouchcancel;
    public event BootStrapBlazorEventHandler OnPointerdown;
    public event BootStrapBlazorEventHandler OnPointermove;
    public event BootStrapBlazorEventHandler OnPointerup;
    public event BootStrapBlazorEventHandler OnPointerleave;
    public event BootStrapBlazorEventHandler OnPointercancel;
    public event BootStrapBlazorEventHandler OnGesturestart;
    public event BootStrapBlazorEventHandler OnGesturechange;
    public event BootStrapBlazorEventHandler OnGestureend;
    public event BootStrapBlazorEventHandler OnFocus;
    public event BootStrapBlazorEventHandler OnBlur;
    public event BootStrapBlazorEventHandler OnChange;
    public event BootStrapBlazorEventHandler OnReset;
    public event BootStrapBlazorEventHandler OnSelect;
    public event BootStrapBlazorEventHandler OnSubmit;
    public event BootStrapBlazorEventHandler OnFocusin;
    public event BootStrapBlazorEventHandler OnFocusout;
    public event BootStrapBlazorEventHandler OnLoad;
    public event BootStrapBlazorEventHandler OnUnload;
    public event BootStrapBlazorEventHandler OnBeforeunload;
    public event BootStrapBlazorEventHandler OnResize;
    public event BootStrapBlazorEventHandler OnMove;
    public event BootStrapBlazorEventHandler OnDOMContentLoaded;
    public event BootStrapBlazorEventHandler OnReadystatechange;
    public event BootStrapBlazorEventHandler OnError;
    public event BootStrapBlazorEventHandler OnAbort;
    public event BootStrapBlazorEventHandler OnScroll;

    #endregion
}

/// <summary>
/// <inheritdoc/>
/// </summary>
public delegate void BootStrapBlazorEventHandler();

class EventHandlesConverter : JsonConverter<BootStrapBlazorEventType>
{
    public override BootStrapBlazorEventType Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        throw new NotImplementedException();
    }

    public override void Write(Utf8JsonWriter writer, BootStrapBlazorEventType value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToDescriptionString());
    }
}

/// <summary>
/// 
/// </summary>
[JsonConverter(typeof(EventHandlesConverter))]
public enum BootStrapBlazorEventType
{
    /// <summary>
    /// 
    /// </summary>
    [Description("click")]
    Click,
    /// <summary>
    /// 
    /// </summary>
    [Description("dblclick")]
    Dblclick,
    /// <summary>
    /// 
    /// </summary>
    [Description("mouseup")]
    Mouseup,
    /// <summary>
    /// 
    /// </summary>
    [Description("mousedown")]
    Mousedown,
    /// <summary>
    /// 
    /// </summary>
    [Description("contextmenu")]
    Contextmenu,
    /// <summary>
    /// 
    /// </summary>
    [Description("mousewheel")]
    Mousewheel,
    /// <summary>
    /// 
    /// </summary>
    [Description("dOMMouseScroll")]
    DOMMouseScroll,
    /// <summary>
    /// 
    /// </summary>
    [Description("mouseover")]
    Mouseover,
    /// <summary>
    /// 
    /// </summary>
    [Description("mouseout")]
    Mouseout,
    /// <summary>
    /// 
    /// </summary>
    [Description("mousemove")]
    Mousemove,
    /// <summary>
    /// 
    /// </summary>
    [Description("selectstart")]
    Selectstart,
    /// <summary>
    /// 
    /// </summary>
    [Description("selectend")]
    Selectend,
    /// <summary>
    /// 
    /// </summary>
    [Description("keydown")]
    Keydown,
    /// <summary>
    /// 
    /// </summary>
    [Description("keypress")]
    Keypress,
    /// <summary>
    /// 
    /// </summary>
    [Description("keyup")]
    Keyup,
    /// <summary>
    /// 
    /// </summary>
    [Description("orientationchange")]
    Orientationchange,
    /// <summary>
    /// 
    /// </summary>
    [Description("touchstart")]
    Touchstart,
    /// <summary>
    /// 
    /// </summary>
    [Description("touchmove")]
    Touchmove,
    /// <summary>
    /// 
    /// </summary>
    [Description("touchend")]
    Touchend,
    /// <summary>
    /// 
    /// </summary>
    [Description("touchcancel")]
    Touchcancel,
    /// <summary>
    /// 
    /// </summary>
    [Description("pointerdown")]
    Pointerdown,
    /// <summary>
    /// 
    /// </summary>
    [Description("pointermove")]
    Pointermove,
    /// <summary>
    /// 
    /// </summary>
    [Description("pointerup")]
    Pointerup,
    /// <summary>
    /// 
    /// </summary>
    [Description("pointerleave")]
    Pointerleave,
    /// <summary>
    /// 
    /// </summary>
    [Description("pointercancel")]
    Pointercancel,
    /// <summary>
    /// 
    /// </summary>
    [Description("gesturestart")]
    Gesturestart,
    /// <summary>
    /// 
    /// </summary>
    [Description("gesturechange")]
    Gesturechange,
    /// <summary>
    /// 
    /// </summary>
    [Description("gestureend")]
    Gestureend,
    /// <summary>
    /// 
    /// </summary>
    [Description("focus")]
    Focus,
    /// <summary>
    /// 
    /// </summary>
    [Description("blur")]
    Blur,
    /// <summary>
    /// 
    /// </summary>
    [Description("change")]
    Change,
    /// <summary>
    /// 
    /// </summary>
    [Description("reset")]
    Reset,
    /// <summary>
    /// 
    /// </summary>
    [Description("select")]
    Select,
    /// <summary>
    /// 
    /// </summary>
    [Description("submit")]
    Submit,
    /// <summary>
    /// 
    /// </summary>
    [Description("focusin")]
    Focusin,
    /// <summary>
    /// 
    /// </summary>
    [Description("focusout")]
    Focusout,
    /// <summary>
    /// 
    /// </summary>
    [Description("load")]
    Load,
    /// <summary>
    /// 
    /// </summary>
    [Description("unload")]
    Unload,
    /// <summary>
    /// 
    /// </summary>
    [Description("beforeunload")]
    Beforeunload,
    /// <summary>
    /// 
    /// </summary>
    [Description("resize")]
    Resize,
    /// <summary>
    /// 
    /// </summary>
    [Description("move")]
    Move,
    /// <summary>
    /// 
    /// </summary>
    [Description("dOMContentLoaded")]
    DOMContentLoaded,
    /// <summary>
    /// 
    /// </summary>
    [Description("readystatechange")]
    Readystatechange,
    /// <summary>
    /// 
    /// </summary>
    [Description("error")]
    Error,
    /// <summary>
    /// 
    /// </summary>
    [Description("abort")]
    Abort,
    /// <summary>
    /// 
    /// </summary>
    [Description("scroll")]
    Scroll
}
