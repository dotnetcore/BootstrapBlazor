// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using System.ComponentModel;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace BootstrapBlazor.Services;

public class BootstrapBlazorHelper : IBootstrapBlazorHelper
{
    private IJSRuntime JSRuntime { get; set; }

    [NotNull]
    private IJSObjectReference? Module { get; set; }

    [NotNull]
    private DotNetObjectReference<BootstrapBlazorHelper>? Interop { get; set; }

    public BootstrapBlazorHelper(IJSRuntime jSRuntime)
    {
        JSRuntime = jSRuntime;
    }

    public async Task RegisterEvent(BootStrapBlazorEventType handles, string? Id)
    {
        Module = await JSRuntime.InvokeAsync<IJSObjectReference>("import", "./_content/BootstrapBlazor/modules/event-handler.js");
        Interop = DotNetObjectReference.Create(this);
        var methodName = $"JSInvokOn{handles}";
        await Module.InvokeVoidAsync("registerEvent", Interop, handles, methodName, Id);
    }

    #region JSInvok
    [JSInvokable] public void JSInvokOnClick(BootStrapBlazorEventArgs args) => OnClick.Invoke(this, args);
    [JSInvokable] public void JSInvokOnDblclick(BootStrapBlazorEventArgs args) => OnDblclick?.Invoke(this, args);
    [JSInvokable] public void JSInvokOnMouseup(BootStrapBlazorEventArgs args) => OnMouseup?.Invoke(this, args);
    [JSInvokable] public void JSInvokOnMousedown(BootStrapBlazorEventArgs args) => OnMousedown?.Invoke(this, args);
    [JSInvokable] public void JSInvokOnContextmenu(BootStrapBlazorEventArgs args) => OnContextmenu?.Invoke(this, args);
    [JSInvokable] public void JSInvokOnMousewheel(BootStrapBlazorEventArgs args) => OnMousewheel?.Invoke(this, args);
    [JSInvokable] public void JSInvokOnDOMMouseScroll(BootStrapBlazorEventArgs args) => OnDOMMouseScroll?.Invoke(this, args);
    [JSInvokable] public void JSInvokOnMouseover(BootStrapBlazorEventArgs args) => OnMouseover?.Invoke(this, args);
    [JSInvokable] public void JSInvokOnMouseout(BootStrapBlazorEventArgs args) => OnMouseout?.Invoke(this, args);
    [JSInvokable] public void JSInvokOnMousemove(BootStrapBlazorEventArgs args) => OnMousemove?.Invoke(this, args);
    [JSInvokable] public void JSInvokOnSelectstart(BootStrapBlazorEventArgs args) => OnSelectstart?.Invoke(this, args);
    [JSInvokable] public void JSInvokOnSelectend(BootStrapBlazorEventArgs args) => OnSelectend?.Invoke(this, args);
    [JSInvokable] public void JSInvokOnKeydown(BootStrapBlazorEventArgs args) => OnKeydown?.Invoke(this, args);
    [JSInvokable] public void JSInvokOnKeypress(BootStrapBlazorEventArgs args) => OnKeypress?.Invoke(this, args);
    [JSInvokable] public void JSInvokOnKeyup(BootStrapBlazorEventArgs args) => OnKeyup?.Invoke(this, args);
    [JSInvokable] public void JSInvokOnOrientationchange(BootStrapBlazorEventArgs args) => OnOrientationchange?.Invoke(this, args);
    [JSInvokable] public void JSInvokOnTouchstart(BootStrapBlazorEventArgs args) => OnTouchstart?.Invoke(this, args);
    [JSInvokable] public void JSInvokOnTouchmove(BootStrapBlazorEventArgs args) => OnTouchmove?.Invoke(this, args);
    [JSInvokable] public void JSInvokOnTouchend(BootStrapBlazorEventArgs args) => OnTouchend?.Invoke(this, args);
    [JSInvokable] public void JSInvokOnTouchcancel(BootStrapBlazorEventArgs args) => OnTouchcancel?.Invoke(this, args);
    [JSInvokable] public void JSInvokOnPointerdown(BootStrapBlazorEventArgs args) => OnPointerdown?.Invoke(this, args);
    [JSInvokable] public void JSInvokOnPointermove(BootStrapBlazorEventArgs args) => OnPointermove?.Invoke(this, args);
    [JSInvokable] public void JSInvokOnPointerup(BootStrapBlazorEventArgs args) => OnPointerup?.Invoke(this, args);
    [JSInvokable] public void JSInvokOnPointerleave(BootStrapBlazorEventArgs args) => OnPointerleave?.Invoke(this, args);
    [JSInvokable] public void JSInvokOnPointercancel(BootStrapBlazorEventArgs args) => OnPointercancel?.Invoke(this, args);
    [JSInvokable] public void JSInvokOnGesturestart(BootStrapBlazorEventArgs args) => OnGesturestart?.Invoke(this, args);
    [JSInvokable] public void JSInvokOnGesturechange(BootStrapBlazorEventArgs args) => OnGesturechange?.Invoke(this, args);
    [JSInvokable] public void JSInvokOnGestureend(BootStrapBlazorEventArgs args) => OnGestureend?.Invoke(this, args);
    [JSInvokable] public void JSInvokOnFocus(BootStrapBlazorEventArgs args) => OnFocus?.Invoke(this, args);
    [JSInvokable] public void JSInvokOnBlur(BootStrapBlazorEventArgs args) => OnBlur?.Invoke(this, args);
    [JSInvokable] public void JSInvokOnChange(BootStrapBlazorEventArgs args) => OnChange?.Invoke(this, args);
    [JSInvokable] public void JSInvokOnReset(BootStrapBlazorEventArgs args) => OnReset?.Invoke(this, args);
    [JSInvokable] public void JSInvokOnSelect(BootStrapBlazorEventArgs args) => OnSelect?.Invoke(this, args);
    [JSInvokable] public void JSInvokOnSubmit(BootStrapBlazorEventArgs args) => OnSubmit?.Invoke(this, args);
    [JSInvokable] public void JSInvokOnFocusin(BootStrapBlazorEventArgs args) => OnFocusin?.Invoke(this, args);
    [JSInvokable] public void JSInvokOnFocusout(BootStrapBlazorEventArgs args) => OnFocusout?.Invoke(this, args);
    [JSInvokable] public void JSInvokOnLoad(BootStrapBlazorEventArgs args) => OnLoad?.Invoke(this, args);
    [JSInvokable] public void JSInvokOnUnload(BootStrapBlazorEventArgs args) => OnUnload?.Invoke(this, args);
    [JSInvokable] public void JSInvokOnBeforeunload(BootStrapBlazorEventArgs args) => OnBeforeunload?.Invoke(this, args);
    [JSInvokable] public void JSInvokOnResize(BootStrapBlazorEventArgs args) => OnResize?.Invoke(this, args);
    [JSInvokable] public void JSInvokOnMove(BootStrapBlazorEventArgs args) => OnMove?.Invoke(this, args);
    [JSInvokable] public void JSInvokOnDOMContentLoaded(BootStrapBlazorEventArgs args) => OnDOMContentLoaded?.Invoke(this, args);
    [JSInvokable] public void JSInvokOnReadystatechange(BootStrapBlazorEventArgs args) => OnReadystatechange?.Invoke(this, args);
    [JSInvokable] public void JSInvokOnError(BootStrapBlazorEventArgs args) => OnError?.Invoke(this, args);
    [JSInvokable] public void JSInvokOnAbort(BootStrapBlazorEventArgs args) => OnAbort?.Invoke(this, args);
    [JSInvokable] public void JSInvokOnScroll(BootStrapBlazorEventArgs args) => OnScroll?.Invoke(this, args);

    #endregion

    #region Event
    public event EventHandler<BootStrapBlazorEventArgs> OnClick;
    public event EventHandler<BootStrapBlazorEventArgs> OnDblclick;
    public event EventHandler<BootStrapBlazorEventArgs> OnMouseup;
    public event EventHandler<BootStrapBlazorEventArgs> OnMousedown;
    public event EventHandler<BootStrapBlazorEventArgs> OnContextmenu;
    public event EventHandler<BootStrapBlazorEventArgs> OnMousewheel;
    public event EventHandler<BootStrapBlazorEventArgs> OnDOMMouseScroll;
    public event EventHandler<BootStrapBlazorEventArgs> OnMouseover;
    public event EventHandler<BootStrapBlazorEventArgs> OnMouseout;
    public event EventHandler<BootStrapBlazorEventArgs> OnMousemove;
    public event EventHandler<BootStrapBlazorEventArgs> OnSelectstart;
    public event EventHandler<BootStrapBlazorEventArgs> OnSelectend;
    public event EventHandler<BootStrapBlazorEventArgs> OnKeydown;
    public event EventHandler<BootStrapBlazorEventArgs> OnKeypress;
    public event EventHandler<BootStrapBlazorEventArgs> OnKeyup;
    public event EventHandler<BootStrapBlazorEventArgs> OnOrientationchange;
    public event EventHandler<BootStrapBlazorEventArgs> OnTouchstart;
    public event EventHandler<BootStrapBlazorEventArgs> OnTouchmove;
    public event EventHandler<BootStrapBlazorEventArgs> OnTouchend;
    public event EventHandler<BootStrapBlazorEventArgs> OnTouchcancel;
    public event EventHandler<BootStrapBlazorEventArgs> OnPointerdown;
    public event EventHandler<BootStrapBlazorEventArgs> OnPointermove;
    public event EventHandler<BootStrapBlazorEventArgs> OnPointerup;
    public event EventHandler<BootStrapBlazorEventArgs> OnPointerleave;
    public event EventHandler<BootStrapBlazorEventArgs> OnPointercancel;
    public event EventHandler<BootStrapBlazorEventArgs> OnGesturestart;
    public event EventHandler<BootStrapBlazorEventArgs> OnGesturechange;
    public event EventHandler<BootStrapBlazorEventArgs> OnGestureend;
    public event EventHandler<BootStrapBlazorEventArgs> OnFocus;
    public event EventHandler<BootStrapBlazorEventArgs> OnBlur;
    public event EventHandler<BootStrapBlazorEventArgs> OnChange;
    public event EventHandler<BootStrapBlazorEventArgs> OnReset;
    public event EventHandler<BootStrapBlazorEventArgs> OnSelect;
    public event EventHandler<BootStrapBlazorEventArgs> OnSubmit;
    public event EventHandler<BootStrapBlazorEventArgs> OnFocusin;
    public event EventHandler<BootStrapBlazorEventArgs> OnFocusout;
    public event EventHandler<BootStrapBlazorEventArgs> OnLoad;
    public event EventHandler<BootStrapBlazorEventArgs> OnUnload;
    public event EventHandler<BootStrapBlazorEventArgs> OnBeforeunload;
    public event EventHandler<BootStrapBlazorEventArgs> OnResize;
    public event EventHandler<BootStrapBlazorEventArgs> OnMove;
    public event EventHandler<BootStrapBlazorEventArgs> OnDOMContentLoaded;
    public event EventHandler<BootStrapBlazorEventArgs> OnReadystatechange;
    public event EventHandler<BootStrapBlazorEventArgs> OnError;
    public event EventHandler<BootStrapBlazorEventArgs> OnAbort;
    public event EventHandler<BootStrapBlazorEventArgs> OnScroll;

    #endregion
}

public class BootStrapBlazorEventArgs : EventArgs
{
    public bool IsTrusted { get; set; }
    public bool Bubbles { get; set; }
    public bool Buttons { get; set; }
    public bool CancelBubble { get; set; }
    public bool Cancelable { get; set; }
    public bool Composed { get; set; }
    public bool DefaultPrevented { get; set; }
    public long EventPhase { get; set; }
    public bool ReturnValue { get; set; }
    public double TimeStamp { get; set; }
    public string Type { get; set; }
}

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
