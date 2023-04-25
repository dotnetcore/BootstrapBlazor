// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Services;

/// <summary>
/// 浏览器事件,通用属性帮助类接口
/// </summary>
public interface IBootstrapBlazorHelper
{
    /// <summary>
    /// 注册浏览器事件
    /// </summary>
    /// <param name="eventType"></param>
    /// <param name="id"></param>
    /// <returns></returns>
    Task RegisterEvent(BootStrapBlazorEventType eventType, string? id);

    #region Event
    /// <summary>
    /// OnClick
    /// </summary>
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
