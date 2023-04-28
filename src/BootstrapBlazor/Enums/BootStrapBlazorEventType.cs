// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using System.ComponentModel;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace BootstrapBlazor.Components;
/// <summary>
/// 浏览器event事件枚举
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

[ExcludeFromCodeCoverage]
class EventHandlesConverter : JsonConverter<BootStrapBlazorEventType>
{
    public override BootStrapBlazorEventType Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => throw new NotImplementedException();

    public override void Write(Utf8JsonWriter writer, BootStrapBlazorEventType value, JsonSerializerOptions options) => writer.WriteStringValue(value.ToDescriptionString());
}
