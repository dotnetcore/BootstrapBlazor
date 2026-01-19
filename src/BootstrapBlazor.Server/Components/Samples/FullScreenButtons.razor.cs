// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Server.Components.Samples;

/// <summary>
/// 全屏按钮代码示例
/// </summary>
public partial class FullScreenButtons
{
    /// <summary>
    /// GetAttributes
    /// </summary>
    /// <returns></returns>
    private static AttributeItem[] GetAttributes() =>
    [
        new()
        {
            Name = nameof(FullScreenButton.Icon),
            Description = "全屏图标",
            Type = "string",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = nameof(FullScreenButton.FullScreenExitIcon),
            Description = "退出全屏图标",
            Type = "string",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = nameof(FullScreenButton.TargetId),
            Description = "全屏元素 Id",
            Type = "string",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = nameof(FullScreenButton.Text),
            Description = "显示文字",
            Type = "string",
            ValueList = " — ",
            DefaultValue = " — "
        }
    ];
}
