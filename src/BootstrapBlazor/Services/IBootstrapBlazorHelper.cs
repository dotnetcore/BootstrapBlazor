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

    Task<T> GetIdPropertieByNameAsync<T>(string id, string tag);

    Task<T> GetDocumentPropertieByNameAsync<T>(string tag);

    Task<T> GetElementPropertieByNameAsync<T>(ElementReference element, string tag);

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
