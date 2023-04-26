// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using System.ComponentModel;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace BootstrapBlazor.Services;

/// <summary>
/// <inheritdoc/>
/// </summary>
[JSModuleAutoLoader("event-handler", JSObjectReference = true)]
public class BootstrapBlazorHelper : BootstrapModuleComponentBase, IBootstrapBlazorHelper
{
    /// <inheritdoc/>
    public async Task RegisterEvent(BootStrapBlazorEventType eventType) => await InvokeVoidAsync("registerEvent", Interop, eventType, $"JSInvokOn{eventType}");

    /// <inheritdoc/>
    public async Task RegisterEvent(BootStrapBlazorEventType eventType, string Id) => await InvokeVoidAsync("registerEvent", Interop, eventType, $"JSInvokOn{eventType}", Id);

    /// <inheritdoc/>
    public async Task RegisterEvent(BootStrapBlazorEventType eventType, ElementReference element) => await InvokeVoidAsync("registerEvent", Interop, eventType, $"JSInvokOn{eventType}", null, element);

    /// <inheritdoc/>
    public async Task<T> GetIdPropertieByNameAsync<T>(string id, string tag) => await Module.InvokeAsync<T>("getIdPropertieByName", id, tag);

    /// <inheritdoc/>
    public async Task<T> GetDocumentPropertieByNameAsync<T>(string tag) => await Module.InvokeAsync<T>("getDocumentPropertieByName", tag);
    /// <inheritdoc/>
    public async Task<T> GetElementPropertieByNameAsync<T>(ElementReference element, string tag) => await Module.InvokeAsync<T>("getElementPropertieByName", element, tag);

    #region JSInvok

    /// <summary>
    /// OnClick JS 回调
    /// </summary>
    [JSInvokable]
    public void JSInvokOnClick() => OnClick?.Invoke();

    /// <summary>
    /// OnDblclick JS 回调
    /// </summary>
    [JSInvokable]
    public void JSInvokOnDblclick() => OnDblclick?.Invoke();

    /// <summary>
    /// OnMouseup JS 回调
    /// </summary>
    [JSInvokable]
    public void JSInvokOnMouseup() => OnMouseup?.Invoke();

    /// <summary>
    /// OnMousedown JS 回调
    /// </summary>
    [JSInvokable]
    public void JSInvokOnMousedown() => OnMousedown?.Invoke();

    /// <summary>
    /// OnContextmenu JS 回调
    /// </summary>
    [JSInvokable]
    public void JSInvokOnContextmenu() => OnContextmenu?.Invoke();

    /// <summary>
    /// OnMousewheel JS 回调
    /// </summary>
    [JSInvokable]
    public void JSInvokOnMousewheel() => OnMousewheel?.Invoke();

    /// <summary>
    /// OnDOMMouseScroll JS 回调
    /// </summary>
    [JSInvokable]
    public void JSInvokOnDOMMouseScroll() => OnDOMMouseScroll?.Invoke();

    /// <summary>
    /// OnMouseover JS 回调
    /// </summary>
    [JSInvokable]
    public void JSInvokOnMouseover() => OnMouseover?.Invoke();

    /// <summary>
    /// OnMouseout JS 回调
    /// </summary>
    [JSInvokable]
    public void JSInvokOnMouseout() => OnMouseout?.Invoke();

    /// <summary>
    /// OnMousemove JS 回调
    /// </summary>
    [JSInvokable]
    public void JSInvokOnMousemove() => OnMousemove?.Invoke();

    /// <summary>
    /// OnSelectstart JS 回调
    /// </summary>
    [JSInvokable]
    public void JSInvokOnSelectstart() => OnSelectstart?.Invoke();

    /// <summary>
    /// OnSelectend JS 回调
    /// </summary>
    [JSInvokable]
    public void JSInvokOnSelectend() => OnSelectend?.Invoke();

    /// <summary>
    /// OnKeydown JS 回调
    /// </summary>
    [JSInvokable]
    public void JSInvokOnKeydown() => OnKeydown?.Invoke();

    /// <summary>
    /// OnKeypress JS 回调
    /// </summary>
    [JSInvokable]
    public void JSInvokOnKeypress() => OnKeypress?.Invoke();

    /// <summary>
    /// OnKeyup JS 回调
    /// </summary>
    [JSInvokable]
    public void JSInvokOnKeyup() => OnKeyup?.Invoke();

    /// <summary>
    /// OnOrientationchange JS 回调
    /// </summary>
    [JSInvokable]
    public void JSInvokOnOrientationchange() => OnOrientationchange?.Invoke();

    /// <summary>
    /// OnTouchstart JS 回调
    /// </summary>
    [JSInvokable]
    public void JSInvokOnTouchstart() => OnTouchstart?.Invoke();

    /// <summary>
    /// OnTouchmove JS 回调
    /// </summary>
    [JSInvokable]
    public void JSInvokOnTouchmove() => OnTouchmove?.Invoke();

    /// <summary>
    /// OnTouchend JS 回调
    /// </summary>
    [JSInvokable]
    public void JSInvokOnTouchend() => OnTouchend?.Invoke();

    /// <summary>
    /// OnTouchcancel JS 回调
    /// </summary>
    [JSInvokable]
    public void JSInvokOnTouchcancel() => OnTouchcancel?.Invoke();

    /// <summary>
    /// OnPointerdown JS 回调
    /// </summary>
    [JSInvokable]
    public void JSInvokOnPointerdown() => OnPointerdown?.Invoke();

    /// <summary>
    /// OnPointermove JS 回调
    /// </summary>
    [JSInvokable]
    public void JSInvokOnPointermove() => OnPointermove?.Invoke();

    /// <summary>
    /// OnPointerup JS 回调
    /// </summary>
    [JSInvokable]
    public void JSInvokOnPointerup() => OnPointerup?.Invoke();

    /// <summary>
    /// OnPointerleave JS 回调
    /// </summary>
    [JSInvokable]
    public void JSInvokOnPointerleave() => OnPointerleave?.Invoke();

    /// <summary>
    /// OnPointercancel JS 回调
    /// </summary>
    [JSInvokable]
    public void JSInvokOnPointercancel() => OnPointercancel?.Invoke();

    /// <summary>
    /// OnGesturestart JS 回调
    /// </summary>
    [JSInvokable]
    public void JSInvokOnGesturestart() => OnGesturestart?.Invoke();

    /// <summary>
    /// OnGesturechange JS 回调
    /// </summary>
    [JSInvokable]
    public void JSInvokOnGesturechange() => OnGesturechange?.Invoke();

    /// <summary>
    /// OnGestureend JS 回调
    /// </summary>
    [JSInvokable]
    public void JSInvokOnGestureend() => OnGestureend?.Invoke();

    /// <summary>
    /// OnFocus JS 回调
    /// </summary>
    [JSInvokable]
    public void JSInvokOnFocus() => OnFocus?.Invoke();

    /// <summary>
    /// OnBlur JS 回调
    /// </summary>
    [JSInvokable]
    public void JSInvokOnBlur() => OnBlur?.Invoke();

    /// <summary>
    /// OnChange JS 回调
    /// </summary>
    [JSInvokable]
    public void JSInvokOnChange() => OnChange?.Invoke();

    /// <summary>
    /// OnReset JS 回调
    /// </summary>
    [JSInvokable]
    public void JSInvokOnReset() => OnReset?.Invoke();

    /// <summary>
    /// OnSelect JS 回调
    /// </summary>
    [JSInvokable]
    public void JSInvokOnSelect() => OnSelect?.Invoke();

    /// <summary>
    /// OnSubmit JS 回调
    /// </summary>
    [JSInvokable]
    public void JSInvokOnSubmit() => OnSubmit?.Invoke();

    /// <summary>
    /// OnFocusin JS 回调
    /// </summary>
    [JSInvokable]
    public void JSInvokOnFocusin() => OnFocusin?.Invoke();

    /// <summary>
    /// OnFocusout JS 回调
    /// </summary>
    [JSInvokable]
    public void JSInvokOnFocusout() => OnFocusout?.Invoke();

    /// <summary>
    /// OnLoad JS 回调
    /// </summary>
    [JSInvokable]
    public void JSInvokOnLoad() => OnLoad?.Invoke();

    /// <summary>
    /// OnUnload JS 回调
    /// </summary>
    [JSInvokable]
    public void JSInvokOnUnload() => OnUnload?.Invoke();

    /// <summary>
    /// OnBeforeunload JS 回调
    /// </summary>
    [JSInvokable]
    public void JSInvokOnBeforeunload() => OnBeforeunload?.Invoke();

    /// <summary>
    /// OnResize JS 回调
    /// </summary>
    [JSInvokable]
    public void JSInvokOnResize() => OnResize?.Invoke();

    /// <summary>
    /// OnMove JS 回调
    /// </summary>
    [JSInvokable]
    public void JSInvokOnMove() => OnMove?.Invoke();

    /// <summary>
    /// OnDOMContentLoaded JS 回调
    /// </summary>
    [JSInvokable]
    public void JSInvokOnDOMContentLoaded() => OnDOMContentLoaded?.Invoke();

    /// <summary>
    /// OnReadystatechange JS 回调
    /// </summary>
    [JSInvokable]
    public void JSInvokOnReadystatechange() => OnReadystatechange?.Invoke();

    /// <summary>
    /// OnError JS 回调
    /// </summary>
    [JSInvokable]
    public void JSInvokOnError() => OnError?.Invoke();

    /// <summary>
    /// OnAbort JS 回调
    /// </summary>
    [JSInvokable]
    public void JSInvokOnAbort() => OnAbort?.Invoke();

    /// <summary>
    /// OnScroll JS 回调
    /// </summary>
    [JSInvokable]
    public void JSInvokOnScroll() => OnScroll?.Invoke();

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
/// 事件枚举
/// </summary>
[JsonConverter(typeof(EventHandlesConverter))]
public enum BootStrapBlazorEventType
{
    /// <summary>
    /// Click 事件枚举
    /// </summary>
    [Description("click")]
    Click,
    /// <summary>
    /// Dblclick 事件枚举
    /// </summary>
    [Description("dblclick")]
    Dblclick,
    /// <summary>
    /// Mouseup 事件枚举
    /// </summary>
    [Description("mouseup")]
    Mouseup,
    /// <summary>
    /// Mousedown 事件枚举
    /// </summary>
    [Description("mousedown")]
    Mousedown,
    /// <summary>
    /// Contextmenu 事件枚举
    /// </summary>
    [Description("contextmenu")]
    Contextmenu,
    /// <summary>
    /// Mousewheel 事件枚举
    /// </summary>
    [Description("mousewheel")]
    Mousewheel,
    /// <summary>
    /// DOMMouseScroll 事件枚举
    /// </summary>
    [Description("dOMMouseScroll")]
    DOMMouseScroll,
    /// <summary>
    /// Mouseover 事件枚举
    /// </summary>
    [Description("mouseover")]
    Mouseover,
    /// <summary>
    /// Mouseout 事件枚举
    /// </summary>
    [Description("mouseout")]
    Mouseout,
    /// <summary>
    /// Mousemove 事件枚举
    /// </summary>
    [Description("mousemove")]
    Mousemove,
    /// <summary>
    /// Selectstart 事件枚举
    /// </summary>
    [Description("selectstart")]
    Selectstart,
    /// <summary>
    /// Selectend 事件枚举
    /// </summary>
    [Description("selectend")]
    Selectend,
    /// <summary>
    /// Keydown 事件枚举
    /// </summary>
    [Description("keydown")]
    Keydown,
    /// <summary>
    /// Keypress 事件枚举
    /// </summary>
    [Description("keypress")]
    Keypress,
    /// <summary>
    /// Keyup 事件枚举
    /// </summary>
    [Description("keyup")]
    Keyup,
    /// <summary>
    /// Orientationchange 事件枚举
    /// </summary>
    [Description("orientationchange")]
    Orientationchange,
    /// <summary>
    /// Touchstart 事件枚举
    /// </summary>
    [Description("touchstart")]
    Touchstart,
    /// <summary>
    /// Touchmove 事件枚举
    /// </summary>
    [Description("touchmove")]
    Touchmove,
    /// <summary>
    /// Touchend 事件枚举
    /// </summary>
    [Description("touchend")]
    Touchend,
    /// <summary>
    /// Touchcancel 事件枚举
    /// </summary>
    [Description("touchcancel")]
    Touchcancel,
    /// <summary>
    /// Pointerdown 事件枚举
    /// </summary>
    [Description("pointerdown")]
    Pointerdown,
    /// <summary>
    /// Pointermove 事件枚举
    /// </summary>
    [Description("pointermove")]
    Pointermove,
    /// <summary>
    /// Pointerup 事件枚举
    /// </summary>
    [Description("pointerup")]
    Pointerup,
    /// <summary>
    /// Pointerleave 事件枚举
    /// </summary>
    [Description("pointerleave")]
    Pointerleave,
    /// <summary>
    /// Pointercancel 事件枚举
    /// </summary>
    [Description("pointercancel")]
    Pointercancel,
    /// <summary>
    /// Gesturestart 事件枚举
    /// </summary>
    [Description("gesturestart")]
    Gesturestart,
    /// <summary>
    /// Gesturechange 事件枚举
    /// </summary>
    [Description("gesturechange")]
    Gesturechange,
    /// <summary>
    /// Gestureend 事件枚举
    /// </summary>
    [Description("gestureend")]
    Gestureend,
    /// <summary>
    /// Focus 事件枚举
    /// </summary>
    [Description("focus")]
    Focus,
    /// <summary>
    /// Blur 事件枚举
    /// </summary>
    [Description("blur")]
    Blur,
    /// <summary>
    /// Change 事件枚举
    /// </summary>
    [Description("change")]
    Change,
    /// <summary>
    /// Reset 事件枚举
    /// </summary>
    [Description("reset")]
    Reset,
    /// <summary>
    /// Select 事件枚举
    /// </summary>
    [Description("select")]
    Select,
    /// <summary>
    /// Submit 事件枚举
    /// </summary>
    [Description("submit")]
    Submit,
    /// <summary>
    /// Focusin 事件枚举
    /// </summary>
    [Description("focusin")]
    Focusin,
    /// <summary>
    /// Focusout 事件枚举
    /// </summary>
    [Description("focusout")]
    Focusout,
    /// <summary>
    /// Load 事件枚举
    /// </summary>
    [Description("load")]
    Load,
    /// <summary>
    /// Unload 事件枚举
    /// </summary>
    [Description("unload")]
    Unload,
    /// <summary>
    /// Beforeunload 事件枚举
    /// </summary>
    [Description("beforeunload")]
    Beforeunload,
    /// <summary>
    /// Resize 事件枚举
    /// </summary>
    [Description("resize")]
    Resize,
    /// <summary>
    /// Move 事件枚举
    /// </summary>
    [Description("move")]
    Move,
    /// <summary>
    /// DOMContentLoaded 事件枚举
    /// </summary>
    [Description("dOMContentLoaded")]
    DOMContentLoaded,
    /// <summary>
    /// Readystatechange 事件枚举
    /// </summary>
    [Description("readystatechange")]
    Readystatechange,
    /// <summary>
    /// Error 事件枚举
    /// </summary>
    [Description("error")]
    Error,
    /// <summary>
    /// Abort 事件枚举
    /// </summary>
    [Description("abort")]
    Abort,
    /// <summary>
    /// Scroll 事件枚举
    /// </summary>
    [Description("scroll")]
    Scroll
}
