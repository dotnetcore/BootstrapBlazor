// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

partial class JSRuntimeEventHandler
{
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
}
