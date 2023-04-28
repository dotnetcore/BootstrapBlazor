// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// 浏览器事件,通用属性帮助类接口
/// </summary>
public interface IJSRuntimeEventHandler : IAsyncDisposable
{
    /// <summary>
    /// 注册浏览器事件
    /// </summary>
    /// <param name="eventName"></param>
    /// <returns></returns>
    ValueTask RegisterEvent(BootStrapBlazorEventType eventName);

    /// <summary>
    /// 通过tag获取元素的属性值
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="id"></param>
    /// <param name="tag"></param>
    /// <returns></returns>
    ValueTask<T?> GetElementPropertiesByTagFromIdAsync<T>(string id, string tag);

    /// <summary>
    /// 通过tag获取元素的属性值
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="tag"></param>
    /// <returns></returns>
    ValueTask<T?> GetDocumentPropertiesByTagAsync<T>(string tag);

    /// <summary>
    /// 通过tag获取元素的属性值
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="element"></param>
    /// <param name="tag"></param>
    /// <returns></returns>
    ValueTask<T?> GetElementPropertiesByTagAsync<T>(ElementReference element, string tag);

    /// <summary>
    /// 动态运行js脚本
    /// </summary>
    /// <param name="scripts">js代码</param>
    /// <returns></returns>
    ValueTask RunEval(string scripts);

    #region Event
    /// <summary>
    /// 鼠标点击时触发些事件
    /// </summary>
    event BootstrapBlazorJSRuntimeEventHandler OnClick;
    /// <summary>
    /// 在单个元素上单击两次鼠标的指针设备按钮时，将触发 dblclick 事件。
    /// </summary>
    event BootstrapBlazorJSRuntimeEventHandler OnDblclick;
    /// <summary>
    /// 当指针在元素中时， mouseup事件在指针设备按钮放开时触发。
    /// </summary>
    event BootstrapBlazorJSRuntimeEventHandler OnMouseup;
    /// <summary>
    /// 事件在指针设备按钮按下时触发。
    /// </summary>
    event BootstrapBlazorJSRuntimeEventHandler OnMousedown;
    /// <summary>
    /// 全局属性是指用于某个元素的“上下文菜单”的ID属性。上下文菜单是指在用户交互（例如右键点击）时出现的菜单。
    /// </summary>
    event BootstrapBlazorJSRuntimeEventHandler OnContextmenu;
    /// <summary>
    /// 过时且非标准的 mousewheel 事件在元素上异步触发，以在操作鼠标滚轮或类似设备时提供更新。
    /// </summary>
    event BootstrapBlazorJSRuntimeEventHandler OnMousewheel;
    /// <summary>
    /// 当操作鼠标滚轮或类似设备并且自上次事件以来累积滚动量超过 1 行或 1 页时，将异步触发 DOM 事件。它由 MouseScrollEvent 接口表示。
    /// </summary>
    event BootstrapBlazorJSRuntimeEventHandler OnDOMMouseScroll;
    /// <summary>
    /// 用来获取或设置当前元素的 mouseover 事件的事件处理函数。
    /// </summary>
    event BootstrapBlazorJSRuntimeEventHandler OnMouseover;
    /// <summary>
    /// 当移动指针设备（通常是鼠标），使指针不再包含在这个元素或其子元素中时，mouseout 事件被触发。
    /// </summary>
    event BootstrapBlazorJSRuntimeEventHandler OnMouseout;
    /// <summary>
    /// 当指针设备 ( 通常指鼠标 ) 在元素上移动时，mousemove 事件被触发。
    /// </summary>
    event BootstrapBlazorJSRuntimeEventHandler OnMousemove;
    /// <summary>
    /// 当文本内容选择将开始发生时触发的事件。
    /// </summary>
    event BootstrapBlazorJSRuntimeEventHandler OnSelectstart;
    /// <summary>
    /// 当文本内容选择结束时发生时触发的事件。
    /// </summary>
    event BootstrapBlazorJSRuntimeEventHandler OnSelectend;
    /// <summary>
    /// 当键盘上的某个键被按下时触发此事件
    /// </summary>
    event BootstrapBlazorJSRuntimeEventHandler OnKeydown;
    /// <summary>
    /// 当键盘上的某个键被按下并且释放时触发此事件
    /// </summary>
    event BootstrapBlazorJSRuntimeEventHandler OnKeypress;
    /// <summary>
    /// 当键盘上的某个键被按开时触发此事件
    /// </summary>
    event BootstrapBlazorJSRuntimeEventHandler OnKeyup;
    /// <summary>
    /// 事件在设备的纵横方向改变时触发。
    /// </summary>
    event BootstrapBlazorJSRuntimeEventHandler OnOrientationchange;
    /// <summary>
    /// 事件在一个或多个触点与触控设备表面接触时被触发。
    /// </summary>
    event BootstrapBlazorJSRuntimeEventHandler OnTouchstart;
    /// <summary>
    /// 事件在触点于触控平面上移动时触发。
    /// </summary>
    event BootstrapBlazorJSRuntimeEventHandler OnTouchmove;
    /// <summary>
    /// 当触点离开触控平面时触发touchend事件。
    /// </summary>
    event BootstrapBlazorJSRuntimeEventHandler OnTouchend;
    /// <summary>
    /// 事件在触点被中断时触发，中断方式基于特定实现而有所不同（例如，创建了太多的触点）。
    /// </summary>
    event BootstrapBlazorJSRuntimeEventHandler OnTouchcancel;
    /// <summary>
    /// 当指针变为活动状态时，将触发该事件。
    /// </summary>
    event BootstrapBlazorJSRuntimeEventHandler OnPointerdown;
    /// <summary>
    /// 一个global event handler(全局事件) pointermove 事件。
    /// </summary>
    event BootstrapBlazorJSRuntimeEventHandler OnPointermove;
    /// <summary>
    /// 当某指针不再活跃时触发。
    /// </summary>
    event BootstrapBlazorJSRuntimeEventHandler OnPointerup;
    /// <summary>
    /// 就像在Element或Window中点击类似，在某目标区域内，发生触点（鼠标指针，触摸等）行为时会触发源于 pointerleave 事件global event handler行为。这个事件本身属于 Pointer Events API 的一部分。
    /// </summary>
    event BootstrapBlazorJSRuntimeEventHandler OnPointerleave;
    /// <summary>
    /// 当浏览器认为某指针不会再生成新的后续事件时触发（例如某设备不再活跃）
    /// </summary>
    event BootstrapBlazorJSRuntimeEventHandler OnPointercancel;
    /// <summary>
    /// 当多个手指接触触摸表面时，将触发 Gesturestart 事件，从而启动新手势。在手势期间，将触发手势更改事件。手势结束后，将触发手势结束事件。
    /// </summary>
    event BootstrapBlazorJSRuntimeEventHandler OnGesturestart;
    /// <summary>
    /// 当数字在触摸手势期间移动时，将触发 Gesturechange 事件。
    /// </summary>
    event BootstrapBlazorJSRuntimeEventHandler OnGesturechange;
    /// <summary>
    /// 当不再有多个手指接触触摸表面时，将触发 Gestureend 事件，从而结束手势。
    /// </summary>
    event BootstrapBlazorJSRuntimeEventHandler OnGestureend;
    /// <summary>
    /// 当某个元素获得焦点时触发此事件
    /// </summary>
    event BootstrapBlazorJSRuntimeEventHandler OnFocus;
    /// <summary>
    /// 当元素失去焦点时，将触发模糊事件。事件不会冒泡，但后面的相关焦点事件会冒泡。
    /// </summary>
    event BootstrapBlazorJSRuntimeEventHandler OnBlur;
    /// <summary>
    /// 当用户修改元素的值时，将为input、select和textarea 元素触发该事件。与输入事件不同，不必为每次更改元素的 .changechangevalue
    /// </summary>
    event BootstrapBlazorJSRuntimeEventHandler OnChange;
    /// <summary>
    /// 当表单中RESET的属性被激发时触发此事件
    /// </summary>
    event BootstrapBlazorJSRuntimeEventHandler OnReset;
    /// <summary>
    /// 当文本内容被选择时的事件
    /// </summary>
    event BootstrapBlazorJSRuntimeEventHandler OnSelect;
    /// <summary>
    /// SubmitEvent 接口定义用于表示 HTML 表单的提交事件的对象。调用表单的提交操作时，将在表单处触发此事件。
    /// </summary>
    event BootstrapBlazorJSRuntimeEventHandler OnSubmit;
    /// <summary>
    /// 当元素在焦点事件之后获得焦点时，将触发 focusin 事件。这两个事件的不同之处在于泡沫，而没有。focusin focus
    /// </summary>
    event BootstrapBlazorJSRuntimeEventHandler OnFocusin;
    /// <summary>
    /// 在模糊事件之后，当元素失去焦点时，将触发焦点事件。这两个事件的不同之处在于泡沫，而没有。focusout blur
    /// </summary>
    event BootstrapBlazorJSRuntimeEventHandler OnFocusout;
    /// <summary>
    /// 加载整个页面（包括样式表、脚本、iframe 和图像等所有依赖资源）时，将触发 load 事件。 这与 DOMContentLoaded 相反，后者在加载页面 DOM 后立即触发，而无需等待资源完成加载。
    /// </summary>
    event BootstrapBlazorJSRuntimeEventHandler OnLoad;
    /// <summary>
    /// 卸载文档或子资源时，将触发 unload 事件。
    /// </summary>
    event BootstrapBlazorJSRuntimeEventHandler OnUnload;
    /// <summary>
    /// 当窗口、文档及其资源即将卸载时，将触发 beforeunload 事件。此时，文档仍然可见，并且事件仍可取消。
    /// </summary>
    event BootstrapBlazorJSRuntimeEventHandler OnBeforeunload;
    /// <summary>
    /// 当浏览器的窗口大小被改变时触发此事件
    /// </summary>
    event BootstrapBlazorJSRuntimeEventHandler OnResize;
    /// <summary>
    /// 浏览器的窗口被移动时触发此事件
    /// </summary>
    event BootstrapBlazorJSRuntimeEventHandler OnMove;
    /// <summary>
    /// 当 HTML 文档已完全解析，并且所有延迟脚本（和）都已下载并执行时，将触发 DOMContentLoaded 事件。它不会等待图像、子帧和异步脚本等其他内容完成加载。
    /// </summary>
    event BootstrapBlazorJSRuntimeEventHandler OnDOMContentLoaded;
    /// <summary>
    /// 当文档的 readyState 属性发生更改时，将触发 readystatechange 事件。
    /// </summary>
    event BootstrapBlazorJSRuntimeEventHandler OnReadystatechange;
    /// <summary>
    /// 运行错误触发onerror
    /// </summary>
    event BootstrapBlazorJSRuntimeEventHandler OnError;
    /// <summary>
    /// 该事件在多媒体数据终止加载时触发，而不是发生错误时触发。
    /// </summary>
    event BootstrapBlazorJSRuntimeEventHandler OnAbort;
    /// <summary>
    /// 滚动元素后将触发滚动事件。 若要检测滚动何时完成，请参阅 Element： scrollend 事件。
    /// 由于 scroll 事件可被高频触发，事件处理程序不应该执行高性能消耗的操作，如 DOM 操作。
    /// </summary>
    event BootstrapBlazorJSRuntimeEventHandler OnScroll;
    #endregion
}
