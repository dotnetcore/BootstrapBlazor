// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Shared.Samples;

/// <summary>
/// VideoPlayers
/// </summary>
public partial class VideoPlayers
{
    /// <summary>
    /// 获得属性方法
    /// </summary>
    /// <returns></returns>
    private IEnumerable<AttributeItem> GetAttributes() => new AttributeItem[]
    {
        // TODO: 移动到数据库中
        new() {
            Name = nameof(VideoPlayer.Url),
            Description = "资源地址",
            Type = "string",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new() {
            Name = nameof(VideoPlayer.MineType),
            Description = "资源类型,video/mp4, application/x-mpegURL, video/ogg .. ",
            Type = "string?",
            ValueList = "(见页脚)",
            DefaultValue = "application/x-mpegURL"
        },
        new() {
            Name = nameof(VideoPlayer.Width),
            Description = "宽度",
            Type = "int",
            ValueList = " — ",
            DefaultValue = "300"
        },
        new() {
            Name = nameof(VideoPlayer.Height),
            Description = "高度",
            Type = "int",
            ValueList = " — ",
            DefaultValue = "200"
        },
        new() {
            Name = nameof(VideoPlayer.Controls),
            Description = "显示控制条",
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "true"
        },
        new() {
            Name = nameof(VideoPlayer.Autoplay),
            Description = "自动播放",
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "true"
        },
        new() {
            Name = nameof(VideoPlayer.Poster),
            Description = "设置封面资源,相对或者绝对路径",
            Type = "string?",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new() {
            Name = "Reload(string url, string type)",
            Description = "切换播放资源",
            Type = "async Task",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new() {
            Name = "SetPoster(string poster)",
            Description = "设置封面",
            Type = "async Task",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new() {
            Name = "OnError",
            Description = "错误回调",
            Type = "Func<string, Task>?",
            ValueList = " — ",
            DefaultValue = " — "
        },
    };
}
