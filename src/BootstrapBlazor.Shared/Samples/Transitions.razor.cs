﻿// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Shared.Common;
using BootstrapBlazor.Shared.Components;

namespace BootstrapBlazor.Shared.Samples;

/// <summary>
///
/// </summary>
public partial class Transitions
{
    private bool Show { get; set; }

    [NotNull]
    private BlockLogger? Trace { get; set; }

    private void OnShow()
    {
        Show = true;
    }

    private Task OnShowEnd()
    {
        Show = false;
        StateHasChanged();
        return Task.CompletedTask;
    }

    private bool TransitionEndShow { get; set; }

    private void OnTransitionShow()
    {
        TransitionEndShow = true;
    }

    private Task OnTransitionEndShow()
    {
        TransitionEndShow = false;
        Trace.Log("动画结束");
        StateHasChanged();
        return Task.CompletedTask;
    }

    private bool FadeInShow { get; set; }

    private void OnFadeInShow()
    {
        FadeInShow = true;
    }

    private Task OnFadeInEndShow()
    {
        FadeInShow = false;
        StateHasChanged();
        return Task.CompletedTask;
    }

    private static IEnumerable<AttributeItem> GetAttributes() => new[]
    {
            // TODO: 移动到数据库中
            new AttributeItem() {
                Name = "TransitionType",
                Description = "动画效果名称",
                Type = "TransitionType",
                ValueList = "FadeIn/FadeOut/Bounce/Flash/Pulse/RubberBand/Shake/ShakeX/ShakeY/HeadShake/Swing/Tada/Wobble/Jello/BounceIn/BounceInDown/BounceInLeft/BounceInRight/BounceInUp/BounceOut/BounceOutDown/BounceOutLeft/BounceOutRight/BounceOutUp/FadeInDown/FadeInDownBig/FadeInLeft/FadeInLeftBig/FadeInRight/FadeInRightBig/FadeInUp/FadeInUpBig/FadeOutDown/FadeOutDownBig/FadeOutLeft/FadeOutLeftBig/FadeOutRight/FadeOutRightBig/FadeOutUp/FadeOutUpBig/FlipInX/FlipInY/FlipOutX/FlipOutY/LightSpeedIn/LightSpeedOut/LightSpeedInRight/LightSpeedInLeft/LightSpeedOutRight/LightSpeedOutLeft/RotateIn/RotateInDownLeft/RotateInDownRight/RotateInUpLeft/RotateInUpRight/RotateOut/RotateOutDownLeft/RotateOutDownRight/RotateOutUpLeft/RotateOutUpRight/Hinge/JackInTheBox/RollIn/RollOut/ZoomIn/ZoomInDown/ZoomInLeft/ZoomInRight/ZoomInUp/ZoomOut/ZoomOutDown/ZoomOutLeft/ZoomOutRight/ZoomOutUp/SlideInDown/SlideInLeft/SlideInRight/SlideInUp/SlideOutDown/SlideOutLeft/SlideOutRight/SlideOutUp/HeartBeat/FadeInTopLeft/FadeInTopRight/FadeInBottomLeft/FadeInBottomRight/FadeOutTopLeft/FadeOutTopRight/FadeOutBottomRight/FadeOutBottomLeft/BackOutDown/BackOutLeft/BackOutRight/BackOutUp",
                DefaultValue = "FadeIn"
            },
            new AttributeItem() {
                Name = "Show",
                Description = "控制动画执行",
                Type = "Boolean",
                ValueList = "true|false",
                DefaultValue = "true"
            },
            new AttributeItem() {
                Name = "Duration",
                Description = "控制动画时长",
                Type = "int",
                ValueList = " — ",
                DefaultValue = "0"
            },
            new AttributeItem() {
                Name = "OnTransitionEnd",
                Description = "动画执行完成回调",
                Type = "Func<Task>",
                ValueList = " — ",
                DefaultValue = " — "
            }
        };
}
