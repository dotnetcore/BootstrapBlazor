// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using System.ComponentModel;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace BootstrapBlazor.Services;

public class BootstrapBlazorHelper
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


    private async void RegisterEvent(EventHandles handles)
    {
        Module = await JSRuntime.InvokeAsync<IJSObjectReference>("import", "./_content/BootstrapBlazor/modules/event-handler.js");
        Interop = DotNetObjectReference.Create(this);
        var methodName = $"JSInvokOn{handles}";
        await Module.InvokeVoidAsync("registerEvent", Interop, handles, methodName);
    }

    #region JSInvok
    /// <summary>
    /// 
    /// </summary>
    [JSInvokable]
    public void JSInvokOnClick() => _onClick.Invoke();

    /// <summary>
    /// 
    /// </summary>
    [JSInvokable]
    public void JSInvokOnDblclick() => _onDblclick?.Invoke();

    /// <summary>
    /// 
    /// </summary>
    [JSInvokable]
    public void JSInvokOnMouseup() => _onMouseup?.Invoke();

    /// <summary>
    /// 
    /// </summary>
    [JSInvokable]
    public void JSInvokOnMousedown() => _onMousedown?.Invoke();

    /// <summary>
    /// 
    /// </summary>
    [JSInvokable]
    public void JSInvokOnContextmenu() => _onContextmenu?.Invoke();

    /// <summary>
    /// 
    /// </summary>
    [JSInvokable]
    public void JSInvokOnMousewheel() => _onMousewheel?.Invoke();

    /// <summary>
    /// 
    /// </summary>
    [JSInvokable]
    public void JSInvokOnDOMMouseScroll() => _onDOMMouseScroll?.Invoke();

    /// <summary>
    /// 
    /// </summary>
    [JSInvokable]
    public void JSInvokOnMouseover() => _onMouseover?.Invoke();

    /// <summary>
    /// 
    /// </summary>
    [JSInvokable]
    public void JSInvokOnMouseout() => _onMouseout?.Invoke();

    /// <summary>
    /// 
    /// </summary>
    [JSInvokable]
    public void JSInvokOnMousemove() => _onMousemove?.Invoke();

    /// <summary>
    /// 
    /// </summary>
    [JSInvokable]
    public void JSInvokOnSelectstart() => _onSelectstart?.Invoke();

    /// <summary>
    /// 
    /// </summary>
    [JSInvokable]
    public void JSInvokOnSelectend() => _onSelectend?.Invoke();

    /// <summary>
    /// 
    /// </summary>
    [JSInvokable]
    public void JSInvokOnKeydown() => _onKeydown?.Invoke();

    /// <summary>
    /// 
    /// </summary>
    [JSInvokable]
    public void JSInvokOnKeypress() => _onKeypress?.Invoke();

    /// <summary>
    /// 
    /// </summary>
    [JSInvokable]
    public void JSInvokOnKeyup() => _onKeyup?.Invoke();

    /// <summary>
    /// 
    /// </summary>
    [JSInvokable]
    public void JSInvokOnOrientationchange() => _onOrientationchange?.Invoke();

    /// <summary>
    /// 
    /// </summary>
    [JSInvokable]
    public void JSInvokOnTouchstart() => _onTouchstart?.Invoke();

    /// <summary>
    /// 
    /// </summary>
    [JSInvokable]
    public void JSInvokOnTouchmove() => _onTouchmove?.Invoke();

    /// <summary>
    /// 
    /// </summary>
    [JSInvokable]
    public void JSInvokOnTouchend() => _onTouchend?.Invoke();

    /// <summary>
    /// 
    /// </summary>
    [JSInvokable]
    public void JSInvokOnTouchcancel() => _onTouchcancel?.Invoke();

    /// <summary>
    /// 
    /// </summary>
    [JSInvokable]
    public void JSInvokOnPointerdown() => _onPointerdown?.Invoke();

    /// <summary>
    /// 
    /// </summary>
    [JSInvokable]
    public void JSInvokOnPointermove() => _onPointermove?.Invoke();

    /// <summary>
    /// 
    /// </summary>
    [JSInvokable]
    public void JSInvokOnPointerup() => _onPointerup?.Invoke();

    /// <summary>
    /// 
    /// </summary>
    [JSInvokable]
    public void JSInvokOnPointerleave() => _onPointerleave?.Invoke();

    /// <summary>
    /// 
    /// </summary>
    [JSInvokable]
    public void JSInvokOnPointercancel() => _onPointercancel?.Invoke();

    /// <summary>
    /// 
    /// </summary>
    [JSInvokable]
    public void JSInvokOnGesturestart() => _onGesturestart?.Invoke();

    /// <summary>
    /// 
    /// </summary>
    [JSInvokable]
    public void JSInvokOnGesturechange() => _onGesturechange?.Invoke();

    /// <summary>
    /// 
    /// </summary>
    [JSInvokable]
    public void JSInvokOnGestureend() => _onGestureend?.Invoke();

    /// <summary>
    /// 
    /// </summary>
    [JSInvokable]
    public void JSInvokOnFocus() => _onFocus?.Invoke();

    /// <summary>
    /// 
    /// </summary>
    [JSInvokable]
    public void JSInvokOnBlur() => _onBlur?.Invoke();

    /// <summary>
    /// 
    /// </summary>
    [JSInvokable]
    public void JSInvokOnChange() => _onChange?.Invoke();

    /// <summary>
    /// 
    /// </summary>
    [JSInvokable]
    public void JSInvokOnReset() => _onReset?.Invoke();

    /// <summary>
    /// 
    /// </summary>
    [JSInvokable]
    public void JSInvokOnSelect() => _onSelect?.Invoke();

    /// <summary>
    /// 
    /// </summary>
    [JSInvokable]
    public void JSInvokOnSubmit() => _onSubmit?.Invoke();

    /// <summary>
    /// 
    /// </summary>
    [JSInvokable]
    public void JSInvokOnFocusin() => _onFocusin?.Invoke();

    /// <summary>
    /// 
    /// </summary>
    [JSInvokable]
    public void JSInvokOnFocusout() => _onFocusout?.Invoke();

    /// <summary>
    /// 
    /// </summary>
    [JSInvokable]
    public void JSInvokOnLoad() => _onLoad?.Invoke();

    /// <summary>
    /// 
    /// </summary>
    [JSInvokable]
    public void JSInvokOnUnload() => _onUnload?.Invoke();

    /// <summary>
    /// 
    /// </summary>
    [JSInvokable]
    public void JSInvokOnBeforeunload() => _onBeforeunload?.Invoke();

    /// <summary>
    /// 
    /// </summary>
    [JSInvokable]
    public void JSInvokOnResize() => _onResize?.Invoke();

    /// <summary>
    /// 
    /// </summary>
    [JSInvokable]
    public void JSInvokOnMove() => _onMove?.Invoke();

    /// <summary>
    /// 
    /// </summary>
    [JSInvokable]
    public void JSInvokOnDOMContentLoaded() => _onDOMContentLoaded?.Invoke();

    /// <summary>
    /// 
    /// </summary>
    [JSInvokable]
    public void JSInvokOnReadystatechange() => _onReadystatechange?.Invoke();

    /// <summary>
    /// 
    /// </summary>
    [JSInvokable]
    public void JSInvokOnError() => _onError?.Invoke();

    /// <summary>
    /// 
    /// </summary>
    [JSInvokable]
    public void JSInvokOnAbort() => _onAbort?.Invoke();

    /// <summary>
    /// 
    /// </summary>
    [JSInvokable]
    public void JSInvokOnScroll() => _onScroll?.Invoke();


    #endregion

    #region Event

    private event BootStrapBlazorEventHandler _onClick;

    public event BootStrapBlazorEventHandler OnClick
    {
        add
        {
            _onClick += value;
            RegisterEvent(EventHandles.Click);
        }
        remove { _onClick -= value; }
    }

    private event BootStrapBlazorEventHandler _onDblclick;

    public event BootStrapBlazorEventHandler OnDblclick
    {
        add
        {
            _onDblclick += value;
            RegisterEvent(EventHandles.Dblclick);
        }
        remove { _onDblclick -= value; }
    }

    private event BootStrapBlazorEventHandler _onMouseup;

    public event BootStrapBlazorEventHandler OnMouseup
    {
        add
        {
            _onMouseup += value;
            RegisterEvent(EventHandles.Mouseup);
        }
        remove { _onMouseup -= value; }
    }

    private event BootStrapBlazorEventHandler _onMousedown;

    public event BootStrapBlazorEventHandler OnMousedown
    {
        add
        {
            _onMousedown += value;
            RegisterEvent(EventHandles.Mousedown);
        }
        remove { _onMousedown -= value; }
    }

    private event BootStrapBlazorEventHandler _onContextmenu;

    public event BootStrapBlazorEventHandler OnCOntextmenu
    {
        add
        {
            _onContextmenu += value;
            RegisterEvent(EventHandles.Contextmenu);
        }
        remove { _onContextmenu -= value; }
    }

    private event BootStrapBlazorEventHandler _onMousewheel;

    public event BootStrapBlazorEventHandler OnMousewheel
    {
        add
        {
            _onMousewheel += value;
            RegisterEvent(EventHandles.Mousewheel);
        }
        remove { _onMousewheel -= value; }
    }

    private event BootStrapBlazorEventHandler _onDOMMouseScroll;

    public event BootStrapBlazorEventHandler OnDOMMouseScroll
    {
        add
        {
            _onDOMMouseScroll += value;
            RegisterEvent(EventHandles.DOMMouseScroll);
        }
        remove { _onDOMMouseScroll -= value; }
    }

    private event BootStrapBlazorEventHandler _onMouseover;

    public event BootStrapBlazorEventHandler OnMouseover
    {
        add
        {
            _onMouseover += value;
            RegisterEvent(EventHandles.Mouseover);
        }
        remove { _onMouseover -= value; }
    }

    private event BootStrapBlazorEventHandler _onMouseout;

    public event BootStrapBlazorEventHandler OnMouseout
    {
        add
        {
            _onMouseout += value;
            RegisterEvent(EventHandles.Mouseout);
        }
        remove { _onMouseout -= value; }
    }

    private event BootStrapBlazorEventHandler _onMousemove;

    public event BootStrapBlazorEventHandler OnMousemove
    {
        add
        {
            _onMousemove += value;
            RegisterEvent(EventHandles.Mousemove);
        }
        remove { _onMousemove -= value; }
    }

    private event BootStrapBlazorEventHandler _onSelectstart;

    public event BootStrapBlazorEventHandler OnSelectstart
    {
        add
        {
            _onSelectstart += value;
            RegisterEvent(EventHandles.Selectstart);
        }
        remove { _onSelectstart -= value; }
    }

    private event BootStrapBlazorEventHandler _onSelectend;

    public event BootStrapBlazorEventHandler OnSelectend
    {
        add
        {
            _onSelectend += value;
            RegisterEvent(EventHandles.Selectend);
        }
        remove { _onSelectend -= value; }
    }

    private event BootStrapBlazorEventHandler _onKeydown;

    public event BootStrapBlazorEventHandler OnKeydown
    {
        add
        {
            _onKeydown += value;
            RegisterEvent(EventHandles.Keydown);
        }
        remove { _onKeydown -= value; }
    }

    private event BootStrapBlazorEventHandler _onKeypress;

    public event BootStrapBlazorEventHandler OnKeypress
    {
        add
        {
            _onKeypress += value;
            RegisterEvent(EventHandles.Keypress);
        }
        remove { _onKeypress -= value; }
    }

    private event BootStrapBlazorEventHandler _onKeyup;

    public event BootStrapBlazorEventHandler OnKeyup
    {
        add
        {
            _onKeyup += value;
            RegisterEvent(EventHandles.Keyup);
        }
        remove { _onKeyup -= value; }
    }

    private event BootStrapBlazorEventHandler _onOrientationchange;

    public event BootStrapBlazorEventHandler OnOrientatiOnchange
    {
        add
        {
            _onOrientationchange += value;
            RegisterEvent(EventHandles.Orientationchange);
        }
        remove { _onOrientationchange -= value; }
    }

    private event BootStrapBlazorEventHandler _onTouchstart;

    public event BootStrapBlazorEventHandler OnTouchstart
    {
        add
        {
            _onTouchstart += value;
            RegisterEvent(EventHandles.Touchstart);
        }
        remove { _onTouchstart -= value; }
    }

    private event BootStrapBlazorEventHandler _onTouchmove;

    public event BootStrapBlazorEventHandler OnTouchmove
    {
        add
        {
            _onTouchmove += value;
            RegisterEvent(EventHandles.Touchmove);
        }
        remove { _onTouchmove -= value; }
    }

    private event BootStrapBlazorEventHandler _onTouchend;

    public event BootStrapBlazorEventHandler OnTouchend
    {
        add
        {
            _onTouchend += value;
            RegisterEvent(EventHandles.Touchend);
        }
        remove { _onTouchend -= value; }
    }

    private event BootStrapBlazorEventHandler _onTouchcancel;

    public event BootStrapBlazorEventHandler OnTouchcancel
    {
        add
        {
            _onTouchcancel += value;
            RegisterEvent(EventHandles.Touchcancel);
        }
        remove { _onTouchcancel -= value; }
    }

    private event BootStrapBlazorEventHandler _onPointerdown;

    public event BootStrapBlazorEventHandler OnPointerdown
    {
        add
        {
            _onPointerdown += value;
            RegisterEvent(EventHandles.Pointerdown);
        }
        remove { _onPointerdown -= value; }
    }

    private event BootStrapBlazorEventHandler _onPointermove;

    public event BootStrapBlazorEventHandler OnPointermove
    {
        add
        {
            _onPointermove += value;
            RegisterEvent(EventHandles.Pointermove);
        }
        remove { _onPointermove -= value; }
    }

    private event BootStrapBlazorEventHandler _onPointerup;

    public event BootStrapBlazorEventHandler OnPointerup
    {
        add
        {
            _onPointerup += value;
            RegisterEvent(EventHandles.Pointerup);
        }
        remove { _onPointerup -= value; }
    }

    private event BootStrapBlazorEventHandler _onPointerleave;

    public event BootStrapBlazorEventHandler OnPointerleave
    {
        add
        {
            _onPointerleave += value;
            RegisterEvent(EventHandles.Pointerleave);
        }
        remove { _onPointerleave -= value; }
    }

    private event BootStrapBlazorEventHandler _onPointercancel;

    public event BootStrapBlazorEventHandler OnPointercancel
    {
        add
        {
            _onPointercancel += value;
            RegisterEvent(EventHandles.Pointercancel);
        }
        remove { _onPointercancel -= value; }
    }

    private event BootStrapBlazorEventHandler _onGesturestart;

    public event BootStrapBlazorEventHandler OnGesturestart
    {
        add
        {
            _onGesturestart += value;
            RegisterEvent(EventHandles.Gesturestart);
        }
        remove { _onGesturestart -= value; }
    }

    private event BootStrapBlazorEventHandler _onGesturechange;

    public event BootStrapBlazorEventHandler OnGesturechange
    {
        add
        {
            _onGesturechange += value;
            RegisterEvent(EventHandles.Gesturechange);
        }
        remove { _onGesturechange -= value; }
    }

    private event BootStrapBlazorEventHandler _onGestureend;

    public event BootStrapBlazorEventHandler OnGestureend
    {
        add
        {
            _onGestureend += value;
            RegisterEvent(EventHandles.Gestureend);
        }
        remove { _onGestureend -= value; }
    }

    private event BootStrapBlazorEventHandler _onFocus;

    public event BootStrapBlazorEventHandler OnFocus
    {
        add
        {
            _onFocus += value;
            RegisterEvent(EventHandles.Focus);
        }
        remove { _onFocus -= value; }
    }

    private event BootStrapBlazorEventHandler _onBlur;

    public event BootStrapBlazorEventHandler OnBlur
    {
        add
        {
            _onBlur += value;
            RegisterEvent(EventHandles.Blur);
        }
        remove { _onBlur -= value; }
    }

    private event BootStrapBlazorEventHandler _onChange;

    public event BootStrapBlazorEventHandler OnChange
    {
        add
        {
            _onChange += value;
            RegisterEvent(EventHandles.Change);
        }
        remove { _onChange -= value; }
    }

    private event BootStrapBlazorEventHandler _onReset;

    public event BootStrapBlazorEventHandler OnReset
    {
        add
        {
            _onReset += value;
            RegisterEvent(EventHandles.Reset);
        }
        remove { _onReset -= value; }
    }

    private event BootStrapBlazorEventHandler _onSelect;

    public event BootStrapBlazorEventHandler OnSelect
    {
        add
        {
            _onSelect += value;
            RegisterEvent(EventHandles.Select);
        }
        remove { _onSelect -= value; }
    }

    private event BootStrapBlazorEventHandler _onSubmit;

    public event BootStrapBlazorEventHandler OnSubmit
    {
        add
        {
            _onSubmit += value;
            RegisterEvent(EventHandles.Submit);
        }
        remove { _onSubmit -= value; }
    }

    private event BootStrapBlazorEventHandler _onFocusin;

    public event BootStrapBlazorEventHandler OnFocusin
    {
        add
        {
            _onFocusin += value;
            RegisterEvent(EventHandles.Focusin);
        }
        remove { _onFocusin -= value; }
    }

    private event BootStrapBlazorEventHandler _onFocusout;

    public event BootStrapBlazorEventHandler OnFocusout
    {
        add
        {
            _onFocusout += value;
            RegisterEvent(EventHandles.Focusout);
        }
        remove { _onFocusout -= value; }
    }

    private event BootStrapBlazorEventHandler _onLoad;

    public event BootStrapBlazorEventHandler OnLoad
    {
        add
        {
            _onLoad += value;
            RegisterEvent(EventHandles.Load);
        }
        remove { _onLoad -= value; }
    }

    private event BootStrapBlazorEventHandler _onUnload;

    public event BootStrapBlazorEventHandler OnUnload
    {
        add
        {
            _onUnload += value;
            RegisterEvent(EventHandles.Unload);
        }
        remove { _onUnload -= value; }
    }

    private event BootStrapBlazorEventHandler _onBeforeunload;

    public event BootStrapBlazorEventHandler OnBeforeunload
    {
        add
        {
            _onBeforeunload += value;
            RegisterEvent(EventHandles.Beforeunload);
        }
        remove { _onBeforeunload -= value; }
    }

    private event BootStrapBlazorEventHandler _onResize;

    public event BootStrapBlazorEventHandler OnResize
    {
        add
        {
            _onResize += value;
            RegisterEvent(EventHandles.Resize);
        }
        remove { _onResize -= value; }
    }

    private event BootStrapBlazorEventHandler _onMove;

    public event BootStrapBlazorEventHandler OnMove
    {
        add
        {
            _onMove += value;
            RegisterEvent(EventHandles.Move);
        }
        remove { _onMove -= value; }
    }

    private event BootStrapBlazorEventHandler _onDOMContentLoaded;

    public event BootStrapBlazorEventHandler OnDOMCOntentLoaded
    {
        add
        {
            _onDOMContentLoaded += value;
            RegisterEvent(EventHandles.DOMContentLoaded);
        }
        remove { _onDOMContentLoaded -= value; }
    }

    private event BootStrapBlazorEventHandler _onReadystatechange;

    public event BootStrapBlazorEventHandler OnReadystatechange
    {
        add
        {
            _onReadystatechange += value;
            RegisterEvent(EventHandles.Readystatechange);
        }
        remove { _onReadystatechange -= value; }
    }

    private event BootStrapBlazorEventHandler _onError;

    public event BootStrapBlazorEventHandler OnError
    {
        add
        {
            _onError += value;
            RegisterEvent(EventHandles.Error);
        }
        remove { _onError -= value; }
    }

    private event BootStrapBlazorEventHandler _onAbort;

    public event BootStrapBlazorEventHandler OnAbort
    {
        add
        {
            _onAbort += value;
            RegisterEvent(EventHandles.Abort);
        }
        remove { _onAbort -= value; }
    }

    private event BootStrapBlazorEventHandler _onScroll;

    public event BootStrapBlazorEventHandler OnScroll
    {
        add
        {
            _onScroll += value;
            RegisterEvent(EventHandles.Scroll);
        }
        remove { _onScroll -= value; }
    }

    #endregion
}

/// <summary>
/// 
/// </summary>
public delegate void BootStrapBlazorEventHandler();

class EventHandlesConverter : JsonConverter<EventHandles>
{
    public override EventHandles Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        throw new NotImplementedException();
    }

    public override void Write(Utf8JsonWriter writer, EventHandles value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToDescriptionString());
    }
}

/// <summary>
/// 
/// </summary>
[JsonConverter(typeof(EventHandlesConverter))]
public enum EventHandles
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
