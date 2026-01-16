// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using System.ComponentModel;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace BootstrapBlazor.Components;
/// <summary>
///  <para lang="zh">浏览器event事件枚举</para>
///  <para lang="en">Browser Event Enum</para>
/// </summary>
[JsonConverter(typeof(DOMEventsConverter))]
public enum DOMEvents
{
    /// <summary>
    ///  <para lang="zh">Click 事件枚举</para>
    ///  <para lang="en">Click Event</para>
    /// </summary>
    [Description("click")]
    Click,
    /// <summary>
    ///  <para lang="zh">Dblclick 事件枚举</para>
    ///  <para lang="en">Dblclick Event</para>
    /// </summary>
    [Description("dblclick")]
    Dblclick,
    /// <summary>
    ///  <para lang="zh">Mouseup 事件枚举</para>
    ///  <para lang="en">Mouseup Event</para>
    /// </summary>
    [Description("mouseup")]
    Mouseup,
    /// <summary>
    ///  <para lang="zh">Mousedown 事件枚举</para>
    ///  <para lang="en">Mousedown Event</para>
    /// </summary>
    [Description("mousedown")]
    Mousedown,
    /// <summary>
    ///  <para lang="zh">Contextmenu 事件枚举</para>
    ///  <para lang="en">Contextmenu Event</para>
    /// </summary>
    [Description("contextmenu")]
    Contextmenu,
    /// <summary>
    ///  <para lang="zh">Mousewheel 事件枚举</para>
    ///  <para lang="en">Mousewheel Event</para>
    /// </summary>
    [Description("mousewheel")]
    Mousewheel,
    /// <summary>
    ///  <para lang="zh">DOMMouseScroll 事件枚举</para>
    ///  <para lang="en">DOMMouseScroll Event</para>
    /// </summary>
    [Description("dOMMouseScroll")]
    DOMMouseScroll,
    /// <summary>
    ///  <para lang="zh">Mouseover 事件枚举</para>
    ///  <para lang="en">Mouseover Event</para>
    /// </summary>
    [Description("mouseover")]
    Mouseover,
    /// <summary>
    ///  <para lang="zh">Mouseout 事件枚举</para>
    ///  <para lang="en">Mouseout Event</para>
    /// </summary>
    [Description("mouseout")]
    Mouseout,
    /// <summary>
    ///  <para lang="zh">Mousemove 事件枚举</para>
    ///  <para lang="en">Mousemove Event</para>
    /// </summary>
    [Description("mousemove")]
    Mousemove,
    /// <summary>
    ///  <para lang="zh">Selectstart 事件枚举</para>
    ///  <para lang="en">Selectstart Event</para>
    /// </summary>
    [Description("selectstart")]
    Selectstart,
    /// <summary>
    ///  <para lang="zh">Selectend 事件枚举</para>
    ///  <para lang="en">Selectend Event</para>
    /// </summary>
    [Description("selectend")]
    Selectend,
    /// <summary>
    ///  <para lang="zh">Keydown 事件枚举</para>
    ///  <para lang="en">Keydown Event</para>
    /// </summary>
    [Description("keydown")]
    Keydown,
    /// <summary>
    ///  <para lang="zh">Keypress 事件枚举</para>
    ///  <para lang="en">Keypress Event</para>
    /// </summary>
    [Description("keypress")]
    Keypress,
    /// <summary>
    ///  <para lang="zh">Keyup 事件枚举</para>
    ///  <para lang="en">Keyup Event</para>
    /// </summary>
    [Description("keyup")]
    Keyup,
    /// <summary>
    ///  <para lang="zh">Orientationchange 事件枚举</para>
    ///  <para lang="en">Orientationchange Event</para>
    /// </summary>
    [Description("orientationchange")]
    Orientationchange,
    /// <summary>
    ///  <para lang="zh">Touchstart 事件枚举</para>
    ///  <para lang="en">Touchstart Event</para>
    /// </summary>
    [Description("touchstart")]
    Touchstart,
    /// <summary>
    ///  <para lang="zh">Touchmove 事件枚举</para>
    ///  <para lang="en">Touchmove Event</para>
    /// </summary>
    [Description("touchmove")]
    Touchmove,
    /// <summary>
    ///  <para lang="zh">Touchend 事件枚举</para>
    ///  <para lang="en">Touchend Event</para>
    /// </summary>
    [Description("touchend")]
    Touchend,
    /// <summary>
    ///  <para lang="zh">Touchcancel 事件枚举</para>
    ///  <para lang="en">Touchcancel Event</para>
    /// </summary>
    [Description("touchcancel")]
    Touchcancel,
    /// <summary>
    ///  <para lang="zh">Pointerdown 事件枚举</para>
    ///  <para lang="en">Pointerdown Event</para>
    /// </summary>
    [Description("pointerdown")]
    Pointerdown,
    /// <summary>
    ///  <para lang="zh">Pointermove 事件枚举</para>
    ///  <para lang="en">Pointermove Event</para>
    /// </summary>
    [Description("pointermove")]
    Pointermove,
    /// <summary>
    ///  <para lang="zh">Pointerup 事件枚举</para>
    ///  <para lang="en">Pointerup Event</para>
    /// </summary>
    [Description("pointerup")]
    Pointerup,
    /// <summary>
    ///  <para lang="zh">Pointerleave 事件枚举</para>
    ///  <para lang="en">Pointerleave Event</para>
    /// </summary>
    [Description("pointerleave")]
    Pointerleave,
    /// <summary>
    ///  <para lang="zh">Pointercancel 事件枚举</para>
    ///  <para lang="en">Pointercancel Event</para>
    /// </summary>
    [Description("pointercancel")]
    Pointercancel,
    /// <summary>
    ///  <para lang="zh">Gesturestart 事件枚举</para>
    ///  <para lang="en">Gesturestart Event</para>
    /// </summary>
    [Description("gesturestart")]
    Gesturestart,
    /// <summary>
    ///  <para lang="zh">Gesturechange 事件枚举</para>
    ///  <para lang="en">Gesturechange Event</para>
    /// </summary>
    [Description("gesturechange")]
    Gesturechange,
    /// <summary>
    ///  <para lang="zh">Gestureend 事件枚举</para>
    ///  <para lang="en">Gestureend Event</para>
    /// </summary>
    [Description("gestureend")]
    Gestureend,
    /// <summary>
    ///  <para lang="zh">Focus 事件枚举</para>
    ///  <para lang="en">Focus Event</para>
    /// </summary>
    [Description("focus")]
    Focus,
    /// <summary>
    ///  <para lang="zh">Blur 事件枚举</para>
    ///  <para lang="en">Blur Event</para>
    /// </summary>
    [Description("blur")]
    Blur,
    /// <summary>
    ///  <para lang="zh">Change 事件枚举</para>
    ///  <para lang="en">Change Event</para>
    /// </summary>
    [Description("change")]
    Change,
    /// <summary>
    ///  <para lang="zh">Reset 事件枚举</para>
    ///  <para lang="en">Reset Event</para>
    /// </summary>
    [Description("reset")]
    Reset,
    /// <summary>
    ///  <para lang="zh">Select 事件枚举</para>
    ///  <para lang="en">Select Event</para>
    /// </summary>
    [Description("select")]
    Select,
    /// <summary>
    ///  <para lang="zh">Submit 事件枚举</para>
    ///  <para lang="en">Submit Event</para>
    /// </summary>
    [Description("submit")]
    Submit,
    /// <summary>
    ///  <para lang="zh">Focusin 事件枚举</para>
    ///  <para lang="en">Focusin Event</para>
    /// </summary>
    [Description("focusin")]
    Focusin,
    /// <summary>
    ///  <para lang="zh">Focusout 事件枚举</para>
    ///  <para lang="en">Focusout Event</para>
    /// </summary>
    [Description("focusout")]
    Focusout,
    /// <summary>
    ///  <para lang="zh">Load 事件枚举</para>
    ///  <para lang="en">Load Event</para>
    /// </summary>
    [Description("load")]
    Load,
    /// <summary>
    ///  <para lang="zh">Unload 事件枚举</para>
    ///  <para lang="en">Unload Event</para>
    /// </summary>
    [Description("unload")]
    Unload,
    /// <summary>
    ///  <para lang="zh">Beforeunload 事件枚举</para>
    ///  <para lang="en">Beforeunload Event</para>
    /// </summary>
    [Description("beforeunload")]
    Beforeunload,
    /// <summary>
    ///  <para lang="zh">Resize 事件枚举</para>
    ///  <para lang="en">Resize Event</para>
    /// </summary>
    [Description("resize")]
    Resize,
    /// <summary>
    ///  <para lang="zh">Move 事件枚举</para>
    ///  <para lang="en">Move Event</para>
    /// </summary>
    [Description("move")]
    Move,
    /// <summary>
    ///  <para lang="zh">DOMContentLoaded 事件枚举</para>
    ///  <para lang="en">DOMContentLoaded Event</para>
    /// </summary>
    [Description("dOMContentLoaded")]
    DOMContentLoaded,
    /// <summary>
    ///  <para lang="zh">Readystatechange 事件枚举</para>
    ///  <para lang="en">Readystatechange Event</para>
    /// </summary>
    [Description("readystatechange")]
    Readystatechange,
    /// <summary>
    ///  <para lang="zh">Error 事件枚举</para>
    ///  <para lang="en">Error Event</para>
    /// </summary>
    [Description("error")]
    Error,
    /// <summary>
    ///  <para lang="zh">Abort 事件枚举</para>
    ///  <para lang="en">Abort Event</para>
    /// </summary>
    [Description("abort")]
    Abort,
    /// <summary>
    ///  <para lang="zh">Scroll 事件枚举</para>
    ///  <para lang="en">Scroll Event</para>
    /// </summary>
    [Description("scroll")]
    Scroll
}

[ExcludeFromCodeCoverage]
class DOMEventsConverter : JsonConverter<DOMEvents>
{
    public override DOMEvents Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => throw new NotImplementedException();

    public override void Write(Utf8JsonWriter writer, DOMEvents value, JsonSerializerOptions options) => writer.WriteStringValue(value.ToDescriptionString());
}
