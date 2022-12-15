// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Components;
using BootstrapBlazor.Shared.Common;

namespace BootstrapBlazor.Shared.Samples;

/// <summary>
/// 
/// </summary>
public partial class VideoPlayers
{
    private string SourcesType = "video/mp4";
    private string SourcesUrl = "//vjs.zencdn.net/v/oceans.mp4";

    VideoPlayer? video1;

    List<string> VideoList = new List<string>
    {
        "https://rtvelivestream.akamaized.net/rtvesec/la1/la1_main.m3u8",
        "https://d2zihajmogu5jn.cloudfront.net/bipbop-advanced/bipbop_16x9_variant.m3u8",
        "https://test-streams.mux.dev/x36xhzz/x36xhzz.m3u8",
        "https://res.cloudinary.com/dannykeane/video/upload/sp_full_hd/q_80:qmax_90,ac_none/v1/dk-memoji-dark.m3u8",
        "https://devstreaming-cdn.apple.com/videos/streaming/examples/img_bipbop_adv_example_fmp4/master.m3u8",
        "https://moctobpltc-i.akamaihd.net/hls/live/571329/eight/playlist.m3u8",
        "https://cph-p2p-msl.akamaized.net/hls/live/2000341/test/master.m3u8",
        "https://demo.unified-streaming.com/k8s/features/stable/video/tears-of-steel/tears-of-steel.mp4/.m3u8",
        "https://diceyk6a7voy4.cloudfront.net/e78752a1-2e83-43fa-85ae-3d508be29366/hls/fitfest-sample-1_Ott_Hls_Ts_Avc_Aac_16x9_1280x720p_30Hz_6.0Mbps_qvbr.m3u8",

    };

    List<SelectedItem> Items = new List<SelectedItem>();

    /// <summary>
    /// OnInitialized 方法
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();
        for (int i = 0; i < VideoList.Count; i++)
        {
            Items.Add(new SelectedItem { Text = $"TestVideo{i}", Value = VideoList[i] });
        }
        Items.Add(new SelectedItem { Text = "Mp4", Value = "//vjs.zencdn.net/v/oceans.mp4" });
    }

    private async Task ChangeURL(SelectedItem e)
    {
        SourcesUrl = e.Value;
        SourcesType = e.Value.EndsWith("mp4") ? "video/mp4" : "application/x-mpegURL";
        StateHasChanged();
        await Apply();
    }

    private async Task Apply()
    {
        await video1.SetPoster("//vjs.zencdn.net/v/oceans.png");
        await video1.Reload(SourcesUrl, SourcesType);
    }

    /// <summary>
    /// 获得属性方法
    /// </summary>
    /// <returns></returns>
    protected IEnumerable<AttributeItem> GetAttributes() => new AttributeItem[]
    {
        // TODO: 移动到数据库中
        new AttributeItem() {
            Name = "SourcesUrl",
            Description = "资源地址",
            Type = "Stream?",
            ValueList = "-",
            DefaultValue = "null"
        },
        new AttributeItem() {
            Name = "SourcesType",
            Description = "资源类型,video/mp4, application/x-mpegURL, video/ogg .. ",
            Type = "string?",
            ValueList = "-",
            DefaultValue = "application/x-mpegURL"
        },
        new AttributeItem() {
            Name = "Width",
            Description = "宽度",
            Type = "int",
            ValueList = "-",
            DefaultValue = "300"
        },
        new AttributeItem() {
            Name = "Height",
            Description = "高度",
            Type = "int",
            ValueList = "-",
            DefaultValue = "200"
        },
        new AttributeItem() {
            Name = "Controls",
            Description = "显示控制条",
            Type = "bool",
            ValueList = "-",
            DefaultValue = "true"
        },
        new AttributeItem() {
            Name = "Autoplay",
            Description = "自动播放",
            Type = "bool",
            ValueList = "-",
            DefaultValue = "true"
        },
        new AttributeItem() {
            Name = "Poster",
            Description = "设置封面资源,相对或者绝对路径",
            Type = "bool",
            ValueList = "string?",
            DefaultValue = "true"
        },
        new AttributeItem() {
            Name = "Option",
            Description = "播放器选项, 不为空则优先使用播放器选项,否则使用参数构建",
            Type = "VideoPlayerOption",
            ValueList = "-",
            DefaultValue = "null"
        },
        new AttributeItem() {
            Name = "Reload(string? url, string? type)",
            Description = "切换播放资源",
            Type = "async Task",
            ValueList = "-",
            DefaultValue = "-"
        },
        new AttributeItem() {
            Name = "SetPoster(string? poster)",
            Description = "设置封面",
            Type = "async Task",
            ValueList = "-",
            DefaultValue = "-"
        },
        new AttributeItem() {
            Name = "OnError",
            Description = "错误回调",
            Type = "Func<string, Task>??",
            ValueList = "-",
            DefaultValue = "-"
        },
    };    

}
