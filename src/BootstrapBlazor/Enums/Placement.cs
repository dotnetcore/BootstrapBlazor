// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using System.ComponentModel;

namespace BootstrapBlazor.Components;

/// <summary>
///  <para lang="zh"></para>
///  <para lang="en"></para>
/// </summary>
public enum Placement
{
    /// <summary>
    ///  <para lang="zh"></para>
    ///  <para lang="en"></para>
    /// </summary>
    [Description("auto")]
    Auto,

    /// <summary>
    ///  <para lang="zh"></para>
    ///  <para lang="en"></para>
    /// </summary>
    [Description("top")]
    Top,

    /// <summary>
    ///  <para lang="zh"></para>
    ///  <para lang="en"></para>
    /// </summary>
    [Description("top-start")]
    TopStart,

    /// <summary>
    ///  <para lang="zh"></para>
    ///  <para lang="en"></para>
    /// </summary>
    [Description("top-center")]
    TopCenter,

    /// <summary>
    ///  <para lang="zh"></para>
    ///  <para lang="en"></para>
    /// </summary>
    [Description("top-end")]
    TopEnd,

    /// <summary>
    ///  <para lang="zh"></para>
    ///  <para lang="en"></para>
    /// </summary>
    [Description("middle")]
    Middle,

    /// <summary>
    ///  <para lang="zh"></para>
    ///  <para lang="en"></para>
    /// </summary>
    [Description("middle-start")]
    MiddleStart,

    /// <summary>
    ///  <para lang="zh"></para>
    ///  <para lang="en"></para>
    /// </summary>
    [Description("middle-center")]
    MiddleCenter,

    /// <summary>
    ///  <para lang="zh"></para>
    ///  <para lang="en"></para>
    /// </summary>
    [Description("middle-end")]
    MiddleEnd,

    /// <summary>
    ///  <para lang="zh"></para>
    ///  <para lang="en"></para>
    /// </summary>
    [Description("bottom")]
    Bottom,

    /// <summary>
    ///  <para lang="zh"></para>
    ///  <para lang="en"></para>
    /// </summary>
    [Description("bottom-start")]
    BottomStart,

    /// <summary>
    ///  <para lang="zh"></para>
    ///  <para lang="en"></para>
    /// </summary>
    [Description("bottom-center")]
    BottomCenter,

    /// <summary>
    ///  <para lang="zh"></para>
    ///  <para lang="en"></para>
    /// </summary>
    [Description("bottom-end")]
    BottomEnd,

    /// <summary>
    ///  <para lang="zh"></para>
    ///  <para lang="en"></para>
    /// </summary>
    [Description("left")]
    Left,

    /// <summary>
    ///  <para lang="zh"></para>
    ///  <para lang="en"></para>
    /// </summary>
    [Description("left-start")]
    LeftStart,

    /// <summary>
    ///  <para lang="zh"></para>
    ///  <para lang="en"></para>
    /// </summary>
    [Description("left-end")]
    LeftEnd,

    /// <summary>
    ///  <para lang="zh"></para>
    ///  <para lang="en"></para>
    /// </summary>
    [Description("right")]
    Right,

    /// <summary>
    ///  <para lang="zh"></para>
    ///  <para lang="en"></para>
    /// </summary>
    [Description("right-start")]
    RightStart,

    /// <summary>
    ///  <para lang="zh"></para>
    ///  <para lang="en"></para>
    /// </summary>
    [Description("right-end")]
    RightEnd,
}
