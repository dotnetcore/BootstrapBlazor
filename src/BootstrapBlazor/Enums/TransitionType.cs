// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using System.ComponentModel;

namespace BootstrapBlazor;

/// <summary>
/// TransitionType 枚举类型
/// </summary>
public enum TransitionType
{
    /// <summary>
    /// 淡入效果
    /// </summary>
    [Description("animate__fadeIn")]
    FadeIn,

    /// <summary>
    /// 淡出效果
    /// </summary>
    [Description("animate__fadeOut")]
    FadeOut,

    /// <summary>
    /// 
    /// </summary>
    [Description("animate__bounce")]
    Bounce,

    /// <summary>
    /// 
    /// </summary>
    [Description("animate__flash")]
    Flash,

    /// <summary>
    /// 
    /// </summary>
    [Description("animate__pulse")]
    Pulse,

    /// <summary>
    /// 
    /// </summary>
    [Description("animate__rubberBand")]
    RubberBand,

    /// <summary>
    /// 
    /// </summary>
    [Description("animate__shake")]
    Shake,

    /// <summary>
    /// 
    /// </summary>
    [Description("animate__shakeX")]
    ShakeX,

    /// <summary>
    /// 
    /// </summary>
    [Description("animate__shakeY")]
    ShakeY,

    /// <summary>
    /// 
    /// </summary>
    [Description("animate__headShake")]
    HeadShake,

    /// <summary>
    /// 
    /// </summary>
    [Description("animate__swing")]
    Swing,

    /// <summary>
    /// 
    /// </summary>
    [Description("animate__tada")]
    Tada,

    /// <summary>
    /// 
    /// </summary>
    [Description("animate__wobble")]
    Wobble,

    /// <summary>
    /// 
    /// </summary>
    [Description("animate__jello")]
    Jello,

    /// <summary>
    /// 
    /// </summary>
    [Description("animate__bounceIn")]
    BounceIn,

    /// <summary>
    /// 
    /// </summary>
    [Description("animate__bounceInDown")]
    BounceInDown,

    /// <summary>
    /// 
    /// </summary>
    [Description("animate__bounceInLeft")]
    BounceInLeft,

    /// <summary>
    /// 
    /// </summary>
    [Description("animate__bounceInRight")]
    BounceInRight,

    /// <summary>
    /// 
    /// </summary>
    [Description("animate__bounceInUp")]
    BounceInUp,

    /// <summary>
    /// 
    /// </summary>
    [Description("animate__bounceOut")]
    BounceOut,

    /// <summary>
    /// 
    /// </summary>
    [Description("animate__bounceOutDown")]
    BounceOutDown,

    /// <summary>
    /// 
    /// </summary>
    [Description("animate__bounceOutLeft")]
    BounceOutLeft,

    /// <summary>
    /// 
    /// </summary>
    [Description("animate__bounceOutRight")]
    BounceOutRight,

    /// <summary>
    /// 
    /// </summary>
    [Description("animate__bounceOutUp")]
    BounceOutUp,

    /// <summary>
    /// 
    /// </summary>
    [Description("animate__fadeInDown")]
    FadeInDown,

    /// <summary>
    /// 
    /// </summary>
    [Description("animate__fadeInDownBig")]
    FadeInDownBig,

    /// <summary>
    /// 
    /// </summary>
    [Description("animate__fadeInLeft")]
    FadeInLeft,

    /// <summary>
    /// 
    /// </summary>
    [Description("animate__fadeInLeftBig")]
    FadeInLeftBig,

    /// <summary>
    /// 
    /// </summary>
    [Description("animate__fadeInRight")]
    FadeInRight,

    /// <summary>
    /// 
    /// </summary>
    [Description("animate__fadeInRightBig")]
    FadeInRightBig,

    /// <summary>
    /// 
    /// </summary>
    [Description("animate__fadeInUp")]
    FadeInUp,

    /// <summary>
    /// 
    /// </summary>
    [Description("animate__fadeInUpBig")]
    FadeInUpBig,

    /// <summary>
    /// 
    /// </summary>
    [Description("animate__fadeOutDown")]
    FadeOutDown,

    /// <summary>
    /// 
    /// </summary>
    [Description("animate__fadeOutDownBig")]
    FadeOutDownBig,

    /// <summary>
    /// 
    /// </summary>
    [Description("animate__fadeOutLeft")]
    FadeOutLeft,

    /// <summary>
    /// 
    /// </summary>
    [Description("animate__fadeOutLeftBig")]
    FadeOutLeftBig,

    /// <summary>
    /// 
    /// </summary>
    [Description("animate__fadeOutRight")]
    FadeOutRight,

    /// <summary>
    /// 
    /// </summary>
    [Description("animate__fadeOutRightBig")]
    FadeOutRightBig,

    /// <summary>
    /// 
    /// </summary>
    [Description("animate__fadeOutUp")]
    FadeOutUp,

    /// <summary>
    /// 
    /// </summary>
    [Description("animate__fadeOutUpBig")]
    FadeOutUpBig,

    /// <summary>
    /// 
    /// </summary>
    [Description("animate__flipInX")]
    FlipInX,

    /// <summary>
    /// 
    /// </summary>
    [Description("animate__flipInY")]
    FlipInY,

    /// <summary>
    /// 
    /// </summary>
    [Description("animate__flipOutX")]
    FlipOutX,

    /// <summary>
    /// 
    /// </summary>
    [Description("animate__flipOutY")]
    FlipOutY,

    /// <summary>
    /// 
    /// </summary>
    [Description("animate__lightSpeedIn")]
    LightSpeedIn,

    /// <summary>
    /// 
    /// </summary>
    [Description("animate__lightSpeedOut")]
    LightSpeedOut,

    /// <summary>
    /// 
    /// </summary>
    [Description("animate__lightSpeedInRight")]
    LightSpeedInRight,

    /// <summary>
    /// 
    /// </summary>
    [Description("animate__lightSpeedInLeft")]
    LightSpeedInLeft,

    /// <summary>
    /// 
    /// </summary>
    [Description("animate__lightSpeedOutRight")]
    LightSpeedOutRight,

    /// <summary>
    /// 
    /// </summary>
    [Description("animate__lightSpeedOutLeft")]
    LightSpeedOutLeft,

    /// <summary>
    /// 
    /// </summary>
    [Description("animate__rotateIn")]
    RotateIn,

    /// <summary>
    /// 
    /// </summary>
    [Description("animate__rotateInDownLeft")]
    RotateInDownLeft,

    /// <summary>
    /// 
    /// </summary>
    [Description("animate__rotateInDownRight")]
    RotateInDownRight,

    /// <summary>
    /// 
    /// </summary>
    [Description("animate__rotateInUpLeft")]
    RotateInUpLeft,

    /// <summary>
    /// 
    /// </summary>
    [Description("animate__rotateInUpRight")]
    RotateInUpRight,

    /// <summary>
    /// 
    /// </summary>
    [Description("animate__rotateOut")]
    RotateOut,

    /// <summary>
    /// 
    /// </summary>
    [Description("animate__rotateOutDownLeft")]
    RotateOutDownLeft,

    /// <summary>
    /// 
    /// </summary>
    [Description("animate__rotateOutDownRight")]
    RotateOutDownRight,

    /// <summary>
    /// 
    /// </summary>
    [Description("animate__rotateOutUpLeft")]
    RotateOutUpLeft,

    /// <summary>
    /// 
    /// </summary>
    [Description("animate__rotateOutUpRight")]
    RotateOutUpRight,

    /// <summary>
    /// 
    /// </summary>
    [Description("animate__hinge")]
    Hinge,

    /// <summary>
    /// 
    /// </summary>
    [Description("animate__jackInTheBox")]
    JackInTheBox,

    /// <summary>
    /// 
    /// </summary>
    [Description("animate__rollIn")]
    RollIn,

    /// <summary>
    /// 
    /// </summary>
    [Description("animate__rollOut")]
    RollOut,

    /// <summary>
    /// 
    /// </summary>
    [Description("animate__zoomIn")]
    ZoomIn,

    /// <summary>
    /// 
    /// </summary>
    [Description("animate__zoomInDown")]
    ZoomInDown,

    /// <summary>
    /// 
    /// </summary>
    [Description("animate__zoomInLeft")]
    ZoomInLeft,

    /// <summary>
    /// 
    /// </summary>
    [Description("animate__zoomInRight")]
    ZoomInRight,

    /// <summary>
    /// 
    /// </summary>
    [Description("animate__zoomInUp")]
    ZoomInUp,

    /// <summary>
    /// 
    /// </summary>
    [Description("animate__zoomOut")]
    ZoomOut,

    /// <summary>
    /// 
    /// </summary>
    [Description("animate__zoomOutDown")]
    ZoomOutDown,

    /// <summary>
    /// 
    /// </summary>
    [Description("animate__zoomOutLeft")]
    ZoomOutLeft,

    /// <summary>
    /// 
    /// </summary>
    [Description("animate__zoomOutRight")]
    ZoomOutRight,

    /// <summary>
    /// 
    /// </summary>
    [Description("animate__zoomOutUp")]
    ZoomOutUp,

    /// <summary>
    /// 
    /// </summary>
    [Description("animate__slideInDown")]
    SlideInDown,

    /// <summary>
    /// 
    /// </summary>
    [Description("animate__slideInLeft")]
    SlideInLeft,

    /// <summary>
    /// 
    /// </summary>
    [Description("animate__slideInRight")]
    SlideInRight,

    /// <summary>
    /// 
    /// </summary>
    [Description("animate__slideInUp")]
    SlideInUp,

    /// <summary>
    /// 
    /// </summary>
    [Description("animate__slideOutDown")]
    SlideOutDown,

    /// <summary>
    /// 
    /// </summary>
    [Description("animate__slideOutLeft")]
    SlideOutLeft,

    /// <summary>
    /// 
    /// </summary>
    [Description("animate__slideOutRight")]
    SlideOutRight,

    /// <summary>
    /// 
    /// </summary>
    [Description("animate__slideOutUp")]
    SlideOutUp,

    /// <summary>
    /// 
    /// </summary>
    [Description("animate__heartBeat")]
    HeartBeat,

    /// <summary>
    /// 
    /// </summary>
    [Description("animate__fadeInTopLeft")]
    FadeInTopLeft,

    /// <summary>
    /// 
    /// </summary>
    [Description("animate__fadeInTopRight")]
    FadeInTopRight,

    /// <summary>
    /// 
    /// </summary>
    [Description("animate__fadeInBottomLeft")]
    FadeInBottomLeft,

    /// <summary>
    /// 
    /// </summary>
    [Description("animate__fadeInBottomRight")]
    FadeInBottomRight,

    /// <summary>
    /// 
    /// </summary>
    [Description("animate__fadeOutTopLeft")]
    FadeOutTopLeft,

    /// <summary>
    /// 
    /// </summary>
    [Description("animate__fadeOutTopRight")]
    FadeOutTopRight,

    /// <summary>
    /// 
    /// </summary>
    [Description("animate__fadeOutBottomRight")]
    FadeOutBottomRight,

    /// <summary>
    /// 
    /// </summary>
    [Description("animate__fadeOutBottomLeft")]
    FadeOutBottomLeft,

    /// <summary>
    /// 
    /// </summary>
    [Description("animate__backOutDown")]
    BackOutDown,

    /// <summary>
    /// 
    /// </summary>
    [Description("animate__backOutLeft")]
    BackOutLeft,

    /// <summary>
    /// 
    /// </summary>
    [Description("animate__backOutRight")]
    BackOutRight,

    /// <summary>
    /// 
    /// </summary>
    [Description("animate__backOutU")]
    BackOutUp
}
