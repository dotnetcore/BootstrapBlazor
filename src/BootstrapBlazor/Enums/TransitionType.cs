// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using System.ComponentModel;

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">TransitionType 枚举类型</para>
/// <para lang="en">TransitionType enumtype</para>
/// </summary>
public enum TransitionType
{
    /// <summary>
    /// <para lang="zh">淡入效果</para>
    /// <para lang="en">淡入效果</para>
    /// </summary>
    [Description("animate__fadeIn")]
    FadeIn,

    /// <summary>
    /// <para lang="zh">淡出效果</para>
    /// <para lang="en">淡出效果</para>
    /// </summary>
    [Description("animate__fadeOut")]
    FadeOut,

    /// <summary>
    /// <para lang="zh"></para>
    /// <para lang="en"></para>
    /// </summary>
    [Description("animate__bounce")]
    Bounce,

    /// <summary>
    /// <para lang="zh"></para>
    /// <para lang="en"></para>
    /// </summary>
    [Description("animate__flash")]
    Flash,

    /// <summary>
    /// <para lang="zh"></para>
    /// <para lang="en"></para>
    /// </summary>
    [Description("animate__pulse")]
    Pulse,

    /// <summary>
    /// <para lang="zh"></para>
    /// <para lang="en"></para>
    /// </summary>
    [Description("animate__rubberBand")]
    RubberBand,

    /// <summary>
    /// <para lang="zh"></para>
    /// <para lang="en"></para>
    /// </summary>
    [Description("animate__shake")]
    Shake,

    /// <summary>
    /// <para lang="zh"></para>
    /// <para lang="en"></para>
    /// </summary>
    [Description("animate__shakeX")]
    ShakeX,

    /// <summary>
    /// <para lang="zh"></para>
    /// <para lang="en"></para>
    /// </summary>
    [Description("animate__shakeY")]
    ShakeY,

    /// <summary>
    /// <para lang="zh"></para>
    /// <para lang="en"></para>
    /// </summary>
    [Description("animate__headShake")]
    HeadShake,

    /// <summary>
    /// <para lang="zh"></para>
    /// <para lang="en"></para>
    /// </summary>
    [Description("animate__swing")]
    Swing,

    /// <summary>
    /// <para lang="zh"></para>
    /// <para lang="en"></para>
    /// </summary>
    [Description("animate__tada")]
    Tada,

    /// <summary>
    /// <para lang="zh"></para>
    /// <para lang="en"></para>
    /// </summary>
    [Description("animate__wobble")]
    Wobble,

    /// <summary>
    /// <para lang="zh"></para>
    /// <para lang="en"></para>
    /// </summary>
    [Description("animate__jello")]
    Jello,

    /// <summary>
    /// <para lang="zh"></para>
    /// <para lang="en"></para>
    /// </summary>
    [Description("animate__bounceIn")]
    BounceIn,

    /// <summary>
    /// <para lang="zh"></para>
    /// <para lang="en"></para>
    /// </summary>
    [Description("animate__bounceInDown")]
    BounceInDown,

    /// <summary>
    /// <para lang="zh"></para>
    /// <para lang="en"></para>
    /// </summary>
    [Description("animate__bounceInLeft")]
    BounceInLeft,

    /// <summary>
    /// <para lang="zh"></para>
    /// <para lang="en"></para>
    /// </summary>
    [Description("animate__bounceInRight")]
    BounceInRight,

    /// <summary>
    /// <para lang="zh"></para>
    /// <para lang="en"></para>
    /// </summary>
    [Description("animate__bounceInUp")]
    BounceInUp,

    /// <summary>
    /// <para lang="zh"></para>
    /// <para lang="en"></para>
    /// </summary>
    [Description("animate__bounceOut")]
    BounceOut,

    /// <summary>
    /// <para lang="zh"></para>
    /// <para lang="en"></para>
    /// </summary>
    [Description("animate__bounceOutDown")]
    BounceOutDown,

    /// <summary>
    /// <para lang="zh"></para>
    /// <para lang="en"></para>
    /// </summary>
    [Description("animate__bounceOutLeft")]
    BounceOutLeft,

    /// <summary>
    /// <para lang="zh"></para>
    /// <para lang="en"></para>
    /// </summary>
    [Description("animate__bounceOutRight")]
    BounceOutRight,

    /// <summary>
    /// <para lang="zh"></para>
    /// <para lang="en"></para>
    /// </summary>
    [Description("animate__bounceOutUp")]
    BounceOutUp,

    /// <summary>
    /// <para lang="zh"></para>
    /// <para lang="en"></para>
    /// </summary>
    [Description("animate__fadeInDown")]
    FadeInDown,

    /// <summary>
    /// <para lang="zh"></para>
    /// <para lang="en"></para>
    /// </summary>
    [Description("animate__fadeInDownBig")]
    FadeInDownBig,

    /// <summary>
    /// <para lang="zh"></para>
    /// <para lang="en"></para>
    /// </summary>
    [Description("animate__fadeInLeft")]
    FadeInLeft,

    /// <summary>
    /// <para lang="zh"></para>
    /// <para lang="en"></para>
    /// </summary>
    [Description("animate__fadeInLeftBig")]
    FadeInLeftBig,

    /// <summary>
    /// <para lang="zh"></para>
    /// <para lang="en"></para>
    /// </summary>
    [Description("animate__fadeInRight")]
    FadeInRight,

    /// <summary>
    /// <para lang="zh"></para>
    /// <para lang="en"></para>
    /// </summary>
    [Description("animate__fadeInRightBig")]
    FadeInRightBig,

    /// <summary>
    /// <para lang="zh"></para>
    /// <para lang="en"></para>
    /// </summary>
    [Description("animate__fadeInUp")]
    FadeInUp,

    /// <summary>
    /// <para lang="zh"></para>
    /// <para lang="en"></para>
    /// </summary>
    [Description("animate__fadeInUpBig")]
    FadeInUpBig,

    /// <summary>
    /// <para lang="zh"></para>
    /// <para lang="en"></para>
    /// </summary>
    [Description("animate__fadeOutDown")]
    FadeOutDown,

    /// <summary>
    /// <para lang="zh"></para>
    /// <para lang="en"></para>
    /// </summary>
    [Description("animate__fadeOutDownBig")]
    FadeOutDownBig,

    /// <summary>
    /// <para lang="zh"></para>
    /// <para lang="en"></para>
    /// </summary>
    [Description("animate__fadeOutLeft")]
    FadeOutLeft,

    /// <summary>
    /// <para lang="zh"></para>
    /// <para lang="en"></para>
    /// </summary>
    [Description("animate__fadeOutLeftBig")]
    FadeOutLeftBig,

    /// <summary>
    /// <para lang="zh"></para>
    /// <para lang="en"></para>
    /// </summary>
    [Description("animate__fadeOutRight")]
    FadeOutRight,

    /// <summary>
    /// <para lang="zh"></para>
    /// <para lang="en"></para>
    /// </summary>
    [Description("animate__fadeOutRightBig")]
    FadeOutRightBig,

    /// <summary>
    /// <para lang="zh"></para>
    /// <para lang="en"></para>
    /// </summary>
    [Description("animate__fadeOutUp")]
    FadeOutUp,

    /// <summary>
    /// <para lang="zh"></para>
    /// <para lang="en"></para>
    /// </summary>
    [Description("animate__fadeOutUpBig")]
    FadeOutUpBig,

    /// <summary>
    /// <para lang="zh"></para>
    /// <para lang="en"></para>
    /// </summary>
    [Description("animate__flipInX")]
    FlipInX,

    /// <summary>
    /// <para lang="zh"></para>
    /// <para lang="en"></para>
    /// </summary>
    [Description("animate__flipInY")]
    FlipInY,

    /// <summary>
    /// <para lang="zh"></para>
    /// <para lang="en"></para>
    /// </summary>
    [Description("animate__flipOutX")]
    FlipOutX,

    /// <summary>
    /// <para lang="zh"></para>
    /// <para lang="en"></para>
    /// </summary>
    [Description("animate__flipOutY")]
    FlipOutY,

    /// <summary>
    /// <para lang="zh"></para>
    /// <para lang="en"></para>
    /// </summary>
    [Description("animate__lightSpeedIn")]
    LightSpeedIn,

    /// <summary>
    /// <para lang="zh"></para>
    /// <para lang="en"></para>
    /// </summary>
    [Description("animate__lightSpeedOut")]
    LightSpeedOut,

    /// <summary>
    /// <para lang="zh"></para>
    /// <para lang="en"></para>
    /// </summary>
    [Description("animate__lightSpeedInRight")]
    LightSpeedInRight,

    /// <summary>
    /// <para lang="zh"></para>
    /// <para lang="en"></para>
    /// </summary>
    [Description("animate__lightSpeedInLeft")]
    LightSpeedInLeft,

    /// <summary>
    /// <para lang="zh"></para>
    /// <para lang="en"></para>
    /// </summary>
    [Description("animate__lightSpeedOutRight")]
    LightSpeedOutRight,

    /// <summary>
    /// <para lang="zh"></para>
    /// <para lang="en"></para>
    /// </summary>
    [Description("animate__lightSpeedOutLeft")]
    LightSpeedOutLeft,

    /// <summary>
    /// <para lang="zh"></para>
    /// <para lang="en"></para>
    /// </summary>
    [Description("animate__rotateIn")]
    RotateIn,

    /// <summary>
    /// <para lang="zh"></para>
    /// <para lang="en"></para>
    /// </summary>
    [Description("animate__rotateInDownLeft")]
    RotateInDownLeft,

    /// <summary>
    /// <para lang="zh"></para>
    /// <para lang="en"></para>
    /// </summary>
    [Description("animate__rotateInDownRight")]
    RotateInDownRight,

    /// <summary>
    /// <para lang="zh"></para>
    /// <para lang="en"></para>
    /// </summary>
    [Description("animate__rotateInUpLeft")]
    RotateInUpLeft,

    /// <summary>
    /// <para lang="zh"></para>
    /// <para lang="en"></para>
    /// </summary>
    [Description("animate__rotateInUpRight")]
    RotateInUpRight,

    /// <summary>
    /// <para lang="zh"></para>
    /// <para lang="en"></para>
    /// </summary>
    [Description("animate__rotateOut")]
    RotateOut,

    /// <summary>
    /// <para lang="zh"></para>
    /// <para lang="en"></para>
    /// </summary>
    [Description("animate__rotateOutDownLeft")]
    RotateOutDownLeft,

    /// <summary>
    /// <para lang="zh"></para>
    /// <para lang="en"></para>
    /// </summary>
    [Description("animate__rotateOutDownRight")]
    RotateOutDownRight,

    /// <summary>
    /// <para lang="zh"></para>
    /// <para lang="en"></para>
    /// </summary>
    [Description("animate__rotateOutUpLeft")]
    RotateOutUpLeft,

    /// <summary>
    /// <para lang="zh"></para>
    /// <para lang="en"></para>
    /// </summary>
    [Description("animate__rotateOutUpRight")]
    RotateOutUpRight,

    /// <summary>
    /// <para lang="zh"></para>
    /// <para lang="en"></para>
    /// </summary>
    [Description("animate__hinge")]
    Hinge,

    /// <summary>
    /// <para lang="zh"></para>
    /// <para lang="en"></para>
    /// </summary>
    [Description("animate__jackInTheBox")]
    JackInTheBox,

    /// <summary>
    /// <para lang="zh"></para>
    /// <para lang="en"></para>
    /// </summary>
    [Description("animate__rollIn")]
    RollIn,

    /// <summary>
    /// <para lang="zh"></para>
    /// <para lang="en"></para>
    /// </summary>
    [Description("animate__rollOut")]
    RollOut,

    /// <summary>
    /// <para lang="zh"></para>
    /// <para lang="en"></para>
    /// </summary>
    [Description("animate__zoomIn")]
    ZoomIn,

    /// <summary>
    /// <para lang="zh"></para>
    /// <para lang="en"></para>
    /// </summary>
    [Description("animate__zoomInDown")]
    ZoomInDown,

    /// <summary>
    /// <para lang="zh"></para>
    /// <para lang="en"></para>
    /// </summary>
    [Description("animate__zoomInLeft")]
    ZoomInLeft,

    /// <summary>
    /// <para lang="zh"></para>
    /// <para lang="en"></para>
    /// </summary>
    [Description("animate__zoomInRight")]
    ZoomInRight,

    /// <summary>
    /// <para lang="zh"></para>
    /// <para lang="en"></para>
    /// </summary>
    [Description("animate__zoomInUp")]
    ZoomInUp,

    /// <summary>
    /// <para lang="zh"></para>
    /// <para lang="en"></para>
    /// </summary>
    [Description("animate__zoomOut")]
    ZoomOut,

    /// <summary>
    /// <para lang="zh"></para>
    /// <para lang="en"></para>
    /// </summary>
    [Description("animate__zoomOutDown")]
    ZoomOutDown,

    /// <summary>
    /// <para lang="zh"></para>
    /// <para lang="en"></para>
    /// </summary>
    [Description("animate__zoomOutLeft")]
    ZoomOutLeft,

    /// <summary>
    /// <para lang="zh"></para>
    /// <para lang="en"></para>
    /// </summary>
    [Description("animate__zoomOutRight")]
    ZoomOutRight,

    /// <summary>
    /// <para lang="zh"></para>
    /// <para lang="en"></para>
    /// </summary>
    [Description("animate__zoomOutUp")]
    ZoomOutUp,

    /// <summary>
    /// <para lang="zh"></para>
    /// <para lang="en"></para>
    /// </summary>
    [Description("animate__slideInDown")]
    SlideInDown,

    /// <summary>
    /// <para lang="zh"></para>
    /// <para lang="en"></para>
    /// </summary>
    [Description("animate__slideInLeft")]
    SlideInLeft,

    /// <summary>
    /// <para lang="zh"></para>
    /// <para lang="en"></para>
    /// </summary>
    [Description("animate__slideInRight")]
    SlideInRight,

    /// <summary>
    /// <para lang="zh"></para>
    /// <para lang="en"></para>
    /// </summary>
    [Description("animate__slideInUp")]
    SlideInUp,

    /// <summary>
    /// <para lang="zh"></para>
    /// <para lang="en"></para>
    /// </summary>
    [Description("animate__slideOutDown")]
    SlideOutDown,

    /// <summary>
    /// <para lang="zh"></para>
    /// <para lang="en"></para>
    /// </summary>
    [Description("animate__slideOutLeft")]
    SlideOutLeft,

    /// <summary>
    /// <para lang="zh"></para>
    /// <para lang="en"></para>
    /// </summary>
    [Description("animate__slideOutRight")]
    SlideOutRight,

    /// <summary>
    /// <para lang="zh"></para>
    /// <para lang="en"></para>
    /// </summary>
    [Description("animate__slideOutUp")]
    SlideOutUp,

    /// <summary>
    /// <para lang="zh"></para>
    /// <para lang="en"></para>
    /// </summary>
    [Description("animate__heartBeat")]
    HeartBeat,

    /// <summary>
    /// <para lang="zh"></para>
    /// <para lang="en"></para>
    /// </summary>
    [Description("animate__fadeInTopLeft")]
    FadeInTopLeft,

    /// <summary>
    /// <para lang="zh"></para>
    /// <para lang="en"></para>
    /// </summary>
    [Description("animate__fadeInTopRight")]
    FadeInTopRight,

    /// <summary>
    /// <para lang="zh"></para>
    /// <para lang="en"></para>
    /// </summary>
    [Description("animate__fadeInBottomLeft")]
    FadeInBottomLeft,

    /// <summary>
    /// <para lang="zh"></para>
    /// <para lang="en"></para>
    /// </summary>
    [Description("animate__fadeInBottomRight")]
    FadeInBottomRight,

    /// <summary>
    /// <para lang="zh"></para>
    /// <para lang="en"></para>
    /// </summary>
    [Description("animate__fadeOutTopLeft")]
    FadeOutTopLeft,

    /// <summary>
    /// <para lang="zh"></para>
    /// <para lang="en"></para>
    /// </summary>
    [Description("animate__fadeOutTopRight")]
    FadeOutTopRight,

    /// <summary>
    /// <para lang="zh"></para>
    /// <para lang="en"></para>
    /// </summary>
    [Description("animate__fadeOutBottomRight")]
    FadeOutBottomRight,

    /// <summary>
    /// <para lang="zh"></para>
    /// <para lang="en"></para>
    /// </summary>
    [Description("animate__fadeOutBottomLeft")]
    FadeOutBottomLeft,

    /// <summary>
    /// <para lang="zh"></para>
    /// <para lang="en"></para>
    /// </summary>
    [Description("animate__backOutDown")]
    BackOutDown,

    /// <summary>
    /// <para lang="zh"></para>
    /// <para lang="en"></para>
    /// </summary>
    [Description("animate__backOutLeft")]
    BackOutLeft,

    /// <summary>
    /// <para lang="zh"></para>
    /// <para lang="en"></para>
    /// </summary>
    [Description("animate__backOutRight")]
    BackOutRight,

    /// <summary>
    /// <para lang="zh"></para>
    /// <para lang="en"></para>
    /// </summary>
    [Description("animate__backOutU")]
    BackOutUp
}
